using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliboxLibrary
{
    public class WaitDetails
    {
        public bool TimeOutActive { get; set; }
        public bool IsWaiting { get; set; }
        public bool ProcessRunning { get; set; }
        public bool CalibrationRunning { get; set; }
        public gProcMain ProcNext { get; set; }
        public string Message { get; set; }
        public int Wait_ms { get; set; }

        public OpCode OpCodeAnswerWaiting { get; set; }
        public List<OpCode> OpCodeAnswerReceive { get; set; }
        public OpCode OpCodeRequest { get; set; }
        public bool Answer_Received { get; set; }

        /// <summary>
        /// On Answer Received go to next process
        /// </summary>
        public bool AnswerGoNext { get; set; }

        public DateTime TimeStart { get; set; }

        public bool TimeExpired()
        {
            if (AnswerGoNext && Answer_Received)
            {
                return true;
            }
            var diff = DateTime.Now - TimeStart;
            var result = diff.TotalMilliseconds > Wait_ms;
            return result;
        }

        public DateTime LastMessageReceved { get; set; } = DateTime.MinValue;
        public void DataReceived(DeviceResponseValues data)
        {
            LastMessageReceved = DateTime.Now;
            if (!OpCodeAnswerReceive.Contains(data.OpCode))
            {
                OpCodeAnswerReceive.Add(data.OpCode);
            }
            Message = data.Response;
            if (OpCodeAnswerReceive == null)
            {
                OpCodeAnswerReceive = new List<OpCode>();
            }
            Answer_Received = OpCodeAnswerReceive.Contains(OpCodeAnswerWaiting);
        }

        public void Reset(int wait, gProcMain procNext, bool waitAnswer = true)
        {
            Reset();
            IsWaiting = false;
            ProcNext = procNext;
            Wait_ms = wait;
            AnswerGoNext = waitAnswer;
        }

        public void Reset()
        {
            OpCodeAnswerReceive = new List<OpCode>();
            TimeStart = DateTime.Now;
            Answer_Received = false;
        }

        public void Reset(OpCode opCode)
        {
            OpCodeRequest = opCode;
            OpCodeAnswerWaiting = opCode.GetOpposite();
            Reset();
        }
    }
}
