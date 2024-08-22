using CaliboxLibrary;
using CaliboxLibrary.BoxCommunication.CMDs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadCalibox.clConfig;

namespace ReadCalibox
{
    public partial class UC_Debug_CMD : UserControl
    {
        /***************************************************************************************
        * Constructor:  Instance to load from other elements
        ****************************************************************************************/
        private static UC_Debug_CMD _Instance;
        public static UC_Debug_CMD Instance
        {
            get
            {
                if (_Instance == null)
                { _Instance = new UC_Debug_CMD(); }
                return _Instance;
            }
        }

        /***************************************************************************************
        * Constructor:
        ****************************************************************************************/
        public UC_Debug_CMD()
        {
            InitializeComponent();
        }

        private void UC_Debug_Load(object sender, EventArgs e)
        {
            Init();
            _Instance = this;
        }

        /***************************************************************************************
        * Initialization:
        ****************************************************************************************/
        private void Init()
        {
            PanelLimits.Height = 0;
            CoB_BaudRate.SelectedIndexChanged -= CoB_BaudRate_SelectedIndexChanged;
            CoB_COM.SelectedIndexChanged -= CoB_COM_SelectedIndexChanged;
            Init_CoB_Port();
            Init_CoB_BaudRate();
            Init_CoB_CMD();
            Init_Port();
            Init_CoB_CMDp();
            Init_CommandSends();

            CoB_BaudRate.SelectedIndexChanged += CoB_BaudRate_SelectedIndexChanged;
            CoB_COM.SelectedIndexChanged += CoB_COM_SelectedIndexChanged;
        }

        private void Init_CoB_Port()
        {
            var list = SerialPortList.ToList();
            list.Add("...Load");
            CoB_COM.DataSource = list;
            CoB_COM.SelectedIndex = 0;
        }

        private int _BaudDefault = 19200;
        private void Init_CoB_BaudRate()
        {
            CoB_BaudRate.DataSource = BaudRateList;
            CoB_BaudRate.Text = _BaudDefault.ToString();
        }
        private void Init_CoB_CMD()
        {
            var ar = System.Enum.GetNames(typeof(OpCode));
            List<string> list = new List<string>();
            list.Add("#RDPG");
            list.Add("#RDBX");
            foreach (var item in ar)
            {
                if (item.StartsWith("S") || item.StartsWith("G"))
                {
                    list.Add(item);
                }
            }

            list = list.OrderBy(x => x).ToList();
            CoB_CMD.DataSource = list;
        }

        private void Init_CoB_CMDp()
        {
            var ar = System.Enum.GetNames(typeof(CMDs));
            CobCMDp.DataSource = ar;
        }

        private void CoB_BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Init_Port();
        }

        private void CoB_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoB_COM.Text.Contains("Load"))
            {
                Init_CoB_Port();
            }
            else
            {
                Init_Port();
            }
        }
        /***************************************************************************************
        * SerialPort:
        ****************************************************************************************/
        private DeviceCom Device = new DeviceCom(-1);
        private void Init_Port()
        {
            Close_COMport();
            if (string.IsNullOrEmpty(CoB_BaudRate.Text))
            {
                CoB_BaudRate.Text = _BaudDefault.ToString();
            }
            int baud = _BaudDefault;
            try { baud = Convert.ToInt32(CoB_BaudRate.Text); } catch { CoB_BaudRate.Text = baud.ToString(); }
            if (Device != null)
            {
                Device.DataReceived -= Device_DataReceived;
            }
            var port = SerialPort_Init(CoB_COM.Text, baud);
            Device.ChangePort(port, channelNo: -1);
            Device.DataReceived += Device_DataReceived;
        }

        /***************************************************************************************
        * SerialPort:   READ
        ****************************************************************************************/
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private void CMDSendCommand(object sender, EventRoutingArgs e)
        {
            Device.Send(e.Command);
        }

        private void Device_DataReceived(object sender, EventDataArgs e)
        {
            Parse(e);
        }

        private void Parse(EventDataArgs e)
        {
            try
            {
                DeviceResponseValues drv = e.DeviceResponseValue;
                if (CkB_Parse.Checked)
                {
                    if (drv.Response.Length > 0)
                    {
                        Message(drv.Response);
                    }
                }
                else if (drv.ResponseParsed != null)
                {
                    if (drv.ResponseParsed.Length > 0)
                    {
                        Message(drv.ResponseParsed);
                    }
                }
            }
            catch
            {

            }
        }

        private void Message(string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { Message(message); });
                return;
            }
            try
            {
                Tb_Info.Text += $"{watch.Elapsed.TotalSeconds:000.000}\t{message}{Environment.NewLine}";
                Tb_Info.SelectionStart = Tb_Info.Text.Length;
                Tb_Info.ScrollToCaret();
                Tb_Info.Refresh();
            }
            catch { }
        }
        /***************************************************************************************
        * SerialPort:
        ****************************************************************************************/

        public void Close_COMport()
        {
            Device.Close();
        }

        /***************************************************************************************
        * SerialPort:   SEND
        ****************************************************************************************/
        private Task CMD_SendAsync(string cmd, bool timerReset)
        {
            return Task.Run(() =>
            {
                CMD_Send(cmd, timerReset);
            });
        }

        private void CMD_Send(string cmd, bool timerReset)
        {
            try
            {
                CMD = cmd;
                if (timerReset)
                {
                    watch.Restart();
                }
                OpCode opcode = OpCode.Error;
                if (CMD.StartsWith("#"))
                {
                    var i = CMD.IndexOf(" ");
                    if (i > -1)
                    {
                        string op = CMD.Substring(1, i).Trim();
                        Enum.TryParse(op, out opcode);
                        CMD = CMD.Substring(i + 1);
                    }
                }
                else
                {
                    Enum.TryParse(CMD, out opcode);
                    CMD = null;
                }
                Device.Send(opcode, CMD);
                Message($"cmd send: {Device.CMD_Sended}");
                CMD = Device.CMD_Sended;
            }
            catch (Exception e)
            {
                Message(e.Message);
            }
        }
        private void Btn_Send_Click(object sender, EventArgs e)
        {
            Enum.TryParse(CoB_CMD.Text, out OpCode commands);
            MultiCommands(commands);
            //CMD_Send(CoB_CMD.Text, timerReset: true);
        }

        private void BtnSendp_Click(object sender, EventArgs e)
        {
            if ((_CMDSequences?.IsRunning ?? false) == false)
            {
                watch.Restart();
                //Tb_Info.Clear();
            }
            Enum.TryParse(CobCMDp.Text, out CMDs commands);
            MultiCommands(commands);
        }

        /***************************************************************************************
        * Events:   Ports
        ****************************************************************************************/
        //clDeviceCom DeviceCom = new clDeviceCom();
        private string CMD;

        /***************************************************************************************
        * Events:   Messages
        ****************************************************************************************/
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            Tb_Info.Clear();
        }

        private void Btn_StopRead_Click(object sender, EventArgs e)
        {
            Close_COMport();
            //ThreadDR.Stop();
        }

        /***************************************************************************************
        * StateMachine:
        ****************************************************************************************/
        private CMDSequences _CMDSequences = new CMDSequences();
        private void Init_CommandSends()
        {
            if (_CMDSequences != null)
            {
                _CMDSequences.CommandSended -= CommandSended;
                _CMDSequences.CommandSended += CommandSended;
                return;
            }
            if (_CMDSequences == null)
            {
                _CMDSequences = new CMDSequences(Device);
                _CMDSequences.CommandSended += CommandSended;
            }
        }

        private string _LastCommandDescription;
        private void CommandSended(object sender, EventRoutingArgs e)
        {
            bool waitSet = false;
            string waitText = $"WAIT {e.Routine.WaitMilliseconds / 1000:0} sec";
            if (e.Routine.Description != _LastCommandDescription)
            {
                _LastCommandDescription = e.Routine.Description;
                if (_LastCommandDescription.StartsWith("["))
                {
                    if (e.Routine.WaitMilliseconds > 2000)
                    {
                        Message($"{e.Routine.Description} / {waitText}");
                        waitSet = true;
                    }
                    else
                    {
                        Message(e.Routine.Description);
                    }
                }
            }
            if (waitSet == false && e.Routine.WaitMilliseconds > 2000)
            {
                Message(waitText);
            }
            Instance.CMD_SendAsync(e.Command, timerReset: false);
        }

        private void MultiCommands(Enum command)
        {
            try
            {
                if (command is CMDs opCode)
                {
                    if (CMDs.ShowBoxLimits == opCode)
                    {
                        ShowBoxLimits(Device.ItemValues.DeviceLimits);
                        PanelLimits.Height = 130;
                        return;
                    }
                }

                _CMDSequences.Start(command);
            }
            catch { }
        }

        /***************************************************************************************
        * Calibration:  Load
        ****************************************************************************************/
        private void ShowBoxLimits(DeviceLimits deviceLimits)
        {
            //var dl = DR.BoxID.DeviceLimits;
            textBoxCAL_High_1.Text = deviceLimits.RawErrorCalibration.High1.ToString();
            textBoxCAL_High_2.Text = deviceLimits.RawErrorCalibration.High2.ToString();
            textBoxCAL_Low_1.Text = deviceLimits.RawErrorCalibration.Low1.ToString();
            textBoxCAL_Low_2.Text = deviceLimits.RawErrorCalibration.Low2.ToString();

            textBoxVER_High_1.Text = deviceLimits.Current.High1.ToString("0.0##");
            textBoxVER_High_2.Text = deviceLimits.Current.High2.ToString("0.0##");
            textBoxVER_Low_1.Text = deviceLimits.Current.Low1.ToString("0.0##");
            textBoxVER_Low_2.Text = deviceLimits.Current.Low2.ToString("0.0##");

            textBoxMB1Zero.Text = deviceLimits.RawVal.Low1.ToString();
            textBoxMB1_175nA.Text = deviceLimits.RawVal.Low2.ToString();
            textBoxMB2_175nA.Text = deviceLimits.RawVal.High1.ToString();
            textBoxMB2_4700nA.Text = deviceLimits.RawVal.High2.ToString();

            lblCurr0.Text = deviceLimits.Current.Low2.ToString("0.0##");
            lblCurr1.Text = deviceLimits.Current.High1.ToString("0.0##");
            lblCurr2.Text = deviceLimits.Current.High2.ToString("0.0##");

            textBoxT5Deg.Text = deviceLimits.TempRefVolt.ToString();
            label20.Text = deviceLimits.TempRefVoltTemp.ToString("0.0##");
            textBoxT25Deg.Text = deviceLimits.TempRefVolt2.ToString();
            label21.Text = deviceLimits.TempRefVolt2Temp.ToString("0.0##");
            textBoxT50Deg.Text = deviceLimits.TempRefVolt3.ToString();
            label22.Text = deviceLimits.TempRefVolt3Temp.ToString("0.0##");
            textBoxTerror.Text = deviceLimits.TempErr.ToString();
            label35.Text = deviceLimits.TempErrTemp.ToString();

            textBoxStdDevMB1L1.Text = deviceLimits.StdDev.Low1.ToString();
            textBoxStdDevMB1L2.Text = deviceLimits.StdDev.Low2.ToString();
            textBoxStdDevMB2L1.Text = deviceLimits.StdDev.High1.ToString();
            textBoxStdDevMB2L2.Text = deviceLimits.StdDev.High2.ToString();

            textBoxUpolErr.Text = deviceLimits.UpolError.ToString();
            label33.Text = deviceLimits.UpolErrorValue.ToString();

            textBoxNr.Text = deviceLimits.BeM_BoxNr;
            label17.Text = deviceLimits.BeM;
        }

        private void CkbWrapLine_CheckedChanged(object sender, EventArgs e)
        {
            Tb_Info.WordWrap = CkbWrapLine.Checked;
        }
    }
}
