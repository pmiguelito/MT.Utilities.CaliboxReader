using System;

namespace CaliboxLibrary.StateMachine.CopyUCChannel
{
    public class ProcessCounterDetails
    {
        public ProcessCounterDetails(gProcMain gProc)
        {
            Process = gProc;
        }
        public override string ToString()
        {
            return $"{Process} {ProcState} {Counter}/{Total}";
        }

        public ProcState ProcState { get; private set; } = ProcState.Idle;
        public gProcMain Process { get; }

        private bool _AnswerReceived;
        public bool AnswerReceived
        {
            get { return _AnswerReceived; }
            set
            {
                _AnswerReceived = value;
                if (value)
                {
                    ProcState = ProcState.Finished;
                }
            }
        }

        /**********************************************************
        * FUNCTION:     Reset
        * DESCRIPTION:
        ***********************************************************/
        public void Reset()
        {
            ProcState = ProcState.Idle;
            Counter = 0;
            AnswerReceived = false;
            TimeStart = DateTime.Now;
        }

        /**********************************************************
        * FUNCTION:     Start / Stop
        * DESCRIPTION:
        ***********************************************************/
        public DateTime TimeStart { get; set; }
        public void Start()
        {
            Reset();
            ProcState = ProcState.Running;
        }

        public void Stop(bool answerReceived)
        {
            AnswerReceived = answerReceived;
            ProcState = ProcState.Finished;
        }

        public void Stop(ProcState state)
        {
            ProcState = state;
        }

        /**********************************************************
        * FUNCTION:     Counter
        * DESCRIPTION:
        ***********************************************************/
        public int Counter { get; set; }
        public int Total { get; set; } = 3;
        public bool IsRetry()
        {
            return Counter <= Total;
        }

        public void Increese()
        {
            Counter++;
            TimeStart = DateTime.Now;
        }

        public string GetAttemptMessage()
        {
            return Counter.Attempts(Total);
        }
    }
}
