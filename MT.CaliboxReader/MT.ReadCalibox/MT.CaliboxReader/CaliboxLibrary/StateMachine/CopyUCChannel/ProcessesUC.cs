using CaliboxLibrary.BoxCommunication.CMDs;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace CaliboxLibrary.StateMachine.CopyUCChannel
{
    public class ProcessesUC
    {
        public DeviceCom Device { get; set; } = new DeviceCom(-1, isDebug: false);
        public Logger Logger
        {
            get { return Device.Logger; }
            set { Device.Logger = value; }
        }

        public WaitDetails WaitingDetails { get; }
        public int ChannelNo { get; private set; }

        /************************************************
         * FUNCTION:    Constructor(s)
         * DESCRIPTION:
         ************************************************/
        public ProcessesUC()
        {
            WaitingDetails = new WaitDetails(this);
        }

        public ProcessesUC(SerialPort port, int channelNo) : this()
        {
            ChannelNo = channelNo;
            Device = new DeviceCom(port, channelNo, isDebug: false);
            Device.DataReceived += Device_DataReceived;
        }

        public ProcessesUC(int channelNo) : this()
        {
            ChangeChannelNumber(channelNo);
        }

        public void ChangeChannelNumber(int channelNo)
        {
            ChannelNo = channelNo;
            Device.ChannelNo = channelNo;
            Device.DataReceived -= Device_DataReceived;
            Device.DataReceived += Device_DataReceived;
        }

        /************************************************
         * FUNCTION:    Device Data Received
         * DESCRIPTION:
         ************************************************/
        public event EventHandler<EventDataArgs> DataReceived;
        protected virtual void OnDataReceived(EventDataArgs args)
        {
            try
            {
                DataReceived?.Invoke(this, args);
            }
            catch { }
        }

        private void Device_DataReceived(object sender, EventDataArgs e)
        {
            var flag = CheckAnswer(e);
            WaitingDetails.DataReceived(e.DeviceResponseValue, flag);
            OnDataReceived(e);
        }

        private bool CheckAnswer(EventDataArgs e)
        {
            bool flag = false;
            if (CmdSend.CmdText == e.CMDsendText)
            {
                if (e.IsData == false)
                {
                    return false;
                }
                var oposite = CmdSend.OpCode.GetOpposite();
                flag = (oposite == e.OpCodeReceived);
                if (flag)
                {
                    if (e.OpCodeReceived == OpCode.S999)
                    {
                        if (e.Data.IndexOf("Box-Okay", StringComparison.CurrentCultureIgnoreCase) > 0)
                        {
                            flag = true;
                            IsBoxReseted = true;
                        }
                    }
                    ProcessCounterValues.AnswerReceived(flag);
                    CmdReceived = true;
                }
            }
            return CmdReceived;
        }

        /************************************************
        * FUNCTION:    Process Order(s)
        * DESCRIPTION:
        ************************************************/
        public gProcMain[] ProcessOrder { get; set; }
        = new gProcMain[]
        {
            gProcMain.idle,
            gProcMain.getReadyForTest,
            gProcMain.StartProcess,
            gProcMain.BoxReset,
            gProcMain.BoxStatus,
            gProcMain.FWcheck,
            gProcMain.SensorPage00Read,
            gProcMain.SensorPage00Write,
            gProcMain.SensorPage01Read,
            gProcMain.SensorPage01Write,
            gProcMain.SensorPage02Read,
            gProcMain.SensorPage02Write,
            gProcMain.BoxIdentification,
            gProcMain.BoxReset,
            gProcMain.FWcheck,
            gProcMain.DBinit,
            gProcMain.Calibration,
            gProcMain.Bewertung,
            gProcMain.TestFinished,
        };
        public int ProcessIndex { get; set; }

        /************************************************
         * FUNCTION:    Processes
         * DESCRIPTION:
         ************************************************/
        public ProcessCounter ProcessCounterValues { get; set; } = new ProcessCounter();
        public ProcessCounterDetails ProcessSelected { get; private set; }
            = new ProcessCounterDetails(gProcMain.idle)
            {
                AnswerReceived = true
            };
        public ProcessCounterDetails ProcessNext { get; private set; }
            = new ProcessCounterDetails(gProcMain.getReadyForTest);

        /************************************************
         * FUNCTION:    Command Send
         * DESCRIPTION:
         ************************************************/
        public CmdSend CmdSend { get; private set; }
        public bool CmdReceived { get; private set; }
        public gProcMain CmdProcess { get; private set; }
        public bool ErrorDetected
        {
            get { return Device.ItemValues.ErrorDetected; }
            set { Device.ItemValues.ErrorDetected = value; }
        }
        public bool CMD_Send(gProcMain current, CmdDefinition cmd, string data = null)
        {
            try
            {
                CmdReceived = false;
                cmd.Data = data;
                CmdSend = new CmdSend(cmd);
                CmdProcess = current;
                int wait = cmd.WaitMilliseconds;
                if (wait < 1000) { wait = 1000; }
                WaitingDetails.Wait_ms = wait;
                WaitingDetails.AnswerGoNext = cmd.NextOnAnswer;
                ProcessIncreese();
                Device.Send(CmdSend);
                return true;
            }
            catch
            {
            }
            return false;
        }

        public bool CMD_Send(gProcMain current, OpCode opCode, string add, int wait = -1, bool goNextOnAnswer = true)
        {
            try
            {
                CmdReceived = false;
                CmdSend = new CmdSend(opCode, add);
                CmdProcess = current;
                if (wait < 1000) { wait = 1000; }
                WaitingDetails.Wait_ms = wait;
                Device.Send(CmdSend);
                ProcessIncreese();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public bool CMD_Send(gProcMain current, OpCode cmd, int wait = -1, bool goNextOnAnswer = true)
        {
            return CMD_Send(current, cmd, null, wait, goNextOnAnswer);
        }

        /************************************************
         * FUNCTION:    States Indicators
         * DESCRIPTION:
         ************************************************/

        public bool IsTestRunning { get; set; } = false;
        public bool IsBoxReseted { get; set; } = false;

        public void ResetAllData()
        {
            ResetAllData(gProcMain.idle);
        }

        public void ResetAllData(gProcMain nextProcess)
        {
            Logger.DeleteData();

            ProcessCounterValues.Reset();
            ProcessIndex = 0;
            ProcessCounterValues.GetOrAdd(ProcessOrder[ProcessIndex], out var next);
            ProcessSelected = next;
            ProcessSelected.Stop(ProcState.Finished);
            ProcessCounterValues.GetOrAdd(ProcessOrder[ProcessIndex + 1], out next);
            ProcessNext = next;

            Device.ClearDeviceResponses();
            Device.ItemValues.Reset();

            WaitingDetails.Reset();
            WaitingDetails.ProcessAfterWait = nextProcess;
            WaitingDetails.Wait_ms = 1000;
            WaitingDetails.CalibrationRunning = false;
        }

        public void ResetStatesIndicators()
        {
            IsTestRunning = false;
            IsBoxReseted = false;
            ProcessCounterValues.Reset();
            State_ProcessRunning = gProcMain.idle;
        }

        public void StartProcess(gProcMain gProc, OpCode opCode)
        {
            if (ProcessSelected.ProcState != ProcState.Finished && gProc != gProcMain.error)
            {
                _State = ProcessSelected.Process;
                return;
            }
            CmdReceived = false;
            State_Last = gProc;
            WaitingDetails.Reset(gProc, opCode);

            ProcessSelected = ProcessCounterValues.Start(gProc);
            if (ProcessIndex == 0 || gProc != gProcMain.idle)
            {
                ProcessIndex = GetIndex(gProc, out var nextProcess);
                WaitingDetails.ProcessAfterWait = nextProcess;
                ProcessCounterValues.GetOrAdd(nextProcess, 3, out var result);
                ProcessNext = result;
                result.Reset();
            }
            else
            {

            }
            Logger.Save(ProcessSelected.Process.ToString(), ProcessSelected.GetAttemptMessage());
        }

        public void StopProcess(gProcMain gProc, ProcState procState, out gProcMain nextProcess)
        {
            if (ProcessSelected.Process != gProc)
            {
                ProcessCounterValues.GetOrAdd(gProc, 3, out var result);
                result.Stop(procState);
            }
            else
            {
                ProcessSelected.Stop(procState);
            }
            if (procState == ProcState.Aborted)
            {
                nextProcess = gProcMain.error;
                return;
            }
            GetIndex(gProc, out nextProcess);
        }
        public void StopAndNext(gProcMain running, ProcState processState, out gProcMain nextProcess)
        {
            StopProcess(running, processState, out nextProcess);
            NextProcess(nextProcess);
        }

        public int GetIndex(gProcMain gProc, out gProcMain nextProcess)
        {
            nextProcess = gProcMain.idle;
            int index = 0;
            for (int i = 0; i < ProcessOrder.Length; i++)
            {
                if (i < ProcessIndex)
                {
                    continue;
                }
                if (gProc == ProcessOrder[i])
                {
                    index = i;
                    if (ProcessOrder.Length - 1 > i)
                    {
                        nextProcess = ProcessOrder[i + 1];
                    }
                    return index;
                }
            }
            return index;
        }

        public void ProcessIncreese()
        {
            ProcessSelected.Increese();
            WaitingDetails.Increese();
            Logger.Save(ProcessSelected.Process.ToString(), $"{ProcessSelected.GetAttemptMessage()} [{WaitingDetails.GetTimeRemain()}]");
        }

        /************************************************
         * FUNCTION:    States
         * DESCRIPTION:
         ************************************************/
        public EventHandler<gProcMain> StateChanged;
        protected virtual Task OnStateChanged(gProcMain state)
        {
            return Task.Run(() =>
            {
                try
                {
                    StateChanged?.Invoke(this, state);
                }
                catch { }
            });
        }

        public bool IsStopped { get; set; }
        public bool IsRunning { get; set; }

        public gProcMain State_ProcessRunning { get; set; } = gProcMain.idle;
        public gProcMain State_Last { get; set; }
        private gProcMain _State;
        public gProcMain State
        {
            get { return _State; }
            set
            {
                if (!TimerStateEngine.Enabled)
                {
                    TimerStateEngine.Interval = 1000;
                    TimerStateEngine.Start();
                }
                if (value == gProcMain.wait)
                {
                    return;
                }
                _State = value;
                OnStateChanged(value);
            }
        }

        /// <summary>
        /// Set <see cref="gProcMain.idle"/> to go to next process,
        /// or set specific process,
        /// <see cref="Processes"/> will change processes
        /// </summary>
        /// <param name="next"></param>
        public void NextProcess(gProcMain next)
        {
            State = next;
        }

        /************************************************
         * FUNCTION:    States
         * DESCRIPTION:
         ************************************************/
        public System.Windows.Forms.Timer TimerStateEngine { get; set; }

        public void Init_timStateEngine()
        {
            if (TimerStateEngine != null)
            {
                TimerStateEngine.Tick -= timStateEngine_Tick;
            }
            TimerStateEngine = new System.Windows.Forms.Timer()
            {
                Interval = 1000
            };
            TimerStateEngine.Tick += timStateEngine_Tick;
        }

        public void timStateEngine_Tick(object sender, EventArgs e)
        {
            if (TimerStateEngine.Interval != 1000)
            {
                TimerStateEngine.Interval = 1000;
            }
            WaitExecutionAsync();
        }

        /************************************************
         * FUNCTION:    Main Functions
         * DESCRIPTION:
         ************************************************/
        public EventHandler<EventMessageArgs> UImessage;
        protected virtual void OnUImessage(EventMessageArgs args)
        {
            try
            {
                UImessage?.Invoke(this, args);
            }
            catch { }
        }

        public Task WaitExecutionAsync()
        {
            return Task.Run(() =>
            {
                if (State == gProcMain.idle)
                {
                    return;
                }
                if (WaitingDetails.CalibrationRunning)
                {
                    var args = new EventMessageArgs(WaitingDetails.ProcessCurrent, Device.ItemValues.meas_time_remain, message: WaitingDetails.Message);
                    OnUImessage(args);
                    return;
                }
                //if (WaitingDetails.CalibrationRunning)
                //{
                //    return;
                //}

                switch (ProcessSelected.ProcState)
                {
                    case ProcState.Aborted:
                        NextProcess(gProcMain.error);
                        return;
                    case ProcState.Finished:
                        NextProcess(WaitingDetails.ProcessCurrent);
                        //NextProcess(WaitingDetails.ProcessAfterWait);
                        return;
                    case ProcState.Idle:
                        NextProcess(WaitingDetails.ProcessCurrent);
                        return;
                    default:
                        break;
                }

                if (WaitingDetails.TimeExpired() == false)
                {
                    var args = new EventMessageArgs(WaitingDetails.ProcessCurrent, WaitingDetails.GetTimeRemain(), message: WaitingDetails.Message);
                    OnUImessage(args);
                    NextProcess(ProcessSelected.Process);
                    return;
                }

                if (IsStopped || ProcessSelected.ProcState == ProcState.Aborted)
                {
                    if (State != gProcMain.error && State != gProcMain.idle)
                    {
                        NextProcess(gProcMain.error);
                    }
                    return;
                }

                //if (ProcessSelected.ProcState == ProcState.Finished)
                //{
                //    if (WaitingDetails.ProcessAfterWait != gProcMain.wait)
                //    {
                //        if (Device.ItemValues.ErrorDetected == false)
                //        {
                //            NextProcess(WaitingDetails.ProcessAfterWait);
                //        }
                //        return;
                //    }
                //    else if (WaitingDetails.ProcessCurrent != gProcMain.wait)
                //    {
                //        NextProcess(WaitingDetails.ProcessCurrent);
                //        return;
                //    }
                //}
                NextProcess(ProcessSelected.Process);
            });
        }
    }
}
