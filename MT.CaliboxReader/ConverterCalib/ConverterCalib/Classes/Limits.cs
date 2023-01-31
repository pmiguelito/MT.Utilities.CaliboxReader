using System;
using System.Collections.Generic;
using System.Linq;

namespace ConverterCalib
{
    class Limits
    {
        public Limits(int nValues, string title)
        {
            ValueTitle = title;
            NValues = nValues;
            LimitsValue = new LimitsDetails();
            LimitsAvg = new LimitsDetails();
            LimitsStdDev = new LimitsDetails(true, 0, 0.1, 1);
            LimitsMinMaxDiff = new LimitsDetails(true, 0, 5, 5);
            Reset();
        }
        public int NValues;
        public string ValueTitle;
        public void Reset()
        {
            NValuesOk = false;
            Values = new List<double>();
            Avg = 0;
            StdDev = 100;
            Value = 0;
        }
        public void SetLimits(LimitsProcDetails limits, double value = 0)
        {
            Reset();
            if(value != 0)
            {
                limits.Values.Set = value;
                limits.Avg.Set = value;
            }
            LimitsValue = limits.Values;
            LimitsAvg = limits.Avg;
            LimitsStdDev = limits.StdDev;
            LimitsMinMaxDiff = limits.MinMaxDiff;
        }

        public List<double> Values;
        public bool NValuesOk { get; private set; }
        public double Value { get; private set; }
        public LimitsDetails LimitsValue { get; set; }
        public bool ValueInRange { get { return LimitsValue.InRange(Value, NValuesOk); } }

        public double Avg { get; private set; }
        public LimitsDetails LimitsAvg { get; set; }
        public bool AvgInRange { get { return LimitsAvg.InRange(Avg, NValuesOk); } }
        
        public double StdDev { get; private set; }
        public LimitsDetails LimitsStdDev { get; set; }
        public bool StdDevInRange { get { return LimitsStdDev.InRange(StdDev, NValuesOk); } }

        public double Min { get; private set; }
        public double Max { get; private set; }
        public LimitsDetails LimitsMinMaxDiff { get; set; }
        public double MinMaxDiff { get; private set; }
        public bool MinMaxDiffInRange { get { return LimitsMinMaxDiff.InRange(MinMaxDiff, NValuesOk); } }
        public double Sum { get; private set; }
        public void AddValue(double value, bool reset = false)
        {
            Value = value;
            Values.Add(value);
            while (Values.Count > NValues)
            {
                NValuesOk = true;
                Values.RemoveAt(0);
            }
            StdDev = CalcSTDdev(true);
            if (StdDev < 0) 
            { 
                StdDev = 0; 
            }
        }
        private double CalcSTDdev(bool asSample)
        {
            MinMaxAVG();
            //if(Values.Count < 2) { return 100; }
            var squares_query = from double value in Values select (value - Avg) * (value - Avg);
            double sum_of_squares = squares_query.Sum();
            if (asSample)
            {
                /*Sample fraction values*/
                return Math.Sqrt(sum_of_squares / (Values.Count -1));
            }
            else
            {
                /*Population total values*/
                return Math.Sqrt(sum_of_squares / (Values.Count));
            }
        }
        private void MinMaxAVG()
        {
            Min = 999999;
            Max = -9999999;
            Sum = 0;
            foreach(var v in Values)
            {
                if (v < Min) { Min = v; }
                if (v > Max) { Max = v; }
                Sum += v;
            }
            Avg = Sum /Values.Count;
            MinMaxDiff = Max - Min;
        }
        public bool InRange
        {
            get
            {
                if (!StdDevInRange) { return false; }
                if (!AvgInRange) { return false; }
                if (!ValueInRange) { return false; }
                if (!MinMaxDiffInRange) { return false; }
                return true;
            }
        }

    }
    static class LimitsProcTest
    {
        private static LimitsProcDetails _NTC_UNTC;
        public static LimitsProcDetails NTC_UNTC
        {
            get
            {
                if (_NTC_UNTC == null)
                {
                    _NTC_UNTC = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 13500, 600, 600),
                        Avg = new LimitsDetails(true, 13500, 600, 600),
                        StdDev = new LimitsDetails(true, 0, 0, 0.5),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _NTC_UNTC;
            }
        }

        private static LimitsProcDetails _NTC_Temp;
        public static LimitsProcDetails NTC_Temp
        {
            get
            {
                if (_NTC_Temp == null)
                {
                    _NTC_Temp = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 25, 10, 10),
                        Avg = new LimitsDetails(true, 25, 10, 10),
                        StdDev = new LimitsDetails(true, 0, 0, 0.5),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _NTC_Temp;
            }
        }


        private static LimitsProcDetails _PT1_UNTC;
        public static LimitsProcDetails PT1_UNTC
        {
            get
            {
                if (_PT1_UNTC == null)
                {
                    _PT1_UNTC = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 30900, 500, 500),
                        Avg = new LimitsDetails(true, 30900, 500, 500),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT1_UNTC;
            }
        }

        private static LimitsProcDetails _PT1_Temp;
        public static LimitsProcDetails PT1_Temp
        {
            get
            {
                if (_PT1_Temp == null)
                {
                    _PT1_Temp = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 20, 10, 10),
                        Avg = new LimitsDetails(true, 20, 10, 10),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT1_Temp;
            }
        }

        private static LimitsProcDetails _PT2_UNTC;
        public static LimitsProcDetails PT2_UNTC
        {
            get
            {
                if (_PT2_UNTC == null)
                {
                    _PT2_UNTC = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, PT1_UNTC.Avg.Set, 100, 100, 200),
                        Avg = new LimitsDetails(true, PT1_UNTC.Avg.Set, 100, 100, 200),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT2_UNTC;
            }
        }
        private static LimitsProcDetails _PT2_Temp;
        public static LimitsProcDetails PT2_Temp
        {
            get
            {
                if (_PT2_Temp == null)
                {
                    _PT2_Temp = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 30, 10, 10),
                        Avg = new LimitsDetails(true, 30, 10, 10),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT2_Temp;
            }
        }
    }
    static class LimitsProc
    {
        private static LimitsProcDetails _NTC_UNTC;
        public static LimitsProcDetails NTC_UNTC
        {
            get
            {
                if(_NTC_UNTC == null)
                {
                    _NTC_UNTC = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 13500, 600, 600),
                        Avg = new LimitsDetails(true, 13500, 600, 600),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _NTC_UNTC;
            }
        }

        private static LimitsProcDetails _NTC_Temp;
        public static LimitsProcDetails NTC_Temp
        {
            get
            {
                if (_NTC_Temp == null)
                {
                    _NTC_Temp = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 25, 3, 3),
                        Avg = new LimitsDetails(true, 25, 3, 3),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _NTC_Temp;
            }
        }


        private static LimitsProcDetails _PT1_UNTC;
        public static LimitsProcDetails PT1_UNTC
        {
            get
            {
                if (_PT1_UNTC == null)
                {
                    _PT1_UNTC = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 30900, 500, 500),
                        Avg = new LimitsDetails(true, 30900, 500, 500),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT1_UNTC;
            }
        }

        private static LimitsProcDetails _PT1_Temp;
        public static LimitsProcDetails PT1_Temp
        {
            get
            {
                if (_PT1_Temp == null)
                {
                    _PT1_Temp = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 20, 10, 10),
                        Avg = new LimitsDetails(false, 20, 10, 10),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT1_Temp;
            }
        }

        private static LimitsProcDetails _PT2_UNTC;
        public static LimitsProcDetails PT2_UNTC
        {
            get
            {
                if (_PT2_UNTC == null)
                {
                    _PT2_UNTC = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, PT1_UNTC.Avg.Set, 100, 100, 200),
                        Avg = new LimitsDetails(true, PT1_UNTC.Avg.Set, 100, 100, 200),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT2_UNTC;
            }
        }
        private static LimitsProcDetails _PT2_Temp;
        public static LimitsProcDetails PT2_Temp
        {
            get
            {
                if (_PT2_Temp == null)
                {
                    _PT2_Temp = new LimitsProcDetails()
                    {
                        Values = new LimitsDetails(false, 30, 10, 10),
                        Avg = new LimitsDetails(false, 30, 10, 10),
                        StdDev = new LimitsDetails(true, 0, 0, 0.6),
                        MinMaxDiff = new LimitsDetails(true, 0, 0, 1)
                    };
                }
                return _PT2_Temp;
            }
        }
    }

    class LimitsProcDetails
    {
        public LimitsDetails Values;
        public LimitsDetails Avg;
        public LimitsDetails StdDev;
        public LimitsDetails MinMaxDiff;
    }

    class LimitsDetails
    {
        public LimitsDetails()
        {
            Active = false;
            SetAddition = 0;
            _Set = 0;
            Plus = 999999;
            Min = 999999;
        }
        public LimitsDetails(bool active, double set, double min, double plus, double setAddition = 0)
        {
            Active = active;
            SetAddition = setAddition;
            _Set = set;
            Min = min;
            Plus = plus;
        }

        public bool Active { get; set; }
        public double SetAddition { get; private set; }
        private double _Set;
        public double Set
        {
            get { return _Set; }
            set
            {
                _Set = value + SetAddition;
                SetMin = _Set - Min;
                SetPlus = _Set + _Plus;
            }
        }
        private double _Min;
        public double Min
        {
            get { return _Min; }
            set
            {
                _Min = value;
                SetMin = _Set - _Min;
            }
        }
        private double _Plus;
        public double Plus
        {
            get { return _Plus; }
            set
            {
                _Plus = value;
                SetPlus = _Set + _Plus;
            }
        }
        public double SetMin { get; private set; }
        public double SetPlus { get; private set; }
        public bool InRange(double value)
        {
            if (!Active) { return true; }
            if (value > SetPlus) { return false; }
            if (value < SetMin) { return false; }
            return true;
        }
        public bool InRange(double value, bool nValuesOk)
        {
            if (!Active) { return true; }
            if (value > SetPlus) { return false; }
            if (value < SetMin) { return false; }
            if (!nValuesOk) { return false; }
            return true;
        }
    }
}
