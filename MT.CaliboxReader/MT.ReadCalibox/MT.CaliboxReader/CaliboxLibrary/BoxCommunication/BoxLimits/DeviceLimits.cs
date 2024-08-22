namespace CaliboxLibrary
{
    public class DeviceLimits
    {
        public struct StrucBoxIdendificationState
        {
            public bool DipSwitch;
            public bool Betriebsmittel;
            public bool CalibrationBox;
            public bool FW;
            public bool RawVal;
            public bool Current;
            public bool RawError;
            public bool CalError;
            public bool WepError;
            public bool StdDev;

            public bool State()
            {
                if (!DipSwitch) { return false; }
                if (!Betriebsmittel) { return false; }
                if (!CalibrationBox) { return false; }
                if (!FW) { return false; }
                if (!RawVal) { return false; }
                if (!Current) { return false; }
                if (!RawError) { return false; }
                if (!CalError) { return false; }
                //if (!WepError) { return false; }
                return true;
            }
        }

        public StrucBoxIdendificationState BoxIdentification_State = new StrucBoxIdendificationState();

        /**************************************************************************************
       ' Limits:   CaliBox HEADER
       '**************************************************************************************/
        public string BeM { get; set; }
        public string BeM_BoxNr { get; set; }
        public string HW_ID { get; set; }
        public string HW_Version { get; set; }
        public string FW_Version { get; set; }
        public string Compiled { get; set; }

        public string DipSwitch { get; set; }
        public string BoxDesc { get; set; }

        /**************************************************************************************
       ' Limits:    CaliBox checksum
       '
       '**************************************************************************************/
        public string CS10calc { get; set; }
        public string CS10EEprom { get; set; }

        public string CS31calc { get; set; }
        public string CS31EEprom { get; set; }

        public bool CS_ok
        {
            get
            {
                return CS10calc == CS10EEprom && CS31calc == CS31EEprom;
            }
        }

        public string BoxConsistent { get; set; }

        /**************************************************************************************
       ' Limits:    CaliBox
       '            Temperature
       '**************************************************************************************/
        /// <summary>
        /// Temperature Minimum
        /// </summary>
        public double TempRefVoltTemp;
        /// <summary>
        /// Temperature Minimum
        /// </summary>
        public double TempRefVolt;

        /// <summary>
        /// Temperature NTC 25°
        /// </summary>
        public double TempRefVolt2Temp;
        /// <summary>
        /// Temperature NTC 25°
        /// </summary>
        public double TempRefVolt2;
        /// <summary>
        /// Temperature Maximum
        /// </summary>
        public double TempRefVolt3Temp;
        /// <summary>
        /// Temperature Maximum
        /// </summary>
        public double TempRefVolt3;

        /// <summary>
        /// Temperature Tolerance
        /// </summary>
        public double TempErr;
        /// <summary>
        /// Temperature Tolerance
        /// </summary>
        public double TempErrTemp;

        /**************************************************************************************
       ' Limits:    CaliBox
       '            Polarization
       '**************************************************************************************/
        public double UpolError;
        public double UpolErrorValue;

        /**************************************************************************************
       ' Limits:    Messbereiche
       '
       '**************************************************************************************/
        public DeviceLimitsModes RawVal { get; set; } = new DeviceLimitsModes();
        public DeviceLimitsModes Current { get; set; } = new DeviceLimitsModes();
        public DeviceLimitsModes RawErrorCalibration { get; set; } = new DeviceLimitsModes();
        public DeviceLimitsModes RawErrorVerification { get; set; } = new DeviceLimitsModes();
        public DeviceLimitsModes CalError { get; set; } = new DeviceLimitsModes();
        public DeviceLimitsModes WepError { get; set; } = new DeviceLimitsModes();
        public DeviceLimitsModes StdDev { get; set; } = new DeviceLimitsModes();

        private DeviceLimtsModesStates _LOW1calib;
        public DeviceLimtsModesStates LOW1calib
        {
            get
            {
                if (_LOW1calib == null)
                {
                    _LOW1calib = new DeviceLimtsModesStates()
                    {
                        Title = "Low1 Calibration",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.Low1,
                        Current = Current.Low1,
                        ErrorActive = false,
                        RawError = RawErrorCalibration.Low1,
                        CalError = CalError.Low1,
                        WepError = WepError.Low1,
                        StdDev = StdDev.Low1
                    };
                }
                return _LOW1calib;
            }
        }

        private DeviceLimtsModesStates _LOW1verif;
        public DeviceLimtsModesStates LOW1verif
        {
            get
            {
                if (_LOW1verif == null)
                {
                    _LOW1verif = new DeviceLimtsModesStates()
                    {
                        Title = "Low1 Verification",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.Low1,
                        Current = Current.Low1,
                        ErrorActive = true,
                        RawError = RawErrorVerification.Low1,
                        CalError = CalError.Low1,
                        WepError = WepError.Low1,
                        StdDev = StdDev.Low1
                    };
                }
                return _LOW1verif;
            }
        }
        private DeviceLimtsModesStates _LOW2calib;
        public DeviceLimtsModesStates LOW2calib
        {
            get
            {
                if (_LOW2calib == null)
                {
                    _LOW2calib = new DeviceLimtsModesStates()
                    {
                        Title = "Low2 Calibration",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.Low2,
                        Current = Current.Low2,
                        ErrorActive = false,
                        RawError = RawErrorCalibration.Low2,
                        CalError = CalError.Low2,
                        WepError = WepError.Low2,
                        StdDev = StdDev.Low2
                    };
                }
                return _LOW2calib;
            }
        }
        private DeviceLimtsModesStates _LOW2verif;
        public DeviceLimtsModesStates LOW2verif
        {
            get
            {
                if (_LOW2verif == null)
                {
                    _LOW2verif = new DeviceLimtsModesStates()
                    {
                        Title = "Low2 Verification",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.Low2,
                        Current = Current.Low2,
                        ErrorActive = true,
                        RawError = RawErrorVerification.Low2,
                        CalError = CalError.Low2,
                        WepError = WepError.Low2,
                        StdDev = StdDev.Low2
                    };
                }
                return _LOW2verif;
            }
        }
        private DeviceLimtsModesStates _HIGH1calib;
        public DeviceLimtsModesStates HIGH1calib
        {
            get
            {
                if (_HIGH1calib == null)
                {
                    _HIGH1calib = new DeviceLimtsModesStates()
                    {
                        Title = "High1 Calibration",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.High1,
                        Current = Current.High1,
                        ErrorActive = false,
                        RawError = RawErrorCalibration.High1,
                        CalError = CalError.High1,
                        WepError = WepError.High1,
                        StdDev = StdDev.High1
                    };
                }
                return _HIGH1calib;
            }
        }
        private DeviceLimtsModesStates _HIGH1verif;
        public DeviceLimtsModesStates HIGH1verif
        {
            get
            {
                if (_HIGH1verif == null)
                {
                    _HIGH1verif = new DeviceLimtsModesStates()
                    {
                        Title = "High1 Verification",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.High1,
                        Current = Current.High1,
                        ErrorActive = true,
                        RawError = RawErrorVerification.High1,
                        CalError = CalError.High1,
                        WepError = WepError.High1,
                        StdDev = StdDev.High1
                    };
                }
                return _HIGH1verif;
            }
        }
        private DeviceLimtsModesStates _HIGH2calib;
        public DeviceLimtsModesStates HIGH2calib
        {
            get
            {
                if (_HIGH2calib == null)
                {
                    _HIGH2calib = new DeviceLimtsModesStates()
                    {
                        Title = "High2 Calibration",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.High2,
                        Current = Current.High2,
                        ErrorActive = false,
                        RawError = RawErrorCalibration.High2,
                        CalError = CalError.High2,
                        WepError = WepError.High2,
                        StdDev = StdDev.High2
                    };
                }
                return _HIGH2calib;
            }
        }
        private DeviceLimtsModesStates _HIGH2verif;
        public DeviceLimtsModesStates HIGH2verif
        {
            get
            {
                if (_HIGH2verif == null)
                {
                    _HIGH2verif = new DeviceLimtsModesStates()
                    {
                        Title = "High1 Verification",
                        ModeType = ModeTypes.Offset,
                        RawValue = RawVal.High2,
                        Current = Current.High2,
                        ErrorActive = true,
                        RawError = RawErrorVerification.High2,
                        CalError = CalError.High2,
                        WepError = WepError.High2,
                        StdDev = StdDev.High2
                    };
                }
                return _HIGH2verif;
            }
        }

        private DeviceLimtsModesStates _TempNTC;
        public DeviceLimtsModesStates TempNTC
        {
            get
            {
                if (_TempNTC == null)
                {
                    _TempNTC = new DeviceLimtsModesStates()
                    {
                        Title = "Temp 25°C NTC",
                        ModeType = ModeTypes.Temperature,
                        RawValue = 25,
                        Current = TempErrTemp,
                        ErrorActive = true,
                        RawError = TempErr,
                        //CalError = CalError.High2,
                        //WepError = WepError.High2,
                        StdDev = 0.1 //StdDev.High2
                    };
                }
                return _TempNTC;
            }
        }

        private DeviceLimtsModesStates _Temp20PT1000;
        public DeviceLimtsModesStates Temp20PT1000
        {
            get
            {
                if (_Temp20PT1000 == null)
                {
                    _Temp20PT1000 = new DeviceLimtsModesStates()
                    {
                        Title = "Temp 20°C PT1000",
                        ModeType = ModeTypes.Temperature,
                        RawValue = 20,
                        Current = TempErrTemp,
                        ErrorActive = true,
                        RawError = TempErr,
                        //CalError = CalError.High2,
                        //WepError = WepError.High2,
                        StdDev = 0.1 //StdDev.High2
                    };
                }
                return _Temp20PT1000;
            }
        }

        private DeviceLimtsModesStates _Temp30PT1000;
        public DeviceLimtsModesStates Temp30PT1000
        {
            get
            {
                if (_Temp30PT1000 == null)
                {
                    _Temp30PT1000 = new DeviceLimtsModesStates()
                    {
                        Title = "Temp 30°C PT1000",
                        ModeType = ModeTypes.Temperature,
                        RawValue = 30,
                        Current = TempErrTemp,
                        ErrorActive = true,
                        RawError = TempErr,
                        //CalError = CalError.High2,
                        //WepError = WepError.High2,
                        StdDev = 0.1 //StdDev.High2
                    };
                }
                return _Temp30PT1000;
            }
        }

        private DeviceLimtsModesStates _UPol500;
        public DeviceLimtsModesStates UPol500
        {
            get
            {
                if (_UPol500 == null)
                {
                    _UPol500 = new DeviceLimtsModesStates()
                    {
                        Title = "Polarization 500mV",
                        ModeType = ModeTypes.Polarization,
                        RawValue = 500,
                        Current = UpolErrorValue,
                        ErrorActive = true,
                        RawError = UpolError,
                        //CalError = CalError.High2,
                        //WepError = WepError.High2,
                        StdDev = 0.1 //StdDev.High2
                    };
                }
                return _UPol500;
            }
        }

        private DeviceLimtsModesStates _UPol674;
        public DeviceLimtsModesStates UPol674
        {
            get
            {
                if (_UPol674 == null)
                {
                    _UPol674 = new DeviceLimtsModesStates()
                    {
                        Title = "Polarization 674mV",
                        ModeType = ModeTypes.Polarization,
                        RawValue = 674,
                        Current = UpolErrorValue,
                        ErrorActive = true,
                        RawError = UpolError,
                        //CalError = CalError.High2,
                        //WepError = WepError.High2,
                        StdDev = 0.1 //StdDev.High2
                    };
                }
                return _UPol674;
            }
        }

        public bool CheckLimits(DeviceResponseValues drv, out DeviceLimitsResults result)
        {
            result = new DeviceLimitsResults(drv, this);
            return result.Rating != null;
        }

        //private Dictionary<BoxMode, DeviceLimtsModesStates> _BoxModeToLimit;
        ///// <summary>
        ///// KEY: BoxMode
        ///// </summary>

        //public Dictionary<BoxMode, DeviceLimtsModesStates> BoxModeToLimit
        //{
        //    get
        //    {
        //        if (_BoxModeToLimit == null)
        //        {
        //            _BoxModeToLimit = new Dictionary<BoxMode, DeviceLimtsModesStates>();
        //            InitializeBoxModeLimits();
        //        }

        //        return _BoxModeToLimit;
        //    }
        //}

        //private void InitializeBoxModeLimits()
        //{
        //    BoxModeToLimit.Add(BoxMode.Calib_674mV_Low1, LOW1calib);
        //    BoxModeToLimit.Add(BoxMode.Calib_500mV_Low1, LOW1calib);

        //    BoxModeToLimit.Add(BoxMode.Verify_674mV_Low1, LOW1verif);
        //    BoxModeToLimit.Add(BoxMode.Verify_500mV_Low1, LOW1verif);

        //    BoxModeToLimit.Add(BoxMode.Calib_674mV_Low2, LOW2calib);
        //    BoxModeToLimit.Add(BoxMode.Calib_500mV_Low2, LOW2calib);

        //    BoxModeToLimit.Add(BoxMode.Verify_674mV_Low2, LOW2verif);
        //    BoxModeToLimit.Add(BoxMode.Verify_500mV_Low2, LOW2verif);

        //    BoxModeToLimit.Add(BoxMode.Calib_674mV_High1, HIGH1calib);
        //    BoxModeToLimit.Add(BoxMode.Calib_500mV_High1, HIGH1calib);

        //    BoxModeToLimit.Add(BoxMode.Verify_674mV_High1, HIGH1verif);
        //    BoxModeToLimit.Add(BoxMode.Verify_500mV_High1, HIGH1verif);

        //    BoxModeToLimit.Add(BoxMode.Calib_674mV_High2, HIGH2calib);
        //    BoxModeToLimit.Add(BoxMode.Calib_500mV_High2, HIGH2calib);

        //    BoxModeToLimit.Add(BoxMode.Verify_674mV_High2, HIGH2verif);
        //    BoxModeToLimit.Add(BoxMode.Verify_500mV_High2, HIGH2verif);

        //    BoxModeToLimit.Add(BoxMode.Verify_Temp, TempNTC);
        //    BoxModeToLimit.Add(BoxMode.Verify_PT1000_20, Temp20PT1000);
        //    BoxModeToLimit.Add(BoxMode.Verify_PT1000_30, Temp30PT1000);

        //    BoxModeToLimit.Add(BoxMode.Verify_UPol500mV, UPol500);

        //    BoxModeToLimit.Add(BoxMode.Verify_UPol674mV, UPol674);
        //}

        ////public bool TryGetValue(string boxMode, out DeviceLimtsModesStates result)
        ////{
        ////    return BoxModeToLimit.TryGetValue(boxMode, out result);
        ////}

        //public bool TryGetValue(BoxMode boxMode, out DeviceLimtsModesStates result)
        //{
        //    if (BoxModeToLimit.TryGetValue(boxMode, out result))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public bool TryGetValue(BoxMode boxMode, out DeviceLimtsModesStates result)
        {
            switch (boxMode.Hex)
            {
                case "00":
                case "04":
                    result = LOW1calib;
                    break;
                case "08":
                case "0C":
                    result = LOW1verif;
                    break;
                case "01":
                case "05":
                    result = LOW2calib;
                    break;
                case "09":
                case "0D":
                    result = LOW2verif;
                    break;
                case "02":
                case "06":
                    result = HIGH1calib;
                    break;
                case "0A":
                case "0E":
                    result = HIGH1verif;
                    break;
                case "03":
                case "07":
                    result = HIGH2calib;
                    break;
                case "0B":
                case "0F":
                    result = HIGH2verif;
                    break;
                case "10":
                    result = TempNTC;
                    break;
                case "3F":
                    result = Temp20PT1000;
                    break;
                case "40":
                    result = Temp30PT1000;
                    break;
                case "41":
                    result = UPol500;
                    break;
                case "42":
                    result = UPol674;
                    break;
                default:
                    result = new DeviceLimtsModesStates();
                    return false; ;
            }
            return true;
        }
    }
}
