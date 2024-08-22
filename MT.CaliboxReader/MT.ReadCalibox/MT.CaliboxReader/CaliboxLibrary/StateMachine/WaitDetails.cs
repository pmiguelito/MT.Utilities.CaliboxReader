using CaliboxLibrary.StateMachine.CopyUCChannel;
using System;
using System.Collections.Generic;

namespace CaliboxLibrary
{
    public class WaitDetails
    {
        public ProcessesUC ProcessesUC { get; private set; }
        /************************************************
         * FUNCTION:    Constructor(s)
         * DESCRIPTION:
         ************************************************/
        public WaitDetails()
        {

        }

        public WaitDetails(ProcessesUC processeUC)
        {
            ProcessesUC = processeUC;
        }

        #region Process States
        /**********************************************************
        * FUNCTION:     Process States
        * DESCRIPTION:
        ***********************************************************/

        public bool CalibrationRunning { get; set; }
        #endregion

        public gProcMain ProcessCurrent { get; set; }
        public gProcMain ProcessAfterWait
        {
            get;
            set;
        }
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
        public DateTime TimeEnd { get; set; }
        public TimeSpan TimeDiff { get { return GetTimeDiff(); } }
        public TimeSpan GetTimeDiff()
        {
            var result = DateTime.Now - TimeStart;
            return result;
        }
        public TimeSpan GetTimeRemain()
        {
            var result = TimeEnd - DateTime.Now;
            return result;
        }
        public bool IsTimeExpired
        {
            get { return TimeExpired(); }
        }

        public bool TimeExpired()
        {
            if (AnswerGoNext && Answer_Received)
            {
                return true;
            }
            var diff = GetTimeDiff();
            var result = diff.TotalMilliseconds > Wait_ms;
            return result;
        }

        public DateTime LastMessageReceved { get; set; } = DateTime.MinValue;

        public DeviceResponseValues DeviceResponseValues { get; set; }
        public void DataReceived(DeviceResponseValues data, bool answerReceived)
        {
            if (answerReceived)
            {
                Answer_Received = answerReceived;
            }
            LastMessageReceved = DateTime.Now;
            DeviceResponseValues = data;
            Message = data.Response;
        }
        public void DataReceived(DeviceResponseValues data)
        {
            LastMessageReceved = DateTime.Now;
            DeviceResponseValues = data;
            if (OpCodeAnswerReceive == null)
            {
                OpCodeAnswerReceive = new List<OpCode>();
            }
            if (!OpCodeAnswerReceive?.Contains(data.OpCodeResp) ?? false)
            {
                OpCodeAnswerReceive.Add(data.OpCodeResp);
            }
            Message = data.Response;
            if (OpCodeAnswerReceive == null)
            {
                OpCodeAnswerReceive = new List<OpCode>();
            }
            if (OpCodeAnswerReceive.Contains(OpCodeAnswerWaiting))
            {
                Answer_Received = true;
            }
        }
        #region Resets
        /**********************************************************
        * FUNCTION:     Resets
        * DESCRIPTION:
        ***********************************************************/

        public void Reset()
        {
            OpCodeAnswerReceive = new List<OpCode>();
            Answer_Received = false;
        }

        public void Reset(gProcMain current, int wait, gProcMain procAfterWait, bool waitAnswer = true)
        {
            Reset(current);
            ProcessAfterWait = procAfterWait;
            Wait_ms = wait;
            AnswerGoNext = waitAnswer;
        }

        public void Reset(gProcMain current)
        {
            Reset();
            ProcessCurrent = current;
            TimeStart = DateTime.Now;
            TimeEnd = TimeStart.AddMilliseconds(Wait_ms);
        }

        public void Reset(gProcMain current, OpCode opCode)
        {
            Reset(current);
            OpCodeRequest = opCode;
            OpCodeAnswerWaiting = opCode.GetOpposite();
        }

        public void Increese()
        {
            TimeStart = DateTime.Now;
            TimeEnd = TimeStart.AddMilliseconds(Wait_ms);
        }
        #endregion
    }
}