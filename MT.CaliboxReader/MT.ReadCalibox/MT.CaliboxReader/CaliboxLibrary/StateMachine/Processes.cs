using CaliboxLibrary.BoxCommunication.CMDs;
using System;
using System.Collections.Generic;

namespace CaliboxLibrary.StateMachine
{
    public enum ProcState
    {
        Idle,
        Running,
        SendCMD,
        Waiting,
        Aborted,
        Finished,
        Terminated
    }

    public class Processes : ProcDetail
    {
        private Processes(gProcMain process) : base(process)
        {

        }
        private Processes(gProcMain process, CmdSend send) : base(process, send)
        {

        }

        public Processes this[int index]
        {
            get { return ProcOrders[index]; }
        }

        public Processes this[gProcMain current]
        {
            get { return Processe[current]; }
        }

        #region Events
        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:
        ***********************************************************/
        public static event EventHandler<Processes> Changed;
        internal static void OnChanged(object sender)
        {
            Changed?.Invoke(sender, (Processes)sender);
        }
        #endregion

        public static readonly Processes Idle
            = new Processes(gProcMain.idle)
            {
                IsCommand = false,
                TimeOutActive = false,
                WaitAfterActive = false,
                AttendsActive = false,
            };

        public static readonly Processes GetReadyForTest
            = new Processes(gProcMain.getReadyForTest)
            {
                IsCommand = false,
                TimeOutActive = false,
                WaitAfterActive = false,
                AttendsActive = false,
            };

        public static readonly Processes StartProcess
            = new Processes(gProcMain.StartProcess)
            {
                IsCommand = false,
                TimeOutActive = false,
                WaitAfterActive = false,
                AttendsActive = false,
            };

        public static readonly Processes BoxIdentification
        = new Processes(gProcMain.BoxIdentification, new CmdSend(OpCode.BoxIdentification))
        {
            TimeOutActive = false,
            TimeOutDuration = 20000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = true,
            WaitAfterDuration = 4000,
            WaitToEnd = true,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes BoxStatus
        = new Processes(gProcMain.BoxStatus, new CmdSend(OpCode.G100))
        {
            TimeOutActive = false,
            TimeOutDuration = 20000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = true,
            WaitAfterDuration = 2000,
            WaitToEnd = true,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes BoxReset
        = new Processes(gProcMain.BoxReset, new CmdSend(OpCode.S999))
        {
            TimeOutActive = false,
            TimeOutDuration = 60000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = true,
            WaitAfterDuration = 4000,
            WaitToEnd = true,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes Finished
        = new Processes(gProcMain.TestFinished)
        {
            TimeOutActive = true,
            TimeOutDuration = 20000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = false,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes FWCheck
        = new Processes(gProcMain.FWcheck, new CmdSend(OpCode.G015))
        {
            TimeOutActive = true,
            TimeOutDuration = 20000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = true,
            WaitAfterDuration = 2000,
            WaitToEnd = true,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes SensorCheck
        = new Processes(gProcMain.SensorCheck, new CmdSend(OpCode.G015))
        {
            TimeOutActive = true,
            TimeOutDuration = 20000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = true,
            WaitAfterDuration = 2000,
            WaitToEnd = true,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes CalibrationStart
        = new Processes(gProcMain.Calibration)
        {
            TimeOutActive = false,
            TimeOutDuration = 20000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = false,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes CalibrationWork
        = new Processes(gProcMain.CalibrationWork)
        {
            TimeOutActive = true,
            TimeOutDuration = 1800000,
            WaitBeforeActive = true,
            WaitBeforeDuration = 2000,
            WaitAfterActive = false,
            WaitAfterDuration = 1800000,
            WaitToEnd = true,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes Evaluation
        = new Processes(gProcMain.Bewertung)
        {
            TimeOutActive = true,
            TimeOutDuration = 20000,
            WaitAfterActive = false,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static readonly Processes DBinit
        = new Processes(gProcMain.DBinit)
        {
            TimeOutActive = true,
            TimeOutDuration = 20000,
            WaitAfterActive = false,
            AttendsActive = true,
            AttendsMax = 3
        };

        public static Dictionary<gProcMain, Processes> Processe { get; }
            = new Dictionary<gProcMain, Processes>()
            {
                {GetReadyForTest.ProcName, GetReadyForTest },
                {StartProcess.ProcName, StartProcess },
                {BoxIdentification.ProcName, BoxIdentification },
                {BoxStatus.ProcName, BoxStatus },
                {BoxReset.ProcName, BoxReset },
                {Finished.ProcName, Finished },
                {FWCheck.ProcName, FWCheck },
                {SensorCheck.ProcName, SensorCheck },
                {CalibrationStart.ProcName, CalibrationStart },
                {CalibrationWork.ProcName, CalibrationWork },
                {Evaluation.ProcName, Evaluation },
                {DBinit.ProcName, DBinit },
            };

        public static bool TryGetValue(gProcMain gProc, out Processes processes)
        {
            return Processe.TryGetValue(gProc, out processes);
        }

        public static Processes GetProcess(int index)
        {
            if (index < ProcOrders.Count)
            {
                var result = ProcOrders[index];
                return result;
            }
            return Idle;
        }

        public static Processes GetNext(int current)
        {
            var next = current + 1;
            return GetProcess(next);
        }

        //public static Processes GetNext(Processes current)
        //{
        //    bool found = false;
        //    foreach (var item in ProcOrders)
        //    {
        //        if (found)
        //        {
        //            return item.Value;
        //        }
        //        if (item.Value == current)
        //        {
        //            found = true;
        //        }
        //    }
        //    return Idle;
        //}
        //public static Processes GetNext(gProcMain current)
        //{
        //    bool found = false;
        //    foreach (var item in ProcOrders)
        //    {
        //        if (found)
        //        {
        //            return item.Value;
        //        }
        //        if (item.Value.ProcName == current)
        //        {
        //            found = true;
        //        }
        //    }
        //    return Idle;
        //}

        public static Dictionary<int, Processes> ProcOrders { get; private set; }
            = new Dictionary<int, Processes>()
        {
            {0, StartProcess },
            {1, BoxReset },

            /* FWcheck needed to start communication */
            {2, BoxStatus },
            {3, FWCheck },
            {4, SensorCheck },
            {5, BoxIdentification },

            {6, BoxReset },
            {7, FWCheck },
            //{7, BoxStatus },

            //{8, DBinit },
            //{9, CalibrationStart },
            //{10, Evaluation },

            {8, DBinit },
            {9, CalibrationStart },
            {10, Evaluation },
        };
    }
}
