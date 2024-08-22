using CaliboxLibrary.BoxCommunication.CMDs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace CaliboxLibrary
{
    public class DeviceCom
    {
        public Logger Logger { get; set; } = new Logger();
        public ChannelValues ItemValues { get; set; } = new ChannelValues();

        /************************************************
         * FUNCTION:    Constructor(s)
         * DESCRIPTION:
         ************************************************/
        public DeviceCom(int channelNo, bool isDebug = true)
        {
            PortReader = new SerialReaderThread("COM1", channelNo);
            IsDebug = isDebug;
            ChannelNo = channelNo;
        }

        public DeviceCom(SerialPort port, int channelNo, bool isDebug)
        {
            IsDebug = isDebug;
            InitPort(port, channelNo);
        }

        #region SerialPort
        /************************************************
         * FUNCTION:    Comport Communication
         * DESCRIPTION:
         ************************************************/
        public bool IsDebug { get; set; }
        private string _ChannelName;
        public string ChannelName
        {
            get { return _ChannelName; }
            private set
            {
                if (_ChannelName != value)
                {
                    _ChannelName = value;
                    if (string.IsNullOrEmpty(value) == false)
                    {
                        if (int.TryParse(value, out int no))
                        {
                            ChannelNo = no;
                        }
                    }
                }
            }
        }
        private int _ChannelNo;
        public int ChannelNo
        {
            get { return _ChannelNo; }
            set
            {
                _ChannelNo = value;
                _ChannelName = value.ToString("00");
                if (ItemValues != null)
                {
                    ItemValues.channel_no = value;
                }
                if (Logger != null)
                {
                    Logger.ChannelNo = _ChannelName;
                }
            }
        }
        public string PortName
        {
            get
            {
                if (PortReader == null || PortReader.Port == null)
                {
                    return null;
                }
                return PortReader.Port.PortName;
            }
        }

        public SerialReaderThread PortReader { get; private set; }

        private void InitPort(SerialPort port, int channelNo)
        {
            if (PortReader != null)
            {
                PortReader.DataReceived -= PortReader_DataReceived;
                PortReader.Stop();
                PortReader = null;
            }
            PortReader = new SerialReaderThread(port, channelNo);
            PortReader.DataReceived += PortReader_DataReceived;
        }

        public void ChangePort(SerialPort port, int channelNo)
        {
            if (PortReader != null)
            {
                PortReader.DataReceived -= PortReader_DataReceived;
                PortReader.Stop();
                PortReader = null;
            }
            PortReader = new SerialReaderThread(port, channelNo);
            ChannelNo = channelNo;
            PortReader.DataReceived += PortReader_DataReceived;
        }
        #endregion

        /************************************************
         * FUNCTION:    Read
         * DESCRIPTION:
         ************************************************/
        private void PortReader_DataReceived(object sender, EventDataArgs e)
        {
            ReadData(e);
        }

        private Task ReadDataAsync(EventDataArgs e)
        {
            return Task.Factory.StartNew(() =>
            {
                ReadData(e);
            });
        }

        private void ReadData(EventDataArgs e)
        {
            if (e.IsData)
            {
                try
                {
                    var response = new DeviceResponseValues(e.CmdSended, e.Data, ItemValues);
                    e.Insert(response);
                    OnDataReceived(e);
                }
                catch
                {

                }
            }
        }

        /************************************************
         * FUNCTION:    Device Responses
         * DESCRIPTION:
         ************************************************/
        private Queue<EventDataArgs> _DeviceResponsesQueue = new Queue<EventDataArgs>();
        public DeviceResponseValues ResponseLast { get; set; }
        public List<DeviceResponseValues> DeviceResponses { get; set; } = new List<DeviceResponseValues>();

        /// <summary>
        /// Clear all device Responses
        /// </summary>
        public void ClearDeviceResponses()
        {
            if (DeviceResponses == null)
            {
                DeviceResponses = new List<DeviceResponseValues>();
                return;
            }
            DeviceResponses.Clear();
        }

        public bool TryGetResponses()
        {
            return false;
        }

        public bool GetLastResponse(out DeviceResponseValues responseValues)
        {
            try
            {
                if (DeviceResponses.Count > 0)
                {
                    responseValues = DeviceResponses[DeviceResponses.Count - 1];
                    return true;
                }
            }
            catch { }
            responseValues = null;
            return false;
        }

        public bool GetLastResponseWithValues(out DeviceResponseValues responseValues)
        {
            return GetLastResponseWithValues(DeviceResponses, out responseValues);
        }

        public bool GetLastResponseWithValues(IEnumerable<DeviceResponseValues> values, out DeviceResponseValues result)
        {
            try
            {
                if (values?.Any() ?? false)
                {
                    var data = values.ToList();
                    for (int i = data.Count - 1; i > -1; i--)
                    {
                        result = data[i];
                        if (string.IsNullOrEmpty(result.MeasValues.I_Set) == false)
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            result = null;
            return false;
        }

        public IEnumerable<DeviceResponseValues> GetAfter(DateTime dateTimeStart)
        {
            return DeviceResponses?.Where(x => x.meas_time_start > dateTimeStart);
        }

        public bool ResponseContainsError(out DeviceResponseValues responseValues)
        {
            return ResponseContainsError(DeviceResponses, out responseValues);
        }

        public bool ResponseContainsError(IEnumerable<DeviceResponseValues> values, out DeviceResponseValues result)
        {
            try
            {
                if (values?.Any() ?? false)
                {
                    result = values.First(x => (x.IsError || x.IsBoxErrorError));
                    return true;
                }
            }
            catch { }
            result = null;
            return false;
        }

        public bool ResponseContainsFinalized(IEnumerable<DeviceResponseValues> values, out DeviceResponseValues result)
        {
            try
            {
                if (values?.Any() ?? false)
                {
                    result = values.First(x => (x.IsFinished));
                    return true;
                }
            }
            catch { }
            result = null;
            return false;
        }

        public bool ResponseContainsFinalized(out DeviceResponseValues responseValues)
        {
            return ResponseContainsFinalized(DeviceResponses, out responseValues);
        }

        #region Write
        /************************************************
         * FUNCTION:    Write
         * DESCRIPTION:
         ************************************************/
        public CmdSend CmdSended { get; private set; }
        //public OpCode OpCode { get; private set; }
        public string CMD_Sended { get { return PortReader.CMD_sended; } }
        public string CMD_Received { get; private set; }

        private readonly object _BalanceSend = new object();

        private DateTime _LastCmdSended;
        public void Send(OpCode opCode, string add = null)
        {
            if (opCode == OpCode.BoxIdentification)
            {
                GetBoxID();
                return;
            }
            lock (_BalanceSend)
            {
                while ((DateTime.Now - _LastCmdSended).TotalSeconds < 1)
                {

                }
                _LastCmdSended = DateTime.Now;
                var cmd = new CmdSend(opCode, add);
                CmdSended = PortReader.Send(cmd);
                if (Logger != null)
                {
                    Logger.Save(null, OpCode.cmdsend, CmdSended.CmdText);
                }
            }
        }

        public void Send(string command)
        {
            _LastCmdSended = DateTime.Now;
            PortReader.Send(command);
        }

        public void Send(CmdSend cmd)
        {
            if (cmd.OpCode == OpCode.BoxIdentification)
            {
                GetBoxID();
                return;
            }
            lock (_BalanceSend)
            {
                while ((DateTime.Now - _LastCmdSended).TotalSeconds < 1)
                {

                }
                _LastCmdSended = DateTime.Now;
                CmdSended = PortReader.Send(cmd);
                if (Logger != null)
                {
                    Logger.Save(null, OpCode.cmdsend, CmdSended.CmdText);
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

        public BoxCalMode CalStatus { get; private set; } = BoxCalMode.NotDefined;
        public event EventHandler<BoxCalMode> CalStatusChanged;
        protected virtual void OnCalStatusChanged(DeviceResponseValues drv)
        {
            Task.Run(() =>
            {
                try
                {
                    if (CalStatus.Hex == drv.BoxCalMode.Hex) { return; }
                    CalStatus = drv.BoxCalMode;
                    CalStatusChanged?.Invoke(this, drv.BoxCalMode);
                }
                catch { }
            });
        }

        public BoxMode BoxMode { get; private set; } = BoxMode.Box_Idle;
        public event EventHandler<BoxMode> BoxModeChanged;
        protected virtual void OnBoxModeChanged(DeviceResponseValues drv, DeviceResponseValues drvLast)
        {
            Task.Run(() =>
            {
                try
                {
                    if (BoxMode.Hex == drv.BoxMode.Hex)
                    { return; }
                    SetProgressAsync(drv);
                    BoxMode = drv.BoxMode;
                    BoxModeChanged?.Invoke(this, drv.BoxMode);
                }
                catch { }
            });
        }

        public event EventHandler<EventDataArgs> DataReceived;
        private static readonly object _BalanceDataReceived = new object();

        protected virtual void OnDataReceived(EventDataArgs e)
        {
            lock (_BalanceDataReceived)
            {
                if (e == null)
                {
                    return;
                }
                _DeviceResponsesQueue.Enqueue(e);
            }
            InsertReceivedAsync();
        }

        private bool IsInserting;
        private Task InsertReceivedAsync()
        {
            return Task.Run(() =>
            {
                if (IsInserting)
                {
                    return;
                }
                IsInserting = true;
                try
                {
                    WhileResponse();
                    Task.Delay(500);
                    WhileResponse();
                }
                finally
                {
                    IsInserting = false;
                }
            });
        }

        private void WhileResponse()
        {
            while (_DeviceResponsesQueue.Count > 0)
            {
                var e = _DeviceResponsesQueue.Dequeue();
                DeviceResponses.Add(e.DeviceResponseValue);
                OnBoxModeChanged(e.DeviceResponseValue, ResponseLast);
                OnCalStatusChanged(e.DeviceResponseValue);
                InsertMeasurementsAsync(e.DeviceResponseValue);
                CMD_Received = e.DeviceResponseValue.OpCodeResp.ToString().ToLower();
                if (e.DeviceResponseValue.IsError)
                {
                    OnErrorReceived(e.DeviceResponseValue);
                }
                if (e.DeviceResponseValue.IsFinished)
                {
                    OnFinalizedReceived(e.DeviceResponseValue);
                }
                Task.Run(() =>
                {
                    try
                    {
                        DataReceived?.Invoke(this, e);
                    }
                    catch { }
                });

                if (string.IsNullOrEmpty(e.DeviceResponseValue.MeasValues.I_Set) == false)
                {
                    ResponseLast = e.DeviceResponseValue;
                }
            }
        }

        public bool IsError { get; private set; }
        public event EventHandler<DeviceResponseValues> ErrorReceived;
        public void OnErrorReceived(DeviceResponseValues e)
        {
            Task.Run(() =>
            {
                try
                {
                    IsError = true;
                    ItemValues.ErrorDetected = true;
                    ErrorReceived?.Invoke(this, e);
                }
                catch { }
            });
        }

        public bool IsFinalized { get; private set; }
        public event EventHandler<DeviceResponseValues> FinalizedReceived;
        protected virtual void OnFinalizedReceived(DeviceResponseValues e)
        {
            Task.Run(() =>
            {
                try
                {
                    IsFinalized = true;
                    FinalizedReceived?.Invoke(this, e);
                }
                catch { }
            });
        }

        public event EventHandler<DeviceResponseValues> ProgressChanged;
        protected virtual void OnProgressChanged(DeviceResponseValues e)
        {
            Task.Run(() =>
            {
                try
                {
                    ProgressChanged?.Invoke(this, e);
                }
                catch
                {

                }
            });
        }

        public event EventHandler<DeviceResponseValues> MeasurementChanged;
        protected virtual void OnMeasurementChanged(DeviceResponseValues e)
        {
            Task.Run(() =>
            {
                try
                {
                    MeasurementChanged?.Invoke(this, e);
                }
                catch
                { }
            });
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
        public Task SetProgressAsync(DeviceResponseValues drv)
        {
            return Task.Run(() =>
            {
                InsertDTprogress(drv);
            });
        }

        private object _LockDTinsert = new object();
        private void InsertDTprogress(DeviceResponseValues drv)
        {
            lock (_LockDTinsert)
            {
                if (IsDebug) { return; }
                if (_DT_Progress == null) { _DT_Progress = DT_Progress; }
                if (BoxParse.Set_Progress(ref _DT_Progress, drv))
                {
                    if (drv.BoxMode.Hex != _LastMode)
                    {
                        _LastMode = drv.BoxMode.Hex;
                        OnProgressChanged(drv);
                    }
                }
            }
        }

        /// <summary>
        /// Usage only from UserInterfaces
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMode"></param>
        /// <param name="value"></param>
        public Task SetUIProgressAsync(BoxModeCodes code, BoxErrorMode errorMode, string value = null)
        {
            var boxMode = BoxMode.FromHex(code);
            return SetProgressAsync(boxMode, errorMode, value);
        }

        /// <summary>
        /// Usage only from UserInterfaces
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMode"></param>
        /// <param name="value"></param>
        public Task SetProgressAsync(BoxMode boxMode, BoxErrorMode errorMode, string value = null)
        {
            var drv = new DeviceResponseValues(boxMode, value, errorMode);
            return SetProgressAsync(drv);
        }

        public Task SetUIProgressAsync(string boxmode_hex, string value = null, string errorcode = null)
        {
            var drv = new DeviceResponseValues(boxmode_hex, value, errorcode);
            return SetProgressAsync(drv);
        }

        /************************************************
         * FUNCTION:    Measurement
         * DESCRIPTION:
         ************************************************/

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

        private Task InsertMeasurementsAsync(DeviceResponseValues drv)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (drv.IsMeasValues == false) { return; }
                    if (ItemValues.AddMeasurement(drv, out var results))
                    {
                        if (BoxParse.Insert_DT_Measurement(DT_Measurements, drv, results))
                        {
                            OnMeasurementChanged(drv);
                        }
                        SetProgressAsync(drv);
                    }
                }
                catch { }
            });
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
            //_DeviceResponsesQueue.Clear();
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
