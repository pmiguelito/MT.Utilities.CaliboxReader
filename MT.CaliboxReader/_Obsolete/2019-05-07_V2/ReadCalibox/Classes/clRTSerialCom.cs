using System;
using System.IO.Ports;
using System.Threading;
using Diagnostics = System.Diagnostics;
using static ReadCalibox.clGlobals;

namespace ReadCalibox
{
    class clRTSerialCom
    {
        public class DataStreamEventArgs : EventArgs
        {
            #region Defines
            private byte[] _bytes;
            #endregion

            #region Constructors
            public DataStreamEventArgs(byte[] bytes)
            {
                _bytes = bytes;
            }
            #endregion

            #region Properties
            public byte[] Response
            {
                get { return _bytes; }
            }
            #endregion
        }

        public class SerialClient : IDisposable
        {
            #region Defines
            private SerialPort _serialPort;
            private Thread serThread;
            private double _PacketsRate;
            private DateTime _lastReceive;
            private UC_Channel Channel;
            /*The Critical Frequency of Communication to Avoid Any Lag*/
            private const int freqCriticalLimit = 20; //20
            #endregion

            #region Constructors
            public SerialClient(SerialPort port, UC_Channel channel)
            {
                Channel = channel;
                _serialPort = port;
                _lastReceive = DateTime.MinValue;

                serThread = new Thread(new ThreadStart(SerialReceiving));
                serThread.Priority = ThreadPriority.Normal;
                serThread.Name = "SerialHandle"+port.PortName + "_" + serThread.ManagedThreadId;
                serThread.Start();
            }
            #endregion

            #region Custom Events
            public event EventHandler<DataStreamEventArgs> OnReceiving;
            #endregion

            #region Properties
            public string ConnectionString
            {
                get
                {
                    return String.Format("[Serial] Port: {0} | Baudrate: {1}",
                        _serialPort.PortName, _serialPort.BaudRate.ToString());
                }
            }
            #endregion

            #region Methods
            #region Port Control
            public bool OpenConn()
            {
                try
                {
                    if (!_serialPort.IsOpen)
                    {
                        _serialPort.ReadTimeout = -1;
                        _serialPort.WriteTimeout = -1;

                        _serialPort.Open();

                        if (_serialPort.IsOpen)
                            serThread.Start(); /*Start The Communication Thread*/
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }
            public bool OpenConn(SerialPort port)
            {
                return OpenConn();
            }
            public void CloseConn()
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    serThread.Abort();

                    if (serThread.ThreadState == ThreadState.Aborted)
                        _serialPort.Close();
                }
            }
            public bool ResetConn()
            {
                CloseConn();
                return OpenConn();
            }
            #endregion
            #region Transmit/Receive
            public void Transmit(byte[] packet)
            {
                _serialPort.Write(packet, 0, packet.Length);
            }
            public int Receive(byte[] bytes, int offset, int count)
            {
                int readBytes = 0;

                if (count > 0)
                {
                    readBytes = _serialPort.Read(bytes, offset, count);
                }

                return readBytes;
            }
            #endregion
            #region IDisposable Methods
            public void Dispose()
            {
                CloseConn();

                if (_serialPort != null)
                {
                    _serialPort.Dispose();
                    _serialPort = null;
                }
            }
            #endregion
            #endregion

            #region Threading Loops
            private void SerialReceiving()
            {
                _lastReceive = DateTime.Now;
                while (true)
                {
                    int count = _serialPort.BytesToRead;

                    /*Get Sleep Inteval*/
                    TimeSpan tmpInterval = (DateTime.Now - _lastReceive);

                    /*Form The Packet in The Buffer*/
                    byte[] buf = new byte[count];
                    int readBytes = Receive(buf, 0, count);

                    if (readBytes > 0)
                    {
                        //OnSerialReceiving(buf);
                        Channel.WriteMessage(_serialPort, buf);
                    }

                    #region Frequency Control
                    _PacketsRate = ((_PacketsRate + readBytes) / 2);

                    _lastReceive = DateTime.Now;

                    if ((double)(readBytes + _serialPort.BytesToRead) / 2 <= _PacketsRate)
                    {
                        if (tmpInterval.TotalMilliseconds > 0)
                            Thread.Sleep(tmpInterval.TotalMilliseconds > freqCriticalLimit ? freqCriticalLimit : tmpInterval.Milliseconds);

                        /*Testing Threading Model*/
                        Diagnostics.Debug.Write(tmpInterval.Milliseconds.ToString());
                        Diagnostics.Debug.Write(" - ");
                        Diagnostics.Debug.Write(readBytes.ToString());
                        Diagnostics.Debug.Write("\r\n");
                    }
                    #endregion
                }

            }
            #endregion

            #region Custom Events Invoke Functions
            private void OnSerialReceiving(byte[] res)
            {
                if (OnReceiving != null)
                {
                    OnReceiving(this, new DataStreamEventArgs(res));
                }
            }
            #endregion
        }

    }
}
