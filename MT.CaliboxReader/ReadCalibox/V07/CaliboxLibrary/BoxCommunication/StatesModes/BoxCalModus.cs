using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliboxLibrary
{
    public class BoxCalModus : BoxModeDetails
    {
        public BoxCalModus(string hex, string desc, bool isError = false):base(hex, desc, isError)
        {

        }

        public virtual string ToStringWithHeader()
        {
            return $"BoxCalHEX: {Hex}\tBoxCalDesc: {Desc}";
        }
        #region Definition
        /************************************************
         * FUNCTION:    Definitions
         * DESCRIPTION:
         ************************************************/
        public static readonly BoxCalModus Cal500_674 = new BoxCalModus("00", "674mV and 500mV");
        public static readonly BoxCalModus Cal674 = new BoxCalModus("01", "674mV");
        public static readonly BoxCalModus Cal500 = new BoxCalModus("02", "500mV");
        public static readonly BoxCalModus NotDefined = new BoxCalModus("FF", "Not Defined");
        #endregion

        #region DefSearcher
        /************************************************
         * FUNCTION:    Definitions Searcher
         * DESCRIPTION:
         ************************************************/
        /// <summary>
        /// KEY: Hex, Calibration Types
        /// </summary>
        public static Dictionary<string, BoxCalModus> CalStatusDic = new Dictionary<string, BoxCalModus>(StringComparer.OrdinalIgnoreCase)
        {
            {"00", Cal500_674 },
            {"01", Cal674},
            {"02", Cal500 },
            {"FF", NotDefined}
        };
        #endregion

        public static BoxCalModus FromHex(string hex)
        {
            if (!CalStatusDic.TryGetValue(hex, out BoxCalModus mode))
            { mode = NotDefined; }
            return mode;
        }
        public static BoxCalModus FromDesc(string desc)
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
