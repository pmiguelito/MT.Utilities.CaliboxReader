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
using static STDhelper.clSTD;
using static ReadCalibox.clConfig;
using static ReadCalibox.Handler;
using CaliboxLibrary;

namespace ReadCalibox
{
    public partial class UC_Betrieb : UserControl
    {
        #region UC Instance
        private UC_Betrieb instance;
        public UC_Betrieb Instance
        {
            get
            {
                if (instance == null)
                { instance = new UC_Betrieb(); }
                return instance;
            }
        }
        #endregion UC Instance

        /****************************************************************************************************
         * Constructor
        '***************************************************************************************************/
        public static Label Lbl_ErrorMessage;
        void ObjRef()
        {
            Lbl_ErrorMessage = _Lbl_ErrorMessage;
        }
        public UC_Betrieb()
        {
            if (instance == null)
            {
                InitializeComponent();
                ObjRef();
            }
        }
        private void UC_Main_Load(object sender, EventArgs e)
        {
            Init();
        }

        /****************************************************************************************************
         * Initialization
         ***************************************************************************************************/
        public static bool ODBC_Initial_Found = false;
        void Init()
        {
            _Lbl_ErrorMessage.Text = "";
            try
            {
                if (H_TT == null)
                { H_TT = new UC_TT_Item_Infos(); }
                H_TT.SensorID_Changed += SensorID_Changed;
            }
            catch { }
            Load_Channels();
            Check_ODBC_Init();
        }
        void Init_Design()
        {
            PanMessage.BackColor = MTcolors.MT_BackGround_Work;
            FlowPan_Channels.BackColor = MTcolors.MT_BackGround_Work;
            PanSep1.BackColor = MTcolors.MT_BackGround_HeaderFooter;
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
        ** Error Messages
        ****************************************************************************************************/
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
        ** Channel
        ****************************************************************************************************/
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
        ** Get Item Infos
        ****************************************************************************************************/
        public CaliboxLibrary.ChannelValues Limits;

        private void NewLimits()
        {
            Limits = new CaliboxLibrary.ChannelValues
            {
                sensor_id = H_TT.tSensor.sensor_id,
                tag_nr = H_TT.tSensor.tag_nr,
                pdno = H_TT.tSensor.pdno,
                item = H_TT.tSensor.item,
                ODBC_EK = H_TT.ProdType_Selected.ODBC_EK,
                ODBC_TT = H_TT.ProdType_Selected.ODBC_TT,
                ProductionType_ID = H_TT.ProdType_ID,
                ProductionType_Desc = H_TT.ProdType_Desc
            };
        }

        //public clItemLimits Limits;
        private void Get_Infos()
        {
            ErrorMessageMain = "";
            string message = "";
            int tagNo = -1;
            bool inUSE = false;

            if (Check_Process(out tagNo, out message))
            {
                if (H_IsDebugModus)
                {
                    /* only for TESTS, this delete completely all database values */
                    DataBase.Truncate_DB(H_TT.ODBC_EK_Selected);
                }
                NewLimits();
                //string item = H_TT.tSensor.item;
                //int sensorID = H_TT.tSensor.sensor_id;
                if (DataBase.Get_Limits(ref Limits, out string errorMessage))
                {
                    inUSE = !Check_TAGno_InUse(tagNo);
                }
                else
                {
                    tagNo = 0;
                    message = $"ERROR: Art-Nr. {H_TT.tSensor.item} Configuration " + errorMessage;
                }
            }
            if (tagNo > 0)
            {
                Channel_RunCheck(tagNo, inUSE);
            }
            ErrorMessageMain += message;
        }

        private bool Check_Process(out int tagNo, out string message)
        {
            tagNo = 0;
            message = "";
            //TODO: Change DoChecks
            if (H_TT.DoChecks(out message))
            {
                tagNo = H_TT.tSensor.tag_nr;
                return true;
            }
            return false;
        }

        private bool Check_TAGno_InUse(int tagNo)
        {
            foreach (UC_Channel channel in Config_ChannelsList)
            {
                if (channel.ItemValues.tag_nr == tagNo)
                {
                    if (channel.Running)
                    {
                        ErrorMessageMain = $"ERROR: TAG-Nr. {tagNo} Channel: {channel.Channel_No}";
                        return false;
                    }
                    else { return true; }
                }
            }
            return true;
        }

        /****************************************************************************************************
        ** Channel Active
        ****************************************************************************************************/
        public void Channel_RunCheck(int tagNo, bool inUSE = false)
        {
            foreach (UC_Channel channel in Config_ChannelsList)
            {
                //if (channel.Active && !channel.Running)
                if (!channel.Running)
                {
                    channel.StartReady = (tagNo > 0 && !inUSE);
                }
            }
        }
        public void Channel_started(int ch = -1)
        {
            if (ch > 0)
            {
                foreach (UC_Channel channel in Config_ChannelsList)
                {
                    if (channel.Active && !channel.Running)
                    {
                        if (ch != channel.Channel_No && channel.StartReady)
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
                    if (channel.Active && !channel.Running && channel.StartReady)
                    {
                        channel.StartReady = false;
                    }
                }
            }
            H_TT.Reset(false);
        }
    }
}
