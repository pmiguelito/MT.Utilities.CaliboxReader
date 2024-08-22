using STDhelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CaliboxLibrary
{
    public class LoggerWorker
    {
        public LoggerWorker()
        {
            InitTimer();
        }

        /************************************************
         * FUNCTION:    Configuration
         * DESCRIPTION:
         ************************************************/
        public DateTime FlushedTime { get; private set; }
        /************************************************
         * FUNCTION:    Data Container
         * DESCRIPTION:
         ************************************************/
        private Queue<clLog> LogQueue { get; set; } = new Queue<clLog>();
        private object _LockAdd = new object();
        public void Add(clLog log)
        {
            lock (_LockAdd)
            {
                LogQueue.Enqueue(log);
            }
        }

        public void DeleteData(string path)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
                {
                }
            }
            catch
            {
            }
        }
        /************************************************
         * FUNCTION:    Timer
         * DESCRIPTION:
         ************************************************/
        private System.Timers.Timer _TimerFlusher;
        private void InitTimer()
        {
            FlushedTime = DateTime.Now;
            if (_TimerFlusher != null)
            {
                _TimerFlusher.Elapsed -= TimerFlusher_Elapsed;
                _TimerFlusher.Stop();
                _TimerFlusher.Dispose();
            }
            _TimerFlusher = new System.Timers.Timer
            {
                Interval = 1000
            };
            _TimerFlusher.Elapsed += TimerFlusher_Elapsed;
            _TimerFlusher.Start();
        }

        private void TimerFlusher_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (CheckTimeToFlush())
            {
                Write();
            }
        }
        private bool CheckTimeToFlush()
        {
            if (_IsWriting)
            {
                return false;
            }
            return true;
            //if ((DateTime.Now - FlushedTime).TotalSeconds >= FlushAtAge_Sec)
            //{
            //    FlushedTime = DateTime.Now;
            //    return true;
            //}
            //return false;
        }

        /************************************************
         * FUNCTION:    Write
         * DESCRIPTION:
         ************************************************/
        private bool _IsWriting;
        private object _LockFlushWrite = new object();
        private void Write()
        {
            lock (_LockFlushWrite)
            {
                if (_IsWriting)
                {
                    return;
                }
                _IsWriting = true;
                FlushedTime = DateTime.Now;
            }
            try
            {
                while (LogQueue.Count > 0)
                {
                    clLog clLog2 = LogQueue.Dequeue();

                    int num = 0;
                    try
                    {
                        using (FileStream fileStream = new FileStream(clLog2.GetLogPath(), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                streamWriter.WriteLine(clLog2.GetDate() + "\t" + clLog2.GetTime() + "\t" + clLog2.GetMessage().TrimStart());
                            }
                        }
                        //Task.Delay(50).Wait();
                    }
                    catch
                    {
                        Add(clLog2);
                        num++;
                        if (num < 11)
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            finally
            {
                lock (_LockFlushWrite)
                {
                    _IsWriting = false;
                }
            }
        }
    }
}
