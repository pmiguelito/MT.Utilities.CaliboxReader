using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Processes(gProcMain process, OpCode send) : base(process, send)
        {

        }
        private Processes(gProcMain process, OpCode send, OpCode response) : base(process, send, response)
        {

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

        public Processes this[int index]
        {
            get { return ProcOrders[index]; }
        }

        public static Processes GetReadyForTest
        {
            get
            {
                return new Processes(gProcMain.getReadyForTest)
                {
                    IsCommand = false,
                    TimeOutActive = false,
                    WaitAfterActive = false,
                    AttendsActive = false,
                };
            }
        }

        public static Processes StartProcess
        {
            get
            {
                return new Processes(gProcMain.StartProcess)
                {
                    IsCommand = false,
                    TimeOutActive = false,
                    WaitAfterActive = false,
                    AttendsActive = false,
                };
            }
        }

        public static Processes BoxIdentification
        {
            get
            {
                return new Processes(gProcMain.BoxIdentification, OpCode.BoxIdentification, OpCode.rdbx)
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
            }
        }
        public static Processes BoxStatus
        {
            get
            {
                return new Processes(gProcMain.BoxStatus, OpCode.G100)
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
            }
        }
        public static Processes BoxReset
        {
            get
            {
                return new Processes(gProcMain.BoxReset, OpCode.S999)
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
            }
        }

        public static Processes Finished
        {
            get
            {
                return new Processes(gProcMain.TestFinished)
                {
                    TimeOutActive = true,
                    TimeOutDuration = 20000,
                    WaitBeforeActive = true,
                    WaitBeforeDuration = 2000,
                    WaitAfterActive = false,
                    AttendsActive = true,
                    AttendsMax = 3
                };
            }
        }

        public static Processes FWCheck
        {
            get
            {
                return new Processes(gProcMain.FWcheck, OpCode.G015)
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
            }
        }

        public static Processes SensorCheck
        {
            get
            {
                return new Processes(gProcMain.SensorCheck, OpCode.G015)
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
            }
        }

        public static Processes CalibrationStart
        {
            get
            {
                return new Processes(gProcMain.Calibration)
                {
                    TimeOutActive = false,
                    TimeOutDuration = 20000,
                    WaitBeforeActive = true,
                    WaitBeforeDuration = 2000,
                    WaitAfterActive = false,
                    AttendsActive = true,
                    AttendsMax = 3
                };
            }
        }
        public static Processes CalibrationWork
        {
            get
            {
                return new Processes(gProcMain.CalibrationWork)
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
            }
        }
        public static Processes Bewertung
        {
            get
            {
                return new Processes(gProcMain.Bewertung)
                {
                    TimeOutActive = true,
                    TimeOutDuration = 20000,
                    WaitAfterActive = false,
                    AttendsActive = true,
                    AttendsMax = 3
                };
            }
        }

        public static Processes DBinit
        {
            get
            {
                return new Processes(gProcMain.DBinit)
                {
                    TimeOutActive = true,
                    TimeOutDuration = 20000,
                    WaitAfterActive = false,
                    AttendsActive = true,
                    AttendsMax = 3
                };
            }
        }

        public static Dictionary<int, gProcMain> ProcOrder { get; private set; } = new Dictionary<int, gProcMain>()
        {
            {0, gProcMain.StartProcess },
            {1, gProcMain.BoxReset },
            {2, gProcMain.FWcheck },
            {3, gProcMain.BoxIdentification },
            {4, gProcMain.SensorCheck },

            {5, gProcMain.BoxReset },
            {6, gProcMain.FWcheck },
            {7, gProcMain.BoxStatus },

            {8, gProcMain.DBinit },
            {9, gProcMain.Calibration },
            {10, gProcMain.Bewertung },
        };

        public static int GetIndex(gProcMain proc)
        {
            foreach (var item in ProcOrder)
            {
                if (item.Value == proc)
                {
                    return item.Key;
                }
            }
            return -1;
        }
        public static int GetIndex(Processes proc)
        {
            foreach (var item in ProcOrders)
            {
                if (item.Value == proc)
                {
                    return item.Key;
                }
            }
            return -1;
        }
        public static Dictionary<int, Processes> ProcOrders { get; private set; } = new Dictionary<int, Processes>()
        {
            {0, StartProcess },
            {1, BoxReset },
            {2, FWCheck },
            {3, BoxIdentification },
            {4, SensorCheck },

            {5, BoxReset },
            {6, FWCheck },
            {7, BoxStatus },

            {8, DBinit },
            {9, CalibrationStart },
            {10, Bewertung },
        };

    }
}
