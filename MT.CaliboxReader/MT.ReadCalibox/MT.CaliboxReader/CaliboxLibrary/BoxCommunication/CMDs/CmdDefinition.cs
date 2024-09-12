namespace CaliboxLibrary
{
    public class CmdDefinition
    {
        public CmdDefinition(OpCode opcode, string cmdAdd = null, int wait = 0, int retry = 1)
        {
            CreateCommand(opcode, cmdAdd);
            Name = opcode.ToString();
            WaitMilliseconds = wait;
            RetriesMax = retry;
        }
        public override string ToString()
        {
            return CommandTextWithData;
        }

        public string Name { get; set; }

        /**********************************************************
        * FUNCTION:     Command
        * DESCRIPTION:
        ***********************************************************/
        public OpCode OpCode { get; private set; }
        private string OpCodeText { get; set; }
        public bool IsOpcodeAdd { get; private set; }
        public string OpCodeAdd { get; private set; }
        public string CommandText
        {
            get
            {
                return $"{OpCodeText} {OpCodeAdd}".Trim();
            }
        }
        public string Data { get; set; }
        public string CommandTextWithData
        {
            get
            {
                return $"{OpCodeText} {OpCodeAdd} {Data}".Trim();
            }
        }

        private string CreateCommand(OpCode opCode, string add)
        {
            OpCode = opCode;
            OpCodeAdd = add;
            string result = string.Empty;
            switch (opCode)
            {
                case OpCode.RDBX:
                case OpCode.rdbx:
                case OpCode.WRBX:
                case OpCode.wrbx:
                case OpCode.RDPG:
                case OpCode.rdpg:
                case OpCode.WRPG:
                case OpCode.wrpg:
                    OpCodeText = $"#{opCode}";
                    IsOpcodeAdd = true;
                    var split = add.Split(' ');
                    OpCodeAdd = split[0].PadLeft(2, '0');
                    if (split.Length > 1)
                    {
                        Data = split[1];
                    }
                    result = $"{OpCodeText} {add}";
                    break;
                default:
                    OpCodeText = opCode.ToString();
                    result = OpCodeText;
                    IsOpcodeAdd = false;
                    break;
            }
            return result;
        }

        /**********************************************************
        * FUNCTION:     Actions
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// Execute Next on answer received
        /// </summary>
        public bool NextOnAnswer { get; set; }

        /// <summary>
        /// Time to wait for next execution
        /// </summary>
        public int WaitMilliseconds { get; set; }

        /// <summary>
        /// Quantity of retries
        /// </summary>
        public int RetriesMax { get; set; }
    }
}
