using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using static ConverterCalib.Enumerators;
using System.IO;
using System.Diagnostics;
using System.IO.Ports;

namespace ConverterCalib
{
    public partial class UcCalib : UserControl
    {
        /*****************************************************************************
        * Instance:
        '****************************************************************************/
        private static UcCalib _instance;
        public static UcCalib Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UcCalib();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        /*****************************************************************************
        * Constructor:
        '****************************************************************************/
        public UcCalib()
        {
            InitializeComponent();
        }
        private void UC_Calib_Load(object sender, EventArgs e)
        {
            TimerWait_Init();
            TimerMeasValues_Init();
            //BGColor_Default = _TB_Limits_STDRange.BackColor;
            Reset();
            InitDGV();
            TbPathTdl.Text = Properties.Settings.Default.PathTDL;
            MeasValues.AVGnValues = (int)NudNvalues.Value;
            CkbDebugging.Checked = Globals.Debugging;
            PanelLimits.Enabled = Globals.Debugging;
            CkbDebugging.CheckedChanged += CkbDebugging_CheckedChanged;
            BindLimitsTemp();
            BindLimitsUNTC();
            LoadPorts();
            CommTest();
        }

        /*****************************************************************************
        * Communication:    OCM461
        *                   Widerstandsdecade
        '****************************************************************************/
        private ComOCM OCM;
        private const string PortLoad = "Load...";
        private void LoadPorts()
        {
            if(OCM == null) 
            { 
                OCM = new ComOCM();
            }
            var list = SerialPort.GetPortNames().ToList();
            list.Add(PortLoad);
            CobPortsOCM.DataSource = list;
        }


        private void CobPortsOCM_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CobPortsOCM.Text)
            {
                case PortLoad:
                    LoadPorts();
                    break;
                default:
                    OCM.ChangePort(CobPortsOCM.Text);
                    break;
            }
        }

        private void BtnOCM_Click(object sender, EventArgs e)
        {
            OCM.Send(RtbCMD.Text);
        }

        private void BtnOCMInit_Click(object sender, EventArgs e)
        {
            OCM.OpenComm();
        }


        private void BtnTestComm_Click(object sender, EventArgs e)
        {
            BtnTestComm.BackColor = OCM.CommTest()? MTcolors.MT_rating_God_Active:MTcolors.MT_rating_Bad_Active;
        }


        /*****************************************************************************
        * Debugging:
        '****************************************************************************/
        private void CkbDebugging_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Debugging = CkbDebugging.Checked;
            PanelLimits.Enabled = Globals.Debugging;
            GUI_LimitsColorsTemp();
            GUI_LimitsColorsUNTC();
        }
        private void ResetInfo()
        {
            RtbInfo.Text = "";
        }
        private void AddInfo(string value)
        {
            if (InvokeRequired) { Invoke(new Action(() => AddInfo(value))); return; }
            RtbInfo.Text += $"{DateTime.Now.ToLongTimeString()} {value}{Environment.NewLine}";
        }
        private void AddInfo(ProcDesc procNow, ProcDesc procNext)
        {
            if (InvokeRequired) { Invoke(new Action(() => AddInfo(procNow, procNext))); return; }
            RtbInfo.Text += $"{DateTime.Now.ToLongTimeString()} Proc:{procNow}, ProcNext:{procNext}{Environment.NewLine}";
            RtbInfo.ScrollToCaret();
            RtbInfo.Refresh();
        }


        private void Reset()
        {
            if (InvokeRequired) { Invoke(new Action(() => Reset())); return; }
            LblStatusTxt.Text = "";
            LblDurationTxt.Text = "";
            LblTdlPathInUse.Text = "";
            LblHEX.Text = "";
            LblPage2.Text = "";
            LblPage3.Text = "";
            LblPage14.Text = "";
            LblPage30.Text = "";
            LedError.Checked = true;
        }

        /*****************************************************************************
        * Helpers:
        '****************************************************************************/
        OneWireUI.UCcom Comm { get { return Globals.Config1Wire.Com1; } }
        OneWire.TEDSInfos SensorInfos { get { return Comm.SensorInfos; } }

        /*****************************************************************************
        * Paths:
        '****************************************************************************/
        private void BtnLoadPathTdl_Click(object sender, EventArgs e)
        {
            LoadPath();
        }

        private void LoadPath()
        {
            string fileEndings = "Templates ";
            string f = "";
            for(int i= 0;i < Globals.FilesEnding_TDL.Length; i++)
            {
                if(i== 0)
                {
                    f += $"*.{Globals.FilesEnding_TDL[i]}";
                }
                else
                {
                    f += $"; *.{Globals.FilesEnding_TDL[i]}";
                }
                
            }
            fileEndings += $"({f})|{f}";
            try
            {
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    FileName = "Select a template file",
                    Filter = fileEndings,
                };

                if (!string.IsNullOrEmpty(TbPathTdl.Text))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(TbPathTdl.Text);
                        ofd.InitialDirectory = fi.DirectoryName;
                    }
                    catch
                    {
                        TbPathTdl.Text += "   ERRROR!!!!";
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TbPathTdl.Text = ofd.FileName;
                    SensorInfos.Path_Tdl = ofd.FileName;
                }
            }
            catch
            {

            }
            GetStatus();
        }

        /*****************************************************************************
        * Sensor Infos: Status And Duration
        *****************************************************************************/
        private bool GetStatus()
        {
            return SetStatus(SensorInfos.SensorStatusCode);
        }
        private bool SetStatus(OneWire.TEDSInfos.StatusCode status)
        {
            if (InvokeRequired) { return (bool)Invoke(new Func<bool>(() => GetStatus())); }
            switch (status)
            {
                case OneWire.TEDSInfos.StatusCode.NoError:
                    LedError.Checked = false;
                    break;
                default:
                    LedError.Checked = true;
                    break;
            }
            LblStatusTxt.Text = SensorInfos.SensorStatusCode.ToString(); 
            if (SensorInfos.Duration.TotalMilliseconds > 1000)
            {
                LblDurationTxt.Text = $"{SensorInfos.Duration.TotalSeconds.ToString("0.###")} sec";
            }
            else
            {
                LblDurationTxt.Text = $"{SensorInfos.Duration.TotalMilliseconds.ToString("0.###")} ms";
            }
            LblTdlPathInUse.Text = SensorInfos.Path_Tdl;
            TbPathTdl.BackColor = SensorInfos.Path_TdlExists ? MTcolors.GreenActive : MTcolors.RedActive;
            return !LedError.Checked;
        }


        /*****************************************************************************
        * Communication:    Test
        *****************************************************************************/
        private void BtnCommTest_Click(object sender, EventArgs e)
        {
            CommTest();
        }
        private bool CommTest()
        {
            Reset();
            if (PowerOnOff(true))
            {
                SensorInfos.Path_Tdl = null;
                SensorInfos.Path_Tdl = TbPathTdl.Text;
                SensorInfos.ReadSensorInfos(loadFilesfromSensorItem: false);
                GetStatus();
            }
            return !LedError.Checked;
        }



        /*****************************************************************************
        * Communication:    Get Values Page 16
        *****************************************************************************/
        private void BtnGetP16_Click(object sender, EventArgs e)
        {
            GetPage16();
        }

        /*****************************************************************************
        * Communication:    Get Values
        *****************************************************************************/
        MeasValues MeasValues = new MeasValues();
        private int _CountMeas = 0;
        private int CountMeas { get { return _CountMeas; } set { SetMeasCount(value); } }

        private void SetMeasCount(int value)
        {
            if (InvokeRequired) { Invoke(new Action(() => SetMeasCount(value))); return; }
            _CountMeas = value;
            _TB_Meas_Counter.Text = $"{value}";
        }
        private int CountMeasInRange = 0;
        private void GetPage16()
        {
            SensorInfos.ReadSensorPage(16, out List<OneWire.TDL_Property> values);
            GetStatus();
            SetValues();
        }

        private void SetValues(bool reset = false)
        {
            if (InvokeRequired) { Invoke(new Action(() => SetValues(reset))); return; }
            if (!SensorInfos.PageDatas[16].DataReceived) { return; }

            LblHEX.Text = SensorInfos.PageDatas[16].HEXseparated;
            if (reset) { MeasValues.Reset(); }
            int countInRange = 0;
            if (GetValueDoubles(MeasValues.MeasCurrent)) { countInRange++; }
            if (GetValueDoubles(MeasValues.UPol)) { countInRange++; }
            if (GetValueDoubles(MeasValues.Temp)) { countInRange++; }
            if (GetValueDoubles(MeasValues.Impendance)) { countInRange++; }
            if (GetValueDoubles(MeasValues.UAnode)) { countInRange++; }
            if (GetValueDoubles(MeasValues.UNTC)) { countInRange++; }
            if (GetValueDoubles(MeasValues.TempBoad)) { countInRange++; }
            if (GetValueDoubles(MeasValues.MBHigh)) { countInRange++; }
            if (GetValueDoubles(MeasValues.MBLow)) { countInRange++; }

            CountMeas++;
            bool inRange = countInRange == 9;
            CountMeasInRange = inRange ? CountMeasInRange + 1 : 0;
            bool MeasCountInRange = CountMeasInRange > (int)NudRatingValues.Value;
            _TB_Meas_Counter.Text = $"{ CountMeasInRange} / {CountMeas}";
            GUI_Rating(MeasCountInRange);
            DgvMeasValues.ClearSelection();
            if (MeasCountInRange)
            {
                if (MeasAutoStop) 
                {
                    if (LedOCMfound.Checked)
                    {
                        ProcessWorker = ProcNext;
                    }
                    else
                    {
                        StartStopButtonText(ProcNext, false);
                    }
                }
            }
        }

        private bool GetValueDoubles(Limits calc)
        {
            if (SensorInfos.TEDS_Template.TDL_Properties_DicByProperty.TryGetValue($"%{calc.ValueTitle}", out OneWire.TDL_Property prop))
            {
                if (double.TryParse(prop.Value, out double value))
                {
                    calc.AddValue(value);
                    GUI_SetMeasValues(calc);
                    ToDgv(calc);
                    return calc.InRange;
                }
            }
            return false;
        }

        int[] pages = new int[] { 2, 3, 14 };
        private bool CheckCalibP14P30()
        {
            SensorInfos.ReadSensorPage(2, out _);
            SensorInfos.ReadSensorPage(3, out _);
            SensorInfos.ReadSensorPage(14, out _);
            SensorInfos.ReadSensorPage(30, out _);
            GetStatus();
            return SetPagesValues();
        }
        private bool SetPagesValues()
        {
            if (InvokeRequired) { return (bool)Invoke(new Func<bool>(() => SetPagesValues())); }
            LblPage2.Text = SensorInfos.PageDatas[2].HEXseparated;
            LblPage3.Text = SensorInfos.PageDatas[3].HEXseparated;
            LblPage14.Text = SensorInfos.PageDatas[14].HEXseparated;
            LblPage30.Text = SensorInfos.PageDatas[30].HEXseparated;
            //if (!SensorInfos.PageDatas[2].DataReceived) { return false; }
            //if (!SensorInfos.PageDatas[3].DataReceived) { return false; }
            if (!SensorInfos.PageDatas[14].DataReceived) { return false; }
            if (!SensorInfos.PageDatas[30].DataReceived) { return false; }
            //if (!GetSum(SensorInfos.PageDatas[2].ByteArray)) { return false; }
            //if (!GetSum(SensorInfos.PageDatas[3].ByteArray)) { return false; }
            if (!GetSum(SensorInfos.PageDatas[14].ByteArray)) { return false; }
            if (!GetSum(SensorInfos.PageDatas[30].ByteArray)) { return false; }
            return true;
        }

        private bool GetSum(byte[] bytes)
        {
            double sum = 0;
            int lastIndex = 1;
            for(int i = 1; i<bytes.Length;i++)
            {
                sum += bytes[i];
                lastIndex = i;
            }
            return sum>0 && sum < (255*lastIndex);
        }


        private void BtnCalibValues_Click(object sender, EventArgs e)
        {
            CheckCalibP14P30();
            GetSensorValues(false);
        }

        /*****************************************************************************
        * Sensor:   Page 3
        *****************************************************************************/
        private OneWire.TDL_Property NTCoffset;
        private OneWire.TDL_Property U_PT1000_Temp20;
        private OneWire.TDL_Property U_PT1000_Temp30;
        private OneWire.TDL_Property CalDate;
        private OneWire.TDL_Property CalDesc;
        private List<OneWire.TDL_Property> ListPage3;
        private bool GetSensorValues(bool compare)
        {
            if (InvokeRequired) { return (bool)Invoke(new Func<bool>(() => GetSensorValues(compare))); }
            ListPage3 = SensorInfos.TEDS_Template.PageProperties[3];
            int count = 0;
            int countCompare = 0;
            foreach(var prop in ListPage3)
            {
                switch (prop.Property.Replace("%",""))
                {
                    case Constants.NTCoffset:
                        if (!string.IsNullOrEmpty(prop.Value))
                        {
                            double.TryParse(prop.Value, out double v);
                            if (Globals.TestLimits)
                            {
                                if (LimitsProcTest.NTC_UNTC.Avg.InRange(v))
                                { count++; }
                            }
                            else if (LimitsProc.NTC_UNTC.Avg.InRange(v))
                            { count++; }
                        }
                        TbSensorValues_NTC.Text = prop.Value;
                        NTCoffset = prop;
                        if (compare) { if (TbSensorValues_NTC.Text == TbMeasVal_NTC.Text) { countCompare++; } }
                        break;
                    case Constants.U_PT1000_Soll_Temp20:
                        if (!string.IsNullOrEmpty(prop.Value))
                        {
                            double.TryParse(prop.Value, out double v);
                            if (Globals.TestLimits)
                            {
                                if (LimitsProcTest.PT1_UNTC.Avg.InRange(v))
                                { count++; }
                            }
                            else if (LimitsProc.PT1_UNTC.Avg.InRange(v))
                            { count++; }
                        }
                        TbSensorValues_PT20.Text = prop.Value;
                        U_PT1000_Temp20 = prop;
                        if (compare) { if (TbSensorValues_PT20.Text == TbMeasVal_PT20.Text) { countCompare++; } }
                        break;
                    case Constants.U_PT1000_Soll_Temp30:
                        if (!string.IsNullOrEmpty(prop.Value)) 
                        {
                            double.TryParse(prop.Value, out double v);
                            if (Globals.TestLimits)
                            {
                                if (LimitsProcTest.PT2_UNTC.Avg.InRange(v))
                                { count++; }
                            }
                            else if (LimitsProc.PT2_UNTC.Avg.InRange(v)) 
                            { count++; } 
                        }
                        TbSensorValues_PT30.Text = prop.Value;
                        U_PT1000_Temp30 = prop;
                        if (compare) { if (TbSensorValues_PT30.Text == TbMeasVal_PT30.Text) { countCompare++; } }
                        break;
                    case Constants.CalConverterDate:
                        if (prop.Value.Contains(".")) { count++ ; }
                        TbSensorValues_Date.Text = prop.Value;
                        CalDate = prop;
                        if (compare) { if (TbSensorValues_Date.Text == TbMeasVal_Date.Text) { countCompare++; } }
                        break;
                    case Constants.CalConverterDesc:
                        if (prop.Value.Contains("Converter")) { count++; }
                        TbSensorValues_Desc.Text = prop.Value;
                        if (!compare) { TbMeasVal_Desc.Text = prop.Value; }
                        CalDesc = prop;
                        if (compare) { if (TbSensorValues_Desc.Text == TbMeasVal_Desc.Text) { countCompare++; } }
                        break;
                    case "ReservePage3":
                    case "ResPage3":
                        if (prop.Value != "72") { prop.Value = prop.Bit.ToString(); }
                        break;
                    default:
                        break;
                }
            }
            if (compare) { if (countCompare != 5) { return false; } }
            return count == 5;
        }

        private bool SetSensorValuesDefault()
        {
            if(ListPage3 == null) { return false; }
            foreach (var prop in ListPage3)
            {
                switch (prop.Property.Replace("%", ""))
                {
                    case Constants.NTCoffset:
                        prop.Value = Globals.TestLimits ? Math.Round(LimitsProcTest.NTC_UNTC.Avg.Set).ToString() : Math.Round(LimitsProc.NTC_UNTC.Avg.Set).ToString();
                        NTCoffset = prop;
                        TbSensorValues_NTC.Text = prop.Value;
                        break;
                    case Constants.U_PT1000_Soll_Temp20:
                        prop.Value = Globals.TestLimits ? Math.Round(LimitsProcTest.PT1_UNTC.Avg.Set).ToString() : Math.Round(LimitsProc.PT1_UNTC.Avg.Set).ToString();
                        U_PT1000_Temp20 = prop;
                        TbSensorValues_PT20.Text = prop.Value;
                        break;
                    case Constants.U_PT1000_Soll_Temp30:
                        prop.Value = Globals.TestLimits ? Math.Round(LimitsProcTest.PT2_UNTC.Avg.Set).ToString() : Math.Round(LimitsProc.PT2_UNTC.Avg.Set).ToString();
                        U_PT1000_Temp30 = prop;
                        TbSensorValues_PT30.Text = prop.Value;
                        break;
                    //case Constants.CalConverterDate:
                    //    prop.Value = DateTime.Now.ToShortDateString();
                    //    CalDate = prop;
                    //    break;
                    //case Constants.CalConverterDesc:
                    //    prop.Value = "Converter00"; 
                    //    CalDesc = prop;
                    //    break;
                    default:
                        break;
                }
            }
            return true;
        }

        private void GUI_Reset_SensorValues()
        {
            LedResult.Checked = false;
            TbSensorValues_NTC.Text = "";
            TbSensorValues_PT20.Text = "";
            TbSensorValues_PT30.Text = "";
            TbSensorValues_Date.Text = "";
            TbSensorValues_Desc.Text = "";
        }

        /*****************************************************************************
        * DataGridView:
        *****************************************************************************/
        private void InitDGV()
        {
            DgvMeasValues.AutoGenerateColumns = false;
            DgvMeasValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvMeasValues.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvMeasValues.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvMeasValues.Columns.Add(Constants.MeasCurrent, Constants.MeasCurrent);
            DgvMeasValues.Columns.Add(Constants.UPol, Constants.UPol);
            DgvMeasValues.Columns.Add(Constants.Temp, Constants.Temp);
            DgvMeasValues.Columns.Add(Constants.Impedance, Constants.Impedance);
            DgvMeasValues.Columns.Add(Constants.UAnode, Constants.UAnode);
            DgvMeasValues.Columns.Add(Constants.UNTC, Constants.UNTC);
            DgvMeasValues.Columns.Add(Constants.TempBoard, Constants.TempBoard);
            DgvMeasValues.Columns.Add(Constants.StatusHighMB, Constants.StatusHighMB);
            DgvMeasValues.Columns.Add(Constants.StatusLowMB, Constants.StatusLowMB);
            DgvMeasValues.Rows.Add();
            DgvMeasValues.Rows.Add();
            DgvMeasValues.Rows.Add();
            DgvMeasValues.RowHeadersWidth = 120;
            DgvMeasValues.Rows[0].HeaderCell.Value = "Value";
            DgvMeasValues.Rows[1].HeaderCell.Value = "AVG";
            DgvMeasValues.Rows[2].HeaderCell.Value = "STDdev";
            int count = DgvMeasValues.ColumnHeadersHeight;
            count += (DgvMeasValues.Rows[0].Height * 3) + 2;
            DgvMeasValues.Height = count;
        }

        private void ToDgv(Limits calc)
        {
            if (InvokeRequired) { Invoke(new Action(() => ToDgv(calc))); return; }

            DgvMeasValues.Rows[0].Cells[calc.ValueTitle].Value = calc.Value;
            DgvMeasValues.Rows[1].Cells[calc.ValueTitle].Value = calc.Avg.ToString("0.#####");
            DgvMeasValues.Rows[1].Cells[calc.ValueTitle].Style.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
            DgvMeasValues.Rows[2].Cells[calc.ValueTitle].Value = calc.StdDev.ToString("0.#####");
            DgvMeasValues.Rows[2].Cells[calc.ValueTitle].Style.BackColor = calc.StdDevInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }

        private void GUI_SetMeasValues(Limits calc)
        {
            if (InvokeRequired) { Invoke(new Action(() => GUI_SetMeasValues(calc))); return; }
            switch (calc.ValueTitle)
            {
                case Constants.Temp:
                    switch (ProcessWorker)
                    {
                        case ProcDesc.NTC_22kOhm_25C:
                            GUI_NTC_Temp(calc);
                            break;
                        case ProcDesc.PT1000_20C:
                            GUI_PT20_Temp(calc);
                            break;
                        case ProcDesc.PT1000_30C:
                            GUI_PT30_Temp(calc);
                            break;
                    }
                    break;
                case Constants.UNTC:
                    switch (ProcessWorker)
                    {
                        case ProcDesc.NTC_22kOhm_25C:
                            GUI_NTC_UNTC(calc);
                            break;
                        case ProcDesc.PT1000_20C:
                            GUI_PT20_UNTC(calc);
                            break;
                        case ProcDesc.PT1000_30C:
                            GUI_PT30_UNTC(calc);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        /*****************************************************************************
        * Timer:    State Machine
        '****************************************************************************/
        private System.Timers.Timer TimerMeasValues;
        private void TimerMeasValues_Init()
        {
            TimerMeasValues = new System.Timers.Timer(1000);
            TimerMeasValues.Elapsed += new System.Timers.ElapsedEventHandler(TimerMeasValues_Elapsed);
        }
        private void TimerMeasValues_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GetPage16();
        }

        private void MeasStart(ProcDesc procDesc)
        {
            CountMeas = 0;
            CountMeasInRange = 0;
            MeasValues.Reset(procDesc);
            GUI_Reset(procDesc);
            TimerMeasValues.Start();
        }
        private void MeasStop()
        {
            if (TimerMeasValues.Enabled) 
            {
                CountMeas = 0;
                CountMeasInRange = 0;
                TimerMeasValues.Stop();
            }
        }


        /*****************************************************************************
        * Start:
        '****************************************************************************/
        #region buttons
        private void CkbStartStop_CheckedChanged(object sender, EventArgs e)
        {
            ProcessWorker = CkbStartStop.Checked ? ProcNext : ProcDesc.idle;
        }

        private void StartStopButtonText(ProcDesc procRunning, bool start = true)
        {
            if (InvokeRequired) { Invoke(new Action(() => StartStopButtonText(procRunning, start))); return; }
            CkbStartStop.CheckedChanged -= CkbStartStop_CheckedChanged;
            if (start)
            {
                CkbStartStop.Text = "Stop" + Environment.NewLine + procRunning;
            }
            else
            {
                if(ProcNext == ProcDesc.StopMeas) { ProcessWorker = ProcNext; return; }
                if (!LedRating_NTC.Checked && ProcNext == ProcDesc.PT1000_20C) { ProcNext = ProcDesc.NTC_22kOhm_25C; }
                else if (!LedRating_PT20.Checked && LedRating_NTC.Checked) { ProcNext = ProcDesc.PT1000_20C; }
                else if (!LedRating_PT30.Checked && LedRating_PT20.Checked) { ProcNext = ProcDesc.PT1000_30C; }
                CkbStartStop.Text = "Start" + Environment.NewLine + ProcNext;
                CkbStartStop.Checked = false;
                MeasStop();
            }
            CkbStartStop.Refresh();
            CkbStartStop.CheckedChanged += CkbStartStop_CheckedChanged;
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ProcNext = ProcDesc.StopProcess;
            ProcessWorker = ProcDesc.ResetValues;
        }
        #endregion buttons


        /*****************************************************************************
        * Timer:    State Machine
        '****************************************************************************/
        private System.Timers.Timer TimerWait;
        private void TimerWait_Init()
        {
            TimerWait = new System.Timers.Timer(300);
            TimerWait.Elapsed += new System.Timers.ElapsedEventHandler(TimerWait_Elapsed);
        }
        private void TimerWait_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        private Task Wait(ProcDesc procNext, double wait)
        {
            return Task.Run(()=>
            {
                if (procNext == ProcessWorker)
                {
                    StartStopButtonText(procNext, false);
                }
                else 
                {
                    if (wait < 10) { wait = 10; }
                    Stopwatch sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds < wait) { }
                    ProcessWorker = procNext;
                }
            });
        }
        private Task Wait(double wait)
        {
            if (wait < 10) { wait = 10; }
            Stopwatch sw = Stopwatch.StartNew();
            return Task.Run(() =>
            {
                while (sw.ElapsedMilliseconds < wait) { }
            });
        }

        public bool PowerOnOff(bool powerOn = true, int delay = 0)
        {
            int power = powerOn ? 1 : 0;
            mtpro.AdapterProtocol.Messages.SetPowerControlCommandMessage cmd = new mtpro.AdapterProtocol.Messages.SetPowerControlCommandMessage(2, power, delay);
            //mtpro.AdapterProtocol.Messages.SetPowerControlResponseMessage rsp = new mtpro.AdapterProtocol.Messages.SetPowerControlResponseMessage(mtpro.AdapterProtocol.Messages.Base.AdapterMessage.AdapterMessageErrorCode.OtherError, mtpro.AdapterProtocol.Messages.Base.AdapterMessage.AdapterMessagePowerState.MoreThan90);
            if (!SensorInfos.ComILink.Port.IsOpen) { SensorInfos.ComILink.Port.Open(); }
            var rsp = (mtpro.AdapterProtocol.Messages.SetPowerControlResponseMessage)mtpro.AdapterProtocol.ProtocolHandler.SendCommand(SensorInfos.ComILink.Port, cmd);
            return rsp.RespFlags == power;
        }

        /*****************************************************************************
        * Processes:
        '****************************************************************************/
        private ProcDesc ProcNext = ProcDesc.StartProcess;

        private ProcDesc _ProcessWorker = ProcDesc.idle;
        private ProcDesc ProcessWorker
        {
            get { return _ProcessWorker; }
            set
            {
                _ProcessWorker = value;
                Processess(value);
            }
        }
        private bool MeasAutoStop { get { return CkbAutoNoStop.Checked; } set { CkbAutoNoStop.Checked = value; } }
        private void Processess(ProcDesc procDesc)
        {
            if (InvokeRequired) { Invoke(new Action(() => Processess(procDesc))); return; }
            StartStopButtonText(procDesc);
            switch (procDesc)
            {
                case ProcDesc.StartProcess:
                    ResetInfo();
                    GUI_Reset(procDesc);
                    ProcNext = ProcDesc.CheckCommunication;
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    ProcessWorker = ProcNext;
                    return;
                case ProcDesc.CheckCommunication:
                    ProcNext = CommTest() ? ProcDesc.CheckCalibP14P30 : ProcDesc.StopProcess;
                    LedOCMfound.Checked = OCM.OpenComm();
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    Wait(ProcNext, 1000);
                    return;
                
                case ProcDesc.CheckCalibP14P30:
                    ProcNext = CheckCalibP14P30() ? ProcDesc.CheckCalibValuesInit : procDesc;
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    Wait(ProcNext, 1000);
                    return;
                case ProcDesc.CheckCalibValuesInit:
                    ProcNext = GetSensorValues(false) ? ProcDesc.NTC_22kOhm_25C : ProcDesc.SetCalibPage3Default;
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    Wait(ProcNext, 1000);
                    return;
                case ProcDesc.CheckCalibValuesWrited:
                    ProcNext = GetSensorValues(true) ? ProcDesc.SetResultOk : ProcDesc.SetResultNok;
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    Wait(ProcNext, 0);
                    return;
                case ProcDesc.SetCalibPage3Default:
                    MeasStop();
                    ProcNext = SetSensorValuesDefault() ? ProcDesc.WritePage3CalibDefault : procDesc;
                    StartStopButtonText(procDesc, false);
                    AddInfo(procDesc, ProcNext);
                    Wait(ProcNext, 1000);
                    return;
                case ProcDesc.WritePage3CalibDefault:
                    MeasStop();
                    ProcNext = WritePage3() ? ProcDesc.StartMeas : procDesc;
                    PowerOnOff(true);
                    if (!MeasAutoStop) { StartStopButtonText(procDesc, false); }
                    AddInfo(procDesc, ProcNext);
                    Wait(ProcNext, 2000);
                    return;
                case ProcDesc.WritePage3CalibValues:
                    MeasStop();
                    ProcNext = WritePage3() ? ProcDesc.CheckCalibValuesWrited : ProcDesc.idle;
                    PowerOnOff(true);
                    if (!MeasAutoStop) { StartStopButtonText(procDesc, false); }
                    Wait(ProcNext, 2000);
                    AddInfo(procDesc, ProcNext);
                    return;
                case ProcDesc.StartMeas:
                    ProcNext = ProcDesc.NTC_22kOhm_25C;
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    Wait(ProcNext, 1000);
                    return;
                case ProcDesc.StopMeas:
                    MeasStop();
                    OCM.CloseComm();
                    ProcNext = ProcDesc.WritePage3CalibValues;
                    AddInfo(procDesc, ProcNext);
                    if (Globals.Debugging)
                    {
                        if (!MeasAutoStop) { StartStopButtonText(procDesc, false); return; }
                    }
                    Wait(ProcNext, 1500);
                    return;
                case ProcDesc.NTC_22kOhm_25C:
                    if (LedOCMfound.Checked) { OCM.SetNTC(); }
                    MeasStart(procDesc);
                    ProcNext = ProcDesc.PT1000_20C;
                    StartStopButtonText(procDesc);
                    AddInfo(procDesc, ProcNext);
                    return;
                case ProcDesc.PT1000_20C:
                    if (LedOCMfound.Checked) { OCM.SetPT20(); }
                    MeasStart(procDesc);
                    ProcNext = ProcDesc.PT1000_30C;
                    StartStopButtonText(procDesc);
                    AddInfo(procDesc, ProcNext);
                    return;
                case ProcDesc.PT1000_30C:
                    if (LedOCMfound.Checked) { OCM.SetPT30(); }
                    MeasStart(procDesc);
                    ProcNext = ProcDesc.StopMeas;
                    StartStopButtonText(procDesc);
                    AddInfo(procDesc, ProcNext);
                    return;
                case ProcDesc.SetResultOk:
                    LedResult.Checked = true;
                    ProcNext = ProcDesc.StartProcess;
                    StartStopButtonText(procDesc, false);
                    AddInfo(procDesc, ProcNext);
                    break;
                case ProcDesc.SetResultNok:
                    LedResult.Checked = false;
                    ProcNext = ProcDesc.WritePage3CalibValues;
                    StartStopButtonText(procDesc, false);
                    AddInfo(procDesc, ProcNext);
                    break;
                case ProcDesc.idle:
                    MeasStop();
                    StartStopButtonText(procDesc, false);
                    if(procDesc == ProcNext) { ProcNext = ProcDesc.StopProcess; }
                    AddInfo(procDesc, ProcNext);
                    return;
                case ProcDesc.ResetValues:
                    MeasStop();
                    GUI_Reset(procDesc);
                    StartStopButtonText(procDesc, false);
                    ProcessWorker = ProcNext;
                    AddInfo(procDesc, ProcNext);
                    return;
                case ProcDesc.StopProcess:
                    MeasStop();
                    ProcNext = ProcDesc.StartProcess;
                    StartStopButtonText(procDesc, false);
                    AddInfo(procDesc, ProcNext);
                    return;
                default:
                    break;
            }

        }


        /*****************************************************************************
        * GUI:  Quantity of measurement values to calculate
        '****************************************************************************/
        private void NudNvalues_ValueChanged(object sender, EventArgs e)
        {
            MeasValues.AVGnValues = (int)NudNvalues.Value;
        }

        /*****************************************************************************
        * GUI:
        '****************************************************************************/
        private MTfonts.clMTcolors MTcolors { get { return MTfonts.clMTcolors.Instance; } }

        private void GUI_Rating(bool inRange)
        {
            if (InvokeRequired) { Invoke(new Action(() => GUI_Rating(inRange))); return; }
            switch (ProcessWorker)
            {
                case ProcDesc.NTC_22kOhm_25C:
                    LedRating_NTC.Checked = inRange;
                    break;
                case ProcDesc.PT1000_20C:
                    LedRating_PT20.Checked = inRange;
                    break;
                case ProcDesc.PT1000_30C:
                    LedRating_PT30.Checked = inRange;
                    break;
            }
        }

        private void GUI_Reset(ProcDesc procDesc)
        {
            if (InvokeRequired) { Invoke(new Action(() => GUI_Reset(procDesc))); return; }
            switch (procDesc)
            {
                case ProcDesc.ResetValues:
                case ProcDesc.StartProcess:
                    GUI_Reset_SensorValues();
                    GUI_Reset_Limits();
                    GUI_Reset_NTC();
                    GUI_LimitsColorsTemp();
                    GUI_LimitsColorsUNTC();
                    break;
                case ProcDesc.NTC_22kOhm_25C:
                    GUI_Reset_Limits();
                    GUI_LimitsColorsTemp();
                    GUI_LimitsColorsUNTC();
                    break;
                case ProcDesc.PT1000_20C:
                    GUI_Reset_Limits();
                    GUI_LimitsColorsTemp();
                    GUI_LimitsColorsUNTC();
                    break;
                case ProcDesc.PT1000_30C:
                    GUI_Reset_Limits();
                    GUI_LimitsColorsTemp();
                    GUI_LimitsColorsUNTC();
                    break;
            }
        }



       
        private Color BGColorEnabledStatusINIT;
        private Color BGColorEnabledStatusChoise;
        private static Color BGColor_Default;


        #region GUIstatus
        
        /*****************************************************************************
        * GUI:  Limits
        '****************************************************************************/
        private void GUI_Reset_Limits()
        {
            if (InvokeRequired) { Invoke(new Action(() => GUI_Reset_Limits())); return; }
            GUI_LimitsTemp();
            GUI_LimitsUNTC();
        }
        private void BindLimitsTemp()
        {
            try
            {
                CkbLimitsTempActive.DataBindings.Clear();
                CkbLimitsTempActive.DataBindings.Add("Checked", MeasValues.Temp.LimitsValue, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsTempSet.DataBindings.Clear();
                TbLimitsTempSet.DataBindings.Add("Text", MeasValues.Temp.LimitsValue, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempMin.DataBindings.Clear();
                TbLimitsTempMin.DataBindings.Add("Text", MeasValues.Temp.LimitsValue, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempPlus.DataBindings.Clear();
                TbLimitsTempPlus.DataBindings.Add("Text", MeasValues.Temp.LimitsValue, "Plus", true, DataSourceUpdateMode.OnValidation);

                CkbLimitsTempMinMaxDiffActive.DataBindings.Clear();
                CkbLimitsTempMinMaxDiffActive.DataBindings.Add("Checked", MeasValues.Temp.LimitsMinMaxDiff, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsTempMinMaxDiffSet.DataBindings.Clear();
                TbLimitsTempMinMaxDiffSet.DataBindings.Add("Text", MeasValues.Temp.LimitsMinMaxDiff, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempMinMaxDiffMin.DataBindings.Clear();
                TbLimitsTempMinMaxDiffMin.DataBindings.Add("Text", MeasValues.Temp.LimitsMinMaxDiff, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempMinMaxDiffPlus.DataBindings.Clear();
                TbLimitsTempMinMaxDiffPlus.DataBindings.Add("Text", MeasValues.Temp.LimitsMinMaxDiff, "Plus", true, DataSourceUpdateMode.OnValidation);

                CkbLimitsTempAvgActive.DataBindings.Clear();
                CkbLimitsTempAvgActive.DataBindings.Add("Checked", MeasValues.Temp.LimitsAvg, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsTempAvgSet.DataBindings.Clear();
                TbLimitsTempAvgSet.DataBindings.Add("Text", MeasValues.Temp.LimitsAvg, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempAvgMin.DataBindings.Clear();
                TbLimitsTempAvgMin.DataBindings.Add("Text", MeasValues.Temp.LimitsAvg, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempAvgPlus.DataBindings.Clear();
                TbLimitsTempAvgPlus.DataBindings.Add("Text", MeasValues.Temp.LimitsAvg, "Plus", true, DataSourceUpdateMode.OnValidation);

                CkbLimitsTempStdDevActive.DataBindings.Clear();
                CkbLimitsTempStdDevActive.DataBindings.Add("Checked", MeasValues.Temp.LimitsStdDev, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsTempStdDevSet.DataBindings.Clear();
                TbLimitsTempStdDevSet.DataBindings.Add("Text", MeasValues.Temp.LimitsStdDev, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempStdDevMin.DataBindings.Clear();
                TbLimitsTempStdDevMin.DataBindings.Add("Text", MeasValues.Temp.LimitsStdDev, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsTempStdDevPlus.DataBindings.Clear();
                TbLimitsTempStdDevPlus.DataBindings.Add("Text", MeasValues.Temp.LimitsStdDev, "Plus", true, DataSourceUpdateMode.OnValidation);
            }
            catch(Exception ex)
            {

            }
        }
        private void GUI_LimitsColorsTemp()
        {
            TbLimitsTempSet.BackColor = CkbLimitsTempActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempMin.BackColor = CkbLimitsTempActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempPlus.BackColor = CkbLimitsTempActive.Checked ? Color.LightYellow : SystemColors.Control;

            TbLimitsTempMinMaxDiffSet.BackColor = CkbLimitsTempMinMaxDiffActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempMinMaxDiffMin.BackColor = CkbLimitsTempMinMaxDiffActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempMinMaxDiffPlus.BackColor = CkbLimitsTempMinMaxDiffActive.Checked ? Color.LightYellow : SystemColors.Control;

            TbLimitsTempAvgSet.BackColor = CkbLimitsTempAvgActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempAvgMin.BackColor = CkbLimitsTempAvgActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempAvgPlus.BackColor = CkbLimitsTempAvgActive.Checked ? Color.LightYellow : SystemColors.Control;

            TbLimitsTempStdDevSet.BackColor = CkbLimitsTempStdDevActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempStdDevMin.BackColor = CkbLimitsTempStdDevActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsTempStdDevPlus.BackColor = CkbLimitsTempStdDevActive.Checked ? Color.LightYellow : SystemColors.Control;
        }
        private void BindLimitsUNTC()
        {
            try
            {
                CkbLimitsUntcActive.DataBindings.Clear();
                CkbLimitsUntcActive.DataBindings.Add("Checked", MeasValues.UNTC.LimitsValue, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsUntcSet.DataBindings.Clear();
                TbLimitsUntcSet.DataBindings.Add("Text", MeasValues.UNTC.LimitsValue, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcMin.DataBindings.Clear();
                TbLimitsUntcMin.DataBindings.Add("Text", MeasValues.UNTC.LimitsValue, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcPlus.DataBindings.Clear();
                TbLimitsUntcPlus.DataBindings.Add("Text", MeasValues.UNTC.LimitsValue, "Plus", true, DataSourceUpdateMode.OnValidation);

                CkbLimitsUntcMinMaxDiffActive.DataBindings.Clear();
                CkbLimitsUntcMinMaxDiffActive.DataBindings.Add("Checked", MeasValues.UNTC.LimitsMinMaxDiff, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsUntcMinMaxDiffSet.DataBindings.Clear();
                TbLimitsUntcMinMaxDiffSet.DataBindings.Add("Text", MeasValues.UNTC.LimitsMinMaxDiff, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcMinMaxDiffMin.DataBindings.Clear();
                TbLimitsUntcMinMaxDiffMin.DataBindings.Add("Text", MeasValues.UNTC.LimitsMinMaxDiff, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcMinMaxDiffPlus.DataBindings.Clear();
                TbLimitsUntcMinMaxDiffPlus.DataBindings.Add("Text", MeasValues.UNTC.LimitsMinMaxDiff, "Plus", true, DataSourceUpdateMode.OnValidation);

                CkbLimitsUntcAvgActive.DataBindings.Clear();
                CkbLimitsUntcAvgActive.DataBindings.Add("Checked", MeasValues.UNTC.LimitsAvg, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsUntcAvgSet.DataBindings.Clear();
                TbLimitsUntcAvgSet.DataBindings.Add("Text", MeasValues.UNTC.LimitsAvg, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcAvgMin.DataBindings.Clear();
                TbLimitsUntcAvgMin.DataBindings.Add("Text", MeasValues.UNTC.LimitsAvg, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcAvgPlus.DataBindings.Clear();
                TbLimitsUntcAvgPlus.DataBindings.Add("Text", MeasValues.UNTC.LimitsAvg, "Plus", true, DataSourceUpdateMode.OnValidation);
                

                CkbLimitsUntcStdDevActive.DataBindings.Clear();
                CkbLimitsUntcStdDevActive.DataBindings.Add("Checked", MeasValues.UNTC.LimitsStdDev, "Active", true, DataSourceUpdateMode.OnPropertyChanged);
                TbLimitsUntcStdDevSet.DataBindings.Clear();
                TbLimitsUntcStdDevSet.DataBindings.Add("Text", MeasValues.UNTC.LimitsStdDev, "Set", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcStdDevMin.DataBindings.Clear();
                TbLimitsUntcStdDevMin.DataBindings.Add("Text", MeasValues.UNTC.LimitsStdDev, "Min", true, DataSourceUpdateMode.OnValidation);
                TbLimitsUntcStdDevPlus.DataBindings.Clear();
                TbLimitsUntcStdDevPlus.DataBindings.Add("Text", MeasValues.UNTC.LimitsStdDev, "Plus", true, DataSourceUpdateMode.OnValidation);
                
            }
            catch(Exception ex)
            {

            }
        }
        private void GUI_LimitsColorsUNTC()
        {
            TbLimitsUntcSet.BackColor = CkbLimitsUntcActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcMin.BackColor = CkbLimitsUntcActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcPlus.BackColor = CkbLimitsUntcActive.Checked ? Color.LightYellow : SystemColors.Control;

            TbLimitsUntcMinMaxDiffSet.BackColor = CkbLimitsUntcMinMaxDiffActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcMinMaxDiffMin.BackColor = CkbLimitsUntcMinMaxDiffActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcMinMaxDiffPlus.BackColor = CkbLimitsUntcMinMaxDiffActive.Checked ? Color.LightYellow : SystemColors.Control;

            TbLimitsUntcAvgSet.BackColor = CkbLimitsUntcAvgActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcAvgMin.BackColor = CkbLimitsUntcAvgActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcAvgPlus.BackColor = CkbLimitsUntcAvgActive.Checked ? Color.LightYellow : SystemColors.Control;

            TbLimitsUntcStdDevSet.BackColor = CkbLimitsUntcStdDevActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcStdDevMin.BackColor = CkbLimitsUntcStdDevActive.Checked ? Color.LightYellow : SystemColors.Control;
            TbLimitsUntcStdDevPlus.BackColor = CkbLimitsUntcStdDevActive.Checked ? Color.LightYellow : SystemColors.Control;
        }
        private void GUI_LimitsTemp()
        {
            BindLimitsTemp();
            //CkbLimitsTempActive.Checked = MeasValues.Temp.LimitsValue.Active;
            //TbLimitsTempSet.Text = MeasValues.Temp.LimitsValue.Set.ToString();
            //TbLimitsTempMin.Text = MeasValues.Temp.LimitsValue.Min.ToString();
            //TbLimitsTempPlus.Text = MeasValues.Temp.LimitsValue.Plus.ToString();

            //CkbLimitsTempAvgActive.Checked = MeasValues.Temp.LimitsAvg.Active;
            //TbLimitsTempAvgSet.Text = MeasValues.Temp.LimitsAvg.Set.ToString();
            //TbLimitsTempAvgMin.Text = MeasValues.Temp.LimitsAvg.Min.ToString();
            //TbLimitsTempAvgPlus.Text = MeasValues.Temp.LimitsAvg.Plus.ToString();

            //CkbLimitsTempStdDevActive.Checked = MeasValues.Temp.LimitsStdDev.Active;
            //TbLimitsTempStdDevSet.Text = MeasValues.Temp.LimitsStdDev.Set.ToString();
            //TbLimitsTempStdDevMin.Text = MeasValues.Temp.LimitsStdDev.Min.ToString();
            //TbLimitsTempStdDevPlus.Text = MeasValues.Temp.LimitsStdDev.Plus.ToString();
        }
        private void GUI_LimitsUNTC()
        {
            BindLimitsUNTC();
            //CkbLimitsUntcActive.Checked = MeasValues.UNTC.LimitsValue.Active;
            //TbLimitsUntcSet.Text = MeasValues.UNTC.LimitsValue.Set.ToString();
            //TbLimitsUntcMin.Text = MeasValues.UNTC.LimitsValue.Min.ToString();
            //TbLimitsUntcPlus.Text = MeasValues.UNTC.LimitsValue.Plus.ToString();

            //CkbLimitsUntcAvgActive.Checked = MeasValues.UNTC.LimitsAvg.Active;
            //TbLimitsUntcAvgSet.Text = MeasValues.UNTC.LimitsAvg.Set.ToString();
            //TbLimitsUntcAvgMin.Text = MeasValues.UNTC.LimitsAvg.Min.ToString();
            //TbLimitsUntcAvgPlus.Text = MeasValues.UNTC.LimitsAvg.Plus.ToString();

            //CkbLimitsUntcStdDevActive.Checked = MeasValues.UNTC.LimitsStdDev.Active;
            //TbLimitsUntcStdDevSet.Text = MeasValues.UNTC.LimitsStdDev.Set.ToString();
            //TbLimitsUntcStdDevMin.Text = MeasValues.UNTC.LimitsStdDev.Min.ToString();
            //TbLimitsUntcStdDevPlus.Text = MeasValues.UNTC.LimitsStdDev.Plus.ToString();
        }
        
        /*****************************************************************************
        * GUI:  NTC
        '****************************************************************************/
        private void GUI_Reset_NTC()
        {
            TbMeasVal_NTC_StdDev.Text = "";
            TbMeasVal_NTC_StdDev.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_NTC_Temp.Text = "";
            TbMeasVal_NTC_Temp.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_NTC.Text = "";
            TbMeasVal_NTC.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_NTC_Diff.Text = "";
            TbMeasVal_NTC_Diff.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_NTC_Min.Text = "";
            TbMeasVal_NTC_Min.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_NTC_Max.Text = "";
            TbMeasVal_NTC_Max.BackColor = BGColorEnabledStatusChoise;
            LedRating_NTC.Checked = false;
            GUI_Reset_PT20();
        }
        private void GUI_NTC_Temp(Limits calc)
        {
            TbMeasVal_NTC_Temp.Text = calc.Avg.ToString("0.000");
            TbMeasVal_NTC_Temp.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }

        private void GUI_NTC_UNTC(Limits calc)
        {
            NTCoffset.Value = Math.Round(calc.Avg).ToString();
            TbMeasVal_NTC.Text = NTCoffset.Value;
            TbMeasVal_NTC.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;

            TbMeasVal_NTC_Diff.Text = calc.MinMaxDiff.ToString();
            TbMeasVal_NTC_Diff.BackColor = calc.MinMaxDiffInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
            TbMeasVal_NTC_Min.Text = calc.Min.ToString();
            TbMeasVal_NTC_Max.Text = calc.Max.ToString();

            TbMeasVal_NTC_StdDev.Text = calc.StdDev.ToString("0.000");
            TbMeasVal_NTC_StdDev.BackColor = calc.StdDevInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }


        /*****************************************************************************
        * GUI:  PT20
        '****************************************************************************/
        private void GUI_Reset_PT20()
        {
            TbMeasVal_PT20_StdDev.Text = "";
            TbMeasVal_PT20_StdDev.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT20_Temp.Text = "";
            TbMeasVal_PT20_Temp.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT20.Text = "";
            TbMeasVal_PT20.BackColor = BGColorEnabledStatusChoise;
            
            TbMeasVal_PT20_Diff.Text = "";
            TbMeasVal_PT20_Diff.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT20_Min.Text = "";
            TbMeasVal_PT20_Min.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT20_Max.Text = "";
            TbMeasVal_PT20_Max.BackColor = BGColorEnabledStatusChoise;
            LedRating_PT20.Checked = false;
            GUI_Reset_PT30();
        }
        private void GUI_PT20_Temp(Limits calc)
        {
            TbMeasVal_PT20_Temp.Text = calc.Avg.ToString("0.000");
            TbMeasVal_PT20_Temp.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }
        private void GUI_PT20_UNTC(Limits calc)
        {
            U_PT1000_Temp20.Value = Math.Round(calc.Avg).ToString();
            TbMeasVal_PT20.Text = U_PT1000_Temp20.Value;
            TbMeasVal_PT20.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;

            TbMeasVal_PT20_Diff.Text = calc.MinMaxDiff.ToString();
            TbMeasVal_PT20_Diff.BackColor = calc.MinMaxDiffInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
            TbMeasVal_PT20_Min.Text = calc.Min.ToString();
            TbMeasVal_PT20_Max.Text = calc.Max.ToString();

            TbMeasVal_PT20_StdDev.Text = calc.StdDev.ToString("0.000");
            TbMeasVal_PT20_StdDev.BackColor = calc.StdDevInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }

        /*****************************************************************************
        * GUI:  PT30
        '****************************************************************************/
        private void GUI_Reset_PT30()
        {
            TbMeasVal_PT30_StdDev.Text = "";
            TbMeasVal_PT30_StdDev.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT30_Temp.Text = "";
            TbMeasVal_PT30_Temp.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT30.Text = "";
            TbMeasVal_PT30.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT30_Diff.Text = "";
            TbMeasVal_PT30_Diff.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT30_Min.Text = "";
            TbMeasVal_PT30_Min.BackColor = BGColorEnabledStatusChoise;
            TbMeasVal_PT30_Max.Text = "";
            TbMeasVal_PT30_Max.BackColor = BGColorEnabledStatusChoise;
            LedRating_PT30.Checked = false;
        }
        private void GUI_PT30_Temp(Limits calc)
        {
            TbMeasVal_PT30_Temp.Text = calc.Avg.ToString("0.000");
            TbMeasVal_PT30_Temp.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }
        private void GUI_PT30_UNTC(Limits calc)
        {
            U_PT1000_Temp30.Value = Math.Round(calc.Avg).ToString();
            TbMeasVal_PT30.Text = U_PT1000_Temp30.Value;
            TbMeasVal_PT30.BackColor = calc.AvgInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;

            TbMeasVal_PT30_Diff.Text = calc.MinMaxDiff.ToString();
            TbMeasVal_PT30_Diff.BackColor = calc.MinMaxDiffInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
            TbMeasVal_PT30_Min.Text = calc.Min.ToString();
            TbMeasVal_PT30_Max.Text = calc.Max.ToString();

            TbMeasVal_PT30_StdDev.Text = calc.StdDev.ToString("0.000");
            TbMeasVal_PT30_StdDev.BackColor = calc.StdDevInRange ? MTcolors.MT_rating_God_Active : MTcolors.MT_rating_Bad_Active;
        }


        #endregion GUIstatus

        private bool WritePage3()
        {
            return true;
            if (InvokeRequired) { return (bool)Invoke(new Func<bool>(() => WritePage3())); }
            CalDate.Value = DateTime.Now.ToShortDateString();
            TbMeasVal_Date.Text = CalDate.Value;
            CalDesc.Value = TbMeasVal_Desc.Text;
            if(SensorInfos.WritePage(3, ListPage3, out TimeSpan duration))
            {
                Stopwatch sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 2000)
                { Application.DoEvents(); }
                PowerOnOff(false);
                sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 2000)
                { Application.DoEvents(); }
                PowerOnOff(true);
                return true;
            }
            return false;
        }


        private const string ConverterPage0 = "7C-3F-80-B0-26-10-01-00-00-0D-C0-1D-3A-1D-30-46-00-00-00-00-04-00-BC-02-32-00-78-02-3C-24-56-03";
        private const string ConverterPage2 = "9D-C3-B7-DB-5E-96-D3-CB-72-10-F1-09-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00";
        private const string ConverterPage3 = "4C-70-21-FF-6F-0C-0E-A9-4F-69-16-00-00-00-E0-FF-A1-0E-06-00-00-00-80-3B-F6-BF-BB-D4-0F-80-FF-03";
        private const string ConverterPage15 = "0F-0F-05-0A-32-7E-FE-37-00-00-00-00-18-C6-2B-68-E6-8A-B9-60-2D-DB-0C-00-00-00-00-00-00-00-00-E0";

        private void BtnWriteDefault_Click(object sender, EventArgs e)
        {
            CheckCalibP14P30();
            if (!GetStatus()) { return; }
            SensorInfos.PageDatas[0].NewHEX(ConverterPage0.Replace("-",""));
            SensorInfos.PageDatas[2].NewHEX(ConverterPage2.Replace("-", ""));
            SensorInfos.PageDatas[3].NewHEX(ConverterPage3.Replace("-", ""));
            SensorInfos.PageDatas[15].NewHEX(ConverterPage15.Replace("-", ""));
            try
            {
                SensorInfos.WritePage(0, out _);
                Thread.Sleep(2000);
                SensorInfos.WritePage(2, out _);
                Thread.Sleep(2000);
                SensorInfos.WritePage(3, out _);
                Thread.Sleep(2000);
                SensorInfos.WritePage(15, out _);
                Thread.Sleep(2000);
                CheckCalibP14P30();
            }
            catch
            {

            }
        }
    }
}
