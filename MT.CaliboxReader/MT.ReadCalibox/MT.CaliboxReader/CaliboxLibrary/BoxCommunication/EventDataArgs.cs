using CaliboxLibrary.BoxCommunication.CMDs;
using System;

namespace CaliboxLibrary
{
    public class EventDataArgs : EventArgs
    {
        public CmdSend CmdSended { get; private set; }
        public string CMDsendText { get { return CmdSended.CmdText; } }
        public OpCode OpCodeSended { get { return CmdSended.OpCode; } }

        public OpCode OpCodeReceived { get; private set; }
        public bool IsOpCodeReceivedAnswer { get; set; }
        public bool IsData { get; private set; }
        public string Data { get; private set; }
        public DeviceResponseValues DeviceResponseValue { get; private set; }
        public EventDataArgs(CmdSend cmd, string data)
        {
            CmdSended = cmd;
            Data = data;
            IsData = string.IsNullOrEmpty(data) == false;
        }

        public bool Insert(DeviceResponseValues values)
        {
            DeviceResponseValue = values;
            return Received(values.OpCodeResp);
        }

        public bool Received(OpCode opCode)
        {
            OpCodeReceived = opCode;
            IsOpCodeReceivedAnswer = OpCodeSended.ToString().ToLower() == opCode.ToString().ToLower();
            return IsOpCodeReceivedAnswer;
        }
    }
}

