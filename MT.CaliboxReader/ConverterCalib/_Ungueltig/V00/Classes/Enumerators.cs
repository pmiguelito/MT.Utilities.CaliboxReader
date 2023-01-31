using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCalib
{
    public static class Enumerators
    {

        /*****************************************************************************
        * Calibration:
        '****************************************************************************/
        public enum ProcDesc
        {
            idle,
            ProcessStop,
            ProcessStart,
            CommTest,
            CheckInitCalib,
            ReadCalibPage3,
            SetCalibPage3Default,
            WriteCalibPage3Default,
            WriteCalibPage3,
            NTC_22kOhm_25C,
            PT1000_20C,
            PT1000_30C,
            
            ResetValues,
            
        }

    }
}
