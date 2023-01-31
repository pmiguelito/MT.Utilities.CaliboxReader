using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadCalibox.clConfig;

namespace ReadCalibox
{

    public partial class UC_BeM : UserControl
    {

        public Element_BeM Selectedvalues;

        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        public UC_BeM(Element_BeM values)
        {
            InitializeComponent();
            Selectedvalues = values;
            Init();
        }


        /***************************************************************************************
        * Initialization
        ****************************************************************************************/
        private void Init()
        {
            Fill_BaudRate();
            Load_BeM();
        }

        public void Load_BeM()
        {
            BeMname = Selectedvalues.BeM_Name;
            BeMdesc = Selectedvalues.BeM_Desc;
            BeMdelay = Selectedvalues.COMreadDelay;
            Baudrate = Selectedvalues.BaudRate;
            BeMreadLine = Selectedvalues.BufferReadLine;
        }

        public void Save_BeM()
        {
            Selectedvalues.BeM_Name = BeMname;
            Selectedvalues.BeM_Desc = BeMdesc;
            Selectedvalues.COMreadDelay = BeMdelay;
            Selectedvalues.BaudRate = Baudrate;
            Selectedvalues.BufferReadLine = BeMreadLine;
        }

        public int Index;
        public int Baudrate
        {
            get { return Convert.ToInt32(_CoB_BaudRate.Text); }
            set
            {
                string br = value.ToString();
                if (_CoB_BaudRate.Text != br)
                { _CoB_BaudRate.Text = br; }
            }
        }

        public string BeMname {get{ return _TB_BeM.Text; } set { _TB_BeM.Text = value; } }
        public string BeMdesc { get { return _TB_Desc.Text; } set { _TB_Desc.Text = value; } }
        public int BeMdelay { get { return Convert.ToInt32(_Tb_ReadDelay.Text); } set { _Tb_ReadDelay.Text = value.ToString(); } }

        public bool BeMreadLine { get { return _CkB_ReadLine.Checked; } set { _CkB_ReadLine.Checked = value; } }

        private void Fill_BaudRate()
        {
            _CoB_BaudRate.SelectedIndexChanged -= _CoB_BaudRate_SelectedIndexChanged;
            _CoB_BaudRate.BindingContext = new BindingContext();
            _CoB_BaudRate.DataSource = BaudRateList;
            _CoB_BaudRate.SelectedIndexChanged += _CoB_BaudRate_SelectedIndexChanged;
        }
        private void _CoB_BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CkB_FTDI_Click(object sender, EventArgs e)
        {

        }

        private void TB_ReadDelay_Leave(object sender, EventArgs e)
        {

        }
    }
}
