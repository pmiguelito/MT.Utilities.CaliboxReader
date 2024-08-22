using System;
using System.Collections.Generic;

namespace CaliboxLibrary
{
    public class BoxCalMode : BoxModeDetails
    {
        public BoxCalMode(string hex, string desc, bool isError = false) : base(hex, desc, isError)
        {

        }

        public virtual string ToStringWithHeader()
        {
            return $"Cal:\t[{Hex}]\t{Desc}";
        }

        #region Definition
        /************************************************
         * FUNCTION:    Definitions
         * DESCRIPTION:
         ************************************************/
        public static readonly BoxCalMode Cal500_674 = new BoxCalMode("00", "674mV_500mV");
        public static readonly BoxCalMode Cal674 = new BoxCalMode("01", "674mV");
        public static readonly BoxCalMode Cal500 = new BoxCalMode("02", "500mV");
        public static readonly BoxCalMode NotDefined = new BoxCalMode("FF", "Not Defined");
        #endregion

        #region DefSearcher
        /************************************************
         * FUNCTION:    Definitions Searcher
         * DESCRIPTION:
         ************************************************/
        /// <summary>
        /// KEY: Hex, Calibration Types
        /// </summary>
        public static Dictionary<string, BoxCalMode> CalStatusDic = new Dictionary<string, BoxCalMode>(StringComparer.OrdinalIgnoreCase)
        {
            {"00", Cal500_674 },
            {"01", Cal674},
            {"02", Cal500 },
            {"FF", NotDefined}
        };
        #endregion

        public static BoxCalMode FromHex(string hex)
        {
            if (!CalStatusDic.TryGetValue(hex, out BoxCalMode mode))
            { mode = NotDefined; }
            return mode;
        }

        public static BoxCalMode FromDesc(string desc)
        {
            foreach (var mode in CalStatusDic.Values)
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
