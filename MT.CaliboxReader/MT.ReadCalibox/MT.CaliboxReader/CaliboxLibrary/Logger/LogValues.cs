using System;

namespace CaliboxLibrary
{
    public class LogValues
    {
        public DateTime LogTime { get; private set; }
        public LogValues()
        {
            LogTime = DateTime.Now;
        }

        public string message { get; set; }
        public DeviceResponseValues Response { get; set; }
        public string Channel_No { get; set; }
        public string BeM { get; set; }
        public ChannelValues Limits { get; set; }
    }
}
