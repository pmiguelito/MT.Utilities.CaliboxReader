using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaliboxLibrary
{
    public enum CMDs
    {
        InitBox_S999,
        Set0nA_G907,
        Set175nA_G908,
        Set4700nA_G909,

        Calib_S100_6850i,
        Calib_S674,
        Calib_S500,

        GetBoxLimits,
        ShowBoxLimits,

        TempCheckNTC25_G911,
        TempCheckPT20_G915,
        TempCheckPT30_G916,

        CheckUpol_G910,
        SetUpol500_G913,
        SetUpol674_G914,

        ReadPage00,
        ReadPage01,
        ReadPage02,

        ReadConverterInfos,

        ActivateBoxLog,

        OpCode
    }

    public static class CMD
    {
        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:
        ***********************************************************/
        public static EventHandler<EventRoutingArgs> SendCMD;
        public static Task OnSendCMD(object sender, CmdDefinition routine)
        {
            return Task.Run(() =>
            {
                try
                {
                    SendCMD?.Invoke(sender, new EventRoutingArgs(routine));
                }
                catch
                {

                }
            });
        }

        /**********************************************************
        * FUNCTION:     Init
        * DESCRIPTION:
        ***********************************************************/

        public static CmdDefinition New_Init()
        {
            CmdDefinition init = new CmdDefinition(OpCode.init, wait: 1000)
            {
                Name = "init status",
                NextOnAnswer = true
            };
            return init;
        }

        private static CmdDefinition New_Init(OpCode opCode)
        {
            CmdDefinition init = new CmdDefinition(opCode, wait: 1000)
            {
                Name = "init status",
                NextOnAnswer = true
            };
            return init;
        }

        public static readonly CmdDefinition Init = New_Init(OpCode.Init);

        public static readonly CmdDefinition init = New_Init(OpCode.init);

        /**********************************************************
        * FUNCTION:     Get Commands
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// Get Box status
        /// </summary>
        /// <param name="wait"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static CmdDefinition New_G100(int wait = 2000, int retry = 3)
        {
            return new CmdDefinition(OpCode.G100, wait: wait, retry: retry)
            {
                Name = "[G100] Get BoxStatus",
                NextOnAnswer = true
            };
        }

        public static readonly CmdDefinition G100 = New_G100();

        /// <summary>
        /// Sensor Read Page 15
        /// </summary>
        /// <param name="wait"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static CmdDefinition New_G015(int wait = 2000, int retry = 3)
        {
            return new CmdDefinition(OpCode.G015, wait: wait, retry: retry)
            {
                Name = "[G015] Sample Page 15",
                NextOnAnswer = true
            };
        }

        public static readonly CmdDefinition G015 = New_G015();

        /// <summary>
        /// Box prepare for next calibration
        /// </summary>
        /// <param name="wait"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static CmdDefinition New_G200(int wait = 20000)
        {
            return new CmdDefinition(OpCode.G200, wait: wait)
            {
                Name = "[G200] Get Error values",
                NextOnAnswer = true,
            };
        }

        public static readonly CmdDefinition G200 = New_G200();

        /// <summary>
        /// Enable print out Mean/StdDev
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G901(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G901, wait: wait)
            {
                Name = "[G901] Enable print out Mean/StdDev"
            };
        }

        public static readonly CmdDefinition G901 = New_G901();

        /// <summary>
        /// Enable print out Measurement per second
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G902(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G902, wait: wait)
            {
                Name = "[G902] Enable print out Measurement per second"
            };
        }
        public static readonly CmdDefinition G902 = New_G902();

        /// <summary>
        /// Measures Pol. Voltage Cathode/GND
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G904(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G904, wait: wait)
            {
                Name = "[G904] Measures Pol. Voltage Cathode/GND"
            };
        }
        public static readonly CmdDefinition G904 = New_G904();


        /// <summary>
        /// Measures Pol. Voltage Anode/GND
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G905(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G905, wait: wait)
            {
                Name = "[G905] Measures Pol. Voltage Anode/GND"
            };
        }
        public static readonly CmdDefinition G905 = New_G905();

        /// <summary>
        /// Sensor Page 16 incl. Decode
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G906(int wait = 1000, int retry = 10)
        {
            return new CmdDefinition(OpCode.G906, wait: wait, retry: retry)
            {
                Name = "[G906] Sensor Page 16 incl. Decode"
            };
        }

        public static readonly CmdDefinition G906 = New_G906();

        /// <summary>
        /// Set 0nA
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G907(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G907, wait: wait)
            {
                Name = "[G907] Set 0nA"
            };
        }
        public static readonly CmdDefinition G907 = New_G907();

        /// <summary>
        /// Set 175nA
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G908(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G908, wait: wait)
            {
                Name = "[G908] Set 175nA"
            };
        }

        public static readonly CmdDefinition G908 = New_G908();

        /// <summary>
        /// Set 4700nA
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G909(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G909, wait: wait)
            {
                Name = "[G909] Set 4700nA"
            };
        }

        public static readonly CmdDefinition G909 = New_G909();

        /// <summary>
        /// UPol Check
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G910(int wait = 16000)
        {
            return new CmdDefinition(OpCode.G910, wait: wait)
            {
                Name = "[G910] UPol. Check"
            };
        }

        public static readonly CmdDefinition G910 = New_G910();

        /// <summary>
        /// Temp. Check NTC 25°C
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G911(int wait = 3000)
        {
            return new CmdDefinition(OpCode.G911, wait: wait)
            {
                Name = "[G911] Temp. Check NTC 25°C"
            };
        }

        public static readonly CmdDefinition G911 = New_G911();

        /// <summary>
        /// Set UPol. 500mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G913(int wait = 16000)
        {
            return new CmdDefinition(OpCode.G913, wait: wait)
            {
                Name = "[G913] Set UPol. 500mV"
            };
        }
        public static readonly CmdDefinition G913 = New_G913();

        /// <summary>
        /// Set UPol. 674mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G914(int wait = 16000)
        {
            return new CmdDefinition(OpCode.G914, wait: wait)
            {
                Name = "[G914] Set UPol. 674mV"
            };
        }

        public static readonly CmdDefinition G914 = New_G914();

        /// <summary>
        /// Temp. Check PT 20°C
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G915(int wait = 3000)
        {
            return new CmdDefinition(OpCode.G915, wait: wait)
            {
                Name = "[G915] Temp. Check PT 20°C"
            };
        }

        public static readonly CmdDefinition G915 = New_G915();

        /// <summary>
        /// Temp. Check PT 30°C
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G916(int wait = 3000)
        {
            return new CmdDefinition(OpCode.G916, wait: wait)
            {
                Name = "[G916] Temp. Check PT 30°C"
            };
        }
        public static readonly CmdDefinition G916 = New_G916();

        public static readonly CmdDefinition RDBX00
            = new CmdDefinition(OpCode.RDBX, cmdAdd: "00", wait: 1000)
            {
                Name = $"[#RDBX] Read Box page 00"
            };

        public static readonly CmdDefinition RDBX10
            = new CmdDefinition(OpCode.RDBX, cmdAdd: "10", wait: 1000)
            {
                Name = $"[#RDBX] Read Box page 10"
            };

        public static readonly CmdDefinition RDBX31
            = new CmdDefinition(OpCode.RDBX, cmdAdd: "31", wait: 1000)
            {
                Name = $"[#RDBX] Read Box page 31"
            };

        /// <summary>
        /// Read Box values using page numbers
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_ReadPageBox(int page, int wait = 2000)
        {
            var result = new CmdDefinition(OpCode.RDBX, cmdAdd: page.ToString(), wait: wait)
            {
                Name = $"[#RDBX] Read Box page {page:00}"
            };
            return result;
        }

        public static readonly CmdDefinition RDPG00
            = new CmdDefinition(OpCode.RDPG, cmdAdd: "00", wait: 1000)
            {
                Name = $"[#RDPG] Read Sensor page 00"
            };

        public static readonly CmdDefinition WRPG00
            = new CmdDefinition(OpCode.WRPG, cmdAdd: "00", wait: 1000)
            {
                Name = $"[#WRPG] Write Sensor page 00"
            };

        public static readonly CmdDefinition RDPG01
            = new CmdDefinition(OpCode.RDPG, cmdAdd: "01", wait: 1000)
            {
                Name = $"[#RDPG] Read Sensor page 01"
            };

        public static readonly CmdDefinition WRPG01
            = new CmdDefinition(OpCode.WRPG, cmdAdd: "01", wait: 1000)
            {
                Name = $"[#WRPG] Write Sensor page 01"
            };

        public static readonly CmdDefinition RDPG02
            = new CmdDefinition(OpCode.RDPG, cmdAdd: "02", wait: 1000)
            {
                Name = $"[#RDPG] Read Sensor page 02"
            };

        public static readonly CmdDefinition WRPG02
            = new CmdDefinition(OpCode.WRPG, cmdAdd: "02", wait: 1000)
            {
                Name = $"[#WRPG] Write Sensor page 02"
            };

        /// <summary>
        /// Read Sensor values using page numbers
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_ReadPageSensor(int page, int wait = 2000)
        {
            var result = new CmdDefinition(OpCode.RDPG, cmdAdd: page.ToString(), wait: wait)
            {
                Name = $"[#RDPG] Read Sensor page {page:00}"
            };
            return result;
        }

        /**********************************************************
        * FUNCTION:     Set Commands
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// Box prepare for next calibration
        /// </summary>
        /// <param name="wait"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static CmdDefinition New_S200(int wait = 20000)
        {
            return new CmdDefinition(OpCode.S200, wait: wait)
            {
                Name = "[S200] Prepare for next calibration"
            };
        }

        public static readonly CmdDefinition S100
            = new CmdDefinition(OpCode.S100, wait: 3000)
            {
                Name = "[S100] Start Calib. UPol: 500mV and 674mV"
            };

        /// <summary>
        /// Start Calibration UPol: 500mV and 674mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_S100(int wait = 20000)
        {
            return new CmdDefinition(OpCode.S100, wait: wait)
            {
                Name = S100.Name
            };
        }
        public static readonly CmdDefinition S500
            = new CmdDefinition(OpCode.S500, wait: 3000)
            {
                Name = "[S500] Start Calib. UPol: 500mV"
            };

        /// <summary>
        /// Start Calibration UPol: 500mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_S500(int wait = 20000)
        {
            return new CmdDefinition(OpCode.S500, wait: wait)
            {
                Name = S500.Name
            };
        }

        public static readonly CmdDefinition S674
            = new CmdDefinition(OpCode.S674, wait: 3000)
            {
                Name = "[S674] Start Calib. UPol: 674mV"
            };

        /// <summary>
        /// Start Calibration UPol: 674mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_S674(int wait = 20000)
        {
            return new CmdDefinition(OpCode.S674, wait: wait)
            {
                Name = S674.Name
            };
        }

        /// <summary>
        /// Initialization CaliBox
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_S999(int wait = 5000)
        {
            if (_S999 != null)
            {
                return _S999;
            }

            return new CmdDefinition(OpCode.S999, wait: wait)
            {
                Name = "[S999] InitBox",
                NextOnAnswer = true
            };
        }
        private static CmdDefinition _S999;
        public static readonly CmdDefinition S999 = New_S999();

        /**********************************************************
        * FUNCTION:     CMD Sequences
        * DESCRIPTION:
        ***********************************************************/
        public static CmdSequence New_InitBox()
        {
            //var result = new CmdSequence(CMDs.Calib_S100_6850i)
            var result = new CmdSequence(CMDs.InitBox_S999)
            {
                Description = "[S999] + [G015] + [G901] InitBox",
                //Description = "[S999] + [G015] + [G906] + [G901] InitBox",
                Routing = new List<CmdDefinition>()
                    {
                        New_S999(),
                        New_G015(),
                        New_G901()
                    }
            };
            return result;
        }
        public static CmdSequence New_Set0nA(int measQuantity = 10)
        {
            var result = new CmdSequence(CMDs.Set0nA_G907)
            {
                Description = "[G907] + [G906] Set 0nA",
                Routing = new List<CmdDefinition>()
                    {
                        New_G907(),
                        New_G906(wait: 1000, retry: measQuantity)
                    }
            };
            return result;
        }

        public static CmdSequence New_Set175nA(int measQuantity = 10)
        {
            var result = new CmdSequence(CMDs.Set175nA_G908)
            {
                Description = "[G908] + [G906] Set 175nA",
                Routing = new List<CmdDefinition>()
                    {
                        New_G908(),
                        New_G906(wait: 1000, retry: measQuantity)
                    }
            };
            return result;
        }
        public static CmdSequence New_Set4700nA(int measQuantity = 10)
        {
            var result = new CmdSequence(CMDs.Set4700nA_G909)
            {
                Description = "[G909] + [G906] Set 4700nA",
                Routing = new List<CmdDefinition>()
                    {
                        New_G909(),
                        New_G906(wait: 1000, retry: measQuantity)
                    }
            };
            return result;
        }

        /// <summary>
        /// [#RDBX 10] + [#RDBX 31]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_GetBoxLimits()
        {
            var result = new CmdSequence(CMDs.GetBoxLimits)
            {
                Description = "[#RDBX 10] + [#RDBX 31] Read Box limits",
                Routing = new List<CmdDefinition>()
                    {
                        New_ReadPageBox(10),
                        New_ReadPageBox(31)
                    }
            };
            return result;
        }

        /**********************************************************
        * FUNCTION:     Complete Calibration
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// G100 + S100
        /// </summary>
        /// <param name="waitStart"></param>
        /// <returns></returns>
        public static CmdSequence New_Calib_500And674(int waitStart = 20000)
        {
            var result = new CmdSequence(CMDs.Calib_S100_6850i)
            {
                Description = "[G100] + [S100] + [G901] Start Calib. UPol: 500mV and 674mV",
                Routing = new List<CmdDefinition>()
                    {
                        //New_G100(),
                        New_S100(wait: waitStart),
                        //New_G901(),
                    }
            };
            return result;
        }

        /// <summary>
        /// G100 + S500
        /// </summary>
        /// <param name="waitStart"></param>
        /// <returns></returns>
        public static CmdSequence New_Calib_500(int waitStart = 20000)
        {
            var result = new CmdSequence(CMDs.Calib_S500)
            {
                Description = "[G100] + [S500] Start Calib. UPol: 500mV",
                Routing = new List<CmdDefinition>()
                    {
                        New_G100(),
                        New_S500(wait: waitStart)
                    }
            };
            return result;
        }

        /// <summary>
        /// G100 + S100
        /// </summary>
        /// <param name="waitStart"></param>
        /// <returns></returns>
        public static CmdSequence New_Calib_674(int waitStart = 20000)
        {
            var result = new CmdSequence(CMDs.Calib_S674)
            {
                Description = "[G100] + [S100] Start Calib. UPol: 674mV",
                Routing = new List<CmdDefinition>()
                    {
                        New_G100(),
                        New_S674(wait: waitStart)
                    }
            };
            return result;
        }

        /**********************************************************
        * FUNCTION:     Temperature Check
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// [G911]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_TempCheck_NTC25()
        {
            var result = new CmdSequence(CMDs.TempCheckNTC25_G911)
            {
                Description = "[G911] Check Temp NTC 25°C",
                Routing = new List<CmdDefinition>()
                    {
                        New_G911(wait: 14000),
                    }
            };
            //AddG906(result, 15);
            return result;
        }

        /// <summary>
        /// [G915]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_TempCheck_PT20()
        {
            var result = new CmdSequence(CMDs.TempCheckPT20_G915)
            {
                Description = "[G915] Check Temp PT 20°C",
                Routing = new List<CmdDefinition>()
                    {
                        New_G915(wait: 14000),
                    }
            };
            //AddG906(result, 15);
            return result;
        }

        /// <summary>
        /// [G916]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_TempCheck_PT30()
        {
            var result = new CmdSequence(CMDs.TempCheckPT30_G916)
            {
                Description = "[G916] Check Temp PT 30°C",
                Routing = new List<CmdDefinition>()
                    {
                        New_G916(wait: 14000),
                    }
            };
            //AddG906(result, 15);
            return result;
        }

        /**********************************************************
        * FUNCTION:     Polarization
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// [G910]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_PolarizationCheck()
        {
            var result = new CmdSequence(CMDs.CheckUpol_G910)
            {
                Description = "[G910] Polarization Check",
                Routing = new List<CmdDefinition>()
                    {
                        New_G910(wait: 16000),
                    }
            };
            AddG906(result, 10);
            return result;
        }

        /// <summary>
        /// [G913] + [906]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_PolarizationSet_500()
        {
            var result = new CmdSequence(CMDs.SetUpol500_G913)
            {
                Description = "[G913] + [906] Polarization 500mV",
                Routing = new List<CmdDefinition>()
                    {
                        New_G913(wait: 16000),
                    }
            };
            AddG906(result, 10);
            return result;
        }
        private static void AddG906(CmdSequence cmd, int count)
        {
            for (int i = 0; i < count; i++)
            {
                cmd.Routing.Add(New_G906(wait: 1000, retry: 1));
            }
        }
        /// <summary>
        /// [G914] + [906]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_PolarizationSet_674()
        {
            var result = new CmdSequence(CMDs.SetUpol674_G914)
            {
                Description = "[G914] + [906] Polarization 674mV",
                Routing = new List<CmdDefinition>()
                    {
                        New_G914(wait: 16000),
                    }
            };
            AddG906(result, 10);
            return result;
        }

        /// <summary>
        /// [G914] + [906]
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_ActivateBoxLog()
        {
            var result = new CmdSequence(CMDs.ActivateBoxLog)
            {
                Description = "[G906] + [901] Activate Box Log",
                Routing = new List<CmdDefinition>()
                    {
                        New_G906(wait: 1000, retry: 0),
                        New_G901(wait: 1000)
                    }
            };
            return result;
        }

        /**********************************************************
        * FUNCTION:     Read Sensor
        * DESCRIPTION:
        ***********************************************************/
        /// <summary>
        /// [#RDPG] Read Sensor Page 00
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_ReadSensorPage00()
        {
            var result = new CmdSequence(CMDs.ReadPage00)
            {
                Description = $"[#RDPG] Read Sensor page 00",
                Routing = new List<CmdDefinition>()
                    {
                        New_ReadPageSensor(page: 0),
                    }
            };
            return result;
        }

        /// <summary>
        /// [#RDPG] Read Sensor Page 01
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_ReadSensorPage01()
        {
            var result = new CmdSequence(CMDs.ReadPage01)
            {
                Description = $"[#RDPG] Read Sensor page 01",
                Routing = new List<CmdDefinition>()
                    {
                        New_ReadPageSensor(page: 1),
                    }
            };
            return result;
        }

        /// <summary>
        /// [#RDPG] Read Sensor Page 02
        /// </summary>
        /// <returns></returns>
        public static CmdSequence New_ReadSensorPage02()
        {
            var result = new CmdSequence(CMDs.ReadPage02)
            {
                Description = $"[#RDPG] Read Sensor page 02",
                Routing = new List<CmdDefinition>()
                    {
                        New_ReadPageSensor(page: 2),
                    }
            };
            return result;
        }

        public static CmdSequence New_ReadConverterInfos()
        {
            var result = new CmdSequence(CMDs.ReadPage02)
            {
                Description = $"[#RDPG 00]+[#RDPG 01]+[#RDPG 02] Read Converter Infos",
                Routing = new List<CmdDefinition>()
                    {
                        New_ReadPageSensor(0, wait: 1000),
                        New_ReadPageSensor(1, wait: 1000),
                        New_ReadPageSensor(2, wait: 0),
                    }
            };
            return result;
        }

        /**********************************************************
        * FUNCTION:     CMDs
        * DESCRIPTION:
        ***********************************************************/
        public static readonly Dictionary<string, CmdDefinition> CmdDefinitions = CreateDictionary();

        private static Dictionary<string, CmdDefinition> CreateDictionary()
        {
            var cmds = new Dictionary<string, CmdDefinition>()
            {
                { Init.CommandText, Init },
                { init.CommandText, init },

                { G100.CommandText, G100 },
                { G015.CommandText, G015 },
                { G200.CommandText, G200 },
                { G901.CommandText, G901 },
                { G902.CommandText, G902 },
                { G904.CommandText, G904 },
                { G905.CommandText, G905 },
                { G906.CommandText, G906 },
                { G907.CommandText, G907 },
                { G908.CommandText, G908 },
                { G909.CommandText, G909 },
                { G910.CommandText, G910 },
                { G911.CommandText, G911 },
                { G913.CommandText, G913 },
                { G914.CommandText, G914 },
                { G915.CommandText, G915 },
                { G916.CommandText, G916 },

                { S100.CommandText, S100 },
                { S500.CommandText, S500 },
                { S674.CommandText, S674 },
                { S999.CommandText, S999 },

                { RDPG00.CommandText, RDPG00 },
                { RDPG01.CommandText, RDPG01 },
                { RDPG02.CommandText, RDPG02 },

                { WRPG00.CommandText, WRPG00 },
                { WRPG01.CommandText, WRPG01 },
                { WRPG02.CommandText, WRPG02 },

                { RDBX00.CommandText, RDBX00 },
                { RDBX10.CommandText, RDBX10 },
                { RDBX31.CommandText, RDBX31 },
            };
            return cmds;
        }

        public static string CreateCommandText(OpCode opCode, string add, out string data)
        {
            string result = opCode.ToString();
            data = null;
            switch (opCode)
            {
                case OpCode.RDBX:
                case OpCode.rdbx:
                case OpCode.WRBX:
                case OpCode.wrbx:
                case OpCode.RDPG:
                case OpCode.rdpg:
                case OpCode.WRPG:
                case OpCode.wrpg:
                    var split = add.Split(' ');
                    result = $"#{opCode} {split[0].PadLeft(2, '0')}";
                    if (split.Length > 1)
                    {
                        data = split[1];
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static CmdDefinition GetOrAdd(OpCode opCode, string add = null)
        {
            var cmdText = CreateCommandText(opCode, add, out string data);
            if (CmdDefinitions.TryGetValue(cmdText, out var cmd) == false)
            {
                cmd = new CmdDefinition(opCode, add);
                CmdDefinitions.Add(cmd.CommandText, cmd);
            }
            cmd.Data = data;
            return cmd;
        }

    }
}
