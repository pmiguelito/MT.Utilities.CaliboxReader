using System.Collections.Generic;

namespace CaliboxLibrary
{
    public enum OpCode
    {
        noOpCode,
        state, State,
        Error, error,
        G000, G001, G002,
        g000, g001, g002,

        G015, G100, G200,
        g015, g100, g200,

        G901, G902, G903, G904, G905, G906, G907, G908, G909,
        g901, g902, g903, g904, g905, g906, g907, g908, g909,
        G910, G911, G912, G913, G914, G915, G916,
        g910, g911, g912, g913, g914, g915, g916,

        S100, S200, S500, S674, S999,
        s100, s200, s500, s674, s999,

        Cmdread, Cmdsend, Parser_error,
        cmdread, cmdsend, parser_error,

        RDPG, WRPG,
        rdpg, wrpg,
        RDBX, WRBX,
        rdbx, wrbx,
        Init, init,

        BoxIdentification,
        boxIdentification,
    }
    public static class OpCodes
    {
        public static Dictionary<OpCode, OpCode> DicRequest = new Dictionary<OpCode, OpCode>()
        {
            { OpCode.Init, OpCode.init },
            { OpCode.init, OpCode.Init },

            { OpCode.BoxIdentification, OpCode.boxIdentification },
            { OpCode.boxIdentification, OpCode.BoxIdentification },

            { OpCode.State, OpCode.state },
            { OpCode.state, OpCode.State },

            { OpCode.Error, OpCode.error },
            { OpCode.error, OpCode.Error },

            { OpCode.Cmdread, OpCode.cmdread },
            { OpCode.cmdread, OpCode.Cmdread },

            { OpCode.Cmdsend, OpCode.cmdsend },
            { OpCode.cmdsend, OpCode.Cmdsend },

            { OpCode.Parser_error, OpCode.parser_error },
            { OpCode.parser_error, OpCode.Parser_error },

#region Pages

            { OpCode.RDPG, OpCode.rdpg },
            { OpCode.rdpg, OpCode.RDPG },

            { OpCode.WRPG, OpCode.wrpg },
            { OpCode.wrpg, OpCode.WRPG },

            { OpCode.RDBX, OpCode.rdbx },
            { OpCode.rdbx, OpCode.RDBX },

            { OpCode.WRBX, OpCode.wrbx },
            { OpCode.wrbx, OpCode.WRBX },

            { OpCode.G000, OpCode.g000 },
            { OpCode.g000, OpCode.G000 },

            { OpCode.G001, OpCode.g001 },
            { OpCode.g001, OpCode.G001 },

            { OpCode.G002, OpCode.g002 },
            { OpCode.g002, OpCode.G002 },

            { OpCode.G015, OpCode.g015 },
            { OpCode.g015, OpCode.G015 },

            { OpCode.G100, OpCode.g100 },
            { OpCode.g100, OpCode.G100 },
            #endregion
#region debugging
            { OpCode.G200, OpCode.g200 },
            { OpCode.g200, OpCode.G200 },

            { OpCode.G901, OpCode.g901 },
            { OpCode.g901, OpCode.G901 },

            { OpCode.G902, OpCode.g902 },
            { OpCode.g902, OpCode.G902 },

            { OpCode.G903, OpCode.g903 },
            { OpCode.g903, OpCode.G903 },

            { OpCode.G904, OpCode.g904 },
            { OpCode.g904, OpCode.G904 },

            { OpCode.G905, OpCode.g905 },
            { OpCode.g905, OpCode.G905 },

            { OpCode.G906, OpCode.g906 },
            { OpCode.g906, OpCode.G906 },

            { OpCode.G907, OpCode.g907 },
            { OpCode.g907, OpCode.G907 },

            { OpCode.G908, OpCode.g908 },
            { OpCode.g908, OpCode.G908 },

            { OpCode.G909, OpCode.g909 },
            { OpCode.g909, OpCode.G909 },

            { OpCode.G910, OpCode.g910 },
            { OpCode.g910, OpCode.G910 },

            { OpCode.G911, OpCode.g911 },
            { OpCode.g911, OpCode.G911 },

            { OpCode.G912, OpCode.g912 },
            { OpCode.g912, OpCode.G912 },

            { OpCode.G913, OpCode.g913 },
            { OpCode.g913, OpCode.G913 },

            { OpCode.G914, OpCode.g914 },
            { OpCode.g914, OpCode.G914 },

            { OpCode.G915, OpCode.g915 },
            { OpCode.g915, OpCode.G915 },

            { OpCode.G916, OpCode.g916 },
            { OpCode.g916, OpCode.G916 },
            #endregion
#region starts
            { OpCode.S100, OpCode.s100 },
            { OpCode.s100, OpCode.S100 },

            { OpCode.S200, OpCode.s200 },
            { OpCode.s200, OpCode.S200 },

            { OpCode.S500, OpCode.s500 },
            { OpCode.s500, OpCode.S500 },

            { OpCode.S674, OpCode.s674 },
            { OpCode.s674, OpCode.S674 },

            { OpCode.S999, OpCode.s999 },
            { OpCode.s999, OpCode.S999 },
#endregion
        };

        /// <summary>
        /// Get Lower or Upper Case, this will return the opposite.
        /// If Upper return Lower and if Lower return Upper
        /// </summary>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static OpCode GetOpposite(this OpCode opCode)
        {
            DicRequest.TryGetValue(opCode, out OpCode response);
            return response;
        }
    }
    /*******************************************************************************************************************
    * Processes:
    *               timStateEngine is placed on "UC_Channel"
    '*******************************************************************************************************************/
    public enum gProcMain
    {
        getReadyForTest,
        BoxStatus,
        BoxIdentification,
        BoxReset,
        FWcheck,
        SensorCheck,
        SensorPage00Read,
        SensorPage00Write,
        SensorPage01Read,
        SensorPage01Write,
        SensorPage02Read,
        SensorPage02Write,
        TestFinished,
        StartProcess,
        Calibration,
        CalibrationWork,
        Bewertung,
        DBinit,
        wait,
        stop_stateEngine,
        error,
        idle
    }

    public enum CH_State
    {
        idle,
        inWork,
        QualityGood,
        QualityBad,
        active,
        notActive,
        error
    }
}
