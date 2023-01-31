using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliboxLibrary
{
    public static class BoxParse
    {
        /*******************************************************************************************************************
        * Names:
        *******************************************************************************************************************/
        public const string MN_StdDev = "StdDev";
        public const string MN_RefValue = "RefValue";
        public const string MN_RefValueSet = "RefValue_set";
        public const string MN_RefValueOK = "RefValue_ok";
        public const string MN_MeanValue = "MeanValue";
        public const string MN_MeanValueSet = "MeanValue_set";
        public const string MN_MeanValueOK = "MeanValue_ok";
        public const string MN_ErrorABS = "ErrorABS";

        /* DataBase copy*/
        public const string MN_Duration = "duration";
        public const string MN_Values = "values";
        public const string MN_Opcode = "opcode";
        public const string MN_FWversion = "FWversion";

        public const string MN_BoxMode_HEX = "BoxHEX";
        public const string MN_BoxMode_Desc = "BoxDesc";
        public const string MN_CalStatus_HEX = "CalStatusHEX";
        public const string MN_CalStatus_Desc = "CalStatusDesc";
        public const string MN_BoxErrorCode_HEX = "BoxErrorHEX";
        public const string MN_BoxErrorCode_Desc = "BoxErrorDesc";

        public const string MN_I = "I";
        public const string MN_Iraw = "I_Raw";
        public const string MN_Upol = "Upol";
        public const string MN_Upol_StdDev = "Upol_StdDev";
        public const string MN_Upol_AVG = "Upol_AVG";
        public const string MN_Upol_Error = "Upol_Error";
        public const string MN_UAnode = "UAnode";
        public const string MN_TempVolt = "TempVolt";
        public const string MN_MBrange = "MBrange";
        public const string MN_Temp = "Temp";

        public const string MN_I_AVG = "I_AVG";
        public const string MN_I_set = "I_Set";
        public const string MN_I_ok = "I_ok";

        public const string MN_I_StdDev = "I_StdDev";
        public const string MN_I_StdDev_set = "I_StdDev_set";
        public const string MN_I_StdDev_ok = "I_StdDev_ok";

        public const string MN_I_Error = "I_Error";
        public const string MN_I_Error_set = "I_Error_set";
        public const string MN_I_Error_ok = "I_Error_ok";

        public const string MN_Temp_set = "Temp_set";
        public const string MN_Temp_ok = "Temp_ok";
        public const string MN_Temp_AVG = "Temp_avg";

        public const string MN_Temp_StdDev = "Temp_StdDev";
        public const string MN_Temp_StdDev_set = "Temp_StdDev_set";
        public const string MN_Temp_StdDev_ok = "Temp_StdDev_ok";

        public const string MN_Temp_Error = "Temp_Error";
        public const string MN_Temp_Error_set = "Temp_Error_set";
        public const string MN_Temp_Error_ok = "Temp_Error_ok";

        /************************************************************
         *  Headers
         *  
         ************************************************************/
        public static readonly string[] OpcodeHeader_GXXX = new string[] { MN_Opcode, MN_Values };

        /// <summary>
        /// Read Sensor Page 15
        /// </summary>
        public static readonly string[] OpcodeHeader_G015 = new string[] { MN_Opcode, MN_Values, MN_FWversion };
        /// <summary>
        /// Read Box State
        /// </summary>
        public static readonly string[] OpcodeHeader_G100 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX };
        public static readonly string[] OpcodeHeader_G200 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_BoxErrorCode_HEX, MN_I_set, MN_I_AVG, MN_I_StdDev, MN_I_Error };
        /// <summary>
        /// Read Box state and Measurement values No Temperatur
        /// </summary>
        public static readonly string[] OpcodeHeader_G901 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_I_set, MN_I_AVG, MN_I_StdDev, MN_I_Error };
        /// <summary>
        /// Read Box state and Measurement values with Temperatur
        /// </summary>
        public static readonly string[] OpcodeHeader_G901ext = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_I_set, MN_I_AVG, MN_I_StdDev, MN_I_Error, MN_Temp_set, MN_Temp_AVG, MN_Temp_StdDev, MN_Temp_Error };
        public static readonly string[] OpcodeHeader_G906 = new string[] { MN_Opcode, MN_I, MN_Iraw, MN_Temp, MN_TempVolt, MN_Upol, MN_UAnode, MN_MBrange };

        public static readonly string[] OpcodeHeader_G910 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_Upol, MN_Upol_AVG, MN_Upol_StdDev, MN_Upol_Error };
        public static readonly string[] OpcodeHeader_G911 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_Temp, MN_Temp_AVG, MN_Temp_StdDev, MN_Temp_Error };

        /************************************************
         * FUNCTION:    OpCode Dictionary
         * DESCRIPTION:
         ************************************************/
        public static OpCodeResponse ParserError { get; private set; } = new OpCodeResponse() { OpCode = OpCode.parser_error, Header = OpcodeHeader_GXXX };

        /// <summary>
        /// KEY: OpCode Request (UPPER), response: OpCode (Lower)
        /// </summary>
        public static Dictionary<OpCode, OpCodeResponse> OpCodeDic = new Dictionary<OpCode, OpCodeResponse>()
        {
            { OpCode.G015, new OpCodeResponse(){ OpCode = OpCode.g015, Header = OpcodeHeader_G015} },
            { OpCode.G100, new OpCodeResponse(){ OpCode = OpCode.g100, Header = OpcodeHeader_G100} },
            { OpCode.G200, new OpCodeResponse(){ OpCode = OpCode.g200, Header = OpcodeHeader_G200} },

            { OpCode.G901, new OpCodeResponse(){ OpCode = OpCode.g901, Header = OpcodeHeader_G901ext} },

            { OpCode.G903, new OpCodeResponse(){ OpCode = OpCode.g903, Header = OpcodeHeader_G901ext} },
            { OpCode.G904, new OpCodeResponse(){ OpCode = OpCode.g904, Header = OpcodeHeader_GXXX} },
            { OpCode.G905, new OpCodeResponse(){ OpCode = OpCode.g905, Header = OpcodeHeader_GXXX} },
            { OpCode.G906, new OpCodeResponse(){ OpCode = OpCode.g906, Header = OpcodeHeader_GXXX} },
            { OpCode.G907, new OpCodeResponse(){ OpCode = OpCode.g907, Header = OpcodeHeader_GXXX} },
            { OpCode.G908, new OpCodeResponse(){ OpCode = OpCode.g908, Header = OpcodeHeader_GXXX} },
            { OpCode.G909, new OpCodeResponse(){ OpCode = OpCode.g909, Header = OpcodeHeader_GXXX} },

            { OpCode.G910, new OpCodeResponse(){ OpCode = OpCode.g910, Header = OpcodeHeader_G910} },
            { OpCode.G911, new OpCodeResponse(){ OpCode = OpCode.g911, Header = OpcodeHeader_G911} },

            { OpCode.S999, new OpCodeResponse(){ OpCode = OpCode.s999, Header = OpcodeHeader_GXXX} },

            { OpCode.parser_error, ParserError },
        };

        public static OpCodeResponse GetOpCodeResponse(this string opcodeRequest)
        {
            var opcode = opcodeRequest.ParseOpcode().ToUpper();
            if (Enum.TryParse(opcode, out OpCode result))
            {
                if (OpCodeDic.TryGetValue(result, out OpCodeResponse value))
                {
                    return value;
                }
            }
            return ParserError;
        }

        /************************************************************
         *  Opcodes
         *  
         ************************************************************/

        private static string[] _OpcodesList;
        public static string[] OpcodesList
        {
            get
            {
                if (_OpcodesList == null)
                {
                    _OpcodesList = Enum.GetNames(typeof(OpCode));
                }
                return _OpcodesList;
            }
        }

        public static OpCode ParseOpcode(this string opcode, bool toLower = false, bool toUpper = false, OpCode defaultOpcode = OpCode.parser_error)
        {
            opcode = opcode.ParseOpcode();
            if (toLower) { opcode = opcode.ToLower(); }
            else if (toUpper) { opcode = opcode.ToUpper(); }
            if (Enum.TryParse(opcode, out OpCode result))
            {
                return result;
            }
            return defaultOpcode;
        }
        private static string ParseOpcode(this string opcode)
        {
            opcode = opcode.Replace("#", "").Trim();
            if (opcode.Contains(" "))
            {
                opcode = opcode.Substring(0, opcode.IndexOf(" "));
            }
            if (opcode.Contains("\t"))
            {
                opcode = opcode.Substring(0, opcode.IndexOf("\t"));
            }
            switch (opcode.ToUpper())
            {
                case "BOXRESET":
                case "DIPSWITCH":
                case "CALIBRATIONBOX":
                case "BETRIEBSMITTEL-NR.:":
                case "FW:":
                case "MESSBEREICH":
                case "RAWVAL":
                case "CURRENT":
                case "ERRORRAW":
                case "CALERROR":
                case "WEPERROR":
                case "STDDEV":
                case "CS10CALC:":
                case "CS31CALC:":
                case "ALL":
                    opcode = "S999";
                    break;
                default:
                    break;
            }
            return opcode;
        }

        /*******************************************************************************************************************
        * Box States:
        * if OpcodeHeader values change, clDeviceCom.Parse_Values switch update
        '*******************************************************************************************************************/
        ///// <summary>
        ///// Definition of process state
        ///// </summary>
        //[Obsolete("Use BoxMode class", true)]
        //public static Dictionary<string, string> BoxMode = new Dictionary<string, string>
        //{
        //    {"00","Calib_674mV_Low1"},
        //    {"01","Calib_674mV_Low2"},
        //    {"02","Calib_674mV_High1"},
        //    {"03","Calib_674mV_High2"},

        //    {"04","Calib_500mV_Low1"},
        //    {"05","Calib_500mV_Low2"},
        //    {"06","Calib_500mV_High1"},
        //    {"07","Calib_500mV_High2"},

        //    {"08","Verify_674mV_Low1"},
        //    {"09","Verify_674mV_Low2"},
        //    {"0A","Verify_674mV_High1"},
        //    {"0B","Verify_674mV_High2"},

        //    {"0C","Verify_500mV_Low1"},
        //    {"0D","Verify_500mV_Low2"},
        //    {"0E","Verify_500mV_High1"},
        //    {"0F","Verify_500mV_High2"},

        //    {"10","Verify_Temp"},

        //    {"11","Box_WritePage_00"},
        //    {"12","Box_WritePage_01"},
        //    {"13","Box_WritePage_12"},
        //    {"14","Box_WritePage_15"},
        //    {"15","Box_SensorCheckUpol_674"},
        //    {"16","Box_SensorVerification"},
        //    {"17","Box_SensorError"},
        //    {"18","Box_SensorWriteCalData674"},
        //    {"19","Box_SensorWriteCalData500"},
        //    {"1A","Box_StartSensorCalibration"},
        //    {"1B","SensorFail"},
        //    {"1C","SensorCalibFinalise"},
        //    {"1D","Box_Calibration"},

        //    {"1E","WEP_Test"},
        //    {"1F","WEP_674mV_Low_1"},
        //    {"20","WEP_674mV_Low_2"},
        //    {"21","WEP_500mV_Low_1"},
        //    {"22","WEP_500mV_Low_2"},
        //    {"23","WEP_674mV_High_1"},
        //    {"24","WEP_674mV_High_2"},
        //    {"25","WEP_500mV_High_1"},
        //    {"26","WEP_500mV_High_2"},
        //    {"27","WEP_SensorError"},
        //    {"28","WEPSensorFail"},

        //    {"29","SensorWepFinalise"},

        //    {"2A","WEP_SensorCheckUpol"},
        //    {"2B","WEP_TempCheck"},

        //    {"32","Box_Idle"},

        //    {"33","Calib_674_Calc_Low"},
        //    {"34","Calib_674Calc_High"},
        //    {"35","Calib_500Calc_Low"},
        //    {"36","Calib_500Calc_High"},

        //    {"37","SuccessfullSensorCalibration"},
        //    {"38","Box_SensorCheckUpol_500"},
        //    {"39","ShowErrorValues"},

        //    {"3A","DebugUpolOnCathode"},
        //    {"3B","DebugUpolOnAnode"},
        //    {"3C","ReadPage16"},
        //    {"3D","RS232_OW_ACCESS"},

        //    {"3E","Verify_UPol"},
        //    {"3F","Verify_PT1000_20"},
        //    {"40","Verify_PT1000_30"},
        //    {"41","Verify_UPol500mV"},
        //    {"42","Verify_UPol674mV"},


        //    {"FF","NotDefined"},

        //    {"S100","CalMode 674/500mV"},
        //    {"S500","CalMode 500mV"},
        //    {"S674","CalMode 674mV"},
        //    {"FWversion","FWversion"},
        //    {"BoxStatus","BoxStatus"},
        //    {"SensorStatus","SensorStatus"}
        //};

        //[Obsolete("Use BoxMode", true)]
        //public static string Get_BoxMode_Desc(string boxmode_hex)
        //{
        //    if (!string.IsNullOrEmpty(boxmode_hex))
        //    {
        //        BoxMode.TryGetValue(boxmode_hex, out BoxMode value);
        //        return value.Desc;
        //    }
        //    return "";
        //}

        /// <summary>
        /// Errors
        /// </summary>
        public static Dictionary<string, string> BoxErrorCode { get; private set; } = new Dictionary<string, string>
        {
            {"0","NotChecked" },
            {"1","ERROR" },
            {"2","PASS" },
            {"00","NoError" },
            {"01","Standard Deviation was out of range (Noisy Signal)" },
            {"02","Calculated Mean was out of range (Offset Error)" },
            {"03","Standard Deviation & Calculated Mean were out of range" },
            {"04","Timeout" },
            {"08","Temperature Error" }
        };

        public static string Get_BoxErrorCode_Desc(string boxErrorCode_hex)
        {
            if (!string.IsNullOrEmpty(boxErrorCode_hex))
            {
                BoxErrorCode.TryGetValue(boxErrorCode_hex, out string value);
                return value;
            }
            return "";
        }

        /// <summary>
        /// Calibration Types
        /// </summary>
        public static Dictionary<string, string> CalibrationStatus = new Dictionary<string, string>
        {
            {"00","674mV and 500mV" },
            {"01","674mV" },
            {"02","500mV" },
            {"FF","NotDefined"}
        };

        public static string Get_BoxCalStatus_Desc(string BoxCalStatus_hex)
        {
            if (!string.IsNullOrEmpty(BoxCalStatus_hex))
            {
                CalibrationStatus.TryGetValue(BoxCalStatus_hex, out string value);
                return value;
            }
            return "";
        }

        #region DT_Progress
        /************************************************
         * FUNCTION:    DataTable Progress
         * DESCRIPTION:
         ************************************************/
        public const string MN_ID = "ID";
        public const string MN_Test_ok = "test_ok";
        public const string MN_meas_time_start = "meas_time_start";
        public const string MN_meas_time_end = "meas_time_end";
        public const string MN_meas_time = "meas_time";
        public const string MN_pass_no = "pass_no";
        public const string MN_item = "item";
        public const string MN_pdno = "pdno";
        public const string MN_Channelno = "channel_no";
        public const string MN_sensorid = "sensor_id";
        public const string MN_ProcOrder = "ProcOrder";
        public const string MN_EK_SWversion = "EK_SW_Version";
        public const string MN_Sample_FWversion = "sample_FW_Version";
        public const string MN_Sample_FWversion_state = "sample_FW_Version_state";
        public const string MN_Device_FWversion = "device_FW_Version";

        private static string[] _ProgHeaderArray;
        public static string[] ProgHeaderArray
        {
            get
            {
                if (_ProgHeaderArray == null)
                {
                    _ProgHeaderArray = new string[]
                    {
                        MN_BoxMode_HEX, MN_BoxMode_Desc, MN_meas_time_start, MN_meas_time_end,
                        MN_Duration, MN_Values, MN_BoxErrorCode_HEX, MN_BoxErrorCode_Desc
                    };

                    //_ProgHeaderArray = Enum.GetNames(typeof(ProgHeader));
                }
                return _ProgHeaderArray;
            }
        }
        public static DataTable DT_InitProgress(this DataTable dt, string tablename = "Progress")
        {
            if (dt == null)
            {
                dt = new DataTable() { TableName = tablename };
                foreach (string header in ProgHeaderArray)
                {
                    dt.Columns.Add(header);
                }
            }
            else
            {
                dt.Rows.Clear();
            }
            return dt;
        }

        private static string ProgHeaderSearcher { get { return MN_BoxMode_HEX; } }
        private static DataRow Set_ProgressValues(ref DataTable dt, DataRow row, DeviceResponseValues drv, bool newRow)
        {
            if (newRow)
            {
                row[MN_meas_time_start] = DateTime.Now;
                row[MN_BoxMode_HEX] = drv.BoxMode_hex;
                row[MN_BoxMode_Desc] = drv.BoxMode_desc;
            }
            var value = string.IsNullOrEmpty(drv.I_AVG) ? drv.BoxMeasValue : $"({drv.I_Set}) {drv.I_AVG}/{drv.I_StdDev}";
            row[MN_Values] = value;
            if (!string.IsNullOrEmpty(drv.BoxErrorCode_hex))
            {
                row[MN_BoxErrorCode_HEX] = drv.BoxErrorCode_hex;
                row[MN_BoxErrorCode_Desc] = drv.BoxErrorCode_desc;
            }
            row[MN_meas_time_end] = DateTime.Now;
            row[MN_Duration] = Calc_Duration(row);
            return row;
        }
        public static bool Set_Progress(ref DataTable dt, DeviceResponseValues drv)
        {
            if (!IsProgress(drv?.BoxMode_hex))
            {
                return false;
            }
            try
            {
                DataRow row = GetRow(dt, drv, out bool newRow);
                row = Set_ProgressValues(ref dt, row, drv, newRow);
                return true;
            }
            catch (Exception ex)
            {
                //H_Log.Save(Channel, ex);
            }
            return false;
        }

        private static DataRow GetRow(DataTable dt, DeviceResponseValues drv, out bool newRow)
        {
            DataRow[] rows = dt.Select($"{ProgHeaderSearcher} = '{drv.BoxMode_hex}'");
            if (rows != null && rows.Length > 0)
            {
                newRow = false;
                return rows[0];
            }
            newRow = true;
            var row = dt.NewRow();
            dt.Rows.Add(row);
            return row;
        }

        private static bool IsProgress(string boxModeHex)
        {
            switch (boxModeHex)
            {
                case null:
                case "":
                case "16":
                case "17":
                case "32":
                case "33":
                case "34":
                case "35":
                case "36":
                case "1C":
                    return false;
                default:
                    return true;
            }
        }
        public static void Set_Progress(ref DataTable dt, string boxmode_hex, string boxmode_desc, string value = null, string errorcode = null, bool lastValue = false)
        {
            string searchValue = boxmode_hex;
            if (!IsProgress(boxmode_hex)) { return; }
            if (!string.IsNullOrEmpty(searchValue))
            {
                DataRow row = dt.NewRow();
                DataRow[] rows = dt.Select($"{ProgHeaderSearcher} = '{searchValue}'");
                bool newRow = rows.Length <= 0;
                if (newRow)
                {
                    row[MN_BoxMode_HEX] = boxmode_hex;
                    if (boxmode_hex == boxmode_desc)
                    {
                        row[MN_BoxMode_Desc] = boxmode_desc;
                    }
                    else
                    {
                        row[MN_BoxMode_Desc] = BoxMode.GetDesc(boxmode_hex);
                    }
                    row[MN_meas_time_start] = DateTime.Now;
                    if (!string.IsNullOrEmpty(value))
                    {
                        row[MN_Values] = value;
                    }
                    if (!string.IsNullOrEmpty(errorcode))
                    {
                        row[MN_BoxErrorCode_HEX] = errorcode;
                        row[MN_BoxErrorCode_Desc] = BoxErrorCode[errorcode];
                    }
                    dt.Rows.Add(row);
                }
                else
                //else if (value != null || errorcode != null)
                {
                    int index = dt.Rows.IndexOf(rows[0]);
                    row = dt.Rows[index];
                    row[MN_meas_time_end] = DateTime.Now;
                    row[MN_Duration] = Calc_Duration(row);
                    if (!string.IsNullOrEmpty(value))
                    {
                        row[MN_Values] = value;
                    }
                    if (!string.IsNullOrEmpty(errorcode))
                    {
                        row[MN_BoxErrorCode_HEX] = errorcode;
                        row[MN_BoxErrorCode_Desc] = BoxErrorCode[errorcode];
                    }
                }
            }
            //Channel.Update_DGVprogress();
        }
        private static string Calc_Duration(DataRow row)
        {
            var time = row[MN_meas_time_start];
            DateTime t = DateTime.Now.AddSeconds(-2);
            if (time != null)
            {
                DateTime.TryParse(time.ToString(), out t);
                //t = time.ToString().ConverDateTime(DateTimeFormatSQL);
            }
            if (t.Year == 1) { t = DateTime.Now.AddSeconds(-2); }
            double d = (DateTime.Now - t).TotalSeconds;
            string dur = d.ToString("00.00");
            return dur;
        }
        #endregion

        #region DT_Measurement
        /************************************************
         * FUNCTION:    DataTable Measurement
         * DESCRIPTION:
         ************************************************/
        private static string[] _MeasHeaderArray;
        public static string[] MeasHeaderArray
        {
            get
            {
                if (_MeasHeaderArray == null)
                {
                    _MeasHeaderArray = new string[]
                    {
                        MN_ID, MN_meas_time_start, MN_Test_ok, MN_BoxMode_HEX, MN_BoxMode_Desc,
                        MN_I_set, MN_I_AVG, MN_I_ok,
                        MN_I_StdDev_set, MN_I_StdDev,MN_I_StdDev_ok,
                        MN_I_Error_set, MN_I_Error, MN_I_Error_ok,
                        MN_Temp_set, MN_Temp_AVG, MN_Temp_ok,
                        MN_Temp_StdDev_set,MN_Temp_StdDev,MN_Temp_StdDev_ok,
                        MN_Temp_Error_set,MN_Temp_Error,MN_Temp_Error_ok
                    };
                }
                return _MeasHeaderArray;
            }
        }
        public static DataTable DT_InitMeasurement()
        {
            DataTable dt = new DataTable("Measurement");
            try
            {
                foreach (string cl in MeasHeaderArray)
                {
                    var ch = dt.Columns.Add(cl);
                    switch (cl)
                    {
                        case MN_ID:
                            ch.DataType = typeof(long);
                            ch.AutoIncrement = true;
                            ch.AutoIncrementSeed = 0;
                            ch.AutoIncrementStep = 1;
                            break;
                        //case MN_RefValueSet:
                        //case MN_MeanValueSet:
                        //case MN_MeanValueOK:
                        //case "refvalue_set":
                        //case "meanvalue_set":
                        //case "meanvalue_ok":
                        //    ch.ColumnMapping = MappingType.Hidden;
                        //    break;
                        default:
                            break;
                    }
                }
            }
            catch
            {

            }
            return dt;
        }
        public static bool Insert_DT_Measurement(DataTable dt, DeviceResponseValues drv, DeviceLimitsResults limits)
        {
            //if (drv.OpCode == OpCode.g901)
            //if (drv.IsMeasValues)
            {
                //if (drv.BoxMode_desc.ToLower().StartsWith("cal") || drv.BoxMode_desc.ToLower().StartsWith("ver") || drv.BoxMode_hex == "3E")
                {
                    try
                    {
                        DataRow row = dt.NewRow();
                        row[MN_meas_time_start] = drv.meas_time_start;

                        row[MN_BoxMode_HEX] = drv.BoxMode_hex;
                        row[MN_BoxMode_Desc] = drv.BoxMode_desc;

                        row[MN_I_set] = drv.Iset.ValueNumeric;
                        row[MN_I_AVG] = drv.Iavg?.ValueNumeric;
                        row[MN_I_StdDev] = drv.IstdDev?.ValueNumeric;
                        row[MN_I_Error] = drv.Ierror?.ValueNumeric;

                        row[MN_Temp_set] = drv.TempSet?.ValueNumeric;
                        row[MN_Temp_AVG] = drv.TempAvg?.ValueNumeric;
                        row[MN_Temp_Error] = drv.TempError?.ValueNumeric;
                        row[MN_Temp_StdDev] = drv.TempStdDev?.ValueNumeric;
                        if (limits != null)
                        {
                            row[MN_Test_ok] = limits.Rating.Test_ok;
                            row[MN_I_ok] = limits.Rating.ValueAVG_ok;

                            row[MN_I_StdDev_set] = limits.Rating.StdDevSet;
                            row[MN_I_StdDev_ok] = limits.Rating.StdDev_ok;

                            row[MN_I_Error_set] = limits.Rating.ErrorSet;
                            row[MN_I_Error_ok] = limits.Rating.ErrorABS_ok;
                        }
                        dt.Rows.Add(row);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //H_Log.Save(Channel, ex);
                    }
                }
            }
            return false;
        }
        #endregion

        //#region Parser
        ///************************************************
        // * FUNCTION:    Parse Box Responses
        // * DESCRIPTION:
        // ************************************************/
        //public static bool Parse(this DeviceResponseValues values, string data)
        //{
        //    if(values == null || data == null) { return false; }
        //    values.meas_time_start = DateTime.Now;
        //    values.Response = data;


        //    return false;
        //}
        //#endregion
    }
}
