using System.Collections.Generic;
using System.Text;

namespace CaliboxLibrary
{
    public class OpCodeResponse
    {
        public OpCodeResponse()
        {

        }

        public OpCodeResponse(OpCode opCode, string[] header)
        {
            OpCode = opCode;
            Header = header;
        }

        /// <summary>
        /// Create a new instance using informations
        /// <see cref="OpCode"/> & <see cref="Header"/>
        /// </summary>
        /// <returns></returns>
        public OpCodeResponse New()
        {
            return new OpCodeResponse(OpCode, Header);
        }

        public OpCode OpCode { get; private set; }

        public string[] Header
        {
            get;
            private set;
        }

        private string[] _Values;
        public string[] Values
        {
            get { return _Values; }
            set
            {
                _Values = value;
                _ResponseParsed = null;
            }
        }

        private string _ResponseParsed;
        /// <summary>
        /// Header and Values
        /// </summary>
        public string ResponseParsed
        {
            get { return Parse(); }
        }

        /// <summary>
        /// Received direct values without changes
        /// </summary>
        public string ResponseValues { get; set; }
        private string Parse(bool force = false)
        {
            if (force || _ResponseParsed == null)
            {
                int i = 0;
                StringBuilder sb = new StringBuilder();
                foreach (string value in Values)
                {
                    try { sb.Append($"{Header[i]}:\t{value}"); } catch { }
                    i++;
                }
                _ResponseParsed = sb.ToString();
            }
            return _ResponseParsed;
        }

        private List<OpCodeResponseValue> _Data = new List<OpCodeResponseValue>();

        public List<OpCodeResponseValue> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }

    public class OpCodeResponseValue
    {
        public string Header { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Header}:\t{Value}";
        }
    }
}
