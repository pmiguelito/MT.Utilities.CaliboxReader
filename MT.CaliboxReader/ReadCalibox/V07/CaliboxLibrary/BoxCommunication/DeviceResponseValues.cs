using MT.OneWire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaliboxLibrary
{
    public class DeviceResponseValues
    {
        /// <summary>
        /// Parse Data
        /// </summary>
        /// <param name="opcodeRequest"></param>
        /// <param name="cmd">Command Send</param>
        /// <param name="data"></param>
        public DeviceResponseValues(OpCode opcodeRequest, string cmd = null, string data = null, ChannelValues limits = null)
        {
            meas_time_start = DateTime.Now;
            OpCode = opcodeRequest;
            CmdSended = cmd;
            Limits = limits;
            InitParse(data);
        }
        public DeviceResponseValues(string boxmodeHex, string value, BoxErrorMode errorCode = null)
        {
            meas_time_start = DateTime.Now;
            BoxMode_hex = boxmodeHex;
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
        public OpCode OpCode { get; private set; }

        public string CmdSended { get; private set; }

        public ChannelValues Limits { get; set; }
        public BoxIdentification BoxId
        {
            get
            {
                if (Limits == null) { Limits = new ChannelValues(); }
                return Limits.BoxId;
            }
            set
            {
                if (Limits == null) { Limits = new ChannelValues(); }
                Limits.BoxId = value;
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
                return IsBoxModeError || IsBoxErrorError || IsBoxCalStatusError;
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
                if (BoxMode != null && BoxMode.IsMeasValues && OpCode == OpCode.g901)
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
            get { return BoxMode.Hex; }
            private set { BoxMode = BoxMode.FromHex(value); }
        }
        public string BoxMode_desc { get { return BoxMode.Desc; } }
        public bool IsBoxModeError
        {
            get
            {
                if (BoxMode == null) { return false; }
                return BoxMode.IsError;
            }
        }
        public bool IsBoxModeFinished
        {
            get
            {
                if (BoxMode == null) { return false; }
                return BoxMode.IsFinished;
            }
        }

        public BoxErrorMode BoxErrorMode { get; set; } = BoxErrorMode.NoError;
        public string BoxErrorCode_hex
        {
            get { return BoxErrorMode.Hex; }
            set { BoxErrorMode = BoxErrorMode.FromHex(value); }
        }
        public string BoxErrorCode_desc { get { return BoxErrorMode.Desc; } }
        public bool IsBoxErrorError
        {
            get
            {
                if (BoxErrorMode == null) { return false; }
                return BoxErrorMode.IsError;
            }
        }
        public bool IsBoxErrorFinished
        {
            get
            {
                if (BoxErrorMode == null) { return false; }
                return BoxErrorMode.IsFinished;
            }
        }

        public BoxCalModus BoxCalModus { get; private set; } = BoxCalModus.NotDefined;
        public string BoxCalStatus_hex
        {
            get { return BoxCalModus.Hex; }
            set { BoxCalModus = BoxCalModus.FromHex(value); }
        }
        public bool IsBoxCalStatusError
        {
            get
            {
                if (BoxCalModus == null) { return false; }
                return BoxCalModus.IsError;
            }
        }

        public string BoxCalStatus_desc { get { return BoxCalModus.Desc; } }
        public string BoxMeasValue { get; set; }

        /************************************************
         * FUNCTION:    Measurement Values
         * DESCRIPTION:
         ************************************************/
        public MeasValues Meas_I = new MeasValues(DataBase.DbMeasBaseValues.I);
        public MeasValue Iset { get { return Meas_I.Set; } }
        //private string _I_Set;
        public string I_Set { get => Iset?.Value; }
        public double? I_SetDec { get => Iset?.ValueNumeric; }

        public MeasValue Iavg { get { return Meas_I.Avg; } }
        public string I_AVG { get => Iavg?.Value; }
        public double? I_AVGDec { get => Iavg?.ValueNumeric; }

        public MeasValue IstdDev { get { return Meas_I.StdDev; } }
        public string I_StdDev { get => IstdDev?.Value; }
        public double? I_StdDevDec { get => IstdDev?.ValueNumeric; }

        public MeasValue Ierror { get { return Meas_I.ErrorAbs; } }
        public string I_Error { get => Ierror?.Value; }
        public double? I_ErrorDec { get => Ierror?.ValueNumeric; }

        public MeasValues Meas_Temp = new MeasValues(DataBase.DbMeasBaseValues.Temp);
        public MeasValue TempSet { get { return Meas_Temp.Set; } }
        public string Temp_Set { get => TempSet?.Value; }
        public double? Temp_SetDec { get => TempSet?.ValueNumeric; }

        public MeasValue TempAvg { get { return Meas_Temp.Avg; } }
        public string Temp_AVG { get => TempAvg?.Value; }
        public double? Temp_AVGDec { get => TempAvg?.ValueNumeric; }

        public MeasValue TempStdDev { get { return Meas_Temp.StdDev; } }
        public string Temp_StdDev { get => TempStdDev?.Value; }
        public double? Temp_StdDevDec { get => TempStdDev?.ValueNumeric; }

        public MeasValue TempError { get { return Meas_Temp.ErrorAbs; } }
        public string Temp_Error { get => TempError?.Value; }
        public double? Temp_ErrorDec { get => TempError?.ValueNumeric; }

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
                OpCode = Check_OpCode();
                ResponseParser();
            }
        }

        private OpCodeResponse _OpCodeResponse;
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
                OpCode = OpCode.parser_error;
            }
            switch (OpCode)
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
                    if (Response.Contains("DelP")) { OpCode = OpCode.init; goto case OpCode.init; } // { ResponseParsed = $"INIT:\t{Response}"; return; }
                    ResponseParsed = $"PARSER_ERROR:\t{Response}";
                    return;
                case OpCode.s999:
                    BoxMeasValue = Response?.Trim();
                    var array = new List<string>() { OpCode.ToString() };
                    array.AddRange(ResponseArray);
                    ResponseArray = array.ToArray();
                    ResponseParsed = $"opcode:\t{OpCode}\t{BoxMeasValue}";
                    BoxId.GetValues(Response);
                    break;
                case OpCode.g015:
                    if (ResponseArrayCount > 0)
                    {
                        if (Limits == null) { Limits = new ChannelValues(); }
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
                        Limits.sample_FW_Version_value = BoxMeasValue;
                        break;
                    }
                    goto case OpCode.parser_error;
                case OpCode.g000:
                case OpCode.g001:
                case OpCode.g002:
                case OpCode.rdpg:
                    split = Response.Split(' ');
                    OpCode = OpCode.rdpg;
                    if (split.Count() > 1)
                    {
                        var page = split[1];
                        if (int.TryParse(page, out int pageNo))
                        {
                            if (Limits == null) { Limits = new ChannelValues(); }
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
                        ResponseParsed = $"opcode:\t{OpCode}\tPage:\t{page}\t{BoxMeasValue}";
                        return;
                    }
                    else
                    {
                        ResponseParsed = $"{OpCode}:\t{Response}";
                    }
                    return;
                case OpCode.wrpg:
                    split = Response.Split(' ');
                    OpCode = OpCode.wrpg;
                    try
                    {
                        BoxMeasValue = split[2];
                        long.TryParse(split[1], System.Globalization.NumberStyles.HexNumber, null, out long page);
                        ResponseParsed = $"opcode:\t{OpCode}\tPage:\t{page}\tvalue:\t{BoxMeasValue}";
                        return;
                    }
                    catch { }
                    ResponseParsed = $"{OpCode}:\t{Response}";
                    return;
                case OpCode.rdbx:
                    if (BoxId == null) { BoxId = new BoxIdentification(); }
                    BoxId.RDBX(Response);
                    ResponseParsed = Response;
                    return;
                case OpCode.cmdsend:
                case OpCode.state:
                    BoxMeasValue = Response;
                    ResponseParsed = $"{OpCode}:\t{Response}";
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
                        OpCode = Check_OpCode();
                        goto again;
                    }
                    BoxMeasValue = Response;
                    ResponseParsed = $"opcode:\t{OpCode}\t{Response}";
                    return;
            }
            ResponseParsed = Parse(_OpCodeResponse.Header);
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
                    sb.Append($"{Meas_I.AddSet(value)}\t");
                    break;
                case BoxParse.MN_I_AVG:
                    sb.Append($"{Meas_I.AddAvg(value)}\t");
                    break;
                case BoxParse.MN_I_StdDev:
                    sb.Append($"{Meas_I.AddStdDev(value)}\t");
                    break;
                case BoxParse.MN_I_Error:
                    sb.Append($"{Meas_I.AddError(value)}\t");
                    break;
                case BoxParse.MN_Temp_set:
                    sb.Append($"{Meas_Temp.AddSet(value)}\t");
                    break;
                case BoxParse.MN_Temp_AVG:
                    sb.Append($"{Meas_Temp.AddAvg(value)}\t");
                    break;
                case BoxParse.MN_Temp_StdDev:
                    sb.Append($"{Meas_Temp.AddStdDev(value)}\t");
                    break;
                case BoxParse.MN_Temp_Error:
                    sb.Append($"{Meas_Temp.AddError(value)}\t");
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
                    BoxCalModus = BoxCalModus.FromHex(value);
                    sb.Append($"{BoxCalModus.ToStringWithHeader()}\t");
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

        private OpCode Check_OpCode()
        {
            if (ResponseArrayCount > 0)
            {
                string opcode = ResponseArray[0].Trim();
                _OpCodeResponse = BoxParse.GetOpCodeResponse(opcode);
                return Check_OpCode_ToAnswer(opcode);
            }
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
