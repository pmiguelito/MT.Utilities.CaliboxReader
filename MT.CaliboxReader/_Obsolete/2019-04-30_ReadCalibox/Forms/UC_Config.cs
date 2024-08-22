using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Configuration;
using System.Collections.Specialized;
using FTDI;
using static ReadCalibox.clConfig;
using STDhelper;
using static STDhelper.clMTcolors;
using static STDhelper.clLogging;
using TT_Item_Infos;

namespace ReadCalibox
{
    public partial class UC_Config : UserControl
    {
        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        public static DataGridView DGV_FTDI;
        public static TextBox TB_Path_MeasLog, TB_Path_Log;
        public static CheckBox CkB_Path_MeasLog_Active, CkB_Path_Log_Active;
        void ObjRef()
        {
            DGV_FTDI = _DGV_FTDI;
            DGV_FTDI.DataSource = DTFTDI;
            TB_Path_MeasLog = _TB_Path_MeasLog;
            TB_Path_Log = _TB_Path_Log;
            CkB_Path_MeasLog_Active = _CkB_Path_MeasLog_Active;
            CkB_Path_Log_Active = _CkB_Path_Log_Active;
        }
        #region Constructor
        public UC_Config()
        {
            InitializeComponent();
            ObjRef();
        }
        private void UC_Config_Load(object sender, EventArgs e)
        {
            Init_UC_Config();
        }
        #endregion Constructor
        /***************************************************************************************
        * Initialization:   
        ****************************************************************************************/
        private void Init_Panel()
        {
           
            //Form_Main.AllocFont_STD(this);
        }

        private void Error()
        {
            ErrorLineSeparator = true;
            PathLog = Path_Log;
            PathLog_Active = Path_Log_Active;
        }

        /***************************************************************************************
        * Configuration:  Load & Save
        ****************************************************************************************/

        public bool Load_Config(int chQuantity)
        {
            Load_Config_InitParameters();
            //Load_Config_ChangeParameters();
            Load_Config_BeM();
            Load_Config_Ports();
            return true;
        }

        public bool Save_Config()
        {
            Save_Config_Ports();
            Save_Config_InitParameters();
            Save_Config_BeM();
            return true;
        }

        /***************************************************************************************
        * Configuration:  SerialPorts
        ****************************************************************************************/
        void Load_Config_Ports()
        {
            try { _FlowPanCOM.Controls.Clear(); } catch { }
            foreach (UC_COM uc in Config_PortList)
            {
                _FlowPanCOM.Controls.Add(uc);
            }
        }

        void Save_Config_Ports()
        {
            foreach(UC_COM uc in Config_PortList)
            {
                var item = Config_ComPorts.ComPorts[uc.Ch_no];
                item.Ch_Name = uc.Ch_name;
                item.COMreadDelay = Convert.ToInt32(uc.ReadDelay);
                item.ModusFTDI = uc.ModusFTDI;
                item.Active = uc.Active;
                item.FTDIname = uc.FTDIname;
                item.BaudRate = uc.Baudrate;
                item.SerialPortName = uc.PortName;
                item.BeM = uc.BeM;

                item.HandShake = uc.HandShake_Selected;
                item.DataBits = uc.DataBits_Selected;
                item.StopBits = uc.StopBits_Selected;
                item.Parity = uc.Parity_Selected;
            }
            Register_ComPorts.Save();
        }

        /***************************************************************************************
        * Configuration:  BeM
        ****************************************************************************************/
        #region BeM
        //Register_BeM Config_BeM { get { return Register_BeM.GetConfig(); } }
        //List<UC_BeM> uC_BeMs;
        void Load_Config_BeM()
        {
            try { FlowPan_BeM.Controls.Clear(); } catch { }
            foreach (UC_BeM uc in Config_BeMsList)
            {
                FlowPan_BeM.Controls.Add(uc);
            }
            //uC_BeMs = new List<UC_BeM>();
            //foreach (Element_BeM uc in Config_BeMList)
            //{
            //    UC_BeM be = new UC_BeM(uc);
            //    FlowPan_BeM.Controls.Add(be);
            //    uC_BeMs.Add(be);
            //}
        }

        void Save_Config_BeM()
        {
            //Config_BeMList = new List<Element_BeM>();
            foreach (UC_BeM uc in Config_BeMsList)
            {
                uc.Save();
                //Config_BeMsList.Add(uc.Selectedvalues);
            }
            Register_BeM.Save();
        }
        #endregion BeM
        /***************************************************************************************
        * Configuration:    InitParameters
        ****************************************************************************************/
        #region InitParameters
        public string Path_MeasLog
        {
            get { return TB_Path_MeasLog.Text; }
            set { TB_Path_MeasLog.Text = value; }
        }
        public bool Path_MeasLog_Active
        {
            get { return CkB_Path_MeasLog_Active.Checked; }
            set { CkB_Path_MeasLog_Active.Checked = value; }
        }

        public string Path_Log
        {
            get { return TB_Path_Log.Text; }
            set { TB_Path_Log.Text = value; PathLog = value; }
        }
        public bool Path_Log_Active
        {
            get { return CkB_Path_Log_Active.Checked; }
            set { CkB_Path_Log_Active.Checked = value; PathLog_Active = value; }
        }

        public string ODBC_Initial
        {
            get { return _TB_ODBC_Initial.Text; }
            set { _TB_ODBC_Initial.Text = value; ODBC_ProdTypes_GUI = value; }
        }
        public string ProdType_Table
        {
            get { return _TB_ProdType_Table.Text; }
            set { _TB_ProdType_Table.Text = value; }
        }
        public bool ODBC_Initial_Found
        {
            get { return ODBC_TT.ODBC.GetODBC_status(ODBC_Initial); }
        }

        private string ProcNo
        {
            get { return _TB_ProcNr.Text; }
            set { _TB_ProcNr.Text = value; }
        }
        public int ProcNr
        {
            get { return Convert.ToInt32(ProcNo); }
            set { ProcNo = value.ToString(); }
        }

        public int Ch_Quantity
        {
            get { return (int)NUD_Ch_Quantity.Value; }
            set
            {
                if (value > 4 && value < 21)
                { NUD_Ch_Quantity.Value = value; }
            }
        }

        private string DoCheckPassTXT { get { return _TB_DoCheckPass.Text; } set { _TB_DoCheckPass.Text = value; } }

        public bool DB_ProdType_Active
        {
            get { return _CkB_DB_ProdType_Active.Checked; }
            set { _CkB_DB_ProdType_Active.Checked = value; }
        }

        private void Load_Config_InitParameters()
        {
            Element_InitParameters element = Config_Initvalues;
            Path_Log = element.Log_Path;
            Path_Log_Active = element.Log_Active;
            Path_MeasLog_Active = element.MeasLog_Active;
            Path_MeasLog = element.MeasLog_Path;
            ODBC_Initial = element.ODBC_Init;
            ProdType_Table = element.DB_ProdType_Table;
            ProcNr = element.ProcNr;
            Ch_Quantity = element.CHquantity;
            DoCheckPassTXT = element.DoCheckPass;
            DB_ProdType_Active = element.DB_ProdType_Active;
        }
        private void Save_Config_InitParameters()
        {
            Element_InitParameters element = Config_Initvalues;
            element.Log_Path = Path_Log;
            element.Log_Active = Path_Log_Active;
            element.MeasLog_Active = Path_MeasLog_Active;
            element.MeasLog_Path = Path_MeasLog;
            element.ODBC_Init = ODBC_Initial;
            element.DB_ProdType_Active = DB_ProdType_Active;
            element.DB_ProdType_Table = ProdType_Table;
            element.ProcNr = ProcNr;
            element.CHquantity = Ch_Quantity;
            element.DoCheckPass = DoCheckPassTXT;
            //Form_Main.ProdType_Initvalues();
            Register_InitParameters.Save();
        }

        private void _TB_ProcNr_Leave(object sender, EventArgs e)
        {
            ProcNo = _TB_ProcNr.Text;
        }

        private void _CkB_DB_ProdType_Active_CheckedChanged(object sender, EventArgs e)
        {
            if(Form_Main.UC_TT != null && _CkB_DB_ProdType_Active.Focused)
            {
                if (!_CkB_DB_ProdType_Active.Checked)
                { TT_ProdType.clTTProdType.TTprodTypes = ProdTypes_Config; }
                Form_Main.UC_TT.DBvaluesActive = _CkB_DB_ProdType_Active.Checked;
                UC_Betrieb.Check_ODBC_Init();
            }
        }

        private void _TB_DoCheckPass_MouseLeave(object sender, EventArgs e)
        {
            if (Form_Main.UC_TT != null)
            {
                Form_Main.UC_TT.Do_check_TT = _TB_DoCheckPass.Text;
            }
        }

        private void _TB_ODBC_Initial_Leave(object sender, EventArgs e)
        {
            _TB_ODBC_Initial.ForeColor = ODBC_Initial_Found ? MT_rating_God_Active : MT_rating_Bad_Active;
        }

        #endregion InitParameters

        /***************************************************************************************
        * Comports:   Initialization
        ****************************************************************************************/
        public void Init_UC_Config()
        {
            Load_Config(Config_Initvalues.CHquantity);
            Load_FTDI();
        }
        
        public static int Log_LinesBuffer
        {
            get { return UC_DataRead.Log_LinesBuffer; }
            set { UC_DataRead.Log_LinesBuffer = value; }
        }

        /***************************************************************************************
        * Comports:   FTDI
        ****************************************************************************************/
        public static void Load_FTDI()
        {
            DGV_FTDI.DataSource = DTFTDI;
        }

        private void Btn_Load_FTDI_Click(object sender, EventArgs e)
        {
            Load_FTDI();
        }

        /***************************************************************************************
        * General:
        ****************************************************************************************/
        private void Btn_SaveConfig_Click(object sender, EventArgs e)
        {
            Save_Config();
        }

    }
}

