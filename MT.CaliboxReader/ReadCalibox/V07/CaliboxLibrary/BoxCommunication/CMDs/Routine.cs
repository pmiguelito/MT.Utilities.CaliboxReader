using System.Diagnostics;

namespace CaliboxLibrary
{
    public class Routine
    {
        public Routine(OpCode opcode, string cmd, int wait = 0, int retry = 1)
        {
            OpCode = opcode;
            Command = cmd;
            Wait = wait;
            RetriesCount = 0;
            RetriesMax = retry;
            Running = false;
            CMDsw = new Stopwatch();
        }

        public OpCode OpCode { get; set; }
        public string Command { get; set; }
        public int Wait { get; set; }
        public int RetriesMax { get; set; }
        public int RetriesCount { get; set; }
        public bool Running { get; set; }

        public Stopwatch CMDsw { get; set; }
        public int Send(int nowrunningIndex)
        {
            if (!Running)
            {
                Running = true;
                CMDsw = Stopwatch.StartNew();
                RetriesCount++;
                CMD.OnSendCMD(this, this);
                return nowrunningIndex;
            }
            if (TimeElapsed && Retry)
            {
                CMD.OnSendCMD(this, this);
                CMDsw.Restart();
                RetriesCount++;
                return nowrunningIndex;
            }
            else if (TimeElapsed && !Retry)
            {
                nowrunningIndex++;
            }
            return nowrunningIndex;
        }
        public bool TimeElapsed
        {
            get
            {
                if (Wait < 1) { return true; }
                return CMDsw.ElapsedMilliseconds > Wait;
            }
        }
        public bool Retry
        {
            get
            {
                if (RetriesMax < 2) { return false; }
                if (RetriesCount >= RetriesMax) { return false; }
                return true;
            }
        }
    }
}
