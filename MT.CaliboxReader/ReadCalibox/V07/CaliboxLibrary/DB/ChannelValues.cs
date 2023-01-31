using MT.OneWire;
using System;
using System.Collections.Generic;
using System.Data;

namespace CaliboxLibrary
{
    /**********************************************************************************************
     * The Property name must be written identically as the Database
     **********************************************************************************************/
    public class ChannelValues
    {
        public ChannelValues()
        {
            LoadTemplate();
        }
        public int channel_no { get; set; }
        public string ODBC_EK { get; set; }
        public string ODBC_TT { get; set; }
        public BoxIdentification BoxId { get; set; } = new BoxIdentification();
        public DeviceLimits DeviceLimits { get { return BoxId.DeviceLimits; } }

        /// <summary>
        /// KEY: BoxModeHEX
        /// </summary>
        public Dictionary<string, List<DeviceLimitsResults>> MeasResultsDic = new Dictionary<string, List<DeviceLimitsResults>>(StringComparer.OrdinalIgnoreCase);
        public DataTable DT_Limits;
        public DataTable DT_tCalMeasVal;
        public DeviceLimitsResults AddMeasurement(DeviceResponseValues drv)
        {
            if (drv == null || drv.Response == null) { return null; }
            var results = DeviceLimits.CheckLimits(drv);
            Add_MeasResult(results);
            return results;
        }
        private void Add_MeasResult(DeviceLimitsResults results)
        {
            string key = results.BoxModeHex;
            if (!MeasResultsDic.TryGetValue(key, out List<DeviceLimitsResults> measList))
            {
                measList = new List<DeviceLimitsResults>();
                MeasResultsDic.Add(key, measList);
            }
            measList.Add(results);
            results.SetCount(measList.Count);
        }
        public bool GetResults(string boxModeHex, out List<DeviceLimitsResults> results)
        {
            return MeasResultsDic.TryGetValue(boxModeHex, out results);
        }
        public bool GetResultLast(string boxModeHex, out DeviceLimitsResults result)
        {
            if (GetResults(boxModeHex, out List<DeviceLimitsResults> results))
            {
                result = results[results.Count];
                return true;
            }
            result = null;
            return false;
        }

        public bool ErrorDetected { get; set; } = false;
        public string ErrorMessage { get; set; }

        /* TRACKTRACE INFOS*/
        public int tag_nr { get; set; }
        public int sensor_id { get; set; }
        public int pass_no { get; set; } = 0;
        public int Technology_ID { get; set; }
        public string technology_desc { get; set; }
        public int ProductionType_ID { get; set; }
        public string ProductionType_Desc { get; set; }
        public string item { get; set; }
        public bool item_active { get; set; }
        public string pdno { get; set; }
        public bool test_ok { get; set; } = false;

        /* Measurement INFOS*/
        public DateTime meas_time_start { get; set; }
        public DateTime meas_time_end { get; set; }
        private DateTime _Meas_time_end_Theo;
        public DateTime meas_time_end_Theo
        {
            get
            {
                if (meas_time_start.Year > 1)
                {
                    _Meas_time_end_Theo = meas_time_start.AddSeconds(Cal_Duration);
                }
                return _Meas_time_end_Theo;
            }
        }
        public TimeSpan meas_time_duration
        {
            get
            {
                if (meas_time_start.Year > 1)
                {
                    if (meas_time_end.Year > 1)
                    {
                        return meas_time_end - meas_time_start;
                    }
                    else { meas_time_end = DateTime.Now; }
                    return meas_time_end - meas_time_start;
                }
                return TimeSpan.Zero;
            }
        }
        public TimeSpan meas_time_remain
        {
            get
            {
                if (meas_time_start.Year > 1)
                {
                    return (meas_time_end_Theo - DateTime.Now);
                }
                return TimeSpan.Zero;
            }
        }
        public int User_ID { get; set; }
        public string UserName { get; set; }
        public string EK_SW_Version { get; } = Handler.EK_SW_Version;
        public int error_no { get; set; }

        public bool sample_FW_Version_Cal_active { get; set; }
        public string sample_FW_Version_value { get; set; }
        public string sample_FW_Version { get; set; }
        public int sample_FW_Version_state
        {
            get
            {
                if (sample_FW_Version_Cal_active)
                {
                    return sample_FW_Version_ok ? 2 : 1;
                }
                return 0;
            }
        }
        public bool sample_FW_Version_ok
        {
            get
            {
                if (sample_FW_Version_Cal_active)
                {
                    if (!string.IsNullOrEmpty(sample_FW_Version_value))
                    { 
                        return sample_FW_Version == sample_FW_Version_value; 
                    }
                    return false;
                }
                return true;
            }
        }

        public int procCounterLast = 0;
        public int procCounter { get { return procCounterLast++; } }

        public int CalMode_ID { get; set; }
        public string cal_opcode { set { CalMode = value.ParseOpcode(defaultOpcode: OpCode.S100); } }

        public OpCode CalMode { get; set; }
        public string Cal_Desc { get; set; }
        public double Cal_Duration { get; set; }

        public string Cal_BoxMode_desc { get; set; }
        public System.Diagnostics.Stopwatch Cal_Stopwatch { get; set; } = new System.Diagnostics.Stopwatch();

        /**********************************************
         *  FUNCTION:   Save To DataBase
         *  DESCRIPTION:
         *********************************************/
        public bool Save_DBprocInit()
        {
            return DataBase.MeasVal_Insert_Init(this);
        }

        public bool Save_DBmeasValues(DataTable dtProgress)
        {
            var response = DataBase.MeasVal_Update(this);
            DataBase.MeasValTemp_Insert(this, dtProgress);
            return response;
        }

        #region TEDS Template
        /**********************************************************
        * FUNCTION:     TEDS Template
        * DESCRIPTION:  
        ***********************************************************/
        public TEDSPageDatas TEDSpageDatas { get; private set; } = new TEDSPageDatas();

        public void LoadTemplate()
        {
            TdlData.GetTemplate(ODBC_EK, out var template);
            TEDSpageDatas.SensorTemplate = template;
        }
        #endregion
    }
}
