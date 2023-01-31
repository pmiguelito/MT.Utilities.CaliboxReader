using System.Threading;
using System;
using System.Text;
using System.Threading.Tasks;
using CaliboxLibrary.StateMachine;
using System.Linq;
using System.Windows.Forms;

namespace CaliboxLibrary
{
    public class InternalProcesses
    {

        /**********************************************************
        * FUNCTION:     State Machine
        * DESCRIPTION:  IN WORK, NOT ACTIVE
        *               actually 1:1 copy from UC_Channel
        *               
        *               IDEE: move all integrated functions and processes
        *               to library
        ***********************************************************/

        public InternalProcesses(Logger logger, DeviceCom deviceCom)
        {
            _Logger = logger;
            DeviceCom = deviceCom;
            DeviceCom.DataReceived += DeviceCom_DataReceived;
        }

        private void DeviceCom_DataReceived(object sender, DeviceResponseValues e)
        {
            ProcCommands(ProcessWorker.Current.ProcName);
        }

        private readonly Logger _Logger = new Logger();
        public DeviceCom DeviceCom { get; private set; }
        /// <summary>
        /// Item Informations and Limits
        /// </summary>
        public ChannelValues ItemValues
        {
            get { return DeviceCom?.ItemValues; }
            set { DeviceCom.ItemValues = value; }
        }

        public CH_State calQuality = CH_State.idle;

        public string stateMessageError = "";

        #region Events
        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:  
        ***********************************************************/
        public event EventHandler<string> InformationReceived;
        protected virtual void OnInformationReceived(string message)
        {
            InformationReceived?.Invoke(this, message);
        }
        public void Gui_Message(string time, gProcMain callerState = gProcMain.idle, string message = null, string attents = null)
        {
            Task.Factory.StartNew(() =>
            {
                var sb = new StringBuilder();
                if (callerState != gProcMain.idle)
                { sb.Append(callerState.ToString()); }
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
                OnInformationReceived(sb.ToString());
            });
        }
        public void Gui_Message(gProcMain callerState = gProcMain.idle, string message = null, string attents = null)
        {
            Gui_Message(null, callerState, message, attents);
        }
        #endregion

        #region Message Progress
        /**********************************************************
        * FUNCTION:     Message Progress
        * DESCRIPTION:  Progress show and set values to DataGridView
        ***********************************************************/
        private void Set_Progress(BoxModeCodes code, BoxErrorMode error, string value = null)
        {
            DeviceCom.SetProgress(code, error, value);
        }
        private void Set_Progress(string boxmode_hex, string boxmode_desc = null, string value = null, string errorcode = null)
        {
            if (string.IsNullOrEmpty(boxmode_desc)) { boxmode_desc = boxmode_hex; }
            DeviceCom.SetProgress(boxmode_hex, boxmode_desc, value, errorcode);
        }
        #endregion

        #region ProcOrders
        /**********************************************************
        * FUNCTION:     ProcOrders
        * DESCRIPTION:  
        ***********************************************************/
        public ProcessWorker ProcessWorker { get; set; }

        public void Start()
        {
            if (ProcessWorker == null || ProcessWorker.ProcessOrders == null)
            {
                ProcessWorker = new ProcessWorker(Processes.ProcOrders.Values.ToList());
            }
            ProcessWorker.ProcessChanged -= ProcessWorker_ProcChanged;
            ProcessWorker.ProcessChanged += ProcessWorker_ProcChanged;
            ProcessWorker.ProcStateChanged += ProcessWorker_ProcChanged;
            ProcessWorker.Start(ItemValues.CalMode, DeviceCom);
        }

        public void Stop()
        {
            //if (Running)
            //{
            //    try
            //    {
            //        WaitingDetails = new WaitDetails() { Answer_Received = false, OpCodeAnswerWaiting = CaliboxLibrary.OpCode.s999 };
            //        CMD_Send(CaliboxLibrary.OpCode.S999);
            //        if (DGV_Progress.Rows.Count > 0)
            //        {
            //            DGV_Progress.ClearSelection();
            //        }

            //        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //        watch.Start();
            //        while (!WaitingDetails.Answer_Received && watch.ElapsedMilliseconds < 5000)
            //        {
            //            Thread.Sleep(200);
            //            //Application.DoEvents();
            //        }
            //        //DeviceCom.SendCMD(Instance, opcode.S999, true);
            //        Thread.Sleep(1000);
            //        //Application.DoEvents();

            //        Logger.Save(OpCode.state, "****** END PROCESS *** END PROCESS *** END PROCESS *** END PROCESS *** END PROCESS ******");
            //        //port.DiscardOutBuffer();
            //        //Application.DoEvents();
            //        Logger.ForceLog();
            //    }
            //    catch (Exception e)
            //    {
            //        //ErrorHandler("Stop", exception: e);
            //        Logger.Save(e);
            //        Gui_Message(message: e.Message);
            //    }
            //    Channel_Running_Change(false);
            //}
            //else
            //{
            //    m_state = gProcMain.stop_stateEngine;
            //}
            //timProcess.Stop();
            //timCalibration.Stop();
            //ucCOM.Stop();
            //H_TT.SetFocus_Input();
        }

        #endregion

        private void ProcessWorker_ProcChanged(object sender, Processes e)
        {
            ProcCommands(e.ProcName);
        }
        private readonly object _BalanceProcCMD = new object();
        private void ProcCommands(gProcMain procMain)
        {
            _Logger.Save($"ProcCommands: {ProcessWorker.Current.ProcName}, {ProcessWorker.Current.ProcState}, {ProcessWorker.Current.ProcStateMachine}", OpCode.state);
            lock (_BalanceProcCMD)
            {
                switch (procMain)
                {
                    case gProcMain.getReadyForTest:
                        Cmd_GetReadyForTest();
                        break;
                    case gProcMain.StartProcess:
                        Cmd_StartProcess();
                        break;
                    case gProcMain.BoxIdentification:
                        Cmd_GetBoxId();
                        break;
                    case gProcMain.BoxStatus:
                        Cmd_BoxStatus();
                        break;
                    case gProcMain.BoxReset:
                        Cmd_BoxReset();
                        break;
                    case gProcMain.TestFinished:
                        Cmd_TestFinished();
                        break;
                    case gProcMain.FWcheck:
                        Cmd_FWcheck();
                        break;
                    case gProcMain.SensorCheck:
                        Cmd_SensorCheck();
                        break;
                    case gProcMain.Calibration:
                        Cmd_CalibrationStart();
                        break;
                    case gProcMain.wait:
                        Cmd_Wait();
                        break;
                    case gProcMain.Bewertung:
                        Cmd_Bewertung();
                        break;
                    case gProcMain.DBinit:
                        Cmd_DBinit();
                        break;
                    case gProcMain.error:
                        Error(DeviceCom.ResponseLast);
                        //Error(null);
                        break;
                    case gProcMain.idle:
                        m_timeOutActive = false;
                        break;
                    case gProcMain.stop_stateEngine:
                        break;
                    default:
                        m_timeOutActive = false;
                        break;
                }
            }
        }

        #region SingleCommands
        /**********************************************************
        * FUNCTION:     SingleCommands
        * DESCRIPTION:  
        ***********************************************************/
        private void Cmd_GetReadyForTest()
        {
            if (ProcessWorker.ProcState != ProcState.Running)
            {
                return;
            }
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state);
            ProcessWorker.Executed();
        }

        private void Cmd_StartProcess()
        {
            if (ProcessWorker.ProcState != ProcState.Running)
            {
                return;
            }
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state);
            Gui_Message(ProcessWorker.Current.ProcName);
            Set_Progress(ItemValues.CalMode.ToString(), ItemValues.Cal_Desc, value: ItemValues.Cal_Desc, errorcode: "0");
            ProcessWorker.Executed();
        }

        private void Cmd_GetBoxId()
        {
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state);
            Gui_Message(ProcessWorker.Current.ProcName);

            bool result = WaitingDetails.Answer_Received;
            var boxMode = result ? (DeviceCom.ItemValues.BoxId.CS_ok ? BoxErrorMode.Pass : BoxErrorMode.Error) : BoxErrorMode.NotChecked;
            Set_Progress(BoxModeCodes.BoxIDStatus, boxMode, value: DeviceCom.ItemValues.BoxId.DeviceLimits.FW_Version);

            if (DeviceCom.ItemValues.BoxId.CS_ok)
            {
                ProcessWorker.Terminate();
                return;
            }
            if (ProcessWorker.ProcState == ProcState.Waiting)
            {
                if (!ProcessWorker.AttentsExceeded())
                {
                    DeviceCom.GetBoxID();
                    //Wait(2000);
                    ProcessWorker.AttentsIncreese();
                }
                else
                {
                    stateMessageError = "Box Kommunikation oder Status";
                    _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, stateMessageError);
                    ProcCommands(gProcMain.error);
                }
            }
        }

        private void Cmd_BoxStatus()
        {
            if (ProcessWorker.Attents == 0)
            {
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state);
                Gui_Message(ProcessWorker.Current.ProcName);
            }
            bool result = ProcessWorker.Current.AnswerReceived;
            if (ProcessWorker.ProcState == ProcState.Waiting && !result)
            {
                return;
            }
            var boxMode = result ? BoxErrorMode.Pass : BoxErrorMode.NoBoxCom;
            Set_Progress(BoxModeCodes.BoxStatus, boxMode);
            if (ProcessWorker.Current.AnswerReceived)
            {
                ProcessWorker.Executed();
                return;
            }

            if (!ProcessWorker.AttentsExceeded())
            {
                ProcessWorker.AttentsIncreese();
                CMD_Send(OpCode.G100);
                //Wait(2000);
            }
            else
            {
                stateMessageError = "Keine Box Kommunikation";
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, stateMessageError);
                Set_Progress(BoxModeCodes.BoxStatus, BoxErrorMode.NoBoxCom);
                ProcCommands(gProcMain.error);
            }
        }

        private void Cmd_BoxReset()
        {
            switch (ProcessWorker.ProcState)
            {
                case ProcState.Idle:
                    return;
                case ProcState.Waiting:
                    break;
            }
            if (ProcessWorker.Attents == 0)
            {
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state);
                Gui_Message(ProcessWorker.Current.ProcName);
            }
            bool result = ProcessWorker.Current.AnswerReceived;
            m_BoxReseted = result;
            if (ProcessWorker.Current.ProcState == ProcState.Waiting && !result && ProcessWorker.Attents > 0)
            {
                return;
            }
            if (ProcessWorker.Current.AnswerReceived)
            {
                ProcessWorker.Executed();
                return;
            }

            if (!ProcessWorker.AttentsExceeded())
            {
                if (ProcessWorker.Current.WaitExeeded())
                {
                    ProcessWorker.AttentsIncreese();
                    CMD_Send(OpCode.S999);
                }
            }
            else
            {
                stateMessageError = "BoxReset";
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, stateMessageError);
                ProcCommands(gProcMain.error);
            }
        }
        private void Cmd_TestFinished()
        {
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state);
            Gui_Message(ProcessWorker.Current.ProcName);
            Stop();
            ProcessWorker.MoveNext();
        }

        private void Cmd_FWcheck()
        {
            if (ProcessWorker.Attents == 0)
            {
                WaitingDetails.Answer_Received = false;
                WaitingDetails.OpCodeRequest = OpCode.g015;
            }
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, $"active = {ItemValues.sample_FW_Version_Cal_active}");
            Gui_Message(ProcessWorker.Current.ProcName);
            bool result = WaitingDetails.Answer_Received;
            string fwmessage = $"Sensor: {ItemValues.sample_FW_Version_value} DB: {ItemValues.sample_FW_Version} ";
            var mode = ItemValues.sample_FW_Version_Cal_active ? (ItemValues.sample_FW_Version_ok ? BoxErrorMode.Pass : BoxErrorMode.Error) : BoxErrorMode.NotChecked;
            Set_Progress(BoxModeCodes.FWversion, mode, ItemValues.sample_FW_Version_value);
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, fwmessage);
            if ((ItemValues.sample_FW_Version_Cal_active && ItemValues.sample_FW_Version_ok) || (!ItemValues.sample_FW_Version_Cal_active && result))
            {
                ProcessWorker.MoveNext();
                return;
            }

            if (!ProcessWorker.AttentsExceeded())
            {
                CMD_Send(OpCode.G015);
                Wait(2000, false);
                ProcessWorker.AttentsIncreese();
            }
            else
            {
                stateMessageError = fwmessage;
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, stateMessageError);
                Set_Progress(BoxModeCodes.FWversion, BoxErrorMode.Error, ItemValues.sample_FW_Version_value);
                ProcCommands(gProcMain.error);
            }
        }

        private void Cmd_SensorCheck()
        {
            if (ProcessWorker.Attents == 0)
            {
                WaitingDetails.Answer_Received = false;
                WaitingDetails.OpCodeRequest = OpCode.g015;
            }
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, $"active = {ItemValues.sample_FW_Version_Cal_active}");
            Gui_Message(ProcessWorker.Current.ProcName);
            bool result = WaitingDetails.Answer_Received;
            string fwmessage = $"Sensor: {ItemValues.sample_FW_Version_value} DB: {ItemValues.sample_FW_Version} ";
            var mode = result ? BoxErrorMode.Pass : BoxErrorMode.NotChecked;
            Set_Progress(BoxModeCodes.FWversion, mode, ItemValues.sample_FW_Version_value);
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, fwmessage);
            if (result)
            {
                ProcessWorker.MoveNext();
                return;
            }

            if (!ProcessWorker.AttentsExceeded())
            {
                CMD_Send(OpCode.G015);
                Wait(2000, false);
                ProcessWorker.AttentsIncreese();
            }
            else
            {
                stateMessageError = "NO SENSOR";
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, stateMessageError);
                Set_Progress(BoxModeCodes.SensorStatus, BoxErrorMode.Error, stateMessageError);
                ProcCommands(gProcMain.error);
            }
        }

        private void Cmd_CalibrationStart()
        {
            if (ProcessWorker.Attents == 0)
            {
                WaitingDetails.Answer_Received = false;
                WaitingDetails.OpCodeRequest = OpCode.g015;
            }
            stateMessageError = $"Calib Modus: {ItemValues.CalMode}/{ItemValues.Cal_Desc}";
            WaitingDetails.Message = stateMessageError;
            _Logger.Save(ProcessWorker.Current.ProcName.ToString(), OpCode.state, stateMessageError);
            Gui_Message(ProcessWorker.Current.ProcName);
            CMD_Send(ItemValues.CalMode);
            Set_Progress(ItemValues.CalMode.ToString(), ItemValues.Cal_Desc, value: ItemValues.Cal_Desc, errorcode: "00");
            ProcessWorker.Terminate();
        }

        private void Cmd_Bewertung()
        {
            ItemValues.meas_time_end = DateTime.Now;
            ItemValues.test_ok = calQuality == CH_State.QualityGood;
            string state = ItemValues.test_ok ? "2" : "1";
            Set_Progress(ItemValues.CalMode.ToString(), ItemValues.Cal_Desc, value: ItemValues.Cal_Desc, errorcode: state);
            string quality = ItemValues.test_ok ? "Gut" : "Schlecht";
            string message = $"Bewertung: {quality}\tduration: {ItemValues.meas_time_duration.ToStringMin()}";
            if (!ItemValues.ErrorDetected) { Gui_Message(message: message); }
            _Logger.Save(gProcMain.Bewertung.ToString(), OpCode.state, message);
            try
            {
                if (!ItemValues.Save_DBmeasValues(DeviceCom.DT_Progress))
                {
                    stateMessageError = "ERROR: DataBank speichern Bewertung";
                    _Logger.Save(gProcMain.Bewertung.ToString(), OpCode.state, stateMessageError);
                    m_state = gProcMain.error;
                    return;
                }
            }
            catch (Exception ex)
            {
                _Logger.Save(ex);
            }
            m_state = gProcMain.TestFinished;
        }
        private void Cmd_DBinit()
        {
            //if (m_timeOutActive) m_timeOutActive = false;
            m_state_ProcessRunning = m_state;
            m_TestRunning = true;
            string message = $"sensorID: {ItemValues.sensor_id}\tTAG-Nr: {ItemValues.tag_nr}\tPassNo: {ItemValues.pass_no}";
            //if (!Save_Values_To_DB_INIT())
            try
            {
                if (!ItemValues.Save_DBprocInit())
                {
                    stateMessageError = "ERROR: DataBank speichern INIT";
                    message += $" {stateMessageError}";
                    m_state = gProcMain.error;
                }
                else
                {
                    m_DBinit = true;
                    m_state = gProcMain.idle;
                }
            }
            catch { }
            _Logger.Save(m_state_ProcessRunning.ToString(), OpCode.state, message);
            ProcessWorker.MoveNext();
        }

        private void Cmd_Wait()
        {
            if (!WaitingDetails.WaitRunning)
            {
                WaitingDetails.WaitRunning = true;
                m_timeOutDevice = DateTime.Now.AddMilliseconds(WaitingDetails.Wait_ms);
                var message = $"wait: {WaitingDetails.Wait_ms / 1000} sec\t{WaitingDetails.Message}";
                m_timeOutActive = true;
                timerCalibration.Stop();
                timProcess.Stop();
                _Logger.Save(m_state.ToString(), message);
            }
            TimeSpan timeDiffDevice = (m_timeOutDevice - DateTime.Now);
            Waittime_Show(timeDiffDevice, WaitingDetails.Message);
            if (timeDiffDevice.TotalSeconds > 0)
            {
                if (WaitingDetails.Answer_Wait)
                {
                    if (!WaitingDetails.Answer_Received)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            WaitingDetails.WaitRunning = false;
            m_timeOutActive = WaitingDetails.TimeOutActive;
            if (WaitingDetails.ProcNext != gProcMain.error)
            {
                if (WaitingDetails.ProcessRunning) { timProcess.Start(); }
                if (WaitingDetails.CalibrationRunning) { timerCalibration.Start(); }
            }
            m_state = WaitingDetails.ProcNext;
        }
        #endregion

        #region CmdSend
        /************************************************
         * FUNCTION:    Command Send
         * DESCRIPTION:
         ************************************************/
        private void CMD_Send(OpCode opcode, string add)
        {
            try
            {
                DeviceCom.Send(opcode, add);
                _Logger.Save(ProcessWorker.Current.ProcName.ToString(), $"CMD-SEND: {DeviceCom.CMD_Sended}");
            }
            catch (Exception ex)
            {
                //Tb_Info.Text += e.Message + Environment.NewLine;
            }
        }
        private void CMD_Send(OpCode cmd)
        {
            CMD_Send(cmd, null);
        }
        #endregion

        #region StateEngine
        #region StateEngineProperties
        /**********************************************************
        * FUNCTION:     StateEngineProperties
        * DESCRIPTION:  
        ***********************************************************/
        public bool m_DBinit { get; set; }
        public bool m_timeOutActive { get; set; }
        //public bool m_Calibration_Running { get; set; }
        public bool m_TestRunning { get; set; }

        public bool m_BoxReseted { get; set; }

        public DateTime m_timeOutDevice { get; set; }

        //public int m_processCounter { get; set; }
        //public int m_processCounterTotal { get; set; }
        public gProcMain m_state { get; set; }
        private gProcMain m_state_ProcessRunning { get; set; } = gProcMain.idle;
        #endregion

        #endregion StateEngine

        /***************************************************************************************
        * FUNCTION:     State Engine
        *               Single Functions
        ****************************************************************************************/
        #region StateEngineFunctions

        private void Error(DeviceResponseValues drv)
        {
            //if (m_timeOutActive) m_timeOutActive = false;
            //timCalibration.Stop();
            //m_timeOutActive = false;
            //Limits.ErrorDetected = true;
            //string message = null;
            //if (!string.IsNullOrEmpty(stateMessageError))
            //{
            //    message = $"ERROR: {stateMessageError}\t{m_state_ProcessRunning}";
            //    Gui_Message(message);
            //}
            //if (!string.IsNullOrEmpty(Limits.ErrorMessage))
            //{
            //    message = $"ERROR: {Limits.ErrorMessage}\t{m_state_ProcessRunning}";
            //    Gui_Message(message);
            //}
            //if (drv != null)
            //{
            //    if (!string.IsNullOrEmpty(drv.BoxErrorCode_hex))
            //    {
            //        if (drv.BoxErrorCode_hex != "0")
            //        {
            //            message = $"ERROR: {drv.BoxErrorCode_hex} - {drv.BoxErrorCode_desc}\t{m_state_ProcessRunning}";
            //            Gui_Message(message);
            //        }
            //    }
            //}
            //if (message == null)
            //{
            //    message = $"ERROR: ";
            //    Gui_Message(message);
            //}

            //Logger.Save(m_state.ToString(), OpCode.Error, message);
            //if (m_TestRunning)
            //{
            //    m_state = gProcMain.Bewertung;
            //    return;
            //}
            //DGV_Progress.ClearSelection();
            //m_timeOutActive = false;
            //m_state = gProcMain.idle;
            //Stop();
        }
        #endregion StateEngineFunctions

        //public bool calibrationRunning { get; set; } = false;

        #region Wait
        /**********************************************************
        * FUNCTION:     Wait
        * DESCRIPTION:  
        ***********************************************************/
        private struct WaitDetails
        {
            public bool TimeOutActive;
            public bool WaitRunning;
            public bool ProcessRunning;
            public bool CalibrationRunning;
            public gProcMain ProcNext;
            public string Message;
            public int Wait_ms;

            public OpCode OpCodeAnswerWaiting;
            public OpCode OpCodeAnswerReceive;
            public OpCode OpCodeRequest;
            public bool Answer_Received;
            public bool Answer_Wait;

            public void DataReceived(DeviceResponseValues data)
            {
                OpCodeAnswerReceive = data.OpCode;
                Message = data.Response;
                Answer_Received = OpCodeAnswerReceive.ToString().ToLower() == OpCodeRequest.ToString().ToLower();
            }
        }

        private WaitDetails WaitingDetails = new WaitDetails();
        #endregion

        #region Wait Message
        /**********************************************************
        * FUNCTION:     Wait Message
        * DESCRIPTION:  
        ***********************************************************/
        private void Wait(int wait, gProcMain procNext, bool waitAnswer = true)
        {
            WaitingDetails.TimeOutActive = m_timeOutActive;
            WaitingDetails.WaitRunning = false;
            WaitingDetails.ProcessRunning = timProcess.Enabled;
            WaitingDetails.CalibrationRunning = timerCalibration.Enabled;
            WaitingDetails.ProcNext = procNext;
            WaitingDetails.Wait_ms = wait;
            WaitingDetails.Answer_Wait = waitAnswer;
            m_state = gProcMain.wait;
        }
        private void Wait(int wait, bool waitAnswer = true)
        {
            Wait(wait, m_state, waitAnswer);
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
            Waittime_Show(ItemValues.meas_time_remain.TotalSeconds, m_state, message);
        }
        #endregion

        /* CALIBRATION *************************************************************************
        'FUNCTION:    Process Calibration
                      process steps
        ****************************************************************************************/
        #region Calibration
        private System.Windows.Forms.Timer timProcess;
        private void Init_timProcess()
        {
            timProcess = new System.Windows.Forms.Timer() { Interval = 1000 };
            timProcess.Tick += new EventHandler(timProcesse_Tick);
        }

        public gProcMain ProcesseLast { get; set; } = gProcMain.wait;
        public gProcMain ProcesseRunning { get; set; } = gProcMain.idle;
        public int processeNr { get; set; } = 0;
        private int _processCount = 0;
        public int processeCount
        {
            get
            {
                if (_processCount == 0)
                {
                    _processCount = Processes.ProcOrder.Count;
                }
                return _processCount;
            }
        }
        public bool ProcesseLastArrived { get { return processeNr == processeCount; } }
        public void timProcesse_Tick(object sender, EventArgs e)
        {
            if (m_state == gProcMain.error || ItemValues.ErrorDetected)
            {
                timerCalibration.Stop();
                timProcess.Stop();
                return;
            }
            else
            if (ProcesseRunning != ProcesseLast && (m_state == gProcMain.idle || m_state == gProcMain.StartProcess))
            {
                bool nextProcess = false;
                switch (m_state)
                {
                    case gProcMain.StartProcess:
                        processeNr = 0;
                        ProcesseRunning = Processes.ProcOrder[0];
                        nextProcess = true;
                        break;
                    default:
                        nextProcess = true;
                        break;
                }
                if (nextProcess && !ItemValues.ErrorDetected)
                {
                    if (!ProcesseLastArrived) { processeNr++; }
                    ProcesseLast = ProcesseRunning;
                    ProcesseRunning = Processes.ProcOrder[processeNr];
                    m_state = ProcesseRunning;
                }
                if (ProcesseLastArrived)
                {
                    timerCalibration.Stop();
                    timProcess.Stop();
                }
            }
        }

        /*******************************************************************************************************************
        * FUNCTION:    Process Calibration Variables
        ********************************************************************************************************************/
        private System.Windows.Forms.Timer timerCalibration;
        private void Init_TimerCalibration(bool start = false, int interval = 250)
        {
            if (timerCalibration != null)
            {
                timerCalibration.Tick -= timCalibration_Tick;
            }
            timerCalibration = new System.Windows.Forms.Timer()
            {
                Interval = interval
            };
            timerCalibration.Tick += timCalibration_Tick;
            if (start)
            {
                timerCalibration.Start();
            }
        }

        /*******************************************************************************************************************
        *FUNCTION:    State Engine Calibration
        ********************************************************************************************************************/
        int CalibNoAnswerCounter { get; set; } = 0;
        bool g901Receved { get; set; } = false;
        public void timCalibration_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Answer())
                {
                    CalibNoAnswerCounter = 0;
                    WaitingDetails.OpCodeAnswerReceive = OpCode.state;
                }
                else
                {
                    CalibNoAnswerCounter++;
                }
            }
            catch { }
            try
            {
                CheckMeasurement(DeviceCom.ResponseLast);
            }
            catch { }
            bool NoError = false;
            try { NoError = CheckFinalize(DeviceCom.ResponseLast); } catch { }


        }
        private void CheckMeasurement(DeviceResponseValues drv)
        {
            //if (m_state == gProcMain.Calibration)
            //{
            //    if (CountCalib == 0)
            //    {
            //        timCalibration.Interval = 1000;
            //        CalibNoAnswerCounter = 0;
            //        calibrationRunning = true;
            //        g901Receved = false;
            //        CMD_Send(OpCode.G901);
            //        CountCalib++;
            //        return;
            //    }
            //    CountCalib++;
            //    if (!g901Receved)
            //    {
            //        if (CalibNoAnswerCounter > 30)
            //        {
            //            CMD_Send(OpCode.G901);
            //            CalibNoAnswerCounter = 0;
            //        }
            //    }
            //    if (drv.BoxMode_desc != CalibLastMode)
            //    {
            //        Limits.Cal_BoxMode_desc = drv.BoxMode_desc;
            //        Limits.Cal_Stopwatch = new System.Diagnostics.Stopwatch();
            //        Limits.Cal_Stopwatch.Start();
            //        CalibLastMode = drv.BoxMode_desc;
            //        CountCalib = 1;
            //    }
            //    Set_ChartVisible();
            //}
        }
        private bool CheckFinalize(DeviceResponseValues drv)
        {
            //if (DeviceCom.IsFinalized || DeviceCom.IsError || drv.OpCode == OpCode.s999 || CalibNoAnswerCounter > 200)
            //{
            //    timCalibration.Stop();
            //    ChB_Chart.Checked = false;
            //    m_Calibration_Running = false;
            //    if (drv.OpCode == OpCode.s999)
            //    {
            //        stateMessageError = "Sensor Kommunikation abgebrochen";
            //        Limits.ErrorDetected = true;
            //        Limits.ErrorMessage = stateMessageError;
            //        drv.BoxErrorMode = BoxErrorMode.NoSensCom;
            //        DeviceCom.OnErrorReceived(drv);
            //    }
            //    if (!DeviceCom.IsError)
            //    {
            //        Limits.test_ok = true;
            //        calQuality = CH_State.QualityGood;
            //        Logger.Save(m_state.ToString(), OpCode.state, $"SUBPROCESS QUALITY GOOD {stateMessageError}");
            //        m_state = gProcMain.idle;
            //    }
            //    else
            //    {
            //        Limits.ErrorDetected = true;
            //        Limits.ErrorMessage = stateMessageError;
            //        calQuality = CH_State.QualityBad;
            //        Limits.test_ok = false;
            //        Logger.Save(m_state.ToString(), OpCode.state, $"SUBPROCESS ERROR {stateMessageError}");
            //        CMD_Send(OpCode.G200);
            //        Wait(4000, gProcMain.error);
            //    }
            //    if (!timProcess.Enabled) { timProcess.Start(); }
            //    return false;
            //}
            //else
            //{
            //    if (CalibNoAnswerCounter > 2)
            //    {
            //        noAnswer = AnswerReplaces(drv.BoxMode_desc, CountCalib, CalibNoAnswerCounter);
            //    }
            //    else
            //    {
            //        noAnswer = null;
            //    }
            //    Waittime_Show_Cal(noAnswer);
            //}
            return true;
        }
        private string AnswerReplaces(string txt, int count, int countNoAnswer)
        {
            StringBuilder sb = new StringBuilder(txt)
                .Replace("_", " ").Replace("Mode", "").Replace("Box", "")
                .Append($"\tCount: {count}");
            if (CalibNoAnswerCounter > 2)
            {
                sb.Append($"; NoAnswer: {CalibNoAnswerCounter}");
            }
            return sb.ToString();
        }
        private bool Answer()
        {
            if (Answer(OpCode.g901))
            {
                g901Receved = true;
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
            if (cmd == WaitingDetails.OpCodeAnswerReceive) { return true; }
            return false;
        }
        #endregion
    }
}