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
using static ReadCalibox.Handler;

namespace ReadCalibox
{
    public partial class UC_Debug_DGV : UserControl
    {
        /****************************************************************************************************
        ** Constructor
        ****************************************************************************************************/
        #region Constructor
        public UC_Debug_DGV()
        {
            InitializeComponent();
        }
        private void UC_DataReader_DGV_Load(object sender, EventArgs e)
        {
            Init_CoB_Selection();
            Init_NumUD();
        }
        #endregion Constructor

        /****************************************************************************************************
        ** Initialization:
        ****************************************************************************************************/
        

        private void Init_CoB_Selection()
        {
            CoB_DTselected.Items.Add(DTname_Meas);
            CoB_DTselected.Items.Add(DTname_Prog);
            CoB_DTselected.Items.Add(DTname_Limits);
            CoB_DTselected.Items.Add(DTname_Cal);
            CoB_DTselected.SelectedItem = DTname_Meas;
        }

        private void Init_NumUD()
        {
            NumUD_Ch.Value = 1;
            NumUD_Ch.Minimum = 1;
            NumUD_Ch.Maximum = Config_ChannelsList.Count()-1;
        }

        /****************************************************************************************************
        ** Data:
        ****************************************************************************************************/
        private int ChannelIndex = 0;


        private DataTable Get_Selection()
        {
            switch (CoB_DTselected.SelectedItem)
            {
                case DTname_Meas:
                    return Config_ChannelsList[ChannelIndex].DeviceCom.DT_Measurements.Copy();
                case DTname_Prog:
                        return Config_ChannelsList[ChannelIndex].DeviceCom.DT_Progress.Copy();
                case DTname_Limits:
                    return Config_ChannelsList[ChannelIndex].ItemValues.DT_Limits;
                case DTname_Cal:
                    return Config_ChannelsList[ChannelIndex].ItemValues.DT_tCalMeasVal;
                default:
                    break;
            }
            return null;
        }
        private void Load_DGV()
        {
            try
            {
                _DGV_Message.DataSource = Get_Selection();
                try { _DGV_Message.Sort(_DGV_Message.Columns[MN_meas_time_start], ListSortDirection.Descending); } catch { }
                _DGV_Message.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                if (_DGV_Message.Rows.Count > 0)
                {
                    _DGV_Message.Rows[0].Selected = true;
                    _DGV_Message.FirstDisplayedScrollingRowIndex = 0;
                    _DGV_Message.ClearSelection();
                    _DGV_Message.AutoResizeColumns();
                }
            }
            catch { }
        }

        private void _DGV_Message_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        /****************************************************************************************************
        ** Timer:
        ****************************************************************************************************/
        #region Timer
        private Timer _tmUpdateDGV;
        private Timer tmUpdateDGV
        {
            get
            {
                if(_tmUpdateDGV == null)
                {
                    _tmUpdateDGV = new Timer() { Interval = 1000 };
                    _tmUpdateDGV.Tick += new EventHandler(timer_Tick);
                }
                return _tmUpdateDGV;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Load_DGV();
                //if (_DGV_Message.Rows.Count > 0)
                //{
                //    try { _DGV_Message.Sort(_DGV_Message.Columns[MN_meas_time_start], ListSortDirection.Descending); } catch { }
                //    _DGV_Message.Rows[0].Selected = true;
                //    _DGV_Message.FirstDisplayedScrollingRowIndex = 0;
                //    _DGV_Message.ClearSelection();
                //}
            }
            catch { }
        }
        #endregion Timer

        /****************************************************************************************************
        ** Buttons:
        ****************************************************************************************************/
        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (_CkB_Autorefresh.Checked)
                {
                    _CkB_Autorefresh.Checked = false;
                }
                Load_DGV();
            }
            catch { }
        }

        private void Autorefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (_CkB_Autorefresh.Checked)
            {
                Load_DGV();
                tmUpdateDGV.Start();
            }
            else
            {
                tmUpdateDGV.Stop();
            }
        }

        private void NumUD_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ChannelIndex = ((int)NumUD_Ch.Value)-1;
                Load_DGV();
            }
            catch { }
        }

        private void CoB_DTselected_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_DGV();
        }

        
    }
}
