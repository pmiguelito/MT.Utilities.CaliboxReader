
using STDhelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static STDhelper.clLogging;

namespace CaliboxLibrary
{
    public class Logger
    {
        public bool LogMeasDB_Active { get; set; }
        public bool LogMeasPath_Active { get; set; }

        public string ChannelNo { get; set; }
        public string BeM { get; set; }
        public string Odbc { get; set; }

        /************************************************
         * FUNCTION:    Constructor(s)
         * DESCRIPTION:
         ************************************************/
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
         * FUNCTION:    Worker
         * DESCRIPTION:
         ************************************************/
        public LoggerWorker Worker { get; set; } = new LoggerWorker();

        /************************************************
         * FUNCTION:    File Path
         * DESCRIPTION:
         ************************************************/

        public string Path
        {
            get { return LogPathWithFile; }
            set { LogPathWithFile = value; }
        }

        public string AppName
        {
            get { return Process.GetCurrentProcess().ProcessName; }
        }
        private string _LogPathDefault;

        private string _LogFileDefault;

        private string _LogPath;

        private string _LogFile;

        private string _LogPathWithFile;
        private string LogPathDefault
        {
            get
            {
                if (string.IsNullOrEmpty(LogFileDefault))
                {
                    _LogPathDefault = "C:\\TEMP\\" + AppName;
                }

                return _LogPathDefault;
            }
        }

        private string LogFileDefault
        {
            get
            {
                if (string.IsNullOrEmpty(_LogFileDefault))
                {
                    _LogFileDefault = AppName + "_Error.Log";
                }

                return _LogFileDefault;
            }
        }

        private string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_LogPath))
                {
                    _LogPath = LogPathDefault;
                }

                return _LogPath;
            }
            set
            {
                _LogPath = value;
            }
        }

        private string LogFile
        {
            get
            {
                if (string.IsNullOrEmpty(_LogFile))
                {
                    _LogFile = LogFileDefault;
                }

                return _LogFile;
            }
            set
            {
                _LogFile = value;
            }
        }

        private string LogPathWithFile
        {
            get
            {
                if (string.IsNullOrEmpty(_LogPathWithFile))
                {
                    return LogPath + "\\" + LogFile;
                }

                return _LogPathWithFile;
            }
            set
            {
                _LogPathWithFile = value;
            }
        }

        /************************************************
         * FUNCTION:    File Length
         * DESCRIPTION:
         ************************************************/
        public int FileLenghtKB { get; set; } = 3000;

        public int FlushAtAge_sec { get; set; } = 1;

        public int FlushAtQty { get; set; } = 10;

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
                var max = FileLenghtKB * 1000;
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
         * FUNCTION:    Delete
         * DESCRIPTION:
         ************************************************/
        public void DeleteData()
        {
            Worker.DeleteData(Path);
        }
        /************************************************
         * FUNCTION:    Save
         * DESCRIPTION:
         ************************************************/

        public void Save(DeviceResponseValues response, ChannelValues limits = null)
        {
            if (!response.Response_Empty)
            {
                if (LogMeasDB_Active || LogMeasPath_Active)
                {
                    DoWork(new LogValues()
                    {
                        Response = response,
                        Channel_No = ChannelNo,
                        BeM = BeM,
                        Limits = limits
                    });
                }
            }
        }

        public void Save(gProcMain state, OpCode opcodeRequest, string response = null)
        {
            SaveLocal(state.ToString(), opcodeRequest.ToString(), response);
        }
        public void Save(string state, OpCode opcodeRequest, string response = null)
        {
            SaveLocal(state, opcodeRequest.ToString(), response);
        }

        public void Save(string state, string response)
        {
            SaveLocal(state, null, response);
        }

        private void SaveLocal(string state, string opcode, string response)
        {
            if (ParseMessage(state, opcode, response, out LogValues log))
            {
                SaveLogMeasFile(log, log.message);
            }
        }

        public void Save(Exception ex)
        {
            clLog item = new clLog(ex.Source.ToString().Trim() + " " + ex.Message.ToString().Trim(), Path, FileLenghtKB);
            Worker.Add(item);
            clLog item2 = new clLog("Stack: " + ex.StackTrace.ToString().Trim(), Path, FileLenghtKB);
            Worker.Add(item2);
        }

        /***************************************************************************************
        * BackGroundWorker Write Messages
        ****************************************************************************************/
        private void DoWork(LogValues log)
        {
            try
            {
                if (ParseMessage(log, log.Response, out string message))
                {
                    SaveLogMeas(log, message, log.Response);
                }
            }
            catch (Exception e)
            {
                ErrorHandler("BGW_MessageWriter_DoWork Executable", e);
            }
        }
        private Task DoWorkAsync(LogValues log)
        {
            return Task.Run(() =>
            {
                DoWork(log);
            });
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
            if (log.Response.Response_Empty == false)
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
            log = new LogValues() { Channel_No = ChannelNo, BeM = BeM };
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
            switch (opcode)
            {
                case null:
                case "":
                case "state":
                    break;
                case "cmdsend":
                    message += $"\tcmdSend:";
                    break;
                default:
                    message += $"\tOpcode: {opcode}";
                    break;
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
                clLog item = new clLog(message, Path, FileLenghtKB);
                Worker.Add(item);
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
