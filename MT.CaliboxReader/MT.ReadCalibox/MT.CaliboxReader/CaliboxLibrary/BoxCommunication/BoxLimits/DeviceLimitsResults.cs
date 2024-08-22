namespace CaliboxLibrary
{
    public class DeviceLimitsResults
    {
        public DeviceLimitsResults()
        {

        }
        public DeviceLimitsResults(DeviceResponseValues drv, DeviceLimits deviceLimits)
        {
            AddValues(drv, deviceLimits);
        }

        public DeviceLimitsValuesRating Rating;
        private DeviceMeasValues MeasValues;
        private DeviceLimtsModesStates Limits;

        public BoxMode BoxMode { get { return MeasValues?.BoxMode; } }
        //public string BoxModeHex { get { return MeasValues?.BoxModeHex; } }
        //public string BoxModeDesc { get { return MeasValues?.BoxModeDesc; } }
        public double Count { get { return MeasValues?.Count ?? 0; } }

        public bool Test_ok { get { return Rating?.Test_ok ?? false; } }

        public double ValueSet { get { return Rating?.ValueSet ?? 0; } }
        public double ValueAVG { get { return Rating?.ValueAVG ?? 0; } }
        public bool ValueAVG_ok { get { return Rating?.ValueAVG_ok ?? false; } }

        public double StdDevSet { get { return Rating?.StdDevSet ?? 1; } }
        public double StdDev { get { return Rating?.StdDev ?? 9999; } }
        public bool StdDev_ok { get { return Rating?.StdDev_ok ?? false; } }

        public bool ErrorActive { get { return Rating?.ErrorActive ?? true; } }
        public double ErrorSet { get { return Rating?.ErrorSet ?? 0; } }
        public double ErrorABS { get { return Rating?.ErrorABS ?? 9999; } }
        public bool ErrorABS_ok { get { return Rating?.ErrorABS_ok ?? false; } }

        public void SetCount(double count)
        {
            MeasValues.Count = count;
        }

        public bool AddValues(DeviceResponseValues drv, DeviceLimits deviceLimits)
        {
            if (deviceLimits.TryGetValue(drv.BoxMode, out var limits))
            {
                Limits = limits;
                MeasValues = new DeviceMeasValues(drv);
                Rating = new DeviceLimitsValuesRating()
                {
                    Limits = Limits,
                    MeasValues = MeasValues
                };
                return true;
            }
            return false;
        }
    }
}
