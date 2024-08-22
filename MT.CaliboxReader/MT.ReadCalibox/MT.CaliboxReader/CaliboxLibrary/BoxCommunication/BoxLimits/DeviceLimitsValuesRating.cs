namespace CaliboxLibrary
{
    public class DeviceLimitsValuesRating
    {
        public DeviceMeasValues MeasValues;
        public DeviceLimtsModesStates Limits;
        public bool Test_ok { get { return ValueAVG_ok && StdDev_ok && ErrorABS_ok; } }
        public double? ValueSet { get { return Limits?.RawValue; } }
        public double? ValueAVG { get { return MeasValues?.ValueAVG; } }
        public bool ValueAVG_ok
        {
            get
            {
                if (Limits != null)
                {
                    return (ValueAVG ?? 9999) <= (ValueSet ?? -9999);
                }
                return false;
            }
        }

        public double? StdDevSet { get { return Limits?.StdDev ?? 1; } }
        public double? StdDev { get { return MeasValues?.StdDev ?? 9999; } }
        public bool StdDev_ok
        {
            get
            {
                if (Limits != null)
                {
                    return (StdDev ?? 9999) <= (StdDevSet ?? -9999);
                }
                return false;
            }
        }

        public bool ErrorActive { get { return Limits?.ErrorActive ?? true; } }
        public double? ErrorSet { get { return Limits?.RawError; } }
        public double? ErrorABS { get { return MeasValues?.ErrorABS; } }
        public bool ErrorABS_ok
        {
            get
            {
                if (ErrorActive)
                {
                    return (ErrorABS ?? 9999) <= (ErrorSet ?? -9999);
                }
                return true;
            }
        }
    }
}
