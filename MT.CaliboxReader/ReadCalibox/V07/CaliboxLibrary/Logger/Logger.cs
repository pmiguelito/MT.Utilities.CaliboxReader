
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STDhelper;
using static STDhelper.clLogging;
using System.IO;
using CaliboxLibrary;

namespace CaliboxLibrary
{
    public class Logger
    {
        private clLogWriter _LogWriter;
        private clLogWriter LogWriter
        {
            get
            {
                if (_LogWriter == null)
                {
                    _LogWriter = clLogWriter.GetInstance;
                    _LogWriter.FlushAtQty = 100;
                    _LogWriter.FileLenghtKB = 3000;
                    _LogWriter.FlushAtAge_sec = 1;
                }
                return _LogWriter;
            }
        }

        public string Path
        {
            get { return LogWriter.LogPathWithFile; }
            set { LogWriter.LogPathWithFile = value; }
        }

        public int FlushAtAge_sec
        {
            get { return LogWriter.FlushAtAge_sec; }
            set { LogWriter.FlushAtAge_sec = value; }
        }
        public int FlushAtQty
        {
            get { return LogWriter.FlushAtQty; }
            set { LogWriter.FlushAtQty = value; }
        }

        public int FileLenght_KB
        {
            get { return LogWriter.FileLenghtKB; }
            set { LogWriter.FileLenghtKB = value; }
        }

        public bool LogMeasDB_Active { get; set; }
        public bool LogMeasPath_Active { get; set; }

        public string ChannelNo { get; set; }
        public string BeM { get; set; }
        public string Odbc { get; set; }

        public Logger(string channelNo = null, string bem = null)
        {
            ChannelNo = channelNo;
            BeM = bem;
        }

        public Logger(string path)
        {
            Path = path;
        }
        public Logger(string path, string channelNo, string bem) : this(channelNo, bem)
        {
            Path = path;
        }

        /************************************************
         * FUNCTION:    Check File Lenght
         * DESCRIPTION:
         ************************************************/
        public const string FileEnding = ".log";
        public string CheckFileLenght(string directory, string fileName)
        {
            var files = SearchFiles(directory, fileName);
            if (files.Any())
            {
                var lFiles = new List<FileInfo>();
                foreach (var file in files)
                {
                    lFiles.Add(new FileInfo(file));
                }
                var max = FileLenght_KB * 1000;
                var list = from file in lFiles
                           where file.Length < max
                           orderby file.LastWriteTime
                           select file;
                var fi = list.FirstOrDefault();
                if (fi != null)
                {
                    Path = fi.FullName;
                    return Path;
                }
            }
            string counter = files.Count().ToString().PadLeft(3, '0');
            string fileNameFull = $"{fileName}_{counter}{FileEnding}";
            Path = System.IO.Path.Combine(directory, fileNameFull);
            return Path;
        }

        public string[] SearchFiles(string directory, string fileName)
        {
            fileName = FileNameCutEnding(fileName);
            var files = Directory.GetFiles(directory, $"*{fileName}*{FileEnding}");
            return files;
        }

        private string FileNameCutEnding(string fileName)
        {
            var i = fileName.LastIndexOf(".");
            if (i > 0)
            {
                fileName = fileName.Substring(0, i);
            }
            return fileName;
        }

        /************************************************
         * FUNCTION:    Save
         * DESCRIPTION:
         ************************************************/
        public void ForceLog()
        {
            LogWriter.ForceFlush();
        }

        public void Save(DeviceResponseValues response, ChannelValues limits = null)
        {
            if (!response.Response_Empty)
            {
                if (LogMeasDB_Active || LogMeasPath_Active)
                {
                    Task t = Task.Factory.StartNew(() =>
                    {
                        DoWork(new LogValues()
                        {
                            Response = response,
                            Channel_No = ChannelNo,
                            BeM = BeM,
                            Limits = limits
                        });
                    });
                }
            }
        }

        public void Save(string state, OpCode opcodeRequest, string response = null)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                if (ParseMessage(state, opcodeRequest.ToString(), response, out LogValues log))
                {
                    SaveLogMeasFile(log, log.message);
                }
            });
        }
        public void Save(string state, string response)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                if (ParseMessage(state, null, response, out LogValues log))
                {
                    SaveLogMeasFile(log, log.message);
                }
            });
        }
        public void Save(string response)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                if (ParseMessage(null, null, response, out LogValues log))
                {
                    SaveLogMeasFile(log, log.message);
                }
            });
        }
        public void Save(OpCode opcodeRequest, string response)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                if (ParseMessage(null, opcodeRequest.ToString(), response, out LogValues log))
                {
                    SaveLogMeasFile(log, log.message);
                }
            });
        }
        public void Save(Exception ex)
        {
            LogWriter.WriteToLog(ex, Path);
        }
        public void Save(string msg, Exception ex)
        {
            LogWriter.WriteToLog(msg);
            LogWriter.WriteToLog(ex);
        }

        public static void Save(string path, string msg, Exception ex)
        {
            var log = new Logger(path);
            log.Save(msg, ex);
        }

        /***************************************************************************************
        * BackGroundWorker Write Messages
        ****************************************************************************************/
        public BackgroundWorker BGW_MessageWriter = new BackgroundWorker();
        private void BGW_MessageWriter_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument != null)
                {
                    DoWork((LogValues)e.Argument);
                }
            }
            catch (Exception a)
            {
                ErrorHandler("BGW_MessageWriter_DoWork Starter", exception: a);
            }
        }
        private void DoWork(LogValues log)
        {
            try
            {
                //foreach (DeviceResponseValues drv in log.Response.ResponseList)
                {
                    if (ParseMessage(log, log.Response, out string message))
                    {
                        SaveLogMeas(log, message, log.Response);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHandler("BGW_MessageWriter_DoWork Executable", e);
            }
        }

        /***************************************************************************************
        * Log Message:  Parse
        ****************************************************************************************/
        private bool ParseMessage(LogValues log, DeviceResponseValues message, out string messageValues)
        {
            if (string.IsNullOrEmpty(message.ResponseParsed))
            {
                messageValues = message.Response;
            }
            else
            {
                messageValues = message.ResponseParsed;
            }
            if (!log.Response.Response_Empty)
            {
                switch (BeM)
                {
                    case "30259861": //Ozon NG sensor
                        if (messageValues == ".")
                        { return false; }
                        break;
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
                messageValues = ChannelNo + "\t" + messageValues;
                //messageValues = $"{log.Channel.Channel_No}\t{log.Channel.BeM_Selected}\t{messageValues}";
                log.Response.ResponseParsedLog = messageValues;
                return true;
            }
            return false;
        }
        private bool ParseMessage(string state, string opcode, string response, out LogValues log)
        {
            log = new LogValues();
            if (!string.IsNullOrEmpty(response))
            {
                switch (BeM)
                {
                    case "30259861": //Ozon NG sensor
                        if (response == ".")
                        { return false; }
                        break;
                    case "30014462": //Ozon old sensor
                        if (response == ".")
                        { return false; }
                        else if (response.Length == 3)
                        {
                            if (response.ToLower().Contains("?"))
                            { return false; }
                        }
                        break;
                    default:
                        break;
                }
            }
            string message = "";
            if (!string.IsNullOrEmpty(state))
            {
                message += $"\tstate: {state}";
            }
            if (!string.IsNullOrEmpty(opcode))
            {
                if (opcode != "state")
                {
                    message += $"\tOpcode: {opcode}";
                }
            }
            if (!string.IsNullOrEmpty(response))
            {
                message += $"\t{response}";
            }
            log.message = ChannelNo + message;
            return true;
        }


        /***************************************************************************************
        * Log Message:
        ****************************************************************************************/
        private void SaveLogMeas(LogValues log, string message, DeviceResponseValues drv)
        {
            SaveLogMeasFile(log, message);
            SaveLogMeasDB(log, drv);
        }

        private void SaveLogMeasFile(LogValues log, string message)
        {
            if (LogMeasPath_Active)
            {
                LogWriter.WriteToLog(message, Path);
            }
        }
        private void SaveLogMeasDB(LogValues log, DeviceResponseValues drv)
        {
            try
            {
                if (LogMeasDB_Active)
                {
                    DataBase.Set_Log(Odbc, drv, log.Limits);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler("SaveLogMeas", message: $"ERROR: ODBC:{Odbc} {ex.Message}");
            }
        }

    }
}
