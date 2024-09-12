namespace CaliboxLibrary
{
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
