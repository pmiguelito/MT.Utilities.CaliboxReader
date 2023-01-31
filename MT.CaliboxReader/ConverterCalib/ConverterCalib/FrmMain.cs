using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConverterCalib
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            SWID();
            LoadConfig();
            LoadTabs();
        }
        /*****************************************************************************
        * Software ID:
        '****************************************************************************/
        private void SWID()
        {
            LblSwTitle.Text = "Converter Calibration";
            this.Name = LblSwTitle.Text;
            this.Text = LblSwTitle.Text.Replace(" ","");
            LblSwVersion.Text = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        /*****************************************************************************
        * Load:     Configuration
        '****************************************************************************/
        private void LoadConfig()
        {
            var prop = Properties.Settings.Default;
            Globals.Debugging = prop.Debugging;
            Globals.Config1Wire = new OneWireUI.UcConfig(1, prop.PortName) { Name = "Config1Wire"};
            Globals.Config1Wire.Com1.SensorInfos.Path_Tdl = prop.PathTDL;
            Globals.UcCalib = new UcCalib() { Name = "Calibration"};
        }

        private void LoadTabs()
        {
            TabCtrMain.TabPages.Clear();
            InitTab(TabCtrMain, Globals.UcCalib);
            InitTab(TabCtrMain, Globals.Config1Wire);
        }


        private void InitTab(TabControl tb, UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            TabPage tp = new TabPage(uc.Name);
            tb.Controls.Add(tp);
            if (!tp.Controls.Contains(uc))
            { tp.Controls.Add(uc); }
            uc.BringToFront();
        }


        /*****************************************************************************
        * Configuration:
        '****************************************************************************/
        private void SaveConfig()
        {
            var prop = Properties.Settings.Default;
            prop.PortName = Globals.Config1Wire.Com1.SensorInfos.ComILink.Port.PortName;
            prop.PathTDL = Globals.UcCalib.TbPathTdl.Text;
            prop.Debugging = Globals.Debugging;
            prop.Save();
            Globals.Config1Wire.SaveConfig();
        }


        /*****************************************************************************
        * Exit
        '****************************************************************************/
        private void BtnExit_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private void TabCtrMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Globals.Config1Wire.Com1.SensorInfos.Path_Tdl != Properties.Settings.Default.PathTDL)
            { 
                Globals.Config1Wire.Com1.SensorInfos.Path_Tdl = Properties.Settings.Default.PathTDL;
                Globals.Config1Wire.Com1.SensorInfos.ReadSensorInfos();

            }
        }
    }
}
