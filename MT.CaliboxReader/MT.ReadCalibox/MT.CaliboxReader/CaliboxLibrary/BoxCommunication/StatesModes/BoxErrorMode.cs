using System.Collections.Generic;

namespace CaliboxLibrary
{
    public class BoxErrorMode : BoxModeDetails
    {
        public readonly bool IsFinished;

        public BoxErrorMode(string hex, string desc, bool isError = true, bool isFinished = false) : base(hex, desc, isError)
        {
            IsFinished = isFinished;
        }

        public virtual string ToStringWithHeader()
        {
            return $"Error:\t[{Hex}]\t{Desc}";
        }

        #region Definition
        /************************************************
         * FUNCTION:    Definitions
         * DESCRIPTION:
         ************************************************/
        public static readonly BoxErrorMode NoSensCom = new BoxErrorMode("NoSensCom", "No Sensor Communication", isError: true, isFinished: true);
        public static readonly BoxErrorMode NoBoxCom = new BoxErrorMode("NoBoxCom", "No Box Communication", isError: true, isFinished: true);

        public static readonly BoxErrorMode NotDefined = new BoxErrorMode("FF", "Not Defined Error Value", isError: true);
        public static readonly BoxErrorMode NotChecked = new BoxErrorMode("0", "Not Checked", isError: false);
        public static readonly BoxErrorMode Error = new BoxErrorMode("1", "Error", isFinished: true);
        public static readonly BoxErrorMode Pass = new BoxErrorMode("2", "PASS", isError: false);

        public static readonly BoxErrorMode NoError = new BoxErrorMode("00", "PASS", isError: false);

        public static readonly BoxErrorMode ErrorStdDevNoisy = new BoxErrorMode("01", "Standard Deviation was out of range (Noisy Signal)");
        public static readonly BoxErrorMode ErrorMeanRange = new BoxErrorMode("02", "Calculated Mean was out of range (Offset Error)");
        public static readonly BoxErrorMode ErrorStdDevMeanRange = new BoxErrorMode("03", "Standard Deviation & Calculated Mean were out of range");
        public static readonly BoxErrorMode Timeout = new BoxErrorMode("04", "Error Timeout");

        public static readonly BoxErrorMode ErrorTemp = new BoxErrorMode("08", "Error Temperature");
        public static readonly BoxErrorMode ErrorTemp20PT1000 = new BoxErrorMode("0C", "Error 20°C PT1000");
        #endregion
        #region DefSearcher
        /************************************************
         * FUNCTION:    Definitions Searcher
         * DESCRIPTION:
         ************************************************/
        /// <summary>
        /// KEY: HEX, Errors
        /// </summary>
        public static Dictionary<string, BoxErrorMode> BoxErrorCodeDic = new Dictionary<string, BoxErrorMode>
        {
            {"0", NotChecked },
            {"NotChecked", NotChecked },
            {"1", Error },
            {"2", Pass },
            {"NoSensCom", NoSensCom },
            {"NoBoxCom", NoBoxCom },

            {"00", NoError },
            {"01", ErrorStdDevNoisy },
            {"02", ErrorMeanRange },
            {"03", ErrorStdDevMeanRange },
            {"04", Timeout },
            {"08", ErrorTemp },
            {"0C", ErrorTemp20PT1000 },

            {"FF", NotDefined }
        };
        #endregion
        //public static implicit operator BoxErrorMode(BoxErrorCodes @enum)
        //{
        //    return FromHex(@enum);
        //}
        public static implicit operator BoxErrorMode(string hex)
        {
            return FromHex(hex);
        }
        //public static BoxErrorMode FromHex(BoxErrorCodes code)
        //{
        //    switch (code)
        //    {
        //        case BoxErrorCodes.Error:
        //            return Error;
        //        case BoxErrorCodes.NoSensCom:
        //            return NoSensorComunication;
        //        case BoxErrorCodes.NoBoxCom:
        //            return NoBoxComunication;
        //        case BoxErrorCodes.NotChecked:
        //            return NotChecked;
        //        case BoxErrorCodes.Pass:
        //            return Pass;
        //        default:
        //            return NotDefined;
        //    }
        //}
        public static BoxErrorMode FromHex(string hex)
        {
            if (!BoxErrorCodeDic.TryGetValue(hex, out BoxErrorMode mode))
            { mode = NotDefined; }
            return mode;
        }

        public static BoxErrorMode FromDesc(string desc)
        {
            foreach (var mode in BoxErrorCodeDic.Values)
            {
                if (mode.Desc == desc)
                {
                    return mode;
                }
            }
            return null;
        }

    }
}
