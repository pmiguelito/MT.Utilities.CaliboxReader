using System;

namespace CaliboxLibrary
{
    public class EventRoutingArgs : EventArgs
    {
        public EventRoutingArgs()
        {

        }
        public EventRoutingArgs(Routine routine)
        {
            Routine = routine;
            Command = routine.Command;
        }
        public readonly Routine Routine;
        public readonly string Command;
    }
}
