﻿using System;

namespace CaliboxLibrary
{
    public class Handler
    {
        public static string DateTimeFormat_Meas { get; } = "yyyy.MM.dd HH:mm:ss.fff";
        public static string DateTimeNow { get { return DateTime.Now.ToSQLstring(true); } }

        public static string EK_SW_Version;
        public static void ChangeOdbcEK(string odbc)
        {
            TdlData.Initialization(odbc);
        }
    }
}
