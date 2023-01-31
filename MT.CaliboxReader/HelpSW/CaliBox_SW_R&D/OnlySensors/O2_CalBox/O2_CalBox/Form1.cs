using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace O2_CalBox
{
	

    public delegate void showstring(string str);

    public partial class Form1 : Form
    {
        public showstring m_ShowString;
        public showstring m_ShowSensorString;

        public string[] Serialp { get; private set; }

        public Form1()
        {
            InitializeComponent();
            m_ShowString = new showstring(DispatchBoxPage);
            m_ShowSensorString = new showstring(DispatchSensorPage);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try
                {

                timer1.Stop();
                serialPort1.Close();
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled= false;
            btnClose.Enabled = true;
            try
            {

                serialPort1.PortName = cboPort.Text;
                serialPort1.Open();
                serialPort1.BaudRate=19200;
                timer1.Start();
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string A = serialPort1.ReadExisting();
            string Time;
            Time = DateTime.Now.ToLongTimeString();

            string[] X;//=A.Split(' ');
            string[] Y;//=A.Split(' ');

            if (A != "") {

                X = A.Split(' ');
                Y = A.Split(';');
                if(( Y[0] == "g901")||(Y[0] == "g903"))
                {
                    richTextBox1.AppendText(Time +';'+ A); ;
                }
                else
                {
                    richTextBox1.AppendText(A);
                }
                    richTextBox1.ScrollToCaret();

                if ((Y[0] == "g901") || (Y[0] == "g903"))
                {
                    if(Y.Count() < 2) { return; }
                    string ss = Y[1];
                    if (string.IsNullOrEmpty(ss)) { return; }
                    byte aa = Convert.ToByte(ss, 16);
                    switch (aa)
                    {
                        case Constants.CalibMode_674mV_Low_1: labelBoxStatus.Text = "CalibMode_674mV_Low_1"; break;
                        case Constants.CalibMode_674mV_Low_2: labelBoxStatus.Text = "CalibMode_674mV_Low_2"; break;
                        case Constants.CalibMode_674mV_High_1: labelBoxStatus.Text = "CalibMode_674mV_High_1"; break;
                        case Constants.CalibMode_674mV_High_2: labelBoxStatus.Text = "CalibMode_674mV_High_2"; break;
                        case Constants.CalibMode_500mV_Low_1: labelBoxStatus.Text = "CalibMode_500mV_Low_1"; break;
                        case Constants.CalibMode_500mV_Low_2: labelBoxStatus.Text = "CalibMode_500mV_Low_2"; break;
                        case Constants.CalibMode_500mV_High_1: labelBoxStatus.Text = "CalibMode_500mV_High_1"; break;
                        case Constants.CalibMode_500mV_High_2: labelBoxStatus.Text = "CalibMode_500mV_High_2"; break;
                        case Constants.VerifyMode_674mV_Low_1: labelBoxStatus.Text = "VerifyMode_674mV_Low_1"; break;
                        case Constants.VerifyMode_674mV_Low_2: labelBoxStatus.Text = "VerifyMode_674mV_Low_2"; break;
                        case Constants.VerifyMode_674mV_High_1: labelBoxStatus.Text = "VerifyMode_674mV_High_1"; break;
                        case Constants.VerifyMode_674mV_High_2: labelBoxStatus.Text = "VerifyMode_674mV_High_2"; break;
                        case Constants.VerifyMode_500mV_Low_1: labelBoxStatus.Text = "VerifyMode_500mV_Low_1"; break;
                        case Constants.VerifyMode_500mV_Low_2: labelBoxStatus.Text = "VerifyMode_500mV_Low_2"; break;
                        case Constants.VerifyMode_500mV_High_1: labelBoxStatus.Text = "VerifyMode_500mV_High_1"; break;
                        case Constants.VerifyMode_500mV_High_2: labelBoxStatus.Text = "VerifyMode_500mV_High_2"; break;
                        case Constants.VerifyTemp: labelBoxStatus.Text = "VerifyTemp"; break;
                        case Constants.CalibMode_674CalculationLow: labelBoxStatus.Text = "CalibMode_674CalculationLow"; break;
                        case Constants.CalibMode_674CalculationHigh: labelBoxStatus.Text = "CalibMode_674CalculationHigh"; break;
                        case Constants.CalibMode_500CalculationLow: labelBoxStatus.Text = "CalibMode_500CalculationLow"; break;
                        case Constants.CalibMode_500CalculationHigh: labelBoxStatus.Text = "CalibMode_500CalculationHigh"; break;
                        case Constants.SuccessfullSensorCalibration: labelBoxStatus.Text = "SuccessfullSensorCalibration"; break;
                        case Constants.Box_SensorCheckUpol_500: labelBoxStatus.Text = "Box_SensorCheckUpol_500"; break;
                        case Constants.ShowErrorValues: labelBoxStatus.Text = "ShowErrorValues"; break;
                        case Constants.DebugUpolOnCathode: labelBoxStatus.Text = "DebugUpolOnCathode"; break;
                        case Constants.DebugUpolOnAnode: labelBoxStatus.Text = "DebugUpolOnAnode"; break;
                        case Constants.ReadPage16: labelBoxStatus.Text = "ReadPage16"; break;
                        case Constants.RS232_OW_ACCESS: labelBoxStatus.Text = "RS232_OW_ACCESS"; break;
                        case Constants.VerifyUpol: labelBoxStatus.Text = "VerifyUpol"; break;
                        case Constants.Box_Idle: labelBoxStatus.Text = "Box_Idle"; break;
                        case Constants.Box_WritePage_00: labelBoxStatus.Text = "Box_WritePage_00"; break;
                        case Constants.Box_WritePage_01: labelBoxStatus.Text = "Box_WritePage_01"; break;
                        case Constants.Box_WritePage_12: labelBoxStatus.Text = "Box_WritePage_12"; break;
                        case Constants.Box_WritePage_15: labelBoxStatus.Text = "Box_WritePage_15"; break;
                        case Constants.Box_SensorCheckUpol_674: labelBoxStatus.Text = "Box_SensorCheckUpol_674"; break;
                        case Constants.Box_SensorVerification: labelBoxStatus.Text = "Box_SensorVerification"; break;
                        case Constants.Box_SensorError: labelBoxStatus.Text = "Box_SensorError"; break;
                        case Constants.Box_SensorWriteCalData674: labelBoxStatus.Text = "Box_SensorWriteCalData674"; break;
                        case Constants.Box_SensorWriteCalData500: labelBoxStatus.Text = "Box_SensorWriteCalData500"; break;
                        case Constants.Box_StartSensorCalibration: labelBoxStatus.Text = "Box_StartSensorCalibration"; break;
                        case Constants.SensorFail: labelBoxStatus.Text = "SensorFail"; break;
                        case Constants.SensorCalibFinalise: labelBoxStatus.Text = "SensorCalibFinalise"; break;
                        case Constants.Box_Calibration: labelBoxStatus.Text = "Box_Calibration"; break;
                        case Constants.WEP_Test: labelBoxStatus.Text = "WEP_Test"; break;
                        case Constants.WEP_674mV_Low_1: labelBoxStatus.Text = "WEP_674mV_Low_1"; break;
                        case Constants.WEP_674mV_Low_2: labelBoxStatus.Text = "WEP_674mV_Low_2"; break;
                        case Constants.WEP_500mV_Low_1: labelBoxStatus.Text = "WEP_500mV_Low_1"; break;
                        case Constants.WEP_500mV_Low_2: labelBoxStatus.Text = "WEP_500mV_Low_2"; break;
                        case Constants.WEP_674mV_High_1: labelBoxStatus.Text = "WEP_674mV_High_1"; break;
                        case Constants.WEP_674mV_High_2: labelBoxStatus.Text = "WEP_674mV_High_2"; break;
                        case Constants.WEP_500mV_High_1: labelBoxStatus.Text = "WEP_500mV_High_1"; break;
                        case Constants.WEP_500mV_High_2: labelBoxStatus.Text = "WEP_500mV_High_2"; break;
                        case Constants.WEP_SensorError: labelBoxStatus.Text = "WEP_SensorError"; break;
                        case Constants.WEPSensorFail: labelBoxStatus.Text = "WEPSensorFail"; break;
                        case Constants.SensorWepFinalise: labelBoxStatus.Text = "SensorWepFinalise"; break;
                        case Constants.WEP_SensorCheckUpol: labelBoxStatus.Text = "WEP_SensorCheckUpol"; break;
                        case Constants.WEP_TempCheck: labelBoxStatus.Text = "WEP_TempCheck"; break;
                        default: labelBoxStatus.Text = "Status Not defined"; break;
                    }
                }

                    //                if ((X[0] == "#rdbx") && (X[1] == Constants.BoxCalTolerancePage.ToString("X2")))
                    try
                    {
                    if (X[0] == "#rdbx")
                    {
                        Invoke(m_ShowString, new Object[] { X[1] + ' ' + X[2] });
                        //DispatchBoxPage();

                    }
                }
                catch { MessageBox.Show("Something was wrong"); }
                try
                {
                    if (X[0] == "#rdpg")
                    {
                        Invoke(m_ShowSensorString, new Object[] { X[1] + ' ' + X[2] });
                        //DispatchBoxPage();

                    }
                }
                catch { MessageBox.Show("Something was wrong"); }

            }
        }
        void DispatchBoxPage(string stream)
        {
            string[] B;
            B = stream.Split(' ');
            string s = B[0];
            int page = Convert.ToInt16(s,16);

            int j;
            int Byte, crc, mask;
            uint checksum;
            int NumberChars = B[1].Length;
            byte[] bytes = new byte[NumberChars / 2];
            for ( j = 0; j < NumberChars - 2; j += 2)
            {
                bytes[j / 2] = Convert.ToByte(B[1].Substring(j, 2), 16);
            }
            int i = 1;
            try
            {
                switch (page)
                {
                    //case Constants.BoxCalTolerancePage:
                    case 10:
                        textBoxCAL_Low_1.Text = Convert.ToString(bytes[i + 1]  * 256 + bytes[i]); i += 2;
                        textBoxCAL_Low_2.Text = Convert.ToString(bytes[i + 1]  * 256 + bytes[i]); i += 2;
                        textBoxCAL_High_1.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxCAL_High_2.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;

                        textBoxVER_Low_1.Text = Convert.ToString(bytes[i + 1]  * 256 + bytes[i]); i += 2;
                        textBoxVER_Low_2.Text = Convert.ToString(bytes[i + 1]  * 256 + bytes[i]); i += 2;
                        textBoxVER_High_1.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxVER_High_2.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;

                        textBoxStdDevMB1L1.Text = Convert.ToString(bytes[i] / 10.0); i += 1;
                        textBoxStdDevMB1L2.Text = Convert.ToString(bytes[i] / 10.0); i += 1;
                        textBoxStdDevMB2L1.Text = Convert.ToString(bytes[i] / 10.0); i += 1;
                        textBoxStdDevMB2L2.Text = Convert.ToString(bytes[i] / 10.0); i += 1;


                        //int j;
                        //int Byte, crc, mask;
                        i = 0;

                        checksum = 0;             /* The checksum mod 2^16. */

                            for (int x = 0; x < 32; x++)
                            {
                                checksum = (checksum >> 1) + ((checksum & (uint)1) << 15);
                                checksum += bytes[x];
                                checksum &= 0xffff;       /* Keep it within bounds. */
                            }
                        
                        label_CS10.Text = checksum.ToString();//Convert.ToString(crc);

                        break;
                    //case Constants.BoxCalSollValues:
                    case 31:
                            textBoxMB1Zero.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                            label17.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i] - 12480) * 0.004);
                            textBoxMB1_175nA.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                            label18.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i] - 12480) * 0.2);
                            textBoxMB2_175nA.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                            label19.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i] - 12480) * 0.2);
                            textBoxMB2_4700nA.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i])); i += 2;
                            double helper = (bytes[i + 1] * 256 + bytes[i]) / 8.0;
                            helper = helper * 243000 / (3000 - helper);
                            helper = ((3740 * 298.15) / ((3740 + Math.Log(helper / 22000) * 298.15))) - 273.15;
                            label21.Text = Convert.ToString((float)helper);
                            textBoxT25Deg.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;

                            helper = (bytes[i + 1] * 256 + bytes[i]);
                            helper /= 10.0;
                            label33.Text = Convert.ToString(helper);
                            textBoxUpolErr.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i])); i += 2;
                            textBoxNr.Text = Convert.ToString(bytes[i]); i += 1;
                            helper = (bytes[i + 1] * 256 + bytes[i]) / 8.0;
                            helper = helper * 243000 / (3000 - helper);
                            helper = ((3740 * 298.15) / ((3740 + Math.Log(helper / 22000) * 298.15))) - 273.15;
                            label20.Text = Convert.ToString((float)helper);

                            textBoxT5Deg.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                            helper = (bytes[i + 1] * 256 + bytes[i]) / 8.0;
                            helper = helper * 243000 / (3000 - helper);
                            helper = ((3740 * 298.15) / ((3740 + Math.Log(helper / 22000) * 298.15))) - 273.15;
                            label22.Text = Convert.ToString((float)helper);
                            textBoxT50Deg.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;

                            helper = bytes[i] / 100.0;
                            label35.Text = Convert.ToString(helper);
                            textBoxTerror.Text = Convert.ToString(bytes[i]); i += 1;

                            //int j;
                            //int Byte, crc, mask;
                            i = 0;

                            checksum = 0;             /* The checksum mod 2^16. */

                            for (int x = 0; x < 32; x++)
                            {
                                checksum = (checksum >> 1) + ((checksum & (uint)1) << 15);
                                checksum += bytes[x];
                                checksum &= 0xffff;       /* Keep it within bounds. */
                            }

                            label_CS31.Text = checksum.ToString();//Convert.ToString(crc);

                            break;




                }
                }
                catch { MessageBox.Show("Somthing was wrong");  }

        }
        void DispatchSensorPage(string stream)
        {
            string[] B;
            B = stream.Split(' ');
            string s = B[0];
            int page = Convert.ToInt16(s, 16);
            Int16 ihelper;
            double fhelper;
            int NumberChars = B[1].Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int j = 0; j < NumberChars - 2; j += 2)
            {
                bytes[j / 2] = Convert.ToByte(B[1].Substring(j, 2), 16);
            }


            int i = 1;
            try
            {
                switch (page)
                {
                    case 15:
                        textBoxCAL_Low_1.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxCAL_Low_2.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxCAL_High_1.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxCAL_High_2.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;

                        textBoxVER_Low_1.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxVER_Low_2.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxVER_High_1.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;
                        textBoxVER_High_2.Text = Convert.ToString(bytes[i + 1] * 256 + bytes[i]); i += 2;

                        break;
                    //case Constants.SensorCalibrationDataPage:
                    case 14:
                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = Convert.ToDouble((double)ihelper/100.0);
                        labelTOff.Text = Convert.ToString(fhelper);
                        textBoxTOff.Text = Convert.ToString(ihelper); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper =(ihelper / 100000.0)+1.0;
                        labelTGain.Text = Convert.ToString(fhelper);
                        //labelTGain.Text = (fhelper.ToString());
                        textBoxTGain.Text = Convert.ToString((short)((((short)bytes[i + 1]) * 256) + (short)bytes[i])); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = Convert.ToDouble((double)ihelper / 100.0);
                        labelMB1Off.Text = Convert.ToString(fhelper);
                        textBoxMB1Offset.Text = Convert.ToString(ihelper); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = (ihelper / 100000.0)+1.0;
                        labelMB1Gain.Text = Convert.ToString(fhelper);
                        textBoxMB1Gain.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i])); i += 2;

                        ihelper = (short)(((bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = Convert.ToDouble((double)ihelper / 100.0);
                        labelMB2Off.Text = Convert.ToString(fhelper);
                        textBoxMB2Offset.Text = Convert.ToString(ihelper); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = (ihelper / 100000.0)+1.0;
                        labelMB2Gain.Text = Convert.ToString(fhelper);
                        textBoxMB2Gain.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i])); i += 2;


                        break;
                    case 30:
                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = Convert.ToDouble((double)ihelper / 100.0);
                        labelTOff30.Text = Convert.ToString(fhelper);
                        textBoxTOff30.Text = Convert.ToString(ihelper); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = (ihelper / 100000.0) + 1.0;
                        labelTGain30.Text = Convert.ToString(fhelper);
                        //labelTGain.Text = (fhelper.ToString());
                        textBoxTGain30.Text = Convert.ToString((short)((((short)bytes[i + 1]) * 256) + (short)bytes[i])); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = Convert.ToDouble((double)ihelper / 100.0);
                        labelMB1Offset30.Text = Convert.ToString(fhelper);
                        textBoxMB1Offset30.Text = Convert.ToString(ihelper); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = (ihelper / 100000.0) + 1.0;
                        labelMB1Gain30.Text = Convert.ToString(fhelper);
                        textBoxMB1Gain30.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i])); i += 2;

                        ihelper = (short)(((bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = Convert.ToDouble((double)ihelper / 100.0);
                        labelMB2Offset30.Text = Convert.ToString(fhelper);
                        textBoxMB2Offset30.Text = Convert.ToString(ihelper); i += 2;

                        ihelper = (short)((((short)bytes[i + 1]) * 256) + (short)bytes[i]);
                        fhelper = (ihelper / 100000.0) + 1.0;
                        labelMB2Gain30.Text = Convert.ToString(fhelper);
                        textBoxMB2Gain30.Text = Convert.ToString((bytes[i + 1] * 256 + bytes[i])); i += 2;


                        break;


                }
            }
            catch { MessageBox.Show("Somthing was wrong"); }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("S999");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G907");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G904");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G906");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G908");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G909");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G905");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("S100");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("S674");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("S500");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G015");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            cboPort.SelectedIndex = 0;
            btnClose.Enabled = false;
         }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G903");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G902");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G901");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G910");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                string x = "#RDPG " + numericUpDown1.Value.ToString();
                serialPort1.Write(x);

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                string x = "#WRBX " + numericUpDown2.Value.ToString();
                serialPort1.Write(x);

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Write Page 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            string s;
            int RawErrorCurrLow_1;
            int RawErrorCurrLow_2;
            int RawErrorCurrHigh_1;
            int RawErrorCurrHigh_2;

            int VerErrorCurrLow_1;
            int VerErrorCurrLow_2;
            int VerErrorCurrHigh_1;
            int VerErrorCurrHigh_2;
            byte StdDevMB1L1;
            byte StdDevMB1L2;
            byte StdDevMB2L1;
            byte StdDevMB2L2;

            int Byte_cnt = 0;
            byte CS = 0;
            try
            {
                RawErrorCurrLow_1 = Convert.ToInt16(textBoxCAL_Low_1.Text); 
                RawErrorCurrLow_2 = Convert.ToInt16(textBoxCAL_Low_2.Text); 
                RawErrorCurrHigh_1 = Convert.ToInt16(textBoxCAL_High_1.Text); 
                RawErrorCurrHigh_2 = Convert.ToInt16(textBoxCAL_High_2.Text); 

                VerErrorCurrLow_1 = Convert.ToInt16(textBoxVER_Low_1.Text); 
                VerErrorCurrLow_2 = Convert.ToInt16(textBoxVER_Low_2.Text); 
                VerErrorCurrHigh_1 = Convert.ToInt16(textBoxVER_High_1.Text); 
                VerErrorCurrHigh_2 = Convert.ToInt16(textBoxVER_High_2.Text);

                StdDevMB1L1 = Convert.ToByte(Convert.ToSingle(textBoxStdDevMB1L1.Text)*10.0);
                StdDevMB1L2 = Convert.ToByte(Convert.ToSingle(textBoxStdDevMB1L2.Text) * 10.0);
                StdDevMB2L1 = Convert.ToByte(Convert.ToSingle(textBoxStdDevMB2L1.Text) * 10.0);
                StdDevMB2L2 = Convert.ToByte(Convert.ToSingle(textBoxStdDevMB2L2.Text) * 10.0);

                CS += ((byte)RawErrorCurrLow_1);         s = ((byte)RawErrorCurrLow_1).ToString("X2");          Byte_cnt++;
                CS += ((byte)(RawErrorCurrLow_1 >> 8)); s += ((byte)(RawErrorCurrLow_1 >> 8)).ToString("X2");   Byte_cnt++;
                CS += ((byte)RawErrorCurrLow_2);        s += ((byte)RawErrorCurrLow_2).ToString("X2");          Byte_cnt++;
                CS += ((byte)(RawErrorCurrLow_2 >> 8)); s += ((byte)(RawErrorCurrLow_2 >> 8)).ToString("X2");   Byte_cnt++;
                CS += ((byte)RawErrorCurrHigh_1);       s += ((byte)RawErrorCurrHigh_1).ToString("X2");         Byte_cnt++;
                CS += ((byte)(RawErrorCurrHigh_1>>8));  s += ((byte)(RawErrorCurrHigh_1 >> 8)).ToString("X2");  Byte_cnt++;
                CS += ((byte)RawErrorCurrHigh_2);       s += ((byte)RawErrorCurrHigh_2).ToString("X2");         Byte_cnt++;
                CS += ((byte)(RawErrorCurrHigh_2>>8));  s += ((byte)(RawErrorCurrHigh_2 >> 8)).ToString("X2");  Byte_cnt++;

                CS += ((byte)VerErrorCurrLow_1);        s += ((byte)VerErrorCurrLow_1).ToString("X2");          Byte_cnt++;
                CS += ((byte)(VerErrorCurrLow_1>>8));   s += ((byte)(VerErrorCurrLow_1 >> 8)).ToString("X2");   Byte_cnt++;
                CS += ((byte)VerErrorCurrLow_2);        s += ((byte)VerErrorCurrLow_2).ToString("X2");          Byte_cnt++;
                CS += ((byte)(VerErrorCurrLow_2>>8));   s += ((byte)(VerErrorCurrLow_2 >> 8)).ToString("X2");   Byte_cnt++;
                CS += ((byte)VerErrorCurrHigh_1);       s += ((byte)VerErrorCurrHigh_1).ToString("X2");         Byte_cnt++;
                CS += ((byte)(VerErrorCurrHigh_1>>8));  s += ((byte)(VerErrorCurrHigh_1 >> 8)).ToString("X2");  Byte_cnt++;
                CS += ((byte)VerErrorCurrHigh_2);       s += ((byte)VerErrorCurrHigh_2).ToString("X2");         Byte_cnt++;
                CS += ((byte)(VerErrorCurrHigh_2>>8));  s += ((byte)(VerErrorCurrHigh_2 >> 8)).ToString("X2");  Byte_cnt++;

                CS += StdDevMB1L1; s += (StdDevMB1L1).ToString("X2"); Byte_cnt++;
                CS += StdDevMB1L2; s += (StdDevMB1L2).ToString("X2"); Byte_cnt++;
                CS += StdDevMB2L1; s += (StdDevMB2L1).ToString("X2"); Byte_cnt++;
                CS += StdDevMB2L2; s += (StdDevMB2L2).ToString("X2"); Byte_cnt++;

                CS ^= 0xFF;
                CS += 1;
                
                s = "#WRBX "+ Constants.BoxCalTolerancePage.ToString()+" "+ CS.ToString("X2") + s; Byte_cnt++;
//                s = "#WRBX 10 "+ CS.ToString("X2") + s; Byte_cnt++;
                //s = "#WRBX 10 " + "0E" + s; Byte_cnt++;
                for (int i = Byte_cnt; i < 32; i++)
                {
                    s += ((byte)(0)).ToString("X2");
                }
                serialPort1.Write(s);

            }
            catch { return; }

        }
        /// <summary>
        /// Read Page 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("#RDBX 10 ");

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("#RDBX 31 ");

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("#RDBX 31 ");

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }
        /// <summary>
        /// Write Page 31
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            string s;
            uint MB1_P_1;
            uint MB1_P_2;
            uint MB2_P_1;
            uint MB2_P_2;
            uint UpolErr;
            byte BoxNr;

            uint TRaw_1;
            uint TRaw_2;
            uint TRaw_3;
            uint Terror;
            uint Byte_cnt = 0;
            byte CS = 0;
            try
            {
                MB1_P_1 = Convert.ToUInt16(textBoxMB1Zero.Text);
                MB1_P_2 = Convert.ToUInt16(textBoxMB1_175nA.Text);
                MB2_P_1 = Convert.ToUInt16(textBoxMB2_175nA.Text);
                MB2_P_2 = Convert.ToUInt16(textBoxMB2_4700nA.Text);

                BoxNr= Convert.ToByte(textBoxNr.Text);
                UpolErr = Convert.ToUInt16(textBoxUpolErr.Text); 

                TRaw_1 = Convert.ToUInt16(textBoxT5Deg.Text);
                TRaw_2 = Convert.ToUInt16(textBoxT25Deg.Text);
                TRaw_3 = Convert.ToUInt16(textBoxT50Deg.Text);
                Terror= Convert.ToUInt16(textBoxTerror.Text);

                // P31 Calibrated RawValues from Box
                CS += ((byte)MB1_P_1); s = ((byte)MB1_P_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB1_P_1 >> 8)); s += ((byte)(MB1_P_1 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB1_P_2); s += ((byte)MB1_P_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB1_P_2 >> 8)); s += ((byte)(MB1_P_2 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB2_P_1); s += ((byte)MB2_P_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB2_P_1 >> 8)); s += ((byte)(MB2_P_1 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB2_P_2); s += ((byte)MB2_P_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB2_P_2 >> 8)); s += ((byte)(MB2_P_2 >> 8)).ToString("X2"); Byte_cnt++;
                // P31 Calibrated RawValues for 25°C
                CS += ((byte)TRaw_2); s += ((byte)TRaw_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TRaw_2 >> 8)); s += ((byte)(TRaw_2 >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)UpolErr); s += ((byte)UpolErr).ToString("X2"); Byte_cnt++;
                CS += ((byte)(UpolErr >> 8)); s += ((byte)(UpolErr >> 8)).ToString("X2"); Byte_cnt++;
                // P31 Box Number
                CS += ((byte)BoxNr); s += ((byte)BoxNr).ToString("X2"); Byte_cnt++;
                // P31 Calibrated RawValues for 4°C
                CS += ((byte)TRaw_1); s += ((byte)TRaw_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TRaw_1 >> 8)); s += ((byte)(TRaw_1 >> 8)).ToString("X2"); Byte_cnt++;
                // P31 Calibrated RawValues for 50°C
                CS += ((byte)TRaw_3); s += ((byte)TRaw_3).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TRaw_3 >> 8)); s += ((byte)(TRaw_3 >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)Terror); s += ((byte)Terror).ToString("X2"); Byte_cnt++;

                CS ^= 0xFF;
                CS += 1;

//                s = "#WRBX " + Constants.BoxCalSollValues.ToString() + " " + CS.ToString("X2") + s; Byte_cnt++;
                s = "#WRBX 31 " + CS.ToString("X2") + s; Byte_cnt++;
                //s = "#WRBX 10 " + "0E" + s; Byte_cnt++;
                for (uint i = Byte_cnt; i < 32; i++)
                {
                    s += ((byte)(0)).ToString("X2");
                }
                serialPort1.Write(s);

            }
            catch { return; }

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G911");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            string s;
            int TOffset;
            int TGain;
            int MB1Offset;
            int MB1Gain;
            int MB2Offset;
            int MB2Gain;

            uint Byte_cnt = 0;
            byte CS = 0;
            try
            {
                TOffset = Convert.ToInt16(textBoxTOff.Text);
                TGain = Convert.ToInt16(textBoxTGain.Text);
                MB1Offset = Convert.ToInt16(textBoxMB1Offset.Text);
                MB1Gain = Convert.ToInt16(textBoxMB1Gain.Text);
                MB2Offset = Convert.ToInt16(textBoxMB2Offset.Text);
                MB2Gain = Convert.ToInt16(textBoxMB2Gain.Text);

                // P31 Calibrated RawValues from Box
                CS += ((byte)TOffset); s = ((byte)TOffset).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TOffset >> 8)); s += ((byte)(TOffset >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)TGain); s += ((byte)TGain).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TGain >> 8)); s += ((byte)(TGain >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)MB1Offset); s += ((byte)MB1Offset).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB1Offset >> 8)); s += ((byte)(MB1Offset >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB1Gain); s += ((byte)MB1Gain).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB1Gain >> 8)); s += ((byte)(MB1Gain >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)MB2Offset); s += ((byte)MB2Offset).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB2Offset >> 8)); s += ((byte)(MB2Offset >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB2Gain); s += ((byte)MB2Gain).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB2Gain >> 8)); s += ((byte)(MB2Gain >> 8)).ToString("X2"); Byte_cnt++;


                CS ^= 0xFF;
                CS += 1;

                s = "#WRPG 14" + " " + CS.ToString("X2") + s; Byte_cnt++;
                //s = "#WRBX 10 " + "0E" + s; Byte_cnt++;
                for (uint i = Byte_cnt; i < 32; i++)
                {
                    s += ((byte)(0)).ToString("X2");
                }
                serialPort1.Write(s);

            }
            catch { MessageBox.Show("Something Wrong");  }

        }

        private void textBoxTOff_TextChanged(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("#RDPG 14 ");

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("S200");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G200");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }



        private void textBoxMB1Offset_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBoxMB2Gain_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("G912");
            }
            catch { MessageBox.Show("Comport ist geschlossen"); }

        }

        private void label_CS10_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("#RDPG 30 ");

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }


        }


private void button30_Click(object sender, EventArgs e)
        {

            string s;
            int TOffset;
            int TGain;
            int MB1Offset;
            int MB1Gain;
            int MB2Offset;
            int MB2Gain;

            uint Byte_cnt = 0;
            byte CS = 0;
            try
            {
                TOffset = Convert.ToInt16(textBoxTOff30.Text);
                TGain = Convert.ToInt16(textBoxTGain30.Text);
                MB1Offset = Convert.ToInt16(textBoxMB1Offset30.Text);
                MB1Gain = Convert.ToInt16(textBoxMB1Gain30.Text);
                MB2Offset = Convert.ToInt16(textBoxMB2Offset30.Text);
                MB2Gain = Convert.ToInt16(textBoxMB2Gain30.Text);

                // P31 Calibrated RawValues from Box
                CS += ((byte)TOffset); s = ((byte)TOffset).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TOffset >> 8)); s += ((byte)(TOffset >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)TGain); s += ((byte)TGain).ToString("X2"); Byte_cnt++;
                CS += ((byte)(TGain >> 8)); s += ((byte)(TGain >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)MB1Offset); s += ((byte)MB1Offset).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB1Offset >> 8)); s += ((byte)(MB1Offset >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB1Gain); s += ((byte)MB1Gain).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB1Gain >> 8)); s += ((byte)(MB1Gain >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)MB2Offset); s += ((byte)MB2Offset).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB2Offset >> 8)); s += ((byte)(MB2Offset >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)MB2Gain); s += ((byte)MB2Gain).ToString("X2"); Byte_cnt++;
                CS += ((byte)(MB2Gain >> 8)); s += ((byte)(MB2Gain >> 8)).ToString("X2"); Byte_cnt++;


                CS ^= 0xFF;
                CS += 1;

                s = "#WRPG 30" + " " + CS.ToString("X2") + s; Byte_cnt++;
                //s = "#WRBX 10 " + "0E" + s; Byte_cnt++;
                for (uint i = Byte_cnt; i < 32; i++)
                {
                    s += ((byte)(0)).ToString("X2");
                }
                serialPort1.Write(s);

            }
            catch { MessageBox.Show("Something Wrong"); }



        }

        private void button31_Click(object sender, EventArgs e)
        {
            float R_T1;
            float R_T2;
            float R_T3;
            R_T1 = Convert.ToSingle(textBoxR_T1.Text);
            R_T2 = Convert.ToSingle(textBoxR_T2.Text);
            R_T3 = Convert.ToSingle(textBoxR_T3.Text);
            R_T1 = 3000 / (243000 + R_T1) * R_T1 * 8;
            R_T2 = 3000 / (243000 + R_T2) * R_T2 * 8;
            R_T3 = 3000 / (243000 + R_T3) * R_T3 * 8;

            textBoxT5Deg.Text = Convert.ToString(R_T1);
            textBoxT25Deg.Text = Convert.ToString(R_T2);
            textBoxT50Deg.Text = Convert.ToString(R_T3);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            float U_T1;
            float U_T2;
            float U_T3;
            U_T1 = Convert.ToSingle(textBoxT5Deg.Text);
            U_T2 = Convert.ToSingle(textBoxT25Deg.Text);
            U_T3 = Convert.ToSingle(textBoxT50Deg.Text);

            U_T1 /= (float)8.0;
            U_T2 /= (float)8.0;
            U_T3 /= (float)8.0;

            U_T1 = U_T1 * 243000 / (3000 - U_T1);
            U_T2 = U_T2 * 243000 / (3000 - U_T2);
            U_T3 = U_T3 * 243000 / (3000 - U_T3);

            label_R_T1.Text = Convert.ToString(U_T1);
            label_R_T2.Text = Convert.ToString(U_T2);
            label_R_T3.Text = Convert.ToString(U_T3);
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            try
            {
                string x = "#RDBX " + numericUpDown2.Value.ToString();
                serialPort1.Write(x);

            }
            catch { MessageBox.Show("Comport ist geschlossen"); }
        

        }
    }
}
