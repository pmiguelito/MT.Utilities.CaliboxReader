using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCalib
{
    class Constants
    {

        /*****************************************************************************
        * TDL:   Sensor Page 16
        *****************************************************************************/
        public const string MeasCurrent = "MeasCurrent";
        public const string UPol = "UPol";
        public const string Temp = "Temp";
        public const string Impedance = "Impedance";
        public const string UAnode = "UAnode";
        public const string UNTC = "UNTC";
        public const string TempBoard = "TempBoard";
        public const string StatusHighMB = "StatusHighMB";
        public const string StatusLowMB = "StatusLowMB";

        /*****************************************************************************
        * TDL:   Converter Page 3
        *****************************************************************************/
        public const string NTCoffset = "NTCoffset";
        public const string U_PT1000_Soll_Temp20 = "U_PT1000_Soll_Temp20";
        public const string U_PT1000_Soll_Temp30 = "U_PT1000_Soll_Temp30";
        public const string CalConverterDate = "CalConverterDate";
        public const string CalConverterDesc = "CalConverterDesc";

    }
}
