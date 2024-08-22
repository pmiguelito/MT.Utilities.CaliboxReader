using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReadCalibox.Classes;
//using TT_Item_Infos;

namespace ReadCalibox.Forms
{
    public partial class UC_Main : UserControl
    {
        #region UC Instance
        private UC_Main _instance;
        public UC_Main Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new UC_Main(); }
                return _instance;
            }
        }
        #endregion UC Instance

        public static UC_TT_Item_Infos UC_TT;

        public UC_Main()
        {
            InitializeComponent();
        }
        private void UC_Main_Load(object sender, EventArgs e)
        {
            Init();
        }
        /****************************************************************************************************
         * Init
         ***************************************************************************************************/
        void Init()
        {
            UC_TT = Form_Main.UC_TT;
            UC_TT.TB_SensorID.TextChanged += TB_SensorID_TextChanger;
            Load_Channels();
        }
        void TB_SensorID_TextChanger(object sender, EventArgs e)
        {
            Get_Infos();
        }

        /****************************************************************************************************
         * Channel
         ***************************************************************************************************/
        public static List<UC_Channel> Channels;

        void Load_Channels()
        {
            try
            {
                Channels = new List<UC_Channel>();
                UC_Channel channel;
                foreach (UC_COM com in UC_Config.PortArray)
                {
                    channel = new UC_Channel(com);
                    Channels.Add(channel);
                    _FLP_Channels.Controls.Add(channel);
                }
            }
            catch { }
        }

        /****************************************************************************************************
         * Get Item Infos
         ***************************************************************************************************/
        public static List<string> TAGno_InUse = new List<string>();
        void Get_Infos()
        {
            if (Check_Process(out string tagNo))
            {
                if (Check_TAGno_InUse(tagNo))
                {
                    Channel_RunCheck(tagNo);
                }
            }
        }
        string procNr = "Gravieren";
        bool Check_Process(out string tagNo)
        {
            tagNo = UC_TT.TAG_no;
            return procNr == UC_TT.ProcDesc_InWork;
        }

        bool Check_TAGno_InUse(string tagNo)
        {
            foreach (string tag in TAGno_InUse)
            {
                if (tag == tagNo)
                {
                    return false;
                }
            }
            TAGno_InUse.Add(tagNo);
            return true;
        }

        private void Clear_TAGno_InUse(string tagNo)
        {
            TAGno_InUse.Remove(tagNo);
        }

        /****************************************************************************************************
         * Channel Active
         ***************************************************************************************************/
        void Channel_RunCheck(string tagNo)
        {
            foreach (UC_Channel channel in Channels)
            {
                if (channel.Active && !channel.Running)
                { channel.StartReady = true; }
            }
        }
        public static void Channel_started(string ch)
        {
            UC_TT.Reset();
            foreach (UC_Channel channel in Channels)
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
    }
}
