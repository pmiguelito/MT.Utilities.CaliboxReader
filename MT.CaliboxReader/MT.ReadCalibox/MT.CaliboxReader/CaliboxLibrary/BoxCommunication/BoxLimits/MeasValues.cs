using static CaliboxLibrary.DataBase;

namespace CaliboxLibrary
{
    public class MeasValues
    {
        public MeasValues(DbMeasBaseValues name)
        {
            Name = name;
            Set = new MeasValue(DataBase.GetEnum(name, DbMeasSuffix.Set));
            Avg = new MeasValue(DataBase.GetEnum(name, DbMeasSuffix.AVG));
            StdDev = new MeasValue(DataBase.GetEnum(name, DbMeasSuffix.StdDev));
            ErrorAbs = new MeasValue(DataBase.GetEnum(name, DbMeasSuffix.Error));
        }
        public MeasValues(DbMeasBaseValues name, string set, string avg, string stdDev, string errorAbs):this(name)
        {
            Set.Value = set;
            Avg.Value = avg;
            StdDev.Value = stdDev;
            ErrorAbs.Value = errorAbs;
        }
        public DbMeasBaseValues Name { get; private set; }
        public bool IsMeasValue { get; set; }
        public MeasValue Set { get; set; }
        public MeasValue Avg { get; set; }
        public MeasValue StdDev { get; set; }
        public MeasValue ErrorAbs { get; set; }
        public string AddSet(string value)
        {
            IsMeasValue = true;
            return Set.Add(value);
        }
        public string AddAvg(string value)
        {
            IsMeasValue = true;
            return Avg.Add(value);
        }
        public string AddStdDev(string value)
        {
            IsMeasValue = true;
            return StdDev.Add(value);
        }
        public string AddError(string value)
        {
            IsMeasValue = true;
            return ErrorAbs.Add(value);
        }
    }
}
