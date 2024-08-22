using System;
using System.Collections;
using System.Collections.Generic;

namespace CaliboxLibrary.StateMachine
{
    public class ProcessWorker : IEnumerable<Processes>, IEnumerator
    {
        public List<Processes> ProcessOrders { get; private set; }
        public ProcessWorker(List<Processes> procOrders)
        {
            ProcessOrders = new List<Processes>(procOrders);
        }

        public IEnumerator<Processes> GetEnumerator()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in ProcessOrders)
            {
                yield return item;
            }
        }

        #region Lists
        /**********************************************************
        * FUNCTION:     Lists
        * DESCRIPTION:
        ***********************************************************/
        public int Position { get; private set; } = -1;

        object IEnumerator.Current { get { return Current; } }
        private Processes _Current;
        public Processes Current { get { return _Current; } }

        public bool MoveNext()
        {
            Position++;
            if (Position < ProcessOrders.Count)
            {
                if (_Current != null)
                {
                    _Current.ProcState = ProcState.Terminated;
                }
                _Current = Processes.GetProcess(Position);
                _Current.Start();
                return true;
            }
            _Current = null;
            return false;
        }
        public bool MoveNext(out Processes current)
        {
            var result = MoveNext();
            current = Current;
            return result;
        }
        public void Reset()
        {
            Position = -1;
            ProcState = ProcState.Idle;
            ProcStartTime = DateTime.MinValue;
            if (ProcessOrders != null)
            {
                foreach (var item in ProcessOrders)
                {
                    item.Reset();
                }
            }

        }

        public void Terminate()
        {
            Current.Terminate();
        }

        public void Executed()
        {
            Current.Executed = true;
        }
        #endregion

        public ProcState ProcState { get; set; } = ProcState.Idle;
        public DateTime ProcStartTime { get; private set; }
        public TimeSpan ProcDuration { get { return DateTime.Now - ProcStartTime; } }
        public int Attents { get { return Current?.Attends ?? 0; } }
        public void AttentsIncreese()
        {
            Current.AttendsIncreese();
        }
        public bool AttentsExceeded()
        {
            return Current.AttendsExeeded();
        }

        #region Start
        /**********************************************************
        * FUNCTION:     Start
        * DESCRIPTION:
        ***********************************************************/
        public DeviceCom DeviceCom { get; set; }
        public void Start(OpCode calMode, DeviceCom device)
        {
            Processes.Changed -= Processes_Changed;
            TimerProcess.Stop();
            DeviceCom = device;
            Reset();
            Processes.Changed += Processes_Changed;
            SetCalibrationMode(calMode);
            if (MoveNext())
            {
                ProcStartTime = DateTime.Now;
                TimerProcess.Interval = 1000;
                TimerProcess.Start();
            }
        }

        private void Processes_Changed(object sender, Processes e)
        {
            OnProcChanged(e);
        }

        private void SetCalibrationMode(OpCode calMode)
        {
            foreach (var item in ProcessOrders)
            {
                if (item.ProcName == gProcMain.Calibration)
                {
                    //item.CMD = new Commands(calMode);
                    return;
                }
            }
        }
        #endregion

        #region Timer
        /**********************************************************
        * FUNCTION:     Timer
        * DESCRIPTION:
        ***********************************************************/
        private System.Windows.Forms.Timer _TimerProcess;
        public System.Windows.Forms.Timer TimerProcess
        {
            get
            {
                if (_TimerProcess == null)
                {
                    Init_timProcess();
                }
                return _TimerProcess;
            }
        }
        private void Init_timProcess()
        {
            _TimerProcess = new System.Windows.Forms.Timer() { Interval = 250 };
            _TimerProcess.Tick += TimerProcess_Tick;
        }
        private void TimerProcess_Tick(object sender, EventArgs e)
        {
            if (Position > -1)
            {
                Current.Exeeded();
                if (CheckAnswer())
                {
                    //return;
                }
                switch (Current.ProcState)
                {
                    case ProcState.Aborted:
                    case ProcState.Idle:
                    case ProcState.Running:
                    case ProcState.SendCMD:
                        break;
                    case ProcState.Terminated:
                        //MoveNext();
                        //OnProcChanged();
                        break;
                    case ProcState.Finished:
                        MoveNext();
                        break;
                        //case ProcessState.WaitAfter:
                        //case ProcessState.WaitBefore:
                        //    if (CheckAnswer())
                        //    {
                        //        Current.ProcState = ProcState.Running;
                        //    }
                        //    break;
                        //case ProcessState.Aborted:
                        //    break;
                        //case ProcessState.Exit:
                        //    MoveNext();
                        //    OnProcChanged();
                        //    return;
                }
                OnProcStateChanged(Current.ProcState);
            }
        }
        private bool CheckAnswer()
        {
            if (Current.IsCommand)
            {
                Enum.TryParse(DeviceCom?.CMD_Received, out OpCode response);
                //if (Current.CMD.CheckResponse(response))
                //{
                //    Current.SetAnswer(received: true);
                //    return true;
                //}
            }
            return false;
        }
        #endregion

        #region Events
        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:
        ***********************************************************/
        public event EventHandler<Processes> ProcessChanged;
        protected virtual void OnProcChanged(Processes e)
        {
            ProcessChanged?.Invoke(this, e);
        }

        public event EventHandler<Processes> ProcStateChanged;
        protected virtual bool OnProcStateChanged(ProcState procState)
        {
            if (ProcState == procState)
            {
                return false;
            }
            ProcState = procState;
            ProcStateChanged?.Invoke(this, Current);
            return true;
        }
        #endregion
    }
}
