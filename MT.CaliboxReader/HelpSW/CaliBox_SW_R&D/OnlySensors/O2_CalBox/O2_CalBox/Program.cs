using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2_CalBox
{
    static class Constants
    {
        public const byte BoxCalTolerancePage = 10; // 
        public const byte BoxCalSollValues = 31; // 
        public const byte SensorCalibrationDataPage = 14;

        public const byte CalibMode_674mV_Low_1 = 0;
        public const byte CalibMode_674mV_Low_2 = 1;
        public const byte  CalibMode_674mV_High_1 = 2;
        public const byte  CalibMode_674mV_High_2 = 3;

        public const byte  CalibMode_500mV_Low_1 = 4;
        public const byte  CalibMode_500mV_Low_2 = 5;

        public const byte  CalibMode_500mV_High_1 = 6;
        public const byte  CalibMode_500mV_High_2 = 7;

        public const byte  VerifyMode_674mV_Low_1 = 8;
        public const byte  VerifyMode_674mV_Low_2 = 9;

        public const byte  VerifyMode_674mV_High_1 = 10;
        public const byte  VerifyMode_674mV_High_2 = 11;

        public const byte  VerifyMode_500mV_Low_1 = 12;
        public const byte  VerifyMode_500mV_Low_2 = 13;

        public const byte  VerifyMode_500mV_High_1 = 14;
        public const byte  VerifyMode_500mV_High_2 = 15;
        public const byte  VerifyTemp = 16;

        public const byte  CalibMode_674CalculationLow = 51;
        public const byte  CalibMode_674CalculationHigh = 52;
        public const byte  CalibMode_500CalculationLow = 53;
        public const byte  CalibMode_500CalculationHigh = 54;
        public const byte  SuccessfullSensorCalibration = 55;
        public const byte  Box_SensorCheckUpol_500 = 56;
        public const byte  ShowErrorValues = 57;
        public const byte  DebugUpolOnCathode = 58;
        public const byte  DebugUpolOnAnode = 59;
        public const byte  ReadPage16 = 60;
        public const byte  RS232_OW_ACCESS = 61;
        public const byte  VerifyUpol = 62;

        public const byte  Box_Idle = 50;
        public const byte  Box_WritePage_00 = 17;
        public const byte  Box_WritePage_01 = 18;
        public const byte  Box_WritePage_12 = 19;
        public const byte  Box_WritePage_15 = 20;
        public const byte  Box_SensorCheckUpol_674 = 21;
        public const byte  Box_SensorVerification = 22;
        public const byte  Box_SensorError = 23;
        public const byte  Box_SensorWriteCalData674 = 24;
        public const byte  Box_SensorWriteCalData500 = 25;
        public const byte  Box_StartSensorCalibration = 26;

        public const byte  SensorFail = 27;
        public const byte  SensorCalibFinalise = 28;
        public const byte  Box_Calibration = 29;

        public const byte  WEP_Test = 30;
        public const byte  WEP_674mV_Low_1 = 31;
        public const byte  WEP_674mV_Low_2 = 32;
        public const byte  WEP_500mV_Low_1 = 33;
        public const byte  WEP_500mV_Low_2 = 34;
        public const byte  WEP_674mV_High_1 = 35;
        public const byte  WEP_674mV_High_2 = 36;
        public const byte  WEP_500mV_High_1 = 37;
        public const byte  WEP_500mV_High_2 = 38;
        public const byte  WEP_SensorError = 39;
        public const byte  WEPSensorFail = 40;
        public const byte  SensorWepFinalise = 41;
        public const byte  WEP_SensorCheckUpol = 42;
        public const byte  WEP_TempCheck = 43;


    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
