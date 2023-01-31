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
using System.Threading;
using System.IO;
using STDhelper;
using static STDhelper.clBGWorker;
using static ReadCalibox.clConfig;
using static ReadCalibox.clGlobals;

namespace ReadCalibox
{
    public partial class UC_DataRead : UserControl
    {
        private static UC_DataRead _instance;
        public static UC_DataRead Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new UC_DataRead(); }
                return _instance;
            }
        }

        /***************************************************************************************
        Constructor:
        ****************************************************************************************/
        static Button Btn_Start, Btn_Stop, Btn_Clear;
        static TextBox TB_COMports;
        public static NumericUpDown Nud_LinesBuffer;
        private static SynchronizationContext MainThread { get; set; }
        private delegate void preventCrossThreading(object arg);
        void ObjRef()
        {
            Btn_Start = _Btn_Start;
            Btn_Stop = _Btn_Stop;
            Btn_Clear = _Btn_Clear;
            TB_COMports = _TB_COMports;
            Nud_LinesBuffer = _NUD_LinesBuffer;
            Nud_LinesBuffer.Value = 80;
            MainThread = SynchronizationContext.Current;
            if (MainThread == null)
            { MainThread = new SynchronizationContext(); }
        }
        public UC_DataRead()
        {
            InitializeComponent();
            ObjRef();
            BGW_MessageWriter = BGW_MessageWriter.Initialize(BGW_MessageWriter_DoWork, BGW_MessageWriter_RunWorkerCompleted);
            Btn_Start.Enabled = true;
            Btn_Stop.Enabled = false;
            Update_ComportCounter(true);
        }

        public BackgroundWorker BGW_MessageReader { get; set; }
        public BackgroundWorker BGW_MessageWriter { get; set; }
        private static bool _DataReadRunning;
        public static bool DataReadRunning
        {
            get { return _DataReadRunning; }
            set
            {
                _DataReadRunning = value;
                Btn_Start.Enabled = !value;
                Btn_Stop.Enabled = value;
            }
        }

        /***************************************************************************************
        Messages:
        ****************************************************************************************/
        static DataTable _DT_Message;
        public static DataTable DT_Message
        {
            get
            {
                if (_DT_Message == null)
                { _DT_Message = Init_DT_Columns(); }
                return _DT_Message;
            }
            set { _DT_Message = value; }
        }
       
        bool Init_DT_Message()
        {
            try
            {
                if (DT_Message == null)
                {
                    DT_Message = Init_DT_Columns();
                    return true;
                }
                else
                {
                    DT_Message.Rows.Clear();
                    return true;
                }
            }
            catch { }
            return false;
        }

        static DataTable Init_DT_Columns()
        {
            DataTable dt = new DataTable("Message");
            dt.Columns.Add("DateTime");
            dt.Columns.Add("Channel");
            dt.Columns.Add("BeM");
            dt.Columns.Add("Wait");
            dt.Columns.Add("Message");
            
            return dt;
        }
        
        void Insert_DT_Message(List<string> values)
        {
            string date = values[0];
            string portName = values[1];
            string bem = values[2];
            string wait = values[3];
            string message = values[4];
            int i = 0;
        again:
            try
            {
                
                message = message.Trim();
                if (!string.IsNullOrEmpty(message))
                {
                    DataRow row = DT_Message.NewRow();
                    row["datetime"] = date;
                    row["channel"] = portName;
                    row["BeM"] = bem;
                    row["Wait"] = wait;
                    row["message"] = message;
                    DT_Message.Rows.Add(row);
                    if (DT_Message.Rows.Count > 20)
                    {
                        DT_Message.Rows[0].Delete();
                    }
                }
            }
            catch
            {
                Init_DT_Message();
                i++;
                if (i < 5)
                { goto again; }
            }
        }

        //public string Path_MeasLog { get { return Form_Main.UC_config.Path_MeasLog; } }
        //public bool Path_MeasLog_Active { get { return Form_Main.UC_config.Path_MeasLog_Active; } }

        /***************************************************************************************
        COMconfig:  
        ****************************************************************************************/
        static DataTable _DT_COMconfig;
        public static DataTable DT_COMconfig
        {
            get
            {
                if (_DT_COMconfig == null)
                { _DT_COMconfig = Init_DT_COMconfig_Columns(); }
                return _DT_COMconfig;
            }
            set { _DT_COMconfig = value; }
        }

        string PortName_Search_DT(string value) { return $"COM = '{value}'"; }
        string ReadWait_Search_DT(string value) { return $"ReadWait = '{value}'"; }
        string BeM_Search_DT(string value) { return $"BeM = '{value}'"; }
        string PortName_Read_DT(DataRow row) { return row.Field<string>("COM"); }
        int ReadWait_Read_DT(DataRow row) { return row.Field<int>("ReadWait"); }
        string BeM_Read_DT(DataRow row) { return row.Field<string>("BeM"); }
        string BeM_Read_DT(string portName)
        {
            DataRow[] rows = DT_COMconfig.Select(PortName_Search_DT(portName));
            if (rows.Length > 0)
            {
                return BeM_Read_DT(rows[0]);
            }
            return "";
        }
        bool Init_DT_COMconfig()
        {
            try
            {
                if (DT_COMconfig == null)
                {
                    DT_COMconfig = Init_DT_COMconfig_Columns();
                    return true;
                }
                else
                {
                    try { DT_COMconfig.Rows.Clear(); } catch { }
                    return true;
                }
            }
            catch { }
            return false;
        }
        static DataTable Init_DT_COMconfig_Columns()
        {
            DataTable dt = new DataTable("COMconfig");
            dt.Columns.Add("COM");
            dt.Columns.Add("ReadWait", typeof(int));
            dt.Columns.Add("BeM");
            return dt;
        }
        void Insert_DT_COMconfig(UC_COM com)
        {
            try
            {
                DataRow row = DT_COMconfig.NewRow();
                row["COM"] = com.PortName;
                row["ReadWait"] = com.ReadDelay;
                row["BeM"] = com.BeM;
                DT_COMconfig.Rows.Add(row);
            }
            catch {  }
        }

        int _ReadWait = 80;
        int ReadWait(SerialPort port)
        {
            DataRow[] rows = DT_COMconfig.Select(PortName_Search_DT(port.PortName));
            if (rows.Length > 0)
            {
                return ReadWait_Read_DT(rows[0]);
            }
            return _ReadWait;
        }
        int ReadWait(string portName)
        {
            DataRow[] rows = DT_COMconfig.Select(PortName_Search_DT(portName));
            if (rows.Length > 0)
            {
                return ReadWait_Read_DT(rows[0]);
            }
            return _ReadWait;
        }

        public static int Log_LinesBuffer
        {
            get { return (int)Nud_LinesBuffer.Value; }
            set { try { Nud_LinesBuffer.Value = value; } catch { } }
        }
       
        /***************************************************************************************
        Executions:    Start & Stop
        ****************************************************************************************/
        int Count_COMactive;
        int Count_COMopen;
        public bool Start(UC_COM port)
        {
            if (port.Start(false))
            {
                if (port.Serialport.IsOpen)
                {
                    SerialReaderThread t = StartThread(port);
                    _Threads.Add(t);
                    //port.COMport.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                    //port.COMport.Write("VER\r");
                    TB_COMports.Text += port.Serialport.PortName + Environment.NewLine; 
                    return true;
                }
            }
            return false;   
        }
        public void Stop(UC_COM port) { try { port.Stop(); } catch { } }
        public void Start_All()
        {
            _Tb_Info.Text = string.Empty;
            _Threads = null;
            try { DT_Message.Rows.Clear();} catch { }
            if(clFiles.Create_FolderFromDir(Config_Initvalues.MeasLog_Path))
            {
                //TODO: clFiles Error modus must be added
                //string message = $"ERROR: {Environment.NewLine + Path_MeasLog}";
                //MessageBox.Show(message);
                //return;
            }
            Update_ComportCounter(true);
            Init_DT_COMconfig();
            OpenComports = new List<UC_COM>();
            foreach (UC_COM port in Config_PortList)
            {
                if (port.Active)
                {
                    Count_COMactive++;
                    if (Start(port))
                    {
                        Insert_DT_COMconfig(port);
                        Count_COMopen++;
                        if (!DataReadRunning)
                        { DataReadRunning = true; }
                        OpenComports.Add(port);
                    }
                }
            }
            Update_ComportCounter();
            DataReadRunning = Count_COMopen > 0;
        }
        List<UC_COM> OpenComports;
        public void Stop_All()
        {
           
            foreach (SerialReaderThread srt in _Threads)
            {
                srt.Stop();
            }
            Application.DoEvents();
            List<SerialPort> open = new List<SerialPort>();
            foreach (UC_COM port in OpenComports)
            {
                if (port.Serialport.IsOpen)
                {
                    open.Add(port.Serialport);
                }
            }
            foreach (UC_COM port in OpenComports)
            {
                if (port.Serialport.IsOpen)
                {
                    port.Serialport.Close();
                }
            }

            //foreach(UC_COM port in OpenComports)
            //{
            //    port.Stop();
            //}
            DataReadRunning = false;
            TB_COMports.Text = "";
            Update_ComportCounter(true);
        }
        public void Update_ComportCounter(bool reset = false)
        {
            if (reset)
            {
                Count_COMopen = 0;
                Count_COMactive = 0;
            }
            _Lbl_COMcounter.Text = $"open: {Count_COMopen.ToString("D2")}/{Count_COMactive.ToString("D2")}";
        }
        

        /***************************************************************************************
        PortReader: 
        ****************************************************************************************/
        private byte _terminator = 0x4;
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs rcvdData)
        {
            SerialPort _serialPort = (SerialPort)sender;
            string portName = _serialPort.PortName;
            Thread.Sleep(ReadWait(portName));
            //Initialize a buffer to hold the received data 
            byte[] buffer = new byte[_serialPort.ReadBufferSize];

            //There is no accurate method for checking how many bytes are read 
            //unless you check the return from the Read method 
            int bytesRead = _serialPort.Read(buffer, 0, buffer.Length);

            string tString = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            ////For the example assume the data we are received is ASCII data. 
            //tString += Encoding.ASCII.GetString(buffer, 0, bytesRead);
            ////Check if string contains the terminator  
            //if (tString.IndexOf((char)_terminator) > -1)
            //{
            //    //If tString does contain terminator we cannot assume that it is the last character received 
            //    string workingString = tString.Substring(0, tString.IndexOf((char)_terminator));
            //    //Remove the data up to the terminator from tString 
            //    tString = tString.Substring(tString.IndexOf((char)_terminator));
            //    //Do something with workingString 
                
            //}
            BGW_MessageWriter = BGW_MessageWriter.Initialize(BGW_MessageWriter_DoWork, true, new string[] { portName, tString });

        }
        bool reading = true;
        List<SerialReaderThread> _Threads = null;
        SerialReaderThread StartThread(UC_COM port)
        {
            if (_Threads == null)
            {
                _Threads = new List<SerialReaderThread>();
            }
            SerialReaderThread t = new SerialReaderThread(port.Serialport, port.ReadDelay_int, port.ReadLine);
            t.DataReceived += ThreadDataReceived;//  ThreadDataReceived;
            t.Start();
            reading = true;
            return t;
        }
        void ThreadDataReceived(object s, EventArgs e)
        {
            //// Note: this method is called in the thread context, thus we must
            //// use Invoke to talk to UI controls. So invoke a method on our
            //// thread.
            //if (reading) Invoke(new EventHandler<DataEventArgs>(ThreadDataReceivedSync), new object[] { s, e });
            var a = (DataEventArgs)e;
            string portName = a.SerialPort.PortName;
            string data = a.Data;
            BGW_MessageWriter = BGW_MessageWriter.Initialize(BGW_MessageWriter_DoWork, true, new string[] { portName, data });
        }

        void ThreadDataReceivedSync(object s, EventArgs e)
        {
            //_Tb_Info.Text += e.Data + "\n";
        }

        /***************************************************************************************
        PortReader: 
        ****************************************************************************************/
        #region BGW_Message
        private void BGW_MessageReader_DoWork(object sender, DoWorkEventArgs e)
        {
            SerialPort port = (SerialPort)e.Argument;
            if (port.IsOpen)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                int w = ReadWait(port);
                int w2 = (w / 4);
                w2 = w2 > 1 ? w2 : 0;
                while (i < 3)
                {
                    while (port.BytesToRead > 0)
                    {
                        try { sb.Append(port.ReadExisting()); } catch { }
                        Thread.Sleep(25);
                    }
                    i++;
                    Thread.Sleep(w2);
                }
                string message = sb.ToString().Trim();
                if (!string.IsNullOrEmpty(message))
                {
                    List<string> values = new List<string> { $"{DateTime.Now}.{DateTime.Now.Millisecond}", port.PortName, BeM_Read_DT(port.PortName), w.ToString(), message};
                    BGW_MessageWriter = BGW_MessageWriter.Initialize(BGW_MessageWriter_DoWork, BGW_MessageWriter_RunWorkerCompleted, true, values);
                }
            }
        }

        private void BGW_MessageReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ParseMessege(e.Result);
        }
        #endregion BGW_Message

        /***************************************************************************************
        Log Writer: 
        ****************************************************************************************/
        #region BGW_Write
        private void BGW_MessageWriter_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument != null)
                {
                    string[] valuesInput = (string[])e.Argument;
                    string portName = valuesInput[0];
                    string message = valuesInput[1];
                    if (ParseMessage(portName, message, out string messageValues, out List<string> values))
                    {
                        SaveLog(messageValues, portName);
                        UpdateGUI(messageValues, values);
                        //Insert_DT_Message(values);
                    }
                }
            }
            catch { }
            //e.Result = e.Argument;
        }

        private void BGW_MessageWriter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //UpdateGUI(e.Result);
        }
        #endregion BGW_Write

        /***************************************************************************************
        Log Message:
        ****************************************************************************************/
        bool ParseMessage(object input, out string portName, out string messageValues, out List<string> values)
        {
            messageValues = "";
            portName = "";
            values = (List<string>)input;
            if (input != null)
            {
                string date = values[0];
                portName = values[1];
                string bem = values[2];
                string wait = values[3];
                string message = values[4];
                switch (bem)
                {
                    case "30326849": //O2 6850
                        break;
                    case "30259861": //Ozon bew sensor
                        if (message == ".")
                        { return false; }
                        break;
                    case "11556": //O2 allg
                        break;
                    case "30014462": //Ozon old sensor
                        if (message == ".")
                        { return false; }
                        else if (message.Length == 3)
                        {
                            if (message.ToLower().Contains("?"))
                            { return false; }
                        }
                        break;
                    case "No":
                        break;
                }
                messageValues = String.Join("\t", values);
                return true;
            }
            return false;
        }
        bool ParseMessage(string portName, string message, out string messageValues, out List<string> values)
        {
            values = null;
            messageValues = message;
            if (!string.IsNullOrEmpty(message))
            {
                
                string bem = BeM_Read_DT(portName);
                string wait = ReadWait(portName).ToString();
                switch (bem)
                {
                    case "30326849": //O2 6850
                        break;
                    case "30259861": //Ozon bew sensor
                        if (message == ".")
                        { return false; }
                        break;
                    case "11556": //O2 allg
                        break;
                    case "30014462": //Ozon old sensor
                        if (message == ".")
                        { return false; }
                        else if (message.Length == 3)
                        {
                            if (message.ToLower().Contains("?"))
                            { return false; }
                        }
                        break;
                    case "No":
                        break;
                }
                string date = $"{DateTime.Now}.{DateTime.Now.Millisecond}";
                messageValues = $"{date}\t{portName}\t{bem}\t{wait}\t{message}";
                values = new List<string> { $"{date}", portName, bem, wait, message};
                return true;
            }
            return false;
        }
        void SaveLog(string values, string portName)
        {
            if (Directory.Exists(Config_Initvalues.MeasLog_Path))
            {
                string filename = "Calibox_" + portName + ".log";
                string path = (Config_Initvalues.MeasLog_Path + @"\" + filename);
                int countAgain = 0;
            again:
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    try
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine("Date\tCOM\tBeM\tWait\tMessage");
                        }
                    }
                    catch
                    {
                        if (countAgain < 2)
                        {
                            countAgain++;
                            goto again;
                        }
                    }
                    countAgain = 0;
                }
                // This text is always added, making the file longer over time
                // if it is not deleted.
                try
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        string me = values.Replace("\r", "\r\t\t\t\t").Trim();
                        sw.WriteLine(me);
                    }
                }
                catch
                {
                    if (countAgain < 5)
                    {
                        countAgain++;
                        goto again;
                    }
                }
            }
        }

        List<string> GUI_Message = new List<string>();
        public void UpdateGUI(string input, List<string> values = null)
        {
            //int numLines = Log_LinesBuffer;
            
                //var linesCount = GUI_Message.Count();
                //GUI_Message.Insert(0, input);
                //if (linesCount > Log_LinesBuffer)
                //{
                //    GUI_Message.RemoveAt(linesCount);
                //}
                if (InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        string trenn = ("-").PadLeft(100, '-');
                        _Tb_Info.AppendText(Environment.NewLine);
                        _Tb_Info.AppendText(trenn);
                        _Tb_Info.AppendText(Environment.NewLine);
                        
                        _Tb_Info.AppendText($"{values[0]}\t{values[1]}\t{values[2]}\t{values[3]}");
                        _Tb_Info.AppendText(Environment.NewLine);
                        _Tb_Info.AppendText(trenn);
                        _Tb_Info.AppendText(Environment.NewLine);
                        _Tb_Info.AppendText($"{values[4].Replace("\r", Environment.NewLine)}");
                        var lines = _Tb_Info.Lines;
                        int lCount = lines.Count();
                        int diff = lCount - Log_LinesBuffer;
                        if (diff > 0)
                        {
                            var nLines = lines.Skip(lCount - Log_LinesBuffer);
                            _Tb_Info.Lines = nLines.ToArray();
                        }
                        _Tb_Info.AppendText(Environment.NewLine);
                        _Tb_Info.AppendText(trenn);
                        //_Tb_Info.Lines = GUI_Message.ToArray();
                    });
                    //_Tb_Info.Invoke((Action)(() => _Tb_Info.AppendText($"{Environment.NewLine}{DateTime.Now}.{DateTime.Now.Millisecond};\t{values[0]};\t{values[1]}")));
                    //_DGV_Message.Invoke((Action)(() => _DGV_Message.Refresh()));
                }
                else
                {
                    _Tb_Info.Lines = GUI_Message.ToArray();
                    //_Tb_Info.AppendText(Environment.NewLine + values[0]);
                    //_DGV_Message.Refresh();
                }
        }


        /***************************************************************************************
        Commands:
        ****************************************************************************************/
        #region Buttons
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            Start_All();
        }
        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            Stop_All();
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            _Tb_Info.Text = "";
            GUI_Message = new List<string>();
            DT_Message.Rows.Clear();
        }
        #endregion Buttons

    }
}
