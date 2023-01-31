using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCalibox
{
    public class clHandler
    {
        /*******************************************************************************************************************
        * Processes:
        *               timStateEngine is placed on "UC_Channel"
        '*******************************************************************************************************************/
        public enum gProcMain
        {
            getReadyForTest,
            BoxStatus,
            BoxReset,
            FWcheck,
            TestFinished,
            StartProcess,
            Calibration,
            Bewertung,
            DBinit,
            wait,
            error,
            idle
        };

        public static gProcMain[] gProcOrder = new gProcMain[]
         {
            gProcMain.BoxReset,
            gProcMain.BoxStatus,
            gProcMain.DBinit,
            gProcMain.FWcheck,
            gProcMain.Calibration,
            gProcMain.Bewertung,
            gProcMain.TestFinished
         };

        /*******************************************************************************************************************
        * Box States:
        '*******************************************************************************************************************/
        public static Dictionary<string, string> BoxMode = new Dictionary<string, string>
        {
            {"00","CalibMode_674mV_Low_1"},
            {"01","CalibMode_674mV_Low_2"},
            {"02","CalibMode_674mV_High_1"},
            {"03","CalibMode_674mV_High_2"},
            {"04","CalibMode_500mV_Low_1"},
            {"05","CalibMode_500mV_Low_2"},
            {"06","CalibMode_500mV_High_1"},
            {"07","CalibMode_500mV_High_2"},
            {"08","VerifyMode_674mV_Low_1"},
            {"09","VerifyMode_674mV_Low_2"},
            {"0A","VerifyMode_674mV_High_1"},
            {"0B","VerifyMode_674mV_High_2"},
            {"0C","VerifyMode_500mV_Low_1"},
            {"0D","VerifyMode_500mV_Low_2"},
            {"0E","VerifyMode_500mV_High_1"},
            {"0F","VerifyMode_500mV_High_2"},
            {"10","VerifyTemp"},
            {"33","CalibMode_674CalculationLow"},
            {"34","CalibMode_674CalculationHigh"},
            {"35","CalibMode_500CalculationLow"},
            {"36","CalibMode_500CalculationHigh"},
            {"37","SuccessfullSensorCalibration"},
            {"38","Box_SensorCheckUpol_500"},
            {"39","ShowErrorValues"},
            {"32","Box_Idle"},
            {"11","Box_WritePage_00"},
            {"12","Box_WritePage_01"},
            {"13","Box_WritePage_12"},
            {"14","Box_WritePage_15"},
            {"15","Box_SensorCheckUpol_674"},
            {"16","Box_SensorVerification"},
            {"17","Box_SensorError"},
            {"18","Box_SensorWriteCalData674"},
            {"19","Box_SensorWriteCalData500"},
            {"1A","Box_StartSensorCalibration"},
            {"1B","SensorFail"},
            {"1C","SensorCalibFinalise"},
            {"1D","Box_Calibration"},
            {"1E","WEP_Test"},
            {"1F","WEP_674mV_Low_1"},
            {"20","WEP_674mV_Low_2"},
            {"21","WEP_500mV_Low_1"},
            {"22","WEP_500mV_Low_2"},
            {"23","WEP_674mV_High_1"},
            {"24","WEP_674mV_High_2"},
            {"25","WEP_500mV_High_1"},
            {"26","WEP_500mV_High_2"},
            {"27","WEP_SensorError"},
            {"28","WEPSensorFail"},
            {"29","SensorWepFinalise"},
            {"2A","WEP_SensorCheckUpol"},
            {"2B","WEP_TempCheck"},

            {"S100","CalMode 674/500mV"},
            {"S500","CalMode 500mV"},
            {"S674","CalMode 674mV"},
            {"FWversion","FWversion"}
        };

        public static Dictionary<string, string> BoxErrorCode = new Dictionary<string, string>
        {
            {"0","NotChecked" },
            {"1","ERROR" },
            {"2","PASS" },
            {"00","NoError" },
            {"01","Standard Deviation was out of range (Noisy Signal)" },
            {"02","Calculated Mean was out of range (Offset Error)" },
            {"03","Standard Deviation & Calculated Mean were out of range" },
        };
        public static Dictionary<string, string> CalibrationStatus = new Dictionary<string, string>
        {
            {"00","674mV and 500mV" },
            {"01","647mV" },
            {"02","500mV" }
        };
        public enum opcode
        {
            state, Error, error,
            G015, G100, G200, G901, G902, G903, G904, G905, G906,
            g015, g100, g200, g901, g902, g903, g904, g905, g906,
            S100, S200, S500, S674, S999,
            s100, s200, s500, s674, s999,
            S901,
            s901
        }
    }
}
