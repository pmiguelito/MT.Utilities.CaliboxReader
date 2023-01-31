using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_Item_Infos;
using static STDhelper.clMTcolors;
using static ReadCalibox.clConfig;
using static ReadCalibox.clGlobals;

namespace ReadCalibox
{
    public partial class UC_Betrieb : UserControl
    {
        private static UC_Betrieb _instance;
        public static UC_Betrieb Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new UC_Betrieb(); }
                return _instance;
            }
        }

        /****************************************************************************************************
         * Constructor
         ***************************************************************************************************/
        public static Label Lbl_ErrorMessage;
        void ObjRef()
        {
            Lbl_ErrorMessage = _Lbl_ErrorMessage;
        }
        public UC_Betrieb()
        {
            InitializeComponent();
            ObjRef();
        }
        private void UC_Main_Load(object sender, EventArgs e)
        {
            //Init_Design();
            Init();
        }

        /****************************************************************************************************
         * Initialization
         ***************************************************************************************************/
        public static bool ODBC_Initial_Found = false;
        void Init()
        {
            _Lbl_ErrorMessage.Text = "";
            try { gTT.SensorID_Changed += SensorID_Changed; } catch { }
            Load_Channels();
            Check_ODBC_Init();
        }
        void Init_Design()
        {
            PanMessage.BackColor = MT_BackGround_Work;
            FlowPan_Channels.BackColor = MT_BackGround_Work;
            PanSep1.BackColor = MT_BackGround_HeaderFooter;
        }

        public static bool Check_ODBC_Init()
        {
            if (Config_Initvalues.DB_ProdType_Active)
            {
                ODBC_Initial_Found = ODBC_TT.ODBC.GetODBC_status(Config_Initvalues.ODBC_Init);
                if (!ODBC_Initial_Found)
                {
                    ErrorMessageMain += $"ERROR: Datenbank nicht gefunden {Config_Initvalues.ODBC_Init}";
                }
            }
            return ODBC_Initial_Found;
        }

        void SensorID_Changed(object sender, EventArgs e)
        {
            Get_Infos();
        }

        /****************************************************************************************************
         * Error Messages
         ***************************************************************************************************/
        public delegate void ErrorMessageDelegate(string message);
        static ErrorMessageDelegate Delegate_Error = new ErrorMessageDelegate(Get_ErrorValue);
        private static void Get_ErrorValue(string message)
        {
            Lbl_ErrorMessage.Text = message;
        }
        
        public static string ErrorMessageMain
        {
            get { return Lbl_ErrorMessage.Text; }
            set { Delegate_Error(value); }
        }

        /****************************************************************************************************
         * Channel
         ***************************************************************************************************/
        //public static List<UC_Channel> Channels;
        //public List<UC_Channel> ChannelsList { get { return Channels; } }
        private static bool _Running;
        public static bool Running
        {
            get { return Form_Main.TestRunning; }
            set { Form_Main.TestRunning = value; }
        }
        private static int _Running_Count;
        public static int Running_Count
        {
            get { return _Running_Count; }
            set
            {
                Running = value > 0;
                _Running_Count = value;
            }
        }
        private void Load_Channels()
        {
            try
            {
                foreach (UC_Channel channel in Config_ChannelsList)
                {
                    FlowPan_Channels.Controls.Add(channel);
                }
            }
            catch { }
        }

        /****************************************************************************************************
         * Get Item Infos
         ***************************************************************************************************/
        public ItemLimits Limits;
        void Get_Infos()
        {
            ErrorMessageMain = "";
            string message = "";
            int tagNo = 0;
            bool inUSE = false;
            if (Check_Process(out tagNo, out message))
            {
                string item = gTT.tSensor.item;
                int sensorID = gTT.tSensor.sensor_id;
                if (clDatenBase.Get_Limits(gTT.ProdType_Selected.ODBC_EK, item, sensorID, out Limits, out string errormessage))
                {
                    inUSE = !Check_TAGno_InUse(tagNo);
                }
                else
                {
                    tagNo = 0;
                    message = $"ERROR: Art-Nr. {item} Configuration " + errormessage;
                }
                
            }
            Channel_RunCheck(tagNo, inUSE);
            ErrorMessageMain += message;
        }
        
        bool Check_Process(out int tagNo, out string message)
        {
            tagNo = 0;
            message = "";
            if (gTT.DoChecks(out message))
            {
                tagNo = gTT.tSensor.tag_nr;
                return true;
            }
            return false;
        }

        bool Check_TAGno_InUse(int tagNo)
        {
            foreach (UC_Channel channel in Config_ChannelsList)
            {
                if (channel.TAGno == tagNo)
                {
                    if (channel.Running)
                    {
                        ErrorMessageMain = $"ERROR: TAG-Nr. {tagNo} Channel: {channel.Channel}";
                        return false;
                    }
                    else { return true; }
                }
            }
            return true;
        }

        /****************************************************************************************************
         * Channel Active
         ***************************************************************************************************/
        //private bool resetInfos = false;

        public void Channel_RunCheck(int tagNo, bool inUSE = false)
        {
            foreach (UC_Channel channel in Config_ChannelsList)
            {
                if (channel.Active && !channel.Running)
                {
                    channel.Reset();
                    channel.StartReady = (tagNo>0 & !inUSE)? true:false;
                }
            }
        }
        public void Channel_started(string ch = "")
        {
            if (!string.IsNullOrEmpty(ch))
            {
                foreach (UC_Channel channel in Config_ChannelsList)
                {
                    if (channel.Active && !channel.Running)
                    {
                        if (ch != channel.Channel)
                        {
                            channel.StartReady = false;
                        }
                    }
                }
            }
            else
            {
                foreach (UC_Channel channel in Config_ChannelsList)
                {
                    if (channel.Active && !channel.Running)
                    { channel.StartReady = false; }
                }
            }
            //resetInfos = true;
            gTT.Reset(false);
            //resetInfos = false;
        }
    }
}
