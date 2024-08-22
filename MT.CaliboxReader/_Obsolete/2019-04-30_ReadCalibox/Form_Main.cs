using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TT_Item_Infos;
using static ReadCalibox.clConfig;
using static STDhelper.clMTcolors;

namespace ReadCalibox
{
    public partial class Form_Main : Form
    {

        public static SynchronizationContext MainThread { get; set; }

        public static string EK_SW_Version;

        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        #region Constructor
        #region Ref
        static Label Lbl_Company, Lbl_SWname, Lbl_SWversion;
        public static UC_TT_Item_Infos UC_TT { get; set; }
        public static UC_Betrieb UC_betrieb { get; set; }
        public static UC_Config UC_config { get; set; }
        public static UC_DataRead UC_Data { get; set; }
        public static UC_DataReader_DGV UC_DGV { get; set; }
        public static Button Btn_Exit, Btn_Admin, Btn_Betrieb, Btn_Config;
        void ObjRef()
        {
            Lbl_Company = _Lbl_Company;
            Lbl_SWname = _Lbl_SWname;
            Lbl_SWversion = _Lbl_SWvwersion;
            Btn_Exit = _Btn_Exit;
            Btn_Exit.Text = "Beenden";
            Btn_Admin = _Btn_Admin;
            Btn_Betrieb = _Btn_Betrieb;
            Btn_Config = _Btn_Config;
            MainThread = SynchronizationContext.Current;
            if (MainThread == null)
            { MainThread = new SynchronizationContext(); }
        }
        #endregion Ref
        public Form_Main()
        {
            InitializeComponent();
            ObjRef();
            clConfig.Load();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Init();
            Application.DoEvents();
            foreach(UC_Channel uc in Config_ChannelsList)
            {
                uc.Reset_DT_Calibration();
            }
            this.BringToFront();
        }
        #endregion Constructor
        /***************************************************************************************
        * Exit:     Close Application
        ****************************************************************************************/
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigSave();
            Application.DoEvents();
            Thread.Sleep(200);
            //Application.Exit();
        }

        /***************************************************************************************
        * Exit:     Button Beenden
        ****************************************************************************************/
        private static bool _TestRunning = false;
        public static bool TestRunning
        {
            get { return _TestRunning; }
            set
            {
                _TestRunning = value;
                TestRunning_Change(!value);
            }
        }
        private static void TestRunning_Change(bool enabled)
        {
            //Btn_Admin.Enabled = enabled;
            Btn_Exit.Enabled = enabled;
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            if (Btn_Exit.Text == "Beenden")
            { this.Close(); }
        }

        /***************************************************************************************
        * Exit:     Save Configuration
        ****************************************************************************************/
        void ConfigSave()
        {
            try
            {
                Config_ChangeParameters.ProductionTypeID = UC_TT.ProductionType_ID;
                Config_ChangeParameters.ProductionType = UC_TT.Production_Type;
                Config_ChangeParameters.UserNameID = UC_TT.UserName_ID;
                Config_ChangeParameters.UserName = UC_TT.UserName;
                Save_UserChanges();
            }
            catch { }
        }

        /***************************************************************************************
        * GUI:      Fonts and Design
        ****************************************************************************************/
        public static void AllocFont(Control c, float size = 8, FontStyle fontStyle = FontStyle.Regular)
        {
            UC_TT_Item_Infos.Alloc_Font(c, size, fontStyle);
        }

        private void Header_Footer_Design()
        {
            AllocFont(PanHeader, 12);
            AllocFont(PanFooter, 12);
            AllocFont(Lbl_Company, 18);
            AllocFont(Lbl_SWname, 12);
            AllocFont(Lbl_SWversion, 8);
            EK_SW_Version = Application.ProductVersion;
            Lbl_SWversion.Text = $"Version: {EK_SW_Version}";
        }

        private void Work_Design()
        {
            AllocFont(PanWork);
            PanWork.BackColor = MT_BackGround_Work;
        }

        /***************************************************************************************
        * GUI:      Main Dimensions
        ****************************************************************************************/
        #region GUI dimensions
        const int sepHeight = 3 * 2;
        const int headerFooter_Height = 40 * 2 + sepHeight;
        const int info_Height = 80;
        const int rand_Width = 14; //10
        const int rand_Heigth = 38;
        const int workArea_Width = 153; //154
        const int workArea_Channel_Height = 352; // one channel Height
        const int workArea_Info_Height = 25 + 3;
        const int min_Width = workArea_Width * 5 + rand_Width;

        void MainFrame_Design()
        {
            PanHeader.Height = headerFooter_Height;
            PanFooter.Height = headerFooter_Height;
            PanInfo.Height = info_Height;
        }

        public void Resize_MainFrame(int ch)
        {
            int multiplier = ch;
            if (ch <= 10) { multiplier = 5; }
            else if (ch > 10 && ch < 23)
            {
                decimal d = (decimal)ch / 2;
                if (d < 5)
                { multiplier = 5; }
                else
                { multiplier = (int)(Math.Round(d, MidpointRounding.AwayFromZero)); }
            }
            else if (ch > 22)
            { multiplier = 11; }
            int width = rand_Width + (workArea_Width * multiplier);
            int min_height = headerFooter_Height + info_Height + workArea_Channel_Height + workArea_Info_Height + rand_Heigth;
            int heigth = min_height;
            if (width < min_Width)
            { width = min_Width; }
            if (ch > 5) { heigth += workArea_Channel_Height; }
            this.MaximumSize = new Size(width + 1, heigth + 1);
            this.MinimumSize = new Size(min_Width, min_height);
            this.Size = new Size(width, heigth);
        }

        #endregion GUI dimensions
        /***************************************************************************************
        * Initialization:   Panels
        ****************************************************************************************/
        void Init()
        {
            Init_Main_Design(Config_Initvalues.CHquantity);
            //Load_Panel_Data();
            //Load_Panel_DGV();
            Load_Panel_TT();
            //Load_Panel_Config();
            Load_Panel_Betrieb();
            Admin_State();
        }

        /// <summary>
        /// Configuration must be loaded
        /// </summary>
        void Init_Main_Design(int ch)
        {
            MainFrame_Design();
            Header_Footer_Design();
            Work_Design();
            Resize_MainFrame(ch);
        }

        /***************************************************************************************
        * Initialization:   Load Panels
        ****************************************************************************************/
        /// <summary>
        /// Config must be loaded befor started
        /// </summary>
        /// <param name="close"></param>
        private bool Load_Panel_TT(bool close = false)
        {
            if (close)
            {
                UC_TT.Dispose();
                return true;
            }
            else
            {
                //if (ProdType_Initvalues())
                try
                {
                    string prodType_Table = "tProductionType";
                    if (!string.IsNullOrEmpty(Config_Initvalues.DB_ProdType_Table))
                    { prodType_Table = Config_Initvalues.DB_ProdType_Table; }
                    UC_TT = new UC_TT_Item_Infos(false, Config_ChangeParameters.UserNameID, Config_ChangeParameters.ProductionTypeID, Config_ChangeParameters.UserName, 
                        Config_Initvalues.ODBC_Init, ProdTypes_Config, doCheck_TT: Config_Initvalues.DoCheckPass, 
                        dbValuesActive:Config_Initvalues.DB_ProdType_Active, prodTypeTableName: prodType_Table);
                    try { PanInfo.Controls.Clear(); } catch { }
                    PanInfo.Controls.Add(UC_TT);
                    UC_TT.Dock = DockStyle.Fill;
                    UC_TT.AllocFont(UC_TT, 8);
                    UC_TT.BringToFront();
                    
                    return true;
                }
                catch (Exception e)
                { }
            }
            return false;
        }

        private void Load_Panel_Betrieb(bool close = false)
        {
            if (close)
            {
                UC_betrieb.Dispose();
            }
            else
            {
                if (UC_betrieb == null) { UC_betrieb = new UC_Betrieb(); }
                if (!PanWork.Controls.Contains(UC_betrieb))
                {
                    PanWork.Controls.Add(UC_betrieb);
                    UC_betrieb.Dock = DockStyle.Fill;
                }
                UC_betrieb.BringToFront();
            }
        }

        private void Load_Panel_Config(bool close = false)
        {
            if (close) { UC_config.Dispose(); }
            else
            {
                if (UC_config == null) { UC_config = new UC_Config(); }
                if (!PanWork.Controls.Contains(UC_config))
                {
                    PanWork.Controls.Add(UC_config);
                    UC_config.Dock = DockStyle.Fill;
                }
                UC_config.BringToFront();
            }
        }

        private void Load_Panel_Data(bool close = false)
        {

            if (close)
            {
                UC_Data.Dispose();
            }
            else
            {
                if (UC_Data == null) { UC_Data = new UC_DataRead(); }
                if (!PanWork.Controls.Contains(UC_Data))
                {
                    PanWork.Controls.Add(UC_Data);
                    UC_Data.Dock = DockStyle.Fill;
                }
                UC_Data.BringToFront();
            }
        }

        private void Load_Panel_DGV(bool close = false)
        {

            if (close)
            {
                UC_DGV.Dispose();
            }
            else
            {
                if (UC_DGV == null) { UC_DGV = new UC_DataReader_DGV(); }
                if (!PanWork.Controls.Contains(UC_DGV))
                {
                    PanWork.Controls.Add(UC_DGV);
                    UC_DGV.Dock = DockStyle.Fill;
                }
                UC_DGV.BringToFront();
            }
        }

        UC_Debug uC_Debug;
        private void Load_Panel_Debug(bool close = false)
        {

            if (close)
            {
                UC_DGV.Dispose();
            }
            else
            {
                if (uC_Debug == null) { uC_Debug = new UC_Debug(); }
                if (!PanWork.Controls.Contains(uC_Debug))
                {
                    PanWork.Controls.Add(uC_Debug);
                    uC_Debug.Dock = DockStyle.Fill;
                }
                uC_Debug.BringToFront();
            }
        }

        /***************************************************************************************
        * Administrator:   
        ****************************************************************************************/
        private bool _LoggedIn;
        private bool LoggedIn
        {
            get { return _LoggedIn; }
            set
            {
                _LoggedIn = value;
                Admin_State();
            }
        }

        

        private void Admin_State()
        {
            Admin_Buttons_Design();
            if (!LoggedIn)
            {
                Load_Panel_Betrieb();
            }
            else
            {
                Load_Panel_Config();
            }
        }

        private void Admin_Buttons_Design()
        {
            Btn_Betrieb.Visible = LoggedIn;
            Btn_Config.Visible = LoggedIn;
            Btn_Debug.Visible = LoggedIn;
            Btn_Admin.BackColor = LoggedIn ? Color.Gray : SystemColors.HotTrack;
        }

        private void Btn_Admin_Click(object sender, EventArgs e)
        {
            LoggedIn = !LoggedIn;
        }

        private void Btn_Betrieb_Click(object sender, EventArgs e)
        {
            Load_Panel_Betrieb();
        }

        private void Btn_Config_Click(object sender, EventArgs e)
        {
            Load_Panel_Config();
        }

        private void Btn_Debug_Click(object sender, EventArgs e)
        {
            Load_Panel_Debug();
        }

    }
}
