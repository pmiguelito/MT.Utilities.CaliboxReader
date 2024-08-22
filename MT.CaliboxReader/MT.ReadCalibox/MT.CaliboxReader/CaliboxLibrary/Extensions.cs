using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CaliboxLibrary
{
    public static class Extensions
    {

        /*******************************************************************************************************************
        * Converter:
        '*******************************************************************************************************************/
        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return 0;
        }

        public static double ToDouble(this string value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            return 0;
        }

        /*****************************************************************************
        * SQL Helpers:  Row to Properties
        '****************************************************************************/
        /// <summary>
        /// Get values to class properties from DataTable based on ColumnNames
        /// <para>properties must be set with "get/set"</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T ToObjectNewClass<T>(this DataRow dataRow) where T : new()
        {
            return dataRow.ToObjectNewClass<T>(out List<string> NotFound);
        }
        /// <summary>
        /// Get values to class properties from DataTable based on ColumnNames
        /// <para>properties must be set with "get/set"</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T ToObjectNewClass<T>(this DataRow dataRow, out List<string> NotFound) where T : new()
        {
            T item = new T();
            NotFound = new List<string>();
            Type type = item.GetType();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                try
                {
                    PropertyInfo property = type.GetProperty(column.ColumnName, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property != null)
                    {
                        if (dataRow[column] != DBNull.Value)
                        {
                            object result = Convert.ChangeType(dataRow[column], property.PropertyType);
                            property.SetValue(item, result, null);
                        }
                    }
                    else if (property == null)
                    {
                        NotFound.Add(column.ColumnName);
                    }
                }
                catch { }
            }
            return item;
        }

        public static T ToObjectLoad<T>(this DataRow dataRow, T item)
        {
            return dataRow.ToObjectLoad(item, out _);
        }
        public static T ToObjectLoad<T>(this DataRow dataRow, T item, out List<string> NotFound)
        {
            NotFound = new List<string>();
            Type type = item.GetType();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                try
                {
                    PropertyInfo property = type.GetProperty(column.ColumnName, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property != null)
                    {
                        if (dataRow[column] != DBNull.Value)
                        {
                            object result = Convert.ChangeType(dataRow[column], property.PropertyType);
                            property.SetValue(item, result, null);
                        }
                    }
                    else if (property == null)
                    {
                        NotFound.Add(column.ColumnName);
                    }
                }
                catch
                {
                    NotFound.Add(column.ColumnName);
                }
            }
            return item;
        }


        /// <summary>
        /// int id = dataRow.FieldOrDefault<int>("Id");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T FieldOrDefault<T>(this DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? default(T) : row.Field<T>(columnName);
        }
        /// <summary>
        /// int id = DataGridViewRow.FieldOrDefault<int>("Id");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T FieldOrDefault<T>(this DataGridViewRow row, string columnName)
        {
            return string.IsNullOrEmpty(row.Cells[columnName].Value.ToString()) ? default(T) : (T)row.Cells[columnName].Value;
        }

        public static DataTable ToDataTable<T>(this List<T> self, DataTable dataTable = null, string tableName = null)
        {
            var properties = typeof(T).GetProperties();
            if (dataTable == null)
            {
                dataTable = new DataTable(tableName);
                foreach (var info in properties)
                {
                    dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType);
                }
            }
            foreach (var entity in self)
            {
                dataTable.Rows.Add(properties.Select(p => p.GetValue(entity)).ToArray());
            }
            return dataTable;
        }
        public static DataTable ToDataTable<T>(this T self, DataTable dataTable = null, string tableName = null)
        {
            var properties = typeof(T).GetProperties();
            if (dataTable == null)
            {
                dataTable = new DataTable(tableName);
                foreach (var info in properties)
                {
                    dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType);
                }
            }
            dataTable.Rows.Add(properties.Select(p => p.GetValue(self)).ToArray());
            return dataTable;
        }
        public static DataTable ToDataTable<T>(this DataTable dataTable, string tableName = null)
        {
            var properties = typeof(T).GetProperties();
            dataTable = new DataTable(tableName);
            foreach (var info in properties)
            {
                dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType);
            }
            return dataTable;
        }


        #region Text
        /**********************************************************
        * FUNCTION:     Text
        * DESCRIPTION:
        ***********************************************************/

        /*******************************************************************************************************************
        * Attends:
        '*******************************************************************************************************************/
        public static string Attempts(this int count, int total)
        {
            return $"attempts: {count.ToString().PadLeft(2, '0')}/{total.ToString().PadLeft(2, '0')}";
        }

        public static string TimeDiff(this double timeDiff)
        {
            TimeSpan ts = DateTime.Now.AddSeconds(timeDiff) - DateTime.Now;
            return TimeDiff(ts);
        }
        public static string TimeDiff(this TimeSpan timeDiff)
        {
            return $"wait... {timeDiff.ToStringMin()}";
        }
        public static string ToStringMin(this TimeSpan ts)
        {
            return $"{ts.TotalMinutes.ToString("N0").PadLeft(2, '0')}:{Math.Abs(ts.Seconds).ToString("N0").PadLeft(2, '0')} min";
        }
        #endregion

        public static string ToSQLstring(this DateTime dt, bool withMilliSeconds = true)
        {
            if (withMilliSeconds)
            {
                return dt.ToString("MM.dd.yyyy HH:mm:ss.fff");
            }
            else
            {
                return dt.ToString("MM.dd.yyyy HH:mm:ss");
            }
        }
    }
}
