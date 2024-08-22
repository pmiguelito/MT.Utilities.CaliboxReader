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
using static ReadCalibox.clConfig;
using static ReadCalibox.Handler;
using STDhelper;
using static STDhelper.clSTD;
using static STDhelper.clLogging;
using TT_Item_Infos;

namespace ReadCalibox
{
    public partial class UC_Config_Main : UserControl
    {
        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        public static DataGridView DGV_FTDI;
        public static TextBox TB_Path_MeasLog, TB_Path_Log;
        public static CheckBox CkB_LogMeas_Path_Active, CkB_LogMeas_DB_Active, CkB_Path_Log_Active;
        void ObjRef()
        {
            
            DGV_FTDI = _DGV_FTDI;
            
            TB_Path_MeasLog = _TB_LogMeas_Path;
            TB_Path_Log = _TB_LogError_Path;
            CkB_LogMeas_Path_Active = _CkB_LogMeas_Path_Active;
            CkB_Path_Log_Active = _CkB_LogError_Path_Active;
            CkB_LogMeas_DB_Active = _CkB_LogMeas_DB_Active;
        }
        public UC_Config_Main()
        {
            InitializeComponent();
            ObjRef();
        }
        private void UC_Config_Load(object sender, EventArgs e)
        {
            Init_UC_Config();
        }
        /***************************************************************************************
        * Initialization:   
        ****************************************************************************************/
        private void Init_Panel()
        {
        }

        private void Error()
        {
            ErrorLineSeparator = true;
            ErrorPathLog = LogError_Path;
            ErrorPathLog_Active = LogError_Path_Active;
        }


        /***************************************************************************************
        * Configuration:  Load & Save
        ****************************************************************************************/

        public bool Load_Config(int chQuantity)
        {
            Load_Config_InitParameters();
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
            foreach (var uc in Config_PortList)
            {
                _FlowPanCOM.Controls.Add(uc);
            }
        }

        void Save_Config_Ports()
        {
            foreach(var uc in Config_PortList)
            {
                //var item = Config_ComPorts.ComPorts[uc.Ch_no];
                var item = Config_ComPorts.ComPorts[uc.Ch_Index];
                item.Active = uc.Active;

                //item.Ch_Name = uc.Ch_name;
                item.Ch_Name = uc.Ch_Title;

                //item.ModusFTDI = uc.ModusFTDI;
                item.ModusFTDI = uc.Ref.m_ComPortDetails.FTDI_Active;
                //item.FTDIname = uc.FTDIname;
                item.FTDIname = uc.Ref.m_ComPortDetails.DeviceSN;
                
                item.BeM = uc.BeM;

                //item.SerialPortName = uc.PortName;
                item.SerialPortName = uc.Ref.m_ComPortDetails.PortName;

                //item.BaudRate = uc.Baudrate;
                //item.COMreadDelay = uc.ReadDelay;
                //item.DataBits = uc.DataBits_Selected;
                //item.StopBits = uc.StopBits_Selected;
                item.BaudRate = uc.Ref.m_ComPortDetails.Baudrate;
                item.HandShake = uc.Ref.m_ComPortDetails.Handshake;
                item.StopBits = uc.Ref.m_ComPortDetails.StopBits;
                item.DataBits = uc.Ref.m_ComPortDetails.DataBits;
                item.Parity = uc.Ref.m_ComPortDetails.Parity;
                item.BufferReadLine = uc.Ref.m_ComPortDetails.ReadLine;
            }
            Register_ComPorts.Save();
        }

        /***************************************************************************************
        * Configuration:  BeM
        ****************************************************************************************/
        #region BeM
        private void Load_Config_BeM()
        {
            try { FlowPan_BeM.Controls.Clear(); } catch { }
            foreach (UC_BeM uc in Config_BeMsList)
            {
                FlowPan_BeM.Controls.Add(uc);
            }
        }

        private void Save_Config_BeM()
        {
            foreach (UC_BeM uc in Config_BeMsList)
            {
                uc.Save_BeM();
            }
            Register_BeM.Save();
        }
        #endregion BeM
        /***************************************************************************************
        * Configuration:    InitParameters
        ****************************************************************************************/
        #region InitParameters
        //private UC_Config_DB uc_ConfigDB_Init = new UC_Config_DB("Init Values");

        public string LogMeas_Path
        {
            get { return TB_Path_MeasLog.Text; }
            set { TB_Path_MeasLog.Text = value; }
        }
        public bool LogMeas_Path_Active
        {
            get { return CkB_LogMeas_Path_Active.Checked; }
            set { CkB_LogMeas_Path_Active.Checked = value; }
        }
        public bool LogMeas_DB_Active
        {
            get { return CkB_LogMeas_DB_Active.Checked; }
            set { CkB_LogMeas_DB_Active.Checked = value; }
        }

        public string LogError_Path
        {
            get { return TB_Path_Log.Text; }
            set { TB_Path_Log.Text = value; ErrorPathLog = value; }
        }
        public bool LogError_Path_Active
        {
            get { return CkB_Path_Log_Active.Checked; }
            set { CkB_Path_Log_Active.Checked = value; ErrorPathLog_Active = value; }
        }


        private UC_Config_DB _Uc_ConfigDB;
        private UC_Config_DB Uc_ConfigDB
        {
            get
            {
                if (_Uc_ConfigDB == null)
                {
                    _Uc_ConfigDB = Uc_Config_DB;
                }
                return _Uc_ConfigDB;
            }
        }
        public string ODBC_Initial
        {
            get { return Uc_ConfigDB.ODBC_Init; }
            set { Uc_ConfigDB.ODBC_Init = value; }
        }

        public bool ODBC_Initial_Found
        {
            get { return Uc_ConfigDB.ODBC_Exists; }
        }

        public string ProcNr
        {
            get { return Uc_ConfigDB.ProcNr; }
            set { Uc_ConfigDB.ProcNr = value; }
        }

        public string ProdType_Table
        {
            get { return Uc_ConfigDB.ProdType_Table; }
            set { Uc_ConfigDB.ProdType_Table = value; }
        }

        private string DoCheckPassTXT
        {
            get { return Uc_ConfigDB.DoCheckPassTXT; }
            set { Uc_ConfigDB.DoCheckPassTXT = value; }
        }

        public bool DB_ProdType_Active
        {
            get { return Uc_ConfigDB.DB_ProdType_Active; }
            set { Uc_ConfigDB.DB_ProdType_Active = value; }
        }

        public int Ch_Quantity
        {
            get { return (int)NUD_Ch_Quantity.Value; }
            set
            {
                if (value > 0 && value < 21)
                { NUD_Ch_Quantity.Value = value; }
                else
                { NUD_Ch_Quantity.Value = 1; }
            }
        }

        private void Load_Config_InitParameters()
        {
            Element_InitParameters element = Config_Initvalues;
            Ch_Quantity = element.CHquantity;
            LogError_Path = element.LogError_Path;
            LogError_Path_Active = element.LogError_Active;
            LogMeas_Path_Active = element.LogMeas_Path_Active;
            LogMeas_DB_Active = element.LogMeas_DB_Active;
            LogMeas_Path = element.LogMeas_Path;

            ODBC_Initial = element.ODBC_Init;
            ProdType_Table = element.DB_ProdType_Table;
            ProcNr = element.ProcNr;
            DoCheckPassTXT = element.DoCheckPass;
            DB_ProdType_Active = element.DB_ProdType_Active;

        }
        private void Save_Config_InitParameters()
        {
            Element_InitParameters element = Config_Initvalues;
            element.CHquantity = Ch_Quantity;
            element.LogError_Path = LogError_Path;
            element.LogError_Active = LogError_Path_Active;
            element.LogMeas_Path_Active = LogMeas_Path_Active;
            element.LogMeas_Path = LogMeas_Path;
            element.LogMeas_DB_Active = LogMeas_DB_Active;

            element.ODBC_Init = ODBC_Initial;
            element.DB_ProdType_Active = DB_ProdType_Active;
            element.DB_ProdType_Table = ProdType_Table;
            element.ProcNr = ProcNr.ToString();
            element.DoCheckPass = DoCheckPassTXT;

            Register_InitParameters.Save();
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

