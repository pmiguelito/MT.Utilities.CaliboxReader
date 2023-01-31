using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace CaliboxLibrary
{
    public class DeviceCom
    {
        public DeviceCom(string channelName = null, bool isDebug = true)
        {
            IsDebug = isDebug;
            ChannelName = channelName;
            InitTimerRead();
        }
        public DeviceCom(SerialPort port, string channelName) : this(channelName)
        {
            InitPort(port, channelName);
        }

        #region SerialPort
        /************************************************
         * FUNCTION:    Comport Communication
         * DESCRIPTION:
         ************************************************/
        public Logger Logger { get; set; }
        public bool IsDebug { get; set; }
        public string ChannelName { get; private set; }
        public string PortName
        {
            get
            {
                if (PortReader == null || PortReader.Port == null) { return null; }
                return PortReader.Port.PortName;
            }
        }

        public SerialReaderThread PortReader { get; private set; }

        private void InitPort(SerialPort port, string channelName)
        {
            if (PortReader != null)
            {
                PortReader.DataReceived -= PortReader_DataReceived;
            }
            PortReader = new SerialReaderThread(port, channelName);
            PortReader.DataReceived += PortReader_DataReceived;
        }
        public void ChangePort(SerialPort port)
        {
            if (PortReader != null)
            {
                PortReader.DataReceived -= PortReader_DataReceived;
            }
            PortReader = new SerialReaderThread(port, ChannelName);
            PortReader.DataReceived += PortReader_DataReceived;
        }
        #endregion

        #region Read
        /************************************************
         * FUNCTION:    Read
         * DESCRIPTION:
         ************************************************/
        private Queue<DeviceResponseValues> _DeviceResponsesQueue = new Queue<DeviceResponseValues>();

        private void PortReader_DataReceived(object sender, DataEventArgs e)
        {
            ReadData(e);
        }

        private System.Timers.Timer _TmRead;
        private void InitTimerRead()
        {
            if (_TmRead != null) { _TmRead.Elapsed -= TmRead_Elapsed; }
            _TmRead = new System.Timers.Timer
            {
                Interval = 100
            };
            _TmRead.Elapsed += TmRead_Elapsed;
        }

        private Task ReadData(DataEventArgs e)
        {
            return Task.Factory.StartNew(() =>
            {
                if (e.Data != null)
                {
                    var i = e.Data.IndexOf(";g");
                    if (i > 0)
                    {
                        var first = e.Data.Substring(0, i);
                        var rest = e.Data.Substring(i + 1);
                        CreateData(first);
                        CreateData(rest);
                    }
                    else
                    {
                        CreateData(e.Data);
                    }
                }
            });
        }
        private void CreateData(string data)
        {
            var response = new DeviceResponseValues(OpCode, CMD_Sended, data, ItemValues);
            ItemValues.BoxId = response.BoxId;
            OnDataReceived(response);
        }
        private void TmRead_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //while (Data.Count > 0)
            //{
            //    var data = Data.Dequeue();
            //    if (data.ResponseValues != null)
            //    {
            //        Responses.Add(data.ResponseValues);
            //        OnBoxModeChanged(data.ResponseValues.BoxMode);
            //        OnCalStatusChanged(data.ResponseValues.BoxCalModus);
            //        InsertMeasurements(data.ResponseValues);
            //        ResponseLast = data.ResponseValues;
            //    }
            //}
            //bool send = false;
            //switch (minResponsesCount)
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        if (Responses.Count > 0)
            //        {
            //            send = true;
            //        }
            //        break;
            //    default:
            //        if((DateTime.Now - StartSended).TotalMilliseconds > 2000 || Responses.Count > 13)
            //        { 
            //            send = true; 
            //        }
            //        break;
            //}
            //if (send)
            //{
            //    swRead.Reset();
            //    swRead.Stop();
            //    tmRead.Stop();
            //    OnDataReceived();
            //}
        }
        #endregion

        #region Write
        /************************************************
         * FUNCTION:    Write
         * DESCRIPTION:
         ************************************************/
        public OpCode OpCode { get; private set; }
        public string CMD_Sended { get { return PortReader.CMD_sended; } }
        public string CMD_Received { get; private set; }
        private readonly object _BalanceSend = new object();
        private DateTime _StartSended;
        public void Send(OpCode opCode, string add = null)
        {
            if (opCode == OpCode.BoxIdentification)
            {
                GetBoxID();
                return;
            }
            lock (_BalanceSend)
            {
                try
                {
                    OpCode = opCode;
                    string cmd_Sended = opCode.ToString();
                    if (opCode == OpCode.RDBX || opCode == OpCode.WRBX || opCode == OpCode.RDPG || opCode == OpCode.WRPG)
                    {
                        cmd_Sended = "#" + opCode.ToString();
                        if (!string.IsNullOrEmpty(add))
                        {
                            if (cmd_Sended != add)
                            {
                                cmd_Sended += " " + add;
                            }
                        }
                        add = cmd_Sended;
                    }
                    _StartSended = DateTime.Now;
                    PortReader.Send(opCode, add);
                    if (Logger != null)
                    {
                        var msg = $"{OpCode.cmdsend}: {PortReader.CMD_sended}";
                        Logger.Save(msg);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
        #endregion
        public void Close()
        {
            if (PortReader != null)
            {
                PortReader.Close();
            }
        }

        /************************************************
         * FUNCTION:    Events
         * DESCRIPTION:
         ************************************************/
        public DeviceResponseValues ResponseLast;
        public BoxCalModus CalStatus { get; private set; } = BoxCalModus.NotDefined;
        public event EventHandler<BoxCalModus> CalStatusChanged;
        protected virtual void OnCalStatusChanged(DeviceResponseValues drv)
        {
            if (CalStatus.Hex == drv.BoxCalModus.Hex) { return; }
            CalStatus = drv.BoxCalModus;
            CalStatusChanged?.Invoke(this, drv.BoxCalModus);
        }

        public BoxMode BoxMode { get; private set; } = BoxMode.Box_Idle;
        public event EventHandler<BoxMode> BoxModeChanged;
        protected virtual void OnBoxModeChanged(DeviceResponseValues drv)
        {
            if (BoxMode.Hex == drv.BoxMode.Hex) { return; }
            if (ResponseLast != null)
            {
                SetProgress(ResponseLast, lastValue: true);
            }
            SetProgress(drv);
            BoxMode = drv.BoxMode;
            BoxModeChanged?.Invoke(this, drv.BoxMode);
        }

        public event EventHandler<DeviceResponseValues> DataReceived;
        private static readonly object _BalanceDataReceived = new object();
        protected virtual void OnDataReceived(DeviceResponseValues e)
        {
            lock (_BalanceDataReceived)
            {
                _DeviceResponsesQueue.Enqueue(e);
                OnBoxModeChanged(e);
                OnCalStatusChanged(e);
                if (e.IsMeasValues)
                {
                    InsertMeasurements(e);
                }
                if (!string.IsNullOrEmpty(e.I_Set))
                { ResponseLast = e; }
                CMD_Received = e.OpCode.ToString().ToLower();
                DataReceived?.Invoke(this, e);
                if (e.IsError)
                {
                    OnErrorReceived(e);
                }
                if (e.IsFinished)
                {
                    OnFinalizedReceived(e);
                }
            }
        }

        public bool IsError { get; private set; }
        public event EventHandler<DeviceResponseValues> ErrorReceived;
        public void OnErrorReceived(DeviceResponseValues e)
        {
            IsError = true;
            ItemValues.ErrorDetected = true;
            ErrorReceived?.Invoke(this, e);
        }

        public bool IsFinalized { get; private set; }
        public event EventHandler<DeviceResponseValues> FinalizedReceived;
        protected virtual void OnFinalizedReceived(DeviceResponseValues e)
        {
            IsFinalized = true;
            FinalizedReceived?.Invoke(this, e);
        }

        public event EventHandler<DeviceResponseValues> ProgressChanged;
        protected virtual void OnProgressChanged(DeviceResponseValues e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        public event EventHandler<DeviceResponseValues> MeasurementChanged;
        protected virtual void OnMeasurementChanged(DeviceResponseValues e)
        {
            MeasurementChanged?.Invoke(this, e);
        }

        /************************************************
         * FUNCTION:    Progress
         * DESCRIPTION:
         ************************************************/
        private DataTable _DT_Progress;
        public DataTable DT_Progress
        {
            get
            {
                if (_DT_Progress == null)
                {
                    _DT_Progress = BoxParse.DT_InitProgress(_DT_Progress);
                }
                return _DT_Progress;
            }
            set { _DT_Progress = value; }
        }
        public Task GetBoxID()
        {
            return Task.Factory.StartNew(() =>
            {
                Send(OpCode.RDBX, "10");
                Task.Delay(400).Wait();
                Send(OpCode.RDBX, "31");
            });
        }

        private string _LastMode;
        public void SetProgress(DeviceResponseValues drv, bool lastValue = false)
        {
            if (IsDebug) { return; }
            if (_DT_Progress == null) { _DT_Progress = DT_Progress; }
            var value = string.IsNullOrEmpty(drv.I_AVG) ? drv.BoxMeasValue : $"({drv.I_Set}) {drv.I_AVG}/{drv.I_StdDev}";
            if (BoxParse.Set_Progress(ref _DT_Progress, drv))
            {
                if (!lastValue || drv.BoxMode_hex != _LastMode)
                {
                    _LastMode = drv.BoxMode_hex;
                    OnProgressChanged(drv);
                }
            }
        }

        /// <summary>
        /// Usage only from UserInterfaces
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error"></param>
        /// <param name="value"></param>
        public void SetProgress(BoxModeCodes code, BoxErrorMode error, string value = null)
        {
            var boxMode = BoxMode.FromHex(code);
            var drv = new DeviceResponseValues(boxMode.Hex, value, error.Hex);
            SetProgress(drv);
        }
        public void SetProgress(string boxmode_hex, string boxmode_desc = null, string value = null, string errorcode = null, bool lastValue = false)
        {
            var drv = new DeviceResponseValues(boxmode_hex, value, errorcode);
            SetProgress(drv, lastValue);
        }

        /************************************************
         * FUNCTION:    Measurement
         * DESCRIPTION:
         ************************************************/
        public ChannelValues ItemValues { get; set; } = new ChannelValues();
        private DataTable _DT_Measurements;
        public DataTable DT_Measurements
        {
            get
            {
                if (_DT_Measurements == null)
                {
                    _DT_Measurements = BoxParse.DT_InitMeasurement();
                }
                return _DT_Measurements;
            }
            private set { _DT_Measurements = value; }
        }
        private void InsertMeasurements(DeviceResponseValues drv)
        {
            if (!drv.IsMeasValues) { return; }
            var values = ItemValues.AddMeasurement(drv);
            if (BoxParse.Insert_DT_Measurement(DT_Measurements, drv, values))
            {
                OnMeasurementChanged(drv);
            }
            SetProgress(drv);
        }

        /************************************************
         * FUNCTION:    Clear Data
         * DESCRIPTION: Delete, Clear existing data from last test
         ************************************************/
        /// <summary>
        /// Delete all stored data
        /// </summary>
        public void ClearData()
        {
            ResetError();
            _DeviceResponsesQueue.Clear();
            ItemValues = new ChannelValues();
            ResponseLast = null;
            DT_Measurements.Rows.Clear();
            DT_Progress.Rows.Clear();
        }
        public void ResetError()
        {
            IsError = false;
            IsFinalized = false;
        }
    }
}
