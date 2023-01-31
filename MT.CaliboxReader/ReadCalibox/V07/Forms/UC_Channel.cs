using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using CaliboxLibrary;
using static STDhelper.clSTD;
using static ReadCalibox.clConfig;
using static ReadCalibox.Handler;
using MTcom;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using CaliboxLibrary.StateMachine;
using MT.OneWire;

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
            Channel_No = ucCOM.Ch_No;
            InitLogger(Channel_No);
            Init_Design();
            Init_timStateEngine();
            Init_timProcess();
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

        #region Log
        /**************************************************************************************
        * Log Message:
        ***************************************************************************************/
        private Logger _Logger = new Logger();
        private const string _LogEnding = ".log";
        private void InitLogger(int chNo)
        {
            _Logger.Path = Create_MeasLogPath(chNo.ToString());
            _Logger.LogMeasDB_Active = Config_Initvalues.LogMeas_DB_Active;
            _Logger.LogMeasPath_Active = Config_Initvalues.LogMeas_Path_Active;
        }

        private string LogFile(string chNumber)
        {

            string fileName = "Calibox_CH" + chNumber.PadLeft(2, '0');
            var pathFull = _Logger.CheckFileLenght(LogDirectory, fileName);
            return pathFull;
        }

        private string Create_MeasLogPath(string chNumber)
        {
            LogDirectory = Config_Initvalues.LogMeas_Path;
            return LogFile(chNumber);
        }
        public string LogDirectory { get; set; }
        //public bool LogPathActive { get { return Config_Initvalues.MeasLog_Active; } }
        private void StartLog()
        {
            _Logger.Path = Create_MeasLogPath(Channel_No.ToString());
            _Logger.BeM = ucCOM.BeM;
            _Logger.Odbc = ItemValues.ODBC_EK;
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
        private int _Channel_No;
        public int Channel_No
        {
            get { return _Channel_No; }
            set { Set_ChannelNo(value); }
        }
        public void Set_ChannelNo(int chNo)
        {
            _Lbl_Channel.Text = chNo.ToString().PadLeft(2, '0');
            _Channel_No = chNo;
        }
        #endregion

        #region BoxComunication
        private DeviceCom _DeviceCom = new DeviceCom(isDebug: false);
        public DeviceCom DeviceCom
        {
            get { return _DeviceCom; }
        }
        #endregion

        /// <summary>
        /// Item Informations and Limits
        /// </summary>
        public ChannelValues ItemValues
        {
            get { return DeviceCom.ItemValues; }
            set { DeviceCom.ItemValues = value; }
        }

        /************************************************
         * FUNCTION:    Device SerialPort
         * DESCRIPTION:
         ************************************************/
        private void Init_DeviceCom()
        {
            try
            {
                if (_DeviceCom != null)
                {
                    _DeviceCom.DataReceived -= c_DeviceCom_DataReceived;
                    _DeviceCom.MeasurementChanged -= c_DeviceCom_MeasurementChanged;
                    _DeviceCom.ProgressChanged -= c_DeviceCom_ProgressChanged;
                }
                if (_DeviceCom == null)
                {
                    _DeviceCom = new DeviceCom(port, ChannelName);

                }
                if (_DeviceCom.PortName != port.PortName)
                {
                    _DeviceCom.ChangePort(port);
                }
                _DeviceCom.DataReceived += c_DeviceCom_DataReceived;
                _DeviceCom.MeasurementChanged += c_DeviceCom_MeasurementChanged;
                _DeviceCom.ProgressChanged += c_DeviceCom_ProgressChanged;
                _DeviceCom.ErrorReceived += c_DeviceCom_ErrorReceived;
                _DeviceCom.FinalizedReceived += c_DeviceCom_FinalizedReceived;
            }
            catch (Exception ex)
            {
                _Logger.Save(ex);
            }
        }

        private void c_DeviceCom_FinalizedReceived(object sender, DeviceResponseValues e)
        {
        }

        private void c_DeviceCom_ErrorReceived(object sender, DeviceResponseValues e)
        {
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

        private DateTime _LastReceveidDataTime = DateTime.Now;
        private void c_DeviceCom_DataReceived(object sender, DeviceResponseValues e)
        {
            _LastReceveidDataTime = DateTime.Now;
            DataReceivedMessage(e);
            if (_WaitForInit)
            {
                if (e.BoxMeasValue.Contains("Del"))
                {
                    _WaitForInit = false;
                    if (!_G901Receved)
                    {
                        CMD_Send(OpCode.G901);
                    }
                }
            }
        }
        private void DataReceivedMessage(DeviceResponseValues data)
        {
            _WaitingDetails.DataReceived(data);
            _Logger.Save(data, ItemValues);
        }
        #endregion

        #region CmdSend
        /************************************************
         * FUNCTION:    Command Send
         * DESCRIPTION:
         ************************************************/
        private Task CMD_Send(OpCode opcode, string add, int wait = -1, bool goNextOnAnswer = true)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _WaitingDetails.Reset(opcode);
                    if (wait > 0)
                    {
                        Wait(wait, goNextOnAnswer);
                    }
                    DeviceCom.Send(opcode, add);
                    _Logger.Save(State.ToString(), DeviceCom.CMD_Sended);

                }
                catch (Exception ex)
                {
                    InsertException(ex);
                }
            });
        }

        private void InsertException(Exception ex)
        {
            if (InvokeRequired)
            {
                return;
            }
            Tb_Info.Text += ex.Message + Environment.NewLine;
        }

        private void CMD_Send(OpCode cmd, int wait = -1, bool goNextOnAnswer = true)
        {
            CMD_Send(cmd, null, wait, goNextOnAnswer);
        }
        #endregion

        #region ChannelState
        /************************************************
         * FUNCTION:    Channel States
         * DESCRIPTION:
         ************************************************/
        private bool _StarReady = false;
        public bool StartReady { get { return _StarReady; } set { StartReady_Change(value); } }
        private void StartReady_Change(bool enabled)
        {
            _StarReady = enabled;
            _Btn_Start.Enabled = enabled;
            _Btn_Start.BackColor = StartReady ? MTcolors.MT_rating_InWork_Active : MTcolors.MT_BackGround_White;
            _Btn_Start.ForeColor = StartReady ? MTcolors.MT_BackGround_White : Color.Black;
        }
        private bool _Running;
        public bool Running { get { return _Running; } private set { Channel_Running_Change(value); } }
        public void Channel_Running_Change(bool running)
        {
            _Running = running;
            CH_State wk = CH_State.idle;
            if (running)
            {
                wk = CH_State.inWork;
                _Btn_Start.BackColor = MTcolors.MT_rating_Alert_Active;
                _Btn_Start.ForeColor = Color.Black;
                H_UC_Betrieb.Channel_started(Channel_No);
            }
            else
            {
                if (StartReady)
                { StartReady = false; }
                wk = _CalQuality;
            }
            _Btn_Start.Text = !running ? "Start" : "Cancel";
            if (StartReady && !running)
            {
                StartReady = false;
            }
            RunningState_Change(Channel_No, wk);
            CH_WorkingStatusColors(wk);
        }

        private bool _Active;
        public bool Active { get { return _Active; } private set { Channel_Active_Change(value); } }
        private void Channel_Activity_Changed(object sender, EventArgs e)
        {
            Channel_Active_Change(ucCOM.ConnectionReady);
        }

        private void Channel_Active_Change(bool enabled)
        {
            _Active = enabled;
            StartReady = false;
            CH_State wk = enabled ? CH_State.active : CH_State.notActive;
            RunningState_Change(Channel_No, wk);
            CH_WorkingStatusColors(wk);
        }

        private void CH_WorkingStatusColors(CH_State status)
        {
            Color col = MTcolors.MT_BackGround_Work;
            Color colSep = MTcolors.MT_BackGround_HeaderFooter;
            Color colorInfoBox = MTcolors.MT_BackGround_White;
            if (ItemValues != null)
            {
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
                Init_DeviceCom();

                /* muss ausgeführt werden bevor T&T Gui reseted wird*/
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
                    InternalProcesses = new InternalProcesses(_Logger, DeviceCom);
                    InternalProcesses.Start();
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
        public void Stop()
        {
            if (Running)
            {
                try
                {
                    _WaitingDetails = new WaitDetails() { Answer_Received = false, OpCodeAnswerWaiting = CaliboxLibrary.OpCode.s999 };
                    CMD_Send(OpCode.S999);
                    if (DGV_Progress.Rows.Count > 0)
                    {
                        DGV_Progress.ClearSelection();
                    }

                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    while (!_WaitingDetails.Answer_Received && watch.ElapsedMilliseconds < 5000)
                    {
                        Thread.Sleep(200);
                        Application.DoEvents();
                    }
                    //DeviceCom.SendCMD(Instance, opcode.S999, true);
                    Thread.Sleep(1000);
                    Application.DoEvents();

                    _Logger.Save(OpCode.state, "****** END PROCESS *** END PROCESS *** END PROCESS *** END PROCESS *** END PROCESS ******");
                    port.DiscardOutBuffer();
                    Application.DoEvents();
                    _Logger.ForceLog();
                }
                catch (Exception e)
                {
                    //ErrorHandler("Stop", exception: e);
                    _Logger.Save(e);
                    Gui_Message(message: e.Message);
                }
                Channel_Running_Change(false);
            }
            else
            {
                State = gProcMain.stop_stateEngine;
            }
            _TimProcess.Stop();
            _TimCalibration.Stop();
            ucCOM.Stop();
            H_TT.SetFocus_Input();
        }
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            bool running = _Btn_Start.Text == "Start";
            if (running)
            {
                Start();
            }
            else
            {
                _StateMessageError = "User Cancelation";
                _TimStateEngine.Stop();
                Application.DoEvents();
                if (_TimStateEngine.Enabled)
                {
                    Init_timStateEngine();
                }

                if (State == gProcMain.error)
                {
                    State = gProcMain.idle;
                }
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
            if (ItemValues.tag_nr != 0 || forceReset)
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
                Lbl_ISet.Text = "";
                Lbl_Mode.Text = "";
                Lbl_Std.Text = "";
                Lbl_IavgTitle.Visible = false;
                Lbl_IerrorTitle.Visible = false;
                Lbl_ISetTitle.Visible = false;
                Lbl_ISet.Visible = false;
                Lbl_StdTitle.Visible = false;
                Lbl_ModeTitle.Visible = false;
                Lbl_Ierror.Visible = false;
                Lbl_Iavg.Visible = false;
                Lbl_Iavg.Text = "";
                Lbl_Ierror.Text = "";
                Tb_Info.Text = "";

                ChB_Chart.Checked = false;
                ChB_Chart.Visible = false;
                Gui_Message();
            }
        }
        private void Reset_Processe()
        {
            _WaitForInit = false;
            m_BoxReseted = false;
            m_BewertungExecuted = false;
            _State_ProcessRunning = gProcMain.idle;
            m_TestRunning = false;
            _ProcesseNr = 0;
            m_Calibration_Running = false;
            _CountCalib = 0;
            _CalQuality = CH_State.idle;
            _ProcesseRunning = gProcMain.idle;
            _ProcesseLast = gProcMain.wait;
        }

        private void Reset_onStart()
        {
            _State_Last = gProcMain.idle;
            Reset_Processe();
            m_DBinit = false;
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
                ItemValues.channel_no = Channel_No;
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
                var limits = DeviceCom.ItemValues.DeviceLimits.Get_LimitsMode(drv.BoxMode.Hex);

                string tpTxt = $"Title:\t{limits.Title}\r\n" +
                               $"Current:\t\t{limits.Current:0.00}\r\n" +
                               $"StdDev:\t\t{limits.StdDev:0.00}\r\n" +
                               $"RawValue:\t{limits.RawValue:0.00}\r\n" +
                               $"ErrorActive:\t{limits.ErrorActive}\r\n" +
                                $"CalError:\t\t{limits.CalError:0.00}\r\n" +
                                $"RawError:\t{limits.RawError:0.00}\r\n" +
                               $"WepError:\t{limits.WepError:0.00}\r\n";
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
        private System.Windows.Forms.Timer _TimStateEngine;
        private void Init_timStateEngine()
        {
            if (_TimStateEngine != null)
            {
                _TimStateEngine.Tick -= timStateEngine_Tick;
            }
            _TimStateEngine = new System.Windows.Forms.Timer() { Interval = 500 };
            _TimStateEngine.Tick += timStateEngine_Tick;
        }

        public bool m_DBinit = false;
        public bool m_timeOutActive = false;
        public bool m_Calibration_Running = false;
        public bool m_TestRunning = false;

        public bool m_BewertungExecuted = false;
        public bool m_BoxReseted { get; set; } = false;

        public DateTime m_timeOutDevice;
        //public DateTime m_timeOutTest;
        public int m_processCounter = 0;
        public int m_processCounterTotal = 0;
        //public DateTime m_procStart;
        //public DateTime m_procEnd;
        public int m_stateIntervalSTD = 100;
        private gProcMain _State_Last;
        private gProcMain _State;
        public gProcMain State
        {
            get { return _State; }
            set
            {
                if (!_TimStateEngine.Enabled)
                {
                    _TimStateEngine.Interval = m_stateIntervalSTD;
                    _TimStateEngine.Start();
                }
                _State = value;
            }
        }
        //public gProcMain m_state_AfterWait;
        //public gProcMain m_state_WaitCaller;
        private gProcMain _State_ProcessRunning = gProcMain.idle;
        private bool _WaitForInit = false;

        /* MAIN PROCESSES *********************************************************************
        * FUNCTION:     State Engine
        *               timStateEngine_Tick 
        *               timProcesse_Tick to change process
        '**************************************************************************************/
        //clDeviceCom DeviceCom = new clDeviceCom();
        CH_State _CalQuality = CH_State.idle;

        string _StateMessageError = "";
        string _Attents;

        public void timStateEngine_Tick(object sender, EventArgs e)
        {
            if ((State != _State_Last) || (m_timeOutActive))
            {
                _State_Last = State;
                string message = "";
                switch (State)
                {
                    case gProcMain.getReadyForTest:
                        GetReadyForTest();
                        break;
                    case gProcMain.StartProcess:
                        StartProcess();
                        break;
                    case gProcMain.BoxIdentification:
                        GetBoxId();
                        break;
                    case gProcMain.BoxStatus:
                        BoxStatus();
                        break;
                    case gProcMain.BoxReset:
                        BoxReset();
                        break;
                    case gProcMain.TestFinished:
                        TestFinished();
                        break;
                    case gProcMain.FWcheck:
                        FWcheck();
                        break;
                    case gProcMain.SensorCheck:
                        SensorCheck();
                        break;
                    case gProcMain.SensorPage00Read:
                        SensorPage00Read();
                        break;
                    case gProcMain.SensorPage00Write:
                        SensorPage00Write();
                        break;
                    case gProcMain.SensorPage01Read:
                        SensorPage01Read();
                        break;
                    case gProcMain.SensorPage02Read:
                        SensorPage02Read();
                        break;
                    case gProcMain.Calibration:
                        Calibration();
                        break;
                    case gProcMain.CalibrationWork:
                        break;
                    case gProcMain.wait:
                        if (!_WaitingDetails.IsWaiting)
                        {
                            _WaitingDetails.IsWaiting = true;
                            _TimStateEngine.Interval = 250;
                            m_timeOutDevice = DateTime.Now.AddMilliseconds(_WaitingDetails.Wait_ms);
                            message = $"wait: {_WaitingDetails.Wait_ms / 1000} sec\t{_WaitingDetails.Message}";
                            m_timeOutActive = true;
                            _TimCalibration.Stop();
                            _TimProcess.Stop();
                            _Logger.Save(message);
                        }
                        TimeSpan timeDiffDevice = (m_timeOutDevice - DateTime.Now);
                        Waittime_Show(timeDiffDevice, _WaitingDetails.Message);
                        if (!_WaitingDetails.TimeExpired())
                        {
                            return;
                        }

                        _WaitingDetails.IsWaiting = false;
                        _TimStateEngine.Interval = m_stateIntervalSTD;
                        m_timeOutActive = _WaitingDetails.TimeOutActive;
                        if (_WaitingDetails.ProcNext != gProcMain.error)
                        {
                            if (_WaitingDetails.ProcessRunning) { _TimProcess.Start(); }
                            if (_WaitingDetails.CalibrationRunning) { _TimCalibration.Start(); }
                        }
                        NextProcess(_WaitingDetails.ProcNext);
                        break;
                    case gProcMain.Bewertung:
                        Bewertung();
                        break;
                    case gProcMain.DBinit:
                        DBinit();
                        break;
                    case gProcMain.error:
                        Error(DeviceCom.ResponseLast);
                        //Error(null);
                        break;
                    case gProcMain.idle:
                        m_timeOutActive = false;
                        if (_TimStateEngine.Interval != m_stateIntervalSTD)
                        {
                            _TimStateEngine.Interval = m_stateIntervalSTD;
                        }
                        break;
                    case gProcMain.stop_stateEngine:
                        _TimStateEngine.Stop();
                        break;
                    default:
                        m_timeOutActive = false;
                        if (_TimStateEngine.Interval != m_stateIntervalSTD)
                        {
                            _TimStateEngine.Interval = m_stateIntervalSTD;
                        }
                        break;
                }
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

        /// <summary>
        /// Set <see cref="gProcMain.idle"/> to go to next process,
        /// or set specific process,
        /// <see cref="Processes"/> will change processes
        /// </summary>
        /// <param name="next"></param>
        private void NextProcess(gProcMain next)
        {
            State = next;
        }

        private void GetReadyForTest()
        {
            if (m_timeOutActive) m_timeOutActive = false;
            _Logger.Save(State.ToString(), OpCode.state);
            _TimStateEngine.Interval = m_stateIntervalSTD;
            DeviceCom.Logger = _Logger;
            DeviceCom.ItemValues.sample_FW_Version_value = null;
            NextProcess(gProcMain.StartProcess);
        }
        private void StartProcess()
        {
            if (m_timeOutActive) m_timeOutActive = false;
            _State_ProcessRunning = State;
            _Logger.Save(State.ToString(), OpCode.state);
            Gui_Message(State);

            try
            {
                Set_Progress(ItemValues.CalMode.ToString(), ItemValues.Cal_Desc, value: ItemValues.Cal_Desc, errorcode: "0");
                _TimProcess.Start();
            }
            catch (Exception ex)
            {
                _Logger.Save(ex);
            }
        }

        /// <summary>
        /// Reset ProcessCounter at the function begin 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mode"></param>
        /// <param name="opCode"></param>
        private bool CounterReset(BoxModeCodes code, BoxErrorMode mode, OpCode opCode)
        {
            if (!m_timeOutActive)
            {
                m_timeOutActive = true;
            }
            if (_State_ProcessRunning != State)
            {
                _State_ProcessRunning = State;
                m_processCounter = 0;
                m_processCounterTotal = 3;
                _WaitingDetails.Reset(opCode);
                Set_Progress(code, mode);
            }
            else if (!_WaitingDetails.TimeExpired())
            {
                return false;
            }
            Application.DoEvents();
            m_processCounter++;
            _WaitingDetails.Message = m_processCounter.Attends(m_processCounterTotal);
            Gui_Message(State, attents: _WaitingDetails.Message);
            _Logger.Save(State.ToString(), OpCode.state, _WaitingDetails.Message);
            return true;
        }

        private void GetBoxId()
        {
            CounterReset(BoxModeCodes.BoxIDStatus, BoxErrorMode.NotChecked, OpCode.RDBX);
            bool boxFound = _WaitingDetails.Answer_Received;
            if (DeviceCom.ItemValues.BoxId.CS_ok)
            {
                ShowToolTip();
                Set_Progress(BoxModeCodes.BoxIDStatus, BoxErrorMode.Pass, value: DeviceCom.ItemValues.BoxId.DeviceLimits.FW_Version);
                NextProcess(gProcMain.idle);
                return;
            }
            if (m_processCounter < m_processCounterTotal)
            {
                CMD_Send(OpCode.BoxIdentification, 4000);
            }
            else
            {
                Set_Progress(BoxModeCodes.BoxIDStatus, BoxErrorMode.Error, value: DeviceCom.ItemValues.BoxId.DeviceLimits.FW_Version);
                m_timeOutActive = false;
                NextProcess(gProcMain.error);
                _StateMessageError = "Box Kommunikation oder Status";
                _Logger.Save(State.ToString(), OpCode.state, _StateMessageError);
            }
        }

        private void BoxStatus()
        {
            CounterReset(BoxModeCodes.BoxStatus, BoxErrorMode.NotChecked, OpCode.G100);
            bool BoxFound = _WaitingDetails.Answer_Received;

            if (BoxFound)
            {
                ShowToolTip();
                Set_Progress(BoxModeCodes.BoxStatus, BoxErrorMode.Pass);
                if (DeviceCom.IsFinalized)
                {
                    DeviceCom.ResetError();
                }
                NextProcess(gProcMain.idle);
                return;
            }

            if (m_processCounter <= m_processCounterTotal)
            {
                CMD_Send(OpCode.G100, wait: 2000, goNextOnAnswer: true);
                return;
            }

            Set_Progress(BoxModeCodes.BoxStatus, BoxErrorMode.NoBoxCom);
            m_timeOutActive = false;
            NextProcess(gProcMain.error);
            _StateMessageError = "Keine Box Kommunikation";
            _Logger.Save(State.ToString(), OpCode.state, _StateMessageError);
        }

        private void BoxReset()
        {
            CounterReset(BoxModeCodes.BoxStatus, BoxErrorMode.Error, OpCode.S999);
            m_BoxReseted = _WaitingDetails.Answer_Received;
            if (m_BoxReseted)
            {
                NextProcess(gProcMain.idle);
                ShowToolTip();
                return;
            }
            if (m_processCounter < m_processCounterTotal + 1)
            {
                CMD_Send(OpCode.S999, wait: 3000, goNextOnAnswer: false);
                return;
            }
            _StateMessageError = "Keine BOX Kommunikation";
            _Logger.Save(State.ToString(), OpCode.state, _StateMessageError);
            NextProcess(gProcMain.error);
        }

        private void TestFinished()
        {
            if (m_timeOutActive) m_timeOutActive = false;
            _TimCalibration.Stop();
            _State_ProcessRunning = State;
            _Logger.Save(State.ToString(), OpCode.state);
            Stop();
        }
        private void FWcheck()
        {
            CounterReset(BoxModeCodes.FWversion, BoxErrorMode.NotChecked, OpCode.G015);
            string fwmessage = $"Sensor: {ItemValues.sample_FW_Version_value} DB: {ItemValues.sample_FW_Version} ";
            var error = ItemValues.sample_FW_Version_ok ? BoxErrorMode.Pass : BoxErrorMode.Error;
            if (_WaitingDetails.Answer_Received)
            {
                Set_Progress(BoxModeCodes.FWversion, error, ItemValues.sample_FW_Version_value);
                if (ItemValues.sample_FW_Version_ok)
                {
                    _WaitingDetails.Message = $"{_Attents}\t{fwmessage}";
                    _Logger.Save(_State_ProcessRunning.ToString(), OpCode.state, _WaitingDetails.Message);
                    NextProcess(gProcMain.idle);
                    return;
                }
            }
            _Attents = m_processCounter.Attends(m_processCounterTotal);
            _WaitingDetails.Message = $"{_Attents}\t{fwmessage}";
            Gui_Message(State, _WaitingDetails.Message);
            bool counterProc = m_processCounter < m_processCounterTotal;
            if (m_processCounter < m_processCounterTotal)
            {
                CMD_Send(OpCode.G015, wait: 2000, goNextOnAnswer: false);
            }
            else
            {
                error = ItemValues.sample_FW_Version_ok ? BoxErrorMode.Pass : BoxErrorMode.Error;
                Set_Progress(BoxModeCodes.FWversion, error, ItemValues.sample_FW_Version_value);
                _StateMessageError = $"{fwmessage}";
                ItemValues.ErrorDetected = true;
                NextProcess(gProcMain.error);
            }
        }
        private void SensorCheck()
        {
            //NextProcess(gProcMain.idle);
            NextProcess(gProcMain.SensorPage00Read);
            return;
            //CounterReset(BoxModeCodes.SensorStatus, BoxErrorMode.NotChecked, OpCode.G015);
            //if (!_WaitingDetails.Answer_Received)
            //{
            //    CMD_Send(OpCode.G015);
            //    Wait(2000, false);
            //}
            //else
            //{
            //    var error = DeviceCom.ResponseLast == null ? BoxErrorMode.NoSensCom : BoxErrorMode.Pass;
            //    Set_Progress(BoxModeCodes.SensorStatus, error, ItemValues.sample_FW_Version_value);
            //    Set_Progress(BoxModeCodes.FWversion, ItemValues.sample_FW_Version_ok ? BoxErrorMode.NoError : BoxErrorMode.Error, ItemValues.sample_FW_Version_value);
            //    if (ItemValues.sample_FW_Version_Cal_active && !ItemValues.sample_FW_Version_ok)
            //    {
            //        _Logger.Save(State.ToString(), OpCode.state, _StateMessageError);
            //        NextProcess(gProcMain.error);
            //        return;
            //    }
            //    NextProcess(gProcMain.SensorPage00);
            //    //NextProcess(gProcMain.idle);
            //}
            //if (m_processCounter >= m_processCounterTotal)
            //{
            //    _StateMessageError = "NO SENSOR";
            //    //Set_Progress("SensorStatus", errorcode: "1");
            //    Set_Progress(BoxModeCodes.SensorStatus, BoxErrorMode.NoSensCom);
            //    _Logger.Save(State.ToString(), OpCode.state, _StateMessageError);
            //    NextProcess(gProcMain.error);
            //    return;
            //}
        }

        #region Sensor Pages
        /**********************************************************
        * FUNCTION:     Sensor Pages
        * DESCRIPTION:  
        ***********************************************************/
        private FrmSerialNumber CreateMsgPage0()
        {
            var title = "SerieNumer Eingabe:";
            var mainQuestion = "This will replace Page0 with default values.";
            var question = title;

            var frm = new FrmSerialNumber(title, mainQuestion, question);
            return frm;
        }

        private FrmSerialNumber CreateMsgPage0(TEDSHeaderValues header)
        {
            var title = "SerieNumer Eingabe:";
            var mainQuestion = "This will replace Page0 with default values." + Environment.NewLine + Environment.NewLine
                + $"PartNumber: {header.SensorItem}" + Environment.NewLine
                + $"SerialNumber: {header.SerialNum}" + Environment.NewLine + Environment.NewLine
                + $"Highest SerialNumber: {Config_ChangeParameters.ConverterHighestSN}" + Environment.NewLine;
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
            return frm;
        }

        private bool CheckCounter(BoxErrorMode error)
        {
            if (m_processCounter >= m_processCounterTotal)
            {
                _StateMessageError = error.Desc;
                Set_Progress(BoxModeCodes.SensorStatus, error);
                _Logger.Save(State.ToString(), OpCode.state, _StateMessageError);
                NextProcess(gProcMain.error);
                return false;
            }
            return true;
        }
        private string _SNsended;
        private void SensorPage00Read()
        {
            if (!CounterReset(BoxModeCodes.SensorStatus, BoxErrorMode.NotChecked, OpCode.RDPG))
            {
                return;
            }
            if (!_WaitingDetails.Answer_Received)
            {
                CMD_Send(OpCode.RDPG, "0", wait: 2000, goNextOnAnswer: false);
            }
            else
            {
                SensorPage00Write();
            }
            CheckCounter(BoxErrorMode.NoSensCom);
        }

        private void SensorPage00Write()
        {
            //CounterReset(BoxModeCodes.SensorStatus, BoxErrorMode.NotChecked, OpCode.WRPG);
            //if (!_WaitingDetails.Answer_Received)
            {
                var error = DeviceCom.ResponseLast == null ? BoxErrorMode.NoSensCom : BoxErrorMode.Pass;
                var pageData = ItemValues.TEDSpageDatas[0];
                if (pageData.DataReceived)
                {
                    var header = ItemValues.TEDSpageDatas.HeaderValues;
                    bool isConverter = header.SensorItem == TdlData.PartNumber;
                    if (!isConverter || header.SerialNum != _SNsended)
                    {
                        _TimStateEngine.Stop();
                        var frm = CreateMsgPage0(header);
                        var dialog = frm.ShowDialog();
                        _SNsended = frm.Value;
                        if (!isConverter)
                        {
                            pageData.NewHEX(TdlData.Page00Default);
                            pageData.ReadHEX();
                            pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                        }
                        switch (dialog)
                        {
                            case DialogResult.Abort:
                            case DialogResult.Cancel:
                                NextProcess(gProcMain.error);
                                _TimStateEngine.Start();
                                return;
                            case DialogResult.OK:
                                var serialNumber = frm.Value;

                                var properties = pageData.SensorPageDatas.SensorTemplate.GetPropertiesFromPageNo(0);
                                if (properties == null)
                                {
                                    NextProcess(gProcMain.error);
                                    return;
                                }
                                var prop = properties.FirstOrDefault(x => x.Property.ToLower().Contains("serial"));
                                if (prop == null)
                                {
                                    NextProcess(gProcMain.error);
                                    return;
                                }
                                prop.Value = frm.Value;
                                pageData.Update();
                                pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
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
                                _WaitingDetails.Reset(OpCode.RDPG);
                                CMD_Send(OpCode.WRPG, $"0 {pageData.HEX.RemoveSeparators()}", wait: 2000, goNextOnAnswer: false);
                                break;
                        }

                        _TimStateEngine.Start();
                    }
                    else if (int.TryParse(header.SerialNum, out int sn))
                    {
                        if (sn > 0 && isConverter)
                        {
                            NextProcess(gProcMain.SensorPage01Read);
                            return;
                        }
                    }
                }
                else
                {
                    SensorPage00Read();
                    return;
                }
            }
            CheckCounter(BoxErrorMode.NoSensCom);
        }

        private void SensorPage01Read()
        {
            if (!CounterReset(BoxModeCodes.SensorStatus, BoxErrorMode.NotChecked, OpCode.RDPG))
            {
                return;
            }
            if (!_WaitingDetails.Answer_Received)
            {
                CMD_Send(OpCode.RDPG, "1", wait: 2000, goNextOnAnswer: false);
            }
            else
            {
                SensorPage01Write();
            }

            CheckCounter(BoxErrorMode.NoSensCom);
        }

        private void SensorPage01Write()
        {
            var error = DeviceCom.ResponseLast == null ? BoxErrorMode.NoSensCom : BoxErrorMode.Pass;
            var pageData = ItemValues.TEDSpageDatas[1];
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
                                _WaitingDetails.Reset(OpCode.RDPG);
                                CMD_Send(OpCode.WRPG, $"1 {pageData.HEX.RemoveSeparators()}", wait: 2000, goNextOnAnswer: false);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                {
                    NextProcess(gProcMain.SensorPage02Read);
                    return;
                }
            }
        }

        private void SensorPage02Read()
        {
            if (!CounterReset(BoxModeCodes.SensorStatus, BoxErrorMode.NotChecked, OpCode.RDPG))
            {
                return;
            }
            if (!_WaitingDetails.Answer_Received)
            {
                CMD_Send(OpCode.RDPG, "2", wait: 2000, goNextOnAnswer: false);
            }
            else
            {
                SensorPage02Write();
            }

            CheckCounter(BoxErrorMode.NoSensCom);
        }
        private void SensorPage02Write()
        {
            var error = DeviceCom.ResponseLast == null ? BoxErrorMode.NoSensCom : BoxErrorMode.Pass;
            var pageData = ItemValues.TEDSpageDatas[1];
            if (pageData.DataReceived)
            {
                var hex = pageData.HEX;
                pageData.ReadHEX();
                pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                var header = ItemValues.TEDSpageDatas.HeaderValues;
                if (header.SensorItemDesc != TdlData.DescriptionDefault)
                {
                    _WaitingDetails.Reset(OpCode.RDPG);
                    CMD_Send(OpCode.WRPG, $"2 {TdlData.Page02Default.RemoveSeparators()}", wait: 2000, goNextOnAnswer: false);
                }
                else
                {

                    NextProcess(gProcMain.idle);
                    return;
                }
            }
        }
        #endregion

        private void Calibration()
        {
            if (!m_timeOutActive)
            {
                m_timeOutActive = true;
                m_processCounter = 0;
                _WaitingDetails.Answer_Received = false;
                if (_TimProcess.Enabled)
                { _TimProcess.Stop(); }
            }
            m_processCounter++;
            if (_State_ProcessRunning != State)
            {
                _State_ProcessRunning = State;
                _WaitingDetails.Message = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}";
                CMD_Send(ItemValues.CalMode);
                _WaitingDetails.CalibrationRunning = true;
                m_Calibration_Running = true;
                _TimCalibration.Interval = 1000;
                _TimCalibration.Start();
                Set_Progress(ItemValues.CalMode.ToString(), ItemValues.Cal_Desc, value: ItemValues.Cal_Desc, errorcode: "00");
                //m_state = gProcMain.CalibrationWork;
            }
        }
        private void Bewertung()
        {
            if (m_timeOutActive) m_timeOutActive = false;
            _TimCalibration.Stop();
            _State_ProcessRunning = State;
            m_BewertungExecuted = true;
            m_TestRunning = false;
            try
            {
                string state = ItemValues.test_ok ? "2" : "1";
                Set_Progress(ItemValues.CalMode.ToString(), ItemValues.Cal_Desc, value: ItemValues.Cal_Desc, errorcode: state);
            }
            catch { }
            ItemValues.meas_time_end = DateTime.Now;
            ItemValues.test_ok = _CalQuality == CH_State.QualityGood;
            //if (!Save_Values_To_DB())
            try
            {
                if (!ItemValues.Save_DBmeasValues(DeviceCom.DT_Progress))
                {
                    _StateMessageError = "ERROR: DataBank speichern Bewertung";
                    _Logger.Save(gProcMain.Bewertung.ToString(), OpCode.state, _StateMessageError);
                    NextProcess(gProcMain.error);
                    return;
                }
            }
            catch (Exception ex)
            {
                _Logger.Save(ex);
            }
            string quality = ItemValues.test_ok ? "Gut" : "Schlecht";
            string message = $"Bewertung: {quality}\tduration: {ItemValues.meas_time_duration.ToStringMin()}";
            _Logger.Save(gProcMain.Bewertung.ToString(), OpCode.state, message);
            if (!ItemValues.ErrorDetected) { Gui_Message(message: message); }
            NextProcess(gProcMain.TestFinished);
        }
        private void DBinit()
        {
            if (m_timeOutActive) m_timeOutActive = false;
            _State_ProcessRunning = State;
            m_TestRunning = true;
            string message = $"sensorID: {ItemValues.sensor_id}\tTAG-Nr: {ItemValues.tag_nr}\tPassNo: {ItemValues.pass_no}";
            //if (!Save_Values_To_DB_INIT())
            if (!ItemValues.Save_DBprocInit())
            {
                _StateMessageError = "ERROR: DataBank speichern INIT";
                message += $" {_StateMessageError}";
                NextProcess(gProcMain.error);
            }
            else
            {
                m_DBinit = true;
                NextProcess(gProcMain.idle);
            }
            _Logger.Save(_State_ProcessRunning.ToString(), OpCode.state, message);
        }
        private void Error(DeviceResponseValues drv)
        {
            if (m_timeOutActive) m_timeOutActive = false;
            _TimCalibration.Stop();
            m_timeOutActive = false;
            ItemValues.ErrorDetected = true;
            string message = null;
            if (!string.IsNullOrEmpty(_StateMessageError))
            {
                message = $"ERROR: {_StateMessageError}\t{_State_ProcessRunning}";
                Gui_Message(message);
            }
            if (!string.IsNullOrEmpty(ItemValues.ErrorMessage))
            {
                message = $"ERROR: {ItemValues.ErrorMessage}\t{_State_ProcessRunning}";
                Gui_Message(message);
            }
            if (drv != null)
            {
                if (!string.IsNullOrEmpty(drv.BoxErrorCode_hex))
                {
                    if (drv.BoxErrorCode_hex != "0")
                    {
                        message = $"ERROR: {drv.BoxErrorCode_hex} - {drv.BoxErrorCode_desc}\t{_State_ProcessRunning}";
                        Gui_Message(message);
                    }
                }
            }
            if (message == null)
            {
                message = $"ERROR: ";
                Gui_Message(message);
            }

            _Logger.Save(State.ToString(), OpCode.Error, message);
            if (m_TestRunning)
            {
                NextProcess(gProcMain.Bewertung);
                return;
            }
            DGV_Progress.ClearSelection();
            m_timeOutActive = false;
            NextProcess(gProcMain.idle);
            Stop();
        }
        #endregion StateEngineFunctions

        #region GUI Message
        /***************************************************************************************
        ** GUI Message:  Error
        ****************************************************************************************/

        private void GUI_TXT(string message, bool addMessage = false)
        {
            bool error = false;
            if (string.IsNullOrEmpty(message)) { return; }
            else
            {
                error = message.ToLower().Contains("error:");
                message = message.Replace("\t", Environment.NewLine);
            }
            if (addMessage)
            {
                string show = (message + Environment.NewLine + Tb_Info.Text).Trim();
                int count = show.Split('\r').Length - 1;
                if (count < 20)
                { Tb_Info.Text = show.Trim(); }
                else
                {
                    int length = show.LastIndexOf(Environment.NewLine);
                    message = show.Substring(0, length).Trim();
                }
            }
            else { message = message.Trim(); }
            GUI_Message(message);
            if (error)
            {
                ItemValues.ErrorDetected = true;
                if (!Config_Initvalues.LogError_Active)
                { _Logger.Save(OpCode.Error, message); }
            }
            //Tb_Info.Refresh();
        }
        private void GUI_Message(string message)
        {
            if (InvokeRequired)
            {
                try { this.Invoke((MethodInvoker)delegate { GUI_Message(message); }); }
                catch (Exception ex)
                {
                    _Logger.Save(ex);
                    //ErrorHandler("GUI_Error_TXT", exception: ex); 
                }
                return;
            }
            Tb_Info.Text = message;
            //Tb_Info.Refresh();
        }

        #region Wait
        /*******************************************************************************************************************
        ** FUNCTION:    Wait
        ********************************************************************************************************************/
        public bool calibrationRunning = false;

        private WaitDetails _WaitingDetails = new WaitDetails();

        private Task Wait(int wait, gProcMain procNext, bool goNextOnAnswer = true)
        {
            return Task.Factory.StartNew(() =>
            {
                _WaitingDetails.TimeOutActive = m_timeOutActive;
                _WaitingDetails.ProcessRunning = _TimProcess.Enabled;
                _WaitingDetails.CalibrationRunning = _TimCalibration.Enabled;
                _WaitingDetails.Reset(wait, procNext, goNextOnAnswer);
                NextProcess(gProcMain.wait);
            });
        }

        private void Wait(int wait, bool goNextOnAnswer = true)
        {
            Wait(wait, State, goNextOnAnswer);
        }

        public void Waittime_Show(TimeSpan timeDiff, string message)
        {
            string time = timeDiff.TimeDiff();
            Gui_Message(time, message: message);
        }
        public void Waittime_Show(double timeDiff, gProcMain callerState, string message = null)
        {
            string time = timeDiff.TimeDiff();
            Gui_Message(time, callerState, message);
        }

        public void Waittime_Show_Cal(string message)
        {
            Waittime_Show(ItemValues.meas_time_remain.TotalSeconds, State, message);
        }

        public void Gui_Message(string time, gProcMain callerState = gProcMain.idle, string message = null, string attents = null)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder sb = new StringBuilder();
                if (callerState != gProcMain.idle) { sb.Append(callerState.ToString()); }
                sb.Append(Environment.NewLine);
                if (!string.IsNullOrEmpty(time))
                {
                    sb.Append(time);
                    sb.Append(Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(attents))
                {
                    sb.Append(time);
                    sb.Append(Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(message)) { sb.Append(message.Trim()); }
                GUI_TXT(sb.ToString(), false);
            });
        }
        public void Gui_Message(gProcMain callerState = gProcMain.idle, string message = null, string attents = null)
        {
            Gui_Message(null, callerState, message, attents);
        }
        #endregion

        #endregion GUI Message

        /* CALIBRATION *************************************************************************
        'FUNCTION:    Process Calibration
                      process steps
        ****************************************************************************************/
        #region Calibration
        private System.Windows.Forms.Timer _TimProcess;
        private void Init_timProcess()
        {
            _TimProcess = new System.Windows.Forms.Timer() { Interval = 1000 };
            _TimProcess.Tick += new EventHandler(timProcesse_Tick);
        }

        gProcMain _ProcesseLast = gProcMain.wait;
        gProcMain _ProcesseRunning = gProcMain.idle;
        int _ProcesseNr = 0;
        int _ProcessCount = 0;
        int processeCount
        {
            get
            {
                if (_ProcessCount == 0)
                { _ProcessCount = Processes.ProcOrder.Count; }
                return _ProcessCount;
            }
        }
        bool ProcesseLastArrived { get { return _ProcesseNr == processeCount; } }
        public void timProcesse_Tick(object sender, EventArgs e)
        {
            if (State == gProcMain.error || ItemValues.ErrorDetected)
            {
                _TimCalibration.Stop();
                _TimProcess.Stop();
                return;
            }
            else
            if (_ProcesseRunning != _ProcesseLast && (State == gProcMain.idle || State == gProcMain.StartProcess))
            {
                bool nextProcess = false;
                switch (State)
                {
                    case gProcMain.StartProcess:
                        _ProcesseNr = 0;
                        _ProcesseRunning = Processes.ProcOrder[0];
                        nextProcess = true;
                        break;
                    default:
                        nextProcess = true;
                        break;
                }
                if (nextProcess && !ItemValues.ErrorDetected)
                {
                    if (!ProcesseLastArrived) { _ProcesseNr++; }
                    _ProcesseLast = _ProcesseRunning;
                    _ProcesseRunning = Processes.ProcOrder[_ProcesseNr];
                    State = _ProcesseRunning;
                }
                if (ProcesseLastArrived)
                {
                    _TimCalibration.Stop();
                    _TimProcess.Stop();
                }
            }
        }
        #endregion Calibration

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
        string _NoAnswer = "";
        bool _G901Receved = false;
        string _CalibLastMode = "";
        public void TimCalibration_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Answer())
                {
                    _CalibNoAnswerCounter = 0;
                    _WaitingDetails.OpCodeAnswerReceive.Contains(OpCode.state);
                }
                else
                {
                    _CalibNoAnswerCounter++;
                }
            }
            catch { }
            try
            {
                CheckMeasurement(DeviceCom.ResponseLast);
            }
            catch { }
            bool NoError = false;
            try { NoError = CheckFinalize(DeviceCom.ResponseLast); }
            catch { }
        }
        private void CheckMeasurement(DeviceResponseValues drv)
        {
            if (State == gProcMain.Calibration)
            {
                if (_CountCalib == 0)
                {
                    _TimCalibration.Interval = 1000;
                    _CalibNoAnswerCounter = 0;
                    calibrationRunning = true;
                    _G901Receved = false;
                    CMD_Send(OpCode.G901);
                    _CountCalib++;
                    return;
                }
                _CountCalib++;
                if (!_G901Receved)
                {
                    if (_CalibNoAnswerCounter > 30)
                    {
                        CMD_Send(OpCode.G901);
                        _CalibNoAnswerCounter = 0;
                    }
                }
                if (drv.BoxMode_desc != _CalibLastMode)
                {
                    ItemValues.Cal_BoxMode_desc = drv.BoxMode_desc;
                    ItemValues.Cal_Stopwatch = new System.Diagnostics.Stopwatch();
                    ItemValues.Cal_Stopwatch.Start();
                    _CalibLastMode = drv.BoxMode_desc;
                    _CountCalib = 1;
                }
                Set_ChartVisible();
            }
        }
        private bool CheckFinalize(DeviceResponseValues drv)
        {
            if (DeviceCom.IsFinalized || DeviceCom.IsError || drv.OpCode == OpCode.s999 || _CalibNoAnswerCounter > 200)
            {
                _TimCalibration.Stop();
                ChB_Chart.Checked = false;
                m_Calibration_Running = false;
                if (drv.OpCode == OpCode.s999)
                {
                    _StateMessageError = "Sensor Kommunikation abgebrochen";
                    ItemValues.ErrorDetected = true;
                    ItemValues.ErrorMessage = _StateMessageError;
                    drv.BoxErrorMode = BoxErrorMode.NoSensCom;
                    DeviceCom.OnErrorReceived(drv);
                }
                if (!DeviceCom.IsError)
                {
                    ItemValues.test_ok = true;
                    _CalQuality = CH_State.QualityGood;
                    _Logger.Save(State.ToString(), OpCode.state, $"SUBPROCESS QUALITY GOOD {_StateMessageError}");
                    NextProcess(gProcMain.idle);
                }
                else
                {
                    ItemValues.ErrorDetected = true;
                    ItemValues.ErrorMessage = _StateMessageError;
                    _CalQuality = CH_State.QualityBad;
                    ItemValues.test_ok = false;
                    _Logger.Save(State.ToString(), OpCode.state, $"SUBPROCESS ERROR {_StateMessageError}");
                    CMD_Send(OpCode.G200);
                    Wait(4000, gProcMain.error);
                }
                if (!_TimProcess.Enabled)
                { _TimProcess.Start(); }
                return false;
            }
            else
            {
                if (_CalibNoAnswerCounter > 2)
                {
                    _NoAnswer = AnswerReplaces(drv.BoxMode_desc, _CountCalib, _CalibNoAnswerCounter);
                }
                else
                {
                    _NoAnswer = null;
                }
                Waittime_Show_Cal(_NoAnswer);
            }
            return true;
        }
        private string AnswerReplaces(string txt, int count, int countNoAnswer)
        {
            StringBuilder sb = new StringBuilder(txt)
                .Replace("_", " ").Replace("Mode", "").Replace("Box", "")
                .Append($"\tCount: {count}");
            if (_CalibNoAnswerCounter > 2)
            {
                sb.Append($"; NoAnswer: {_CalibNoAnswerCounter}");
            }
            return sb.ToString();
        }
        private bool Answer()
        {
            if (Answer(OpCode.g901))
            {
                _G901Receved = true;
                return true;
            }
            if (Answer(OpCode.g100))
            {
                return true;
            }
            if (Answer(OpCode.s999))
            {
                return true;
            }
            return false;
        }
        private bool Answer(OpCode cmd)
        {
            if (_WaitingDetails.OpCodeAnswerReceive.Contains(cmd))
            {
                return true;
            }
            return false;
        }

        #region ProgressUI
        /************************************************
         * FUNCTION:    Progress
         * DESCRIPTION: Progress Show and set
         *              Show values to DataGridView
         ************************************************/
        private void Set_Progress(BoxModeCodes code, BoxErrorMode error, string value = null)
        {
            DeviceCom.SetProgress(code, error, value);
        }
        private void Set_Progress(string boxmode_hex, string boxmode_desc = null, string value = null, string errorcode = null)
        {
            if (string.IsNullOrEmpty(boxmode_desc)) { boxmode_desc = boxmode_hex; }
            DeviceCom.SetProgress(boxmode_hex, boxmode_desc, value, errorcode);
        }

        void Init_DGV()
        {
            //if (SampleResponse == null) { SampleResponse = new clDeviceResponse(this); }
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
                }
            }
            catch { }
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
        private void Chart_Update(DeviceResponseValues drv)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { Chart_Update(drv); }); return;
            }
            Chart_UpdateFromMeasResults(drv);
        }

        private void Set_ChartVisible()
        {
            if (!ChB_Chart.Visible)
            {
                if (DeviceCom.DT_Measurements.Rows.Count > 0)
                {
                    ChB_Chart.Visible = true;
                    ChB_Chart.Checked = true;
                    Lbl_IavgTitle.Visible = true;
                    Lbl_IerrorTitle.Visible = true;
                    Lbl_ISetTitle.Visible = true;
                    Lbl_Ierror.Visible = true;
                    Lbl_Iavg.Visible = true;
                    Lbl_ISet.Visible = true;
                    Lbl_ModeTitle.Visible = true;
                    Lbl_StdTitle.Visible = true;
                }
            }
        }
        private void Chart_UpdateFromMeasResults(DeviceResponseValues drv)
        {
            if (ItemValues.GetResults(drv.BoxMode.Hex, out List<DeviceLimitsResults> results))
            {
                var limitsLast = Chart_Values(results);
                Chart_Colors(limitsLast);
            }
        }
        private DeviceLimitsResults Chart_Values(List<DeviceLimitsResults> results)
        {
            DeviceLimitsResults limitsLast = null;
            try
            {
                Chart_Measurement.Series[MN_I_StdDev].Points.Clear();
                Chart_Measurement.Series[MN_I_Error].Points.Clear();
                Chart_Measurement.Series[MN_I_AVG].Points.Clear();
                Chart_Measurement.Series[MN_I_set].Points.Clear();
                var start = results.Count - _Chart_ShowVaulesQuantiy + 1;
                if (start < 0) { start = 0; }
                for (int i = start; i < results.Count; i++)
                {
                    limitsLast = results[i];
                    Chart_Measurement.Series[MN_I_StdDev].Points.AddXY(limitsLast.Count, limitsLast.StdDev);
                    Chart_Measurement.Series[MN_I_Error].Points.AddXY(limitsLast.Count, limitsLast.ErrorABS);
                    Chart_Measurement.Series[MN_I_AVG].Points.AddXY(limitsLast.Count, limitsLast.ValueAVG);
                    Chart_Measurement.Series[MN_I_set].Points.AddXY(limitsLast.Count, limitsLast.ValueSet);
                }
            }
            catch (Exception ex)
            {
                _Logger.Save(ex);
            }
            return limitsLast;
        }
        private void Chart_LabelColors(DeviceLimitsResults results)
        {
            try
            {
                Lbl_Mode.Text = results.BoxModeDesc;

                Lbl_Iavg.Text = results.ValueAVG.ToString("0.00");
                Lbl_Ierror.Text = results.ErrorABS.ToString("0.00");

                Lbl_Ierror.ForeColor = results.ErrorActive ? GetLabelColor(results.ErrorABS_ok, results.ErrorSet, 1.1, results.ErrorABS) : Color.Black;
                Lbl_ISet.Text = results.ValueSet.ToString("0.00");

                Lbl_Std.Text = results.StdDev.ToString("0.000");
                Lbl_Std.ForeColor = GetLabelColor(results.StdDev_ok, results.StdDevSet, 1.5, results.StdDev);
            }
            catch { }
        }

        private void Chart_AxisColors(DeviceLimitsResults results)
        {
            switch (ChartMode_Selection)
            {
                case ChartMode.STDdeviation:
                case ChartMode.STD_Error:
                    if (results.StdDev < results.StdDevSet)
                    {
                        Chart_ChangeColorsStdDev(MTcolors.MT_rating_God_Active);
                    }
                    else if (results.StdDev < results.StdDevSet * 1.5)
                    {
                        Chart_ChangeColorsStdDev(MTcolors.MT_rating_Alert_Active);
                    }
                    else
                    {
                        Chart_ChangeColorsStdDev(MTcolors.MT_rating_Bad_Active_No);
                    }

                    break;
                default:
                    break;
            }
        }
        private void Chart_Colors(DeviceLimitsResults results)
        {
            if (results == null) { return; }
            Chart_LabelColors(results);
            Chart_AxisColors(results);
        }

        /****************************************************************************************************
         * Chart:       Measurement
         ***************************************************************************************************/
        public enum ChartMode { STDdeviation, MeanValue, ErrorValue, RefMean, RefValue, STD_Error }
        public ChartMode ChartMode_Selection = ChartMode.STD_Error;
        private int _Chart_ShowVaulesQuantiy = 20;

        private void Init_Chart()
        {
            string XvalueMember = "Count";
            Chart_Measurement.Visible = false;
            Chart_Measurement.Series.Clear();
            Chart_Measurement.Series.Add(New_ChartSerie(MN_I_set, XvalueMember, System.Drawing.Color.Gray));
            Chart_Measurement.Series.Add(New_ChartSerie(MN_I_AVG, XvalueMember, System.Drawing.Color.Orange));
            Chart_Measurement.Series.Add(New_ChartSerie(MN_I_StdDev, XvalueMember, System.Drawing.Color.White));
            Chart_Measurement.Series.Add(New_ChartSerie(MN_I_Error, XvalueMember, System.Drawing.Color.Yellow));
            Chart_Measurement.ChartAreas[0].AxisY.IsStartedFromZero = false;
            Chart_Measurement.ChartAreas[0].AxisY2.IsStartedFromZero = false;
            Chart_Measurement.ChartAreas[0].AxisX.Interval = 1;
            Chart_Measurement.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart_Measurement.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart_Measurement.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart_Measurement.ChartAreas[0].AxisX.IsMarginVisible = false;
            Chart_Measurement.ChartAreas[0].AxisY.IsMarginVisible = false;
            Chart_Measurement.ChartAreas[0].AxisY2.IsMarginVisible = false;
            Chart_Measurement.ChartAreas[0].AxisY.LabelStyle.Format = "0.##";
            Chart_Measurement.ChartAreas[0].AxisY2.LabelStyle.Format = "0.##";
            Chart_Measurement.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            Chart_Measurement.ChartAreas[0].AxisY2.MinorGrid.Enabled = false;
            ChB_Chart.Checked = false;
        }
        private Series New_ChartSerie(string name, string xvaluemember, Color color)
        {
            return new Series
            {
                Name = name,
                Color = color,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = false,
                ChartType = SeriesChartType.Line,
                BorderWidth = 4,
                XValueMember = xvaluemember,
                YValueMembers = name
            };
        }
        private const string _ChartAreaName = "Default";
        private void Change_ChartModeSelection(ChartMode selection)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { Change_ChartModeSelection(selection); }); return;
            }
            bool refValues = false;
            bool meanValues = false;
            bool stdValues = false;
            bool errorValues = false;
            int actives = 0;
            switch (selection)
            {
                case ChartMode.STDdeviation:
                    actives = 1;
                    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    _Chart_ShowVaulesQuantiy = 20;
                    stdValues = true;
                    break;
                case ChartMode.MeanValue:
                    actives = 1;
                    _Chart_ShowVaulesQuantiy = 20;
                    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    meanValues = true;
                    break;
                case ChartMode.ErrorValue:
                    actives = 1;
                    _Chart_ShowVaulesQuantiy = 20;
                    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    errorValues = true;
                    break;
                case ChartMode.RefMean:
                    actives = 2;
                    _Chart_ShowVaulesQuantiy = 20;
                    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    refValues = true;
                    meanValues = true;
                    break;
                //case ChartMode.All:
                //    actives = 4;
                //    Chart_ShowVaulesQuantiy = 15;
                //    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                //    refValues = true;
                //    meanValues = true;
                //    stdValues = true;
                //    errorValues = true;
                //    break;
                case ChartMode.RefValue:
                    actives = 1;
                    _Chart_ShowVaulesQuantiy = 20;
                    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    refValues = true;
                    break;
                case ChartMode.STD_Error:
                    actives = 2;
                    _Chart_ShowVaulesQuantiy = 20;
                    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    errorValues = true;
                    stdValues = true;
                    break;
                default:
                    _Chart_ShowVaulesQuantiy = 20;
                    break;
            }
            Chart_ChangeColorsPrimaryAxis(MN_I_set, refValues, Color.White);
            Chart_ChangeColorsPrimaryAxis(MN_I_AVG, meanValues, Color.White);
            Chart_ChangeColorsPrimaryAxis(MN_I_StdDev, stdValues, Color.White);
            switch (actives)
            {
                case int n when n < 2:
                    Chart_ChangeColorsPrimaryAxis(MN_I_Error, errorValues, Color.White);
                    break;
                default:
                    Chart_ChangeColorsSecondaryAxis(MN_I_Error, errorValues);
                    break;
            }
            ChartMode_Selection = selection;
        }

        private Color GetLabelColor(bool rating, double set, double factor, double? value)
        {
            if (rating) { return MTcolors.MT_rating_God_Active; }
            //var color = value > set * factor ? MTcolors.MT_rating_Bad_Active: MTcolors.MT_rating_Alert_Active;
            var color = value > set * factor ? MTcolors.MT_rating_Bad_Active_No : MTcolors.MT_rating_Alert_Active;
            return color;
        }
        private void Chart_ChangeColorsStdDev(Color color)
        {
            Chart_ChangeColorsPrimaryAxis(MN_I_StdDev, true, color);
        }
        private void Chart_ChangeColorsPrimaryAxis(string title, bool active, Color color)
        {
            Chart_Measurement.Series[title].Enabled = active;
            if (!active) { return; }
            Chart_Measurement.Series[title].Color = color;
            Chart_Measurement.ChartAreas[0].AxisY.LabelStyle.ForeColor = Chart_Measurement.Series[title].Color;
            Chart_Measurement.Series[title].YAxisType = AxisType.Primary;
            Chart_Measurement.ChartAreas[0].AxisY.Title = title;
            Chart_Measurement.ChartAreas[0].AxisY.TitleForeColor = Chart_Measurement.Series[title].Color;
        }
        private void Chart_ChangeColorsSecondaryAxis(string title, bool active)
        {
            Chart_Measurement.Series[title].Enabled = active;
            if (!active) { return; }
            Chart_Measurement.Series[title].Color = Color.Yellow;
            Chart_Measurement.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Chart_Measurement.Series[title].Color;
            Chart_Measurement.Series[title].YAxisType = AxisType.Secondary;
            Chart_Measurement.ChartAreas[0].AxisY2.Title = title;
            Chart_Measurement.ChartAreas[0].AxisY2.TitleForeColor = Chart_Measurement.Series[title].Color;
        }
        private void Chart_Visible(bool visible)
        {
            Chart_Measurement.Visible = visible;
            if (visible)
            {
                Change_ChartModeSelection(ChartMode_Selection);
                //if (calibrationRunning)
                //{ timMeasurement.Start(); }
                Chart_Measurement.BringToFront();
            }
            //else
            //{
            //    timMeasurement.Stop();
            //}
        }
        private void ChB_Chart_CheckedChanged(object sender, EventArgs e)
        {
            Chart_Visible(ChB_Chart.Checked);
        }

        /****************************************************************************************************
        ** Chart:       Measurement Menu
        ****************************************************************************************************/

        private void sTDDeviationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.STDdeviation);
        }

        private void refValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.RefValue);
        }

        private void meanVauesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.MeanValue);
        }

        private void errorValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.ErrorValue);
        }

        private void allValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Change_ChartModeSelection(ChartMode.All);
        }

        private void refMeanValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.RefMean);
        }

        private void sTDDeviationErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.STD_Error);
        }

        #endregion Chart
    }
}
