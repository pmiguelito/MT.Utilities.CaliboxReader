namespace CaliboxLibrary
{
    public enum ModeTypes
    {
        Offset, Temperature, Polarization
    }
    public class DeviceLimtsModesStates
    {
        public string Title;

        public ModeTypes ModeType;

        /// <summary>
        /// ValueSet
        /// </summary>
        public double RawValue;

        public double Current;

        public bool ErrorActive;
        public double RawError;
        public double CalError;
        public double WepError;

        public double StdDev;

        // /**************************************************************************************
        //' Limits:    CaliBox
        //'            Temperature
        //'**************************************************************************************/
        // public double TempRefVoltTemp;
        // public double TempRefVolt;
        // public double TempRefVolt2Temp;
        // public double TempRefVolt2;
        // public double TempRefVolt3Temp;
        // public double TempRefVolt3;
        // public double TempErr;
        // public double TempErrTemp;

        // /**************************************************************************************
        //' Limits:    CaliBox
        //'            Polarization
        //'**************************************************************************************/
        // public double UpolError;
        // public double UpolErrorValue;
    }
}
