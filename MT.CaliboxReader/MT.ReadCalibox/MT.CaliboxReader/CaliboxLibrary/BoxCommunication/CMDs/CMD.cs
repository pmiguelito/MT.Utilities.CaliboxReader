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

        public static readonly CmdDefinition Init
            = new CmdDefinition(OpCode.Init, wait: 1000)
            {
                Description = "Init status",
                NextOnAnswer = true
            };

        public static readonly CmdDefinition init
            = new CmdDefinition(OpCode.init, wait: 1000)
            {
                Description = "init status",
                NextOnAnswer = true
            };

        /**********************************************************
        * FUNCTION:     Get Commands
        * DESCRIPTION:
        ***********************************************************/
        public static readonly CmdDefinition G100
            = new CmdDefinition(OpCode.G100, wait: 2000, retry: 3)
            {
                Description = "[G100] Get BoxStatus",
                NextOnAnswer = true
            };

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
                Description = G100.Description,
                NextOnAnswer = true
            };
        }

        public static readonly CmdDefinition G015
            = new CmdDefinition(OpCode.G015, wait: 2000, retry: 3)
            {
                Description = "[G015] Sample Page 15",
                NextOnAnswer = true
            };

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
                Description = G015.Description,
                NextOnAnswer = true
            };
        }

        public static readonly CmdDefinition G200
            = new CmdDefinition(OpCode.G200, wait: 2000, retry: 3)
            {
                Description = "[G200] Get Error values"
            };

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
                Description = G200.Description
            };
        }

        public static readonly CmdDefinition G901
            = new CmdDefinition(OpCode.G901, wait: 1000)
            {
                Description = "[G901] Enable print out Mean/StdDev"
            };

        /// <summary>
        /// Enable print out Mean/StdDev
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G901(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G901, wait: wait)
            {
                Description = G901.Description
            };
        }

        public static readonly CmdDefinition G902
            = new CmdDefinition(OpCode.G902, wait: 1000)
            {
                Description = "[G902] Enable print out Measurement per second"
            };

        /// <summary>
        /// Enable print out Measurement per second
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G902(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G902, wait: wait)
            {
                Description = G902.Description
            };
        }

        public static readonly CmdDefinition G904
            = new CmdDefinition(OpCode.G904, wait: 1000)
            {
                Description = "[G904] Measures Pol. Voltage Cathode/GND"
            };

        /// <summary>
        /// Measures Pol. Voltage Cathode/GND
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G904(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G904, wait: wait)
            {
                Description = G904.Description
            };
        }

        public static readonly CmdDefinition G905
            = new CmdDefinition(OpCode.G905, wait: 1000)
            {
                Description = "[G905] Measures Pol. Voltage Anode/GND"
            };

        /// <summary>
        /// Measures Pol. Voltage Anode/GND
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G905(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G905, wait: wait)
            {
                Description = G905.Description
            };
        }

        public static readonly CmdDefinition G906
            = new CmdDefinition(OpCode.G906, wait: 1000)
            {
                Description = "[G906] Sensor Page 16 incl. Decode"
            };

        /// <summary>
        /// Sensor Page 16 incl. Decode
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G906(int wait = 1000, int retry = 10)
        {
            return new CmdDefinition(OpCode.G906, wait: wait, retry: retry)
            {
                Description = G906.Description
            };
        }

        public static readonly CmdDefinition G907
            = new CmdDefinition(OpCode.G907, wait: 1000)
            {
                Description = "[G907] Set 0nA"
            };

        /// <summary>
        /// Set 0nA
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G907(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G907, wait: wait)
            {
                Description = G907.Description
            };
        }

        public static readonly CmdDefinition G908
            = new CmdDefinition(OpCode.G908, wait: 1000)
            {
                Description = "[G908] Set 175nA"
            };

        /// <summary>
        /// Set 175nA
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G908(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G908, wait: wait)
            {
                Description = G908.Description
            };
        }

        public static readonly CmdDefinition G909
            = new CmdDefinition(OpCode.G909, wait: 1000)
            {
                Description = "[G909] Set 4700nA"
            };

        /// <summary>
        /// Set 4700nA
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G909(int wait = 1000)
        {
            return new CmdDefinition(OpCode.G909, wait: wait)
            {
                Description = G909.Description
            };
        }

        public static readonly CmdDefinition G910
            = new CmdDefinition(OpCode.G910, wait: 3000)
            {
                Description = "[G910] UPol. Check"
            };

        /// <summary>
        /// UPol Check
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G910(int wait = 16000)
        {
            return new CmdDefinition(OpCode.G910, wait: wait)
            {
                Description = G910.Description
            };
        }

        public static readonly CmdDefinition G911
            = new CmdDefinition(OpCode.G911, wait: 3000)
            {
                Description = "[G911] Temp. Check NTC 25°C"
            };

        /// <summary>
        /// Temp. Check NTC 25°C
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G911(int wait = 3000)
        {
            return new CmdDefinition(OpCode.G911, wait: wait)
            {
                Description = G911.Description
            };
        }

        public static readonly CmdDefinition G913
            = new CmdDefinition(OpCode.G913, wait: 3000)
            {
                Description = "[G913] Set UPol. 500mV"
            };

        /// <summary>
        /// Set UPol. 500mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G913(int wait = 16000)
        {
            return new CmdDefinition(OpCode.G913, wait: wait)
            {
                Description = G913.Description
            };
        }

        public static readonly CmdDefinition G914
            = new CmdDefinition(OpCode.G914, wait: 3000)
            {
                Description = "[G914] Set UPol. 674mV"
            };

        /// <summary>
        /// Set UPol. 674mV
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G914(int wait = 16000)
        {
            return new CmdDefinition(OpCode.G914, wait: wait)
            {
                Description = G914.Description
            };
        }

        public static readonly CmdDefinition G915
            = new CmdDefinition(OpCode.G915, wait: 3000)
            {
                Description = "[G915] Temp. Check PT 20°C"
            };

        /// <summary>
        /// Temp. Check PT 20°C
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G915(int wait = 3000)
        {
            return new CmdDefinition(OpCode.G915, wait: wait)
            {
                Description = G915.Description
            };
        }

        public static readonly CmdDefinition G916
            = new CmdDefinition(OpCode.G916, wait: 3000)
            {
                Description = "[G916] Temp. Check PT 30°C"
            };

        /// <summary>
        /// Temp. Check PT 30°C
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_G916(int wait = 3000)
        {
            return new CmdDefinition(OpCode.G916, wait: wait)
            {
                Description = G916.Description
            };
        }

        public static readonly CmdDefinition RDBX00
            = new CmdDefinition(OpCode.RDBX, cmdAdd: "00", wait: 1000)
            {
                Description = $"[#RDBX] Read Box page 00"
            };

        public static readonly CmdDefinition RDBX10
            = new CmdDefinition(OpCode.RDBX, cmdAdd: "10", wait: 1000)
            {
                Description = $"[#RDBX] Read Box page 10"
            };

        public static readonly CmdDefinition RDBX31
            = new CmdDefinition(OpCode.RDBX, cmdAdd: "31", wait: 1000)
            {
                Description = $"[#RDBX] Read Box page 31"
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
                Description = $"[#RDBX] Read Box page {page:00}"
            };
            return result;
        }

        public static readonly CmdDefinition RDPG00
            = new CmdDefinition(OpCode.RDPG, cmdAdd: "00", wait: 1000)
            {
                Description = $"[#RDPG] Read Sensor page 00"
            };

        public static readonly CmdDefinition WRPG00
            = new CmdDefinition(OpCode.WRPG, cmdAdd: "00", wait: 1000)
            {
                Description = $"[#WRPG] Write Sensor page 00"
            };

        public static readonly CmdDefinition RDPG01
            = new CmdDefinition(OpCode.RDPG, cmdAdd: "01", wait: 1000)
            {
                Description = $"[#RDPG] Read Sensor page 01"
            };

        public static readonly CmdDefinition WRPG01
            = new CmdDefinition(OpCode.WRPG, cmdAdd: "01", wait: 1000)
            {
                Description = $"[#WRPG] Write Sensor page 01"
            };

        public static readonly CmdDefinition RDPG02
            = new CmdDefinition(OpCode.RDPG, cmdAdd: "02", wait: 1000)
            {
                Description = $"[#RDPG] Read Sensor page 02"
            };

        public static readonly CmdDefinition WRPG02
            = new CmdDefinition(OpCode.WRPG, cmdAdd: "02", wait: 1000)
            {
                Description = $"[#WRPG] Write Sensor page 02"
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
                Description = $"[#RDPG] Read Sensor page {page:00}"
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
                Description = "[S200] Prepare for next calibration"
            };
        }

        public static readonly CmdDefinition S100
            = new CmdDefinition(OpCode.S100, wait: 3000)
            {
                Description = "[S100] Start Calib. UPol: 500mV and 674mV"
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
                Description = S100.Description
            };
        }
        public static readonly CmdDefinition S500
            = new CmdDefinition(OpCode.S500, wait: 3000)
            {
                Description = "[S500] Start Calib. UPol: 500mV"
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
                Description = S500.Description
            };
        }

        public static readonly CmdDefinition S674
            = new CmdDefinition(OpCode.S674, wait: 3000)
            {
                Description = "[S674] Start Calib. UPol: 674mV"
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
                Description = S674.Description
            };
        }

        public static readonly CmdDefinition S999
            = new CmdDefinition(OpCode.S999, wait: 4000)
            {
                Description = "[S999] InitBox",
                NextOnAnswer = true
            };

        /// <summary>
        /// Initialization Calibox
        /// </summary>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static CmdDefinition New_S999(int wait = 5000)
        {
            return new CmdDefinition(OpCode.S999, wait: wait)
            {
                Description = S999.Description,
                NextOnAnswer = true
            };
        }

        /**********************************************************
        * FUNCTION:     CMD Sequences
        * DESCRIPTION:
        ***********************************************************/
        public static CmdSequence New_InitBox()
        {
            var result = new CmdSequence(CMDs.Calib_S100_6850i)
            {
                Description = "[S999] + [G015] + [G906] + [G901] InitBox",
                Routing = new List<CmdDefinition>()
                    {
                        New_S999(wait: 4000),
                        New_G015(wait: 1000, retry: 2),
                        //New_G906(wait: 1000, retry: 1),
                        //New_G901(wait: 1000)
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
                Description = "[G100] + [S100] Start Calib. UPol: 500mV and 674mV",
                Routing = new List<CmdDefinition>()
                    {
                        New_G100(),
                        New_S100(wait: waitStart)
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
        * FUNCTION:     Temperatur Check
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
                        New_G906(wait: 1000, retry: 15)
                    }
            };
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
                        New_G906(wait: 1000, retry: 15)
                    }
            };
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
                        New_G906(wait: 1000, retry: 15)
                    }
            };
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
                        New_G906(wait: 1000, retry: 10)
                    }
            };
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
                        New_G906(wait: 1000, retry: 10)
                    }
            };
            return result;
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
                        New_G906(wait: 1000, retry: 10)
                    }
            };
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
                        New_ReadPageSensor(0),
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
                        New_ReadPageSensor(1),
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
                        New_ReadPageSensor(2),
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
        public static readonly Dictionary<string, CmdDefinition> CmdDefinitions
            = new Dictionary<string, CmdDefinition>()
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
