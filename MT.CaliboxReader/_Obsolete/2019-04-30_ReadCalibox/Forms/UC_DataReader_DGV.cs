using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCalibox
{
    public partial class UC_DataReader_DGV : UserControl
    {
        DataTable DT_Message { get { return UC_DataRead.DT_Message; } }

        #region Constructor
        public UC_DataReader_DGV()
        {
            InitializeComponent();
        }
        private void UC_DataReader_DGV_Load(object sender, EventArgs e)
        {
            Init_DGV();
            Init_Timer();
        }

        #endregion Constructor
        void Init_DGV()
        {
            _DGV_Message.DataSource = DT_Message;
            _DGV_Message.Sort(_DGV_Message.Columns[0], ListSortDirection.Descending);
            _DGV_Message.Columns[0].Width = 140;
            _DGV_Message.Columns[1].Width = 50;
            _DGV_Message.Columns[2].Width = 80;
            _DGV_Message.Columns[3].Width = 30;
            _DGV_Message.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _DGV_Message.ClearSelection();
            _DGV_Message.Enabled = false;
        }

        #region Timer
        static System.Windows.Forms.Timer UpdateTimer = new System.Windows.Forms.Timer();
        void Init_Timer()
        {
            UpdateTimer.Interval = 1000; // specify interval time as you want
            UpdateTimer.Tick += new EventHandler(timer_Tick);
        }
        static void TimerRunning(bool run)
        {
            if (UpdateTimer.Enabled)
            {
                if (!run)
                { UpdateTimer.Stop(); }
            }
            else if (run) { UpdateTimer.Start(); }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _DGV_Message.Refresh();
                _DGV_Message.ClearSelection();
                _DGV_Message.FirstDisplayedScrollingRowIndex = 0;
            }
            catch { }
        }


        #endregion Timer

        private void _Btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                _CkB_Autorefresh.Checked = false;
                _DGV_Message.ClearSelection();
            }
            catch { }
        }

        private void _CkB_Autorefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (_CkB_Autorefresh.Checked)
            {
                _DGV_Message.Enabled = false;
                _DGV_Message.DataSource = DT_Message;
                UpdateTimer.Start();
            }
            else
            {
                _DGV_Message.Enabled = true;
                _DGV_Message.DataSource = DT_Message.Copy();
                UpdateTimer.Stop();
            }
        }

        private void _DGV_Message_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
