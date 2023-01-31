using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConverterCalib.Enumerators;

namespace ConverterCalib
{
    class MeasValues
    {
        public MeasValues(int avgNvalues = 30)
        {
            _AVGnValues = avgNvalues;
            MeasCurrent = new Limits(avgNvalues, Constants.MeasCurrent);
            UPol = new Limits(avgNvalues, Constants.UPol);
            Temp = new Limits(avgNvalues, Constants.Temp);
            Impendance = new Limits(avgNvalues, Constants.Impedance);
            UAnode = new Limits(avgNvalues, Constants.UAnode);
            UNTC = new Limits(avgNvalues, Constants.UNTC);
            TempBoad = new Limits(avgNvalues, Constants.TempBoard);
            MBHigh = new Limits(avgNvalues, Constants.StatusHighMB);
            MBLow = new Limits(avgNvalues, Constants.StatusLowMB);
            Reset(_ProcDesc, avgNvalues);
        }

        public void Reset(ProcDesc proc = ProcDesc.idle, int avgNvalues = 30)
        {
            if(proc!= ProcDesc.idle)
            {
                _ProcDesc = proc;
            }
            LimitsChange(_ProcDesc, false);
        }

        private int _AVGnValues;
        public int AVGnValues
        {
            get { return _AVGnValues; }
            set
            {
                if(_AVGnValues != value)
                {
                    MeasCurrent.NValues = value;
                    UPol.NValues = value;
                    Temp.NValues = value;
                    Impendance.NValues = value;
                    UAnode.NValues = value;
                    UNTC.NValues = value;
                    TempBoad.NValues = value;
                    MBHigh.NValues = value;
                    MBLow.NValues = value;
                }
                _AVGnValues = value;
            }
        }

        private ProcDesc _ProcDesc = ProcDesc.idle;
        public ProcDesc ProcDesc
        {
            get
            {
                return _ProcDesc;
            }
            set
            {
                LimitsChange(value);
                _ProcDesc = value;
            }
        }

        private void LimitsChange(ProcDesc proc, bool reset = true)
        {
            if (reset) { Reset(); }
            switch (proc)
            {
                case ProcDesc.NTC_22kOhm_25C:
                    if (!Globals.TestLimits)
                    {
                        UNTC.SetLimits(LimitsProc.NTC_UNTC);
                        Temp.SetLimits(LimitsProc.NTC_Temp);
                    }
                    else
                    {
                        UNTC.SetLimits(LimitsProcTest.NTC_UNTC);
                        Temp.SetLimits(LimitsProcTest.NTC_Temp);
                    }
                    break;
                case ProcDesc.PT1000_20C:
                    if (!Globals.TestLimits)
                    {
                        UNTC.SetLimits(LimitsProc.PT1_UNTC);
                        Temp.SetLimits(LimitsProc.PT1_Temp);
                    }
                    else
                    {
                        UNTC.SetLimits(LimitsProcTest.PT1_UNTC);
                        Temp.SetLimits(LimitsProcTest.PT1_Temp);
                    }
                    break;
                case ProcDesc.PT1000_30C:
                    if (!Globals.TestLimits)
                    {
                        UNTC.SetLimits(LimitsProc.PT2_UNTC, UNTC.Avg);
                        Temp.SetLimits(LimitsProc.PT2_Temp);
                    }
                    else
                    {
                        UNTC.SetLimits(LimitsProcTest.PT2_UNTC, UNTC.Avg);
                        Temp.SetLimits(LimitsProcTest.PT2_Temp);
                    }
                    break;
                default:
                    break;
            }
            MeasCurrent.Reset();
            UPol.Reset();
            Impendance.Reset();
            UAnode.Reset();
            TempBoad.Reset();
            MBHigh.Reset();
            MBLow.Reset();
        }


        public Limits MeasCurrent { get; private set; }
        public Limits UPol { get; private set; }
        public Limits Temp { get; private set; }
        public Limits Impendance { get; private set; }
        public Limits UAnode { get; private set; }
        public Limits UNTC { get; private set; }
        public Limits TempBoad { get; private set; }
        public Limits MBHigh { get; private set; }
        public Limits MBLow { get; private set; }
    }

}
