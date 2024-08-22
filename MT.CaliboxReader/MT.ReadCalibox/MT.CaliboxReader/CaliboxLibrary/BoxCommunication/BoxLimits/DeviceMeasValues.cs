
namespace CaliboxLibrary
{
    public class DeviceMeasValues
    {
        public DeviceMeasValues()
        {
            DeviceValues = new DeviceResponseValues();
        }

        public DeviceMeasValues(DeviceResponseValues drv)
        {
            DeviceValues = drv ?? new DeviceResponseValues();
        }

        public DeviceResponseValues DeviceValues { get; private set; }

        public double Count;
        public BoxMode BoxMode
        {
            get
            {
                if (DeviceValues == null) { return null; }
                return DeviceValues.BoxMode;
            }
        }

        public MeasValues Meas_I { get { return DeviceValues.MeasValues.Meas_I; } }
        public double? ValueSet { get { return Meas_I.Set.ValueNumeric; } }
        public double? ValueAVG { get { return Meas_I.Avg.ValueNumeric; } }
        public double? StdDev { get { return Meas_I.StdDev.ValueNumeric; } }
        public double? ErrorABS { get { return Meas_I.ErrorAbs.ValueNumeric; } }
        public MeasValues Meas_Temp { get { return DeviceValues.MeasValues.Meas_Temp; } }
    }
}