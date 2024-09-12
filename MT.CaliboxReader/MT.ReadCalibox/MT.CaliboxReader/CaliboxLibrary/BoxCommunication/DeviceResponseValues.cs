using CaliboxLibrary.BoxCommunication.CMDs;
using CaliboxLibrary.BoxCommunication.Responses;
using MT.OneWire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaliboxLibrary
{
    public class DeviceResponseValues
    {
        public DeviceResponseValues()
        {

        }

        /// <summary>
        /// Parse Data
        /// </summary>
        /// <param name="opcodeRequest"></param>
        /// <param name="cmd">Command Send</param>
        /// <param name="data"></param>
        public DeviceResponseValues(CmdSend cmd, string data = null, ChannelValues limits = null)
        {
            meas_time_start = DateTime.Now;
            CmdSend = cmd;
            OpCodeRequest = cmd.OpCode;
            CmdSendText = cmd.CmdText;
            Limits = limits;
            InitParse(data);
        }

        public DeviceResponseValues(BoxMode boxMode, string value, BoxErrorMode errorCode = null)
        {
            meas_time_start = DateTime.Now;
            BoxMode = boxMode;
            BoxMeasValue = value;
            if (errorCode != null)
            {
                BoxErrorMode = errorCode;
            }
            Response = value;
            ResponseParsed = value;
            ResponseParsedLog = value;
        }

        public DeviceResponseValues(string boxModeHex, string value, BoxErrorMode errorCode = null)
        {
            meas_time_start = DateTime.Now;
            BoxMode_hex = boxModeHex;
            BoxMeasValue = value;
            if (errorCode != null)
            {
                BoxErrorMode = errorCode;
            }
            Response = value;
            ResponseParsed = value;
            ResponseParsedLog = value;
        }

        /************************************************
         * FUNCTION:    Header
         * DESCRIPTION:
         ************************************************/
        public CmdSend CmdSend { get; private set; }
        public OpCode OpCodeRequest { get; private set; }
        public string CmdSendText { get; private set; }

        private OpCodeResponse _OpCodeResponse;
        public OpCodeResponse OpCodeResponse
        {
            get { return _OpCodeResponse; }
        }

        public OpCode OpCodeResp { get; set; }

        private ChannelValues _Limits;
        public ChannelValues Limits
        {
            get
            {
                if (_Limits == null)
                { _Limits = new ChannelValues(); }
                return _Limits;
            }
            set { _Limits = value; }
        }
        public BoxIdentification BoxId
        {
            get
            {
                if (Limits == null)
                { Limits = new ChannelValues(); }
                return Limits.BoxId;
            }
        }

        public DateTime meas_time_start { get; private set; }
        public string StartDate { get { return $"{meas_time_start}.{meas_time_start.Millisecond}"; } }

        /************************************************
         * FUNCTION:    Error
         * DESCRIPTION:
         ************************************************/
        public bool IsError
        {
            get
            {
                return IsBoxModeError || IsBoxErrorError || IsBoxCalModeError;
            }
        }
        public bool IsFinished
        {
            get
            {
                return IsBoxModeFinished || IsBoxErrorFinished;
            }
        }
        public bool IsMeasValues
        {
            get
            {
                if (BoxMode != null && BoxMode.IsMeasValues && OpCodeResp == OpCode.g901)
                {
                    return true;
                }
                return false;
            }
        }

        /************************************************
         * FUNCTION:    States
         * DESCRIPTION:
         ************************************************/
        public BoxMode BoxMode { get; private set; } = BoxMode.Box_Idle;
        public string BoxMode_hex
        {
            get { return BoxMode?.Hex; }
            private set
            {
                BoxMode = BoxMode.FromHex(value);
            }
        }

        public bool IsBoxModeError
        {
            get
            {
                return BoxMode?.IsError ?? false;
            }
        }
        public bool IsBoxModeFinished
        {
            get
            {
                return BoxMode?.IsFinished ?? false;
            }
        }

        public BoxErrorMode BoxErrorMode { get; set; } = BoxErrorMode.NoError;
        public string BoxErrorMode_hex
        {
            get { return BoxErrorMode?.Hex; }
            set { BoxErrorMode = BoxErrorMode.FromHex(value); }
        }
        public bool IsBoxErrorError
        {
            get
            {
                return BoxErrorMode?.IsError ?? false;
            }
        }
        public bool IsBoxErrorFinished
        {
            get
            {
                return BoxErrorMode?.IsFinished ?? false;
            }
        }

        public BoxCalMode BoxCalMode { get; private set; } = BoxCalMode.NotDefined;
        public string BoxCalMode_hex
        {
            get { return BoxCalMode?.Hex; }
            set { BoxCalMode = BoxCalMode.FromHex(value); }
        }
        public bool IsBoxCalModeError
        {
            get
            {
                return BoxCalMode?.IsError ?? false;
            }
        }

        //public string BoxCalStatus_desc { get { return BoxCalModus.Desc; } }
        public string BoxMeasValue { get; set; }

        /************************************************
         * FUNCTION:    Measurement Values
         * DESCRIPTION:
         ************************************************/
        public ResponseMeasValue MeasValues { get; set; } = new ResponseMeasValue();

        /************************************************
         * FUNCTION:    Parse
         * DESCRIPTION:
         ************************************************/
        public string Response { get; private set; }
        public bool Response_Empty { get { return string.IsNullOrEmpty(Response); } }
        public string ResponseParsed { get; set; }
        public string ResponseParsedLog { get; set; }

        public string[] ResponseArray { get; private set; }
        public int ResponseArrayCount { get { return ResponseArray != null ? ResponseArray.Length : 0; } }

        private void InitParse(string data)
        {
            meas_time_start = DateTime.Now;
            if (data == null) { Response = null; return; }
            Response = data?.Replace("\0", "").Replace("*", "").Trim();
            if (Split(Response))
            {
                OpCodeResp = GetOpCodeResponse(out _OpCodeResponse);
                ResponseParser();
            }
        }

        private bool Split(string data)
        {
            if (data.Length > 0)
            {
                ResponseArray = data.Split(';');
                return ResponseArrayCount > 0;
            }
            return false;
        }

        private void ResponseParser()
        {
            if (ResponseArrayCount == 0) { return; }
            BoxMeasValue = Response;
            ResponseParsed = Response;
            string[] split = new string[0];
            int i = 0;
        again:
            i++;
            if (i > 2)
            {
                OpCodeResp = OpCode.parser_error;
            }
            switch (OpCodeResp)
            {
                case OpCode.g100:
                case OpCode.g200:
                case OpCode.g901:
                case OpCode.g903:
                case OpCode.g904:
                case OpCode.g905:
                case OpCode.g906:
                case OpCode.g907:
                case OpCode.g908:
                case OpCode.g909:
                case OpCode.g910:
                case OpCode.g911:
                    break;
                case OpCode.parser_error:
                    if (Response.Contains("DelP"))
                    { OpCodeResp = OpCode.init; goto case OpCode.init; } // { ResponseParsed = $"INIT:\t{Response}"; return; }
                    ResponseParsed = $"PARSER_ERROR:\t{Response}";
                    return;
                case OpCode.s999:
                    BoxMeasValue = Response?.Trim();
                    var array = new List<string>() { OpCodeResp.ToString() };
                    array.AddRange(ResponseArray);
                    ResponseArray = array.ToArray();
                    ResponseParsed = $"opcode:\t{OpCodeResp}\t{BoxMeasValue}";
                    BoxId.GetValues(Response);
                    break;
                case OpCode.g015:
                    if (ResponseArrayCount > 0)
                    {
                        var hex = ResponseArray[1];
                        if (hex.Length > 31)
                        {
                            var pageData = new TEDSPageData(15, hex);
                            Limits.TEDSpageDatas.Add(pageData, checkHeaders: false);
                            pageData.ReadHEX();
                            pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                            BoxMeasValue = Limits.TEDSpageDatas.HeaderValues.SensorFWVersion;
                        }
                        ResponseArray = new string[] { ResponseArray[0], ResponseArray[1], BoxMeasValue };
                        _OpCodeResponse.Values = ResponseArray;
                        Limits.sample_FW_Version_value = BoxMeasValue.Trim();
                        break;
                    }
                    goto case OpCode.parser_error;
                case OpCode.g000:
                case OpCode.g001:
                case OpCode.g002:
                case OpCode.rdpg:
                    split = Response.Split(' ');
                    OpCodeResp = OpCode.rdpg;
                    if (split.Count() > 1)
                    {
                        var page = split[1];
                        if (int.TryParse(page, out int pageNo))
                        {
                            string hex = split[2];
                            var pageData = new TEDSPageData(pageNo, hex);
                            Limits.TEDSpageDatas.Add(pageData, checkHeaders: false);
                            pageData.ReadHEX();
                            pageData.SensorPageDatas.HeaderValues.LoadValues(pageData.SensorPageDatas.GetAll());
                            var header = Limits.TEDSpageDatas.HeaderValues;
                            switch (pageNo)
                            {
                                case 0:
                                    BoxMeasValue = $"PN: {header.SensorItem}\tSN: {header.SerialNum}";
                                    break;
                                case 1:
                                    BoxMeasValue = $"FactoryCalDate: {header.FactoryCalDate}\t";
                                    break;
                                case 2:
                                    BoxMeasValue = $"PN Desc: {header.SensorItemDesc}\t";
                                    break;
                            }

                        }
                        //if (TdlData.CMD_ReadPage(page, split[2], out List<string> pageResult))
                        //{
                        //    BoxMeasValue = string.Join("\t", pageResult);
                        //}
                        //else
                        //{
                        //    BoxMeasValue = split[2];
                        //}
                        ResponseParsed = $"opcode:\t{OpCodeResp}\tPage:\t{page}\t{BoxMeasValue}";
                        return;
                    }
                    else
                    {
                        ResponseParsed = $"{OpCodeResp}:\t{Response}";
                    }
                    return;
                case OpCode.wrpg:
                    split = Response.Split(' ');
                    OpCodeResp = OpCode.wrpg;
                    try
                    {
                        BoxMeasValue = split[2];
                        long.TryParse(split[1], System.Globalization.NumberStyles.HexNumber, null, out long page);
                        ResponseParsed = $"opcode:\t{OpCodeResp}\tPage:\t{page}\tvalue:\t{BoxMeasValue}";
                        return;
                    }
                    catch { }
                    ResponseParsed = $"{OpCodeResp}:\t{Response}";
                    return;
                case OpCode.rdbx:
                    //if (BoxId == null) { BoxId = new BoxIdentification(); }
                    BoxId.RDBX(Response);
                    ResponseParsed = Response;
                    return;
                case OpCode.cmdsend:
                case OpCode.state:
                    BoxMeasValue = Response;
                    ResponseParsed = $"{OpCodeResp}:\t{Response}";
                    return;
                case OpCode.error:
                    BoxMeasValue = Response;
                    ResponseParsed = Response;
                    return;
                case OpCode.init:
                    ResponseParsed = $"INIT:\t{Response}";
                    return;
                default:
                    if (ResponseArrayCount > 0)
                    {
                        OpCodeResp = GetOpCodeResponse(out _OpCodeResponse);
                        goto again;
                    }
                    BoxMeasValue = Response;
                    ResponseParsed = $"opcode:\t{OpCodeResp}\t{Response}";
                    return;
            }
            ResponseParsed = Parse(_OpCodeResponse.Header);
            if (BoxMode.Hex == BoxMode.Box_SensorCheckUpol_500.Hex || BoxMode.Hex == BoxMode.Box_SensorCheckUpol_674.Hex)
            {
                double.TryParse(MeasValues.I_Set, out var set);
                if (set > 0)
                {
                    double.TryParse(MeasValues.I_AVG, out var avg);

                    var diff = set - avg;
                    var factor = set * .03;
                    if (diff > factor)
                    {
                        BoxErrorMode = BoxErrorMode.Error;
                    }
                }

            }
        }
        private string Parse(string[] header)
        {
            _OpCodeResponse.Values = ResponseArray;
            _OpCodeResponse.ResponseValues = Response;
            int i = 0;
            StringBuilder sb = new StringBuilder();
            foreach (string value in ResponseArray)
            {
                try { sb.Append(Parse_Values(header[i], i)); }
                catch { break; }
                i++;
            }
            return sb.ToString().Trim();
        }
        private string Parse_Values(string header, int answerIndex)
        {
            StringBuilder sb = new StringBuilder();
            var value = ResponseArray[answerIndex];

            switch (header)
            {
                //case BoxParse.MN_Opcode:
                //    sb.Append($"{value}\t");
                //    break;
                //case BoxParse.MN_Values:
                case BoxParse.MN_I_set:
                    sb.Append($"{MeasValues.Meas_I.AddSet(value)}\t");
                    break;
                case BoxParse.MN_I_AVG:
                    sb.Append($"{MeasValues.Meas_I.AddAvg(value)}\t");
                    break;
                case BoxParse.MN_I_StdDev:
                    sb.Append($"{MeasValues.Meas_I.AddStdDev(value)}\t");
                    break;
                case BoxParse.MN_I_Error:
                    sb.Append($"{MeasValues.Meas_I.AddError(value)}\t");
                    break;
                case BoxParse.MN_Temp_set:
                    sb.Append($"{MeasValues.Meas_Temp.AddSet(value)}\t");
                    break;
                case BoxParse.MN_Temp_AVG:
                    sb.Append($"{MeasValues.Meas_Temp.AddAvg(value)}\t");
                    break;
                case BoxParse.MN_Temp_StdDev:
                    sb.Append($"{MeasValues.Meas_Temp.AddStdDev(value)}\t");
                    break;
                case BoxParse.MN_Temp_Error:
                    sb.Append($"{MeasValues.Meas_Temp.AddError(value)}\t");
                    break;
                case BoxParse.MN_BoxMode_HEX:
                    try
                    {
                        BoxMode = BoxMode.FromHex(value);
                        sb.Append($"{BoxMode.ToStringWithHeader()}\t");
                    }
                    catch
                    { }
                    break;
                case BoxParse.MN_BoxErrorCode_HEX:
                    BoxErrorMode = BoxErrorMode.FromHex(value);
                    sb.Append($"{BoxErrorMode.ToStringWithHeader()}\t");
                    break;
                case BoxParse.MN_CalStatus_HEX:
                    BoxCalMode = BoxCalMode.FromHex(value);
                    sb.Append($"{BoxCalMode.ToStringWithHeader()}\t");
                    break;
                default:
                    sb.Append($"{header}:\t");
                    sb.Append($"{value}\t");
                    break;
            }
            string response = sb.ToString();
            if (string.IsNullOrEmpty(response)) { return ""; }
            else { return response; }
        }

        private OpCode GetOpCodeResponse(out OpCodeResponse opCodeResponse)
        {
            if (ResponseArrayCount > 0)
            {
                string opCodeTxt = ResponseArray[0].Trim();
                OpCode opCode = Check_OpCode_ToAnswer(opCodeTxt);
                opCodeResponse = BoxParse.GetOpCodeResponse(opCode);
                return opCode;
            }
            opCodeResponse = BoxParse.GetOpCodeResponse(OpCode.parser_error);
            return OpCode.parser_error;
        }
        private OpCode Check_OpCode(string opcode)
        {
            return opcode.ParseOpcode(defaultOpcode: OpCode.parser_error);
        }
        private OpCode Check_OpCode_ToAnswer(string opcode)
        {
            return opcode.ParseOpcode(toLower: true, defaultOpcode: OpCode.parser_error);
        }
        private OpCode Check_OpCode_ToAnswer(OpCode opcode)
        {
            return opcode.ToString().ParseOpcode(toLower: true, defaultOpcode: OpCode.parser_error);
        }
    }
}
