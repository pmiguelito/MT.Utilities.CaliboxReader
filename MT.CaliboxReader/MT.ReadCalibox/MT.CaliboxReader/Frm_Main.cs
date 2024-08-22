using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TT_Item_Infos;
using static ReadCalibox.clConfig;
using static ReadCalibox.Handler;
using static STDhelper.clSTD;

namespace ReadCalibox
{
    public partial class Frm_Main : Form
    {
        public static SynchronizationContext MainThread { get; set; }

        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        #region Constructor
        static Label Lbl_Company, Lbl_SWname, Lbl_SWversion;

        public static Button Btn_Exit, Btn_Admin, Btn_Betrieb, Btn_Config;
        public static string SWname = "O2 Board Calibration";
        void ObjRef()
        {
            this.Text = SWname;
            Lbl_Company = _Lbl_Company;
            Lbl_SWname = _Lbl_SWname;
            Lbl_SWname.Text = SWname;
            Lbl_SWversion = _Lbl_SWvwersion;
            Btn_Exit = _Btn_Exit;
            Btn_Exit.Text = _TxtBeenden;
            Btn_Admin = _Btn_Admin;
            Btn_Betrieb = _Btn_Betrieb;
            Btn_Config = _Btn_Config;
            MainThread = SynchronizationContext.Current;
            if (MainThread == null)
            { MainThread = new SynchronizationContext(); }
        }
        private frmSplash _Splash;
        public Frm_Main()
        {
            ShowSplash();
            Application.DoEvents();
            InitializeComponent();
            H_IsDebugModus = System.Diagnostics.Debugger.IsAttached;
            H_AdminModus = H_IsDebugModus;
            ObjRef();
            clConfig.Load();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Init();
            foreach (UC_Channel uc in Config_ChannelsList)
            {
                uc.Reset();
            }
            SplashStop();
        }

        /***************************************************************************************
        * Exit:     Close Application
        ****************************************************************************************/
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (H_TestRunning)
            { e.Cancel = true; return; }
            ConfigSave();
            Application.DoEvents();
            Thread.Sleep(200);
        }

        #endregion Constructor
        /***************************************************************************************
        * SpashScreen:
        ****************************************************************************************/
        #region Splash
        private static Thread _ThreadSplash;
        private void ShowSplash()
        {
            try
            {
                _ThreadSplash = new Thread(new ThreadStart(SplashStart))
                {
                    IsBackground = true
                };
                _ThreadSplash.Start();
            }
            catch
            {

            }
        }
        private void SplashStart()
        {
            try
            {
                _Splash = new frmSplash(SWname, Application.ProductVersion, this);
                Application.Run(_Splash);
            }
            catch { }
        }
        private void SplashStop()
        {
            try
            {
                _ThreadSplash.Abort();
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;
                this.Focus();
                this.BringToFront();
                this.TopMost = false;
            }
            catch { }
        }
        #endregion Splash

        /***************************************************************************************
        * Exit:     Button Beenden
        ****************************************************************************************/
        private const string _TxtRunning = "...Running";
        private const string _TxtBeenden = "Beenden";
        public static void TestRunning_Change(bool enabled)
        {
            Btn_Exit.Enabled = !enabled;
            if (enabled)
            {
                Btn_Exit.Text = _TxtRunning;
            }
            else
            {
                Btn_Exit.Text = _TxtBeenden;
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            if (Btn_Exit.Text == _TxtBeenden)
            {
                this.Close();
            }
        }

        /***************************************************************************************
        * Exit:     Save Configuration
        ****************************************************************************************/
        void ConfigSave()
        {
            try
            {
                Config_ChangeParameters.ProductionTypeID = H_TT.ProdType_ID;
                Config_ChangeParameters.ProductionType = H_TT.ProdType_Desc;
                Config_ChangeParameters.UserNameID = H_TT.UserName_ID;
                Config_ChangeParameters.UserName = H_TT.UserName;
                Save_UserChanges();
            }
            catch { }
        }

        /***************************************************************************************
        * GUI:      Fonts and Design
        ****************************************************************************************/
        #region GUI
        private void Header_Footer_Design()
        {
            AllocFont(PanHeader, 12);
            AllocFont(PanFooter, 12);
            AllocFont(Lbl_Company, 18);
            AllocFont(Lbl_SWname, 12);
            AllocFont(Lbl_SWversion, 8);
            Handler.EK_SW_Version = Application.ProductVersion;
            Lbl_SWversion.Text = $"Version: {Handler.EK_SW_Version}";
        }

        private void Work_Design()
        {
            AllocFont(PanWork);
            PanWork.BackColor = MTcolors.MT_BackGround_Work;
        }

        /***************************************************************************************
        * GUI:      Main Dimensions
        ****************************************************************************************/
        const int _SepHeight = 3 * 2;
        const int _HeaderFooter_Height = 40 * 2 + _SepHeight;
        const int _Info_Height = 80;
        const int _Rand_Width = 18;//14;
        const int _Rand_Heigth = 38;
        const int _WorkArea_Width = 303; //153
        const int _WorkArea_Channel_Height = 352; // one channel Height
        const int _WorkArea_Info_Height = 25 + 3;
        const int _Min_Width = _WorkArea_Width * 4 + _Rand_Width;

        void MainFrame_Design()
        {
            PanHeader.Height = _HeaderFooter_Height;
            PanFooter.Height = _HeaderFooter_Height;
            PanInfo.Height = _Info_Height;
        }

        public void Resize_MainFrame(int ch)
        {
            int multiplier = ch;
            int workArea_CH_Heigth = _WorkArea_Channel_Height;
            if (ch <= 4)
            {
                multiplier = 4;
                workArea_CH_Heigth = _WorkArea_Channel_Height + 200;
                foreach (UC_Channel uc in Config_ChannelsList)
                {
                    uc.Height = workArea_CH_Heigth - 5;
                }
            }
            else if (ch > 4)
            {
                decimal d = (decimal)ch / 2;
                if (d < 4)
                { multiplier = 4; }
                else
                { multiplier = (int)(Math.Round(d, MidpointRounding.AwayFromZero)); }
            }
            int width = _Rand_Width + (_WorkArea_Width * multiplier);
            int min_height = _HeaderFooter_Height + _Info_Height + workArea_CH_Heigth + _WorkArea_Info_Height + _Rand_Heigth;
            int heigth = min_height;
            if (width < _Min_Width)
            { width = _Min_Width; }
            if (ch > 4) { heigth += workArea_CH_Heigth; }
            this.MaximumSize = new Size(width + 1, heigth + 1);
            this.MinimumSize = new Size(_Min_Width, min_height);
            this.Size = new Size(width, heigth);
        }
        #endregion GUI

        /***************************************************************************************
        ** Initialization:   Panels
        ****************************************************************************************/
        void Init()
        {
            clConfig.Load();
            Init_Main_Design(Config_Initvalues.CHquantity);
            Load_Panel_TT();
            Load_Panel_Betrieb();
            Init_Login();
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
        ** Initialization:   Load Panels
        ****************************************************************************************/
        #region Panels
        /// <summary>
        /// Config must be loaded befor started
        /// </summary>
        /// <param name="close"></param>
        private bool Load_Panel_TT(bool close = false)
        {
            if (close)
            {
                H_TT.Dispose();
                return true;
            }
            else
            {
                try
                {
                    string prodType_Table = "tProductionType";
                    if (!string.IsNullOrEmpty(Config_Initvalues.DB_ProdType_Table))
                    { prodType_Table = Config_Initvalues.DB_ProdType_Table; }
                    try
                    {
                        H_TT = new UC_TT_Item_Infos(false, selectBoxSearchModus: UC_TT_Item_Infos.SearchModus.TAG, userID: Config_ChangeParameters.UserNameID,
                            prodtypeID: Config_ChangeParameters.ProductionTypeID, username: Config_ChangeParameters.UserName,
                            odbcEK_Init: Config_Initvalues.ODBC_Init, prodtypes: ProdTypes_Config, doCheck_TT: Config_Initvalues.DoCheckPass,
                            dbValuesActive: Config_Initvalues.DB_ProdType_Active, prodTypeTableName: prodType_Table);
                    }
                    catch
                    {
                        H_TT = new UC_TT_Item_Infos(false, UC_TT_Item_Infos.SearchModus.TAG, 0, 0);
                    }
                    try { PanInfo.Controls.Clear(); } catch { }
                    PanInfo.Controls.Add(H_TT);
                    H_TT.Dock = DockStyle.Fill;
                    AllocFont(H_TT, 8);
                    H_TT.BringToFront();
                    H_TT.ProductionType_Changed += H_TT_ProductionType_Changed;
                    CaliboxLibrary.Handler.ChangeOdbcEK(H_TT.ODBC_EK_Selected);
                    return true;
                }
                catch (Exception ex)
                {
                }
            }
            return false;
        }

        private void H_TT_ProductionType_Changed(object sender, EventArgs e)
        {
            var tt = sender as UC_TT_Item_Infos;
            CaliboxLibrary.Handler.ChangeOdbcEK(tt.ODBC_EK_Selected);
        }

        private void Load_Panel_Betrieb(bool close = false)
        {
            if (close)
            {
                H_UC_Betrieb.Dispose();
            }
            else
            {
                if (H_UC_Betrieb == null) { H_UC_Betrieb = new UC_Betrieb(); }
                if (!PanWork.Controls.Contains(H_UC_Betrieb))
                {
                    PanWork.Controls.Add(H_UC_Betrieb);
                    H_UC_Betrieb.Dock = DockStyle.Fill;
                }
                H_UC_Betrieb.BringToFront();
            }
        }

        private void Load_Panel_Config(bool close = false)
        {
            if (close) { H_UC_Config.Dispose(); }
            else
            {
                if (H_UC_Config == null) { H_UC_Config = new UC_Config_Main(); }
                if (!PanWork.Controls.Contains(H_UC_Config))
                {
                    PanWork.Controls.Add(H_UC_Config);
                    H_UC_Config.Dock = DockStyle.Fill;
                }
                H_UC_Config.BringToFront();
            }
        }
        #endregion Panels

        /***************************************************************************************
        ** Administrator:
        ****************************************************************************************/
        #region ADMIN
        private MTlogin.Login Login { get { return MTlogin.Login.Instance; } }


        private void Init_Login()
        {
            Admin_State();
        }

        private void Add_Admins()
        {
            if (!MachineName.Contains("CH04"))
            {
                string username = Environment.UserName;
                Login.Add_UserNameToDT(username, MTlogin.Login.ProfilNr.Administrator);
            }
            Login.Add_UserNameToDT("miguelito-1", MTlogin.Login.ProfilNr.Administrator);
        }



        private void LogIn()
        {
            H_AdminModus = Login.Login_State;
            string user = null;
            if (H_TT != null)
            {
                user = H_TT.UserName;
                try
                {
                    if (H_TT.Users_DT.Columns.Count > 0)
                    {
                        Login.UsersDT = H_TT.Users_DT.Copy();
                    }
                }
                catch { }
            }
            Add_Admins();
            Login.Open(this, user);
            H_AdminModus = Login.Login_State;
            Admin_State();
        }


        private void Admin_State()
        {
            Admin_Buttons_Design();
            if (!H_AdminModus)
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
            Btn_Betrieb.Visible = H_AdminModus;
            Btn_Config.Visible = H_AdminModus;
            Btn_Admin.BackColor = H_AdminModus ? Color.Gray : SystemColors.HotTrack;
        }

        private void Btn_Admin_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void Btn_Betrieb_Click(object sender, EventArgs e)
        {
            try
            {
                UC_Debug_CMD.Instance.Close_COMport();
            }
            catch { }
            Load_Panel_Betrieb();
            H_TT.SetFocus_Input();
        }

        private void Btn_Config_Click(object sender, EventArgs e)
        {
            Load_Panel_Config();
        }
        #endregion ADMIN
    }
}
