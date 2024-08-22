using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCalibox
{
    public partial class frmSplash : Form
    {
        public frmSplash(string swname, string swversion, Form main)
        {
            InitializeComponent();
            Set_SWversion(swversion);
            SWname = swname;
            WaitForm = main;
            
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            lbl_Version.Text = SWversion;
            lbl_Titel.Text = SWname;
            Start();
        }

        /***************************************************************************************
        * Properties:
        ****************************************************************************************/
        public Form WaitForm { get; set; }
        public string SWname { get; set; }
        public string SWversion { get; set; }

        public double TimeOut_ms { get; set; } = 60000;


        public DateTime RunningTimeStart { get; private set; }
        public DateTime AbortTime { get; private set; }

        private void Set_SWversion(string swv)
        {
            string v = swv.ToLower();
            if (!v.Contains("version"))
            {
                SWversion = $"Version: {swv}";
            }
            else
            {
                SWversion = swv;
            }
        }

        private void Start()
        {
            RunningTimeStart = DateTime.Now;
            AbortTime = RunningTimeStart.AddMilliseconds(TimeOut_ms);
            this.BringToFront();
            timProgBar.Start();
        }



        /***************************************************************************************
        * Timer:
        ****************************************************************************************/
        private void TimProgBar_Tick(object sender, EventArgs e)
        {
            progBar.Increment(1);
            if(progBar.Value == 100)
            {
                progBar.Value = 0;
            }
        }

    }
}
