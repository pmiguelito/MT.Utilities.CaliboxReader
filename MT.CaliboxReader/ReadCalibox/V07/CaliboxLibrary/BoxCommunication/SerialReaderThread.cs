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

namespace CaliboxLibrary
{

    public class SerialReaderThread
    {
        public SerialReaderThread(string portName, string channelName)
        {
            ChannelName = channelName;
            Port = new SerialPort(portName)
            {
                ReadTimeout = 500,
                WriteTimeout = 500
            };
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
        }

        public SerialReaderThread(SerialPort port, string channelName)
        {
            ChannelName = channelName;
            Port = port;
            Port.ReadTimeout = 500;
            Port.WriteTimeout = 500;
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
        }

        public string ChannelName { get; set; }
        public SerialPort Port { get; private set; }
        public bool Running = false;
        public bool PortIsOpen
        {
            get
            {
                if (Port == null)
                { return false; }
                return Port.IsOpen;
            }
        }

        public void Close()
        {
            if (Port != null && Port.IsOpen)
            {
                Port.Close();
            }
        }

        /************************************************
         * FUNCTION:    Events
         * DESCRIPTION:
         ************************************************/
        public event EventHandler<DataEventArgs> DataReceived;
        private void OnDataReceived(DataEventArgs e)
        {
            var handler = DataReceived;
            handler?.Invoke(this, e);
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReadPort();
        }



        /************************************************
         * FUNCTION:    Read
         * DESCRIPTION:
         ************************************************/
        private void StartRead()
        {
            //StopRead = true;
            //try
            //{
            //    if (ReadThread.IsAlive)
            //    {
            //        ReadThread.Abort();
            //    }
            //    ReadThread = new Thread(ReadPort);
            //    ReadThread.Start();
            StartReading = DateTime.Now;
            //}
            //catch(Exception ex)
            //{

            //}
        }
        private readonly object BalanceRead = new object();
        private Thread ReadThread;
        private bool IsReading;
        private DateTime StartReading;
        private bool StopRead;
        private void ReadPort()
        {
            //if (IsReading) { return; }
            lock (BalanceRead)
            {
                //if (IsReading) { return; }
                try
                {
                    StopRead = false;
                    IsReading = true;
                    StartReading = DateTime.Now;
                    int count = 0;
                    while (count < 1)
                    {
                        //if (StopRead) { return; }
                        try
                        {
                            string txt = Port.ReadLine();
                            if (!string.IsNullOrEmpty(txt))
                            {
                                count = 0;
                                //txt = txt.Replace("\0", "");
                                Task.Factory.StartNew(() =>
                                {
                                    var args = new DataEventArgs(OpCode, CMD_sended, txt);
                                    OnDataReceived(args);
                                });
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        count++;
                        Thread.Sleep(50);
                    }
                    IsReading = false;
                }
                catch (Exception ex)
                {

                }
            }
        }

        /****************************************************************************************************
        ** Send:
        ****************************************************************************************************/
        public OpCode OpCode { get; private set; }
        public string CMD_sended { get; private set; }
        public void Send(OpCode opCode, string cmd)
        {
            try
            {
                if (!Port.IsOpen) { Port.Open(); }
                OpCode = opCode;
                StartRead();
                if (String.IsNullOrEmpty(cmd))
                {
                    cmd = opCode.ToString();
                }
                CMD_sended = cmd;
                Port.Write(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception($"CMD: {cmd}" + Environment.NewLine + ex.Message, ex);
            }
        }

        /****************************************************************************************************
        ** Stop:
        ****************************************************************************************************/
        public void Stop()
        {
            if (Port.IsOpen)
            {
                Port.Close();
            }
        }

    }

    public class DataEventArgs : EventArgs
    {
        public string CMD_send { get; private set; }
        public OpCode OpCode { get; private set; }
        public string Data { get; private set; }
        public DataEventArgs(OpCode opCode, string cmd, string data)
        {
            OpCode = opCode;
            CMD_send = cmd;
            Data = data;
        }
    }

}

