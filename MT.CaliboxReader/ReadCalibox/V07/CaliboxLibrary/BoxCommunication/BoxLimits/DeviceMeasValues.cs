using static System.Windows.Forms.AxHost;
using static System.Net.Mime.MediaTypeNames;

namespace CaliboxLibrary
{
    public class DeviceMeasValues
    {
        public DeviceMeasValues()
        {

        }
        //public DeviceMeasValues(string boxModeHex, double? valueSet, double? valueAVG, double? stdDev, double? errorABS)
        //{
        //    BoxMode = boxModeHex;
        //    ValueSet = valueSet;
        //    ValueAVG = valueAVG;
        //    StdDev = stdDev;
        //    ErrorABS = errorABS;
        //}
        //public DeviceMeasValues(string boxModeHex, string valueSet, string valueAVG, string stdDev, string errorABS)
        //{
        //    BoxMode = boxModeHex;
        //    ValueSet = valueSet.ToDouble();
        //    ValueAVG = valueAVG.ToDouble();
        //    StdDev = stdDev.ToDouble();
        //    ErrorABS = errorABS.ToDouble();
        //}
        public DeviceMeasValues(DeviceResponseValues drv)//:this(drv.BoxMode.Hex, drv.I_SetDec, drv.I_AVGDec, drv.I_StdDevDec, drv.I_ErrorDec)
        {
            DeviceValues = drv;
        }
        public DeviceResponseValues DeviceValues { get; private set; }

        public double Count;
        public BoxMode BoxMode 
        { 
            get 
            { 
                if(DeviceValues == null) { return null; }
                return DeviceValues.BoxMode; 
            } 
        }
        public string BoxModeHex 
        { 
            get 
            {
                if (BoxMode == null) { return null; }
                return BoxMode.Hex; 
            } 
        }
        public string BoxModeDesc
        {
            get
            {
                if (BoxModeHex == null) { return null; }
                return BoxMode.Desc;
            }
        }

        public MeasValues Meas_I { get { return DeviceValues.Meas_I; } }
        public double? ValueSet { get { return Meas_I.Set.ValueNumeric; } }
        public double? ValueAVG { get { return Meas_I.Avg.ValueNumeric; } }
        public double? StdDev { get { return Meas_I.StdDev.ValueNumeric; } }
        public double? ErrorABS { get { return Meas_I.ErrorAbs.ValueNumeric; } }
        public MeasValues Meas_Temp { get { return DeviceValues.Meas_Temp; } }
    }
}