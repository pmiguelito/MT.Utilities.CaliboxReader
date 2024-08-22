using System;
using System.Collections.Generic;
using System.Linq;

namespace CaliboxLibrary
{

    public enum BoxModeCodes
    {
        S100, S500, S674,
        FWversion, BoxStatus, BoxReset, BoxIDStatus, SensorStatus,
        hFF,
        h00, h01, h02, h03, h04, h05, h08, h09, h0A, h0B, h0C, h0D, h0E, h0F,
        h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h1A, h1B, h1C, h1D, h1E, h1F,
        h20, h21, h22, h23, h24, h25, h26, h27, h28, h29, h2A, h2B,
        h32, h33, h34, h35, h36, h37, h38, h39, h3A, h3B, h3C, h3D, h3E, h3F,
        h40, h41, h42
    }
    public class BoxMode : BoxModeDetails
    {
        public readonly bool IsMeasValues;
        public readonly BoxModeCodes BoxModeCode;
        public readonly bool IsFinished;
        public BoxMode() : base()
        {
        }

        internal BoxMode(string hex, string desc, bool isError = false, bool isFinished = false, bool isMeasValues = true)
            : base(hex, desc, isError)
        {
            IsMeasValues = isMeasValues;
            IsFinished = isFinished;
            if (hex.Length == 2) { hex = "h" + hex; }
            Enum.TryParse(hex, out BoxModeCodes code);
            BoxModeCode = code;
        }

        public virtual string ToStringWithHeader()
        {
            return $"Mode:\t[{Hex}]\t{Desc}";
        }

        #region Definition
        /************************************************
         * FUNCTION:    Definitions
         * DESCRIPTION:
         ************************************************/
        public static readonly BoxMode Calib_674mV_Low1 = new BoxMode("00", "Cal_674mV_Low1");
        public static readonly BoxMode Calib_674mV_Low2 = new BoxMode("01", "Cal_674mV_Low2");
        public static readonly BoxMode Calib_674mV_High1 = new BoxMode("02", "Cal_674mV_High1");
        public static readonly BoxMode Calib_674mV_High2 = new BoxMode("03", "Cal_674mV_High2");

        public static readonly BoxMode Calib_500mV_Low1 = new BoxMode("04", "Cal_500mV_Low1");
        public static readonly BoxMode Calib_500mV_Low2 = new BoxMode("05", "Cal_500mV_Low2");
        public static readonly BoxMode Calib_500mV_High1 = new BoxMode("06", "Cal_500mV_High1");
        public static readonly BoxMode Calib_500mV_High2 = new BoxMode("07", "Cal_500mV_High2");

        public static readonly BoxMode Verify_674mV_Low1 = new BoxMode("08", "Verify_674mV_Low1");
        public static readonly BoxMode Verify_674mV_Low2 = new BoxMode("09", "Verify_674mV_Low2");
        public static readonly BoxMode Verify_674mV_High1 = new BoxMode("0A", "Verify_674mV_High1");
        public static readonly BoxMode Verify_674mV_High2 = new BoxMode("0B", "Verify_674mV_High2");

        public static readonly BoxMode Verify_500mV_Low1 = new BoxMode("0C", "Verify_500mV_Low1");
        public static readonly BoxMode Verify_500mV_Low2 = new BoxMode("0D", "Verify_500mV_Low2");
        public static readonly BoxMode Verify_500mV_High1 = new BoxMode("0E", "Verify_500mV_High1");
        public static readonly BoxMode Verify_500mV_High2 = new BoxMode("0F", "Verify_500mV_High2");

        public static readonly BoxMode Verify_Temp = new BoxMode("10", "Verify_NTC_25");
        //public static readonly BoxMode Verify_Temp = new BoxMode("10", "Verify_Temp");

        public static readonly BoxMode Box_WritePage_00 = new BoxMode("11", "Box_WritePage_00", isMeasValues: false);
        public static readonly BoxMode Box_WritePage_01 = new BoxMode("12", "Box_WritePage_01", isMeasValues: false);
        public static readonly BoxMode Box_WritePage_12 = new BoxMode("13", "Box_WritePage_12", isMeasValues: false);
        public static readonly BoxMode Box_WritePage_15 = new BoxMode("14", "Box_WritePage_15", isMeasValues: false);
        public static readonly BoxMode Box_SensorCheckUpol_674 = new BoxMode("15", "Box_SensorCheckUpol_674", isMeasValues: false);
        public static readonly BoxMode Box_SensorVerification = new BoxMode("16", "Box_SensorVerification", isMeasValues: false);
        public static readonly BoxMode Box_SensorError = new BoxMode("17", "Box_SensorError", isError: true, isFinished: true, isMeasValues: false);
        public static readonly BoxMode Box_SensorWriteCalData674 = new BoxMode("18", "Box_SensorWriteCalData674", isMeasValues: false);
        public static readonly BoxMode Box_SensorWriteCalData500 = new BoxMode("19", "Box_SensorWriteCalData500", isMeasValues: false);
        public static readonly BoxMode Box_StartSensorCalibration = new BoxMode("1A", "Box_StartSensorCalibration", isMeasValues: false);
        public static readonly BoxMode SensorFail = new BoxMode("1B", "SensorFail", isError: true, isFinished: true, isMeasValues: false);
        public static readonly BoxMode SensorCalibFinalise = new BoxMode("1C", "SensorCalibFinalise", isMeasValues: false);
        public static readonly BoxMode Box_Calibration = new BoxMode("1D", "Box_Calibration", isMeasValues: false);

        public static readonly BoxMode WEP_Test = new BoxMode("1E", "WEP_Test", isMeasValues: false);
        public static readonly BoxMode WEP_674mV_Low_1 = new BoxMode("1F", "WEP_674mV_Low_1");
        public static readonly BoxMode WEP_674mV_Low_2 = new BoxMode("20", "WEP_674mV_Low_2");
        public static readonly BoxMode WEP_500mV_Low_1 = new BoxMode("21", "WEP_500mV_Low_1");
        public static readonly BoxMode WEP_500mV_Low_2 = new BoxMode("22", "WEP_500mV_Low_2");
        public static readonly BoxMode WEP_674mV_High_1 = new BoxMode("23", "WEP_674mV_High_1");
        public static readonly BoxMode WEP_674mV_High_2 = new BoxMode("24", "WEP_674mV_High_2");
        public static readonly BoxMode WEP_500mV_High_1 = new BoxMode("25", "WEP_500mV_High_1");
        public static readonly BoxMode WEP_500mV_High_2 = new BoxMode("26", "WEP_500mV_High_2");
        public static readonly BoxMode WEP_SensorError = new BoxMode("27", "WEP_SensorError", isError: true, isFinished: true, isMeasValues: false);
        public static readonly BoxMode WEPSensorFail = new BoxMode("28", "WEPSensorFail", isError: true, isFinished: true, isMeasValues: false);

        public static readonly BoxMode SensorWepFinalise = new BoxMode("29", "SensorWepFinalise", isFinished: true, isMeasValues: false);

        public static readonly BoxMode WEP_SensorCheckUpol = new BoxMode("2A", "WEP_SensorCheckUpol");
        public static readonly BoxMode WEP_TempCheck = new BoxMode("2B", "WEP_TempCheck");

        public static readonly BoxMode Box_Idle = new BoxMode("32", "Box_Idle", isMeasValues: false);

        public static readonly BoxMode Calib_674_Calc_Low = new BoxMode("33", "Cal_674_Calc_Low");
        public static readonly BoxMode Calib_674Calc_High = new BoxMode("34", "Cal_674Calc_High");
        public static readonly BoxMode Calib_500Calc_Low = new BoxMode("35", "Cal_500Calc_Low");
        public static readonly BoxMode Calib_500Calc_High = new BoxMode("36", "Cal_500Calc_High");

        public static readonly BoxMode SuccessfullSensorCalibration = new BoxMode("37", "SuccessfullSensorCalibration", isFinished: true, isMeasValues: false);
        public static readonly BoxMode Box_SensorCheckUpol_500 = new BoxMode("38", "Box_SensorCheckUpol_500");
        public static readonly BoxMode ShowErrorValues = new BoxMode("39", "ShowErrorValues", isMeasValues: false);

        public static readonly BoxMode DebugUpolOnCathode = new BoxMode("3A", "DebugUpolOnCathode");
        public static readonly BoxMode DebugUpolOnAnode = new BoxMode("3B", "DebugUpolOnAnode");
        public static readonly BoxMode ReadPage16 = new BoxMode("3C", "ReadPage16", isMeasValues: false);
        public static readonly BoxMode RS232_OW_ACCESS = new BoxMode("3D", "RS232_OW_ACCESS", isMeasValues: false);

        public static readonly BoxMode Verify_UPol = new BoxMode("3E", "Verify_UPol");
        public static readonly BoxMode Verify_PT1000_20 = new BoxMode("3F", "Verify_PT1000_20");
        public static readonly BoxMode Verify_PT1000_30 = new BoxMode("40", "Verify_PT1000_30");
        public static readonly BoxMode Verify_UPol500mV = new BoxMode("41", "Verify_UPol500mV");
        public static readonly BoxMode Verify_UPol674mV = new BoxMode("42", "Verify_UPol674mV");

        public static readonly BoxMode NotDefined = new BoxMode("FF", "NotDefined", isMeasValues: false);

        public static readonly BoxMode CalMode_674_500mV = new BoxMode("S100", "CalMode 674/500mV", isMeasValues: false);
        public static readonly BoxMode CalMode_500mV = new BoxMode("S500", "CalMode 500mV", isMeasValues: false);
        public static readonly BoxMode CalMode_674mV = new BoxMode("S674", "CalMode 6740mV", isMeasValues: false);
        public static readonly BoxMode FWversion = new BoxMode("FWversion", "FWversion", isMeasValues: false);
        public static readonly BoxMode BoxStatus = new BoxMode("BoxStatus", "BoxStatus", isMeasValues: false);
        public static readonly BoxMode BoxReset = new BoxMode("BoxReset", "BoxReset", isMeasValues: false);
        public static readonly BoxMode BoxIDStatus = new BoxMode("BoxIDStatus", "BoxIDStatus", isMeasValues: false);
        public static readonly BoxMode SensorStatus = new BoxMode("SensorStatus", "SensorStatus", isMeasValues: false);
        #endregion

        /// <summary>
        /// KEY: HEX, Definition of the process state
        /// </summary>
        public static Dictionary<string, BoxMode> BoxModeDic { get; } = new Dictionary<string, BoxMode>(StringComparer.OrdinalIgnoreCase)
        {
            {Calib_674mV_Low1.Hex, Calib_674mV_Low1},
            {Calib_674mV_Low2.Hex,Calib_674mV_Low2},
            {Calib_674mV_High1.Hex,Calib_674mV_High1},
            {Calib_674mV_High2.Hex,Calib_674mV_High2},

            {Calib_500mV_Low1.Hex, Calib_500mV_Low1},
            {Calib_500mV_Low2.Hex,Calib_500mV_Low2},
            {Calib_500mV_High1.Hex,Calib_500mV_High1},
            {Calib_500mV_High2.Hex,Calib_500mV_High2},

            {Verify_674mV_Low1.Hex,Verify_674mV_Low1},
            {Verify_674mV_Low2.Hex,Verify_674mV_Low2},
            {Verify_674mV_High1.Hex,Verify_674mV_High1},
            {Verify_674mV_High2.Hex,Verify_674mV_High2},

            {Verify_500mV_Low1.Hex, Verify_500mV_Low1},
            {Verify_500mV_Low2.Hex, Verify_500mV_Low2},
            {Verify_500mV_High1.Hex,Verify_500mV_High1},
            {Verify_500mV_High2.Hex,Verify_500mV_High2},

            {Verify_Temp.Hex,Verify_Temp},

            {Box_WritePage_00.Hex,Box_WritePage_00},
            {Box_WritePage_01.Hex,Box_WritePage_01},
            {Box_WritePage_12.Hex,Box_WritePage_12},
            {Box_WritePage_15.Hex,Box_WritePage_15},

            {Box_SensorCheckUpol_674.Hex,Box_SensorCheckUpol_674},
            {Box_SensorVerification.Hex,Box_SensorVerification},
            {Box_SensorError.Hex,Box_SensorError},
            {Box_SensorWriteCalData674.Hex,Box_SensorWriteCalData674},
            {Box_SensorWriteCalData500.Hex,Box_SensorWriteCalData500},
            {Box_StartSensorCalibration.Hex,Box_StartSensorCalibration},
            {SensorFail.Hex,SensorFail},
            {SensorCalibFinalise.Hex,SensorCalibFinalise},
            {Box_Calibration.Hex,Box_Calibration},

            {WEP_Test.Hex,WEP_Test},
            {WEP_674mV_Low_1.Hex,WEP_674mV_Low_1},
            {WEP_674mV_Low_2.Hex,WEP_674mV_Low_2},
            {WEP_500mV_Low_1.Hex,WEP_500mV_Low_1},
            {WEP_500mV_Low_2.Hex,WEP_500mV_Low_2},
            {WEP_674mV_High_1.Hex,WEP_674mV_High_1},
            {WEP_674mV_High_2.Hex,WEP_674mV_High_2},
            {WEP_500mV_High_1.Hex,WEP_500mV_High_1},
            {WEP_500mV_High_2.Hex,WEP_500mV_High_2},
            {WEP_SensorError.Hex,WEP_SensorError},
            {WEPSensorFail.Hex,WEPSensorFail},

            {SensorWepFinalise.Hex,SensorWepFinalise},

            {WEP_SensorCheckUpol.Hex,WEP_SensorCheckUpol},
            {WEP_TempCheck.Hex,WEP_TempCheck},

            {Box_Idle.Hex,Box_Idle},

            {Calib_674_Calc_Low.Hex,Calib_674_Calc_Low},
            {Calib_674Calc_High.Hex,Calib_674Calc_High},
            {Calib_500Calc_Low.Hex,Calib_500Calc_Low},
            {Calib_500Calc_High.Hex,Calib_500Calc_High},

            {SuccessfullSensorCalibration.Hex,SuccessfullSensorCalibration},
            {Box_SensorCheckUpol_500.Hex,Box_SensorCheckUpol_500},
            {ShowErrorValues.Hex,ShowErrorValues},

            {DebugUpolOnCathode.Hex, DebugUpolOnCathode},
            {DebugUpolOnAnode.Hex, DebugUpolOnAnode},
            {ReadPage16.Hex, ReadPage16},
            {RS232_OW_ACCESS.Hex, RS232_OW_ACCESS},

            {Verify_UPol.Hex,Verify_UPol},
            {Verify_PT1000_20.Hex,Verify_PT1000_20},
            {Verify_PT1000_30.Hex,Verify_PT1000_30},
            {Verify_UPol500mV.Hex,Verify_UPol500mV},
            {Verify_UPol674mV.Hex,Verify_UPol674mV},

            {NotDefined.Hex,NotDefined},

            {CalMode_674_500mV.Hex,CalMode_674_500mV},
            {CalMode_500mV.Hex,CalMode_500mV},
            {CalMode_674mV.Hex,CalMode_674mV},

            {FWversion.Hex,   FWversion},
            {BoxStatus.Hex,   BoxStatus},
            {BoxReset.Hex,   BoxReset},
            {BoxIDStatus.Hex, BoxIDStatus},
            {SensorStatus.Hex,SensorStatus}
        };

        #region DefSearcher
        /************************************************
         * FUNCTION:    Definitions Searcher
         * DESCRIPTION:
         ************************************************/
        public static implicit operator BoxMode(BoxModeCodes code)
        {
            return FromHex(code);
        }
        public static implicit operator BoxMode(string hex)
        {
            return FromHex(hex);
        }
        #endregion

        public static BoxMode FromHex(OpCode opCode)
        {
            switch (opCode)
            {
                case OpCode.S100:
                    return CalMode_674_500mV;
                case OpCode.S500:
                    return CalMode_500mV;
                case OpCode.S674:
                    return CalMode_674mV;
                default:
                    break;
            }
            return NotDefined;
        }

        public static BoxMode FromHex(BoxModeCodes code)
        {
            switch (code)
            {
                case BoxModeCodes.S100:
                    return CalMode_674_500mV;
                case BoxModeCodes.S500:
                    return CalMode_500mV;
                case BoxModeCodes.S674:
                    return CalMode_674mV;
                case BoxModeCodes.BoxIDStatus:
                    return BoxIDStatus;
                case BoxModeCodes.BoxStatus:
                    return BoxStatus;
                case BoxModeCodes.FWversion:
                    return FWversion;
                case BoxModeCodes.SensorStatus:
                    return SensorStatus;
                default:
                    try
                    {
                        var mode = BoxModeDic.Values.First(x => x.BoxModeCode == code);
                        return mode;
                    }
                    catch
                    {

                    }
                    return NotDefined;
            }
        }
        public static BoxMode FromHex(string hex)
        {
            if (BoxModeDic.TryGetValue(hex, out BoxMode mode) == false)
            {
                mode = new BoxMode(hex, NotDefined.Desc);
            }
            return mode;
        }
        public static BoxMode FromDesc(string desc)
        {
            foreach (var mode in BoxModeDic.Values)
            {
                if (mode.Desc == desc)
                {
                    return mode;
                }
            }
            return null;
        }

        public static string GetDesc(string hex)
        {
            var mode = FromHex(hex);
            return mode.Desc;
        }

        public static bool TryGetValue(string hex, out BoxMode value)
        {
            if (!BoxModeDic.TryGetValue(hex, out value))
            {
                value = NotDefined;
                return false;
            }
            return true;
        }
    }
}
