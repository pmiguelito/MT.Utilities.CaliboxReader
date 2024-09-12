using CaliboxLibrary;
using CaliboxLibrary.StateMachine;
using CaliboxLibrary.StateMachine.CopyUCChannel;
using MT.OneWire;
using MTcom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadCalibox.clConfig;
using static ReadCalibox.Handler;
using static STDhelper.clSTD;

namespace ReadCalibox
{
    public partial class UC_Channel : UserControl
    {
        /**************************************************************************************
        ** Constructor: Can be used for Panel Controls Load
        ***************************************************************************************/
        #region UC Instance
        private UC_Channel _Instance;
        public UC_Channel Instance
        {
            get
            {
                if (_Instance == null)
                { _Instance = new UC_Channel(ucCOM); }
                return _Instance;
            }
        }
        #endregion UC Instance

        #region Constructors
        /************************************************
         * FUNCTION:    Constructors
         * DESCRIPTION:
         ************************************************/
        public UC_Channel(UC_COM uc_COM)
        {
            InitializeComponent();
            this.ucCOM = uc_COM;
            this.ucCOM.BeM_Changed += new EventHandler(BeM_Changed);
            _Instance = this;
        }

        private void UC_Channel_Load(object sender, EventArgs e)
        {
            Init();
            Reset(true);
            _Instance = this;
        }
        #endregion

        #region Initialization
        /************************************************
         * FUNCTION:    Initialization
         * DESCRIPTION:
         ************************************************/
        void Init()
        {
            InitializeProcesses(ucCOM);

            Init_Design();
            Init_timStateEngine();
            //Init_timProcess();
            Init_timCalibration();
            RunningState_ADD(ucCOM.Ch_No, CH_State.notActive);

            try { Channel_Active_Change(ucCOM.ConnectionReady); } catch { _Active = false; }
            try { ucCOM.Readyness_Changed += new EventHandler(Channel_Activity_Changed); } catch { }
            try { Init_DGV(); } catch { }
            try { Init_Chart(); } catch { }
        }

        void Init_Design()
        {
            Init_Colors();
            Init_Design_Dimensions();
        }

        void Init_Colors()
        {
            Panel_Sep1.BackColor = MTcolors.MT_BackGround_HeaderFooter;
            Panel_Sep2.BackColor = MTcolors.MT_BackGround_HeaderFooter;
            Panel_Sep3.BackColor = MTcolors.MT_BackGround_HeaderFooter;
            Panel_Sep4.BackColor = MTcolors.MT_BackGround_HeaderFooter;

            Panel_Header.BackColor = MTcolors.MT_BackGround_Work;
            Panel_Button.BackColor = MTcolors.MT_BackGround_Work;
            Panel_ItemInfos.BackColor = MTcolors.MT_BackGround_Work;
            Panel_Info.BackColor = MTcolors.MT_BackGround_Work;
            Panel_Time.BackColor = MTcolors.MT_BackGround_Work;
        }

        void Init_Design_Dimensions()
        {
            AllocFont(_Lbl_Channel, 18, FontStyle.Bold);
            AllocFont(_Lbl_TAGno, 12, FontStyle.Bold);
            AllocFont(Lbl_TAGnoTitle, 12, FontStyle.Bold);
            AllocFont(_Btn_Start, 10, FontStyle.Bold);
        }

        #endregion

        #region ExternalComponents
        /************************************************
         * FUNCTION:    External Components
         * DESCRIPTION:
         ************************************************/
        private void InitializeProcesses(UC_COM com)
        {
            int channelNo = com.Ch_No;
            SetChannelNoLocal(channelNo);
            ProcessesUC = new ProcessesUC(com.Serialport, channelNo);
            ProcessesUC.StateChanged -= ProcessesUC_StateChanged;
            ProcessesUC.StateChanged += ProcessesUC_StateChanged;
            ProcessesUC.UImessage -= ProcessesUC_UImessage;
            ProcessesUC.UImessage += ProcessesUC_UImessage;
            ProcessesUC.DataReceived -= ProcessesUC_DataReceived;
            ProcessesUC.DataReceived += ProcessesUC_DataReceived;
            InitLogger(channelNo);
        }

        public ProcessesUC ProcessesUC { get; set; }
        public DeviceCom DeviceCom
        {
            get { return ProcessesUC.Device; }
            set { ProcessesUC.Device = value; }
        }
        public Logger Logger { get { return ProcessesUC?.Logger; } }

        /// <summary>
        /// Item Informations and Limits
        /// </summary>
        public ChannelValues ItemValues
        {
            get { return DeviceCom.ItemValues; }
            set { DeviceCom.ItemValues = value; }
        }
        #endregion

        #region Log
        /**************************************************************************************
        * Log Message:
        ***************************************************************************************/
        public string LogDirectory { get; set; }
        private const string _LogEnding = ".log";
        private void InitLogger(int chNo)
        {
            Logger.Path = Create_MeasLogPath(chNo.ToString());
            Logger.LogMeasDB_Active = Config_Initvalues.LogMeas_DB_Active;
            Logger.LogMeasPath_Active = Config_Initvalues.LogMeas_Path_Active;
            DeviceCom.Logger = Logger;
        }

        private string LogFile(string chNumber)
        {
            string fileName = "Calibox_CH" + chNumber.PadLeft(2, '0');
            var pathFull = Logger.CheckFileLenght(LogDirectory, fileName);
            return pathFull;
        }

        private string Create_MeasLogPath(string chNumber)
        {
            LogDirectory = Config_Initvalues.LogMeas_Path;
            return LogFile(chNumber);
        }

        private void StartLog()
        {
            Logger.Path = Create_MeasLogPath(ChannelNo.ToString());
            Logger.BeM = ucCOM.BeM;
            Logger.Odbc = ItemValues.ODBC_EK;
        }
        #endregion Log

        #region Communication
        /************************************************
         * FUNCTION:    Communication
         * DESCRIPTION:
         ************************************************/
        #region BasisInfos
        public UC_COM ucCOM;
        private SerialPort port { get { return ucCOM.Serialport; } }
        public string ChannelName { get { return ucCOM.Ch_NoTxt; } }
        private int _ChannelNo;
        public int ChannelNo
        {
            get { return _ChannelNo; }
            set { SetChannelNoLocal(value); }
        }
        private void SetChannelNoLocal(int chNo)
        {
            _Lbl_Channel.Text = chNo.ToString().PadLeft(2, '0');
            _ChannelNo = chNo;
        }
        #endregion

        #region Device Communication
        /**********************************************************
        * FUNCTION:     Device Communication
        * DESCRIPTION:  SerialPort
        ***********************************************************/
        private void Init_DeviceCom()
        {
            try
            {
                if (DeviceCom != null)
                {
                    //DeviceCom.DataReceived -= c_DeviceCom_DataReceived;
                    DeviceCom.MeasurementChanged -= c_DeviceCom_MeasurementChanged;
                    DeviceCom.ProgressChanged -= c_DeviceCom_ProgressChanged;
                }
                if (DeviceCom == null)
                {
                    DeviceCom = new DeviceCom(port, ChannelNo, isDebug: false);
                }
                if (DeviceCom.PortName != port.PortName)
                {
                    DeviceCom.ChangePort(port, ChannelNo);
                }
                //DeviceCom.DataReceived += c_DeviceCom_DataReceived;
                DeviceCom.MeasurementChanged += c_DeviceCom_MeasurementChanged;
                DeviceCom.ProgressChanged += c_DeviceCom_ProgressChanged;
            }
            catch (Exception ex)
            {
                Logger.Save(ex);
            }
        }

        private void c_DeviceCom_ProgressChanged(object sender, DeviceResponseValues e)
        {
            ShowToolTipLimits(e);
            Update_DGVprogress();
        }

        private void c_DeviceCom_MeasurementChanged(object sender, DeviceResponseValues e)
        {
            Chart_Update(e);
        }

        private void ProcessesUC_DataReceived(object sender, EventDataArgs e)
        {
            DeviceResponseValues data = e.DeviceResponseValue;
            bool noError = CheckPolarisationStatus(data);
            Logger.Save(data, ItemValues);
        }

        private bool CheckPolarisationStatus(DeviceResponseValues data)
        {
            if (data.OpCodeResp == OpCode.g100 || data.OpCodeResp == OpCode.G100)
            {
                if (data.IsBoxErrorError)
                {
                    if (data.BoxMode == BoxMode.Box_SensorCheckUpol_500 || data.BoxMode == BoxMode.Box_SensorCheckUpol_674)
                    {
                        State = gProcMain.error;
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #endregion

        #region ChannelState
        /************************************************
         * FUNCTION:    Channel States
         * DESCRIPTION:
         ************************************************/
        private bool _IsStarReady = false;
        public bool IsStartReady
        {
            get { return _IsStarReady; }
            set { StartReady_Change(value); }
        }
        private void StartReady_Change(bool enabled)
        {
            _IsStarReady = enabled;
            _Btn_Start.Enabled = enabled;
            _Btn_Start.BackColor = IsStartReady ? MTcolors.MT_rating_InWork_Active : MTcolors.MT_BackGround_White;
            _Btn_Start.ForeColor = IsStartReady ? MTcolors.MT_BackGround_White : Color.Black;
        }

        public bool IsStopped
        {
            get { return ProcessesUC.IsStopped; }
            set { ProcessesUC.IsStopped = value; }
        }

        public bool IsRunning
        {
            get { return ProcessesUC.IsRunning; }
            private set { Channel_Running_Change(value); }
        }

        private const string ButtonTEXT_Start = "Start";
        private const string ButtonTEXT_Cancel = "Cancel";

        public void Channel_Running_Change(bool running)
        {
            ProcessesUC.IsRunning = running;
            CH_State wk = CH_State.idle;
            if (running)
            {
                wk = CH_State.inWork;
                _Btn_Start.BackColor = MTcolors.MT_rating_Alert_Active;
                _Btn_Start.ForeColor = Color.Black;
                H_UC_Betrieb.Channel_started(ChannelNo);
            }
            else
            {
                if (IsStartReady)
                { IsStartReady = false; }
                wk = _CalQuality;
            }
            _Btn_Start.Text = !running ? ButtonTEXT_Start : ButtonTEXT_Cancel;
            if (IsStartReady && !running)
            {
                IsStartReady = false;
            }
            RunningState_Change(ChannelNo, wk);
            CH_WorkingStatusColors(wk);
        }

        private bool _Active;
        public bool Active
        {
            get { return _Active; }
            private set { Channel_Active_Change(value); }
        }

        private void Channel_Activity_Changed(object sender, EventArgs e)
        {
            Channel_Active_Change(ucCOM.ConnectionReady);
        }

        private void Channel_Active_Change(bool enabled)
        {
            _Active = enabled;
            IsStartReady = false;
            CH_State wk = enabled ? CH_State.active : CH_State.notActive;
            RunningState_Change(ChannelNo, wk);
            CH_WorkingStatusColors(wk);
        }

        private void CH_WorkingStatusColors(CH_State status)
        {
            Color col = MTcolors.MT_BackGround_Work;
            Color colSep = MTcolors.MT_BackGround_HeaderFooter;
            Color colorInfoBox = MTcolors.MT_BackGround_White;
            if (ItemValues != null)
            {
                if (ItemValues.ErrorDetected)
                {
                    if (ItemValues.test_ok)
                    {
                        ItemValues.ErrorDetected = !ItemValues.test_ok;
                    }
                }

                colorInfoBox = ItemValues.ErrorDetected ? MTcolors.MT_rating_Bad_Active : MTcolors.MT_BackGround_White;
                if (status == CH_State.idle && ItemValues.ErrorDetected) { status = CH_State.QualityBad; }
            }
            bool onlyCH = false;
            switch (status)
            {
                case CH_State.idle:
                    break;
                case CH_State.inWork:
                    colorInfoBox = MTcolors.MT_BackGround_White;
                    col = MTcolors.MT_rating_InWork_Active;
                    colSep = MTcolors.MT_BackGround_Work;
                    break;
                case CH_State.QualityBad:
                    col = MTcolors.MT_rating_Bad_Active;
                    break;
                case CH_State.QualityGood:
                    col = MTcolors.MT_rating_God_Active;
                    break;
                case CH_State.active:
                    this.Enabled = true;
                    _Btn_Start.Visible = true;
                    break;
                case CH_State.notActive:
                    this.Enabled = false;
                    _Btn_Start.Visible = false;
                    colSep = Color.SteelBlue;
                    break;
                case CH_State.error:
                    if (!onlyCH)
                    {
                        this.Enabled = true;
                        _Btn_Start.Visible = true;
                    }
                    col = MTcolors.MT_rating_Alert_Active;
                    //colSep = Color.SteelBlue;
                    break;
            }
            Tb_Info.BackColor = colorInfoBox;
            if (!onlyCH)
            {
                Panel_Header.BackColor = col;
                Panel_ItemInfos.BackColor = col;
                Panel_Time.BackColor = col;
                Panel_Info.BackColor = col;
                Panel_Button.BackColor = col;
                Panel_Sep1.BackColor = colSep;
                Panel_Sep2.BackColor = colSep;
                Panel_Sep3.BackColor = colSep;
                Panel_Sep4.BackColor = colSep;
                Chart_Measurement.BackColor = col;
                Chart_Measurement.ChartAreas[0].BackColor = col;
                Chart_Measurement.Update();
            }
        }

        private void BeM_Changed(object sender, EventArgs e)
        {
            foreach (var bem in Config_BeMsList)
            {
                if (bem.BeMname == ucCOM.BeM)
                {
                    ucCOM.Ref.m_ComPortDetails.Baudrate = bem.Baudrate;
                    ucCOM.Ref.m_ComPortDetails.ReadDelay = bem.BeMdelay;
                    ucCOM.Ref.m_ComPortDetails.ReadLine = bem.BeMreadLine;
                    break;
                }
            }
        }
        #endregion

        #region StartStop
        /************************************************
         * FUNCTION:    Measurement Start/Stop
         * DESCRIPTION:
         ************************************************/
        public string BeM_Selected;
        public int ReadDelay_Selected;

        public InternalProcesses InternalProcesses { get; set; }
        public bool Start()
        {
            if (ucCOM.Start(false))
            {
                IsStopped = false;
                Init_DeviceCom();

                /* muss ausgeführt werden bevor T&T Gui reseted wird */
                try { GUI_Infos(); } catch { }

                Channel_Running_Change(true);
                bool version1 = true;
                if (version1)
                {
                    /// Version 1 local stateMachine
                    _TimStateEngine.Start();
                    State = gProcMain.getReadyForTest;
                }
                else
                {
                    /// Version 2 test and in work
                    _TimStateEngine.Stop();
                    //InternalProcesses = new InternalProcesses(Logger, DeviceCom);
                    //InternalProcesses.Start();
                }
                return true;
            }
            else
            {
                Gui_Message(message: $"ERROR: ComPort {ucCOM.Ref.m_ComPortDetails.PortName} ist besetzt");
                Channel_Running_Change(false);
            }
            H_UC_Betrieb.Channel_started(); /*this cancel the other ready channels*/
            return false;
        }

        private const string ENDPROCESS = "****** END PROCESS *** END PROCESS *** END PROCESS *** END PROCESS *** END PROCESS ******";

        public void Stop(gProcMain current)
        {
            if (IsRunning)
            {
                try
                {
                    if (current != gProcMain.TestFinished)
                    {
                        IsStopped = true;
                    }
                    _WaitingDetails.Reset();

                    //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    //watch.Start();
                    //while (!_WaitingDetails.Answer_Received && watch.ElapsedMilliseconds < 5000)
                    //{
                    //    Thread.Sleep(200);
                    //    Application.DoEvents();
                    //}
                    //Thread.Sleep(1000);
                    //Application.DoEvents();

                    Logger.Save(null, OpCode.state, ENDPROCESS);
                    //if (port.IsOpen)
                    //{
                    //    port.DiscardOutBuffer();
                    //}
                    //Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Logger.Save(ex);
                    Gui_Message(message: ex.Message);
                }
                Channel_Running_Change(false);
            }
            //else
            //{
            //    State = gProcMain.stop_stateEngine;
            //}
            //_TimProcess.Stop();
            _TimCalibration.Stop();
            ucCOM.Stop();
            H_TT.SetFocus_Input();
        }
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            bool running = _Btn_Start.Text == ButtonTEXT_Start;
            if (running)
            {
                Start();
            }
            else
            {
                _WaitingDetails.CalibrationRunning = false;
                _StateMessageError = "User Cancelation";
                ProcessesUC.IsStopped = true;
                //_TimStateEngine.Stop();
                ProcessesUC.ErrorDetected = true;
                //if (_TimStateEngine.Enabled)
                //{
                //    Init_timStateEngine();
                //}

                State = gProcMain.error;
            }
            H_TT.SetFocus_Input();
        }
        #endregion

        #region Reset
        /**************************************************************************************
        ** GUI:     Reset Informations
        ***************************************************************************************/
        public void Reset(bool forceReset = false)
        {
            GUI_Infos_Reset(forceReset);
            Reset_onStart();
        }

        private void GUI_Infos_Reset(bool forceReset = false)
        {
            //if (ItemValues.tag_nr != 0 || forceReset)
            {
                DeviceCom.ClearData();

                Ttip.Active = false;
                _Lbl_TAGno.Text = "";
                _Lbl_Item.Text = "";
                _Lbl_Pdno.Text = "";
                _Lbl_User.Text = "";
                _Lbl_ProdType.Text = "";
                _Lbl_TimeStart.Text = "";
                _Lbl_TimeEnd.Text = "";
                _Lbl_PassNo.Text = "";
                _Lbl_CalMode.Text = "";
                Tb_Info.Text = "";

                ChB_Chart.Checked = false;
                ChB_Chart.Visible = false;
                Gui_Message();
            }
        }
        private void Reset_Processe()
        {
            if (ItemValues != null)
            {
                ItemValues.ErrorDetected = false;
            }
            IsStopped = false;
            _WaitForInit = false;

            ChartMeasurement.Reset();

            ProcessesUC.ResetStatesIndicators();

            //_ProcesseNr = 0;
            _CountCalib = 0;
            _CalQuality = CH_State.idle;
            //_ProcesseRunning = gProcMain.idle;
            //_ProcesseLast = gProcMain.wait;
        }

        private void Reset_onStart()
        {
            ProcessesUC.State_Last = gProcMain.idle;
            Reset_Processe();
            _StateMessageError = null;
            BeM_Selected = ucCOM.BeM;
            ReadDelay_Selected = ucCOM.Ref.m_ComPortDetails.ReadDelay;
            StartLog();
            Init_DGV();
        }
        #endregion Reset

        #region GUI Information
        /**************************************************************************************
        ** GUI:     Update channel Informations
        ***************************************************************************************/
        private ToolTip _Ttip;
        private ToolTip Ttip
        {
            get
            {
                if (_Ttip == null)
                {
                    _Ttip = new ToolTip()
                    {
                        Active = true,
                        UseAnimation = true,
                        UseFading = true,
                        ShowAlways = true,
                        //IsBalloon = true,
                        InitialDelay = 1000,
                        AutoPopDelay = 8000,
                        ReshowDelay = 500
                    };
                }
                return _Ttip;
            }
            set { _Ttip = value; }
        }

        public void GUI_Infos(bool reset = false)
        {
            Reset();
            if (!reset)
            {
                Reset_onStart();
                ItemValues = H_UC_Betrieb.Limits;

                /* values set in UC_Betrieb
                Limits.ODBC_EK = m_TT.ODBC_EK_Selected;
                Limits.ODBC_TT = m_TT.ODBC_TT_Selected;
                Limits.tag_nr = m_TT.tSensor.tag_nr;
                Limits.sensor_id = m_TT.tSensor.sensor_id;
                Limits.ProductionType_ID = m_TT.ProdType_ID;
                Limits.ProductionType_Desc = m_TT.ProdType_Desc;
                */

                ItemValues.UserName = H_TT.UserName;
                ItemValues.User_ID = H_TT.UserName_ID;

                //Limits.DeviceLimits = DeviceLimits;
                ItemValues.channel_no = ChannelNo;
                ItemValues.meas_time_start = DateTime.Now;

                _Lbl_TAGno.Text = ItemValues.tag_nr.ToString();
                _Lbl_Item.Text = ItemValues.item;
                _Lbl_Pdno.Text = ItemValues.pdno;
                _Lbl_User.Text = ItemValues.UserName;
                _Lbl_ProdType.Text = ItemValues.ProductionType_Desc;
                _Lbl_TimeStart.Text = ItemValues.meas_time_start.ToShortTimeString();
                _Lbl_TimeEnd.Text = ItemValues.meas_time_end_Theo.ToShortTimeString();
                _Lbl_PassNo.Text = ItemValues.pass_no.ToString();
                _Lbl_CalMode.Text = ItemValues.Cal_Desc;

                Tb_Info.Text = "";
                ChB_Chart.Checked = false;
                StartLog();

                ItemValues.LoadTemplate();
            }
        }

        public void ShowToolTip()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ShowToolTip(); }); return;
            }
            string tpTxt = $"SensorID:\t{ItemValues.sensor_id}\r\n" +
                           $"COMport:\t{ucCOM.Ref.m_ComPortDetails.PortName}\r\n" +
                           $"CalMode_ID:\t{ItemValues.CalMode_ID}\r\n" +
                           $"Technology_ID:\t{ItemValues.Technology_ID}\r\n";
            if (ItemValues.DeviceLimits != null)
            {
                tpTxt += $"Device FW:\t{ItemValues.DeviceLimits.FW_Version}\r\n" +
                            $"Device cop:\t{ItemValues.DeviceLimits.Compiled}";
            }
            Ttip.SetToolTip(_Lbl_Channel, tpTxt);
            Ttip.Active = true;
        }

        public void ShowToolTipLimits(DeviceResponseValues drv)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ShowToolTipLimits(drv); }); return;
            }
            try
            {
                DeviceCom.ItemValues.DeviceLimits.TryGetValue(drv.BoxMode, out var limits);

                string tpTxt = $"Title:\t{limits?.Title}\r\n" +
                               $"Current:\t\t{limits?.Current:0.00}\r\n" +
                               $"StdDev:\t\t{limits?.StdDev:0.00}\r\n" +
                               $"RawValue:\t{limits?.RawValue:0.00}\r\n" +
                               $"ErrorActive:\t{limits?.ErrorActive}\r\n" +
                                $"CalError:\t\t{limits?.CalError:0.00}\r\n" +
                                $"RawError:\t{limits?.RawError:0.00}\r\n" +
                               $"WepError:\t{limits?.WepError:0.00}\r\n";
                Ttip.SetToolTip(Chart_Measurement, tpTxt);
            }
            catch
            {

            }
        }
        #endregion

        #region StateEngine
        /**************************************************************************************
       'FUNCTION:    State Engine
       ****************************************************************************************/
        private System.Windows.Forms.Timer _TimStateEngine
        {
            get { return ProcessesUC.TimerStateEngine; }
            set { ProcessesUC.TimerStateEngine = value; }
        }

        private void ProcessesUC_StateChanged(object sender, gProcMain state)
        {
            StateSelection(state);
        }

        private void ProcessesUC_UImessage(object sender, EventMessageArgs args)
        {
            Gui_Message(args);
        }

        private void Init_timStateEngine()
        {
            ProcessesUC.Init_timStateEngine();
        }

        public gProcMain State
        {
            get { return ProcessesUC.State; }
            set { ProcessesUC.State = value; }
        }
        private bool _WaitForInit = false;

        /* MAIN PROCESSES *********************************************************************
        *  FUNCTION:    State Engine
        *               timStateEngine_Tick
        *               timProcesse_Tick to change process
        '**************************************************************************************/
        //clDeviceCom DeviceCom = new clDeviceCom();
        CH_State _CalQuality = CH_State.idle;

        string _StateMessageError = "";
        string _Attempts;

        private void StateSelection(gProcMain state)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { StateSelection(state); });
                return;
            }
            bool goToNext = false;
            gProcMain next = state;
            switch (state)
            {
                case gProcMain.getReadyForTest:
                    goToNext = GetReadyForTest(out next);
                    break;
                case gProcMain.StartProcess:
                    goToNext = StartProcess(out next);
                    break;
                case gProcMain.BoxIdentification:
                    goToNext = GetBoxIdentification(out next);
                    break;
                case gProcMain.BoxStatus:
                    goToNext = BoxStatus(out next);
                    break;
                case gProcMain.BoxReset:
                    goToNext = BoxReset(out next);
                    break;
                case gProcMain.TestFinished:
                    TestFinished();
                    break;
                case gProcMain.FWcheck:
                    goToNext = FWcheck(out next);
                    break;
                case gProcMain.SensorCheck:
                    goToNext = SensorCheck(out next);
                    break;
                case gProcMain.SensorPage00Read:
                    goToNext = SensorPage00Read(out next);
                    break;
                case gProcMain.SensorPage00Write:
                    goToNext = SensorPage00Write(out next);
                    break;
                case gProcMain.SensorPage01Read:
                    goToNext = SensorPage01Read(out next);
                    break;
                case gProcMain.SensorPage01Write:
                    goToNext = SensorPage01Write(out next);
                    break;
                case gProcMain.SensorPage02Read:
                    goToNext = SensorPage02Read(out next);
                    break;
                case gProcMain.SensorPage02Write:
                    goToNext = SensorPage02Write(out next);
                    break;
                case gProcMain.Calibration:
                    Calibration();
                    goto case gProcMain.wait;
                case gProcMain.CalibrationWork:
                    break;
                case gProcMain.wait:
                    break;
                case gProcMain.Bewertung:
                    Bewertung();
                    break;
                case gProcMain.DBinit:
                    goToNext = DBinit(out next);
                    break;
                case gProcMain.error:
                    if (DeviceCom.ResponseContainsError(out DeviceResponseValues resp))
                    {
                        Error(ProcessesUC.State_Last, resp);
                        break;
                    }
                    Error(ProcessesUC.State_Last, DeviceCom.ResponseLast);
                    break;
                case gProcMain.idle:
                    goto case gProcMain.wait;
                case gProcMain.stop_stateEngine:
                    break;
                default:
                    break;
            }

            if (ProcessesUC.State_Last != State)
            {
                ProcessesUC.State_Last = State;
            }
        }
        #endregion StateEngine

        /***************************************************************************************
        * FUNCTION:     State Engine
        *               Single Functions
        *               set nextProcess idle to get for next process
        *               timProcesse_Tick implement the change function
        ****************************************************************************************/
        #region StateEngineFunctions

        private BoxErrorMode FromState(bool state)
        {
            return state ? BoxErrorMode.Pass : BoxErrorMode.Error;
        }
        private BoxErrorMode FromState(int state)
        {
            if (state == 0)
            {
                return BoxErrorMode.NotChecked;
            }
            if (state == 1)
            {
                return BoxErrorMode.Error;
            }
            return BoxErrorMode.Pass;
        }

        private bool GetReadyForTest(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.getReadyForTest;
            ProcessesUC.ResetAllData();
            ProcessesUC.StartProcess(current, OpCode.state);
            ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
            return true;
        }

        private bool StartProcess(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.StartProcess;
            nextProcess = current;
            if (_WaitingDetails.ProcessCurrent == current)
            {
                if (_WaitingDetails.IsTimeExpired)
                {
                    if (current != _WaitingDetails.ProcessAfterWait)
                    {
                        nextProcess = _WaitingDetails.ProcessAfterWait;
                        return true;
                    }
                }
                return false;
            }

            ProcessesUC.State_ProcessRunning = State;
            Gui_Message(State);
            try
            {
                ProcessesUC.StartProcess(current, opCode: OpCode.state);
                SetUIProgress(ItemValues.CalMode.ToString(), value: ItemValues.Cal_Desc, errorcode: BoxErrorMode.Pass.Hex);
                //if (ItemValues.ErrorDetected == false)
                //{
                //    _TimProcess.Start();
                //}
                ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Save(ex);
            }
            finally
            {
            }
            return false;
        }

        /// <summary>
        /// Reset ProcessCounter at the function begin
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mode"></param>
        /// <param name="opCode"></param>
        /// <returns><see href="true"/> time expired or <see cref="ProcessCounter"/> is 0</returns>
        private bool StartWorkCounterReset(gProcMain current,
                                           BoxModeCodes code,
                                           BoxErrorMode mode,
                                           OpCode opCode)
        {
            try
            {
                if (ProcessesUC.ProcessSelected.Process != current)
                {
                    ProcessesUC.StartProcess(current, opCode);
                    SetUIProgress(code, mode);
                    return true;
                }
                bool expired = _WaitingDetails.TimeExpired();
                return expired;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Reset ProcessCounter at the function begin
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mode"></param>
        /// <param name="opCode"></param>
        /// <returns><see href="true"/> time expired or <see cref="ProcessCounter"/> is 0</returns>
        private bool StartWorkCounterReset(gProcMain current, OpCode opCode)
        {
            try
            {
                if (ProcessesUC.ProcessSelected.Process != current)
                {
                    ProcessesUC.StartProcess(current, opCode);
                    return true;
                }
                bool expired = _WaitingDetails.TimeExpired();
                return expired;
            }
            finally
            {

            }
        }

        private bool GetBoxIdentification(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.BoxIdentification;
            nextProcess = current;
            try
            {
                if (StartWorkCounterReset(current, BoxModeCodes.BoxIDStatus, BoxErrorMode.NotChecked, OpCode.RDBX) == false)
                {
                    return false;
                }
                bool boxFound = _WaitingDetails.Answer_Received;
                if (DeviceCom.ItemValues.CheckBoxFW(out string result))
                {
                    ShowToolTip();
                    SetUIProgress(BoxModeCodes.BoxIDStatus, BoxErrorMode.Pass, value: result);
                    ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                    return true;
                }
                if (ProcessesUC.ProcessCounterValues.IsRetry(current))
                {
                    ProcessesUC.CMD_Send(current, OpCode.BoxIdentification, 4000);
                    return false;
                }
                _StateMessageError = "Box Kommunikation oder Status";
                SetUIProgress(BoxModeCodes.BoxIDStatus, BoxErrorMode.Error, value: DeviceCom.ItemValues.BoxId.DeviceLimits.FW_Version);
                Logger.Save(State, OpCode.state, _StateMessageError);
                ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                return true;
            }
            finally
            {
            }
        }

        private bool BoxStatus(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.BoxStatus;
            nextProcess = current;
            try
            {
                if (StartWorkCounterReset(current, BoxModeCodes.BoxStatus, BoxErrorMode.NotChecked, OpCode.G100) == false)
                {
                    return false;
                }

                if (_WaitingDetails.Answer_Received)
                {
                    bool BoxFound = ItemValues.CheckBoxStatus(out string result);
                    ShowToolTip();
                    _WaitingDetails.Message = result;
                    BoxErrorMode error = FromState(BoxFound);
                    SetUIProgress(BoxModeCodes.BoxStatus, error, value: _WaitingDetails.Message);
                    if (BoxFound)
                    {
                        if (DeviceCom.IsFinalized)
                        {
                            DeviceCom.ResetError();
                        }
                        ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                        return true;
                    }
                    else
                    {
                        ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                        return true;
                    }
                }

                if (ProcessesUC.ProcessCounterValues.IsRetry(current))
                {
                    ProcessesUC.CMD_Send(current, CMD.G100);
                    return false;
                }

                SetUIProgress(BoxModeCodes.BoxStatus, BoxErrorMode.NoBoxCom);
                _StateMessageError = "Keine Box Kommunikation";
                Logger.Save(State, OpCode.state, _StateMessageError);
                ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                return true;

            }
            finally
            {
            }
        }

        private bool BoxReset(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.BoxReset;
            nextProcess = current;
            try
            {
                if (StartWorkCounterReset(current, BoxModeCodes.BoxReset, BoxErrorMode.NotChecked, OpCode.S999) == false)
                {
                    return false;
                }

                if (ProcessesUC.IsBoxReseted || ProcessesUC.ProcessSelected.AnswerReceived)
                {
                    SetUIProgress(BoxModeCodes.BoxReset, BoxErrorMode.Pass, value: _WaitingDetails.Message);
                    ShowToolTip();
                    ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                    return true;
                }

                if (ProcessesUC.ProcessCounterValues.IsRetry(current))
                {
                    ProcessesUC.CMD_Send(current, CMD.S999);
                    return false;
                }
                _StateMessageError = "Keine BOX Kommunikation";
                Logger.Save(State, OpCode.state, _StateMessageError);
                SetUIProgress(BoxModeCodes.BoxReset, BoxErrorMode.Error, value: _WaitingDetails.Message);
                ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                return true;
            }
            finally
            {
            }
        }

        private void TestFinished()
        {
            try
            {
                gProcMain current = gProcMain.TestFinished;
                ProcessesUC.StartProcess(current, OpCode.state);
                _TimCalibration.Stop();
                ProcessesUC.State_ProcessRunning = State;
                Stop(gProcMain.stop_stateEngine);
                ProcessesUC.StopAndNext(current, ProcState.Finished, out var next);
            }
            finally
            {
            }
        }

        private bool FWcheck(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.FWcheck;
            nextProcess = current;
            try
            {
                if (StartWorkCounterReset(current, BoxModeCodes.FWversion, FromState(ItemValues.sample_FW_Version_state), OpCode.G015) == false)
                {
                    return false;
                }
                string fwmessage = $"Sensor: {ItemValues.sample_FW_Version_value} DB: {ItemValues.sample_FW_Version} ";
                BoxErrorMode error = FromState(ItemValues.SampleFWVersion.State);
                if (_WaitingDetails.Answer_Received)
                {
                    SetUIProgress(BoxModeCodes.FWversion, error, ItemValues.sample_FW_Version_value);
                    if (ItemValues.sample_FW_Version_ok)
                    {
                        _WaitingDetails.Message = $"{_Attempts}\t{fwmessage}";
                        Logger.Save(ProcessesUC.State_ProcessRunning, OpCode.state, _WaitingDetails.Message);
                        ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                        return true;
                    }
                }
                _Attempts = ProcessesUC.ProcessCounterValues.GetAttemptMessage(current);
                _WaitingDetails.Message = $"{_Attempts}\t{fwmessage}";
                Gui_Message(State, message: _WaitingDetails.Message);

                if (ProcessesUC.ProcessCounterValues.IsRetry(current))
                {
                    ProcessesUC.CMD_Send(current, CMD.G015);
                    return false;
                }
                error = ItemValues.sample_FW_Version_ok ? BoxErrorMode.Pass : BoxErrorMode.Error;
                SetUIProgress(BoxModeCodes.FWversion, error, ItemValues.sample_FW_Version_value);
                _StateMessageError = $"{fwmessage}";
                ItemValues.ErrorDetected = true;
                ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                return true;
            }
            finally
            {
            }
        }

        private bool SensorCheck(out gProcMain nextProcess)
        {
            //gProcMain current = gProcMain.SensorCheck;
            SensorPage00Read(out nextProcess);
            return true;
        }

        #region Sensor Pages
        /**********************************************************
        * FUNCTION:     Sensor Pages
        * DESCRIPTION:
        ***********************************************************/
        private FrmSerialNumber CreateMsgPage0(int channelNo, TEDSHeaderValues header)
        {
            var title = $"Channel: {channelNo:00} / SerieNumer Eingabe:";
            var mainQuestion = "This will replace Page0 with default values." + Environment.NewLine + Environment.NewLine
                + $"PartNumber: {header.SensorItem}" + Environment.NewLine
                + $"SerialNumber: {header.SerialNum}" + Environment.NewLine + Environment.NewLine
                + $"Last Highest SerialNumber: {Config_ChangeParameters.ConverterHighestSN}" + Environment.NewLine;
            var question = title;
            var sn = header.SerialNum;
            if (int.TryParse(header.SerialNum, out int value))
            {
                if (value > 100000)
                {
                    sn = (Config_ChangeParameters.ConverterHighestSN + 1).ToString();
                }
            }

            var frm = new FrmSerialNumber(title, mainQuestion, question, sn);
            frm.BringToFront();
            return frm;
        }

        private bool SensorPage00Read(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.SensorPage00Read;
            nextProcess = current;
            if (StartWorkCounterReset(current, BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), OpCode.RDPG) == false)
            {
                return false;
            }
            string message = $"SN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            if (_WaitingDetails.Answer_Received == false)
            {
                if (ProcessesUC.ProcessSelected.IsRetry() == false)
                {
                    SetUIProgress(BoxModeCodes.SensorStatus, BoxErrorMode.Error, message);
                    ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                    return false;
                }
                ProcessesUC.CMD_Send(current, CMD.RDPG00);
                return false;
            }
            var pageData = ItemValues.TEDSpageDatas.Get(pageNo: 0);
            if (pageData.DataReceived == false)
            {
                ProcessesUC.CMD_Send(current, CMD.RDPG00);
                return false;
            }
            SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
            ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
            return true;
        }

        private bool SensorPage00Write(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.SensorPage00Write;
            nextProcess = current;

            if (StartWorkCounterReset(current, OpCode.WRPG) == false)
            {
                return false;
            }
            string message = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}\tSN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            var pageData = ItemValues.TEDSpageDatas[0];
            if (pageData.DataReceived)
            {
                var header = ItemValues.TEDSpageDatas.HeaderValues;
                bool isConverter = header.SensorItem == TdlData.PartNumber;

                if (!isConverter || header.SerialNum != ItemValues.sample_SN)
                {
                    _TimStateEngine.Stop();
                    var frm = CreateMsgPage0(ChannelNo, header);
                    var dialog = frm.ShowDialog();
                    ItemValues.sample_SN = frm.Value.PadLeft(7, '0');
                    if (!isConverter)
                    {
                        pageData.NewHEX(TdlData.Page00Default);
                        pageData.ReadHEX();
                        pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                    }
                    _TimStateEngine.Start();
                    switch (dialog)
                    {
                        case DialogResult.Abort:
                        case DialogResult.Cancel:
                            ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                            return true;
                        case DialogResult.OK:
                            var serialNumber = frm.Value.PadLeft(7, '0');

                            var properties = pageData.SensorPageDatas.SensorTemplate.GetPropertiesFromPageNo(0);
                            if (properties == null)
                            {
                                ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                                return true;
                            }
                            var prop = properties.FirstOrDefault(x => x.Property.ToLower().Contains("serial"));
                            if (prop == null)
                            {
                                ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                                return true;
                            }

                            if (int.TryParse(serialNumber, out int value))
                            {
                                if (value < 100000)
                                {
                                    if (value > Config_ChangeParameters.ConverterHighestSN)
                                    {
                                        Config_ChangeParameters.ConverterHighestSN = value;
                                        clConfig.Save();
                                    }
                                }
                            }
                            if (prop.Value == value.ToString())
                            {
                                ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                                return true;
                            }
                            prop.Value = serialNumber;
                            pageData.Update();
                            pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                            //_WaitingDetails.Reset(gProcMain.SensorPage00Read, OpCode.RDPG);
                            ProcessesUC.CMD_Send(current, CMD.WRPG00, data: pageData.HEX.RemoveSeparators());
                            //_WaitingDetails.ProcessAfterWait = gProcMain.SensorPage00Read;
                            break;
                    }
                }
                else if (int.TryParse(header.SerialNum, out int sn))
                {
                    if (sn > 0 && isConverter)
                    {
                        SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
                        ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                        return true;
                    }
                }
            }
            else
            {
                nextProcess = gProcMain.SensorPage00Read;
                return true;
            }
            return false;
        }

        private bool SensorPage01Read(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.SensorPage01Read;
            nextProcess = current;
            if (StartWorkCounterReset(current, OpCode.RDPG) == false)
            {
                return false;
            }
            string message = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}\tSN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            if (_WaitingDetails.Answer_Received == false)
            {
                if (ProcessesUC.ProcessSelected.IsRetry() == false)
                {
                    SetUIProgress(BoxModeCodes.SensorStatus, BoxErrorMode.Error, message);
                    ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                    return false;
                }
                ProcessesUC.CMD_Send(current, CMD.RDPG01);
                return false;
            }
            var pageData = ItemValues.TEDSpageDatas.Get(pageNo: 1);
            if (pageData.DataReceived == false)
            {
                ProcessesUC.CMD_Send(current, CMD.RDPG01);
                return false;
            }
            SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
            ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
            return true;
        }

        private bool SensorPage01Write(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.SensorPage01Write;
            nextProcess = current;

            if (StartWorkCounterReset(current, OpCode.WRPG) == false)
            {
                return false;
            }
            string message = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}\tSN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            var pageData = ItemValues.TEDSpageDatas.Get(pageNo: 1);
            if (pageData.DataReceived)
            {
                pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                var hex = pageData.HEX;
                var header = ItemValues.TEDSpageDatas.HeaderValues;
                DateTime.TryParse(header.FactoryCalDate, out DateTime date);
                if (date != DateTime.Today)
                {
                    if (pageData.GetProperty("FactoryCalDate", out var property))
                    {
                        try
                        {
                            property.Value = DateTime.Today.ToShortDateString();
                            pageData.Update();
                            pageData.SensorPageDatas.CheckHeaderValues();
                            if (hex != pageData.HEX)
                            {
                                ProcessesUC.CMD_Send(current, CMD.WRPG01, data: pageData.HEX.RemoveSeparators());
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
                    ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                    return true;
                }
            }
            return false;
        }

        private bool SensorPage02Read(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.SensorPage02Read;
            nextProcess = current;
            if (StartWorkCounterReset(current, OpCode.RDPG) == false)
            {
                return false;
            }
            string message = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}\tSN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            if (_WaitingDetails.Answer_Received == false)
            {
                if (ProcessesUC.ProcessSelected.IsRetry() == false)
                {
                    SetUIProgress(BoxModeCodes.SensorStatus, BoxErrorMode.Error, message);
                    ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                    return false;
                }
                ProcessesUC.CMD_Send(current, CMD.RDPG02);
                return false;
            }
            var pageData = ItemValues.TEDSpageDatas.Get(pageNo: 2);
            if (pageData.DataReceived == false)
            {
                ProcessesUC.CMD_Send(current, CMD.RDPG02);
                return false;
            }
            SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
            ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
            return true;
        }

        private bool SensorPage02Write(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.SensorPage02Write;
            nextProcess = current;
            if (StartWorkCounterReset(current, OpCode.WRPG) == false)
            {
                return false;
            }
            string message = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}\tSN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            var pageData = ItemValues.TEDSpageDatas.Get(pageNo: 2);
            if (pageData.DataReceived)
            {
                var hex = pageData.HEX;
                pageData.ReadHEX();
                pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                var header = ItemValues.TEDSpageDatas.HeaderValues;
                if (header.SensorItemDesc != TdlData.DescriptionDefault)
                {
                    _WaitingDetails.Reset(gProcMain.SensorPage02Read, OpCode.RDPG);
                    ProcessesUC.CMD_Send(current, CMD.WRPG02, data: TdlData.Page02Default.RemoveSeparators());
                    return false;
                }
                SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
                ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                return true;
            }
            return false;
        }
        #endregion

        private void Calibration()
        {
            gProcMain current = gProcMain.Calibration;
            if (StartWorkCounterReset(current, OpCode.g100) == false)
            {
                return;
            }
            _WaitingDetails.Answer_Received = false;
            if (ProcessesUC.State_ProcessRunning != current)
            {
                ProcessesUC.State_ProcessRunning = current;
                ResetCalibrationValues();
                ChartMeasurement.Activate(true);

                string message = $"Calibration Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}";
                _WaitingDetails.Message = message;

                _WaitingDetails.CalibrationRunning = true;
                _TimCalibration.Interval = 1000;
                _TimCalibration.Start();

                ProcessesUC.CMD_Send(current, CMD.GetOrAdd(ItemValues.CalMode));
                var boxMode = BoxMode.FromHex(ItemValues.CalMode);
                SetUIProgress(boxMode.BoxModeCode, BoxErrorMode.NotChecked, message);
            }
        }

        private void Bewertung()
        {
            var current = gProcMain.Bewertung;
            gProcMain next = current;
            if (StartWorkCounterReset(current, OpCode.g100) == false)
            {
                return;
            }
            _TimCalibration.Stop();
            ProcessesUC.State_ProcessRunning = State;
            ProcessesUC.IsTestRunning = false;
            ItemValues.meas_time_end = DateTime.Now;
            ItemValues.test_ok = _CalQuality == CH_State.QualityGood;
            string message = $"Calibration Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}\tSN: {ItemValues.sample_SN}";
            SetUIProgress(BoxModeCodes.SensorStatus, FromState(ItemValues.sample_SN_state), message);
            try
            {
                string state = ItemValues.test_ok ? "2" : "1";
                SetUIProgress(ItemValues.CalMode.ToString(), value: ItemValues.Cal_Desc, errorcode: state);
            }
            catch { }
            //if (!Save_Values_To_DB())
            try
            {
                if (!ItemValues.Save_DBmeasValues(DeviceCom.DT_Progress))
                {
                    _StateMessageError = "ERROR: DataBank speichern Bewertung";
                    Logger.Save(gProcMain.Bewertung, OpCode.state, _StateMessageError);
                    ProcessesUC.StopAndNext(current, ProcState.Aborted, out next);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Save(ex);
            }
            string quality = ItemValues.test_ok ? "Gut" : "Schlecht";
            message = $"Bewertung: {quality}\tduration: {ItemValues.meas_time_duration.ToStringMin()}\tSN: {ItemValues.sample_SN}";
            _WaitingDetails.Message = message;
            Logger.Save(gProcMain.Bewertung, OpCode.state, message);
            if (!ItemValues.ErrorDetected)
            {
                Gui_Message(message: message);
            }
            ProcessesUC.StopAndNext(current, ProcState.Finished, out next);
        }

        private bool DBinit(out gProcMain nextProcess)
        {
            gProcMain current = gProcMain.DBinit;
            nextProcess = gProcMain.DBinit;
            if (StartWorkCounterReset(current, OpCode.state) == false)
            {
                return false;
            }
            try
            {
                ProcessesUC.State_ProcessRunning = State;
                ProcessesUC.IsTestRunning = true;
                string message = $"sensorID: {ItemValues.sensor_id}\tTAG-Nr: {ItemValues.tag_nr}\tPassNo: {ItemValues.pass_no}\tSN: {ItemValues.sample_SN}";
                Logger.Save(current.ToString(), OpCode.state, message);
                if (ItemValues.Save_DBprocInit() == false)
                {
                    _StateMessageError = "ERROR: DataBank speichern INIT";
                    message += $" {_StateMessageError}";
                    ProcessesUC.StopAndNext(current, ProcState.Aborted, out nextProcess);
                    return false;
                }
                ProcessesUC.StopAndNext(current, ProcState.Finished, out nextProcess);
                return true;
            }
            finally
            {
            }
        }
        private void Error(gProcMain current, DeviceResponseValues drv)
        {
            _TimCalibration.Stop();
            IsRunning = false;
            string message = null;
            if (!string.IsNullOrEmpty(_StateMessageError))
            {
                message = $"ERROR: {_StateMessageError}\t{ProcessesUC.State_ProcessRunning}";
                Gui_Message(message);
            }
            if (!string.IsNullOrEmpty(ItemValues.ErrorMessage))
            {
                message = $"ERROR: {ItemValues.ErrorMessage}\t{ProcessesUC.State_ProcessRunning}";
                Gui_Message(message);
            }
            if (drv != null)
            {
                switch (drv.BoxErrorMode?.Hex)
                {
                    case null:
                    case "0":
                    case "00":
                        if (string.IsNullOrEmpty(message))
                        {
                            message = string.Empty;
                        }
                        break;
                    default:
                        ItemValues.ErrorDetected = true;
                        message = $"ERROR: {drv.BoxErrorMode.Hex} - {drv.BoxErrorMode.Desc}\t{ProcessesUC.State_ProcessRunning}";
                        Gui_Message(message);
                        break;
                }
            }
            if (message == null)
            {
                message = $"ERROR: ";
                Gui_Message(message);
            }
            ClearSelection();
            Logger.Save(State, OpCode.Error, message);
            if (ProcessesUC.IsTestRunning)
            {
                ProcessesUC.NextProcess(gProcMain.Bewertung);
            }
            else
            {
                Stop(gProcMain.idle);
                State = gProcMain.idle;
            }
        }
        #endregion StateEngineFunctions

        #region GUI Message
        /***************************************************************************************
        ** GUI Message:  Error
        ****************************************************************************************/
        string _Msg = string.Empty;
        private void GUI_TXT(string message, bool addMessage = false)
        {
            if (_Msg == message)
            {
                return;
            }
            _Msg = message;
            bool error = false;
            if (string.IsNullOrEmpty(message))
            { return; }

            bool isWait = false;

            error = message.ToLower().Contains("error:");
            message = message.Replace("\t", Environment.NewLine);

            if (addMessage)
            {
                string show = (message + Environment.NewLine + _Msg).Trim();
                int count = show.Split('\r').Length - 1;
                if (count < 20)
                {
                    GUI_Message(show.Trim());
                }
                else
                {
                    int length = show.LastIndexOf(Environment.NewLine);
                    message = show.Substring(0, length).Trim();
                }
            }
            else
            {
                message = message.Trim();
            }
            GUI_Message(message);
            if (error)
            {
                ItemValues.ErrorDetected = true;
            }
        }

        private void GUI_Message(string message)
        {
            try
            {
                if (Tb_Info.InvokeRequired)
                {
                    Tb_Info.Invoke(new Action(() => { GUI_Message(message); }));
                    return;
                }

                Tb_Info.Text = message;
                _Msg = Tb_Info.Text;
            }
            catch { }
        }

        #region Wait
        /*******************************************************************************************************************
        ** FUNCTION:    Wait
        ********************************************************************************************************************/
        public bool calibrationRunning = false;

        private WaitDetails _WaitingDetails
        {
            get { return ProcessesUC.WaitingDetails; }
        }

        public void Gui_Message(EventMessageArgs args)
        {
            string time = null;
            if (args.Time != TimeSpan.Zero)
            {
                time = args.Time.TimeDiff();
            }
            Gui_Message(time, args.State, args.Message, args.Attempts);
        }

        public void Gui_Message(string time,
                                gProcMain callerState = gProcMain.idle,
                                string message = null,
                                string attents = null)
        {
            StringBuilder sb = new StringBuilder();
            if (callerState != gProcMain.idle)
            {
                sb.Append(callerState.ToString());
            }
            sb.Append(Environment.NewLine);
            if (!string.IsNullOrEmpty(time))
            {
                sb.Append(time);
                sb.Append(Environment.NewLine);
            }

            if (!string.IsNullOrEmpty(attents))
            {
                sb.Append(attents);
                sb.Append(Environment.NewLine);
            }

            if (!string.IsNullOrEmpty(message))
            {
                sb.Append(message.Trim());
            }
            GUI_TXT(sb.ToString(), false);
        }

        public void Gui_Message(gProcMain callerState = gProcMain.idle, string message = null, string attents = null)
        {
            Gui_Message(null, callerState, message, attents);
        }
        #endregion

        #endregion GUI Message

        /*******************************************************************************************************************
        * FUNCTION:    Process Calibration Variables
        ********************************************************************************************************************/
        private System.Windows.Forms.Timer _TimCalibration;
        private void Init_timCalibration(bool start = false, int interval = 250)
        {
            _TimCalibration = new System.Windows.Forms.Timer() { Interval = interval };
            _TimCalibration.Tick += new EventHandler(TimCalibration_Tick);
            if (start)
            { _TimCalibration.Start(); }
        }

        /*******************************************************************************************************************
        *FUNCTION:    State Engine Calibration
        ********************************************************************************************************************/
        int _CountCalib = 0;
        int _CalibNoAnswerCounter = 0;
        bool _G901Receved = false;
        string _CalibLastMode = "";
        bool _S999Receved = false;
        public DateTime CalibrationStartTime { get; set; }
        private void ResetCalibrationValues()
        {
            _CountCalib = 0;
            _CalibNoAnswerCounter = 0;
            _G901Receved = false;
            _CalibLastMode = string.Empty;
            _S999Receved = false;
            CalibrationStartTime = DateTime.Now;
        }

        public void TimCalibration_Tick(object sender, EventArgs e)
        {
            GetLastResponseCalibration(out var responses, out DeviceResponseValues last);
            try
            {
                if (AnswerCheck(last))
                {
                    if (State != gProcMain.CalibrationWork)
                    {
                        State = gProcMain.CalibrationWork;
                    }
                    _CalibNoAnswerCounter = 0;
                }
                else
                {
                    _CalibNoAnswerCounter++;
                }
            }
            catch { }
            try
            {
                CheckMeasurement(last);
            }
            catch { }
            try
            {
                CheckFinalize(State, responses, last);
            }
            catch { }
        }

        private bool GetLastResponseCalibration(out IEnumerable<DeviceResponseValues> responses, out DeviceResponseValues last)
        {
            responses = DeviceCom.GetAfter(CalibrationStartTime);
            if (responses.Any())
            {
                last = responses.LastOrDefault();
                return true;
            }
            last = null;
            return false;
        }

        private void CheckMeasurement(DeviceResponseValues drv)
        {
            if (State == gProcMain.Calibration || State == gProcMain.CalibrationWork)
            {
                if (_CountCalib == 0)
                {
                    _TimCalibration.Interval = 1000;
                    calibrationRunning = true;
                    Task.Run(() =>
                    {
                        Task.Delay(12_000).Wait();
                        ProcessesUC.CMD_Send(State, CMD.G901);
                    });
                    _CountCalib++;
                    return;
                }
                _CountCalib++;
                if (_G901Receved == false)
                {
                    if (_CalibNoAnswerCounter > 30)
                    {
                        ProcessesUC.CMD_Send(State, CMD.G901);
                        _CalibNoAnswerCounter = 0;
                    }
                }
                if (drv?.BoxMode.Desc != _CalibLastMode)
                {
                    ItemValues.Cal_BoxMode_desc = drv?.BoxMode.Desc;
                    ItemValues.Cal_Stopwatch = new System.Diagnostics.Stopwatch();
                    ItemValues.Cal_Stopwatch.Start();
                    _CalibLastMode = drv?.BoxMode.Desc;
                    _CountCalib = 1;
                }
            }
        }

        private bool CheckFinalize(gProcMain current, IEnumerable<DeviceResponseValues> responses, DeviceResponseValues last)
        {
            bool isError = ItemValues.ErrorDetected == true || _S999Receved == true;
            bool isFinalized = false;
            if (DeviceCom.ResponseContainsError(responses, out DeviceResponseValues drv))
            {
                _S999Receved = true;
                isError = true;
                last = drv;
            }
            else
            {
                if (DeviceCom.ResponseContainsFinalized(responses, out drv))
                {
                    last = drv;
                    isFinalized = true;
                }
            }
            if (isFinalized || isError || _CalibNoAnswerCounter > 200)
            {
                //_TimCalibration.Stop();
                ChartMeasurement.SetVisible(false);

                if (last?.OpCodeResp == OpCode.s999)
                {
                    IsStopped = true;
                    _StateMessageError = "Sensor Kommunikation abgebrochen";
                    ItemValues.test_ok = false;
                    _CalQuality = CH_State.QualityBad;
                    ItemValues.ErrorDetected = true;
                    ItemValues.ErrorMessage = _StateMessageError;
                    last.BoxErrorMode = BoxErrorMode.NoSensCom;
                    DeviceCom.OnErrorReceived(last);
                }
                if (isError == false)
                {
                    ItemValues.test_ok = true;
                    _CalQuality = CH_State.QualityGood;
                    Logger.Save(State, OpCode.state, $"SUBPROCESS QUALITY GOOD {_StateMessageError}");
                    ProcessesUC.StopAndNext(gProcMain.Calibration, ProcState.Finished, out var nextProcess);
                }
                else if (IsStopped)
                {
                    State = gProcMain.TestFinished;
                    _TimCalibration.Stop();
                }
                else
                {
                    IsStopped = true;
                    ItemValues.ErrorDetected = true;
                    ItemValues.ErrorMessage = _StateMessageError;
                    _CalQuality = CH_State.QualityBad;
                    ItemValues.test_ok = false;
                    Logger.Save(State, OpCode.state, $"SUBPROCESS ERROR {_StateMessageError}");
                    ProcessesUC.CMD_Send(current, CMD.G200);
                    _WaitingDetails.CalibrationRunning = false;
                    //_TimCalibration.Stop();
                }
                return false;
            }
            return true;
        }

        private bool AnswerCheck(DeviceResponseValues responseValues)
        {
            if (responseValues == null)
            {
                return false;
            }
            var ts = DateTime.Now - responseValues.meas_time_start;
            if (ts.TotalSeconds > 5)
            {
                return false;
            }

            if (responseValues.OpCodeResponse.OpCode == OpCode.s999)
            {
                _S999Receved = true;
                return true;
            }
            if (responseValues.OpCodeResponse.OpCode == OpCode.g901)
            {
                _G901Receved = true;
                return true;
            }
            if (responseValues.OpCodeResponse.OpCode == OpCode.g100)
            {
                return true;
            }
            return false;
        }

        private bool Answer(IEnumerable<DeviceResponseValues> responses, out DeviceResponseValues result)
        {
            if (AnswerLast(responses, OpCode.s999, out result))
            {
                _S999Receved = true;
                return true;
            }
            if (AnswerLast(responses, OpCode.g901, out result))
            {
                _G901Receved = true;
                return true;
            }
            if (AnswerLast(responses, OpCode.g100, out result))
            {
                return true;
            }

            return false;
        }

        private bool AnswerFirst(IEnumerable<DeviceResponseValues> responses, OpCode cmd, out DeviceResponseValues result)
        {
            try
            {
                result = responses.First(x => x.OpCodeResponse.OpCode == cmd);
            }
            catch { }
            result = null;
            return false;
        }
        private bool AnswerLast(IEnumerable<DeviceResponseValues> responses, OpCode cmd, out DeviceResponseValues result)
        {
            try
            {
                result = responses.Last(x => x.OpCodeResponse.OpCode == cmd);
            }
            catch { }
            result = null;
            return false;
        }

        #region ProgressUI
        /************************************************
         * FUNCTION:    Progress
         * DESCRIPTION: Progress Show and set
         *              Show values to DataGridView
         ************************************************/
        private void SetUIProgress(BoxModeCodes code, BoxErrorMode error, string value = null)
        {
            DeviceCom.SetUIProgressAsync(code, error, value);
        }

        private void SetUIProgress(string boxmode_hex, string value = null, string errorcode = null)
        {
            //if (string.IsNullOrEmpty(boxmode_desc)) { boxmode_desc = boxmode_hex; }
            DeviceCom.SetUIProgressAsync(boxmode_hex, value, errorcode);
        }

        private void Init_DGV()
        {
            //if (SampleResponse == null) { SampleResponse = new clDeviceResponse(this); }
            try
            {
                DGV_Progress.BackgroundColor = MTcolors.MT_BackGround_Work;
                DGV_Progress.DataSource = DeviceCom.DT_Progress;
                Font n = new Font("Tahoma", 6, FontStyle.Regular);
                DGV_Progress.ColumnHeadersDefaultCellStyle.Font = n;
                n = new Font("Tahoma", 7, FontStyle.Regular);
                DGV_Progress.RowsDefaultCellStyle.Font = n;
                DGV_Progress.MultiSelect = false;
                DGV_Progress.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DGV_Progress.ReadOnly = true;
                Progress_AdminModus(DGV_Progress, showAllInfos: false);
                DGV_Progress.Sort(DGV_Progress.Columns[MN_meas_time_start], ListSortDirection.Descending);
            }
            catch
            { }
        }
        public void Update_DGVprogress()
        {
            try
            {
                if (DGV_Progress.InvokeRequired)
                {
                    DGV_Progress.Invoke((MethodInvoker)delegate { Update_DGVprogress(); });
                    return;
                }
                if (DGV_Progress.Rows.Count > 0)
                {
                    foreach (DataGridViewRow item in DGV_Progress.Rows)
                    {
                        item.Selected = false;
                        var value = item.Cells[MN_BoxErrorCode_HEX].Value;
                        if (value != null)
                        {
                            switch (value.ToString())
                            {
                                case "":
                                case "0":
                                    break;
                                case "00":
                                case "2":
                                    try { item.DefaultCellStyle.BackColor = MT_Colors[MTfonts.clMTcolors.selection.rating_GOOD_active]; }
                                    catch { }
                                    break;
                                default:
                                    try { item.DefaultCellStyle.BackColor = MT_Colors[MTfonts.clMTcolors.selection.rating_BAD_active]; }
                                    catch { }
                                    break;
                            }
                        }
                    }
                    DGV_Progress.Sort(DGV_Progress.Columns[MN_meas_time_start], ListSortDirection.Descending);
                    DGV_Progress.Rows[0].Selected = true;
                    DGV_Progress.FirstDisplayedScrollingRowIndex = 0;
                    DGV_Progress.Refresh();
                    if (IsStopped)
                    {
                        DGV_Progress.ClearSelection();
                    }
                }
            }
            catch { }
        }

        private void ClearSelection()
        {
            if (DGV_Progress.InvokeRequired)
            {
                DGV_Progress.Invoke((MethodInvoker)delegate { ClearSelection(); });
                return;
            }
            DGV_Progress.ClearSelection();
        }

        private void DGV_Progress_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //Update_DGV();
        }
        private void DGV_Progress_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //Update_DGV();
        }
        private void DGV_Progress_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion

        #region Chart
        /************************************************
         * FUNCTION:    Chart Measurement
         * DESCRIPTION: this are trigger from Event
         *              "DeviceCom_MeasurementChanged"
         *              to "ChartUpdate(values)"
         ************************************************/
        public ChartManagement ChartMeasurement { get; set; }
        private void Chart_Update(DeviceResponseValues drv)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { Chart_Update(drv); }); return;
            }
            ChartMeasurement.Chart_UpdateFromMeasResults(drv, ItemValues);
        }

        private void Init_Chart()
        {
            ChartMeasurement = new ChartManagement(DeviceCom, Chart_Measurement, ChB_Chart);
            ChartMeasurement.Initialization();
            ChartMeasurement.ChartLabels.Mode.Title = Lbl_ModeTitle;
            ChartMeasurement.ChartLabels.Mode.Value = Lbl_Mode;

            ChartMeasurement.ChartLabels.Set.Title = Lbl_ISetTitle;
            ChartMeasurement.ChartLabels.Set.Value = Lbl_ISet;

            ChartMeasurement.ChartLabels.AVG.Title = Lbl_IavgTitle;
            ChartMeasurement.ChartLabels.AVG.Value = Lbl_Iavg;

            ChartMeasurement.ChartLabels.Diff.Title = Lbl_IerrorTitle;
            ChartMeasurement.ChartLabels.Diff.Value = Lbl_Ierror;

            ChartMeasurement.ChartLabels.StdDev.Title = Lbl_StdTitle;
            ChartMeasurement.ChartLabels.StdDev.Value = Lbl_Std;
            ChartMeasurement.SetVisible(false);
        }

        private void sTDDeviationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartMeasurement.Change_ChartModeSelection(ChartMode.STDdeviation);
        }

        private void refValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartMeasurement.Change_ChartModeSelection(ChartMode.RefValue);
        }

        private void meanVauesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartMeasurement.Change_ChartModeSelection(ChartMode.MeanValue);
        }

        private void errorValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartMeasurement.Change_ChartModeSelection(ChartMode.ErrorValue);
        }

        private void allValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Change_ChartModeSelection(ChartMode.All);
        }

        private void refMeanValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartMeasurement.Change_ChartModeSelection(ChartMode.RefMean);
        }

        private void sTDDeviationErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartMeasurement.Change_ChartModeSelection(ChartMode.STD_Error);
        }

        #endregion Chart
    }
}
