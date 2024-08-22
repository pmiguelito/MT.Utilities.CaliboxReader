using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_Item_Infos;
using System.IO.Ports;
using System.Threading;
using System.IO;
using STDhelper;
using static STDhelper.clBGWorker;
using static STDhelper.clMTcolors;
using static STDhelper.clLogging;
using static ReadCalibox.clConfig;
using static ReadCalibox.clDeviceCom;
using static ReadCalibox.clHandler;
using ODBC_TT;

namespace ReadCalibox
{
    public partial class UC_Channel : UserControl
    {
        /****************************************************************************************************
         * Constructor: Can be used for Panel Controls Load
         ***************************************************************************************************/
        #region UC Instance
        private UC_Channel _instance;
        public UC_Channel Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new UC_Channel(ucCOM); }
                return _instance;
            }
        }
        #endregion UC Instance

        //public UC_Channel ucCH { get { return this; } }
        private clLog log = new clLog();
        public UC_COM ucCOM;
        public UC_Betrieb ucBetrieb = Form_Main.UC_betrieb;
        private SerialPort port { get { return ucCOM.Serialport; } }

        public string ODBC_EK { get; set; }
        public clItemLimits Limits { get; set; }
        //private static DBInfos ItemInfos { get; set; }
        public string Channel { get { return _Label_CH.Text; } set { _Label_CH.Text = value; } }
        public string TAGno { get { return _Lbl_TAGno.Text; } set { _Lbl_TAGno.Text = value; } }
        public string Item { get { return _Lbl_Item.Text; } set { _Lbl_Item.Text = value; } }
        public string Pdno { get { return _Lbl_Pdno.Text; } set { _Lbl_Pdno.Text = value; } }
        public string UserName { get { return _Lbl_User.Text; } set { _Lbl_User.Text = value; } }
        public string ProdType { get { return _Lbl_ProdType.Text; } set { _Lbl_ProdType.Text = value; } }
        public string TimeStart { get { return _Lbl_TimeStart.Text; } set { _Lbl_TimeStart.Text = value; } }
        public string TimeEnd { get { return _Lbl_TimeEnd.Text; } set { _Lbl_TimeEnd.Text = value; } }

        public string PassNo { get { return _Lbl_PassNo.Text; } set { _Lbl_PassNo.Text = value; } }
        public string CalModeDesc { get { return _Lbl_CalMode.Text; } set { _Lbl_CalMode.Text = value; } }

        private bool _StarReady = false;
        public bool StartReady
        {
            get { return _StarReady; }
            set
            {
                _StarReady = value;
                StartReady_Change(value);
            }
        }
        private bool _Running;
        public bool Running
        {
            get { return _Running; }
            set
            {
                if (_Running != value)
                {
                    _Running = value;
                    Channel_Running_Change(value);
                }
                else
                { _Running = value; }
            }
        }
        private bool _Active;
        public bool Active { get { return _Active; } set { _Active = value; Channel_Active_Change(value); } }

        
        /****************************************************************************************************
         * Constructor: Main
         ***************************************************************************************************/
        public UC_Channel(UC_COM uc_COM)
        {
            InitializeComponent();
            uc_COM.UCChannel = this;
            this.ucCOM = uc_COM;
            StartReady = uc_COM.MeasReady;
        }
        private void UC_Channel_Load(object sender, EventArgs e)
        {
            DGV_Calibration.Visible = false;
            Init_Design();
            Init();
            StartReady = false;
            Init_timStateEngine();
            Init_timProcess();
            Init_timCalibration();
            Init_Chart();
            _instance = this;
        }
        void Init()
        {
            GUI_Infos_Reset();
            Channel = ucCOM.Ch_name.Remove(0, 2);
            Active = ucCOM.SerialPort_Found;
            if (!Active) { Channel_Active_Change(false); }
            Running = false;
            Channel_Running_Change(false);
            ucCOM.SerialPort_Found_Changed += Channel_Activity_Changed;
            Init_DGV();
        }

        void Init_Design()
        {
            Init_Colors();
            Init_Design_Dimensions();
        }

        void Init_Colors()
        {
            Panel_Sep1.BackColor = MT_BackGround_HeaderFooter;
            Panel_Sep2.BackColor = MT_BackGround_HeaderFooter;
            Panel_Sep3.BackColor = MT_BackGround_HeaderFooter;
            Panel_Sep4.BackColor = MT_BackGround_HeaderFooter;

            Panel_Header.BackColor = MT_BackGround_Work;
            Panel_Button.BackColor = MT_BackGround_Work;
            Panel_ItemInfos.BackColor = MT_BackGround_Work;
            Panel_Info.BackColor = MT_BackGround_Work;
            Panel_Time.BackColor = MT_BackGround_Work;
        }

        void Init_Design_Dimensions()
        {
            Form_Main.UC_TT.AllocFont(_Label_CH, 18, FontStyle.Bold);
            Form_Main.UC_TT.AllocFont(_Lbl_TAGno, 12, FontStyle.Bold);
            Form_Main.UC_TT.AllocFont(_Lbl_TAGno_TXT, 12, FontStyle.Bold);
            Form_Main.UC_TT.AllocFont(_Btn_Start, 10, FontStyle.Bold);
        }

        void Init_DGV()
        {
            DGV_Calibration.BackgroundColor = MT_BackGround_Work;
            DGV_Calibration.DataSource = DT_Calibration;
            DGV_Calibration.Columns[0].Visible = false;
            Font n = new Font("Tahoma", 6, FontStyle.Regular);
            DGV_Calibration.ColumnHeadersDefaultCellStyle.Font = n;
            n = new Font("Tahoma", 7, FontStyle.Regular);
            DGV_Calibration.RowsDefaultCellStyle.Font = n;
            DGV_Calibration.MultiSelect = false;
            DGV_Calibration.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Calibration.Columns[1].MinimumWidth = 140;
            DGV_Calibration.ClearSelection();
        }

        #region TEST
        /****************************************************************************************************
         * Debug:   Test
         *          send message to SerialPort
         ***************************************************************************************************/
        System.Windows.Forms.Timer TESTtimer;
        public void Init_TESTtimer()
        {
            TESTtimer = new System.Windows.Forms.Timer() { Interval = 1000 };
            TESTtimer.Tick += TESTtimer_Tick;
            Init_Test_Echo();
        }
        SerialPort Sp_Echo;
        private void Init_Test_Echo()
        {
            Sp_Echo = new SerialPort();
            int no = Convert.ToInt32(ucCOM.Serialport.PortName.Substring(3));
            Sp_Echo.PortName = $"COM{no + 1}";
            Sp_Echo.Parity = ucCOM.Serialport.Parity;
            Sp_Echo.BaudRate = ucCOM.Serialport.BaudRate;
            Sp_Echo.Handshake = ucCOM.Serialport.Handshake;
            Sp_Echo.DataBits = ucCOM.Serialport.DataBits;
            Sp_Echo.StopBits = ucCOM.Serialport.StopBits;
        }

        int testNr = 0;
        private void TESTtimer_Tick(object sender, EventArgs e)
        {
            if (ucCOM.Serialport.IsOpen)
            {
                //BoardCheck.clDeviceCom.getDevice_AllInfos(_UC_COM.Serialport, this);
                if (!Sp_Echo.IsOpen) { Sp_Echo.Open(); }
                string message = $"TEST_{testNr}: {DateTime.Now}.{DateTime.Now.Millisecond}{Environment.NewLine}";
                Sp_Echo.Write(message);
                //ErrorMessageChannel = message;
                //byte[] data = Encoding.ASCII.GetBytes("R00");
                //_UC_COM.Serialport.Write(message);
                testNr++;
                //_UC_COM.Serialport.Close();
                //TESTtimer.Stop();
            }
            else
            { TESTtimer.Stop(); }
        }

        public void Start_TESTtimer(int interval = 1000)
        {
            testNr = 0;
            if (TESTtimer == null)
            { Init_TESTtimer(); }
            TESTtimer.Interval = interval;
            TESTtimer.Start();
        }
        #endregion TEST

        /****************************************************************************************************
         * Running
         ***************************************************************************************************/
        private void Channel_Running_Change(bool running)
        {
            CH_State wk = CH_State.nothing;
            if (running)
            {
                wk = CH_State.inWork;
                _Btn_Start.BackColor = MT_rating_Alert_Active;
                _Btn_Start.ForeColor = Color.Black;
            }
            else
            {
                wk = calQuality;
            }
            _Btn_Start.Text = !running ? "Start" : "Cancel";
            CH_WorkingStatusColors(wk);
        }

        private enum CH_State { nothing, inWork, QualityGod, QualityBad, active, notActive, error }
        private void CH_WorkingStatusColors(CH_State status)
        {
            Color col = MT_BackGround_Work;
            Color colSep = MT_BackGround_HeaderFooter;
            Color colorInfoBox = Error_Detected ? MT_rating_Bad_Active : MT_BackGround_White;
            bool onlyCH = false;
            switch (status)
            {
                case CH_State.nothing:
                    break;
                case CH_State.inWork:
                    colorInfoBox = MT_BackGround_White;
                    col = MT_rating_InWork_Active;
                    colSep = MT_BackGround_Work;
                    break;
                case CH_State.QualityBad:
                    col = MT_rating_Bad_Active;
                    //colorInfoBox = Error_Detected ? MT_rating_Bad_Active : MT_BackGround_White;
                    break;
                case CH_State.QualityGod:
                    col = MT_rating_God_Active;
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
                    col = MT_rating_Alert_Active;
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

        private void StartReady_Change(bool enabled)
        {
            _Btn_Start.Enabled = enabled;
            _Btn_Start.BackColor = StartReady ? MT_rating_InWork_Active : MT_BackGround_White;
            _Btn_Start.ForeColor = StartReady ? MT_BackGround_White : Color.Black;
        }

        private void Channel_Active_Change(bool enabled)
        {
            CH_State wk = enabled ? CH_State.active : CH_State.notActive;
            CH_WorkingStatusColors(wk);
        }

        private void Channel_Activity_Changed(object sender, EventArgs e)
        {
            Active = ucCOM.SerialPort_Found;
        }

        /****************************************************************************************************
         * GUI:     Reset Informations
         ***************************************************************************************************/
        public void Reset()
        {
            GUI_Infos_Reset();
            Reset_Processe();
            Reset_DT_Calibration();
        }
        private void GUI_Infos_Reset()
        {
            if (TAGno != "")
            {
                Error_Detected = false;
                ODBC_EK = "";
                TAGno = "";
                Item = "";
                Pdno = "";
                UserName = "";
                ProdType = "";
                TimeStart = "";
                TimeEnd = "";
                PassNo = "";
                CalModeDesc = "";
                Tb_Info.Text = "";
                CH_WorkingStatusColors(CH_State.nothing);
                Limits = null;
                SampleResponse = new DeviceResponse(null);
                ChB_Chart.Checked = false;
                ChB_Chart.Visible = false;
                MessageChannel = null;
                //GUI_Message = new List<string>();
            }
        }
        private void Reset_Processe()
        {
            BoxMode_Last = "";
            lLast = "";
            m_BoxReseted = false;
            m_state_ProcessRunning = gProcMain.idle;
            m_TestRunning = false;
            processeNr = 0;
            m_Calibration_Running = false;
            CountCalib = 0;
            calQuality = CH_State.nothing;
            ProcesseRunning = gProcMain.idle;
            ProcesseLast = gProcMain.wait;
            BoxMode_Last = "";
        }
        /****************************************************************************************************
         * GUI:     Update Informations
         ***************************************************************************************************/
        /// <summary>
        /// Informations transfer from main to channel
        /// </summary>
        /// <param name="reset"></param>
        public void GUI_Infos(bool reset = false)
        {
            Reset();
            if (!reset)
            {
                Limits = Form_Main.UC_betrieb.Limits;
                ODBC_EK = Form_Main.UC_TT.ODBCEK_Selected;
                Choise_CalMode();
                PassNo = Limits.pass_no.ToString();
                UserName = Limits.UserName;
                ProdType = Limits.ProductionType_Desc;
                TAGno = Limits.tag_nr;
                Item = Limits.item;
                Pdno = Limits.pdno;
                DGV_Calibration.Visible = true;
                ChB_Chart.Checked = false;
            }
            //else
            //{
            //    Reset();
            //}
        }

        #region NotActive
        /***************************************************************************************
        * Executions:    Start & Stop SerialPort Read
        ****************************************************************************************/
        SerialReaderThread ThreadDR;
        clRTSerialCom.SerialClient serial1;
        void SerialClient_Start()
        {
            serial1 = new clRTSerialCom.SerialClient(ucCOM.Serialport, this);
            //serial1.OnReceiving += new EventHandler<clRTSerialCom.DataStreamEventArgs>(receiveHandler);
        }

        void SerialClient_Stop()
        {
            serial1.CloseConn();
            //serial1.OnReceiving -= new EventHandler<clRTSerialCom.DataStreamEventArgs>(receiveHandler);
            serial1.Dispose();
        }

        /***************************************************************************************
        PortReader: 
        ****************************************************************************************/
        void ThreadDataReceived(object s, EventArgs e)
        {
            var a = (DataEventArgs)e;
            string portName = a.SerialPort.PortName;
            string data = a.Data;
            log.Save(Instance, data, opcode.state);
            //BGW_MessageWriter = BGW_MessageWriter.Initilise(BGW_MessageWriter_DoWork, true, new string[] { portName, data });
        }
        void ThreadDataReceivedSync(object s, EventArgs e)
        {
            //_Tb_Info.Text += e.Data + "\n";
        }
        int storedResponses;
        int maxLen;
        int minLen;
        int currentLen;
        private void receiveHandler(object sender, clRTSerialCom.DataStreamEventArgs e)
        {
            //storedResponses++;
            //minLen = minLen > e.Response.Length ? e.Response.Length : minLen;
            //maxLen = maxLen < e.Response.Length ? e.Response.Length : maxLen;
            //currentLen = e.Response.Length;
            string data = System.Text.Encoding.ASCII.GetString(e.Response);
            log.Save(Instance, data, opcode.state);
            //BGW_MessageWriter = BGW_MessageWriter.Initilise(BGW_MessageWriter_DoWork, true, new string[] { ucCOM.Serialport.PortName, data });
        }
        public void WriteMessage(SerialPort port, byte[] data)
        {
            string dataTXT = System.Text.Encoding.ASCII.GetString(data);
            log.Save(Instance, dataTXT, opcode.state);
            //BGW_MessageWriter = BGW_MessageWriter.Initilise(BGW_MessageWriter_DoWork, true, new string[] { port.PortName, dataTXT });
        }
        #endregion NotActive

        /***************************************************************************************
        * Executions:    Start & Stop
        ****************************************************************************************/
        public string BeM_Selected;
        public int ReadDelay_Selected;

        public bool Start()
        {
            CH_WorkingStatusColors(CH_State.inWork);
            if (!Running)
            {
                if (ucCOM.Start(false))
                {
                    UC_Betrieb.Running_Count++;
                    try { GUI_Infos(); } catch { } /* muss ausgeführt werden bevor T&T Gui reset wird*/
                    m_DBinit = false;
                    LogPathMeas = Create_TMP_MeasPath(ucCOM.PortName);
                    BeM_Selected = ucCOM.BeM;
                    ReadDelay_Selected = Convert.ToInt32(ucCOM.ReadDelay);
                    Running = true;
                    ucBetrieb.Channel_started(Channel);
                    Error_Detected = false;
                    timStateEngine.Start();
                    m_state_Last = gProcMain.idle;
                    m_state = gProcMain.getReadyForTest;
                    return true;
                }
                else
                {
                    MessageChannel = "ERROR: ComPort ist besetzt";
                }
            }
            else
            {
                MessageChannel = "ERROR: ComPort ist besetzt";
            }
            ucBetrieb.Channel_started("");
            return false;
        }
        public void Stop()
        {
            timProcess.Stop();
            timCalibration.Stop();
            timStateEngine.Stop();
            if (Running)
            {
                try
                {
                    DeviceCom.SendCMD(Instance, opcode.S999);
                    UC_Betrieb.Running_Count--;
                    StartReady = false;
                    Running = false;
                }
                catch (Exception e)
                {
                    ErrorHandler("Stop", exception: e);
                    MessageChannel = e.Message;
                }
            }
            ucCOM.Stop();
            Form_Main.UC_TT.SetFocus_Input();
        }
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            bool running = _Btn_Start.Text == "Start";
            if (running)
            { Start(); }
            else
            {
                timProcess.Stop();
                timCalibration.Stop();
                m_BoxReseted = false;
                stateMessageError = "User Cancelation";
                m_state = gProcMain.error;
            }
            Form_Main.UC_TT.SetFocus_Input();
        }

        [Obsolete("use clLog", true)]
        /***************************************************************************************
        * BackGroundWorker Write Messages
        ****************************************************************************************/
        public BackgroundWorker BGW_MessageWriter { get; set; }
        [Obsolete("use clLog", true)]
        private void BGW_MessageWriter_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument != null)
                {
                    string[] valuesInput = (string[])e.Argument;
                    string portName = valuesInput[0];
                    string message = valuesInput[1];
                    if (ParseMessage(portName, message, out string messageValues))
                    {
                        SaveLogMeas(messageValues, portName);
                        e.Result = message;
                    }
                }
            }
            catch (Exception a)
            {
                ErrorHandler("BGW_MessageWriter", exception: a);
            }
        }

        /***************************************************************************************
        * Log Message:
        ****************************************************************************************/
        [Obsolete("use clLog", true)]
        private bool ParseMessage(string portName, string message, out string messageValues)
        {
            messageValues = message;
            bool empty = string.IsNullOrEmpty(message);
            string bem = BeM_Selected;
            string wait = ReadDelay_Selected.ToString();
            switch (bem)
            {
                case "30326849": //O2 6850
                    if (empty) { return false; }
                    break;
                case "30259861": //Ozon bew sensor
                    if (message == "." || empty)
                    { return false; }
                    break;
                case "11556": //O2 allg
                    if (empty) { return false; }
                    break;
                case "30014462": //Ozon old sensor
                    if (message == "." || empty)
                    { return false; }
                    else if (message.Length == 3)
                    {
                        if (message.ToLower().Contains("?"))
                        { return false; }
                    }
                    break;
                case "No":
                    break;
            }
            string date = $"{DateTime.Now}.{DateTime.Now.Millisecond}";
            messageValues = $"{date}\t{portName}\t{bem}\t{wait}\t{message}";
            return true;
        }

        private string Create_TMP_MeasPath(string portName)
        {
            LogDirectory = Config_Initvalues.MeasLog_Path;
            string filename = "Calibox_" + portName + ".log";
            return (LogDirectory + @"\" + filename);
        }

        public string LogPathMeas { get; set; }
        public string LogDirectory { get; set; }
        public bool LogPathActive { get { return Config_Initvalues.MeasLog_Active; } }
        [Obsolete("use clLog", true)]
        private void SaveLogMeas(string values, string portName)
        {
            try
            {
                if (Directory.Exists(Config_Initvalues.MeasLog_Path) & Config_Initvalues.MeasLog_Active)
                {
                    int countAgain = 0;
                    string me = values.Replace("\r", "\r\t\t\t\t").Trim();
                again:
                    if (!File.Exists(LogPathMeas))
                    {
                        // Create a file to write to.
                        try
                        {
                            using (StreamWriter sw = File.CreateText(LogPathMeas))
                            { sw.WriteLine("Date\tCOM\tBeM\tWait\tMessage"); }
                        }
                        catch
                        {
                            ErrorHandler("SaveLogMeas", information: "NEW crash");
                            if (countAgain < 2)
                            {
                                countAgain++;
                                goto again;
                            }
                        }
                        countAgain = 0;
                    }
                    // This text is always added, making the file longer over time if it is not deleted.
                    try
                    {
                        using (StreamWriter sw = File.AppendText(LogPathMeas))
                        {
                            sw.WriteLine(me);
                        }
                    }
                    catch
                    {
                        ErrorHandler("SaveLogMeas", information: "ADD crash");
                        if (countAgain < 5)
                        {
                            countAgain++;
                            goto again;
                        }
                    }
                }
            }
            catch
            {
                MessageChannel = $"ERROR: Directory {Config_Initvalues.MeasLog_Path} don't found";
            }
        }


        /*******************************************************************************************************************
       'FUNCTION:    State Engine
       ********************************************************************************************************************/
        private System.Windows.Forms.Timer timStateEngine;
        private void Init_timStateEngine()
        {
            timStateEngine = new System.Windows.Forms.Timer() { Interval = 500 };
            timStateEngine.Tick += new EventHandler(timStateEngine_Tick);
        }

        public bool m_DBinit = false;
        public bool m_timeOutActive = false;
        public bool m_Calibration_Running = false;
        public bool m_TestRunning = false;
        public bool m_BoxReseted { get; set; } = false;
        
        public DateTime m_timeOutDevice;
        public DateTime m_timeOutTest;
        public int m_processCounter = 0;
        public int m_processCounterTotal = 0;
        public DateTime m_procStart;
        public DateTime m_procEnd;
        public int m_stateIntervalSTD = 250;
        private gProcMain m_state_Last;
        private gProcMain _m_state;
        public gProcMain m_state
        {
            get { return _m_state; }
            set
            {
                if (!timStateEngine.Enabled)
                { timStateEngine.Interval = m_stateIntervalSTD; timStateEngine.Start(); }
                _m_state = value;
            }
        }
        public gProcMain m_state_AfterWait;
        public gProcMain m_state_WaitCaller;
        private gProcMain m_state_ProcessRunning = gProcMain.idle;
        //public enum enProcesses
        //{
        //    getReadyForTest,
        //    BoxStatus,
        //    BoxReset,
        //    //BoxErrorStatus, /* nur während calibration möglich */
        //    FWcheck,
        //    //BoxGetErrorValues, /* nur während calibration möglich */
        //    TestFinished,
        //    StartProcess,
        //    Calibration,
        //    Bewertung,
        //    DBinit,
        //    wait,
        //    error,
        //    idle
        //};

        /*******************************************************************************************************************
        * FUNCTION:    timStateEngine_Tick
        '*******************************************************************************************************************/
        clDeviceCom DeviceCom = new clDeviceCom();
        CH_State calQuality = CH_State.nothing;
        
        string stateMessageError = "";
        DataRow rowState;
        bool newRowState;
        public void timStateEngine_Tick(object sender, EventArgs e)
        {
            if ((m_state != m_state_Last) || (m_timeOutActive))
            {
                m_state_Last = _m_state;
                string message = "";
                bool executed = false;
                switch (m_state)
                {
                    case gProcMain.getReadyForTest: /* Channel initialization */
                        if (m_timeOutActive) m_timeOutActive = false;
                        //Truncate_DB(); /* only for TESTS, this delete completely all database values */
                        timStateEngine.Interval = m_stateIntervalSTD;
                        message = $"state: {m_state}";
                        log.Save(Instance, message, opcode.state);
                        m_state = gProcMain.StartProcess;
                        break;
                    case gProcMain.StartProcess:
                        if (m_timeOutActive) m_timeOutActive = false;
                        m_state_ProcessRunning = m_state;
                        message = $"state: {m_state}";
                        log.Save(Instance, message, opcode.state);
                        MessageChannel = message;
                        m_state = gProcMain.idle;
                        timProcess.Start();
                        break;
                    case gProcMain.BoxStatus:
                        if (!m_timeOutActive) m_timeOutActive = true;
                        bool BoxFound = false;
                        if(m_state_ProcessRunning != m_state)
                        {
                            m_state_ProcessRunning = m_state;
                            m_processCounter = 0;
                            m_processCounterTotal = 3;
                        }
                        m_processCounter++;
                        if (m_processCounter < m_processCounterTotal)
                        {
                            BoxFound = DeviceCom.SendCMD(Instance, opcode.G100);
                        }
                        else
                        {
                            m_timeOutActive = false;
                            m_state = gProcMain.error;
                        }
                        stateMessageError = BoxFound ? "" : "Keine Antwort erhalten";
                        string info = $"{m_state}\tattents: {m_processCounter}/{m_processCounterTotal}\tBoxFound: {BoxFound} {stateMessageError}";
                        message = $"state: {info}";
                        MessageChannel = info;
                        log.Save(Instance, message, opcode.state);
                        if (BoxFound)
                        {
                            m_timeOutActive = false;
                            m_state = gProcMain.idle;
                        }
                        else
                        {
                            Wait(1000, m_state_ProcessRunning, info, true);
                        }
                        break;
                    case gProcMain.BoxReset:
                        if (m_timeOutActive) m_timeOutActive = false;
                        m_state_ProcessRunning = m_state;
                        if (!m_BoxReseted)
                        {
                            message = $"state: {m_state}";
                            MessageChannel = "BoxReset";
                            log.Save(Instance, message, opcode.state);
                            if (!DeviceCom.SendCMD(Instance, opcode.S999))
                            {
                                stateMessageError = "keine Kommunikation";
                                message += $" {stateMessageError}";
                                m_state = gProcMain.error;
                            }
                            else
                            {
                                m_BoxReseted = true;
                                Wait(2000, gProcMain.idle, m_state.ToString(), true);
                            }
                        }
                        else { m_state = gProcMain.idle; }
                        break;
                    case gProcMain.TestFinished:
                        
                        if (m_timeOutActive) m_timeOutActive = false;
                        m_state_ProcessRunning = m_state;
                        message = $"state: {m_state}";
                        //MessageChannel = message;
                        log.Save(Instance, message, opcode.state);
                        if (!m_TestRunning)
                        { Stop(); }
                        else
                        { m_state = gProcMain.Bewertung; }
                        break;
                    case gProcMain.FWcheck:
                        if (!Limits.sample_FW_Version_active)
                        {
                            if (!m_timeOutActive) m_timeOutActive = true;
                            if (m_state_ProcessRunning != m_state)
                            {
                                m_TestRunning = true;
                                message = $"state: {m_state}\tstarted";
                                m_state_ProcessRunning = m_state;
                                m_processCounter = 0;
                                m_processCounterTotal = 5;
                                MessageChannel = "FWcheck started";
                            }
                            m_processCounter++;
                            string attents = $"{m_processCounter}/{m_processCounterTotal}";
                            string fwVersion;
                            bool counterProc = m_processCounter == m_processCounterTotal || m_processCounter > m_processCounterTotal;
                            executed = DeviceCom.Get_Sample_FWversion(Instance, out fwVersion);
                            Limits.sample_FW_Version_value = fwVersion;
                            string fwmessage = $"DB: {Limits.sample_FW_Version} Sensor: {fwVersion}";
                            message = $"state: {m_state}\tattents: {attents}\t{fwmessage}";
                            if (Limits.sample_FW_Version_ok)
                            {
                                m_state = gProcMain.idle;
                                m_timeOutActive = false;
                            }
                            else
                            {
                                if (!counterProc)
                                {
                                    info = $"FWcheck {attents} {fwmessage}";
                                    Wait(3000, m_state_ProcessRunning, info, true);
                                }
                                else
                                {
                                    stateMessageError = $"{fwmessage}";
                                    Error_Detected = true;
                                    m_state = gProcMain.error;
                                }
                            }
                            Calibration_Proc_Track("", "FWversion", fwmessage, Limits.sample_FW_Version_state.ToString(), true);
                            log.Save(Instance, message, opcode.state);
                        }
                        SampleResponse.BoxMeasValue = null;
                        break;
                    case gProcMain.Calibration:
                        if (!m_timeOutActive)  m_timeOutActive = true;
                        if (timProcess.Enabled) { timProcess.Stop(); }
                        //if (!m_Calibration_Running)
                        if (m_state_ProcessRunning != m_state)
                        {
                            m_state_ProcessRunning = m_state;
                            m_Calibration_Running = true;
                            string cmdInfo = $"startet cmd: {CalMode}-{CalModeDesc}"; 
                            message = $"state: {m_state}\t{cmdInfo}";
                            if (!DeviceCom.SendCMD(Instance, CalMode))
                            {
                                stateMessageError = $"Calibrations Modus {CalMode}/{CalModeDesc}";
                                Calibration_Proc_Track("", CalMode.ToString(), stateMessageError , "1", true);
                                m_state = gProcMain.error;
                                m_timeOutActive = false;
                                log.Save(Instance, message, opcode.state);
                                break;
                            }
                            else
                            {
                                Calibration_Proc_Track("", CalMode.ToString(), CalModeDesc, "00", true);
                                log.Save(Instance, message, opcode.state);
                                info = $"Calibration {cmdInfo}";
                                Wait(1000, gProcMain.Calibration, info, true);
                                break;
                            }
                        }
                        if (!timCalibration.Enabled) { timCalibration.Start(); }
                        break;
                    case gProcMain.wait:
                        if (!WaitRunning)
                        {
                            timStateEngine.Interval = 250;
                            WaitRunning = true;
                            m_timeOutDevice = DateTime.Now.AddMilliseconds(m_timStateEngine_Waitms);
                            message = $"state: {m_state}\tduration: {m_timStateEngine_Waitms/1000} sec";
                            m_timeOutActive = true;
                            timCalibration.Stop();
                            timProcess.Stop();
                            timMeasurement.Stop();
                            log.Save(Instance, message, opcode.state);
                        }
                        double timeDiffDevice = (m_timeOutDevice - DateTime.Now).TotalSeconds;
                        Waittime_Show(timeDiffDevice, WaitMessage);
                        if (!(timeDiffDevice < 0)) { return; }
                        WaitRunning = false;
                        timStateEngine.Interval = m_stateIntervalSTD;
                        m_timeOutActive = WaitTimeOutActive;
                        if (processRunning) { timProcess.Start(); }
                        if (calibrationRunning) { timCalibration.Start(); }
                        m_state = m_state_AfterWait;
                        break;
                    case gProcMain.Bewertung:
                        if (m_timeOutActive) m_timeOutActive = false;
                        m_state_ProcessRunning = m_state;
                        string state = m_state.ToString();
                        m_TestRunning = false;
                        try
                        {
                            rowState = Search_Progress(out newRowState, out int indexBewertung, searchValue: "CalMode");
                            rowState["meas_time_end"] = DateTime.Now;
                        }
                        catch { }
                        Limits.meas_time_end = DateTime.Now;
                        Limits.test_ok = calQuality == CH_State.QualityGod ? true : false;
                        if (!Save_Values_To_DB())
                        {
                            stateMessageError = "DataBank speichern Bewertung";
                            m_state = gProcMain.error;
                        }
                        else
                        {
                            if(processeNr == processeCount-1 || Error_Detected)
                            { m_state = gProcMain.TestFinished; }
                            else { m_state = gProcMain.idle; }
                        }
                        string d = String.Format("{0:N0}", (CalibrationEnd - CalibrationStart).TotalSeconds);
                        message = $"state: {state} {calQuality}-{Limits.test_ok}\tduration: {d} sec\tgoto: {m_state}";
                        log.Save(Instance, message, opcode.state);
                        if (!Error_Detected) { MessageChannel = message; }
                        //GUI_TXT(message, true);
                        break;
                    case gProcMain.DBinit:
                        if (m_timeOutActive) m_timeOutActive = false;
                        m_state_ProcessRunning = m_state;
                        m_TestRunning = true;
                        message = $"state: {m_state}\tsensorID: {Limits.sensor_id}\tTAG-Nr: {Limits.tag_nr}\tPassNo: {Limits.pass_no}";
                        if (!Save_Values_To_DB_INIT())
                        {
                            stateMessageError = "DataBank speichern INIT";
                            message += $" {stateMessageError}";
                            m_state = gProcMain.error;
                        }
                        else
                        {
                            m_DBinit = true;
                            m_state = gProcMain.idle;
                        }
                        log.Save(Instance, message, opcode.state);
                        break;
                    case gProcMain.error:
                        if (m_timeOutActive) m_timeOutActive = false;
                        m_timeOutActive = false;
                        Error_Detected = true;
                        message = $"{m_state.ToString().ToUpper()}: {stateMessageError}\t{m_state_Last}/{m_state_ProcessRunning}";
                        DGV_Calibration.ClearSelection();
                        if (!string.IsNullOrEmpty(stateMessageError))
                        { GUI_TXT(message, true); }
                        log.Save(Instance, message, opcode.Error);
                        stateMessageError = "";
                        m_processCounter = 0;
                        timProcess.Stop();
                        timCalibration.Stop();
                        timMeasurement.Stop();
                        if (m_TestRunning)
                        {
                            m_state = gProcMain.Bewertung;
                            break;
                        }
                        m_timeOutActive = false;
                        m_state = gProcMain.idle;
                        Stop();
                        break;
                    case gProcMain.idle:
                        m_timeOutActive = false;
                        timStateEngine.Interval = m_stateIntervalSTD;
                        break;
                    default:
                        m_timeOutActive = false;
                        timStateEngine.Interval = m_stateIntervalSTD;
                        break;
                }
            }
        }

        #region GUI Message
        /***************************************************************************************
        * GUI Message:  Error
        ****************************************************************************************/
        public string ErrorMessageMain
        {
            get { return UC_Betrieb.ErrorMessageMain; }
            set { UC_Betrieb.ErrorMessageMain = value; }
        }
        public string MessageChannel
        {
            get { return Tb_Info.Text; }
            set { GUI_Error_TXT(value); }
        }
        public string MessageChannel_Add
        {
            get { return Tb_Info.Text; }
            set { GUI_Error_TXT(value,true); }
        }

        public bool Error_Detected;

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
                string show = (message + Environment.NewLine + Tb_Info.Text);
                int count = show.Split('\r').Length - 1;
                if (count < 20)
                { Tb_Info.Text = show.Trim(); }
                else
                {
                    int length = show.LastIndexOf(Environment.NewLine);
                    Tb_Info.Text = show.Substring(0, length).Trim();
                }
            }
            else { Tb_Info.Text = message; }
            if (error)
            {
                Error_Detected = true;
                if (!Config_Initvalues.Log_Active)
                { log.Save(Instance, message, opcode.Error); }
            }
            Tb_Info.Refresh();
        }
        private void GUI_Error_TXT(string message, bool addMessage = false)
        {
            Task.Factory.StartNew(() =>
            {
                if (InvokeRequired)
                {
                    try { this.Invoke((MethodInvoker)delegate { GUI_TXT(message, addMessage); }); }
                    catch (Exception a)
                    { ErrorHandler("GUI_Error_TXT", exception: a); }
                }
                else
                { GUI_TXT(message, addMessage); }
            });
        }

        #endregion GUI Message

        /*******************************************************************************************************************
        'FUNCTION:    Wait
        ********************************************************************************************************************/
        bool processRunning = false;
        public bool calibrationRunning = false;
        bool WaitTimeOutActive = false;
        public int m_timStateEngine_Waitms = 1000;
        bool WaitRunning = false;
        string WaitMessage = "";
        private void Wait(int wait, gProcMain nextStep, gProcMain message, bool start = false)
        {
            Wait(wait, nextStep, message.ToString(), start);
        }
        private void Wait(int wait, gProcMain nextStep, string message, bool start = false)
        {
            WaitTimeOutActive = m_timeOutActive;
            WaitRunning = false;
            processRunning = timProcess.Enabled;
            calibrationRunning = timCalibration.Enabled;
            m_state_AfterWait = nextStep;
            m_timStateEngine_Waitms = wait;
            WaitMessage = message;
            if (start)
            {
                timStateEngine.Interval = 10;
                m_state = gProcMain.wait;
            }
        }

        public void Waittime_Show(double timeDiff, string message)
        {
            string time = "";
            if (timeDiff > 0) { time = String.Format("wait... {0:N0} sec ", timeDiff); }
            string messageCPL = $"{time}\r\n{message}";
            GUI_TXT(messageCPL, false);
        }
        public void Waittime_Show(double timeDiff, gProcMain callerState, string message = null)
        {
            string time = "";
            if (timeDiff > 0) { time = String.Format("wait... {0:N0} sec ", timeDiff); }
            string messageCPL = $"{callerState}\r\n{time}\r\n{message}";
            GUI_TXT(messageCPL, false);
        }
        public void Waittime_Show(DateTime dtOld, gProcMain callerState, string message = null)
        {
            Waittime_Show(CalDurationSec - ((DateTime.Now - dtOld).TotalSeconds), callerState, message);
        }
        public void Waittime_Show(DateTime dtOld, string message = null)
        {
            Waittime_Show(CalDurationSec - ((DateTime.Now - dtOld).TotalSeconds), message);
        }

        /*******************************************************************************************************************
        'FUNCTION:    Process
        ********************************************************************************************************************/
        private System.Windows.Forms.Timer timProcess;
        private void Init_timProcess()
        {
            timProcess = new System.Windows.Forms.Timer() { Interval = 1000 };
            timProcess.Tick += new EventHandler(timProcesse_Tick);
        }
        

        gProcMain ProcesseLast = gProcMain.wait;
        gProcMain ProcesseRunning = gProcMain.idle;
        int processeNr = 0;
        int _processCount = 0;
        int processeCount
        {
            get
            {
                if (_processCount == 0)
                { _processCount = gProcOrder.Length; }
                return _processCount;
            }
        }
        bool ProcesseLastArrived { get { return processeNr == processeCount; } }
        public void timProcesse_Tick(object sender, EventArgs e)
        {
            if (m_state == gProcMain.error)
            {
                timCalibration.Stop();
                timMeasurement.Stop();
                timProcess.Stop();
                return;
            }
            else if (m_state == gProcMain.idle && ProcesseRunning != ProcesseLast)
            {
                switch (ProcesseRunning)
                {
                    case gProcMain.idle:
                        processeNr = -1;
                        goto default;
                    case gProcMain.Bewertung:
                        timCalibration.Stop();
                        timMeasurement.Stop();
                        goto default;
                    case gProcMain.FWcheck:
                        calQuality = CH_State.QualityBad;
                        goto default;
                    default:
                        if (!ProcesseLastArrived) { processeNr++; }
                        ProcesseLast = ProcesseRunning;
                        ProcesseRunning = gProcOrder[processeNr];
                        m_state = ProcesseRunning;
                        break;
                }
                if (ProcesseLastArrived)
                {
                    timCalibration.Stop();
                    timMeasurement.Stop();
                    timProcess.Stop();
                }
            }
        }


        /*******************************************************************************************************************
        'FUNCTION:    Save Result to DataBase
        ********************************************************************************************************************/
        private bool Save_Values_To_DB()
        {
            bool result = clDatenBase.MeasVal_Update(ODBC_EK, Limits);
            clDatenBase.MeasValTemp_Insert(ODBC_EK, Limits.sensor_id, Limits.pass_no.ToString(), DT_Calibration);
            return result;
        }

        private bool Save_Values_To_DB_INIT()
        { return clDatenBase.MeasVal_Insert_Init(ODBC_EK, Limits); }

        private void Truncate_DB()
        {
            clDatenBase.Truncate_DB(ODBC_EK);
        }


        /*******************************************************************************************************************
        'Values:     Device Measurement Values
        ********************************************************************************************************************/

        private string lLast = "";
        private DeviceResponse _SampleResponse = new DeviceResponse(null);
        public DeviceResponse SampleResponse
        {
            get
            {
                if (BoxMode_Last != BoxMode && !BoxMode_Empty && BoxMode != lLast)
                {
                    lLast = BoxMode;
                    Task.Factory.StartNew(() =>
                    { Calibration_Proc_Track(BoxMode_Last, BoxMode); });
                }
                else if(BoxMode_Last == BoxMode && !BoxMode_Empty)
                {
                    BoxErrorCode_Last = BoxErrorCode;
                    BoxErrorCodeDesc_Last = BoxErrorCodeDesc;
                    BoxMeasValue_Last = BoxMeasValue;
                }
                return _SampleResponse;
            }
            set { _SampleResponse = value; }
        }

        public string BoxMode { get { return _SampleResponse.BoxMode_hex;} }
        public bool BoxMode_Empty { get { return _SampleResponse.BoxMode_Empty; } }
        public string BoxMode_Last;

        public string BoxModeDesc {  get { return _SampleResponse.BoxMode_desc; } }
        public string BoxMode_Desc_Last { get { return clHandler.BoxMode[BoxMode_Last]; } }

        public string BoxErrorCode { get {return  _SampleResponse.BoxErrorCode_hex; }  }
        public string BoxErrorCode_Last;
        public string BoxErrorCodeDesc { get {return _SampleResponse.BoxErrorCode_desc; } }
        public string BoxErrorCodeDesc_Last;

        public string BoxMeasValue { get { return _SampleResponse.BoxMeasValue; } }
        public string BoxMeasValue_Last;

        /*******************************************************************************************************************
        * FUNCTION:    Process Calibration GUI
        ********************************************************************************************************************/
        private DataTable _DT_Calibration;
        public DataTable DT_Calibration
        {
            get
            {
                if (_DT_Calibration == null)
                { _DT_Calibration = Init_DT_Calibration(); }
                return _DT_Calibration;
            }
            set { _DT_Calibration = value; }
        }
        private DataTable Init_DT_Calibration()
        {
            DataTable dt = new DataTable() { TableName = "Calibration" };
            dt.Columns.Add("boxmode_hex");
            dt.Columns.Add("boxmode_desc");
            dt.Columns.Add("meas_time_start", typeof(DateTime));
            dt.Columns.Add("meas_time_end", typeof(DateTime));
            dt.Columns.Add("duration");
            dt.Columns.Add("values");
            dt.Columns.Add("boxerror_hex");
            dt.Columns.Add("boxerror_desc");
            return dt;
        }
        public void Reset_DT_Calibration()
        {
            DT_Calibration.Rows.Clear();
            DGV_Calibration.ClearSelection();
        }

        private DataRow Search_Progress(out bool newRow, out int index, string searchColumn = "boxmode_hex", string searchValue = null)
        {
            index = -1;
            DataRow row = DT_Calibration.NewRow();
            searchValue = searchValue == null ? BoxMode : searchValue;
            DataRow[] rows = DT_Calibration.Select($"{searchColumn} = '{searchValue}'");
            newRow = (rows.Count() > 0) ? false : true;
            if (!newRow)
            {
                row = rows[0];
                index = DT_Calibration.Rows.IndexOf(row);
            }
            return row;
        }

        DateTime CalibrationStartBoxMode = DateTime.Now;
        public void Calibration_Proc_Track(string boxlastmode, string boxmode, string boxmeasvalue = null, string boxerrorcode = null, bool inputValues = false)
        {
            string boxmodedesc = null;
            if(boxmode == "32" || boxmode == "39") { BoxMode_Last = boxmode; return; } /*32-Box_Idle, 39-ShowErrorValues*/
            try
            {
                if ((boxmode != boxlastmode) || (inputValues))
                {
                    string lastSearcher = BoxMode_Last;
                    BoxMode_Last = boxmode;
                    int selectedIndex = -1;
                    string boxerrorcodedesc = null;
                    if (!string.IsNullOrEmpty(boxmode))
                    {
                        try { boxmodedesc = clHandler.BoxMode[boxmode]; } catch { boxmodedesc = $"ERROR {boxmode}"; }
                    }
                    else { return; }
                    if (boxmeasvalue == null) { boxmeasvalue = BoxMeasValue; }
                    if (boxerrorcode == null)
                    {
                        boxerrorcode = BoxErrorCode;
                        boxerrorcodedesc = BoxErrorCodeDesc;

                    }
                    else if (!string.IsNullOrEmpty(boxerrorcode))
                    {
                        if (boxerrorcode == "2") { boxerrorcode = "00"; }
                        try { boxerrorcodedesc = clHandler.BoxErrorCode[boxerrorcode]; } catch { boxerrorcodedesc = $"ERROR {boxerrorcode}"; }
                    }

                    DateTime start = DateTime.Now;
                    DataRow row = Search_Progress(out bool newRow, out selectedIndex, searchValue: boxmode);
                    if (newRow)
                    {
                        if (!string.IsNullOrEmpty(boxmode))
                        { row["boxmode_hex"] = boxmode; }
                        if (!string.IsNullOrEmpty(boxmodedesc))
                        { row["boxmode_desc"] = boxmodedesc; }
                        row["meas_time_start"] = start;
                    }
                    if (inputValues)
                    {
                        DateTime end = DateTime.Now;
                        row["meas_time_end"] = end;
                        if (!string.IsNullOrEmpty(boxmeasvalue))
                        { row["values"] = boxmeasvalue; }
                        if (!string.IsNullOrEmpty(boxerrorcode))
                        { row["boxerror_hex"] = boxerrorcode; }
                        if (!string.IsNullOrEmpty(boxerrorcodedesc))
                        { row["boxerror_desc"] = boxerrorcodedesc; }
                        try
                        {
                            start = row.Field<DateTime>("meas_time_start");
                            row["duration"] = (end - start).TotalSeconds;
                        }
                        catch { }
                    }

                    if (!string.IsNullOrEmpty(lastSearcher) && !inputValues && lastSearcher != "32")
                    {
                        DataRow rowlast = Search_Progress(out bool newRowLast, out selectedIndex, searchValue: lastSearcher);
                        if (selectedIndex > -1)
                        {
                            DateTime end = DateTime.Now;
                            rowlast["meas_time_end"] = end;
                            if (!string.IsNullOrEmpty(BoxMeasValue_Last))
                            { rowlast["values"] = BoxMeasValue_Last; }
                            if (!string.IsNullOrEmpty(BoxErrorCode_Last))
                            {
                                rowlast["boxerror_hex"] = BoxErrorCode_Last;
                                if (!string.IsNullOrEmpty(BoxErrorCodeDesc_Last))
                                { rowlast["boxerror_desc"] = BoxErrorCodeDesc_Last; }
                            }
                            try
                            {
                                start = rowlast.Field<DateTime>("meas_time_start");
                                rowlast["duration"] = (end - start).TotalSeconds;
                            }
                            catch { }
                        }
                    }
                    try
                    {
                        if (newRow)
                        {
                            DT_Calibration.Rows.Add(row);
                            if (selectedIndex < 0)
                            {
                                selectedIndex = DT_Calibration.Rows.IndexOf(row);
                            }
                        }
                    }
                    catch { }
                    int count = DGV_Calibration.Rows.Count;
                    if (count > 0)
                    {
                        if (count > 3)
                        {
                            try { DGV_Calibration.FirstDisplayedScrollingRowIndex = count - 3; } catch { }
                        }
                        try
                        {
                            int index = count - 1;
                            if (index > -1 && selectedIndex > -1)
                            {

                                clMTcolors.selection colorSelected = selection.Background_EMPTY;
                                var e = DT_Calibration.Rows[selectedIndex]["boxerror_hex"] ?? "";
                                string r = e.ToString();
                                if (r != "")
                                { colorSelected = r =="00"? selection.rating_GOD_active: selection.rating_BAD_active; }
                                try { DGV_Calibration.Rows[selectedIndex].DefaultCellStyle.BackColor = clMTcolors.MT_Color(colorSelected); } catch { }
                            }
                            try { if (index > -1) { DGV_Calibration.Rows[index].Selected = true; } } catch { }
                        }
                        catch (Exception e)
                        { log.Save(Instance, e.Message, opcode.Error); }
                    }
                    try { DGV_Calibration.Refresh(); } catch { }
                }
            }
            catch (Exception a)
            { log.Save(Instance, a.Message, opcode.Error); }
        }

        /*******************************************************************************************************************
        * FUNCTION:    Process Calibration Variables
        ********************************************************************************************************************/
        private System.Windows.Forms.Timer timCalibration;
        private void Init_timCalibration(bool start = false, int interval = 250)
        {
            timCalibration = new System.Windows.Forms.Timer() { Interval = interval };
            timCalibration.Tick += new EventHandler(timCalibration_Tick);
            if (start)
            { timCalibration.Start(); }
        }

        opcode CalMode;
        int CalDuration = 0;
        int CalDurationSec = 0;
        private opcode Choise_CalMode()
        {
            CalDuration = 20;
            string modeDesc = "500/674mV";
            if (Limits.pol_voltage_cal_multi)
            { CalMode = opcode.S100; }
            else
            {
                CalDuration = 15;
                if(Limits.pol_voltage == -500)
                {
                    CalMode = opcode.S500;
                    modeDesc = "500mV";
                }
                else
                {
                    modeDesc = "674mV";
                    CalMode = opcode.S674;
                }
            }
            CalModeDesc = modeDesc;
            TimeStart = DateTime.Now.ToShortTimeString();
            TimeEnd = DateTime.Now.AddMinutes(CalDuration).ToShortTimeString();
            CalDurationSec = CalDuration * 60;
            return CalMode;
        }

        DateTime CalibrationStart = DateTime.Now;
        DateTime CalibrationEnd = DateTime.Now;

        /*******************************************************************************************************************
        *FUNCTION:    State Engine Calibration
        ********************************************************************************************************************/
        int CountCalib = 0;
        int CountCommandChange = 0;
        int CalibNoAnswerCounter = 0;
        string CalibLastBoxMode = "";
        bool autoValue = false;
        string noAnswer = "";
        public void timCalibration_Tick(object sender, EventArgs e)
        {
            if (m_state == gProcMain.error)
            { timProcess.Stop(); return; }

            if (m_state == gProcMain.Calibration)
            {
                if (CountCalib == 0)
                {
                    timCalibration.Interval = 3000;
                    CountCommandChange = 0;
                    CalibNoAnswerCounter = 0;
                    autoValue = false;
                    CalibrationStart = DateTime.Now;
                    calibrationRunning = true;
                    CalibLastBoxMode = SampleResponse.BoxMode_hex;
                    //device.SendCMD(Instance, opcode.G100);
                }
                CountCalib++;

                if (!DeviceCom.SendCMD(Instance, opcode.G901))
                {
                    CalibNoAnswerCounter++;
                    if (CountCalib < 30)
                    {
                        //DeviceCom.SendCMD(Instance, opcode.G100);
                        if (BoxModeDesc.ToLower().Contains("calib"))
                        {
                            DeviceCom.SendCMD(Instance, opcode.S901);
                            //DeviceCom.SendCMD(Instance, opcode.G901);
                            timCalibration.Interval = 1000;
                        }
                        else { timCalibration.Interval = 3000; }
                    }
                    else if (CalibNoAnswerCounter > 15)// || CalibLastBoxMode != SampleResponse.BoxMode_hex)
                    {
                        CalibLastBoxMode = SampleResponse.BoxMode_hex;
                        if (!DeviceCom.SendCMD(Instance, opcode.G100))
                        {
                            timMeasurement.Stop();
                            stateMessageError = "timCalibration keine Antwort";
                            m_state = gProcMain.error;
                        }
                        CalibNoAnswerCounter = 0;
                        CountCommandChange++;
                        if(CountCommandChange > 3)
                        {
                            m_state = gProcMain.error;
                        }
                    }
                }
                else
                {
                    autoValue = true;
                    CountCommandChange = 0;
                    CalibNoAnswerCounter = 0;
                }
                if (!ChB_Chart.Visible)
                {
                    if (SampleResponse.DT_Measurements.Rows.Count > 0)
                    {
                        ChB_Chart.Visible = true;
                        ChB_Chart.Checked = true;
                    }
                }
            }
            CalibrationEnd = DateTime.Now;
            if (SampleResponse.TestFinalise || SampleResponse.TestError)
            {
                timMeasurement.Stop();
                if (!timProcess.Enabled) { timProcess.Start(); }
                DeviceCom.SendCMD(Instance, opcode.G200);
                stateMessageError = $"{BoxModeDesc} {SampleResponse.ResponseParsed}";
                if (!SampleResponse.TestError)
                {
                    Limits.test_ok = true;
                    ChB_Chart.Checked = false;
                    calQuality = CH_State.QualityGod;
                    Wait(2000, gProcMain.idle, m_state, true);
                }
                else
                {
                    Wait(2000, gProcMain.error, m_state, true);
                }
                m_Calibration_Running = false;
                timCalibration.Stop();
            }
            else
            {
                noAnswer = $"{BoxModeDesc.Replace("_", " ").Replace("Mode","").Replace("Box","")}\tCount: {CountCalib}; NoAnswer: {CalibNoAnswerCounter}";
                Waittime_Show(CalibrationStart, noAnswer);
            }
        }

        /*******************************************************************************************************************
       * FUNCTION:    Chart Measurement Timer
       ********************************************************************************************************************/
        private System.Windows.Forms.Timer _timMeasurement;
        private System.Windows.Forms.Timer timMeasurement { get { if(_timMeasurement == null) { Init_timMeasurement(); }return _timMeasurement; } }
        private void Init_timMeasurement(bool start = false, int interval = 1000)
        {
            _timMeasurement = new System.Windows.Forms.Timer() { Interval = interval };
            _timMeasurement.Tick += new EventHandler(timMeasurement_Tick);
            if (start)
            { timMeasurement.Start(); }
        }
        public void timMeasurement_Tick(object sender, EventArgs e)
        {
            if(SampleResponse.DT_Measurements.Rows.Count > 0)
            {
                DataRow[] rows = SampleResponse.DT_Measurements.Select($"boxmode_hex = '{BoxMode}'");
                int rCount = rows.Length-1;
                if (rCount > 0)
                {
                    if (rCount > Chart_ShowVaulesQuantiy-1)
                    {
                        DataTable dt2 = DT_MeasTemp.Clone();
                        try
                        {
                            for (int i = Chart_ShowVaulesQuantiy; i > 0; i--)
                            { dt2.ImportRow(rows[rCount - i]); }
                            DT_MeasTemp = dt2.Copy();
                        }
                        catch (Exception b)
                        { }
                    }
                    else { DT_MeasTemp = rows.CopyToDataTable(); }
                    try
                    {
                        Chart_Measurement.DataSource = DT_MeasTemp;
                        Chart_Measurement.DataBind();
                    }
                    catch { }
                    try
                    {
                        switch (ChartMode_Selection)
                        {
                            case ChartMode.STDdeviation:
                                //if (rCount > 15)
                                {
                                    double stdDev = 0;
                                    try { stdDev = Convert.ToDouble(rows[rCount]["stddeviation"]); } catch { }
                                    Chart_Measurement.Series["stddeviation"].Color = stdDev > 2 ? MT_rating_Bad_Active : MT_rating_God_Active;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception a)
                    {
                    }
                }
            }
        }

        /****************************************************************************************************
         * Chart:       Measurement
         ***************************************************************************************************/
        public enum ChartMode { STDdeviation, MeanValue, ErrorValue, RefMean, All, RefValue, STD_Error}
        public ChartMode ChartMode_Selection = ChartMode.STDdeviation;
        private DataTable _DT_MeasTemp;
        private DataTable DT_MeasTemp
        {
            get
            {
                if(_DT_MeasTemp == null)
                {
                    _DT_MeasTemp = SampleResponse.DT_Measurements.Clone();
                }
                return _DT_MeasTemp;
            }
            set { _DT_MeasTemp = value; }
        }
        private void Init_Chart()
        {
            Chart_Measurement.Visible = false;
            Chart_Measurement.DataSource = DT_MeasTemp;// SampleResponse.DT_Measurements;
            Chart_Measurement.DataBind();
            Chart_Measurement.Series.Clear();
            /*Mode MeanValue*/
            var serie_refvalue = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "refvalue",
                Color = System.Drawing.Color.Gray,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = false,
                //IsXValueIndexed = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 4,
                XValueMember = "ID",
                YValueMembers = "refvalue"
            };
            /*Mode MeanValue*/
            var serie_meanvalue = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "meanvalue",
                Color = System.Drawing.Color.Yellow,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = false,
                //IsXValueIndexed = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 4,
                XValueMember = "ID",
                YValueMembers = "meanvalue"
            };
            /*Mode STDdeviation*/
            var serie_stddev = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "stddeviation",
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = false,
                //IsXValueIndexed = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 4,
                XValueMember = "ID",
                YValueMembers = "stddeviation"
            };
            /*Mode ErrorValue*/
            var serie_errorvalue = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "errorvalue",
                Color = System.Drawing.Color.Orange,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = false,
                //IsXValueIndexed = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 4,
                XValueMember = "ID",
                YValueMembers = "errorvalue"
            };
            Chart_Measurement.Series.Add(serie_refvalue);
            Chart_Measurement.Series.Add(serie_meanvalue);
            Chart_Measurement.Series.Add(serie_stddev);
            Chart_Measurement.Series.Add(serie_errorvalue);
            Chart_Measurement.ChartAreas[0].AxisY.IsStartedFromZero = false;
            Chart_Measurement.ChartAreas[0].AxisX.Interval = 3;
            Chart_Measurement.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart_Measurement.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 6);
            Chart_Measurement.ChartAreas[0].AxisX.IsMarginVisible = false;
            Chart_Measurement.ChartAreas[0].AxisY.IsMarginVisible = false;
            Chart_Measurement.ChartAreas[0].AxisY.LabelStyle.Format = "0.##";
            ChB_Chart.Checked = false;
        }

        private int Chart_ShowVaulesQuantiy = 15;

        private void Change_ChartModeSelection(ChartMode selection)
        {
            if(ChartMode_Selection == selection) { return; }
            bool refValues = false;
            bool meanValues = false;
            bool stdValues = false;
            bool errorValues = false;
            switch (selection)
            {
                case ChartMode.STDdeviation:
                    Chart_ShowVaulesQuantiy = 10;
                    stdValues = true;
                    break;
                case ChartMode.MeanValue:
                    Chart_ShowVaulesQuantiy = 15;
                    meanValues = true;
                    break;
                case ChartMode.ErrorValue:
                    Chart_ShowVaulesQuantiy = 15;
                    errorValues = true;
                    break;
                case ChartMode.RefMean:
                    Chart_ShowVaulesQuantiy = 15;
                    refValues = true;
                    meanValues = true;
                    break;
                case ChartMode.All:
                    Chart_ShowVaulesQuantiy = 15;
                    refValues = true;
                    meanValues = true;
                    stdValues = true;
                    errorValues = true;
                    break;
                case ChartMode.RefValue:
                    Chart_ShowVaulesQuantiy = 15;
                    refValues = true;
                    break;
                case ChartMode.STD_Error:
                    Chart_ShowVaulesQuantiy = 10;
                    errorValues = true;
                    stdValues = true;
                    break;
                default:
                    Chart_ShowVaulesQuantiy = 15;
                    break;
            }
            Chart_Measurement.Series["refvalue"].Enabled = refValues;
            Chart_Measurement.Series["meanvalue"].Enabled = meanValues;
            Chart_Measurement.Series["stddeviation"].Enabled = stdValues;
            Chart_Measurement.Series["errorvalue"].Enabled = errorValues;
            ChartMode_Selection = selection;
        }
        private void Chart_Visible(bool visible)
        {
            Chart_Measurement.Visible = visible;
            if (visible)
            {
                Change_ChartModeSelection(ChartMode_Selection);
                if (calibrationRunning)
                { timMeasurement.Start(); }
                Chart_Measurement.BringToFront();
            }
            else
            {
                timMeasurement.Stop();
            }
        }

        private void ChB_Chart_CheckedChanged(object sender, EventArgs e)
        {
            Chart_Visible(ChB_Chart.Checked);
        }




        /****************************************************************************************************
         * Chart:       Measurement Menu
         ***************************************************************************************************/

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
            Change_ChartModeSelection(ChartMode.All);
        }

        private void refMeanValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.RefMean);
        }

        private void sTDDeviationErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_ChartModeSelection(ChartMode.STD_Error);
        }
    }


}
