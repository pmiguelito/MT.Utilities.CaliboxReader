using System;

namespace CaliboxLibrary.StateMachine.CopyUCChannel
{
    public class EventMessageArgs : EventArgs
    {
        public EventMessageArgs(gProcMain state,
                                string message,
                                string attempts = null) : this(state, TimeSpan.Zero, message, attempts)
        {

        }
        public EventMessageArgs(gProcMain state,
                                TimeSpan time,
                                string message,
                                string attempts = null)
        {
            Time = time;
            State = state;
            Message = message;
            Attempts = attempts;
        }

        public TimeSpan Time { get; }
        public gProcMain State { get; }

        public string Message { get; }
        public string Attempts { get; }
    }
}
