using CaliboxLibrary.DB;
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

        private int _channel_no = -1;
        public int channel_no
        {
            get { return _channel_no; }
            set
            {
                if (_channel_no != value)
                {
                    _channel_no = value;
                    _BoxID = BoxIdentification.GetOrAdd(value);
                }
            }
        }
        public string ODBC_EK { get; set; }
        public string ODBC_TT { get; set; }

        private BoxIdentification _BoxID;
        public BoxIdentification BoxId
        {
            get
            {
                if (_BoxID == null)
                {
                    _BoxID = BoxIdentification.GetOrAdd(channel_no);
                }
                return _BoxID;
            }
        }
        public DeviceLimits DeviceLimits
        { get { return BoxId.DeviceLimits; } }

        /// <summary>
        /// KEY: BoxModeHEX
        /// </summary>
        public Dictionary<string, List<DeviceLimitsResults>> MeasResultsDic { get; set; }
            = new Dictionary<string, List<DeviceLimitsResults>>(StringComparer.OrdinalIgnoreCase);

        public DataTable DT_Limits;
        public DataTable DT_tCalMeasVal;

        public bool AddMeasurement(DeviceResponseValues drv, out DeviceLimitsResults results)
        {
            results = null;
            if (drv == null || drv.Response == null)
            {
                return false;
            }
            if (DeviceLimits.CheckLimits(drv, out results))
            {
                Add_MeasResult(results);
                return true;
            }
            return false;
        }

        private void Add_MeasResult(DeviceLimitsResults results)
        {
            string key = results.BoxMode.Hex;
            if (MeasResultsDic.TryGetValue(key, out List<DeviceLimitsResults> measList) == false)
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

        public void Reset()
        {
            ErrorDetected = false;
            ErrorMessage = string.Empty;
            SampleSN.Reset(string.Empty);
            SampleFWVersion.Reset(string.Empty);
            CaliboxFW.Reset(string.Empty);
            CaliboxStatus.Reset(string.Empty);
        }

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

        public Evaluation<string> SampleFWVersion { get; set; }
            = new Evaluation<string>("Sample FW Version") { Active = true };

        public bool sample_FW_Version_Cal_active
        {
            get { return SampleFWVersion.Active; }
            set { SampleFWVersion.Active = value; }
        }

        /// <summary>
        /// Sensor value
        /// </summary>
        public string sample_FW_Version_value
        {
            get { return SampleFWVersion.Value; }
            set
            {
                SampleFWVersion.Active = true;
                SampleFWVersion.SetValueCompaireIsNotEqual(value, string.Empty);
            }
        }

        /// <summary>
        /// DataBase value
        /// </summary>
        public string sample_FW_Version
        {
            get { return SampleFWVersion.ValueDB; }
            set { SampleFWVersion.ValueDB = value; }
        }
        public int sample_FW_Version_state
        {
            get
            {
                return SampleFWVersion.State;
            }
        }
        public bool sample_FW_Version_ok
        {
            get
            {
                return SampleFWVersion.OK;
            }
        }

        public Evaluation<string> SampleSN { get; set; }
            = new Evaluation<string>("Sample SN")
            {
                Active = true
            };

        public bool sample_SN_ok
        {
            get
            {
                return SampleSN.OK;
            }
        }
        public int sample_SN_state { get { return SampleSN.State; } }
        public string sample_SN
        {
            get { return SampleSN.Value; }
            set
            {
                SampleSN.Active = true;
                SampleSN.SetValueCompaireIsNotEqual(value, string.Empty);
            }
        }

        public int procCounterLast = 0;
        public int procCounter { get { return procCounterLast++; } }

        public int CalMode_ID { get; set; }
        public string cal_opcode
        {
            set { CalMode = value.ParseOpcode(defaultOpcode: OpCode.S100); }
        }

        public OpCode CalMode { get; set; }
        public string Cal_Desc { get; set; }
        public double Cal_Duration { get; set; }

        public string Cal_BoxMode_desc { get; set; }
        public System.Diagnostics.Stopwatch Cal_Stopwatch { get; set; } = new System.Diagnostics.Stopwatch();

        /**********************************************
         *  FUNCTION:   CaliBox Infos
         *  DESCRIPTION:
         *********************************************/
        public Evaluation<string> CaliboxFW { get; set; }
        = new Evaluation<string>("Calibox FW Version") { Active = true };

        public bool CaliBoxFWversion_OK
        {
            get { return CaliboxFW.OK; }
        }

        public int CaliBoxFWversion_State
        {
            get { return CaliboxFW.State; }
        }

        public string CaliBoxFWversion
        {
            get { return CaliboxFW.Value; }
            set
            {
                CaliboxFW.Active = true;
                CaliboxFW.SetValueCompaireIsNotEqual(value, string.Empty);
            }
        }

        public bool CheckBoxFW(out string value)
        {
            value = string.Empty;
            if (BoxId != null)
            {
                value = BoxId.DeviceLimits.FW_Version;
            }
            CaliBoxFWversion = value;
            return CaliBoxFWversion_OK;
        }

        public Evaluation<string> CaliboxStatus { get; set; }
       = new Evaluation<string>("Calibox FW Version") { Active = true, ValueDB = "CheckSum OK" };

        public bool CaliBoxFWstatus_OK
        {
            get { return CaliboxStatus.OK; }
        }

        public int CaliBoxFWstatus_State
        {
            get { return CaliboxStatus.State; }
        }

        public string CaliBoxFWstatus
        {
            get { return CaliboxStatus.Value; }
            set
            {
                CaliboxStatus.Active = true;
                CaliboxStatus.SetValueCompaireIsEqual(value);
            }
        }

        public bool CheckBoxStatus(out string value)
        {
            value = string.Empty;
            if (BoxId != null)
            {
                value = BoxId.CS_ok ? "CheckSum OK" : "CheckSum Fail";
            }
            CaliBoxFWstatus = value;
            return CaliBoxFWstatus_OK;
        }

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
