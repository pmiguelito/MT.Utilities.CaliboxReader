using System;

namespace CaliboxLibrary.BoxCommunication.CMDs
{
    public class CmdSend
    {
        public CmdSend()
        {
        }

        public CmdSend(OpCode opCode, string cmdText = null)
        {
            CmdDefinition = CMD.GetOrAdd(opCode, cmdText);
            Restart();
            OpCode = opCode;
            IsCMD = opCode != OpCode.noOpCode;
            CmdText = CmdDefinition.CommandTextWithData;
        }

        public CmdSend(CmdDefinition cmd)
        {
            CmdDefinition = cmd;
            OpCode = cmd.OpCode;
            IsCMD = cmd.OpCode != OpCode.noOpCode;
            CmdText = CmdDefinition.CommandTextWithData;
            Restart();
        }

        public bool IsCMD { get; set; }
        public DateTime DateTime { get; set; }

        public CmdDefinition CmdDefinition { get; set; }
        public OpCode OpCode { get; set; } = OpCode.noOpCode;
        public string CmdText { get; set; }

        public void Restart()
        {
            DateTime = DateTime.Now;
        }
    }
}
