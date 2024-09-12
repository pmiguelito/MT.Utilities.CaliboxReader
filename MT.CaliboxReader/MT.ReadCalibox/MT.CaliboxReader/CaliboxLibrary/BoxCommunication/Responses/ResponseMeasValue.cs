namespace CaliboxLibrary.BoxCommunication.Responses
{
    public class ResponseMeasValue
    {
        /************************************************
         * FUNCTION:    Measurement Values
         * DESCRIPTION:
         ************************************************/
        public MeasValues Meas_I = new MeasValues(DataBase.DbMeasBaseValues.I);
        public MeasValue ISet { get { return Meas_I.Set; } }
        public string I_Set { get => ISet?.Value; }
        public double? I_SetDec { get => ISet?.ValueNumeric; }

        public MeasValue IAvg { get { return Meas_I.Avg; } }
        public string I_AVG { get => IAvg?.Value; }
        public double? I_AVGDec { get => IAvg?.ValueNumeric; }

        public MeasValue IStdDev { get { return Meas_I.StdDev; } }
        public string I_StdDev { get => IStdDev?.Value; }
        public double? I_StdDevDec { get => IStdDev?.ValueNumeric; }

        public MeasValue IError { get { return Meas_I.ErrorAbs; } }
        public string I_Error { get => IError?.Value; }
        public double? I_ErrorDec { get => IError?.ValueNumeric; }

        public MeasValues Meas_Temp = new MeasValues(DataBase.DbMeasBaseValues.Temp);
        public MeasValue TempSet { get { return Meas_Temp.Set; } }
        public string Temp_Set { get => TempSet?.Value; }
        public double? Temp_SetDec { get => TempSet?.ValueNumeric; }

        public MeasValue TempAvg { get { return Meas_Temp.Avg; } }
        public string Temp_AVG { get => TempAvg?.Value; }
        public double? Temp_AVGDec { get => TempAvg?.ValueNumeric; }

        public MeasValue TempStdDev { get { return Meas_Temp.StdDev; } }
        public string Temp_StdDev { get => TempStdDev?.Value; }
        public double? Temp_StdDevDec { get => TempStdDev?.ValueNumeric; }

        public MeasValue TempError { get { return Meas_Temp.ErrorAbs; } }
        public string Temp_Error { get => TempError?.Value; }
        public double? Temp_ErrorDec { get => TempError?.ValueNumeric; }
    }
}
