using System.Text;
using static CaliboxLibrary.DataBase;

namespace CaliboxLibrary
{
    public class OpCodeResponse
    {
        public OpCode OpCode { get; set; }
        public string[] Header { get; set; }
        public string[] Values { get; set; }

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
            if(force ||_ResponseParsed == null)
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

    }

}
