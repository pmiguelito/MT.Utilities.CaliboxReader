using System;

namespace CaliboxLibrary
{
    public class EventRoutingArgs : EventArgs
    {
        public EventRoutingArgs()
        {

        }
        public EventRoutingArgs(CmdDefinition routine)
        {
            Routine = routine;
            Command = routine.CommandText;
        }
        public readonly CmdDefinition Routine;
        public readonly string Command;
    }
}
