using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TT_Item_Infos;
using static STDhelper.clSTD;
using CaliboxLibrary;

namespace ReadCalibox
{
    public class Handler : CaliboxLibrary.Handler
    {
        /*******************************************************************************************************************
        * AdminModus:
        '*******************************************************************************************************************/
        private static string _MachineName;
        public static string MachineName
        {
            get
            {
                if (_MachineName == null)
                {
                    try { _MachineName = System.Environment.MachineName.ToUpper(); } catch { _MachineName = ""; }
                }
                return _MachineName;
            }
        }

        public static bool H_IsDebugModus { get; set; } = false;
        public static bool H_AdminModus { get; set; } = false;

        private static bool _H_TestRunning = false;
        public static bool H_TestRunning
        {
            get { return _H_TestRunning; }
            set
            {
                _H_TestRunning = value;
                Frm_Main.TestRunning_Change(value);
            }
        }

        /// <summary>
        /// channelNo, state
        /// </summary>
        public static Dictionary<int, CH_State> H_TestRunningStates = new Dictionary<int, CH_State>();

        public static bool RunningState_InWork()
        {
            foreach (var item in H_TestRunningStates)
            {
                if (item.Value == CH_State.inWork)
                {
                    H_TestRunning = true;
                    return true;
                }
            }
            H_TestRunning = false;
            return false;
        }

        public static void RunningState_ADD(int chNo, CH_State state)
        {
            if (!H_TestRunningStates.ContainsKey(chNo))
            {
                H_TestRunningStates.Add(chNo, state);
            }
        }

        public static void RunningState_Change(int chNo, CH_State state)
        {
            H_TestRunningStates[chNo] = state;
            RunningState_InWork();
        }

        public static int H_ProgShowRows = 3;
        public static void Progress_AdminModus(DataGridView dgv, bool showAllInfos = false)
        {
            bool colVisible = false;

            //if (H_AdminModus)
            if (showAllInfos)
            {
                dgv.ScrollBars = ScrollBars.Both;
                dgv.Enabled = true;
                colVisible = true;
                H_ProgShowRows = 2;
            }
            else
            {
                dgv.ScrollBars = ScrollBars.Vertical;
                dgv.Enabled = true;
                H_ProgShowRows = 3;
            }
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                dgvc.Visible = colVisible;
                dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            if (!showAllInfos)
            {
                dgv.Columns[MN_BoxMode_Desc].Visible = true;
                //dgv.Columns[MN_BoxMode_Desc].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["values"].Visible = true;
                dgv.Columns["values"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public static UC_TT_Item_Infos H_TT { get; set; }
        public static UC_Betrieb H_UC_Betrieb { get; set; }
        public static UC_Config_Main H_UC_Config { get; set; }

        //public static string H_FWversion { get; set; }
        /*******************************************************************************************************************
        * Names:
        '*******************************************************************************************************************/
        //public const string MN_StdDev = "StdDev";
        //public const string MN_RefValue = "RefValue";
        //public const string MN_RefValueSet = "RefValue_set";
        //public const string MN_RefValueOK = "RefValue_ok";
        //public const string MN_MeanValue = "MeanValue";
        //public const string MN_MeanValueSet = "MeanValue_set";
        //public const string MN_MeanValueOK = "MeanValue_ok";
        //public const string MN_ErrorABS = "ErrorABS";

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

        public const string MN_Duration = "duration";
        public const string MN_Values = "values";
        public const string MN_Opcode = "opcode";
        public const string MN_FWversion = "FWversion";
        public const string MN_BoxMode_HEX = "BoxHEX";
        public const string MN_BoxMode_Desc = "BoxDesc";
        public const string MN_CalStatus_HEX = "CalStatusHEX";
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


        /*******************************************************************************************************************
        * DataTables:
        '*******************************************************************************************************************/
        public const string DTname_Meas = "Measurement";
        public const string DTname_Prog = "Progress";
        public const string DTname_Limits = "Limits";
        public const string DTname_Cal = "tCalMeasVal";

        //public enum ProgHeader
        //{
        //    boxmode_hex, boxmode_desc, meas_time_start, meas_time_end,
        //    duration, values, boxerror_hex, boxerror_desc
        //};

        //private static string[] _ProgHeaderArray;
        //[Obsolete("Use Library", true)]
        //public static string[] ProgHeaderArray
        //{
        //    get
        //    {
        //        if(_ProgHeaderArray == null)
        //        {
        //            _ProgHeaderArray = new string[]
        //            {
        //                MN_BoxMode_HEX, MN_BoxMode_Desc, MN_meas_time_start, MN_meas_time_end,
        //                MN_Duration, MN_Values, MN_BoxErrorCode_HEX, MN_BoxErrorCode_Desc
        //            };

        //            //_ProgHeaderArray = Enum.GetNames(typeof(ProgHeader));
        //        }
        //        return _ProgHeaderArray;
        //    }
        //}

        //[Obsolete("Use Library", true)]
        //public static DataTable DT_ProgressColumnsAndClearRows(DataTable dt, string tablename = DTname_Prog)
        //{
        //    if (dt == null)
        //    {
        //        dt = new DataTable() { TableName = tablename };
        //        foreach (string header in ProgHeaderArray)
        //        {
        //            dt.Columns.Add(header);
        //        }
        //    }
        //    else
        //    { dt.Rows.Clear(); }
        //    return dt;
        //}

        //public enum MeasHeader
        //{
        //    ID, meas_time_start, test_ok, boxmode_hex, boxmode_desc,
        //    RefValue, RefValue_Set, RefValue_ok,
        //    MeanValue, MeanValue_set, MeanValue_ok,
        //    stddeviation, stddeviation_set, stddeviation_ok,
        //    errorvalue, errorvalue_set, errorvalue_ok
        //};

        //private static string[] _MeasHeaderArray;
        //public static string[] MeasHeaderArray
        //{
        //    get
        //    {
        //        if (_MeasHeaderArray == null)
        //        {
        //            _MeasHeaderArray = new string[]
        //            {
        //                MN_ID, MN_meas_time_start, MN_Test_ok, MN_BoxMode_HEX,MN_BoxMode_Desc,
        //                MN_I_set, MN_I_AVG, MN_I_ok,
        //                MN_I_StdDev_set, MN_I_StdDev,MN_I_StdDev_ok,
        //                MN_I_Error_set, MN_I_Error, MN_I_Error_ok,
        //                MN_Temp_set, MN_Temp_AVG, MN_Temp_ok,
        //                MN_Temp_StdDev_set,MN_Temp_StdDev,MN_Temp_StdDev_ok,
        //                MN_Temp_Error_set,MN_Temp_Error,MN_Temp_Error_ok
        //            };
        //            //_MeasHeaderArray = Enum.GetNames(typeof(MeasHeader));
        //        }
        //        return _MeasHeaderArray;
        //    }
        //}

        //public static DataTable Init_DT_Measurement()
        //{

        //    DataTable dt = new DataTable(DTname_Meas);
        //    try
        //    {
        //        foreach (string cl in MeasHeaderArray)
        //        {
        //            var ch = dt.Columns.Add(cl);
        //            switch (cl)
        //            {
        //                case MN_ID:
        //                    ch.DataType = typeof(long);
        //                    ch.AutoIncrement = true;
        //                    ch.AutoIncrementSeed = 0;
        //                    ch.AutoIncrementStep = 1;
        //                    break;
        //                //case MN_RefValueSet:
        //                //case MN_MeanValueSet:
        //                //case MN_MeanValueOK:
        //                //case "refvalue_set":
        //                //case "meanvalue_set":
        //                //case "meanvalue_ok":
        //                //    ch.ColumnMapping = MappingType.Hidden;
        //                //    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return dt;
        //}

        /*******************************************************************************************************************
        * Fonts:
        '*******************************************************************************************************************/

        public static void AllocFont(Control c, float size = 8, FontStyle fontStyle = FontStyle.Regular)
        {
            Fonts.AllocFont(c, size, fontStyle);
        }

        /*******************************************************************************************************************
        * Processes:
        *               timStateEngine is placed on "UC_Channel"
        '*******************************************************************************************************************/
        //public enum gProcMain
        //{
        //    getReadyForTest,
        //    BoxStatus,
        //    BoxIdentification,
        //    BoxReset,
        //    FWcheck,
        //    SensorCheck,
        //    TestFinished,
        //    StartProcess,
        //    Calibration,
        //    Bewertung,
        //    DBinit,
        //    wait,
        //    stop_stateEngine,
        //    error,
        //    idle
        //}




        //public enum CH_State { idle, inWork, QualityGood, QualityBad, active, notActive, error }

        ///*******************************************************************************************************************
        //* Box States:
        //* if OpcodeHeader values change, clDeviceCom.Parse_Values switch updaten
        //'*******************************************************************************************************************/
        //public static readonly string[] OpcodeHeader_G200 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_BoxErrorCode_HEX, MN_I_set, MN_I_AVG, MN_I_StdDev, MN_I_Error };

        //public static readonly string[] OpcodeHeader_GXXX = new string[] { MN_Opcode, MN_Values };

        ///// <summary>
        ///// Read Sensor Page 15
        ///// </summary>
        //public static readonly string[] OpcodeHeader_G015 = new string[] { MN_Opcode, MN_Values, MN_FWversion };
        ///// <summary>
        ///// Read Box State
        ///// </summary>
        //public static readonly string[] OpcodeHeader_G100 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX };
        ///// <summary>
        ///// Read Box state and Measurement values No Temperatur
        ///// </summary>
        //public static readonly string[] OpcodeHeader_G901 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_I_set, MN_I_AVG, MN_I_StdDev, MN_I_Error };
        ///// <summary>
        ///// Read Box state and Measurement values with Temperatur
        ///// </summary>
        //public static readonly string[] OpcodeHeader_G901ext = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_I_set, MN_I_AVG, MN_I_StdDev, MN_I_Error, MN_Temp_set, MN_Temp_AVG, MN_Temp_StdDev,MN_Temp_Error };
        //public static readonly string[] OpcodeHeader_G906 = new string[] { MN_Opcode, MN_I, MN_Iraw, MN_Temp, MN_TempVolt, MN_Upol, MN_UAnode, MN_MBrange };

        //public static readonly string[] OpcodeHeader_G910 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_Upol, MN_Upol_AVG, MN_Upol_StdDev, MN_Upol_Error };
        //public static readonly string[] OpcodeHeader_G911 = new string[] { MN_Opcode, MN_BoxMode_HEX, MN_CalStatus_HEX, MN_Temp, MN_Temp_AVG, MN_Temp_StdDev, MN_Temp_Error };


        //public static Dictionary<string, string> BoxMode = new Dictionary<string, string>
        //{
        //    {"00","CalibMode_674mV_Low_1"},
        //    {"01","CalibMode_674mV_Low_2"},
        //    {"02","CalibMode_674mV_High_1"},
        //    {"03","CalibMode_674mV_High_2"},

        //    {"04","CalibMode_500mV_Low_1"},
        //    {"05","CalibMode_500mV_Low_2"},
        //    {"06","CalibMode_500mV_High_1"},
        //    {"07","CalibMode_500mV_High_2"},

        //    {"08","VerifyMode_674mV_Low_1"},
        //    {"09","VerifyMode_674mV_Low_2"},
        //    {"0A","VerifyMode_674mV_High_1"},
        //    {"0B","VerifyMode_674mV_High_2"},

        //    {"0C","VerifyMode_500mV_Low_1"},
        //    {"0D","VerifyMode_500mV_Low_2"},
        //    {"0E","VerifyMode_500mV_High_1"},
        //    {"0F","VerifyMode_500mV_High_2"},

        //    {"10","VerifyTemp"},

        //    {"32","Box_Idle"},

        //    {"33","CalibMode_674CalculationLow"},
        //    {"34","CalibMode_674CalculationHigh"},
        //    {"35","CalibMode_500CalculationLow"},
        //    {"36","CalibMode_500CalculationHigh"},

        //    {"37","SuccessfullSensorCalibration"},
        //    {"38","Box_SensorCheckUpol_500"},
        //    {"39","ShowErrorValues"},

        //    {"3A","DebugUpolOnCathode"},
        //    {"3B","DebugUpolOnAnode"},
        //    {"3C","ReadPage16"},
        //    {"3D","RS232_OW_ACCESS"},

        //    {"3E","CheckUPol"},



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

        //    {"FF","NotDefined"},

        //    {"S100","CalMode 674/500mV"},
        //    {"S500","CalMode 500mV"},
        //    {"S674","CalMode 674mV"},
        //    {"FWversion","FWversion"},
        //    {"BoxStatus","BoxStatus"},
        //    {"SensorStatus","SensorStatus"}
        //};

        //[Obsolete("Use Library", true)]
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

        //[Obsolete("Use Library", true)]
        //public static Dictionary<string, string> BoxErrorCode { get; } = new Dictionary<string, string>
        //{
        //    {"0","NotChecked" },
        //    {"1","ERROR" },
        //    {"2","PASS" },
        //    {"00","NoError" },
        //    {"01","Standard Deviation was out of range (Noisy Signal)" },
        //    {"02","Calculated Mean was out of range (Offset Error)" },
        //    {"03","Standard Deviation & Calculated Mean were out of range" },
        //    {"04","Timeout" },
        //    {"08","Temperature Error" }
        //};

        //[Obsolete("Use Library", true)]
        //public static Dictionary<string, string> CalibrationStatus { get; } = new Dictionary<string, string>
        //{
        //    {"00","674mV and 500mV" },
        //    {"01","674mV" },
        //    {"02","500mV" },
        //    {"FF","NotDefined"}
        //};

        //[Obsolete("Use Library", true)]
        //public enum OpCode
        //{
        //    state, Error, error, 
        //    G015, G100, G200, G901, G902, G903, G904, G905, G906, G907, G908, G909, G910, G911,
        //    g015, g100, g200, g901, g902, g903, g904, g905, g906, g907, g908, g909, g910, g911,
        //    S100, S200, S500, S674, S999,
        //    s100, s200, s500, s674, s999,
        //    cmdread, cmdsend, parser_error,
        //    RDPG, WRPG,
        //    rdpg, wrpg,
        //    RDBX, WRBX,
        //    rdbx, wrbx,
        //    Init, init
        //}

        //private static string[] _Opcodes;
        //public static string[] Opcodes
        //{
        //    get
        //    {
        //        if (_Opcodes == null)
        //        { _Opcodes = Enum.GetNames(typeof(OpCode)); }
        //        return _Opcodes;
        //    }
        //}


        //public static OpCode ParseOpcode(this string opcode, bool toLower = false, bool toUpper = false, OpCode defaultOpcode = OpCode.parser_error)
        //{
        //    opcode = opcode.Replace("#", "");
        //    if (toLower) { opcode = opcode.ToLower(); }
        //    else if (toUpper) { opcode = opcode.ToUpper(); }
        //    if (Enum.TryParse(opcode, out OpCode result))
        //    { return result; }
        //    else
        //    { return defaultOpcode; }
        //}
        //[Obsolete("Use Library", true)]
        //public static string Get_BoxMode_Desc(string boxmode_hex)
        //{
        //    if (!string.IsNullOrEmpty(boxmode_hex))
        //    {
        //        try { return BoxMode.GetDesc(boxmode_hex); }
        //        catch { }
        //    }
        //    return "";
        //}
        //[Obsolete("Use Library", true)]
        //public static string Get_BoxErrorCode_Desc(string BoxErrorCode_hex)
        //{
        //    if (!string.IsNullOrEmpty(BoxErrorCode_hex))
        //    {
        //        try { return BoxErrorCode[BoxErrorCode_hex]; }
        //        catch { }
        //    }
        //    return "";
        //}
        //[Obsolete("Use Library", true)]
        //public static string Get_BoxCalStatus_Desc(string BoxCalStatus_hex)
        //{
        //    if (!string.IsNullOrEmpty(BoxCalStatus_hex))
        //    {
        //        //CalibrationStatus.TryGetValue(BoxCalStatus_hex, out string value);
        //        //return value;
        //        try { return CalibrationStatus[BoxCalStatus_hex]; }
        //        catch { }
        //    }
        //    return "";
        //}

        /*******************************************************************************************************************
        * Converter:
        '*******************************************************************************************************************/
        public static int ToInt(string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }
        public static int ToInt(TextBox tb)
        {
            return ToInt(tb.Text);
        }

        public static double ToDouble(string value)
        {
            return double.TryParse(value, out double result) ? result : 0;
        }
    }
}
