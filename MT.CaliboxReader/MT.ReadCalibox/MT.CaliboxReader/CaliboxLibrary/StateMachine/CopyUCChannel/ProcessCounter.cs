using System.Collections.Generic;

namespace CaliboxLibrary.StateMachine.CopyUCChannel
{
    public class ProcessCounter
    {
        public Dictionary<gProcMain, ProcessCounterDetails> CounterContainer { get; set; }
            = new Dictionary<gProcMain, ProcessCounterDetails>();

        public void Reset()
        {
            foreach (var item in CounterContainer.Values)
            {
                item.Reset();
            }
        }

        private ProcessCounterDetails _Running;
        public ProcessCounterDetails Running { get { return _Running; } }

        public ProcessCounterDetails Start(gProcMain gProc, int total = 3)
        {
            GetOrAdd(gProc, total, out _Running);
            _Running.Start();
            return _Running;
        }

        public void AnswerReceived(bool answerReceived)
        {
            if (answerReceived)
            {
                Running.Stop(answerReceived);
            }
        }

        public void GetOrAdd(gProcMain gProc, int total, out ProcessCounterDetails result)
        {
            if (CounterContainer.TryGetValue(gProc, out result))
            {
                result.Total = total;
                return;
            }
            result = new ProcessCounterDetails(gProc) { Total = total };
            CounterContainer.Add(gProc, new ProcessCounterDetails(gProc));
        }

        public void GetOrAdd(gProcMain gProc, out ProcessCounterDetails result)
        {
            if (CounterContainer.TryGetValue(gProc, out result))
            {
                return;
            }
            result = new ProcessCounterDetails(gProc);
            CounterContainer.Add(gProc, new ProcessCounterDetails(gProc));
        }

        public bool IsRetry(gProcMain gProc)
        {
            GetOrAdd(gProc, out var result);
            return result.IsRetry();
        }

        public ProcessCounterDetails Increese(gProcMain gProc)
        {
            GetOrAdd(gProc, out var result);
            result.Increese();
            return result;
        }

        public string GetAttemptMessage(gProcMain gProc)
        {
            GetOrAdd(gProc, out var result);
            return result.GetAttemptMessage();
        }
    }
}
