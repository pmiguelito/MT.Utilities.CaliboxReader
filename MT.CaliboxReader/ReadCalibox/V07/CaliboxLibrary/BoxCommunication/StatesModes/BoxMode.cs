using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliboxLibrary
{

    public enum BoxModeCodes
    {
        S100, S500, S674,
        FWversion, BoxStatus, BoxIDStatus, SensorStatus,
        hFF,
        h00,h01,h02,h03,h04,h05,        h08,h09, h0A, h0B, h0C, h0D, h0E, h0F,
        h10,h11,h12,h13,h14,h15,h16,h17,h18,h19, h1A, h1B, h1C, h1D, h1E, h1F,
        h20,h21,h22,h23,h24,h25,h26,h27,h28,h29, h2A, h2B,
                h32, h33, h34, h35, h36, h37, h38, h39, h3A, h3B, h3C, h3D, h3E, h3F,
        h40,h41,h42
    }
    public class BoxMode: BoxModeDetails
    {
        public readonly bool IsMeasValues;
        public readonly BoxModeCodes BoxModeCode;
        public readonly bool IsFinished;
        public BoxMode():base()
        {
        }
        internal BoxMode(string hex, string desc, bool isError = false, bool isFinished = false, bool isMeasValues = true) 
            :base(hex, desc, isError)
        {
            IsMeasValues = isMeasValues;
            IsFinished = isFinished;
            if (hex.Length == 2) { hex = "h" + hex; }
            Enum.TryParse(hex, out BoxModeCodes code);
            BoxModeCode = code;
        }
        public virtual string ToStringWithHeader()
        {
            return $"BoxModeHEX: {Hex}\tBoxModeDesc: {Desc}";
        }
        #region Definition
        /************************************************
         * FUNCTION:    Definitions
         * DESCRIPTION:
         ************************************************/
        public static readonly BoxMode Calib_674mV_Low1 = new BoxMode("00", "Calib_674mV_Low1");
        public static readonly BoxMode Calib_674mV_Low2 = new BoxMode("01", "Calib_674mV_Low2");
        public static readonly BoxMode Calib_674mV_High1 = new BoxMode("02", "Calib_674mV_High1");
        public static readonly BoxMode Calib_674mV_High2 = new BoxMode("03", "Calib_674mV_High2");

        public static readonly BoxMode Calib_500mV_Low1 = new BoxMode("04", "Calib_500mV_Low1");
        public static readonly BoxMode Calib_500mV_Low2 = new BoxMode("05", "Calib_500mV_Low2");
        public static readonly BoxMode Calib_500mV_High1 = new BoxMode("04", "Calib_500mV_High1");
        public static readonly BoxMode Calib_500mV_High2 = new BoxMode("05", "Calib_500mV_High2");

        public static readonly BoxMode Verify_674mV_Low1 = new BoxMode("08", "Verify_674mV_Low1");
        public static readonly BoxMode Verify_674mV_Low2 = new BoxMode("09", "Verify_674mV_Low2");
        public static readonly BoxMode Verify_674mV_High1 = new BoxMode("0A", "Verify_674mV_High1");
        public static readonly BoxMode Verify_674mV_High2 = new BoxMode("0B", "Verify_674mV_High2");


        public static readonly BoxMode Verify_500mV_Low1 = new BoxMode("0C", "Verify_500mV_Low1");
        public static readonly BoxMode Verify_500mV_Low2 = new BoxMode("0D", "Verify_500mV_Low2");
        public static readonly BoxMode Verify_500mV_High1 = new BoxMode("0E", "Verify_500mV_High1");
        public static readonly BoxMode Verify_500mV_High2 = new BoxMode("0F", "Verify_500mV_High2");

        public static readonly BoxMode Verify_Temp = new BoxMode("10", "Verify_Temp");

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

        public static readonly BoxMode Calib_674_Calc_Low = new BoxMode("33", "Calib_674_Calc_Low");
        public static readonly BoxMode Calib_674Calc_High = new BoxMode("34", "Calib_674Calc_High");
        public static readonly BoxMode Calib_500Calc_Low = new BoxMode("35", "Calib_500Calc_Low");
        public static readonly BoxMode Calib_500Calc_High = new BoxMode("36", "Calib_500Calc_High");
        
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
        public static readonly BoxMode BoxIDStatus = new BoxMode("BoxIDStatus", "BoxIDStatus", isMeasValues: false);
        public static readonly BoxMode SensorStatus = new BoxMode("SensorStatus", "SensorStatus", isMeasValues: false);
        #endregion

        /// <summary>
        /// KEY: HEX, Definition of the process state
        /// </summary>
        public static Dictionary<string, BoxMode> BoxModeDic = new Dictionary<string, BoxMode>(StringComparer.OrdinalIgnoreCase)
        {
            {"00",Calib_674mV_Low1},
            {"01",Calib_674mV_Low2},
            {"02",Calib_674mV_High1},
            {"03",Calib_674mV_High2},

            {"04",Calib_500mV_Low1},
            {"05",Calib_500mV_Low2},
            {"06",Calib_500mV_High1},
            {"07",Calib_500mV_High2},

            {"08",Verify_674mV_Low1},
            {"09",Verify_674mV_Low2},
            {"0A",Verify_674mV_High1},
            {"0B",Verify_674mV_High2},

            {"0C",Verify_500mV_Low1},
            {"0D",Verify_500mV_Low2},
            {"0E",Verify_500mV_High1},
            {"0F",Verify_500mV_High2},

            {"10",Verify_Temp},

            {"11",Box_WritePage_00},
            {"12",Box_WritePage_01},
            {"13",Box_WritePage_12},
            {"14",Box_WritePage_15},
            {"15",Box_SensorCheckUpol_674},
            {"16",Box_SensorVerification},
            {"17",Box_SensorError},
            {"18",Box_SensorWriteCalData674},
            {"19",Box_SensorWriteCalData500},
            {"1A",Box_StartSensorCalibration},
            {"1B",SensorFail},
            {"1C",SensorCalibFinalise},
            {"1D",Box_Calibration},

            {"1E",WEP_Test},
            {"1F",WEP_674mV_Low_1},
            {"20",WEP_674mV_Low_2},
            {"21",WEP_500mV_Low_1},
            {"22",WEP_500mV_Low_2},
            {"23",WEP_674mV_High_1},
            {"24",WEP_674mV_High_2},
            {"25",WEP_500mV_High_1},
            {"26",WEP_500mV_High_2},
            {"27",WEP_SensorError},
            {"28",WEPSensorFail},

            {"29",SensorWepFinalise},

            {"2A",WEP_SensorCheckUpol},
            {"2B",WEP_TempCheck},

            {"32",Box_Idle},

            {"33",Calib_674_Calc_Low},
            {"34",Calib_674Calc_High},
            {"35",Calib_500Calc_Low},
            {"36",Calib_500Calc_High},

            {"37",SuccessfullSensorCalibration},
            {"38",Box_SensorCheckUpol_500},
            {"39",ShowErrorValues},

            {"3A",DebugUpolOnCathode},
            {"3B",DebugUpolOnAnode},
            {"3C",ReadPage16},
            {"3D",RS232_OW_ACCESS},

            {"3E",Verify_UPol},
            {"3F",Verify_PT1000_20},
            {"40",Verify_PT1000_30},
            {"41",Verify_UPol500mV},
            {"42",Verify_UPol674mV},


            {"FF",NotDefined},

            {"S100",CalMode_674_500mV},
            {"S500",CalMode_500mV},
            {"S674",CalMode_674mV},
            {"FWversion",FWversion},
            {"BoxStatus",BoxStatus},
            {"BoxIDStatus",BoxIDStatus},
            {"SensorStatus",SensorStatus}
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
                    } catch
                    {

                    }
                    return NotDefined;
            }
        }
        public static BoxMode FromHex(string hex)
        {
            if(!BoxModeDic.TryGetValue(hex, out BoxMode mode))
            {
                mode = NotDefined;
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
