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
using CaliboxLibrary;
using System.Diagnostics;

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

        int BaudDefault = 19200;
        private void Init_CoB_BaudRate()
        {
            CoB_BaudRate.DataSource = BaudRateList;
            CoB_BaudRate.Text = BaudDefault.ToString();
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
            Init_Timer();
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
        DeviceCom Device = new DeviceCom("TEST");
        //SerialPort Port { get { return Device.PortReader.Port; } }
        private void Init_Port()
        {
            Close_COMport();
            if (string.IsNullOrEmpty(CoB_BaudRate.Text))
            {
                CoB_BaudRate.Text = BaudDefault.ToString();
            }
            int baud = BaudDefault;
            try { baud = Convert.ToInt32(CoB_BaudRate.Text); } catch { CoB_BaudRate.Text = baud.ToString(); }
            if (Device != null) { Device.DataReceived -= Device_DataReceived; }
            var port = SerialPort_Init(CoB_COM.Text, baud);
            Device.ChangePort(port);
            Device.DataReceived += Device_DataReceived;
        }

        /***************************************************************************************
        * SerialPort:   READ
        ****************************************************************************************/
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private void Device_DataReceived(object sender, DeviceResponseValues e)
        {
            Parse(e);
        }

        private void Parse(DeviceResponseValues drv)
        {
            try
            {
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
                Tb_Info.Text += $"{watch.Elapsed.TotalSeconds:0.000}\t{message}{Environment.NewLine}";
                Tb_Info.SelectionStart = Tb_Info.Text.Length;
                Tb_Info.ScrollToCaret();
                Tb_Info.Refresh();
            }
            catch { }
        }
        /***************************************************************************************
        * SerialPort:
        ****************************************************************************************/
        //public SerialPort port = new SerialPort();

        public void Close_COMport()
        {
            Device.Close();
        }


        /***************************************************************************************
        * SerialPort:   SEND
        ****************************************************************************************/
        private void CMD_Send(string cmd, bool timerReset)
        {
            try
            {
                CMD = cmd;
                if (timerReset)
                {
                    watch.Restart();
                }
                if (CMD.Contains("S999")) { G001sended = false; }
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
            CMD_Send(CoB_CMD.Text, timerReset: true);
        }

        //enum CMDs
        //{
        //    InitBox_S999,
        //    Set0nA_G907,
        //    Set175nA_G908,
        //    Set4700nA_G909,
        //    Calib_S100_6850i,
        //    Calib_S674,
        //    Calib_S500,
        //    GetBoxLimits,
        //    ShowBoxLimits,
        //    TempCheckNTC25_G911,
        //    TempCheckPT20_G915,
        //    TempCheckPT30_G916,
        //    CheckUpol_G910,
        //    SetUpol500_G913,
        //    SetUpol674_G914
        //}

        private void BtnSendp_Click(object sender, EventArgs e)
        {
            watch.Restart();
            Enum.TryParse(CobCMDp.Text, out commands);
            MultiCommands();
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
        private CMDs commands;
        List<Routine> InitBoxList = new List<Routine>();
        private bool G001sended = false;

        private void AddG001()
        {
            if (!G001sended)
            {
                StateSend.Add(new Routine(OpCode.G906, "G906", wait: 1000));
                StateSend.Add(new Routine(OpCode.G901, "G901", wait: 1000));
                G001sended = true;
            }
        }

        private void MultiCommands()
        {
            StateSend = new List<Routine>();
            bool startCMDs = true;
            switch (commands)
            {
                case CMDs.InitBox_S999:
                    if (InitBoxList.Count == 0)
                    {
                        InitBoxList.Add(new Routine(OpCode.S999, "S999", wait: 3000));
                        InitBoxList.Add(new Routine(OpCode.G015, "G015", wait: 3000, retry: 2));
                    }
                    StateSend = InitBoxList;
                    if (commands != CMDs.InitBox_S999)
                    {
                        MultiCommands();
                        return;
                    }
                    break;
                case CMDs.Set0nA_G907:
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.Add(new Routine(OpCode.G907, "G907", wait: 2000));
                    StateSend.Add(new Routine(OpCode.G906, "G906", wait: 1000, retry: 10));
                    break;
                case CMDs.Set175nA_G908:
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.Add(new Routine(OpCode.G908, "G908", wait: 2000));
                    StateSend.Add(new Routine(OpCode.G906, "G906", wait: 1000, retry: 10));
                    break;
                case CMDs.Set4700nA_G909:
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.Add(new Routine(OpCode.G909, "G909", wait: 2000));
                    StateSend.Add(new Routine(OpCode.G906, "G906", wait: 1000, retry: 10));
                    break;
                case CMDs.Calib_S100_6850i:
                    Tb_Info.Clear();
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.AddRange(InitBoxList);
                    StateSend.Add(new Routine(OpCode.G100, "G100", wait: 2000));
                    StateSend.Add(new Routine(OpCode.S100, "S100", wait: 20000));
                    AddG001();
                    break;
                case CMDs.Calib_S500:
                    Tb_Info.Clear();
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.AddRange(InitBoxList);
                    StateSend.Add(new Routine(OpCode.G100, "G100", wait: 2000));
                    StateSend.Add(new Routine(OpCode.S500, "S500", wait: 20000));
                    AddG001();
                    break;
                case CMDs.Calib_S674:
                    Tb_Info.Clear();
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.AddRange(InitBoxList);
                    StateSend.Add(new Routine(OpCode.G100, "G100", wait: 2000));
                    StateSend.Add(new Routine(OpCode.S674, "S674", wait: 20000));
                    AddG001();
                    break;
                case CMDs.GetBoxLimits:
                    Tb_Info.Clear();
                    if (InitBoxList.Count == 0) { goto case CMDs.InitBox_S999; }
                    StateSend.AddRange(InitBoxList);
                    StateSend.Add(new Routine(OpCode.RDBX, "#RDBX 10", wait: 2000));
                    StateSend.Add(new Routine(OpCode.RDBX, "#RDBX 31", wait: 2000));
                    break;
                case CMDs.ShowBoxLimits:
                    startCMDs = false;
                    //ShowBoxLimits();
                    PanelLimits.Height = 130;
                    break;
                case CMDs.TempCheckNTC25_G911:
                    Tb_Info.Clear();
                    StateSend.Add(new Routine(OpCode.G915, "G911", wait: 3000));
                    AddG001();
                    break;
                case CMDs.TempCheckPT20_G915:
                    Tb_Info.Clear();
                    StateSend.Add(new Routine(OpCode.G915, "G915", wait: 3000));
                    AddG001();
                    break;
                case CMDs.TempCheckPT30_G916:
                    Tb_Info.Clear();
                    StateSend.Add(new Routine(OpCode.G916, "G916", wait: 3000));
                    AddG001();
                    break;
                case CMDs.CheckUpol_G910:
                    Tb_Info.Clear();
                    Message("16sec Warten");
                    StateSend.Add(new Routine(OpCode.G910, "G910", wait: 16000));
                    AddG001();
                    break;
                case CMDs.SetUpol500_G913:
                    Tb_Info.Clear();
                    Message("16sec Warten");
                    StateSend.Add(new Routine(OpCode.G913, "G913", wait: 16000));
                    StateSend.Add(new Routine(OpCode.G906, "G906", wait: 1000, retry: 10));
                    break;
                case CMDs.SetUpol674_G914:
                    Tb_Info.Clear();
                    Message("16sec Warten");
                    StateSend.Add(new Routine(OpCode.G914, "G914", wait: 16000));
                    StateSend.Add(new Routine(OpCode.G906, "G906", wait: 1000, retry: 10));
                    break;

                case CMDs.ReadPage00:
                    StateSend.Add(new Routine(OpCode.RDPG, "#RDPG 00"));
                    break;
                case CMDs.ReadPage01:
                    StateSend.Add(new Routine(OpCode.RDPG, "#RDPG 01"));
                    break;
                case CMDs.ReadPage02:
                    StateSend.Add(new Routine(OpCode.RDPG, "#RDPG 02"));
                    break;
            }
            if (startCMDs)
            {
                PanelLimits.Height = 0;
                NowRunningIndex = 0;
                NowRunning = StateSend[NowRunningIndex];
                CMDsw.Restart();
                tmSendp.Interval = 1;
                tmSendp.Start();
            }
        }
        private struct Routine
        {
            public Routine(OpCode opcode, string cmd, int wait = 0, int retry = 1)
            {
                OpCode = opcode;
                CMD = cmd;
                Wait = wait;
                RetriesCount = 0;
                RetriesMax = retry;
                Running = false;
            }

            public OpCode OpCode { get; set; }
            public string CMD { get; set; }
            public int Wait { get; set; }
            public int RetriesMax { get; set; }
            public int RetriesCount { get; set; }
            public bool Running { get; set; }
            public int Send(int nowrunningIndex)
            {
                if (!Running)
                {
                    Running = true;
                    CMDsw = Stopwatch.StartNew();
                    RetriesCount++;
                    Instance.CMD_Send(CMD, timerReset: false);
                    return nowrunningIndex;
                }
                if (TimeElapsed && Retry)
                {
                    Instance.CMD_Send(CMD, timerReset: false);
                    CMDsw.Restart();
                    RetriesCount++;
                    return nowrunningIndex;
                }
                else if (TimeElapsed && !Retry)
                {
                    nowrunningIndex++;
                }
                return nowrunningIndex;
            }
            public bool TimeElapsed
            {
                get
                {
                    if (Wait < 1) { return true; }
                    return CMDsw.ElapsedMilliseconds > Wait;
                }
            }
            public bool Retry
            {
                get
                {
                    if (RetriesMax < 2) { return false; }
                    if (RetriesCount >= RetriesMax) { return false; }
                    return true;
                }
            }
        }

        List<Routine> StateSend = new List<Routine>();
        public static Stopwatch CMDsw = new Stopwatch();
        Timer tmSendp = new Timer();
        private int tmInterval = 500;
        private void Init_Timer()
        {
            tmSendp = new Timer
            {
                Interval = tmInterval
            };
            tmSendp.Tick += TmSendp_Tick;
        }
        Routine NowRunning;
        int NowRunningIndex;
        private void TmSendp_Tick(object sender, EventArgs e)
        {
            if (NowRunningIndex >= StateSend.Count)
            {
                tmSendp.Stop();
                return;
            }
            var i = NowRunning.Send(NowRunningIndex);
            if (i != NowRunningIndex)
            {
                NowRunningIndex = i;
                if (NowRunningIndex < StateSend.Count)
                {
                    NowRunning = StateSend[NowRunningIndex];
                }
                else
                {
                    tmSendp.Stop();
                }
            }
        }

        /***************************************************************************************
        * Calibration:  Load
        ****************************************************************************************/
        //private void ShowBoxLimits()
        //{
        //    var dl = DR.BoxID.DeviceLimits;
        //    textBoxCAL_High_1.Text = dl.RawErrorCalibration.High1.ToString();
        //    textBoxCAL_High_2.Text = dl.RawErrorCalibration.High2.ToString();
        //    textBoxCAL_Low_1.Text = dl.RawErrorCalibration.Low1.ToString();
        //    textBoxCAL_Low_2.Text = dl.RawErrorCalibration.Low2.ToString();

        //    textBoxVER_High_1.Text = dl.Current.High1.ToString("0.0##");
        //    textBoxVER_High_2.Text = dl.Current.High2.ToString("0.0##");
        //    textBoxVER_Low_1.Text = dl.Current.Low1.ToString("0.0##");
        //    textBoxVER_Low_2.Text = dl.Current.Low2.ToString("0.0##");

        //    textBoxMB1Zero.Text = dl.RawVal.Low1.ToString();
        //    textBoxMB1_175nA.Text = dl.RawVal.Low2.ToString();
        //    textBoxMB2_175nA.Text = dl.RawVal.High1.ToString();
        //    textBoxMB2_4700nA.Text = dl.RawVal.High2.ToString();

        //    lblCurr0.Text = dl.Current.Low2.ToString("0.0##");
        //    lblCurr1.Text = dl.Current.High1.ToString("0.0##");
        //    lblCurr2.Text = dl.Current.High2.ToString("0.0##");

        //    textBoxT5Deg.Text = dl.TempRefVolt.ToString();
        //    label20.Text = dl.TempRefVoltTemp.ToString("0.0##");
        //    textBoxT25Deg.Text = dl.TempRefVolt2.ToString();
        //    label21.Text = dl.TempRefVolt2Temp.ToString("0.0##");
        //    textBoxT50Deg.Text = dl.TempRefVolt3.ToString();
        //    label22.Text = dl.TempRefVolt3Temp.ToString("0.0##");
        //    textBoxTerror.Text = dl.TempErr.ToString();
        //    label35.Text = dl.TempErrTemp.ToString();

        //    textBoxStdDevMB1L1.Text = dl.StdDev.Low1.ToString();
        //    textBoxStdDevMB1L2.Text = dl.StdDev.Low2.ToString();
        //    textBoxStdDevMB2L1.Text = dl.StdDev.High1.ToString();
        //    textBoxStdDevMB2L2.Text = dl.StdDev.High2.ToString();

        //    textBoxUpolErr.Text = dl.UpolError.ToString();
        //    label33.Text = dl.UpolErrorValue.ToString();

        //    textBoxNr.Text = dl.BeM_BoxNr;
        //    label17.Text = dl.BeM;
        //}

        private void CkbWrapLine_CheckedChanged(object sender, EventArgs e)
        {
            Tb_Info.WordWrap = CkbWrapLine.Checked;
        }
    }
}
