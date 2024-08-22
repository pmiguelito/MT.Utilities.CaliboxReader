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
using static ReadCalibox.clGlobals;
using static ReadCalibox.clConfig;
using static ReadCalibox.clLog;
using static ReadCalibox.clDeviceCom;

namespace ReadCalibox
{
    public partial class UC_Debug : UserControl
    {
        /***************************************************************************************
        * Constructor:  Instance to load from other elements
        ****************************************************************************************/
        #region UC Instance
        private static UC_Debug _instance;
        public static UC_Debug Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new UC_Debug(); }
                return _instance;
            }
        }
        #endregion UC Instance

        /***************************************************************************************
        * Constructor:
        ****************************************************************************************/
        public UC_Debug()
        {
            InitializeComponent();
        }

        private void UC_Debug_Load(object sender, EventArgs e)
        {
            Init();
        }

        /***************************************************************************************
        * Initialization:
        ****************************************************************************************/
        private void Init()
        {
            Init_CoB_Port();
            Init_CoB_BaudRate();
            Init_CoB_CMD();
            Init_Port();
        }
        private void Init_CoB_Port()
        {
            CoB_COM.DataSource = SerialPortList;
            CoB_COM.SelectedIndex = 0;
        }

        private void Init_CoB_BaudRate()
        {
            CoB_BaudRate.DataSource = BaudRateList;
            CoB_BaudRate.SelectedIndex = 0;
        }
        private void Init_CoB_CMD()
        {
            CoB_CMD.DataSource = System.Enum.GetNames(typeof(clDeviceCom.opcode)).ToArray();
            CoB_BaudRate.SelectedIndex = 0;
        }

        private void Init_Port()
        {
            int baud = 19200;
            try { baud = Convert.ToInt32(CoB_BaudRate.Text); } catch { CoB_BaudRate.Text = baud.ToString(); }
            port = SerialPort_Init(CoB_COM.Text, baud);
            ThreadDR.Port = port;
        }

        /***************************************************************************************
        * SerialPort:
        ****************************************************************************************/
        static SerialPort port = new SerialPort();

        /***************************************************************************************
        * PortReader:    SerialPort Read
        ****************************************************************************************/
        SerialReaderThread _ThreadDR;
        SerialReaderThread ThreadDR
        {
            get
            {
                if(_ThreadDR == null)
                { _ThreadDR = Init_DatReader(); }
                return _ThreadDR;
            }
        }

        SerialReaderThread Init_DatReader()
        {
            SerialReaderThread SRT = new SerialReaderThread(port, 25, false);
            SRT.DataReceived += ThreadDataReceived; //  ThreadDataReceived;
            return SRT;
        }
        void ThreadDataReceived(object s, EventArgs e)
        {
            var a = (DataEventArgs)e;
            Task.Factory.StartNew(() =>
            {
                if (InvokeRequired)
                {
                    try { this.Invoke((MethodInvoker)delegate { Tb_Info.Text += a.Data; Tb_Info.SelectionStart = Tb_Info.Text.Length;
                        Tb_Info.ScrollToCaret();
                    }); }
                    catch (Exception ex)
                    {  }
                }
                else
                { Tb_Info.Text += a.Data; Tb_Info.SelectionStart = Tb_Info.Text.Length;
                    Tb_Info.ScrollToCaret();
                }
            });
            
        }

        private void CMD_Send()
        {
            try
            {
                ThreadDR.Send(CoB_CMD.Text);
            }
            catch (Exception e)
            {
                Tb_Info.Text += e.Message + Environment.NewLine;
            }
        }


        /***************************************************************************************
        * Events:
        ****************************************************************************************/
        clDeviceCom DeviceCom = new clDeviceCom();


        private void CoB_BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Init_Port();
        }

        private void CoB_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            Init_Port();
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            CMD_Send();
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            Tb_Info.Text = "";
        }

        private void Btn_StopRead_Click(object sender, EventArgs e)
        {
            ThreadDR.Stop();
        }
    }
}
