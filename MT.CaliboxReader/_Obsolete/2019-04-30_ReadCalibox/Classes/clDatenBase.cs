using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STDhelper;
using static STDhelper.clSTD;
using static STDhelper.clLogging;

namespace ReadCalibox
{
    public static class clDatenBase
    {
        /****************************************************************************************************
         * tables:
         ***************************************************************************************************/
        public const string tLimitValEval = "tLimitValEval";
        public const string tTechnology = "tTechnology";
        public const string tCalMeasVal = "tCalMeasVal";
        public const string tCalMeasValTemp = "tCalMeasValTemp";
        public const string tCalLog = "tCalLog";
        public const string tTDL = "tTDL";

        /****************************************************************************************************
         * SQL:     Truncate Tables
         *          use this only for tests
         ***************************************************************************************************/

        public static void Truncate_DB(string odbc)
        {
            ODBC_TT.ODBC.DB_Truncate(odbc, tCalLog, false);
            ODBC_TT.ODBC.DB_Truncate(odbc, tCalMeasValTemp, false);
            ODBC_TT.ODBC.DB_Truncate(odbc, tCalMeasVal);
        }


        /****************************************************************************************************
         * SQL:     Query
         ***************************************************************************************************/

        const string JoinLimitTechnology = "SELECT tLimitValEval.item, tLimitValEval.active AS item_active, " +
            "tLimitValEval.Technology_ID, tTechnology.active AS technology_active, tTechnology.[desc] as technology_desc, " +
            "tTechnology.pol_voltage, tTechnology.pol_voltage_active, tTechnology.pol_voltage_cal_multi, " +
            "tTechnology.sample_FW_Version_active, sample_FW_Version " +
            "FROM tLimitValEval INNER JOIN tTechnology ON tLimitValEval.Technology_ID = tTechnology.Technology_ID ";

        static string Select_tCalMeasVal = $"SELECT * FROM {tCalMeasVal} ";

        static string Select_tTDL = $"SELECT * FROM {tTDL} ";

        /****************************************************************************************************
         * SQL:     Checks
         ***************************************************************************************************/

        static bool ODBC_Initial_Found { get { return UC_Betrieb.ODBC_Initial_Found; } }

        public static bool Check_ODBC(string odbc, out string errormessage)
        {
            errormessage = "";
            if (!ODBC_TT.ODBC.GetODBC_status(odbc))
            {
                errormessage = $"ERROR: ODBC {odbc} communication fail";
                return true;
            }
            return true;
        }


        /****************************************************************************************************
         * SQL:     GET Limits
         ***************************************************************************************************/
        public static bool Get_Limits(string odbc, string item, string sensorID, out clItemLimits limits, out string errormessage)
        {
            limits = new clItemLimits();
            errormessage = "";
            if (ODBC_Initial_Found)
            {
                int count = 0;
                if (Check_ODBC(odbc, out errormessage))
                {
                    string where = $" WHERE {tLimitValEval}.item = '{item}' AND {tLimitValEval}.active = 1 AND {tTechnology}.active = 1";
                    string sqlQuery = JoinLimitTechnology + where;
                    DataTable dt = new DataTable("Limits");
                    ODBC_TT.ODBC.Get_DataFrom_DB(odbc, sqlQuery, ref dt, out count);
                    if (count > 0)
                    {
                        try
                        {
                            DataRow row = dt.Rows[0];
                            limits = row.ToObject<clItemLimits>();
                            if (limits.pol_voltage == 0)
                            { errormessage = $"ERROR: polarisation {limits.pol_voltage}"; return false; }
                            else
                            {
                                dt = new DataTable(tCalMeasVal);
                                where = $"WHERE sensor_id = {sensorID}";
                                sqlQuery = Select_tCalMeasVal + where + "ORDER BY pass_no DESC";
                                ODBC_TT.ODBC.Get_DataFrom_DB(odbc, sqlQuery, ref dt, out int countPassNo);
                                if (countPassNo > 0)
                                {
                                    limits.pass_no = (dt.Rows[0].Field<int>("pass_no") + 1 );
                                }
                                else { limits.pass_no = 1; }
                            }
                        }
                        catch
                        {
                            errormessage = "ERROR: DB Limits Technology Variablen fail";
                            return false;
                        }
                    }
                    //else { errormessage = "ERROR: DB Limits Art-Nr nicht gefunden"; }
                }
                return count > 0;
            }
            else { return true; }
        }

        /****************************************************************************************************
         * SQL:     GET TDL
         ***************************************************************************************************/

        public static bool Get_TDL_Page(this int pageNo, string odbc, out List<clTDLproperty> pageProperties)
        {
            string where = $" WHERE page_no = '{pageNo}' ORDER BY order_no";
            string sqlQuery = Select_tTDL + where;
            DataTable dt = new DataTable("tTDL");

            ODBC_TT.ODBC.Get_DataFrom_DB(odbc, sqlQuery, ref dt, out int count);
            pageProperties = new List<clTDLproperty>();
            foreach(DataRow row in dt.Rows)
            { pageProperties.Add(row.ToObject<clTDLproperty>()); }
            return count>0;
        }


        private static string SQL_TDL_FWversion = $"{Select_tTDL} WHERE page_no = 15 AND bit_start = 104";
        public static bool Get_TDL_FWversion(string odbc, out clTDLproperty property)
        {
            DataTable dt = new DataTable("tTDL");
            ODBC_TT.ODBC.Get_DataFrom_DB(odbc, SQL_TDL_FWversion, ref dt, out int count);
            if (count > 0)
            { property = dt.Rows[0].ToObject<clTDLproperty>(); }
            else { property = new clTDLproperty(); }
            return count > 0;
        }

        /****************************************************************************************************
         * SQL:     DataBase Insert procedure
         ***************************************************************************************************/
        private static bool Set_DB(string odbc, string sqlQuery, bool closeDBconnection=true)
        {
            try
            {
                string connection = ODBC_TT.ODBC.ODBC_Conn_String(odbc, out bool e);
                using (OdbcConnection con = new OdbcConnection(connection))
                {
                    using (OdbcCommand cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        try
                        {
                            if (con.State == ConnectionState.Closed) { con.Open(); }
                            int count = cmd.ExecuteNonQuery();
                        }
                        catch (Exception aa)
                        {
                            ErrorHandler("Set_DB", exception: aa, information: $"ODBC: {odbc}\r\n{sqlQuery}");
                            return false;
                        }
                        finally
                        { if (con.State == ConnectionState.Open) { con.Close(); } }
                    }
                }
            }
            catch (Exception ee)
            { return false; }
            return true;
        }

        /****************************************************************************************************
         * SQL:     Insert MeasVal
         ***************************************************************************************************/

        public static void MeasValTemp_Insert(string odbc, string sensorID, string passNo, DataTable dt)
        {
            DataTable dts = new DataTable();
            int procOrder = 1;
            DateTime meas_time_start = DateTime.Now;
            DateTime meas_time_end = DateTime.Now;
            StringBuilder InsertHeader;
            StringBuilder InsertValues;
            bool durationStart = false;
            bool durationEnd = false;
            double durationInt = 0;
            int RowsCount = dt.Rows.Count;
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                InsertHeader = new StringBuilder();
                InsertValues = new StringBuilder();
                InsertHeader.Append("sensor_id, pass_no, ProcOrder");
                InsertValues.Append($"{sensorID}, {passNo}, '{procOrder}'");
                durationStart = false;
                durationEnd = false;

                string boxmode_hex = row["boxmode_hex"].ToString();
                if(!string.IsNullOrEmpty(boxmode_hex)) { InsertHeader.Append(", boxmode_hex");  InsertValues.Append($", '{boxmode_hex}'"); }
                string boxmode_desc = row["boxmode_desc"].ToString();
                if (!string.IsNullOrEmpty(boxmode_desc)) { InsertHeader.Append(", boxmode_desc"); InsertValues.Append($", '{boxmode_desc}'"); }
                try
                {
                    meas_time_start = row.Field<DateTime>("meas_time_start");
                    if (meas_time_start != null)
                    {
                        if (meas_time_start.Year > 1)
                        { InsertHeader.Append(", meas_time_start"); InsertValues.Append($", '{meas_time_start.ToString("MM.dd.yyyy HH:mm:ss.mmm")}'"); durationStart = true; }
                    }
                }
                catch { }
                try
                {
                    meas_time_end = row.Field<DateTime>("meas_time_end");
                    if (meas_time_end != null)
                    {
                        if (meas_time_end.Year > 1)
                        { InsertHeader.Append(", meas_time_end"); InsertValues.Append($", '{meas_time_end.ToString("MM.dd.yyyy HH:mm:ss.mmm")}'"); durationEnd = true; }
                    }
                }
                catch { }
                string duration = row["duration"].ToString();
                if (!string.IsNullOrEmpty(duration)) { InsertHeader.Append(", duration"); InsertValues.Append($", {duration}"); }
                else
                {
                    if (durationStart && durationEnd)
                    {
                        try
                        {
                            durationInt = (meas_time_end - meas_time_start).TotalSeconds;
                            InsertHeader.Append(", duration"); InsertValues.Append($", {durationInt}");
                        } catch { }
                    }
                }
                string values = row["values"].ToString();
                if (!string.IsNullOrEmpty(values)) { InsertHeader.Append(", [values]"); InsertValues.Append($", '{values}'"); }
                string boxerror_hex = row["boxerror_hex"].ToString();
                if (!string.IsNullOrEmpty(boxerror_hex)) { InsertHeader.Append(", boxerrorcode_hex"); InsertValues.Append($", '{boxerror_hex}'"); }
                string boxerror_desc = row["boxerror_desc"].ToString();
                if (!string.IsNullOrEmpty(boxerror_desc)) { InsertHeader.Append(", boxerrorcode_desc"); InsertValues.Append($", '{boxerror_desc}'"); }
                bool test_ok = boxerror_hex.ToString() != "00" ? false : true;
                string sqlQuery = $"INSERT INTO {tCalMeasValTemp} ({InsertHeader}) VALUES ({InsertValues}); ";
                i++;
                if(!Set_DB(odbc, sqlQuery, i==RowsCount))
                {
                    //break;
                }
                procOrder++;
            }
        }

        private static string SQL_tMeasVal_Header_Insert = $"INSERT INTO {tCalMeasVal} (Sensor_ID, pass_no, " +
            "Technology_ID, ProductionType_ID, item, pdno, test_ok, meas_time_start, meas_time_end, " +
            "User_ID,EK_SW_Version, error_no, sample_FW_Version, sample_FW_Version_state, pol_voltage, pol_voltage_cal_multi,duration)";
        public static bool MeasVal_Insert(string odbc, clItemLimits limits)
        {
            double duration = (limits.meas_time_end - limits.meas_time_start).TotalSeconds;
            string InsertValues = $"{limits.sensor_id}, '{limits.pass_no}', " +
                $"{limits.Technology_ID}, {limits.ProductionType_ID}, '{limits.item}','{limits.pdno}', '{limits.test_ok}'," +
                $"'{limits.meas_time_start.ToString("MM.dd.yyyy HH:mm:ss")}', " +
                $"'{limits.meas_time_end.ToString("MM.dd.yyyy HH:mm:ss")}', " +
                $"{limits.User_ID}, '{limits.EK_SW_Version}', "+
                $"'{limits.error_no}', '{limits.sample_FW_Version_value}', '{limits.sample_FW_Version_state}', " +
                $" '{limits.pol_voltage}', '{limits.pol_voltage_cal_multi}', {duration}";

            string sqlQuery = $"{SQL_tMeasVal_Header_Insert} Values({InsertValues});";
            return Set_DB(odbc, sqlQuery);
        }

        private static string SQL_tMeasVal_Header_Insert_Init = $"INSERT INTO {tCalMeasVal} (Sensor_ID, pass_no, Technology_ID, ProductionType_ID, item, pdno, test_ok, meas_time_start, User_ID, EK_SW_Version)" ;
        public static bool MeasVal_Insert_Init(string odbc, clItemLimits limits)
        {
            string InsertValues = $"{limits.sensor_id}, '{limits.pass_no}', {limits.Technology_ID}, {limits.ProductionType_ID}, " +
                $"'{limits.item}','{limits.pdno}', '{limits.test_ok}'," +
                $"'{limits.meas_time_start.ToString("MM.dd.yyyy HH:mm:ss")}', {limits.User_ID}, '{limits.EK_SW_Version}'";
            string sqlQuery = $"{SQL_tMeasVal_Header_Insert_Init} Values({InsertValues});";
            return Set_DB(odbc, sqlQuery);
        }

        public static bool MeasVal_Update(string odbc, clItemLimits limits)
        {
            double duration = 0;
            try { duration = (limits.meas_time_end - limits.meas_time_start).TotalSeconds; } catch { }
            string where = $"WHERE sensor_id = {limits.sensor_id} AND pass_no = {limits.pass_no}";
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE {tCalMeasVal} SET ");
            sb.Append($"test_ok = '{limits.test_ok}'");
            sb.Append($", meas_time_end = '{limits.meas_time_end.ToString("MM.dd.yyyy HH:mm:ss")}'");
            sb.Append($", sample_FW_Version = '{limits.sample_FW_Version_value}'");
            sb.Append($", sample_FW_Version_state = '{limits.sample_FW_Version_state}'");
            sb.Append($", pol_voltage = '{limits.pol_voltage}'");
            sb.Append($", pol_voltage_cal_multi = '{limits.pol_voltage_cal_multi}'");
            sb.Append($", duration = {duration}");
            sb.Append($", error_no = '{limits.error_no}'");
            string sqlQuery = $"{sb} {where}; ";
            if(!Set_DB(odbc, sqlQuery))
            {
                ErrorHandler("MeasVal_Update", information: sqlQuery);
                return false;
            }
            else { return true; }
        }

        /****************************************************************************************************
         * SQL:     Insert Log Values
         ***************************************************************************************************/
        public static void Set_Log(string odbc, DeviceResponseValues drv, clItemLimits limits)
        {
            if (drv.OpCode == "g901") 
            {
                StringBuilder InsertHeader = new StringBuilder();
                StringBuilder InsertValues = new StringBuilder();
                InsertHeader.Append("sensor_id, pass_no, ProcOrder");
                InsertValues.Append($"{limits.sensor_id}, {limits.pass_no}, '{limits.procCounter}'");

                if (drv.BoxMode_hex!=null) { InsertHeader.Append(", boxmode_hex"); InsertValues.Append($", '{drv.BoxMode_hex}'"); }
                if (drv.BoxMode_desc!=null) { InsertHeader.Append(", boxmode_desc"); InsertValues.Append($", '{drv.BoxMode_desc}'"); }
                if (drv.meas_time_start != null)
                {
                    if (drv.meas_time_start.Year > 1)
                    { InsertHeader.Append(", meas_time_start"); InsertValues.Append($", '{drv.meas_time_start.ToString("MM.dd.yyyy HH:mm:ss.mmm")}'"); }
                }
                InsertHeader.Append(", meas_time_end"); InsertValues.Append($", '{DateTime.Now.ToString("MM.dd.yyyy HH:mm:ss.mmm")}'");
                //string duration = row["duration"].ToString();
                //if (duration.Length > 0) { InsertHeader.Append(", duration"; InsertValues.Append($", '{duration}'"; }
                if (drv.BoxMeasValue != null) { InsertHeader.Append(", refvalue"); InsertValues.Append($", '{drv.BoxMeasValue}'"); }
                if (drv.MeanValue != null) { InsertHeader.Append(", meanvalue"); InsertValues.Append($", '{drv.MeanValue}'"); }
                if (drv.StdDeviation != null) { InsertHeader.Append(", stddeviation"); InsertValues.Append($", '{drv.StdDeviation}'"); }
                if (drv.MeasErrorValue != null) { InsertHeader.Append(", errorvalue"); InsertValues.Append($", '{drv.MeasErrorValue}'"); }
                if (drv.BoxErrorCode_hex != null) { InsertHeader.Append(", boxerrorcode_hex"); InsertValues.Append($", '{drv.BoxErrorCode_hex}'"); }
                if (drv.BoxErrorCode_desc != null) { InsertHeader.Append(", boxerrorcode_desc"); InsertValues.Append($", '{drv.BoxErrorCode_desc}'"); }
                //bool test_ok = boxerror_hex.ToString() != "00" ? false : true;
                string sqlQuery = $"INSERT INTO {tCalLog} ({InsertHeader}) VALUES ({InsertValues}); ";
                Set_DB(odbc, sqlQuery, false);
            }
        }
    }
}
