using MT.OneWire;
using System.Collections.Generic;
using System.Linq;

namespace CaliboxLibrary
{
    public static class TdlData
    {
        #region Identification
        /**********************************************************
        * FUNCTION:     Identification
        * DESCRIPTION:
        ***********************************************************/
        public static Dictionary<string, TEDSTemplate> Templates { get; private set; } = new Dictionary<string, TEDSTemplate>();
        public static string Odbc { get; private set; }
        public static TEDSTemplate Template { get; private set; }
        #endregion

        #region Initialization
        /**********************************************************
        * FUNCTION:     Initialization
        * DESCRIPTION:
        ***********************************************************/
        public static void Initialization(string odbc)
        {
            if (!Templates.TryGetValue(odbc, out var values))
            {
                DataBase.Get_TDLpropertiesFromDB(odbc, out List<TDL_Property> properties);
                Odbc = odbc;
                Template = new TEDSTemplate(properties, TEDSTemplate.P16Types.O2);
                Templates.Add(odbc, Template);
                return;
            }
            Odbc = odbc;
            Template = values;
        }

        public static bool GetTemplate(string odbc, out TEDSTemplate template)
        {
            if (string.IsNullOrEmpty(odbc))
            {
                template = Template?.Copy();
                return template != null;
            }
            Initialization(odbc);
            if (Template != null)
            {
                template = Template.Copy();
                return true;
            }
            template = new TEDSTemplate();
            return false;
        }
        #endregion

        #region Get Properties
        /**********************************************************
        * FUNCTION:     Get Properties
        * DESCRIPTION:
        ***********************************************************/
        private static bool GetProperty(int pageNo, string prop, out TDL_Property property)
        {
            if (GetProperty(pageNo, out Dictionary<string, TDL_Property> properties))
            {
                bool result = properties.TryGetValue(prop, out property);
                if (!result)
                {
                    var search = prop.Replace("%", "").Trim().ToLower();
                    foreach (var item in properties.Values)
                    {
                        if (item.Property.ToLower().Contains(search))
                        {
                            property = item;
                            return true;
                        }
                    }
                }
                return result;
            }
            property = new TDL_Property();
            return false;
        }

        /// <summary>
        /// Get PageNumber properties
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static bool GetProperty(int pageNo, out Dictionary<string, TDL_Property> properties)
        {
            if (Template != null)
            {
                return Template.PropertiesByPagesAndProps.TryGetValue(pageNo, out properties);
            }
            properties = null;
            return false;
        }
        #endregion

        #region ReadProperties

        /**********************************************************
        * FUNCTION:     ReadProperties
        * DESCRIPTION:
        ***********************************************************/
        private static bool ReadPropertyValue(TDL_Property property, string hex, out string value)
        {
            try
            {
                value = property.ReadValue(hex);
                return !string.IsNullOrEmpty(value);
            }
            catch { }
            value = null;
            return false;
        }
        #endregion

        #region HEX Separators
        /**********************************************************
        * FUNCTION:     HEX Separators
        * DESCRIPTION:
        ***********************************************************/
        public static string RemoveSeparators(this string hex)
        {
            return hex.Replace("-", "").Replace(" ", "").Replace("\"", "");
        }

        public static string AddSeparator(this string hex)
        {
            hex = hex.RemoveSeparators();
            var split = Enumerable.Range(0, hex.Length / 2).Select(i => hex.Substring(i * 2, 2));
            hex = string.Join("-", split);
            return hex;
        }
        #endregion

        #region Page0
        /**********************************************************
        * FUNCTION:     Page0
        * DESCRIPTION:
        ***********************************************************/
        public const string Page00Default = "05-3F-80-B0-46-04-00-00-00-0D-C0-1D-3A-1D-00-00-00-00-30-03-64-04-C2-C1-76-00-78-00-78-24-56-03";
        public static TDL_Property Prop_PartNumber;
        public static TDL_Property Prop_SerialNumber;

        public const string PartNumber = "30646748";
        public static bool GetPartNumber(string hex, out string result)
        {
            result = null;
            if (Prop_PartNumber == null)
            {
                if (!GetProperty(00, "PartNumber", out Prop_PartNumber))
                {
                    return false;
                }
            }
            return ReadPropertyValue(Prop_PartNumber, hex, out result);
        }

        public static bool GetSerialNumber(string hex, out string result)
        {
            result = null;
            if (Prop_SerialNumber == null)
            {
                if (!GetProperty(00, "Attached_SerialNum", out Prop_SerialNumber))
                {
                    return false;
                }
            }
            return ReadPropertyValue(Prop_SerialNumber, hex, out result);
        }

        private static void CreatePageData()
        {
            //var pageData = new MT.OneWire.TEDSpageData();
        }
        #endregion

        #region Page1
        /**********************************************************
        * FUNCTION:     Page1
        * DESCRIPTION:
        ***********************************************************/
        public const string Page01Default = "6D-9A-00-00-A0-0F-7D-50-14-4D-C4-FC-F9-FB-7F-D1-83-B9-12-04-32-80-8C-0C-58-02-00-00-40-29-32-87";
        public static TDL_Property Prop_FactoryCalibrationDate;

        public static bool GetFactoryCalDate(string hex, out string result)
        {
            result = null;
            if (Prop_FactoryCalibrationDate == null)
            {
                if (!GetProperty(01, "FactoryCalDate", out Prop_FactoryCalibrationDate))
                {
                    return false;
                }
            }
            return ReadPropertyValue(Prop_FactoryCalibrationDate, hex, out result);
        }
        #endregion

        #region Page2
        /**********************************************************
        * FUNCTION:     Page2
        * DESCRIPTION:
        ***********************************************************/
        public const string Page02Default = "E2-C3-B7-DB-5E-96-D3-CB-72-10-F1-09-00-00-00-C3-30-3B-2D-7C-E3-01-00-00-00-00-00-00-00-00-00-00";
        public static TDL_Property Prop_Description;

        public const string DescriptionDefault = "Converter DO";

        public static bool GetDescription(string hex, out string result)
        {
            result = null;
            if (Prop_Description == null)
            {
                if (!GetProperty(02, "SensorDescription", out Prop_Description))
                {
                    return false;
                }
            }
            return ReadPropertyValue(Prop_Description, hex, out result);
        }
        #endregion

        #region Page15
        /**********************************************************
        * FUNCTION:     Page15
        * DESCRIPTION:
        ***********************************************************/
        public static TDL_Property Prop_FwVersion;
        public static bool GetFwVersion(string hex, out string result)
        {
            result = null;
            if (Prop_FwVersion == null)
            {
                if (!GetProperty(15, "FirmwareVersion", out Prop_FwVersion))
                {
                    return false;
                }
            }
            return ReadPropertyValue(Prop_FwVersion, hex, out result);
        }
        #endregion

        #region Command ReadPage
        /**********************************************************
        * FUNCTION:     Command ReadPage
        * DESCRIPTION:  #RDPG
        ***********************************************************/
        public static string CMD_ReadPage(int pageNo)
        {
            return $"#RDPG {pageNo}";
        }

        public static bool CMD_ReadPage(string pageNo, string hex, out List<string> results)
        {
            results = new List<string>();
            switch (pageNo)
            {
                case "00":
                case "0":

                    if (GetPartNumber(hex, out string pn))
                    {
                        results.Add($"PartNumber: {pn}");
                    }
                    if (GetSerialNumber(hex, out string sn))
                    {
                        results.Add($"SerialNumber: {sn}");
                    }
                    return results.Count > 0;
                case "01":
                case "1":
                    if (GetFactoryCalDate(hex, out string facCal))
                    {
                        results.Add($"FactoryCalibration: {facCal}");
                        return true;
                    }
                    break;
                case "02":
                case "2":
                    if (GetDescription(hex, out string desc))
                    {
                        results.Add($"Description: {desc}");
                        return true;
                    }
                    break;
            }
            return false;
        }
        #endregion

        #region Command WritePage
        /**********************************************************
        * FUNCTION:     Command WritePage
        * DESCRIPTION:  #WRPG
        ***********************************************************/
        /// <summary>
        /// Create a command with data to send to sensor
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string CMD_WritePage(int pageNo, string hex)
        {
            return $"#WRPG {pageNo} {hex.RemoveSeparators()}";
        }
        #endregion
    }
}
