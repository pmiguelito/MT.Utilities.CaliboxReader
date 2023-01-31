using static CaliboxLibrary.DataBase;

namespace CaliboxLibrary
{
    public class MeasValue
    {
        public MeasValue()
        {

        }
        public MeasValue(DbMeasValues header)
        {
            Header = header.ToString();
        }
        public MeasValue(string header)
        {
            Header = header;
        }
        public MeasValue(DbMeasValues header, string value) : this(header)
        {
            Value = value;
        }
        public MeasValue(string header, string value)
        {
            Header = header;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
        public string ToStringWithHeader()
        {
            return $"{Header}:\t{Value}";
        }

        public string Header { get; private set; }
        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { _Value = ToDecimal(value); }
        }
        public double? ValueNumeric { get; private set; }
        private string ToDecimal(string value)
        {
            if (string.IsNullOrEmpty(value)) { ValueNumeric = null; return value; }
            if (double.TryParse(value, out double result))
            {
                ValueNumeric = result;
                return ValueNumeric?.ToString("0.###");
            }
            return value;
        }

        /// <summary>
        /// Add Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Header with Value</returns>
        public string Add(string value)
        {
            Value = value;
            return ToStringWithHeader();
        }
    }
}
