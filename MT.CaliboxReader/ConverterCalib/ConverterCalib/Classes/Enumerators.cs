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
            StopProcess,
            StartProcess,
            StartMeas,
            StopMeas,
            CheckCommunication,
            CheckCalibP14P30,
            CheckCalibValuesInit,
            CheckCalibValuesWrited,
            SetCalibPage3Default,
            WritePage3CalibDefault,
            WritePage3CalibValues,
            NTC_22kOhm_25C,
            PT1000_20C,
            PT1000_30C,
            ResetValues,
            SetResultOk,
            SetResultNok,
        }

    }
}
