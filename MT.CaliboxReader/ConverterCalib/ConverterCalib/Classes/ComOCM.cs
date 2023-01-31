using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCalib
{
    class ComOCM
    {

        public SerialPort Port = new SerialPort("COM1") { BaudRate = 9600 };
        private SerialPort NewPort(string portName)
        {
            return new SerialPort(portName)
            {
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                Handshake = Handshake.None
            };
        }

        public void ChangePort(string portName)
        {
            if (Port.IsOpen) { Port.Close(); }
            Port = NewPort(portName);
            Port.DataReceived += Port_DataReceived;
        }

        public List<string> Responces;
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            Responces.Add(port.ReadExisting());
        }

        public void Send(string cmd)
        {
            try
            {
                Responces = new List<string>();
                if (!Port.IsOpen) { Port.Open(); }
                Port.Write(cmd + CR + LF);
            }
            catch
            {

            }
        }

        public const string LocalMode = "SYST:LOC";
        public const string RemoteMode = "SYST:REM";
        public const string Identification = "*IDN?";

        public const string Pt1000 = "PLAT:ZRES 1000";
        public const string Pt1000_20 = "RES 1077.9";
        public const string Pt1000_30 = "RES 1116.7";

        public const string NTC22 = "RES 22000";

        public const char LF = '\n';
        public const char CR = '\r';

        /*****************************************************************************
        * Comm:     Communication Start
        *           Initialization of remote mode to send commands
        '****************************************************************************/
        public bool OpenComm()
        {
            Send(RemoteMode);
            System.Threading.Thread.Sleep(200);
            Send(Identification);
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(200);
            System.Windows.Forms.Application.DoEvents();
            if (Responces.Count > 0) { return true; }
            return false;
        }

        /*****************************************************************************
        * Comm:     Communication Test
        *           
        '****************************************************************************/
        public bool CommTest()
        {
            if (OpenComm()) 
            { 
                CloseComm();
                return true;
            }
            return false;
        }

        /*****************************************************************************
        * Comm:     Communication End
        *           End the remote mode to manual inputs
        '****************************************************************************/
        public void CloseComm()
        {
            Send("OUTP OFF");
            System.Threading.Thread.Sleep(50);
            Send(LocalMode);
            Port.Close();
        }
        /*****************************************************************************
        * Temperatur:   Send Temperatur values
        '****************************************************************************/
        public void SetNTC()
        {
            Send(NTC22);
            System.Threading.Thread.Sleep(50);
            Send("OUTP ON");
        }
        public void SetPT20()
        {
            SetPt1000Temp(20);
        }
        public void SetPT30()
        {
            SetPt1000Temp(30);
        }
        public void SetPt1000Temp(int gradCelcius)
        {
            Send(Pt1000);
            System.Threading.Thread.Sleep(50);
            Send($"PLAT {gradCelcius}");
            System.Threading.Thread.Sleep(50);
            Send("OUTP ON");
        }

    }
}
