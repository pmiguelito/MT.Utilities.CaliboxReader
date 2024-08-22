using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadCalibox.clGlobals;

namespace ReadCalibox
{

    class SerialReaderThread
    {
        public Thread Thread_Sreader;
        public SerialPort Port;
        private int ReadDelay { get; }
        private bool ReadLine;
        public bool Running = false;
        public bool PortIsOpen { get { return Port.IsOpen; } }
        public void ClosePort()
        {
            Port.Close();
        }

        public SerialReaderThread(SerialPort port, int readDelay, bool readLinie)
        {
            Port = port;
            //_SerialPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            if(Thread_Sreader != null) { Thread_Sreader.Abort(); }
            Thread_Sreader = new Thread(RunMethod);
            Thread_Sreader.Name = $"Thread_Sreader {Port.PortName}_{Thread_Sreader.ManagedThreadId}";
            Thread_Sreader.IsBackground = true;
            ReadLine = readLinie;
            ReadDelay = readDelay;
            closed = false;
        }
        public void Start()
        {
            closed = false;
            if (Thread_Sreader != null) { Thread_Sreader.Abort(); }
            Thread_Sreader = new Thread(RunMethod);
            Thread_Sreader.Name = $"Thread_Sreader {Port.PortName}_{Thread_Sreader.ManagedThreadId}";
            Thread_Sreader.IsBackground = true;
            Thread_Sreader.Start();
        }
        public void Start(int timeout)
        {
            closed = false;
            Init_Timer();
            TimeOut = timeout;
            TimeElapsed = false;
            Timer_TimeOut.Start();
            Thread_Sreader.IsBackground = true;
            Thread_Sreader.Start();
        }
        public void Stop()
        {
            if (Port.IsOpen) { Port.Close(); }
            try { Thread_Sreader.Abort(); } catch { }
        }

        // note: this event is fired in the background thread
        public event EventHandler<DataEventArgs> DataReceived;

        private bool closed = false;
        private void Close()
        {
            closed = true;
            Stop();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string txt = Port.ReadLine();
            DataReceived(this, new DataEventArgs(txt, Port));
        }

        public void Send(string cmd)
        {
            if (!Thread_Sreader.IsAlive) { Start(); }
            if (!Port.IsOpen) { Port.Open(); }
            Port.Write(cmd.ToString());
            if (!Thread_Sreader.IsAlive) { Start(); }
            //Thread_Sreader.Start();
            //Start();
        }
        private byte _terminator = 0x4;
        private void RunMethod()
        {
            startTime = DateTime.Now;
            while (!closed)
            {
                string tString = "";
                if (Port.IsOpen)
                {
                    Running = true;
                    try
                    {
                        Thread.Sleep(ReadDelay);
                        if (ReadLine)
                        { tString = Port.ReadLine(); }
                        else
                        { tString = Port.ReadExisting(); }
                        if (tString != "")
                        {
                            DataReceived(this, new DataEventArgs(tString, Port));
                        }
                    }
                    catch(Exception e)
                    { closed = true; }
                }
                else
                { closed = true; }
            }
            Running = false;
        }

        /****************************************************************************************************
         * TimeOut:     
         ***************************************************************************************************/
        int TimeOut = 4000;
        private System.Windows.Forms.Timer Timer_TimeOut = new System.Windows.Forms.Timer();
        bool TimeElapsed = false;
        void Init_Timer()
        {
            Timer_TimeOut.Interval = TimeOut ;
            Timer_TimeOut.Tick += new EventHandler(Timer_Tick);
        }
        DateTime startTime;
        private void Timer_Tick(object sender, EventArgs e)
        {
            var timeDiff = (DateTime.Now - startTime).TotalMilliseconds;
            if (timeDiff > TimeOut)
            {
                //_SerialPort.Close();
                //TimeElapsed = true;
                //Timer_TimeOut.Stop();
            }
        }
    }

    public class DataEventArgs : EventArgs
    {
        public string Data { get; private set; }
        public SerialPort SerialPort { get; private set ; }
        public DataEventArgs(string data, SerialPort serialPort)
        {
            Data = data;
            SerialPort = serialPort;
        }
        public DataEventArgs(string data, UC_COM port)
        {
            Data = data;
            SerialPort = port.Serialport;
        }
    }

}

