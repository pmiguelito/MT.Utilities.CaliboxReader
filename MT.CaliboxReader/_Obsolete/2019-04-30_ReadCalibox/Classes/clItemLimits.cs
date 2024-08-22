using System;

namespace ReadCalibox
{
    /**********************************************************************************************
     * The Property name must be written identically as the Database
     **********************************************************************************************/
    public class clItemLimits
    {
        public string tag_nr { get; set; } = Form_Main.UC_TT.ItemInfos.tSensor.tag_nr;
        public string sensor_id { get; set; } = Form_Main.UC_TT.ItemInfos.tSensor.sensor_id;
        public int pass_no { get; set; } = 0;
        public int Technology_ID { get; set; }
        public string technology_desc { get; set; }
        public int ProductionType_ID { get; set; } = Form_Main.UC_TT.ProductionType_ID;
        public string ProductionType_Desc { get; set; } = Form_Main.UC_TT.Production_Type;
        public string item { get; set; } = Form_Main.UC_TT.ItemInfos.tSensor.item;
        public bool item_active { get; set; }
        public string pdno { get; set; } = Form_Main.UC_TT.ItemInfos.tSensor.pdno;
        public bool test_ok { get; set; } = false;
        public DateTime meas_time_start { get; } = DateTime.Now;
        public DateTime meas_time_end { get; set; } = DateTime.Now;
        public int User_ID { get; set; } = Form_Main.UC_TT.UserName_ID;
        public string UserName { get; set; } = Form_Main.UC_TT.UserName;
        public string EK_SW_Version { get; } = Form_Main.EK_SW_Version;
        public int error_no { get; set; }

        public bool sample_FW_Version_active { get; set; }
        public string sample_FW_Version_value { get; set; }
        public string sample_FW_Version {get;set;}
        public int sample_FW_Version_state
        {
            get
            {
                if (sample_FW_Version_active)
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
                if (sample_FW_Version_active)
                {
                    if (!string.IsNullOrEmpty(sample_FW_Version_value))
                    { return sample_FW_Version == sample_FW_Version_value; }
                    return false;
                }
                return true;
            }
        }
        public float pol_voltage { get; set; }
        public bool pol_voltage_active { get; set; }
        public bool pol_voltage_cal_multi { get; set; }

        public int procCounterLast = 0;
        public int procCounter { get { return procCounterLast++; } }
    }
}
