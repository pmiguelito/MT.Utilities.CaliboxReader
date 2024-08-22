using System;
using System.Collections.Generic;
using System.Data;

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
        public static readonly OpCodeResponse OpCodeResponse_g015 = new OpCodeResponse(OpCode.g015, OpcodeHeader_G015);
        public static readonly OpCodeResponse OpCodeResponse_g100 = new OpCodeResponse(OpCode.g100, OpcodeHeader_G100);
        public static readonly OpCodeResponse OpCodeResponse_g200 = new OpCodeResponse(OpCode.g200, OpcodeHeader_G200);
        public static readonly OpCodeResponse OpCodeResponse_g901 = new OpCodeResponse(OpCode.g901, OpcodeHeader_G901ext);
        public static readonly OpCodeResponse OpCodeResponse_g903 = new OpCodeResponse(OpCode.g903, OpcodeHeader_G901ext);
        public static readonly OpCodeResponse OpCodeResponse_g904 = new OpCodeResponse(OpCode.g904, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_g905 = new OpCodeResponse(OpCode.g905, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_g906 = new OpCodeResponse(OpCode.g906, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_g907 = new OpCodeResponse(OpCode.g907, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_g908 = new OpCodeResponse(OpCode.g908, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_g909 = new OpCodeResponse(OpCode.g909, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_g910 = new OpCodeResponse(OpCode.g910, OpcodeHeader_G910);
        public static readonly OpCodeResponse OpCodeResponse_g911 = new OpCodeResponse(OpCode.g911, OpcodeHeader_G911);
        public static readonly OpCodeResponse OpCodeResponse_s999 = new OpCodeResponse(OpCode.s999, OpcodeHeader_GXXX);
        public static readonly OpCodeResponse OpCodeResponse_ParseError = new OpCodeResponse(OpCode.parser_error, OpcodeHeader_GXXX);

        /// <summary>
        /// KEY: OpCode Request (UPPER), response: OpCode (Lower)
        /// </summary>
        private static readonly Dictionary<OpCode, OpCodeResponse> OpCodeDic = new Dictionary<OpCode, OpCodeResponse>()
        {
            { OpCode.G015, OpCodeResponse_g015 },
            { OpCode.g015, OpCodeResponse_g015 },
            { OpCode.G100, OpCodeResponse_g100 },
            { OpCode.g100, OpCodeResponse_g100 },
            { OpCode.G200, OpCodeResponse_g200 },
            { OpCode.g200, OpCodeResponse_g200 },
            { OpCode.G901, OpCodeResponse_g901 },
            { OpCode.g901, OpCodeResponse_g901 },
            { OpCode.G903, OpCodeResponse_g903 },
            { OpCode.g903, OpCodeResponse_g903 },
            { OpCode.G904, OpCodeResponse_g904 },
            { OpCode.g904, OpCodeResponse_g904 },
            { OpCode.G905, OpCodeResponse_g905 },
            { OpCode.g905, OpCodeResponse_g905 },
            { OpCode.G906, OpCodeResponse_g906 },
            { OpCode.g906, OpCodeResponse_g906 },
            { OpCode.G907, OpCodeResponse_g907 },
            { OpCode.g907, OpCodeResponse_g907 },
            { OpCode.G908, OpCodeResponse_g908 },
            { OpCode.g908, OpCodeResponse_g908 },
            { OpCode.G909, OpCodeResponse_g909 },
            { OpCode.g909, OpCodeResponse_g909 },
            { OpCode.G910, OpCodeResponse_g910 },
            { OpCode.g910, OpCodeResponse_g910 },
            { OpCode.G911, OpCodeResponse_g911 },
            { OpCode.g911, OpCodeResponse_g911 },
            { OpCode.S999, OpCodeResponse_s999 },
            { OpCode.s999, OpCodeResponse_s999 },
            { OpCode.Parser_error, OpCodeResponse_ParseError },
            { OpCode.parser_error, OpCodeResponse_ParseError },
        };

        public static OpCodeResponse GetOpCodeResponse(this string opcodeRequest)
        {
            var opcode = opcodeRequest.ParseOpcode().ToUpper();
            if (Enum.TryParse(opcode, out OpCode result))
            {
                if (OpCodeDic.TryGetValue(result, out OpCodeResponse value))
                {
                    return value.New();
                }
            }
            return OpCodeResponse_ParseError.New();
        }

        public static OpCodeResponse GetOpCodeResponse(this OpCode opcodeRequest)
        {
            if (OpCodeDic.TryGetValue(opcodeRequest, out OpCodeResponse value))
            {
                return value.New();
            }
            return OpCodeResponse_ParseError.New();
        }

        /************************************************************
         *  Opcodes
         *
         ************************************************************/

        private static string[] _OpCodesList;
        public static string[] OpCodesList
        {
            get
            {
                if (_OpCodesList == null)
                {
                    _OpCodesList = Enum.GetNames(typeof(OpCode));
                }
                return _OpCodesList;
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
                row[MN_BoxMode_HEX] = drv.BoxMode.Hex;
                row[MN_BoxMode_Desc] = drv.BoxMode.Desc;
            }
            var value = string.IsNullOrEmpty(drv.MeasValues.I_AVG) ? drv.BoxMeasValue : $"({drv.MeasValues.I_Set}) {drv.MeasValues.I_AVG}/{drv.MeasValues.I_StdDev}";
            row[MN_Values] = value;
            if (!string.IsNullOrEmpty(drv.BoxErrorMode?.Hex))
            {
                row[MN_BoxErrorCode_HEX] = drv.BoxErrorMode.Hex;
                row[MN_BoxErrorCode_Desc] = drv.BoxErrorMode.Desc;
            }
            row[MN_meas_time_end] = DateTime.Now;
            row[MN_Duration] = Calc_Duration(row);
            return row;
        }

        public static bool Set_Progress(ref DataTable dt, DeviceResponseValues drv)
        {
            if (IsProgress(drv?.BoxMode) == false)
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
        private static object _lockDTinsert = new object();
        private static DataRow GetRow(DataTable dt, DeviceResponseValues drv, out bool newRow)
        {
            lock (_lockDTinsert)
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
        }

        private static bool IsProgress(BoxMode boxModeHex)
        {
            return IsProgress(boxModeHex.Hex);
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

                        row[MN_BoxMode_HEX] = drv.BoxMode.Hex;
                        row[MN_BoxMode_Desc] = drv.BoxMode.Desc;

                        row[MN_I_set] = drv.MeasValues.Iset.ValueNumeric;
                        row[MN_I_AVG] = drv.MeasValues.Iavg?.ValueNumeric;
                        row[MN_I_StdDev] = drv.MeasValues.IstdDev?.ValueNumeric;
                        row[MN_I_Error] = drv.MeasValues.Ierror?.ValueNumeric;

                        row[MN_Temp_set] = drv.MeasValues.TempSet?.ValueNumeric;
                        row[MN_Temp_AVG] = drv.MeasValues.TempAvg?.ValueNumeric;
                        row[MN_Temp_Error] = drv.MeasValues.TempError?.ValueNumeric;
                        row[MN_Temp_StdDev] = drv.MeasValues.TempStdDev?.ValueNumeric;
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
    }
}
