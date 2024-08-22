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
using static ReadCalibox.clConfig;
using static ReadCalibox.clLog;
using static ReadCalibox.clDeviceCom;
using static ReadCalibox.clHandler;

namespace ReadCalibox
{
    public partial class UC_Debug : UserControl
    {
        /***************************************************************************************
        * Constructor:  Instance to load from other elements
        ****************************************************************************************/
        #region UC Instance
        private UC_Debug _instance;
        public UC_Debug Instance
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
            CoB_CMD.DataSource = System.Enum.GetNames(typeof(opcode)).ToArray();
            CoB_BaudRate.SelectedIndex = 0;
        }

        private void Init_Port()
        {
            Close_COMport();
            int baud = 19200;
            try { baud = Convert.ToInt32(CoB_BaudRate.Text); } catch { CoB_BaudRate.Text = baud.ToString(); }
            port = SerialPort_Init(CoB_COM.Text, baud);
            ThreadDR.Port = port;
        }

        /***************************************************************************************
        * SerialPort:
        ****************************************************************************************/
        static SerialPort port = new SerialPort();

        static void Close_COMport()
        {
            if (port.IsOpen) { port.Close(); }
        }

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

        string temp;
        void ThreadDataReceived(object s, EventArgs e)
        {
            var a = (DataEventArgs)e;
            Task.Factory.StartNew(() =>
            {
                string response = a.Data;
                if (!CkB_Parse.Checked)
                {
                    temp += response;
                    StringBuilder sb = new StringBuilder();
                    again:
                    if (temp.Contains("\n"))
                    {
                        int i = temp.LastIndexOf('\n');
                        while (i==0)
                        {
                            temp = temp.Substring(1); i = temp.IndexOf('\n');
                        }
                    string anf = "";
                    if (i == -1) { anf = temp; temp = ""; }
                    else
                    {
                        try { anf = temp.Substring(0, i).Trim(); } catch { }
                        temp = temp.Substring(i);
                        if (temp == "\n") { temp = ""; }
                    }
                    
                        if (anf != "")
                        {
                            DeviceResponse dr = new DeviceResponse(CMD, anf);
                            foreach (DeviceResponseValues drv in dr.ResponseList)
                            {
                                sb.Append(drv.ResponseParsed.Replace(":\t",": ").Replace("\t","; ")+Environment.NewLine); 
                            }
                        }
                        if(temp.Length>0)
                        { goto again; }
                        Message(sb.ToString());
                    }
                }
                else
                {
                    Message(response);
                }
            });
            
        }
        private void Message(string message)
        {
            if (InvokeRequired)
            {
                Tb_Info.BeginInvoke(new Action<string>(Message), message);
            }
            else
            {
                Tb_Info.Text += message;
                Tb_Info.SelectionStart = Tb_Info.Text.Length;
                Tb_Info.ScrollToCaret();
                Tb_Info.Refresh();
            }
        }

        private void CMD_Send()
        {
            try
            {
                CMD = CoB_CMD.Text;
                ThreadDR.Send(CoB_CMD.Text);
            }
            catch (Exception e)
            { Tb_Info.Text += e.Message + Environment.NewLine; }
        }


        /***************************************************************************************
        * Events:
        ****************************************************************************************/
        clDeviceCom DeviceCom = new clDeviceCom();
        string CMD;

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
            Close_COMport();
            ThreadDR.Stop();
        }
    }
}
