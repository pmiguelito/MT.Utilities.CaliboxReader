using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadCalibox.Handler;
using static ReadCalibox.clConfig;
using STDhelper;
using static STDhelper.clSTD;

namespace ReadCalibox
{
    public partial class UC_Config_DB : UserControl
    {

        /*******************************************************************************************************
        ** Instance:
        ********************************************************************************************************/
        private static UC_Config_DB _Instance;
        public static UC_Config_DB Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new UC_Config_DB();
                }
                return _Instance;
            }
        }


        /*******************************************************************************************************
        ** Constructor:
        ********************************************************************************************************/
        public UC_Config_DB()
        {
            InitializeComponent();
            Kategorie_Name = "Init Values"; 
            GrB_ODBC.Text = Kategorie_Name;
            BackColorReadOnly = TB_ODBC_EK.BackColor;
            _Instance = this;
        }

        private void UC_Config_DB_Load(object sender, EventArgs e)
        {
            Init();
        }

        /*******************************************************************************************************
        ** Properties:
        ********************************************************************************************************/
        public string Kategorie_Name { get; set; }

        private Color BackColorReadOnly;

        /*******************************************************************************************************
        ** Initialization:
        ********************************************************************************************************/
        private void Init()
        {
            if (H_TT != null)
            {
                H_TT.ProductionType_Changed += new EventHandler(ProductionType_Changed);
            }
            Load_GUI();
            Check_ODBC(ODBC_Init);
        }

        /*******************************************************************************************************
        ** Events:
        ********************************************************************************************************/
        private void ProductionType_Changed(object sender, EventArgs e)
        {
            Load_GUI();
        }


        /*******************************************************************************************************
        ** Load: GUI
        ********************************************************************************************************/
        public void Load_GUI()
        {
            TB_ODBC_Initial.Text = ODBC_Init;
            ODBC_CheckIfExists(TB_ODBC_Initial);
            CkB_DB_ProdType_Active.Checked = DB_ProdType_Active;
            TB_ProdType_Table.Text = ProdType_Table;
            if (H_TT != null)
            {
                Grb_Values.Text = $"ID: {H_TT.ProdType_Selected.ProductionType_ID} -- {H_TT.ProdType_Selected.desc}";
                TB_ODBC_EK.Text = H_TT.ProdType_Selected.ODBC_EK;
                ODBC_CheckIfExists(TB_ODBC_EK);
                TB_ODBC_TT.Text = H_TT.ProdType_Selected.ODBC_TT;
                ODBC_CheckIfExists(TB_ODBC_TT);

                Ckb_GenerateSN.Checked = H_TT.ProdType_Selected.generate_serie_nr;
                Ckb_Log_Path_TT.Checked = H_TT.ProdType_Selected.log_active_TT;
                TB_Log_Path_TT.Text = H_TT.ProdType_Selected.log_pathName_TT;
                TB_UserTable.Text = H_TT.ProdType_Selected.user_tableName_EK;
            }
            else
            {
                Grb_Values.Text = "Database Values";
                TB_ODBC_EK.Text = "";
                TB_ODBC_TT.Text = "";
                TB_Log_Path_TT.Text = "";
                TB_UserTable.Text = "";
                Ckb_GenerateSN.Checked = false;
                Ckb_Log_Path_TT.Checked = false;
            }
            if (!DB_ProdType_Active)
            {
                TB_ProcNr.Text = ProcNr;
                TB_DoCheckPass.Text = DoCheckPassTXT;
            }
            else if(H_TT != null)
            {
                TB_ProcNr.Text = H_TT.ProdType_Selected.proc_nr_TT.ToString();
                TB_DoCheckPass.Text = H_TT.ProdType_Selected.do_check_TT;
            }

        }

        /*******************************************************************************************************
        ** Check:
        ********************************************************************************************************/
        #region check
        private void Check_ODBC(string odbc)
        {
            ODBC_Exists = Check_ODBC_Exists(odbc);
            if (ODBC_Exists)
            {
                ODBC_Tables_DT = ODBC_TT.ODBC.DB_Get_TableNames(odbc, out ODBC_Tables_DT_Count);
                CoB_Tables_Load();
                Check_Table(ProdType_Table);
            }
        }

        private void Check_Table(string table)
        {
            if (ODBC_Exists && ODBC_Tables_DT_Count > 1)
            {
                if (CoB_Tables_Selection(ProdType_Table))
                {
                    TT_ProdTypeMode();
                }
                else
                {
                    DB_Modus();
                }
                Load_GUI();
            }
        }
        #endregion check

        /*******************************************************************************************************
        ** ODBC:
        ********************************************************************************************************/
        #region ODBCinit
        public string ODBC_Init { get; set; }

        public bool ODBC_Exists { get; private set; }
        
        private bool Check_ODBC_Exists(string odbc)
        {
            return ODBC_TT.ODBC.GetODBC_status(odbc);
        }
        private void TB_ODBC_Initial_Leave(object sender, EventArgs e)
        {
            string odbc = TB_ODBC_Initial.Text.Trim();
            TB_ODBC_Initial.Text = odbc;
            if (ODBC_Init == odbc) { return; }
            ODBC_Init = odbc;
            Check_ODBC(ODBC_Init);
        }

        private void ODBC_CheckIfExists(TextBox tb)
        {
            tb.BackColor = Check_ODBC_Exists(tb.Text) ? STDhelper.clSTD.Rating(Rating_sel.good) : STDhelper.clSTD.Rating(Rating_sel.bad);
        }

        #endregion ODBCinit


        /*******************************************************************************************************
        ** ProdType:    Modus
        ********************************************************************************************************/
        #region DBmodus
        public bool DB_ProdType_Active { get; set; }

        private void CkB_DB_ProdType_Active_CheckedChanged(object sender, EventArgs e)
        {
            if (!CkB_DB_ProdType_Active.Focused) { return; }
            DB_ProdType_Active = CkB_DB_ProdType_Active.Checked;
            if (H_TT == null) { return; }
            if (DB_ProdType_Active)
            { Check_Table(ProdType_Table); }
            else
            {
                DB_Modus();
            }
        }

        private void TT_ProdTypeMode()
        {
            try
            {
                bool dbModus = DB_ProdType_Active;
                if (!dbModus)
                {
                    TT_ProdType.clTTProdType.TTprodTypes = ProdTypes_Config;
                }
                ProdTypeTT_Change(ODBC_Init, ProdType_Table);
            }
            catch { }
            DB_Modus();
        }

        private void DB_Modus()
        {
            bool dbModus = DB_ProdType_Active;
            CoB_Tables.Enabled = !dbModus;
            TB_DoCheckPass.ReadOnly = dbModus;
            TB_ProcNr.ReadOnly = dbModus;
        }
        #endregion DBmodus

        /*******************************************************************************************************
        ** ProdType:    Table
        ********************************************************************************************************/
        #region ProdType_Table
        public string ProdType_Table { get; set; }
        private DataTable ODBC_Tables_DT;
        private int ODBC_Tables_DT_Count;
        private void CoB_Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CoB_Tables.Focused) { return; }
            ProdType_Table = CoB_Tables.Text;
            Check_ODBC(ODBC_Init);
            //Check_Table(ProdType_Table);
        }

        private void CoB_Tables_Load()
        {
            CoB_Tables.SelectedIndexChanged -= CoB_Tables_SelectedIndexChanged;
            CoB_Tables.DisplayMember = "TABLE_NAME";
            CoB_Tables.ValueMember = "TABLE_NAME";
            CoB_Tables.DataSource = ODBC_Tables_DT;
            CoB_Tables.SelectedIndexChanged += CoB_Tables_SelectedIndexChanged;
        }

        private bool CoB_Tables_Selection(string table)
        {
            if(CoB_Tables.Text == table) { return false; }
            bool changed = false;
            CoB_Tables.SelectedIndexChanged -= CoB_Tables_SelectedIndexChanged;
            if (ODBC_Tables_DT.Select($"TABLE_NAME = '{table}'").Length > 0)
            {
                if (CoB_Tables.SelectedValue.ToString() != table)
                {
                    CoB_Tables.SelectedValue = table;
                    ProdType_Table = CoB_Tables.Text;
                    changed = true;
                }
            }
            else
            {
                var t = ODBC_Tables_DT.Select($"TABLE_NAME like '%productiontype%'");
                if (t.Length > 0)
                {
                    table = t[0][2].ToString();
                    if (CoB_Tables.SelectedValue.ToString() != table)
                    {
                        CoB_Tables.SelectedValue = table;
                        ProdType_Table = CoB_Tables.Text;
                        changed = true;
                    }
                }
                else
                {
                    CoB_Tables.SelectedIndex = 0;
                    changed = true;
                }
            }
            CoB_Tables.SelectedIndexChanged += CoB_Tables_SelectedIndexChanged;
            return changed;
        }

        private void ProdTypeTT_Change(string odbc, string table)
        {
            if (H_TT != null)
            {
                H_TT.ProdType_Change(odbc, table);
            }
        }

        #endregion ProdType_Table

        /*******************************************************************************************************
        ** ProcNr:
        ********************************************************************************************************/
        #region ProcNr
        public string ProcNr { get; set; }

        private void TB_ProcNr_Leave(object sender, EventArgs e)
        {
            ProcNr = TB_ProcNr.Text;
            if(H_TT != null)
            {
                H_TT.ProdType_Selected.proc_nr_TT = H_TT.ParseStringToInt(ProcNr);
            }
        }
        #endregion ProcNr

        /*******************************************************************************************************
        ** DoCheckPass:
        ********************************************************************************************************/
        #region DoCheckPass
        public string DoCheckPassTXT { get; set; }
        private void TB_DoCheckPass_MouseLeave(object sender, EventArgs e)
        {
            DoCheckPassTXT = TB_DoCheckPass.Text;
            if (H_TT != null)
            {
                H_TT.Do_check_TT = TB_DoCheckPass.Text;
            }
        }

        #endregion DoCheckPass
    }
}
