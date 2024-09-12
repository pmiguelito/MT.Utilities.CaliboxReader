using MT.ODBC;
using MT.ODBC.DataConn;
using MT.OneWire;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace CaliboxLibrary
{
    public static class DataBase
    {

        public static IDataConnection ConnectionEK;
        public static IDataConnection ConnectionTT;
        private static bool CreateOdbcEk(string odbc)
        {
            try
            {
                DataConnector.GetOdbc(odbc, out ConnectionEK, odbcIsTT: false);
                return ConnectionEK.OdbcExists;
            }
            catch
            {

            }
            return false;
        }
        private static bool CreateOdbcTrackTrace(string odbc)
        {
            DataConnector.GetOdbc(odbc, out ConnectionTT, odbcIsTT: true);
            return ConnectionTT.OdbcExists;
        }

        /****************************************************************************************************
         * tables:
         ***************************************************************************************************/
        public const string tLimitValEval = "tLimitValEval";
        public const string tTechnology = "tTechnology";
        public const string tCalMeasVal = "tCalMeasVal";
        public const string tCalMeasValTemp = "tCalMeasValTemp";
        public const string tCalLog = "tCalLog";
        public const string tTDL = "tTDL";
        public const string tCalMode = "tCalMode";

        /*******************************************************************************************************************
        * Names:
        '*******************************************************************************************************************/
        //public const string MN_StdDev = "StdDev";
        //public const string MN_RefValue = "RefValue";
        //public const string MN_RefValueSet = "RefValue_set";
        //public const string MN_RefValueOK = "RefValue_ok";
        //public const string MN_MeanValue = "MeanValue";
        //public const string MN_MeanValueSet = "MeanValue_set";
        //public const string MN_MeanValueOK = "MeanValue_ok";
        //public const string MN_ErrorABS = "ErrorABS";

        public const string MN_ID = "ID";
        public const string MN_Test_ok = "test_ok";
        public const string MN_meas_time_start = "meas_time_start";
        public const string MN_meas_time_end = "meas_time_end";
        public const string MN_meas_time = "meas_time";
        public const string MN_pass_no = "pass_no";
        public const string MN_item = "item";
        public const string MN_pdno = "pdno";
        public const string MN_Channelno = "channel_no";
        public const string MN_sensorid = "sensor_id";
        public const string MN_ProcOrder = "ProcOrder";
        public const string MN_EK_SWversion = "EK_SW_Version";
        public const string MN_Sample_FWversion = "sample_FW_Version";
        public const string MN_Sample_FWversion_state = "sample_FW_Version_state";
        public const string MN_Device_FWversion = "device_FW_Version";

        public const string MN_Duration = "duration";
        public const string MN_Values = "values";
        public const string MN_Opcode = "opcode";
        public const string MN_FWversion = "FWversion";
        public const string MN_BoxMode_HEX = "BoxHEX";
        public const string MN_BoxMode_Desc = "BoxDesc";
        public const string MN_CalStatus_HEX = "CalStatusHEX";
        public const string MN_BoxErrorMode_HEX = "BoxErrorHEX";
        public const string MN_BoxErrorMode_Desc = "BoxErrorDesc";

        public const string MN_I = "I";
        public const string MN_Iraw = "I_Raw";
        public const string MN_Upol = "Upol";
        public const string MN_Upol_StdDev = "Upol_StdDev";
        public const string MN_Upol_AVG = "Upol_AVG";
        public const string MN_Upol_Error = "Upol_Error";
        public const string MN_UAnode = "UAnode";
        public const string MN_TempVolt = "TempVolt";
        public const string MN_MBrange = "MBrange";
        public const string MN_Temp = "Temp";

        #region MeasValues
        public const string MN_I_AVG = "I_AVG";
        public const string MN_I_set = "I_Set";
        public const string MN_I_ok = "I_ok";

        public const string MN_I_StdDev = "I_StdDev";
        public const string MN_I_StdDev_set = "I_StdDev_set";
        public const string MN_I_StdDev_ok = "I_StdDev_ok";

        public const string MN_I_Error = "I_Error";
        public const string MN_I_Error_set = "I_Error_set";
        public const string MN_I_Error_ok = "I_Error_ok";

        public const string MN_Temp_set = "Temp_set";
        public const string MN_Temp_ok = "Temp_ok";
        public const string MN_Temp_AVG = "Temp_avg";

        public const string MN_Temp_StdDev = "Temp_StdDev";
        public const string MN_Temp_StdDev_set = "Temp_StdDev_set";
        public const string MN_Temp_StdDev_ok = "Temp_StdDev_ok";

        public const string MN_Temp_Error = "Temp_Error";
        public const string MN_Temp_Error_set = "Temp_Error_set";
        public const string MN_Temp_Error_ok = "Temp_Error_ok";

        public enum DbMeasBaseValues
        {
            I,
            Temp
        }
        public enum DbMeasValues
        {
            I_AVG, I_Set, I_ok,
            I_StdDev, I_StdDev_Set, I_StdDev_ok,
            I_Error, I_Error_Set, I_Error_ok,

            Temp_AVG, Temp_Set, Temp_ok,
            Temp_StdDev, Temp_StdDev_Set, Temp_StdDev_ok,
            Temp_Error, Temp_Error_Set, Temp_Error_ok,
        }
        public enum DbMeasSuffix
        {
            AVG, Set, Ok,
            StdDev, StdDev_Set, StdDev_ok,
            Error, Error_Set, Error_ok,
        }
        public static Dictionary<DbMeasBaseValues, Dictionary<DbMeasSuffix, DbMeasValues>> Dic = new Dictionary<DbMeasBaseValues, Dictionary<DbMeasSuffix, DbMeasValues>>() { };
        public static DbMeasValues GetEnum(DbMeasBaseValues baseValue, DbMeasSuffix suffix)
        {
            string key = $"{baseValue}_{suffix}";
            Enum.TryParse(key, out DbMeasValues value);
            return value;
        }
        #endregion

        /****************************************************************************************************
         * SQL:     Truncate Tables
         *          use this only for tests
         ***************************************************************************************************/

        public static void Truncate_DB(string odbc)
        {
            /*
            if ((DialogResult)MessageBox.Show($"Truncate ODBC: {odbc}", "DELETE", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ODBC_TT.ODBC.DB_Truncate(odbc, tCalLog, false);
                ODBC_TT.ODBC.DB_Truncate(odbc, tCalMeasValTemp, false);
                ODBC_TT.ODBC.DB_Truncate(odbc, tCalMeasVal);
            }
            */
        }

        #region TDL

        /***************************************************
         *  FUNCTION:       TDL Properties
         *  DESCRIPTION:
         ***************************************************/
        //public static bool Get_TDL_Page(this int pageNo, string odbc, out List<TDLproperty> pageProperties)
        //{
        //    pageProperties = new List<TDLproperty>();
        //    CreateOdbcEk(odbc);
        //    if (!ConnectionEK.OdbcExists) { return false; }
        //    string where = $" WHERE page_no = '{pageNo}' ORDER BY page_no, bit_start";
        //    string sqlQuery = Select_tTDL + where;
        //    DataTable dt = new DataTable("tTDL");
        //    ODBC_TT.ODBC.DB_Get_Data(odbc, sqlQuery, ref dt, out int count);

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        pageProperties.Add(row.ToObjectNewClass<TDLproperty>());
        //    }
        //    return count > 0;
        //}

        public static bool Get_TDLpropertiesFromDB(string odbc, out List<TDL_Property> properties)
        {
            CreateOdbcEk(odbc);
            properties = new List<TDL_Property>();
            if (!ConnectionEK.OdbcExists || !ConnectionEK.TableExists(tTDL))
            {
                return false;
            }
            string orderBy = $"ORDER BY page_no, bit_start";
            string sqlQuery = ConnectionEK.CreateSelectQuery(tTDL, where: null, orderBy: orderBy);
            DataTable dt = new DataTable("tTDL");
            if (ConnectionEK.Select(sqlQuery, ref dt))
            {
                foreach (DataRow row in dt.Rows)
                {
                    var property = row.ToObjectNewClass<TDLproperty>();
                    properties.Add(property.TDL_Property);
                }
            }
            return properties.Any();
        }

        //private static void ADD_TDLpage_Properties(ref Dictionary<int, Dictionary<string, TDL_Property>> _TDLproperties, TDL_Property property)
        //{
        //    if (_TDLproperties.ContainsKey(property.PageNo))
        //    {
        //        if (!_TDLproperties[property.PageNo].ContainsKey(property.Property))
        //        {
        //            _TDLproperties[property.PageNo].Add(property.Property, property);
        //        }
        //    }
        //    else
        //    {
        //        _TDLproperties.Add(property.PageNo, new Dictionary<string, TDL_Property>(StringComparer.OrdinalIgnoreCase));
        //        _TDLproperties[property.PageNo].Add(property.Property, property);
        //    }
        //}

        //public static bool Get_TDL_Page(this int pageNo, string odbc, ref Dictionary<int, Dictionary<string, TDLproperty>> _TDLproperties)
        //{
        //    string where = $" WHERE page_no = '{pageNo}' ORDER BY bit_start";
        //    string sqlQuery = Select_tTDL + where;
        //    DataTable dt = new DataTable("tTDL");
        //    _TDLproperties = new Dictionary<int, Dictionary<string, TDLproperty>>();
        //    ODBC_TT.ODBC.DB_Get_Data(odbc, sqlQuery, ref dt, out int count);
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        TDLproperty prop = row.ToObjectNewClass<TDLproperty>();
        //        if (_TDLproperties[prop.page_no][prop.property_tag] == null)
        //        {
        //            _TDLproperties.Add(prop.page_no, new Dictionary<string, TDLproperty>());
        //            _TDLproperties[prop.page_no].Add(prop.property_tag, prop);
        //        }
        //    }
        //    return count > 0;
        //}

        //public static TDLproperty GetTDLproperty(int pageNo, string propertyTag, ref Dictionary<int, Dictionary<string, TDLproperty>> _TDLproperties)
        //{
        //    return _TDLproperties[pageNo][propertyTag];
        //}

        //private static string SQL_TDL_Page15 = $"{Select_tTDL} WHERE page_no = 15";
        //private static string SQL_TDL_FWversion = $"{SQL_TDL_Page15} AND bit_start = 104";
        //public static bool Get_TDL_FWversion(string odbc, out TDLproperty property)
        //{
        //    DataTable dt = new DataTable("tTDL");
        //    ODBC_TT.ODBC.DB_Get_Data(odbc, SQL_TDL_FWversion, ref dt, out int count);
        //    if (count > 0)
        //    { property = dt.Rows[0].ToObjectNewClass<TDLproperty>(); return true; }
        //    else { property = new TDLproperty(); }
        //    return false;
        //}
        //public static bool Get_TDL_Page15(string odbc, out List<TDLproperty> properties)
        //{
        //    DataTable dt = new DataTable("tTDL");
        //    ODBC_TT.ODBC.DB_Get_Data(odbc, $"{SQL_TDL_Page15} Order by page_no, bit_start", ref dt, out int count);
        //    properties = new List<TDLproperty>();
        //    if (count > 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            TDLproperty prop = row.ToObjectNewClass<TDLproperty>();
        //            properties.Add(prop);
        //        }
        //    }
        //    return count > 0;
        //}
        #endregion

        #region EK Duration

        /***************************************************
         *  FUNCTION:       TDL Properties
         *  DESCRIPTION:
         ***************************************************/
        private static bool Get_Duration(string odbc, string item, out double AVGduration, out string errormessage)
        {
            AVGduration = 0;
            errormessage = "";
            if (CreateOdbcEk(odbc))
            {
                string where = $" WHERE {MN_item} = '{item}' AND {MN_Test_ok} = 1 AND {MN_Duration} > 0";
                string sqlQuery = $"SELECT AVG({MN_Duration}) FROM {tCalMeasVal} {where}";
                DataTable dt = new DataTable("Duration");
                if (ConnectionEK.Select(sqlQuery, ref dt))
                {
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            AVGduration = dt.Rows[0][0].ToString().ToDouble();
                            return true;
                        }
                        catch
                        {
                            errormessage = "ERROR: DB duration fail";
                            return false;
                        }
                    }
                }
                errormessage = "ERROR: DB No Data";
                return dt.Rows.Count > 0;
            }
            errormessage = "ERROR: DB Connection";
            return false;
        }
        #endregion

        /****************************************************************************************************
         * SQL:     Query
         ***************************************************************************************************/
        public const string DTname_Meas = "Measurement";
        public const string DTname_Prog = "Progress";

        public const string DTname_Cal = "tCalMeasVal";

        private static string _Select_tCalMeasVal = $"SELECT * FROM {tCalMeasVal} ";

        private static string _Select_tTDL = $"SELECT * FROM {tTDL} ";

        #region LoadLimits
        /***************************************************
         *  FUNCTION:       TDL Properties
         *  DESCRIPTION:
         ***************************************************/
        public const string DTname_Limits = "Limits";

        private static string _JoinLimitTechnology = $"SELECT [{tLimitValEval}].[{MN_item}], [{tLimitValEval}].[active] AS item_active, [{tLimitValEval}].[Technology_ID], " +
            $"{tTechnology}.*,{tTechnology}.[desc] AS technology_desc, {tCalMode}.* " +
            $" FROM {tLimitValEval} " +
            $"INNER JOIN {tTechnology} ON {tLimitValEval}.Technology_ID = {tTechnology}.Technology_ID " +
            $"INNER JOIN {tCalMode} ON {tTechnology}.CalMode_ID = {tCalMode}.CalMode_ID ";
        public static bool Get_Limits(ref ChannelValues item, out string errorMessage)
        {
            if (Get_tLimits(ref item))
            {
                if (Get_tCalMeasVal(ref item, out errorMessage))
                {
                    if (Get_Duration(item.ODBC_EK, item.item, out double avg, out errorMessage))
                    {
                        if (avg > 0)
                        {
                            item.Cal_Duration = avg;
                        }
                    }
                }
                return true;
            }
            errorMessage = "ERROR: DB Limits Art-Nr nicht gefunden";
            return false;
        }

        private static bool Get_tLimits(ref ChannelValues item)
        {
            if (CreateOdbcEk(item.ODBC_EK))
            {
                string where = $" WHERE {tLimitValEval}.{MN_item} = '{item.item}' AND {tLimitValEval}.active = 1 AND {tTechnology}.active = 1";
                string sqlQuery = $"{_JoinLimitTechnology} {where}";
                item.DT_Limits = new DataTable(DTname_Limits);
                if (ConnectionEK.Select(sqlQuery, ref item.DT_Limits))
                {
                    try
                    {
                        if (item.DT_Limits.Rows.Count > 0)
                        {
                            item = item.DT_Limits.Rows[0].ToObjectLoad(item);
                            return true;
                        }
                    }
                    catch { }
                }
            }
            return false;
        }

        private static bool Get_tCalMeasVal(ref ChannelValues item, out string errorMessage)
        {
            errorMessage = null;
            if (CreateOdbcEk(item.ODBC_EK))
            {
                string where = $"WHERE {MN_sensorid} = {item.sensor_id} ";
                string sqlQuery = _Select_tCalMeasVal + where + $"ORDER BY {MN_pass_no} DESC";
                item.DT_tCalMeasVal = new DataTable(tCalMeasVal);
                if (ConnectionEK.Select(sqlQuery, ref item.DT_tCalMeasVal))
                {
                    try
                    {
                        if (item.DT_tCalMeasVal.Rows.Count > 0)
                        {
                            item.pass_no = (item.DT_tCalMeasVal.Rows[0].Field<int>(MN_pass_no) + 1);
                            return true;
                        }
                        item.pass_no = 1;
                        return true;
                    }
                    catch
                    {
                        errorMessage = "ERROR: Load tCalMeasVal";
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion

        /****************************************************************************************************
         * SQL:     DataBase Insert procedure
         ***************************************************************************************************/
        private static bool Set_DB(string odbc, string sqlQuery, bool closeDBconnection = true)
        {
            bool result = false;
            if (CreateOdbcEk(odbc))
            {
                //string connection = ODBC_TT.ODBC.ODBC_Conn_String(odbc, out bool e);
                using (OdbcConnection con = ConnectionEK.NewConnection())
                {
                    using (OdbcCommand cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        try
                        {
                            if (con.State != ConnectionState.Open) { con.Open(); }
                            int count = cmd.ExecuteNonQuery();
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(sqlQuery + Environment.NewLine + ex.Message, ex);
                        }
                        finally
                        {
                            if (con.State != ConnectionState.Closed) { con.Close(); }
                        }
                        cmd.Dispose();
                    }
                    con.Dispose();
                }
            }
            return result;
        }

        /****************************************************************************************************
         * SQL:     Insert MeasVal
         ***************************************************************************************************/
        public static void MeasValTemp_Insert(ChannelValues limits, DataTable dt)
        {
            DataTable dts = new DataTable();
            int procOrder = 1;
            DateTime meas_time_start = DateTime.Now;
            DateTime meas_time_end = DateTime.Now;
            StringBuilder sbHeader;
            StringBuilder sbValues;
            int RowsCount = dt.Rows.Count;
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                sbHeader = new StringBuilder();
                sbValues = new StringBuilder();
                sbHeader.Append($"[{MN_sensorid}], [{MN_pass_no}], [{MN_ProcOrder}], [{MN_Channelno}]");
                sbValues.Append($"{limits.sensor_id}, {limits.pass_no}, '{procOrder}', {limits.channel_no}");
                foreach (DataColumn item in dt.Columns)
                {
                    DB_InserValue(ref sbHeader, ref sbValues, item.ColumnName, row, true);
                }
                //DB_InserValue(ref sbHeader, ref sbValues, MN_BoxMode_HEX, row, true);
                //DB_InserValue(ref sbHeader, ref sbValues, MN_BoxMode_Desc, row, true);
                //DB_InserValue(ref sbHeader, ref sbValues, MN_meas_time_start, row, true);
                //DB_InserValue(ref sbHeader, ref sbValues, MN_meas_time_end, row, true);
                //DB_InserValue(ref sbHeader, ref sbValues, MN_Duration, row, false);
                //DB_InserValue(ref sbHeader, ref sbValues, MN_Values, row, true);
                //string boxerror_hex = DB_InserValue(ref sbHeader, ref sbValues, MN_BoxErrorCode_HEX, row, true);
                bool test_ok = false;
                switch (row[MN_BoxErrorMode_HEX])
                {
                    case "0":
                    case "00":
                    case "2":
                        test_ok = true;
                        break;
                    default:
                        break;
                }
                //DB_InserValue(ref sbHeader, ref sbValues, MN_BoxErrorCode_Desc, row, true);
                sbHeader.Append($", [{MN_Test_ok}]"); sbValues.Append($", '{test_ok}'");
                string sqlQuery = $"INSERT INTO {tCalMeasValTemp} ({sbHeader}) VALUES ({sbValues}); ";
                i++;
                if (!Set_DB(limits.ODBC_EK, sqlQuery, i == RowsCount))
                {
                    //break;
                }
                procOrder++;
            }
        }

        private static void DB_InserValue(ref StringBuilder sbHeader, ref StringBuilder sbValue, string header, DataRow row, bool dbIsString = true)
        {
            string value = "";
            try
            {
                if (header.Contains("meas_time"))
                {
                    value = row[header].ToString();
                    if (value.Length > 0)
                    {
                        var dt = Convert.ToDateTime(value);
                        value = dt.ToSQLstring();
                    }
                }
                else
                {
                    value = row[header].ToString();
                }
                DB_InserValue(ref sbHeader, ref sbValue, header, value, dbIsString);
            }
            catch
            {

            }
        }

        private static void DB_InserValue(ref StringBuilder sbHeader, ref StringBuilder sbValue, string header, double? value, bool dbIsString = true)
        {
            if (value != null)
            {
                DB_InserValue(ref sbHeader, ref sbValue, header, value?.ToString(), dbIsString);
            }
        }
        private static void DB_InserValue(ref StringBuilder sbHeader, ref StringBuilder sbValue, string header, string value, bool dbIsString = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    sbHeader.Append($", [{header}]");
                    switch (header)
                    {
                        case MN_Values:
                            int i = value.IndexOf(MN_I_set);
                            if (i > 0)
                            {
                                value = value.Substring(i);
                            }
                            break;
                        default:
                            break;
                    }
                    if (dbIsString)
                    {
                        sbValue.Append($", '{value}'");
                    }
                    else
                    {
                        sbValue.Append($", {value}");
                    }
                }
            }
            catch
            {

            }
        }

        private static string _SQL_tMeasVal_Header_Insert_Init = $"INSERT INTO {tCalMeasVal} " +
            $"([{MN_sensorid}], [{MN_pass_no}], [Technology_ID], [ProductionType_ID], [{MN_item}], [{MN_pdno}], [{MN_Test_ok}], [{MN_meas_time_start}], " +
            $"[User_ID], [{MN_EK_SWversion}], [CalMode_ID], [{MN_Channelno}]," +
            $"[{MN_Sample_FWversion}], [{MN_Device_FWversion}])";
        public static bool MeasVal_Insert_Init(ChannelValues limits)
        {
            string InsertValues = $"{limits.sensor_id}, '{limits.pass_no}', {limits.Technology_ID}, {limits.ProductionType_ID}, " +
                $"'{limits.item}','{limits.pdno}', '{limits.test_ok}'," +
                $"'{limits.meas_time_start.ToSQLstring()}', {limits.User_ID}, '{limits.EK_SW_Version}', {limits.CalMode_ID}, {limits.channel_no}, '0' ";
            if (limits.DeviceLimits == null)
            {
                InsertValues += $", '','' ";
            }
            else { InsertValues += $", '{limits.DeviceLimits.FW_Version}' "; }
            string sqlQuery = $"{_SQL_tMeasVal_Header_Insert_Init} Values({InsertValues});";
            return Set_DB(limits.ODBC_EK, sqlQuery);
        }

        public static bool MeasVal_Update(ChannelValues limits)
        {
            limits.meas_time_end = DateTime.Now;
            string where = $"WHERE sensor_id = {limits.sensor_id} AND {MN_pass_no} = {limits.pass_no}";
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE {tCalMeasVal} SET ");
            sb.Append($"{MN_Test_ok} = '{limits.test_ok}'");
            sb.Append($", {MN_meas_time_end} = '{limits.meas_time_end.ToSQLstring()}'");
            if (!string.IsNullOrEmpty(limits.sample_FW_Version_value)) { sb.Append($", {MN_Sample_FWversion} = '{limits.sample_FW_Version_value}'"); }
            sb.Append($", {MN_Sample_FWversion_state} = '{limits.sample_FW_Version_state}'");
            sb.Append($", {MN_Duration} = {limits.meas_time_duration.TotalSeconds}");
            sb.Append($", error_no = '{limits.error_no}'");
            string sqlQuery = $"{sb} {where}; ";
            if (!Set_DB(limits.ODBC_EK, sqlQuery))
            {
                //ErrorHandler("MeasVal_Update", message: sqlQuery);
                return false;
            }
            else { return true; }
        }

        /****************************************************************************************************
         * SQL:     Insert Log Values
         ***************************************************************************************************/
        public static void Set_Log(string odbc, DeviceResponseValues drv, ChannelValues limits)
        {
            if (drv.OpCodeResp == OpCode.g901)
            {
                StringBuilder sbHeader = new StringBuilder();
                StringBuilder sbValues = new StringBuilder();
                sbHeader.Append($"{MN_sensorid}, {MN_pass_no}, {MN_ProcOrder}, {MN_Channelno}");
                sbValues.Append($"{limits.sensor_id}, {limits.pass_no}, '{limits.procCounter}', {limits.channel_no}");
                DB_InserValue(ref sbHeader, ref sbValues, MN_BoxMode_HEX, drv.BoxMode.Hex, true);
                DB_InserValue(ref sbHeader, ref sbValues, MN_BoxMode_Desc, drv.BoxMode.Desc, true);
                if (drv.meas_time_start != null)
                {
                    if (drv.meas_time_start.Year > 1)
                    {
                        DB_InserValue(ref sbHeader, ref sbValues, MN_meas_time_start, drv.meas_time_start.ToSQLstring(), true);
                    }
                }
                DB_InserValue(ref sbHeader, ref sbValues, MN_meas_time_end, DateTime.Now.ToSQLstring(), true);

                if (limits.Cal_Stopwatch.IsRunning)
                {
                    sbHeader.Append(", " + MN_Duration);
                    sbValues.Append($", '{limits.Cal_Stopwatch.Elapsed.TotalSeconds}'");
                }
                if (drv.MeasValues.Meas_I.IsMeasValue)
                {
                    DB_InserValue(ref sbHeader, ref sbValues, MN_I_set, drv.MeasValues.ISet.ValueNumeric, true);
                    DB_InserValue(ref sbHeader, ref sbValues, MN_I_AVG, drv.MeasValues.IAvg.ValueNumeric, true);
                    DB_InserValue(ref sbHeader, ref sbValues, MN_I_StdDev, drv.MeasValues.IStdDev.ValueNumeric, true);
                    DB_InserValue(ref sbHeader, ref sbValues, MN_I_Error, drv.MeasValues.IError.ValueNumeric, true);
                }
                if (drv.MeasValues.Meas_Temp.IsMeasValue)
                {
                    DB_InserValue(ref sbHeader, ref sbValues, MN_Temp_set, drv.MeasValues.TempSet.ValueNumeric, true);
                    DB_InserValue(ref sbHeader, ref sbValues, MN_Temp_AVG, drv.MeasValues.TempAvg.ValueNumeric, true);
                    DB_InserValue(ref sbHeader, ref sbValues, MN_Temp_StdDev, drv.MeasValues.TempStdDev.ValueNumeric, true);
                    DB_InserValue(ref sbHeader, ref sbValues, MN_Temp_Error, drv.MeasValues.TempError.ValueNumeric, true);
                }
                DB_InserValue(ref sbHeader, ref sbValues, MN_BoxErrorMode_HEX, drv.BoxErrorMode?.Hex, true);
                bool test_ok = false;
                switch (drv.BoxErrorMode?.Hex)
                {
                    case null:
                    case "0":
                    case "00":
                    case "2":
                        test_ok = true;
                        break;
                    default:
                        break;
                }
                DB_InserValue(ref sbHeader, ref sbValues, MN_BoxErrorMode_Desc, drv.BoxErrorMode.Desc, true);
                sbHeader.Append(", " + MN_Test_ok); sbValues.Append($", '{test_ok}'");
                string sqlQuery = $"INSERT INTO {tCalLog} ({sbHeader}) VALUES ({sbValues}); ";
                Set_DB(odbc, sqlQuery, false);
            }
        }
    }
}
