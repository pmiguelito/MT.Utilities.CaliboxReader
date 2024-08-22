using OneWire_DataConverter;
using static OneWire_DataConverter.OneWire_TDLdatatype;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ReadCalibox.clGlobals;
using static ReadCalibox.clDatenBase;
using static STDhelper.clSTD;
using System.Data;

namespace ReadCalibox
{
    public class clDeviceCom
    {
        private clLog log = new clLog();
        public static Dictionary<string, string> BoxMode = new Dictionary<string, string>
        {
            {"00","CalibMode_674mV_Low_1"},
            {"01","CalibMode_674mV_Low_2"},
            {"02","CalibMode_674mV_High_1"},
            {"03","CalibMode_674mV_High_2"},
            {"04","CalibMode_500mV_Low_1"},
            {"05","CalibMode_500mV_Low_2"},
            {"06","CalibMode_500mV_High_1"},
            {"07","CalibMode_500mV_High_2"},
            {"08","VerifyMode_674mV_Low_1"},
            {"09","VerifyMode_674mV_Low_2"},
            {"0A","VerifyMode_674mV_High_1"},
            {"0B","VerifyMode_674mV_High_2"},
            {"0C","VerifyMode_500mV_Low_1"},
            {"0D","VerifyMode_500mV_Low_2"},
            {"0E","VerifyMode_500mV_High_1"},
            {"0F","VerifyMode_500mV_High_2"},
            {"10","VerifyTemp"},
            {"33","CalibMode_674CalculationLow"},
            {"34","CalibMode_674CalculationHigh"},
            {"35","CalibMode_500CalculationLow"},
            {"36","CalibMode_500CalculationHigh"},
            {"37","SuccessfullSensorCalibration"},
            {"38","Box_SensorCheckUpol_500"},
            {"39","ShowErrorValues"},
            {"32","Box_Idle"},
            {"11","Box_WritePage_00"},
            {"12","Box_WritePage_01"},
            {"13","Box_WritePage_12"},
            {"14","Box_WritePage_15"},
            {"15","Box_SensorCheckUpol_674"},
            {"16","Box_SensorVerification"},
            {"17","Box_SensorError"},
            {"18","Box_SensorWriteCalData674"},
            {"19","Box_SensorWriteCalData500"},
            {"1A","Box_StartSensorCalibration"},
            {"1B","SensorFail"},
            {"1C","SensorCalibFinalise"},
            {"1D","Box_Calibration"},
            {"1E","WEP_Test"},
            {"1F","WEP_674mV_Low_1"},
            {"20","WEP_674mV_Low_2"},
            {"21","WEP_500mV_Low_1"},
            {"22","WEP_500mV_Low_2"},
            {"23","WEP_674mV_High_1"},
            {"24","WEP_674mV_High_2"},
            {"25","WEP_500mV_High_1"},
            {"26","WEP_500mV_High_2"},
            {"27","WEP_SensorError"},
            {"28","WEPSensorFail"},
            {"29","SensorWepFinalise"},
            {"2A","WEP_SensorCheckUpol"},
            {"2B","WEP_TempCheck"},

            {"S100","CalMode 674/500mV"},
            {"S500","CalMode 500mV"},
            {"S674","CalMode 674mV"},
            {"FWversion","FWversion"}
        };
        
        public static Dictionary<string, string> BoxErrorCode = new Dictionary<string, string>
        {
            {"0","NotChecked" },
            {"1","ERROR" },
            {"2","PASS" },
            {"00","NoError" },
            {"01","Standard Deviation was out of range (Noisy Signal)" },
            {"02","Calculated Mean was out of range (Offset Error)" },
            {"03","Standard Deviation & Calculated Mean were out of range" },
        };
        public static Dictionary<string, string> CalibrationStatus = new Dictionary<string, string>
        {
            {"00","674mV and 500mV" },
            {"01","647mV" },
            {"02","500mV" }
        };
        public enum opcode
        {
            state, Error, error,
            G015, G100, G200, G901, G902, G903, G904,G905,G906,
            g015, g100, g200, g901, g902, g903, g904,g905,g906,
            S100, S200, S500, S674, S999,
            s100, s200, s500, s674, s999,
            S901,
            s901
        }

        /****************************************************************************************************
         * SerialPort:  Write
         ***************************************************************************************************/
        public void SendCMD(SerialPort port, opcode cmd, string parameter = null)
        {
            if (!port.IsOpen) { port.Open(); }
            port.Write(cmd.ToString());
        }


        /****************************************************************************************************
         * Command:  Write
         ***************************************************************************************************/

        public bool SendCMD(UC_Channel ucCH, opcode cmd)
        {
            SerialPort port = ucCH.ucCOM.Serialport;
            switch (cmd)
            {
                case opcode.G901:
                    break;
                case opcode.S901:
                    SendCMD(port, opcode.G901);
                    break;
                case opcode.S999:
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    SendCMD(port, cmd);
                    break;
                default:
                    SendCMD(port, cmd);
                    break;
            }
            int readRepeat = 5;
            int readDelay = ucCH.ucCOM.ReadDelay_int; 
            bool stopbyNewLine = ucCH.ucCOM.ReadLine;
            bool readLine = ucCH.ucCOM.ReadLine;
            int wait = 150;
            switch (cmd)
            {
                case opcode.G015:
                    //Thread.Sleep(1000);
                    readLine = false;
                    stopbyNewLine = true;
                    //wait = 0;
                    //wait = 1000;
                    readRepeat = 20;
                    break;
                case opcode.G901:
                    wait = 0;
                    readRepeat = 3;
                    readDelay = 20;
                    stopbyNewLine = true;
                    readLine = false;
                    break;
                case opcode.S100:
                case opcode.S500:
                case opcode.S674:
                case opcode.S901:
                    log.Save(ucCH, "CMD send", cmd);
                    return true;
                case opcode.S999:
                    wait = 500;
                    readRepeat = 10;
                    stopbyNewLine = false;
                    break;
                default:
                    break;
            }
            Thread.Sleep(wait);
            return Read(ucCH, cmd, readRepeat, readDelay, stopbyNewLine, readLine);
        }

        /****************************************************************************************************
         * Command:  Read
         ***************************************************************************************************/
        private bool Read(UC_Channel ucCH, opcode cmd, int readRepeat = 5, int readDelay = 25, bool stopbyNewLine = true, bool readLine = false)
        {
            if (Read_SerialPort(ucCH.ucCOM, out string responseTXT, readRepeat, readDelay, stopbyNewLine))
            {
                try
                {
                    if(cmd == opcode.S999) { responseTXT = responseTXT.Replace("*", ""); }
                    ucCH.SampleResponse.OpCode_Request = cmd.ToString();
                    ucCH.SampleResponse.Response = responseTXT;
                }
                catch (Exception e)
                {
                    try { ucCH.SampleResponse.Response = responseTXT; } catch { }
                    STDhelper.clLogging.ErrorHandler("READ", exception: e, message: responseTXT);
                }
                log.Save(ucCH, ucCH.SampleResponse);
                return !ucCH.SampleResponse.Response_Empty;
            }
            else { return false; }
        }

        /****************************************************************************************************
         * SerialPort:  Read
         ***************************************************************************************************/
        private bool Read_SerialPort(UC_COM ucCOM, out string response, int readRepeat = 5, int readDelay = 25, bool stopbyNewLine = true, bool readLine = false)
        {
            response = "";
            SerialPort port = ucCOM.Serialport;
            bool closed = false;
            bool NoError = true;
            bool stopbyNL = stopbyNewLine;
            //int readDelay = delay100ms? 100: ucCOM.ReadDelay_int;
            int count = 0;
            DateTime start = DateTime.Now;
            while (!closed && count < readRepeat && (DateTime.Now - start).TotalMilliseconds < 1000)
            {
                if (port.IsOpen)
                {
                    try
                    {
                        //int dataLength = 0;
                        byte[] data = new byte[0];
                        if (readLine)
                        {
                            response += port.ReadLine();
                            closed = true;
                        }
                        else
                        {
                            if (port.BytesToRead > 0)
                            {
                                string resp = port.ReadExisting();
                                response += resp;
                                if (resp.Contains("\r\n") && stopbyNL)
                                { break; }
                                if ((DateTime.Now - start).TotalMilliseconds < 300)
                                { count = 0; }
                            }
                        }
                        Thread.Sleep(readDelay);
                    }
                    catch (Exception e)
                    {
                        if (!ucCOM.UCChannel.Error_Detected)
                        {
                            ucCOM.ErrorMessageChannel = "ERROR: " + e.Message;
                            NoError = false;
                        }
                        closed = true;

                    }
                }
                else
                {
                    if (!ucCOM.UCChannel.Error_Detected)
                    {
                        ucCOM.ErrorMessageChannel = "ERROR: Comport is closed";
                        NoError = false;
                    }
                    closed = true;
                }
                count++;
            }
            if (string.IsNullOrEmpty(response))
            { return false; }
            else
            { return NoError; }
        }

        /****************************************************************************************************
         * Command:  Read Page15 FW version And Parse
         ***************************************************************************************************/
        private TDLproperty Page15_FWversion;

        public bool Get_Sample_FWversion(string odbc, string page15HEX, out string fwversion)
        {
            fwversion = "";
            bool error = false;
            if(Page15_FWversion == null) { error = !Get_TDL_FWversion(odbc, out Page15_FWversion); }
            if(Page15_FWversion.data_type == null && !error) { error = !Get_TDL_FWversion(odbc, out Page15_FWversion); }
            if (Page15_FWversion.data_type != null && !error)
            {
                try
                {
                    fwversion = page15HEX.GetValuesFromTEDS_HEX(Page15_FWversion.data_type, Page15_FWversion.start, Page15_FWversion.tol, Page15_FWversion.bit, Page15_FWversion.bit_start);
                }
                catch { }
                return !string.IsNullOrEmpty(fwversion);
            }
            return false;
        }

        public bool Get_Sample_FWversion(UC_Channel ucCH, out string fwversion)
        {
            fwversion = "";
            if(SendCMD(ucCH, opcode.G015))
            {
                return Get_Sample_FWversion(ucCH.ODBC_EK, ucCH.SampleResponse.BoxMeasValue, out fwversion);
            }
            return false;
        }

    }

    public class DeviceResponse
    {
        public DeviceResponse(string opcodeRequest, string response = null)
        {
            OpCode_Request = opcodeRequest;
            if (response != null)
            {
                Response = response;
            }
        }
        public DeviceResponse(clDeviceCom.opcode opcodeRequest, string response = null)
        {
            OpCode_Request = opcodeRequest.ToString();
            if (response != null)
            {
                Response = response;
            }
        }


        public DateTime meas_time_start { get; private set; }
        public double TimeDiff { get { if (meas_time_start.Year > 1) { return (DateTime.Now - meas_time_start).TotalMilliseconds; } return 0; } }
        private string _OpCode_Request;
        public string OpCode_Request
        {
            get { return _OpCode_Request; }
            set { _OpCode_Request = value; OpCode_Response = value; }
        }
        private string _Response;
        public string Response
        {
            get { return _Response; }
            set
            {
                _Response = value;
                if (!string.IsNullOrEmpty(value))
                { ResponseParser(); }
            }
        }
        public string ResponseParsed { get; set; }
        public string ResponseParsedLog { get; set; }
        public bool Response_Empty { get { return Response == null ? true : false; } }
        public string OpCode_Response { get; set; }
        private string _BoxMode_hex;
        public string BoxMode_hex
        {
            get
            {
                if (!string.IsNullOrEmpty(_BoxMode_hex)) { int i = ResponseListCount; }
                return _BoxMode_hex;
            }
            set { _BoxMode_hex = value; }
        }
        public bool BoxMode_Empty { get { return string.IsNullOrEmpty(BoxMode_hex); } }
        public string BoxMode_desc { get { return Get_BoxMode_Desc(); } }
        public string BoxErrorCode_hex { get; set; }
        public bool BoxErrorCode_Empty { get { return string.IsNullOrEmpty(BoxErrorCode_hex); } }
        public string BoxErrorCode_desc { get { return Get_BoxErrorCode_Desc(); } }
        public string BoxCalStatus_hex { get; set; }
        public bool BoxCalStatus_Empty { get { return string.IsNullOrEmpty(BoxCalStatus_hex); } }
        public string BoxCalStatus_desc { get { return Get_BoxCalStatus_Desc(); } }
        public string BoxMeasValue { get; set; }
        public bool TestError { get; private set; }
        public bool TestFinalise { get; private set; }

        public string Get_BoxMode_Desc()
        {
            if (!BoxMode_Empty)
            { try { return clDeviceCom.BoxMode[BoxMode_hex]; } catch {  } }
            return "";
        }
        public string Get_BoxErrorCode_Desc()
        {
            if (!BoxErrorCode_Empty)
            { try { return clDeviceCom.BoxErrorCode[BoxErrorCode_hex]; } catch { } }
            return "";
        }
        public string Get_BoxCalStatus_Desc()
        {
            if (!BoxCalStatus_Empty)
            { try { return clDeviceCom.CalibrationStatus[BoxCalStatus_hex]; } catch { } }
            return "";
        }

        public int ResponseListCount { get { return (ResponseList != null) ? ResponseList.Count : 0; } }
        public List<DeviceResponseValues> _ResponseList;
        public List<DeviceResponseValues> ResponseList
        {
            get
            {
                if(_ResponseList == null && Response != null)
                { ResponseParser(); }
                return _ResponseList;
            }
            set { _ResponseList = value; }
        }

        /****************************************************************************************************
         * Response:    
         ***************************************************************************************************/
        public string[] Lines { get; private set; }
        public int LinesCount { get { return (Lines != null) ? Lines.Length : 0; } }

        public void ResponseParser()
        {
            if (!Response_Empty)
            {
                Split_Line(); 
            }
        }

        private bool Split_Line()
        {
            Lines = Response.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            if (LinesCount > 0)
            {
                _ResponseList = new List<DeviceResponseValues>();
                foreach (string line in Lines)
                {
                    string l = line.Trim();
                    if (!string.IsNullOrEmpty(l) && l != "\0")
                    {
                        DeviceResponseValues resp = new DeviceResponseValues(OpCode_Request, l);
                        _ResponseList.Add(resp);
                        ToMain(resp);
                    }
                }
                Insert_DT_Measurement();
            }
            return true;
        }
        private void ToMain(DeviceResponseValues resp)
        {
            if (resp.Error) { return; }
            if (OpCode_Response == "S999") { return; }
            OpCode_Response = resp.OpCode;

            if (!resp.BoxMode_Empty)
            {
                if (!TestError)
                {
                    BoxMode_hex = resp.BoxMode_hex;
                    TestError = resp.TestError;
                    TestFinalise = resp.TestFinalise;
                }
                
            }
            else
            {
                resp.BoxMode_hex = BoxMode_hex;
            }
            if (!resp.BoxCalStatus_Empty)
            { BoxCalStatus_hex = resp.BoxCalStatus_hex; }
            else
            { resp.BoxCalStatus_hex = BoxCalStatus_hex; }

            if (resp.BoxMeasValue != null)
            { BoxMeasValue = resp.BoxMeasValue; }
            else
            { resp.BoxMeasValue = BoxMeasValue; }
            if (!resp.BoxErrorCode_Empty)
            {
                BoxErrorCode_hex = resp.BoxErrorCode_hex;
            }
            else { resp.BoxErrorCode_hex = BoxErrorCode_hex; }

        }

        /****************************************************************************************************
         * Measurement:
         ***************************************************************************************************/
        private DataTable _DT_Measurements;
        public DataTable DT_Measurements
        {
            get
            {
                if(_DT_Measurements == null)
                {
                    _DT_Measurements = Init_DT_Measurement();
                }
                return _DT_Measurements;
            }
            private set { _DT_Measurements = value; }
        }

        public DataTable Init_DT_Measurement()
        {
            DataTable dt = new DataTable("Measurement");
            var col = dt.Columns.Add("ID", typeof(long));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 0;
            col.AutoIncrementStep = 1;
            dt.Columns.Add("meas_time_start", typeof(DateTime));
            dt.Columns.Add("boxmode_hex");
            dt.Columns.Add("boxmode_desc");
            dt.Columns.Add("refvalue");
            dt.Columns.Add("meanvalue");
            dt.Columns.Add("stddeviation");
            dt.Columns.Add("errorvalue");
            return dt;
        }

        private void Insert_DT_Measurement()
        {
            foreach(DeviceResponseValues drv in ResponseList)
            {
                if (!drv.Error)
                {
                    if (drv.OpCode == "g901" || drv.OpCode == "G901")
                    {
                        DataRow row = DT_Measurements.NewRow();
                        row["meas_time_start"] = drv.meas_time_start;
                        row["boxmode_hex"] = drv.BoxMode_hex;
                        row["boxmode_desc"] = drv.BoxMode_desc;
                        row["refvalue"] = drv.BoxMeasValue;
                        row["meanvalue"] = drv.MeanValue;
                        row["stddeviation"] = drv.StdDeviation;
                        row["errorvalue"] = drv.MeasErrorValue;
                        DT_Measurements.Rows.Add(row);
                    }
                }
            }
        }

    }

    public class DeviceResponseValues
    {
        public DeviceResponseValues(string opcodeRequest, string line = null)
        {
            OpCode = opcodeRequest;
            if (!string.IsNullOrEmpty(line) && line != "\0")
            { Response = line.Trim(); }
            else { Response = null; }
        }
        public DeviceResponseValues(clDeviceCom opcodeRequest, string line = null)
        {
            OpCode = opcodeRequest.ToString();
            meas_time_start = DateTime.Now;
            if (!string.IsNullOrEmpty(line))
            { Response = line.Trim(); }
        }
        public DateTime meas_time_start { get; private set; }
        public string StartDate { get { return $"{meas_time_start}.{meas_time_start.Millisecond}"; } }
        public bool Error { get; private set; } = false;
        private string _Response;
        public string Response
        {
            get { return _Response; }
            set
            {
                _Response = value;
                meas_time_start = DateTime.Now;
                if (!string.IsNullOrEmpty(value))
                { ResponseValues(); }
            }
        }
        public string ResponseParsed { get; set; }
        public string ResponseParsedLog { get; set; }
        public bool Response_Empty { get { return Response == null ? true : false; } }
        private string _OpCode;
        /// <summary>
        /// OpCode is Lower
        /// </summary>
        public string OpCode
        {
            get
            {
                if(_OpCode == null)
                {
                    _OpCode = Check_OpCode();
                }
                return _OpCode;
            }
            set { _OpCode = value.ToLower(); }
        }
        public string BoxMode_hex { get; set; }
        public bool BoxMode_Empty { get { return string.IsNullOrEmpty(BoxMode_hex); } }
        public string BoxMode_desc { get { return Get_BoxMode_Desc(); } }
        public string BoxErrorCode_hex { get; set; }
        public bool BoxErrorCode_Empty { get { return string.IsNullOrEmpty(BoxErrorCode_hex); } }
        public string BoxErrorCode_desc { get { return Get_BoxErrorCode_Desc(); } }
        public string BoxCalStatus_hex { get; set; }
        public bool BoxCalStatus_Empty { get { return string.IsNullOrEmpty(BoxCalStatus_hex); } }
        public string BoxCalStatus_desc { get { return Get_BoxCalStatus_Desc(); } }
        public string BoxMeasValue { get; set; }
        public string MeanValue { get; set; }
        public string StdDeviation { get; set; }
        public string MeasErrorValue { get; set; }
        private bool _TestError = false;
        public bool TestError
        {
            get
            {
                if (!string.IsNullOrEmpty(BoxMode_hex))
                {
                    _TestError = (BoxMode_hex == "17") ? true : false;
                }
                return _TestError;
            }
            private set { _TestError = value; }
        }
        private bool _TestFinalise = false;
        public bool TestFinalise
        {
            get
            {
                if (!string.IsNullOrEmpty(BoxMode_hex))
                { _TestFinalise = (BoxMode_hex == "37") ? true : false; }
                return _TestFinalise;
            }
            private set { _TestFinalise = value; }
        }

        private string[] _ResponseArray;
        
        public string[] ResponseArray
        {
            get
            {
                if(_ResponseArray == null && !Response_Empty)
                {
                    try
                    { _ResponseArray = Response.Split(';'); }
                    catch { }
                }
                return _ResponseArray;
            }
            private set { _ResponseArray = value; }
        }
        public int ResponseArrayCount { get { return ResponseArray != null ? ResponseArray.Length : 0; } }

        private void ResponseValues()
        {
            string[] Header = new string[0];
            int i = 0;
        again:
            i++;
            if (i > 2) { _OpCode = "parser_error"; }
            switch (OpCode.ToLower())
            {
                case "g015":
                    switch (ResponseArrayCount)
                    {
                        case 2:
                            Header = new string[] { "opcode", "value" };
                            break;
                        default:
                            _OpCode = Check_OpCode();
                            goto again;
                    }
                    break;
                case "g100":
                    switch (ResponseArrayCount)
                    {
                        case 3:
                            Header = new string[] { "opcode", "BoxMode", "CalStatus" };
                            break;
                        default:
                            _OpCode = Check_OpCode();
                            goto again;
                    }
                    break;
                case "g901":
                case "g200":
                    switch (ResponseArrayCount)
                    {
                        case 7:
                            Header = new string[] { "opcode", "BoxMode", "BoxErrorCode", "value", "Mean", "StdDev", "ErrorABS" };
                            break;
                        default:
                            _OpCode = Check_OpCode();
                            goto again;
                    }
                    break;
                case "parser_error":
                    Error = true;
                    BoxMeasValue = Response;
                    ResponseParsed = $"PARSER_ERROR:\t{Response}";
                    return;
                //case "s100":
                //case "s500":
                //case "s674":
                //case "s901":
                //case "s999":
                //case "S999":
                //case "g999":
                //    BoxMeasValue = Response;
                //    ResponseParsed = $"opcode:\t{OpCode}\t{Response}";
                //    return;
                case "state":
                case "error":
                    BoxMeasValue = Response;
                    ResponseParsed = Response;
                    return;
                default:
                    BoxMeasValue = Response;
                    ResponseParsed = $"opcode:\t{OpCode}\t{Response}";
                    return;
            }
            ResponseParsed = Parse(Header);
        }

        private string Parse(string[] header)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            foreach (string value in ResponseArray)
            {
                try { sb.Append(Parse_Values(header[i], i)); } catch { }
                i++;
            }
            return sb.ToString().Trim();
        }
        private string Parse_Values(string header, int answerIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{header}:\t");
            switch (header)
            {
                case "opcode":
                    sb.Append($"{OpCode}\t");
                    break;
                case "value":
                    try
                    {
                        BoxMeasValue = ResponseArray[answerIndex];
                        sb.Append($"{BoxMeasValue}\t");
                    } catch { }
                    break;
                case "Mean":
                    try
                    {
                        MeanValue = ResponseArray[answerIndex];
                        sb.Append($"{MeanValue}\t");
                    } catch { }
                    break;
                case "StdDev":
                    try { StdDeviation = ResponseArray[answerIndex]; sb.Append($"{StdDeviation}\t"); } catch { }
                    break;
                case "ErrorABS":
                    try { MeasErrorValue = ResponseArray[answerIndex]; sb.Append($"{MeasErrorValue}\t"); } catch { }
                    break;
                case "BoxMode":
                    try
                    {
                        BoxMode_hex = ResponseArray[answerIndex];
                        sb.Append($"{BoxMode_hex}\t{header}_desc:\t{BoxMode_desc}\t");
                    } catch { }
                    break;
                case "BoxErrorCode":
                    try
                    {
                        BoxErrorCode_hex = ResponseArray[answerIndex];
                        sb.Append($"{BoxErrorCode_hex}\t{header}_desc:\t{BoxErrorCode_desc}\t");
                    } catch { }
                    break;
                case "CalStatus":
                    try
                    {
                        BoxCalStatus_hex = ResponseArray[answerIndex];
                        sb.Append($"{BoxCalStatus_hex}\t{header}_desc:\t{BoxCalStatus_desc}\t");
                    } catch { }
                    break;
                default:
                    break;
            }
            string response = sb.ToString();
            if (string.IsNullOrEmpty(response)) { return ""; }
            else { return response; }
        }

        private string Check_OpCode()
        {
            if (ResponseArrayCount > 0)
            {
                string r = ResponseArray[0].Trim().ToLower();
                return Check_OpCode(r);
            }
            return _OpCode;
        }
        private string Check_OpCode(string opcode)
        {
            clDeviceCom.opcode result;
            if (Enum.TryParse(opcode, out result))
            {
                return result.ToString().ToLower();
            }
            else
            {
                return "parser_error"; 
            }
        }
       
        public string Get_BoxMode_Desc()
        {
            if (!BoxMode_Empty)
            { try { return clDeviceCom.BoxMode[BoxMode_hex]; } catch { } }
            return "";
        }
        public string Get_BoxErrorCode_Desc()
        {
            if (!BoxErrorCode_Empty)
            { try { return clDeviceCom.BoxErrorCode[BoxErrorCode_hex]; } catch { } }
            return "";
        }
        public string Get_BoxCalStatus_Desc()
        {
            if (!BoxCalStatus_Empty)
            { try { return clDeviceCom.CalibrationStatus[BoxCalStatus_hex]; } catch { } }
            return "";
        }

    }
}
