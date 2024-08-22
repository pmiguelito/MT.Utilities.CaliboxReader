using CaliboxLibrary;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TT_Item_Infos;
using static STDhelper.clSTD;

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

        /*******************************************************************************************************************
        * Fonts:
        '*******************************************************************************************************************/

        public static void AllocFont(Control c, float size = 8, FontStyle fontStyle = FontStyle.Regular)
        {
            Fonts.AllocFont(c, size, fontStyle);
        }

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
