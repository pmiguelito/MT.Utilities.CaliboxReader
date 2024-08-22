
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReadCalibox.clGlobals;
using STDhelper;
using static STDhelper.clLogging;
using System.IO;

namespace ReadCalibox
{
    public class clLog
    {
        public clLog()
        {

        }
        public clLog(UC_Channel ucCH, DeviceResponse response)
        {
            Save(ucCH, response);
        }

        public clLog(UC_Channel ucCH, string response)
        {
            DeviceResponse resp = ucCH.SampleResponse;
            resp.Response = response;
            Save(ucCH, resp);
        }

        public void Save(UC_Channel ucCH, DeviceResponse response)
        {
            if (ucCH.LogPathActive && !response.Response_Empty)
            { BGW_MessageWriter = BGW_MessageWriter.Initialize(BGW_MessageWriter_DoWork, true, new LogValues() { Response = response, Channel = ucCH }); }
        }

        public void Save(UC_Channel ucCH, string response, clDeviceCom.opcode opcodeRequest)
        {
            DeviceResponse resp = new DeviceResponse(opcodeRequest,response);
            Save(ucCH, resp);
        }
        /***************************************************************************************
        * BackGroundWorker Write Messages
        ****************************************************************************************/
        public BackgroundWorker BGW_MessageWriter { get; set; }
        private void BGW_MessageWriter_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument != null)
                {
                    LogValues log = (LogValues)e.Argument;
                    foreach (DeviceResponseValues drv in log.Response.ResponseList)
                    {
                        if (ParseMessage(log, drv, out string message))
                        {
                            SaveLogMeas(log, message, drv);
                        }
                    }
                }
            }
            catch (Exception a)
            {
                ErrorHandler("BGW_MessageWriter", exception: a);
            }
        }


        /***************************************************************************************
        * Log Message:
        ****************************************************************************************/
        private bool ParseMessage(LogValues log, DeviceResponseValues message, out string messageValues)
        {
            if (string.IsNullOrEmpty(message.ResponseParsed))
            {
                messageValues = message.Response;
            }
            else
            { messageValues = message.ResponseParsed; }
            if (!log.Response.Response_Empty)
            {
                switch (log.Channel.BeM_Selected)
                {
                    //case "30326849": //O2 6850
                    //    if (empty) { return ""; }
                    //    break;
                    case "30259861": //Ozon NG sensor
                        if (messageValues == ".")
                        { return false; }
                        break;
                    //case "11556": //O2 allg
                    //    if (empty) { return ""; }
                    //    break;
                    case "30014462": //Ozon old sensor
                        if (messageValues == ".")
                        { return false; }
                        else if (messageValues.Length == 3)
                        {
                            if (messageValues.ToLower().Contains("?"))
                            { return false; }
                        }
                        break;
                    default:
                        break;
                }
                //messageValues = messageValues.Replace("\r", "\r\t\t\t\t").Trim();
                string date = $"{message.StartDate}";//$"{DateTime.Now}.{DateTime.Now.Millisecond}";
                messageValues = $"{date}\t{log.Channel.ucCOM.PortName}\t{log.Channel.BeM_Selected}\t{messageValues}";
                log.Response.ResponseParsedLog = messageValues;
                return true;
            }
            return false;
        }

        private void SaveLogMeas(LogValues log, string message, DeviceResponseValues drv)
        {
            SaveLogMeasFile(log, message, drv);
            if (log.Channel.calibrationRunning)
            { SaveLogMeasDB(log, message, drv); }
        }

        private void SaveLogMeasFile(LogValues log, string message, DeviceResponseValues drv)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    if (Directory.Exists(log.Channel.LogDirectory))
                {
                    int countAgain = 0;
                again:
                    if (!File.Exists(log.Channel.LogPathMeas))
                    {
                        // Create a file to write to.
                        try
                        {
                            using (StreamWriter sw = File.CreateText(log.Channel.LogPathMeas))
                            { sw.WriteLine("Date\tCOM\tBeM\tWait\tMessage"); }
                        }
                        catch
                        {
                            countAgain++;

                            if (countAgain < 2)
                            { goto again; }
                            else
                            { ErrorHandler("SaveLogMeas", message: "NEW crash"); }
                        }
                        countAgain = 0;
                    }
                    // This text is always added, making the file longer over time if it is not deleted.
                    try
                    {
                        using (StreamWriter sw = File.AppendText(log.Channel.LogPathMeas))
                        { sw.WriteLine(message); }
                    }
                    catch
                    {
                        countAgain++;
                        if (countAgain < 5) { goto again; }
                        else
                        { ErrorHandler("SaveLogMeas", message: "ADD crash"); }
                    }
                }
                });
            }
            catch
            {
                ErrorHandler("SaveLogMeas", message: $"ERROR: Directory {log.Channel.LogPathMeas} don't found");
            }
        }
        private void SaveLogMeasDB(LogValues log, string message, DeviceResponseValues drv)
        {
            try
            {
               bool saveDB = true;
                if (saveDB)
                {
                    Task.Factory.StartNew(() =>
                    {
                        clDatenBase.Set_Log(log.Channel.ODBC_EK, drv, log.Channel.Limits);
                    });
                }
                    
            }
            catch
            {
                ErrorHandler("SaveLogMeas", message: $"ERROR: Directory {log.Channel.LogPathMeas} don't found");
            }
        }

    }
    public class LogValues
    {
        public DeviceResponse Response { get; set; }
        public UC_Channel Channel { get; set; }
    }
}
