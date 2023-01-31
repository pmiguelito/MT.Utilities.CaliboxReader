using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConverterCalib.Enumerators;

namespace ConverterCalib
{

    public class clLimits
    {
        private clLimits_Calibration _NTC;
        public clLimits_Calibration NTC
        {
            get
            {
                if(_NTC == null)
                {
                    _NTC = Init_NTC();
                }
                return _NTC;
            }
        }
        private clLimits_Calibration Init_NTC()
        {
            return new clLimits_Calibration()
            {
                Process = ProcDesc.NTC_22kOhm_25C,
                Process_Next = ProcDesc.PT1000_20C,
                Meas_Count = new clLimits_MinMax() { LimitDescription = "NTC Meas_Count", Active = true, Set = 30, Min = 0, Plus = 9999999 },
                Meas_Value = new clLimits_MinMax() { LimitDescription = "NTC Meas_Value", Active = false, Set = 12900, Min = 300, Plus = 300},
                StdDev_Range = new clLimits_MinMax() { LimitDescription = "NTC StdDev_Range", Active = true, Set = 0, Min = 0, Plus = 0.03},
                StdDev_Count = new clLimits_MinMax() { LimitDescription = "NTC StdDev_Count", Active = true, Set = 10, Min = 0, Plus = 9999999 }
            };
        }

        private clLimits_Calibration _PT20;
        public clLimits_Calibration PT20
        {
            get
            {
                if (_PT20 == null)
                {
                    _PT20 = Init_PT20();
                }
                return _PT20;
            }
        }
        private clLimits_Calibration Init_PT20()
        {
            return new clLimits_Calibration()
            {
                Process = ProcDesc.PT1000_20C,
                Process_Next = ProcDesc.PT1000_30C,
                Meas_Count = new clLimits_MinMax() { LimitDescription = "PT20 Meas_Count", Active = true, Set = 50, Min = 0, Plus = 9999999 },
                Meas_Value = new clLimits_MinMax() { LimitDescription = "PT20 Meas_Value", Active = false, Set = 30900, Min = 300, Plus = 300 },
                StdDev_Range = new clLimits_MinMax() { LimitDescription = "PT20 StdDev_Range", Active = true, Set = 0, Min = 0, Plus = 0.6 },
                StdDev_Count = new clLimits_MinMax() { LimitDescription = "PT20 StdDev_Count", Active = true, Set = 10, Min = 0, Plus = 9999999 }
            };
        }


        private clLimits_Calibration _PT30;
        public clLimits_Calibration PT30
        {
            get
            {
                if (_PT30 == null)
                {
                    _PT30 = Init_PT30();
                }
                return _PT30;
            }
        }
        private clLimits_Calibration Init_PT30()
        {
            return new clLimits_Calibration()
            {
                Process = ProcDesc.PT1000_30C,
                Process_Next = ProcDesc.WritePage3CalibValues,
                Meas_Count = new clLimits_MinMax() { LimitDescription = "PT30 Meas_Count", Active = true, Set = 50, Min = 0, Plus = 9999999 },
                Meas_Value = new clLimits_MinMax() { LimitDescription = "PT30 Meas_Value", Active = false, Set = PT20.Meas_Value.Value +200, Min = 20, Plus = 20 },
                StdDev_Range = new clLimits_MinMax() { LimitDescription = "PT30 StdDev_Range", Active = true, Set = 0, Min = 0, Plus = 0.6 },
                StdDev_Count = new clLimits_MinMax() { LimitDescription = "PT30 StdDev_Count", Active = true, Set = 10, Min = 0, Plus = 9999999 }
            };
        }

    }

    public class clLimits_Calibration
    {
        public clLimits_Calibration()
        {
            Meas_Count = new clLimits_MinMax() { LimitDescription = "Default Counter", Active = true, Set = 30, Min = 0, Plus = 9999999 };
            Meas_Value = new clLimits_MinMax() { LimitDescription = "Default Value", Active = false, Set = 12900, Min = 300, Plus = 300 };
            StdDev_Range = new clLimits_MinMax() { LimitDescription = "Default StdDev Range", Active = true, Set = 0, Min = 0, Plus = 0.03 };
            StdDev_Count = new clLimits_MinMax() { LimitDescription = "Default StdDev Count", Active = true, Set = 10, Min = 0, Plus = 9999999};
        }
        public ProcDesc Process { get; set; }
        public ProcDesc Process_Next { get; set; }

        public List<double> Values = new List<double>();
        private double _Value;
        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                Values.Add(value);
                Meas_Value.Value = value;
                Meas_Count.Value = Counter;
            }
        }

        public List<double> Values_STDdev = new List<double>();
        private double _Value_STDdev;
        public double Value_STDdev
        {
            get { return _Value_STDdev; }
            set
            {
                _Value_STDdev = value;
                Values_STDdev.Add(value);
                StdDev_Range.Value = value;
                StdDev_Count.Value = StdDev_Range.Count;
            }
        }

        public List<double> Temperaturen = new List<double>();
        private double _Temperatur;
        public double Temperatur
        {
            get { return _Temperatur; }
            set
            {
                _Temperatur = value;
                Temperaturen.Add(value);
            }
        }
        public double Counter { get { return Values.Count; } }

        public bool Test_OK { get { return Get_TestOK();  } }
        private bool Get_TestOK()
        {
            if(Meas_OK && StdDev_OK) { return true; }
            if (StdDev_OK)
            {
                if (Meas_Value.Test_OK) { return false; }
            }
            Values = new List<double>();
            Values_STDdev = new List<double>();
            //Reset();
            return false;
        }
        
        public bool Meas_OK { get { return Meas_Count.Test_OK && Meas_Value.Test_OK; } }
        public bool StdDev_OK { get { return StdDev_Range.Test_OK && StdDev_Count.Test_OK; } }

        public clLimits_MinMax Meas_Value { get; set; }
        public clLimits_MinMax Meas_Count { get; set; }

        public clLimits_MinMax StdDev_Range { get; set; }
        public clLimits_MinMax StdDev_Count { get; set; }
        public void Reset()
        {
            Values = new List<double>();
            Values_STDdev = new List<double>();
            Meas_Value.Reset();
            Meas_Count.Reset();
            StdDev_Range.Reset();
            StdDev_Count.Reset();
        }
    }

    public class clLimits_MinMax
    {
        public void Reset()
        {
            Values = new List<double>();
        }
        public string LimitDescription { get; set; }
        public bool Active { get; set; }
        public bool Test_OK { get { return State == 0 || State == 2; } }
        public int State
        {
            get
            {
                if (Active)
                {
                    if(!Value_InRange) { Reset(); }
                    return Value_InRange ? 2 : 1;
                }
                return 0;
            }
        }

        public bool Value_InRange { get { return _Value >= Set_Min && _Value <= Set_Max; } }

        public double Value_Diff { get { return Set - Value; } }
        public double Count { get { return Values.Count; } }

        public List<double> Values = new List<double>();
        public double _Value;
        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (!Value_InRange)
                { Values = new List<double>(); Lowest = 9999999999; Upper = -9999999999; }
                Values.Add(value);
                MinMax(value);
            }
        }
        private void MinMax(double value)
        {
            if (value < Lowest)
            {
                Lowest = value;
            }
            if (value > Upper)
            {
                Upper = value;
            }
        }

        public double Set { get; set; }
        public double Min { get; set; }
        public double Plus { get; set; }

        public double Set_Min
        {
            get { return Set - Min; }
        }
        public double Set_Max
        {
            get { return Set + Plus; }
        }

        public double Lowest;
        public double Upper;
        public double LowerUpper_Diff
        {
            get { return Upper - Lowest; }
        }
    }
}
