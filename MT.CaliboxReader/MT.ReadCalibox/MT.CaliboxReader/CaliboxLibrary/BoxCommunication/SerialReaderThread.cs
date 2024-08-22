using CaliboxLibrary.BoxCommunication.CMDs;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace CaliboxLibrary
{

    public class SerialReaderThread
    {
        public SerialReaderThread(string portName, int channelNo)
        {
            ChannelNo = channelNo;
            Port = new SerialPort(portName)
            {
                ReadTimeout = 500,
                WriteTimeout = 500
            };
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
        }

        public SerialReaderThread(SerialPort port, int channelNo)
        {
            ChannelNo = channelNo;
            Port = port;
            Port.ReadTimeout = 500;
            Port.WriteTimeout = 500;
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
        }

        public int ChannelNo { get; set; }
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
        public event EventHandler<EventDataArgs> DataReceived;
        private Task OnDataReceived(EventDataArgs e)
        {
            return Task.Run(() =>
            {
                try
                {
                    DataReceived?.Invoke(this, e);
                }
                catch
                { }
            });
        }

        private void OnDataReceived(string data)
        {
            var args = new EventDataArgs(CmdSended, data);
            OnDataReceived(args);
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReadPort();
        }

        /************************************************
         * FUNCTION:    Read
         * DESCRIPTION:
         ************************************************/

        private readonly object _BalanceRead = new object();
        private void ReadPort()
        {
            lock (_BalanceRead)
            {
                try
                {
                    int count = 0;
                    while (count < 1)
                    {
                        try
                        {
                            string data = Port.ReadLine();
                            if (string.IsNullOrEmpty(data) == false)
                            {
                                var i = data.IndexOf(";g");
                                if (i > 0)
                                {
                                    var first = data.Substring(0, i);
                                    OnDataReceived(first);

                                    var rest = data.Substring(i + 1);
                                    OnDataReceived(rest);
                                    return;
                                }
                                count = 0;
                                OnDataReceived(data);
                            }
                        }
                        catch
                        {

                        }
                        count++;
                        //Thread.Sleep(50);
                    }
                }
                catch
                {

                }
            }
        }

        /****************************************************************************************************
        ** Send:
        ****************************************************************************************************/

        public CmdSend CmdSended { get; set; } = new CmdSend(OpCode.init);
        public string CMD_sended { get { return CmdSended.CmdText; } }

        public CmdSend Send(CmdSend cmd)
        {
            try
            {
                if (!Port.IsOpen) { Port.Open(); }
                CmdSended = cmd;
                cmd.Restart();
                Port.Write(cmd.CmdText);
                return CmdSended;
            }
            catch (Exception ex)
            {
                throw new Exception($"CMD: {cmd.CmdText}" + Environment.NewLine + ex.Message, ex);
            }
        }

        public CmdSend Send(OpCode opCode, string cmd)
        {
            CmdSended = new CmdSend(opCode, cmd);
            Send(CmdSended);
            return CmdSended;
        }

        public void Send(string command)
        {
            Port.Write(command);
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
}

