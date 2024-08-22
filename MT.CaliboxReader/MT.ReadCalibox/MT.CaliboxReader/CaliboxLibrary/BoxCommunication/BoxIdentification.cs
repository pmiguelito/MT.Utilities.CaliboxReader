using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CaliboxLibrary
{
    public class BoxIdentification
    {
        public int ChannelNo { get; set; }
        /**********************************************************
        * FUNCTION:     Constructor(s)
        * DESCRIPTION:
        ***********************************************************/

        public BoxIdentification()
        {

        }

        public BoxIdentification(int channelNo)
        {
            ChannelNo = channelNo;
        }

        /**********************************************************
        * FUNCTION:     Boxvalues for all channels
        * DESCRIPTION:
        ***********************************************************/
        public static Dictionary<int, BoxIdentification> ChannelBoxIdentification { get; set; }
        = new Dictionary<int, BoxIdentification>();

        public static BoxIdentification GetOrAdd(int channelNo)
        {
            if (ChannelBoxIdentification.TryGetValue(channelNo, out BoxIdentification result))
            {
                return result;
            }
            result = new BoxIdentification(channelNo);
            Add(channelNo, result);
            return result;
        }

        public static void Add(int channelNo, BoxIdentification boxID)
        {
            if (ChannelBoxIdentification.ContainsKey(channelNo))
            {
                ChannelBoxIdentification[channelNo] = boxID;
                return;
            }
            ChannelBoxIdentification.Add(channelNo, boxID);
        }

        /**********************************************************
        * FUNCTION:     BoxLimits
        * DESCRIPTION:
        ***********************************************************/
        public DeviceLimits DeviceLimits = new DeviceLimits();
        public bool CS_ok { get { return DeviceLimits.CS_ok; } }

        #region BoxReset
        /**********************************************************
        * FUNCTION:     BoxReset S999
        * DESCRIPTION:
        ***********************************************************/
        public const string txt_BoxReset = "BoxReset";
        public const string txt_Messbereich = "Messbereich";
        public const string txt_DipSwicht = "DipSwitch";
        public const string txt_RawVal = "RawVal";
        public const string txt_Current = "Current";
        public const string txt_CalError = "CalError";
        public const string txt_RawError = "ErrorRaw";
        public const string txt_WepError = "WepError";
        public const string txt_StdDev = "StdDev";
        public const string txt_CS10Calc = "CS10Calc";
        public const string txt_CS31Calc = "CS31Calc";
        public const string txt_Betriebsmittel = "Betriebsmittel";

        /// <summary>
        /// Load DeviceLimits
        /// </summary>
        /// <param name="resp"></param>
        public void Get_Values(DeviceResponseValues resp)
        {
            if (resp.Response.StartsWith(txt_BoxReset)) { return; }
            if (resp.Response.StartsWith(txt_Messbereich)) { return; } //HEADER
            GetValues(resp.Response);
        }
        public void GetValues(string value)
        {
            string txt = value.Trim();
            if (txt.Length == 0) { return; }
            if (DeviceLimits == null) { DeviceLimits = new DeviceLimits(); }

            string[] split = value.Split('\t');
            if (split.Length < 1) { return; }
            if (BoxIdentification_BeM(txt, split)) { return; }
            if (BoxIdentification_DipSwitch(txt)) { return; }
            if (BoxIdentification_CalibMode(txt)) { return; }
            if (BoxIdentification_FW(txt, split)) { return; }
            if (BoxIdentification_CS10(txt, split)) { return; }
            if (BoxIdentification_CS31(txt, split)) { return; }

            if (BoxIdentification_RawVal(txt, split)) { return; }
            if (BoxIdentification_Current(txt, split)) { return; }
            if (BoxIdentification_RawError(txt, split)) { return; }
            if (BoxIdentification_CalError(txt, split)) { return; }
            if (BoxIdentification_WepError(txt, split)) { return; }
            if (BoxIdentification_StdDev(txt, split)) { return; }
        }

        private bool BoxIdentification_BeM(string txt, string[] values)
        {
            if (txt.StartsWith(txt_Betriebsmittel))
            {
                if (values.Length > 3)
                {
                    string bem = values[0];
                    string hwid = values[2];
                    string hwversion = values[3];
                    DeviceLimits.BeM = bem.Substring(bem.IndexOf(':') + 1).Trim();
                    DeviceLimits.HW_ID = hwid.Substring(hwid.IndexOf(':') + 1).Trim();
                    DeviceLimits.HW_Version = hwversion.Substring(hwversion.IndexOf(':') + 1).Trim();
                    DeviceLimits.BoxIdentification_State.Betriebsmittel = true;
                }
                return true;
            }
            return false;
        }

        private bool BoxIdentification_DipSwitch(string txt)
        {
            if (txt.Contains(txt_DipSwicht))
            {
                DeviceLimits.DipSwitch = txt.Replace("*", "").Trim();
                DeviceLimits.BoxIdentification_State.DipSwitch = true;
                return true;
            }
            return false;
        }
        private bool BoxIdentification_CalibMode(string txt)
        {
            if (txt.StartsWith("CalibrationBox"))
            {
                DeviceLimits.BoxDesc = txt.Trim();
                DeviceLimits.BoxIdentification_State.CalibrationBox = true;
                return true;
            }
            return false;
        }
        private bool BoxIdentification_FW(string txt, string[] values)
        {
            if (txt.StartsWith("FW:"))
            {
                if (values.Length > 2)
                {
                    string fw = values[0];
                    string cp = values[2];
                    DeviceLimits.FW_Version = fw.Substring(fw.IndexOf(':') + 1).Trim();
                    DeviceLimits.Compiled = cp.Substring(cp.IndexOf(':') + 1).Trim();
                    DeviceLimits.BoxIdentification_State.FW = true;
                }
                return true;
            }
            return false;
        }

        private bool BoxIdentification_CS10(string txt, string[] values)
        {
            if (txt.StartsWith(txt_CS10Calc))
            {
                if (values.Length > 1)
                {
                    string v1 = values[0];
                    string v2 = values[1];
                    DeviceLimits.CS10calc = v1.Substring(v1.IndexOf(':') + 1).Trim();
                    DeviceLimits.CS10EEprom = v2.Substring(v2.IndexOf(':') + 1).Trim();
                }
                return true;
            }
            return false;
        }
        private bool BoxIdentification_CS31(string txt, string[] values)
        {
            if (txt.StartsWith(txt_CS31Calc))
            {
                if (values.Length > 1)
                {
                    string v1 = values[0];
                    string v2 = values[1];
                    DeviceLimits.CS31calc = v1.Substring(v1.IndexOf(':') + 1).Trim();
                    DeviceLimits.CS31EEprom = v2.Substring(v2.IndexOf(':') + 1).Trim();
                }
                return true;
            }
            return false;
        }

        /**************************************************************************************
         * Measurement Values
         **************************************************************************************/
        private bool Get_MeasMode(string[] values, out DeviceLimitsModes mode)
        {
            mode = new DeviceLimitsModes();
            if (values.Length > 5)
            {
                mode.Low1 = values[1].ToDouble();
                mode.Low2 = values[2].ToDouble();
                mode.High1 = values[3].ToDouble();
                mode.High2 = values[4].ToDouble();
                mode.Unit = values[5].Trim();
                return true;
            }
            return false;
        }
        private bool BoxIdentification_RawVal(string txt, string[] values)
        {
            if (txt.StartsWith(txt_RawVal))
            {
                if (Get_MeasMode(values, out DeviceLimitsModes mode))
                {
                    DeviceLimits.RawVal = mode;
                    if (values.Length > 7)
                    {
                        try
                        {
                            DeviceLimits.TempRefVolt = values[6].ToDouble();
                            DeviceLimits.TempRefVolt2 = values[7].ToDouble();
                            DeviceLimits.TempRefVolt3 = values[8].ToDouble();
                        }
                        catch { }
                    }
                    if (values.Length > 10)
                    {
                        DeviceLimits.TempErr = values[9].ToDouble();
                        DeviceLimits.UpolError = values[10].ToDouble();
                    }
                    DeviceLimits.BoxIdentification_State.RawVal = true;
                    return true;
                }
            }
            return false;
        }

        private bool BoxIdentification_Current(string txt, string[] values)
        {
            if (txt.StartsWith(txt_Current))
            {
                if (Get_MeasMode(values, out DeviceLimitsModes mode))
                {
                    DeviceLimits.Current = mode;
                    DeviceLimits.BoxIdentification_State.Current = true;
                    return true;
                }
            }
            return false;
        }
        private bool BoxIdentification_RawError(string txt, string[] values)
        {
            if (txt.StartsWith(txt_RawError))
            {
                if (Get_MeasMode(values, out DeviceLimitsModes mode))
                {
                    DeviceLimits.RawErrorCalibration = mode;
                    DeviceLimits.BoxIdentification_State.RawError = true;
                    return true;
                }
            }
            return false;
        }
        private bool BoxIdentification_CalError(string txt, string[] values)
        {
            if (txt.StartsWith(txt_CalError))
            {
                if (Get_MeasMode(values, out DeviceLimitsModes mode))
                {
                    DeviceLimits.CalError = mode;
                    DeviceLimits.BoxIdentification_State.CalError = true;
                    return true;
                }
            }
            return false;
        }
        private bool BoxIdentification_WepError(string txt, string[] values)
        {
            if (txt.StartsWith(txt_RawError))
            {
                if (Get_MeasMode(values, out DeviceLimitsModes mode))
                {
                    DeviceLimits.WepError = mode;
                    DeviceLimits.BoxIdentification_State.WepError = true;
                    return true;
                }
            }
            return false;
        }
        private bool BoxIdentification_StdDev(string txt, string[] values)
        {
            if (txt.StartsWith(txt_StdDev))
            {
                if (Get_MeasMode(values, out DeviceLimitsModes mode))
                {
                    DeviceLimits.StdDev = mode;
                    DeviceLimits.BoxIdentification_State.StdDev = true;
                    return true;
                }
            }
            return false;
        }
        #endregion S999

        #region Read Box Values
        /**********************************************************
        * FUNCTION:     Read Box Values
        * DESCRIPTION:
        ***********************************************************/
        public const float FactorLow = 0.004f;
        public const float FactorHigh = 0.2f;
        public void RDBX(string values)
        {
            if (string.IsNullOrEmpty(values)) { return; }
            string[] B = values.Split(' ');
            if (B.Length < 3) { return; }
            string opcode = B[0];
            string pagetxt = B[1];
            string data = B[2];
            int page = Convert.ToInt16(pagetxt, 16);
            byte[] bytes = HexStringToByte(data);
            //int i = 1;
            try
            {
                switch (page)
                {
                    case 10:
                        ParseBoxPage10(bytes);
                        break;
                    case 31:
                        ParseBoxPage31(bytes);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something was wrong{Environment.NewLine}{ex.Message}");
            }
        }
        private void ParseBoxPage10(byte[] bytes)
        {
            DeviceLimits.RawErrorCalibration.Low1 = GetByteValue(1, bytes, FactorLow);
            DeviceLimits.RawErrorCalibration.Low2 = GetByteValue(3, bytes, FactorLow);
            DeviceLimits.RawErrorCalibration.High1 = GetByteValue(5, bytes, FactorHigh);
            DeviceLimits.RawErrorCalibration.High2 = GetByteValue(7, bytes, FactorHigh);

            DeviceLimits.RawErrorVerification.Low1 = GetByteValue(9, bytes, FactorLow * 100);
            DeviceLimits.RawErrorVerification.Low2 = GetByteValue(11, bytes, FactorLow * 100);
            DeviceLimits.RawErrorVerification.High1 = GetByteValue(13, bytes, FactorHigh * 100);
            DeviceLimits.RawErrorVerification.High2 = GetByteValue(15, bytes, FactorHigh * 100);

            DeviceLimits.StdDev.Low1 = (bytes[17] / 10.0);
            DeviceLimits.StdDev.Low2 = (bytes[18] / 10.0);
            DeviceLimits.StdDev.High1 = (bytes[19] / 10.0);
            DeviceLimits.StdDev.High2 = (bytes[20] / 10.0);

            DeviceLimits.CS10calc = GetChecksum(bytes).ToString();
        }
        private void ParseBoxPage31(byte[] bytes)
        {
            #region Page31
            //i = 1;
            /*MB1 0nA*/
            DeviceLimits.Current.Low1 = 0;
            DeviceLimits.RawVal.Low1 = GetByteValue(1, bytes);

            /*MB1 175nA*/
            var value = GetByteValue(3, bytes);
            DeviceLimits.Current.Low2 = ((value - 12480) * FactorLow);
            DeviceLimits.RawVal.Low2 = value;

            /*MB2 175nA*/
            value = GetByteValue(5, bytes);
            DeviceLimits.Current.High1 = (value - 12480) * FactorHigh;
            DeviceLimits.RawVal.High1 = value;

            /*MB2 4700nA*/
            value = GetByteValue(7, bytes);
            DeviceLimits.Current.High2 = (value - 12480) * FactorHigh;
            DeviceLimits.RawVal.High2 = value;

            /*Temperature NTC 25°C*/
            double helper = GetHelperLog(9, bytes);
            DeviceLimits.TempRefVolt2Temp = (float)helper;
            DeviceLimits.TempRefVolt2 = GetByteValue(9, bytes);

            /*U Polarization*/
            helper = (GetByteValue(11, bytes));
            helper /= 10.0;
            DeviceLimits.UpolErrorValue = helper;
            DeviceLimits.UpolError = GetByteValue(11, bytes);

            /*Box Number*/
            DeviceLimits.BeM_BoxNr = Convert.ToString(bytes[13]);

            /*Temperature Min*/
            helper = GetHelperLog(14, bytes);
            DeviceLimits.TempRefVoltTemp = (float)helper;
            DeviceLimits.TempRefVolt = (GetByteValue(14, bytes));

            /*Temperature Max*/
            helper = GetHelperLog(16, bytes);
            DeviceLimits.TempRefVolt3Temp = ((float)helper);
            DeviceLimits.TempRefVolt3 = (GetByteValue(16, bytes));

            /*Temperature Tolerance*/
            helper = bytes[18] / 100.0;
            DeviceLimits.TempErrTemp = helper;
            DeviceLimits.TempErr = bytes[18];

            DeviceLimits.CS31calc = GetChecksum(bytes).ToString();
            #endregion
        }

        private byte[] HexStringToByte(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int j = 0; j < NumberChars - 2; j += 2)
            {
                bytes[j / 2] = Convert.ToByte(hex.Substring(j, 2), 16);
            }
            return bytes;
        }

        private int GetByteValue(int i, byte[] bytes)
        {
            return bytes[i + 1] * 256 + bytes[i];
        }

        private double GetByteValue(int i, byte[] bytes, float factor)
        {
            return (GetByteValue(i, bytes) * factor);
        }

        private double GetHelperLog(int i, byte[] bytes)
        {
            double helper = (GetByteValue(i, bytes)) / 8.0;
            helper = helper * 243000 / (3000 - helper);
            helper = ((3740 * 298.15) / (3740 + Math.Log(helper / 22000) * 298.15)) - 273.15;
            return helper;
        }

        /// <summary>
        /// mod 2^16
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private uint GetChecksum(byte[] bytes)
        {
            uint checksum = 0;
            for (int x = 0; x < 32; x++)
            {
                checksum = (checksum >> 1) + ((checksum & (uint)1) << 15);
                checksum += bytes[x];
                checksum &= 0xffff;       /* Keep it within bounds. */
            }
            return checksum;
        }
        #endregion

        #region Write Box Values
        /**********************************************************
        * FUNCTION:     Write Box Values
        * DESCRIPTION:
        ***********************************************************/
        /// <summary>
        /// Write values to CaliBox
        /// </summary>
        /// <param name="value">Page Number</param>
        /// <returns></returns>
        public string WRBX(string value)
        {

            switch (value)
            {
                case "10":
                    return WriteBoxPage10();
                case "31":
                    return WriteBoxPage31();
            }
            return "";
        }

        public string WriteBoxPage10()
        {
            string s = "";
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
                RawErrorCurrLow_1 = Convert.ToInt16(DeviceLimits.RawErrorCalibration.Low1);
                RawErrorCurrLow_2 = Convert.ToInt16(DeviceLimits.RawErrorCalibration.Low2);
                RawErrorCurrHigh_1 = Convert.ToInt16(DeviceLimits.RawErrorCalibration.High1);
                RawErrorCurrHigh_2 = Convert.ToInt16(DeviceLimits.RawErrorCalibration.High2);

                VerErrorCurrLow_1 = Convert.ToInt16(DeviceLimits.RawErrorVerification.Low1);
                VerErrorCurrLow_2 = Convert.ToInt16(DeviceLimits.RawErrorVerification.Low2);
                VerErrorCurrHigh_1 = Convert.ToInt16(DeviceLimits.RawErrorVerification.High1);
                VerErrorCurrHigh_2 = Convert.ToInt16(DeviceLimits.RawErrorVerification.High2);

                StdDevMB1L1 = Convert.ToByte(Convert.ToSingle(DeviceLimits.StdDev.Low1) * 10.0);
                StdDevMB1L2 = Convert.ToByte(Convert.ToSingle(DeviceLimits.StdDev.Low2) * 10.0);
                StdDevMB2L1 = Convert.ToByte(Convert.ToSingle(DeviceLimits.StdDev.High1) * 10.0);
                StdDevMB2L2 = Convert.ToByte(Convert.ToSingle(DeviceLimits.StdDev.High2) * 10.0);

                CS += ((byte)RawErrorCurrLow_1); s = ((byte)RawErrorCurrLow_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(RawErrorCurrLow_1 >> 8)); s += ((byte)(RawErrorCurrLow_1 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)RawErrorCurrLow_2); s += ((byte)RawErrorCurrLow_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(RawErrorCurrLow_2 >> 8)); s += ((byte)(RawErrorCurrLow_2 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)RawErrorCurrHigh_1); s += ((byte)RawErrorCurrHigh_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(RawErrorCurrHigh_1 >> 8)); s += ((byte)(RawErrorCurrHigh_1 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)RawErrorCurrHigh_2); s += ((byte)RawErrorCurrHigh_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(RawErrorCurrHigh_2 >> 8)); s += ((byte)(RawErrorCurrHigh_2 >> 8)).ToString("X2"); Byte_cnt++;

                CS += ((byte)VerErrorCurrLow_1); s += ((byte)VerErrorCurrLow_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(VerErrorCurrLow_1 >> 8)); s += ((byte)(VerErrorCurrLow_1 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)VerErrorCurrLow_2); s += ((byte)VerErrorCurrLow_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(VerErrorCurrLow_2 >> 8)); s += ((byte)(VerErrorCurrLow_2 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)VerErrorCurrHigh_1); s += ((byte)VerErrorCurrHigh_1).ToString("X2"); Byte_cnt++;
                CS += ((byte)(VerErrorCurrHigh_1 >> 8)); s += ((byte)(VerErrorCurrHigh_1 >> 8)).ToString("X2"); Byte_cnt++;
                CS += ((byte)VerErrorCurrHigh_2); s += ((byte)VerErrorCurrHigh_2).ToString("X2"); Byte_cnt++;
                CS += ((byte)(VerErrorCurrHigh_2 >> 8)); s += ((byte)(VerErrorCurrHigh_2 >> 8)).ToString("X2"); Byte_cnt++;

                CS += StdDevMB1L1; s += (StdDevMB1L1).ToString("X2"); Byte_cnt++;
                CS += StdDevMB1L2; s += (StdDevMB1L2).ToString("X2"); Byte_cnt++;
                CS += StdDevMB2L1; s += (StdDevMB2L1).ToString("X2"); Byte_cnt++;
                CS += StdDevMB2L2; s += (StdDevMB2L2).ToString("X2"); Byte_cnt++;

                CS ^= 0xFF;
                CS += 1;

                s = $"#WRBX 10  {CS:X2} {s}"; Byte_cnt++;
                for (int i = Byte_cnt; i < 32; i++)
                {
                    s += ((byte)(0)).ToString("X2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Convert Page 10 Error{Environment.NewLine}{ex.Message}");
            }
            return s;
        }

        private string WriteBoxPage31()
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
                var dl = DeviceLimits;
                MB1_P_1 = Convert.ToUInt16(dl.RawVal.Low1);
                MB1_P_2 = Convert.ToUInt16(dl.RawVal.Low2);
                MB2_P_1 = Convert.ToUInt16(dl.RawVal.High1);
                MB2_P_2 = Convert.ToUInt16(dl.RawVal.High2);

                BoxNr = Convert.ToByte(dl.BeM_BoxNr);
                UpolErr = Convert.ToUInt16(dl.UpolError);

                TRaw_1 = Convert.ToUInt16(dl.TempRefVolt);
                TRaw_2 = Convert.ToUInt16(dl.TempRefVolt2);
                TRaw_3 = Convert.ToUInt16(dl.TempRefVolt3);
                Terror = Convert.ToUInt16(dl.TempErr);

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

                // s = "#WRBX " + Constants.BoxCalSollValues.ToString() + " " + CS.ToString("X2") + s; Byte_cnt++;
                s = "#WRBX 31 " + CS.ToString("X2") + s; Byte_cnt++;
                //s = "#WRBX 10 " + "0E" + s; Byte_cnt++;
                for (uint i = Byte_cnt; i < 32; i++)
                {
                    s += ((byte)(0)).ToString("X2");
                }
                return s;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Convert Page 31 Error{Environment.NewLine}{ex.Message}");
            }
            return "";

        }
        #endregion
    }
}
