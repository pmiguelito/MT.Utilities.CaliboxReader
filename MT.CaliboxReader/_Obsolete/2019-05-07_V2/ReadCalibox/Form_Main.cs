using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static ReadCalibox.clConfig;
using static ReadCalibox.clGlobals;
using static STDhelper.clMTcolors;
using TT_Item_Infos;


namespace ReadCalibox
{
    public partial class Form_Main : Form
    {

        public static SynchronizationContext MainThread { get; set; }

        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        #region Constructor

        #region Ref
        static Label Lbl_Company, Lbl_SWname, Lbl_SWversion;
        
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
            frmMain = this;
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
            frmMain = this;
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
            Application.Exit();
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
                Config_ChangeParameters.ProductionTypeID = gTT.ProdType_ID;
                Config_ChangeParameters.ProductionType = gTT.ProdType_Desc;
                Config_ChangeParameters.UserNameID = gTT.UserName_ID;
                Config_ChangeParameters.UserName = gTT.UserName;
                Save_UserChanges();
                int i = 0;
                while (i < 5)
                {
                    i++;
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            catch { }
        }


        /*****************************************************************************************
        * Fonts and Design
        '****************************************************************************************/
        private void Header_Footer_Design()
        {
            clGlobals.
            Alloc_Fonts(PanHeader, 12);
            Alloc_Fonts(PanFooter, 12);
            Alloc_Fonts(Lbl_Company, 18);
            Alloc_Fonts(Lbl_SWname, 12);
            Alloc_Fonts(Lbl_SWversion, 8);
            EK_SW_Version = Application.ProductVersion;
            Lbl_SWversion.Text = $"Version: {EK_SW_Version}";
        }

        private void Work_Design()
        {
            Alloc_Fonts(PanWork);
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
                gTT.Dispose();
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
                    gTT = new UC_TT_Item_Infos(false, UC_TT_Item_Infos.SearchModus.TAG, Config_ChangeParameters.UserNameID, Config_ChangeParameters.ProductionTypeID, Config_ChangeParameters.UserName, 
                        Config_Initvalues.ODBC_Init, ProdTypes_Config, doCheck_TT: Config_Initvalues.DoCheckPass, 
                        dbValuesActive:Config_Initvalues.DB_ProdType_Active, prodTypeTableName: prodType_Table);
                    try { PanInfo.Controls.Clear(); } catch { }
                    PanInfo.Controls.Add(gTT);
                    gTT.Dock = DockStyle.Fill;
                    Alloc_Fonts(gTT, 8);
                    gTT.BringToFront();
                    
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
                gBetrieb.Dispose();
            }
            else
            {
                if (gBetrieb == null) { gBetrieb = new UC_Betrieb(); }
                if (!PanWork.Controls.Contains(gBetrieb))
                {
                    PanWork.Controls.Add(gBetrieb);
                    gBetrieb.Dock = DockStyle.Fill;
                }
                gBetrieb.BringToFront();
            }
        }

        private void Load_Panel_Config(bool close = false)
        {
            if (close) { gConfig.Dispose(); }
            else
            {
                if (!PanWork.Controls.Contains(gConfig))
                {
                    PanWork.Controls.Add(gConfig);
                    gConfig.Dock = DockStyle.Fill;
                }
                gConfig.BringToFront();
            }
        }

        private void Load_Panel_Data(bool close = false)
        {

            if (close)
            {
                gDataRead.Dispose();
            }
            else
            {
                if (!PanWork.Controls.Contains(gDataRead))
                {
                    PanWork.Controls.Add(gDataRead);
                    gDataRead.Dock = DockStyle.Fill;
                }
                gDataRead.BringToFront();
            }
        }

        private void Load_Panel_DGV(bool close = false)
        {

            if (close)
            {
                gDataReader_DGV.Dispose();
            }
            else
            {
                if (!PanWork.Controls.Contains(gDataReader_DGV))
                {
                    PanWork.Controls.Add(gDataReader_DGV);
                    gDataReader_DGV.Dock = DockStyle.Fill;
                }
                gDataReader_DGV.BringToFront();
            }
        }

        private void Load_Panel_Debug(bool close = false)
        {
            if (close)
            {
                gDataReader_DGV.Dispose();
            }
            else
            {
                if (gDebug == null) { gDebug = new UC_Debug(); }
                if (!PanWork.Controls.Contains(gDebug))
                {
                    PanWork.Controls.Add(gDebug);
                    gDebug.Dock = DockStyle.Fill;
                }
                gDebug.BringToFront();
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
