//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using OneWireUI;
//using System.IO;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace ConverterCalib
//{
//    public static class clHandler
//    {
//        public static string UserAdmin;


//        public static void WriteLog(string eventName, Exception e = null,string message = null )
//        {
//            clLogging.ErrorHandler(eventName, e, message, true);
//        }
//        /*****************************************************************************
//        * User Controls:
//        '****************************************************************************/
//        public static void Load_Panel(TabControl tab, UserControl uc)
//        {
//            try
//            {
//                if (!tab.Controls.Contains(uc))
//                {
//                    tab.Controls.Add(uc);
//                    uc.Dock = DockStyle.Fill;
//                }
//                uc.BringToFront();
//            }
//            catch (Exception e)
//            {
//                WriteLog("Load_Panel", e, uc.Name);
//            }
//        }
//        public static void Load_Panel(Panel tab, UserControl uc)
//        {
//            try
//            {
//                if (!tab.Controls.Contains(uc))
//                {
//                    tab.Controls.Add(uc);
//                    uc.Dock = DockStyle.Fill;
//                }
//                uc.BringToFront();
//            }
//            catch (Exception e)
//            {
//                WriteLog("Load_Panel", e, uc.Name);
//            }
//        }

//        /*****************************************************************************
//        * User Controls:
//        '****************************************************************************/
//        private static Config _m_Config;
//        public static Config m_Config
//        {
//            get
//            {
//                if(_m_Config == null) { _m_Config = new Config(); }
//                return _m_Config;
//            }
//            set
//            {
//                _m_Config = value;
//            }
//        }

//        public static ConfigValues m_ConfigValues { get { return m_Config.values; } }

//        public static UC_TT_Item_Infos m_TT { get; set; }

//        public static UC_SensorInfos Uc_SensorInfos
//        {
//            get { return UC_SensorInfos.Instance; }
//            set { UC_SensorInfos.Instance = value; }
//        }

//        public static UC_FilesLoader Uc_FilesLoader_Log { get; set; }
//        public static UC_FilesLoader Uc_FilesLoader_TDL { get; set; }
//        public static UC_TEDS_Main Uc_TEDS_Main
//        {
//            get { return UC_TEDS_Main.Instance; }
//            set { UC_TEDS_Main.Instance = value; }
//        }
//        public static UC_Meas Uc_Meas
//        {
//            get { return UC_Meas.Instance; }
//            set { UC_Meas.Instance = value; }
//        }

//        public static UC_TEDS_Images Uc_Images
//        {
//            get { return UC_TEDS_Images.Instance; }
//            set { UC_TEDS_Images.Instance = value; }
//        }

//        public static UC_TEDS_ImagesDir Uc_ImageDir
//        {
//            get { return UC_TEDS_ImagesDir.Instance; }
//            set { UC_TEDS_ImagesDir.Instance = value; }
//        }

//        public static UC_TEDS_DirReader Uc_DirReader
//        {
//            get { return UC_TEDS_DirReader.Instance; }
//            set { UC_TEDS_DirReader.Instance = value; }
//        }
//        public static UC_TEDS_bytesreader Uc_bytes
//        {
//            get { return UC_TEDS_bytesreader.Instance; }
//            set { UC_TEDS_bytesreader.Instance = value; }
//        }

//        public static UC_MeasChart Uc_MeasChart
//        {
//            get { return UC_MeasChart.Instance; }
//            set { UC_MeasChart.Instance = value; }
//        }

//        public static UC_Calib Uc_Calib
//        {
//            get { return UC_Calib.Instance; }
//            set { UC_Calib.Instance = value; }
//        }

//        /*****************************************************************************
//        * Informations:
//        '****************************************************************************/
//        public static bool MeasRunning { get { return Uc_Meas.MeasRunning; } set { Uc_Meas.MeasRunning = value; } }

//        public static bool SaveMeas { get { return Uc_Meas.checkB_SaveLive.Checked; } set { Uc_Meas.checkB_SaveLive.Checked = value; } }
//        public static int AVG_n_Values { get { return Uc_Meas.AVG_n_Values; } set { Uc_Meas.AVG_n_Values = value; } }

//        private static TEDSInfos _SensorInfos;
//        public static TEDSInfos SensorInfos
//        {
//            get
//            {
//                if(_SensorInfos == null)
//                {
//                    _SensorInfos = new TEDSInfos();
//                }
//                return _SensorInfos;
//            }
//            set
//            {
//                _SensorInfos = value;
//                UC_SensorInfos.Instance.ShowSensorInfos(value);
//            }
//        }

//        public static TEDSHeader SensorHeader
//        {
//            get
//            {
//                return SensorInfos.Header;
//            }
//            set
//            {
//                _SensorInfos.Header = value;
//                UC_SensorInfos.Instance.ShowSensorInfos(value);
//            }
//        }

//        /*****************************************************************************
//        * Measurement:
//        '****************************************************************************/
//        private static bool _Clear_History;
//        public static bool Clear_History
//        {
//            get { return _Clear_History; }
//            set { Clear_History_Set(value); }
//        }
//        private static void Clear_History_Set(bool value)
//        {
//            try { Uc_Meas.CheckB_ClearHistory.Checked = value; }catch{ }
//            _Clear_History = value;
//        }

//        private static BindingSource _BS_Meas, _BS_MeasAll, _BS_MeasAVG, _BS_Meas_ColLegend, _BS_TDL_Cpl, _BS_TDL_Meas;
//        public static BindingSource BS_Meas
//        {
//            get
//            {
//                if(_BS_Meas == null) { _BS_Meas = new BindingSource() { DataSource = DT_Meas}; }
//                return _BS_Meas;
//            }
//            set { _BS_Meas = value; }
//        }
//        public static BindingSource BS_MeasAll
//        {
//            get
//            {
//                if (_BS_MeasAll == null) { _BS_MeasAll = new BindingSource() { DataSource = DT_MeasAll}; }
//                return _BS_MeasAll;
//            }
//            set { _BS_MeasAll = value; }
//        }
//        public static BindingSource BS_MeasAVG
//        {
//            get
//            {
//                if (_BS_MeasAVG == null) { _BS_MeasAVG = new BindingSource() { DataSource = DT_MeasAVG}; }
//                return _BS_MeasAVG;
//            }
//            set { _BS_MeasAVG = value; }
//        }
//        public static BindingSource BS_Meas_ColLegend
//        {
//            get
//            {
//                if (_BS_Meas_ColLegend == null) { _BS_Meas_ColLegend = new BindingSource(); }
//                return _BS_Meas_ColLegend;
//            }
//            set { _BS_Meas_ColLegend = value; }
//        }

//        public static BindingSource BS_TDL_Cpl
//        {
//            get
//            {
//                if (_BS_TDL_Cpl == null) { _BS_TDL_Cpl = new BindingSource() { DataSource = DT_TDL_Cpl}; }
//                return _BS_TDL_Cpl;
//            }
//            set { _BS_TDL_Cpl = value; }
//        }
//        public static BindingSource BS_TDL_Meas
//        {
//            get
//            {
//                if (_BS_TDL_Meas == null) { _BS_TDL_Meas = new BindingSource() { DataSource = DT_TDL_Meas }; }
//                return _BS_TDL_Meas;
//            }
//            set { _BS_TDL_Meas = value; }
//        }

//        private static DataTable _DT_MeasAll;
//        public static DataTable DT_MeasAll
//        {
//            get { return _DT_MeasAll; }
//            set { _DT_MeasAll = value; }
//        }
//        public static int DT_MeasAll_Count
//        {
//            get
//            {
//                if (DT_MeasAll != null)
//                {
//                    return DT_MeasAll.Rows.Count;
//                }
//                return 0;
//            }
//        }

//        private static DataTable _DT_MeasAVG;
//        public static DataTable DT_MeasAVG
//        {
//            get { return _DT_MeasAVG; }
//            set { _DT_MeasAVG = value; }
//        }
//        public static int DT_MeasAVG_Count
//        {
//            get
//            {
//                if (DT_MeasAVG != null)
//                {
//                    return DT_MeasAVG.Rows.Count;
//                }
//                return 0;
//            }
//        }

//        //private static DataTable _DT_TEDS;
//        //public static DataTable DT_TEDS { get { _DT_TEDS = (DataTable)UC_TEDS_Main.BS_TDL_Meas.DataSource; return _DT_TEDS; } }

//        private static DataTable _DT_TDL_Cpl;
//        public static DataTable DT_TDL_Cpl
//        {
//            get { return _DT_TDL_Cpl; }
//            set
//            {
//                _DT_TDL_Cpl = value;
//                BS_TDL_Cpl.DataSource = value;
//            }
//        }
//        public static int DT_TDL_Cpl_Count
//        {
//            get
//            {
//                if (_DT_TDL_Cpl != null)
//                {
//                    return _DT_TDL_Cpl.Rows.Count;
//                }
//                return 0;
//            }
//        }

//        private static DataTable _DT_TDL_Meas;
//        public static DataTable DT_TDL_Meas { get { return _DT_TDL_Meas; } set { _DT_TDL_Meas = value; } }
//        public static int DT_TDL_Meas_Count
//        {
//            get
//            {
//                if (_DT_TDL_Meas != null)
//                {
//                    return _DT_TDL_Meas.Rows.Count;
//                }
//                return 0;
//            }
//        }

//        private static DataTable _DT_Meas;
//        public static DataTable DT_Meas
//        {
//            get { return _DT_Meas; }
//            set
//            {
//                _DT_Meas = value;
//                //BS_Meas.DataSource = value;
//                //Uc_MeasChart.Chart_Meas.DataBind();
//            }
//        }
//        public static int DT_Meas_Count
//        {
//            get
//            {
//                if(DT_Meas != null)
//                {
//                    return DT_Meas.Rows.Count;
//                }
//                return 0;
//            }
//        }

//        public static DataTable DT_Meas_All_Report;

//        private static DataTable _Chart_Meas_Legend;
//        public static DataTable Chart_Meas_Legend
//        {
//            get
//            {
//                if(_Chart_Meas_Legend == null)
//                {
//                    Init_ChartMeas_Legend();
//                }
//                return _Chart_Meas_Legend;
//            }
//            set { _Chart_Meas_Legend = value; }
//        }

//        public static void Init_ChartMeas_Legend()
//        {
//            _Chart_Meas_Legend = new DataTable();
//            _Chart_Meas_Legend.Columns.Add(ColHeader_Title, typeof(string));
//            _Chart_Meas_Legend.Columns.Add(ColHeader_Active, typeof(bool));
//            _Chart_Meas_Legend.Columns.Add(ColHeader_Visible, typeof(bool));
//        }
//        public static int Chart_Meas_Legend_Count
//        {
//            get
//            {
//                if (Chart_Meas_Legend != null)
//                {
//                    return Chart_Meas_Legend.Rows.Count;
//                }
//                return 0;
//            }
//        }

//        private static List<string> _LegendTitle;
//        public static List<string> LegendTitle
//        {
//            get
//            {
//                if(_LegendTitle == null)
//                {
//                    if (DT_Meas_Count > 0)
//                    {
//                        _LegendTitle = Get_Titles(DT_Meas);
//                    }
//                }
//                return _LegendTitle;
//            }
//            set
//            {
//                _LegendTitle = value;
//            }
//        }

//        public static string ColHeader_DateTime = "datetime";
//        public static string ColHeader_Title = "title";
//        public static string ColHeader_Active = "active";
//        public static string ColHeader_Visible = "visible";
//        public static List<string> Get_Titles(DataTable dtMeas)
//        {
//            _LegendTitle = new List<string>();
//            DT_Meas = dtMeas.Copy();
//            DT_Meas.TableName = "Meas";
//            DT_MeasAll = new DataTable("MeasALL");
//            DT_MeasAVG = new DataTable("MeasAVG");
//            DT_Meas_All_Report = new DataTable("MeasReport");
//            Chart_Meas_Legend.Rows.Clear();
//            DT_MeasAll.Columns.Add(ColHeader_DateTime);
//            DT_MeasAVG.Columns.Add(ColHeader_DateTime);
//            DT_Meas_All_Report.Columns.Add(ColHeader_DateTime);
//            foreach (DataRow row in dtMeas.Rows)
//            {
//                string title = row.Field<string>("Property").Replace("%", "");
//                _LegendTitle.Add(title);
//                DT_MeasAll.Columns.Add(title);
//                DT_MeasAVG.Columns.Add(title);
//                DT_Meas_All_Report.Columns.Add(title);
//                DataRow rowN = Chart_Meas_Legend.NewRow();
//                rowN[ColHeader_Title] = title;
//                bool active = true;
//                if (title.ToLower().Contains("empty") || title.ToLower().Contains("status"))
//                {
//                    active = false;
//                }
//                rowN[ColHeader_Active] = active;
//                rowN[ColHeader_Visible] = active;
//                Chart_Meas_Legend.Rows.Add(rowN);
//            }
//            DT_MeasAVG.Rows.Add(DT_MeasAVG.NewRow());
//            DT_MeasAVG.Rows.Add(DT_MeasAVG.NewRow());
//            BS_Meas.DataSource = DT_Meas;
//            BS_MeasAll.DataSource = DT_MeasAll;
//            BS_MeasAVG.DataSource = DT_MeasAVG;
//            Uc_Meas.Init_DGV();
//            Uc_MeasChart.Init_Chart();
//            return _LegendTitle;
//        }
//        public static void Init_MeasTables(DataTable dtMeas)
//        {
//            _LegendTitle = Get_Titles(dtMeas);
//        }


//        public static void DisableColumnsPageRead(DataGridView dgv)
//        {
//            try
//            {
//                dgv.Columns["Result_DB"].Visible = false;
//                dgv.Columns["PageNo"].Visible = false;
//                dgv.Columns["DBTable"].Visible = false;
//                dgv.Columns["DBKey"].Visible = false;
//                dgv.Columns["DBField"].Visible = false;
//                dgv.Columns["DBName"].Visible = false;
//                dgv.Columns["Factor"].Visible = false;
//                dgv.Columns["Offset"].Visible = false;
//            }
//            catch { }
//        }
//        public static void Bind_Chart()
//        {
//           Uc_MeasChart.Chart_Meas.DataBind();
//        }

//        /*****************************************************************************
//        * TDL:
//        '****************************************************************************/
//        public static DataTable TEDS_TID;
//        public static void Get_TDL_All(string dir)
//        {
//            GetAll_TDLFiles(dir, true);
//            TEDS_TID = TEDS_TDL_TDI;
//        }


//        /*****************************************************************************
//        * Paths:
//        '****************************************************************************/
//        public static string[] FilesEnding_TDL = new string[] { "tdl", "icf" };
//        public static string[] FilesEnding_All = new string[] { "tdl", "icf", "txt", "log", "csv" };
//        public static string[] FilesEnding_Images = new string[] { "bit", "ted", "hex", "tit", "tithex", "icf" };

//        private static string _FilesEnding_ImagesFiltered;
//        public static string FilesEnding_ImagesFiltered
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(_FilesEnding_ImagesFiltered))
//                {
//                    _FilesEnding_ImagesFiltered = FilesEnding(FilesEnding_Images);
//                }
//                return _FilesEnding_ImagesFiltered;
//            }
//        }



//        public static string FilesEnding(string[] filesEnding)
//        {
//            return clSTD.FileEndingToFilter(filesEnding);
//        }

//        public static string Path_Initial_Default { get { return m_ConfigValues.Path_Initial_Default; } }
//        public static string Path_Initial { get { return m_ConfigValues.Path_Initial; } set { Set_PathInitial(value); } }
//        public static string Set_PathInitial(string path)
//        {
//            m_ConfigValues.Path_Initial = path;
//            return path;
//        }
//        public static string PathTDL_Dir
//        {
//            get { return m_ConfigValues.PathTDL_Dir; }
//            set { Set_PathTDL_Dir(value); }
//        }
//        public static string Set_PathTDL_Dir(string path)
//        {
//            m_ConfigValues.PathTDL_Dir = path;
//            try { Uc_DirReader.TB_TDL_MainDir.Text = path; } catch { }
//            return path;
//        }

//        public static string PathImage_Dir
//        {
//            get { return m_ConfigValues.PathImage_Dir; }
//            set { Set_PathImage_Dir(value); }
//        }
//        public static string Set_PathImage_Dir(string path)
//        {
//            m_ConfigValues.PathImage_Dir = path;
//            try { Uc_ImageDir.Path = path; } catch { }
//            return path;
//        }

//        public static string Path_LogErrors { get { return m_ConfigValues.PathLog; } set { Set_PathLog_Errors(value); } }
//        public static string Set_PathLog_Errors(string path)
//        {
//            m_ConfigValues.PathLog = path;
//            try { Uc_FilesLoader_Log.PathName = path; } catch { }
//            return path;
//        }

//        public static string Path_Log_Meas { get { return m_ConfigValues.PathLog_Measurement; } set { Path_Log_Meas_GUI(value); } }

//        private static string Path_Log_Meas_GUI(string path = null)
//        {
//            if (path == null) { path = m_ConfigValues.PathLog_Measurement; }
//            else { m_ConfigValues.PathLog_Measurement = path; }
//            try { Uc_Meas.TB_PathMeas.Text = path; } catch { }
//            return path;
//        }
//        public static string Path_TDL_CPL { get { return m_ConfigValues.PathTDL_LastUsed_CPL; } set { Path_TDLcpl_GUI(value); } }

//        private static string Path_TDLcpl_GUI(string path = null)
//        {
//            if (path == null) { path = m_ConfigValues.PathTDL_LastUsed_CPL; }
//            else { m_ConfigValues.PathTDL_LastUsed_CPL = path; }
//            try { Uc_TEDS_Main.Set_PathTDL_CPL(path); } catch { }
//            return path;
//        }

//        public static string Path_TDL_Meas { get { return m_ConfigValues.PathTDL_LastUsed_Meas; } set { Path_TDLmeas_GUI(value); } }
//        private static string Path_TDLmeas_GUI(string path = null)
//        {
//            if (path == null) { path = m_ConfigValues.PathTDL_LastUsed_Meas; }
//            else { m_ConfigValues.PathTDL_LastUsed_Meas = path; }
//            try { Uc_TEDS_Main.Set_PathTDL_Meas(path); } catch { }
//            try { Form_Main.SelectedDirAndFile = path; } catch { }
//            return path;
//        }

//        public static string GetDir_InitPath(this string path, out bool pathDefault, string pathInit = null)
//        {
//            string dir = pathInit == null ? Path_Initial : pathInit ;
//            pathDefault = false;
//            if (!string.IsNullOrEmpty(path))
//            {
//                try
//                {
//                    FileAttributes fa = File.GetAttributes(path);
//                    if (fa.HasFlag(FileAttributes.Directory))
//                    {
//                        if (Directory.Exists(path))
//                        {
//                            return path;
//                        }
//                    }
//                    else
//                    {

//                        FileInfo fi = new FileInfo(path);
//                        if (Directory.Exists(fi.Directory.FullName))
//                        {
//                            return fi.Directory.FullName;
//                        }
//                    }
//                }
//                catch { }
//            }
//            pathDefault = true;
//            return dir;
//        }
//        public static string GetDir_InitPath(this string path, string pathInit = null)
//        {
//            return path.GetDir_InitPath(out bool pathDefault, pathInit);
//        }
//        public static bool Get_File(string path, string filesExtension, out string file, string pathInit = null)
//        {
//            file = OpenFileDialog(path.GetDir_InitPath(out bool pathDefault, pathInit), filesExtension, out bool ok);
//            if (ok && pathDefault)
//            {
//                string dir = new FileInfo(file).DirectoryName;
//                if (pathInit == null)
//                {
//                    Path_Initial = dir;
//                }
//                else
//                {
//                    pathInit = dir;
//                }
                
//            } 
//            return ok;
//        }
//        public static bool Get_File(string path, string[] filesExtension, out string file, string pathInit = null)
//        {
//            file = OpenFileDialog(path.GetDir_InitPath(out bool pathDefault, pathInit), filesExtension, out bool ok);
//            if (ok && pathDefault)
//            {
//                string dir = new FileInfo(file).DirectoryName;
//                if (pathInit == null)
//                {
//                    Path_Initial = dir;
//                }
//                else
//                {
//                    pathInit = dir;
//                }
//            } 
//            return ok;
//        }

//        /*****************************************************************************
//        * 1-Wire:
//        '****************************************************************************/
//        public static void Close_DLL()
//        {
//            COM1WireMicroBackend.CloseDLL();
//        }

//        public static int _SensorRateMeas_Read = 1000;
//        public static int SensorRateMeas_Read
//        {
//            get { return _SensorRateMeas_Read; }
//            set
//            {
//                if (value < 1000) { _SensorRateMeas_Read = 1000; }
//                _SensorRateMeas_Read = value;
//            }
//        }

//        public static bool SensorPresense
//        {
//            get
//            {
//                bool presense = COM1WireMicroBackend.SensorPresence;
//                Show_SensorPresense = COM1WireMicroBackend.SensorStatus;
//                return presense;
//            }
//        }
//        public static string Show_SensorPresense
//        {
//            get { return Form_Main.SensorStatus; }
//            set { Form_Main.SensorStatus = value; }
//        }

//        public static void Show_SensorResult_CPL(string sensorResult = null, string presense = null, string duration = null,  string unit = "ms", int attents = 0, int attentsTotal = 0)
//        {
//            if (sensorResult != null)
//            { Show_SensorResult = sensorResult; }
//            if (presense != null)
//            { Show_SensorPresense = presense; }
//            if (duration != null)
//            { Show_Duration(duration, unit, attents, attentsTotal); }
//        }
//        public static void Show_SensorResult_CPL(string sensorResult = null, string presense = null, double duration = 0, string unit = "ms", int attents = 0, int attentsTotal = 0)
//        {
//            if (sensorResult != null)
//            { Show_SensorResult = sensorResult; }
//            if (presense != null)
//            { Show_SensorPresense = presense; }
//            if (duration != 0)
//            { Show_Duration(duration, unit, attents, attentsTotal); }
//        }
//        public static void Show_SensorResult_CPL(TEDSPageData sensorResult = null, double duration = 0, string unit = "ms", int attents = 0, int attentsTotal = 0)
//        {
//            if (sensorResult != null)
//            {
//                Show_SensorResult = sensorResult.HEXseparated;
//                Show_SensorPresense = sensorResult.SensorStatus;
//                Show_Duration(sensorResult.Duration);
//            }
//            if (duration != 0)
//            { Show_Duration(duration, unit, attents, attentsTotal); }
//        }
//        public static void Show_SensorResult_CPL(TimeSpan duration, string sensorResult = null, string presense = null, int attents = 0, int attentsTotal = 0)
//        {
//            if (sensorResult != null)
//            { Show_SensorResult = sensorResult; }
//            if (presense != null)
//            { Show_SensorPresense = presense; }
//            Show_Duration(duration, attents, attentsTotal);
//        }
//        public static void Show_SensorResult_CPL(TimeSpan duration, TEDSPageData sensorResult, int attents = 0, int attentsTotal = 0)
//        {
//            if (sensorResult != null)
//            {
//                if (sensorResult.HEXseparated != null)
//                { Show_SensorResult = sensorResult.HEXseparated; }
//                else if(sensorResult.HEX != null)
//                { Show_SensorResult = sensorResult.HEX; }
//                Show_SensorPresense = sensorResult.SensorStatus;
//            }
//            Show_Duration(duration, attents, attentsTotal);
//        }


//        public static string Show_SensorResult { set { Form_Main.SensorResult = value; } }
//        private static void Show_Duration(string duration, string unit = "ms", int attents = 0, int attentsTotal = 0)
//        {
//            if (attentsTotal > 0)
//            {
//                Form_Main.SensorDuration = $"{duration} {unit}  {attents}/{attentsTotal}";
//                return;
//            }
//            else if(attents> 0 )
//            {
//                Form_Main.SensorDuration = $"{duration} {unit}  {attents.ToString().PadLeft(4,'0')}";
//                return;
//            }
//            Form_Main.SensorDuration = $"{duration} {unit}";
//        }
//        private static void Show_Duration(double duration, string unit = "ms", int attents = 0, int attentsTotal = 0)
//        {
//            Show_Duration(string.Format("{0:0.0}", duration), unit, attents, attentsTotal);
//        }
//        private static void Show_Duration(TimeSpan duration, int attents = 0, int attentsTotal = 0)
//        {
//            if (duration.TotalMinutes < 1)
//            {
//                if (duration.TotalSeconds < 10)
//                {
//                    Show_Duration(duration.TotalMilliseconds, "ms", attents, attentsTotal);
//                    return;
//                }
//                Show_Duration(duration.TotalSeconds, "sec", attents, attentsTotal);
//                return;
//            }
//            Show_Duration(duration.TotalMinutes, "min", attents, attentsTotal);
//            return;

//        }

//        public static int ConnectionCheckAttends { get; set; } = 10;

//        public static bool ConnectionCheck
//        {
//            get
//            {
//                DurationValues durationTotal = StartDuration();
//                int count = 0;
//                while (!SensorPresense && durationTotal.TimeElapsed.TotalSeconds < ConnectionCheckAttends)
//                {
//                    Thread.Sleep(1000);
//                    Show_SensorResult_CPL(durationTotal.TimeElapsed, attents: count, attentsTotal: ConnectionCheckAttends);
//                    count++;
//                }
//                return count < ConnectionCheckAttends;
//            }
//        }


        
//        /*****************************************************************************
//        * Read Pages:
//        '****************************************************************************/
//        public static bool ReadSensorInfos(bool resetSensorInfos)
//        {
//            if (ConnectionCheck)
//            {
//                DurationValues duration = StartDuration();
//                bool found = GetSensorInfos(SensorInfos);
//                SensorHeader = SensorInfos.Header;
//                Show_SensorResult_CPL(duration.Stop());
//                return found;
//            }
//            return false;
//        }

//        public static bool ReadPage(string pageNo, DataTable dtMeas, ref Button btnSelected)
//        {
//            int pNo = Convert.ToInt32(pageNo);
//            SensorInfos.DT_Read = dtMeas;
//            return ReadPage(pNo, SensorInfos, ref btnSelected);
//        }
//        public static bool ReadPage(decimal pageNo, DataTable dtMeas, ref Button btnSelected)
//        {
//            if (SensorInfos.Path_Tdl != dtMeas.TableName)
//            { SensorInfos.Path_Tdl = dtMeas.TableName; }
//            if (SensorInfos.DT_Properties.TableName != dtMeas.TableName)
//            { SensorInfos.DT_Properties = dtMeas; }
//            SensorInfos.DT_Read = dtMeas;
//            bool found = ReadPage((int)pageNo, SensorInfos, ref btnSelected);
//            dtMeas = SensorInfos.DT_Read;
//            return found;
//        }
//        private static bool ReadPage(int pageNo, TEDSInfos sensorInfos, ref Button btnSelected)
//        {
//            Color initColor = btnSelected.BackColor;
//            btnSelected.BackColor = clMTcolors.MT_rating_InWork_Active;
//            btnSelected.Enabled = false;
//            btnSelected.Refresh();
//            bool found = ReadPage(pageNo);
//            btnSelected.BackColor = initColor;
//            btnSelected.Enabled = true;
//            btnSelected.Refresh();
//            return found;
//        }
//        public static bool ReadPage(int pageNo, bool showDuration =true)
//        {
//            if (ConnectionCheck)
//            {
//                if (pageNo > -1)
//                {
//                    bool found = GetPage(pageNo, SensorInfos);
//                    if (showDuration)
//                    { Show_SensorResult_CPL(SensorInfos.PageDatas[pageNo]); }
//                    return found;
//                }
//            }
//            return false;
//        }

//        public static bool ReadPageAll(DataTable dtMeas, ref Button btnSelected)
//        {
//            DurationValues durationTotal = StartDuration();
//            if (SensorInfos.Path_Tdl != dtMeas.TableName)
//            { SensorInfos.Path_Tdl = dtMeas.TableName; }
//            if (SensorInfos.DT_Properties.TableName != dtMeas.TableName)
//            { SensorInfos.DT_Properties = dtMeas; }
//            SensorInfos.DT_Read = dtMeas;
//            int count = dtMeas.Rows.Count;
//            if(count>0)
//            {
//                Color initColor = btnSelected.BackColor;
//                btnSelected.BackColor = STDhelper.clMTcolors.MT_rating_InWork_Active;
//                btnSelected.Enabled = false;
//                btnSelected.Refresh();
//                if (ConnectionCheck)
//                {
//                    SensorInfos.PageDatas = OneWireSpace.OneWire.ReadSensorPageALL(out TimeSpan span);
//                    Show_Duration(durationTotal.TimeElapsed);
//                    int total = SensorInfos.LastPageNo;
//                    for(int pageNo =0; pageNo <= total; pageNo++)
//                    {
//                        SensorInfos.Get_Values(pageNo);
//                    //    //ReadPage(pageNo, false);
//                        Show_SensorResult_CPL(durationTotal.TimeElapsed, attents: pageNo, attentsTotal: total);
//                    //    //Thread.Sleep(40);
//                    }

//                    //OneWireSpace.OneWire.GetPageAll(SensorInfos, out TimeSpan d);
//                    btnSelected.Enabled = true;
//                    btnSelected.BackColor = initColor;
//                    btnSelected.Refresh();
//                    Show_SensorResult_CPL(durationTotal.Stop());
//                    return true;
//                }
//                btnSelected.BackColor = initColor;
//                btnSelected.Enabled = true;
//                btnSelected.Refresh();
//            }
//            Show_SensorResult_CPL(durationTotal.Stop());
//            return false;
//        }

//        public static bool ReadPage16()
//        {
//            if (ReadPage(16, false))
//            {
//                try
//                {
//                    return Uc_Meas.DT_Input(SensorInfos.DT_Read);
//                }
//                catch (Exception e)
//                {
//                    WriteLog("ReadPage16", e);
//                }
//            }
//            return false;
//        }

//        public static void GetPage16Values(SynchronizationContext t = null, string logpath = null)
//        {
//            //SensorInfos.DT_Read = DT_TDL_Meas;
//            SensorInfos.DT_Read = DT_Meas.Select("PageNo = '16'").CopyToDataTable();
//            string BTtxt = Uc_Meas.BT_GetP16.Text;
//            int i = 0;
//            int CountErrorsTotal = 5;
//            if (ConnectionCheck)
//            {
//                DurationValues ID = StartDuration();
//                while (!ReadSensorInfos(false) && i<5)
//                {
//                    Thread.Sleep(20);
//                    i++;
//                }
//                i = 0;
//                COM1WireMicroBackend.MeasureModusStart(true);
//                int countErrors = 0;
//                bool error = false;
//                double duration = 0;
//                TimeSpan span;
//                while (Thread_P16Continous.IsAlive && countErrors < CountErrorsTotal && SensorPresense)
//                {
//                    try
//                    {
//                        t.Send((object state) =>
//                        {
//                            if (ReadPage16())
//                            {
//                                i++;
//                                MeasAVG(t, true);
//                                Uc_Meas.Info_BtnP16(i);
//                                countErrors = 0;
//                                error = false;
//                            }
//                            else
//                            {
//                                error = true;
//                                countErrors++;
//                            }
                            
//                        }, null);
//                        if (!error && SaveMeas)
//                        {
//                            WriteMeasData(logpath);
//                        }
//                        if (i > 0)
//                        {
//                            duration = (SensorRateMeas_Read - ID.TimeElapsed.TotalMilliseconds)+1;
//                            if (duration > 10)
//                            { Thread.Sleep((int)duration); }
//                        }
//                        span = ID.TimeElapsed;
//                        ID = ID.Start();
//                        if (countErrors == 0)
//                        {
//                            Show_SensorResult_CPL(span, SensorInfos.PageDatas[16], attents: i, attentsTotal: 0);
//                        }
//                        else
//                        {
//                            Show_SensorResult_CPL(span, SensorInfos.PageDatas[16], attents: countErrors, attentsTotal: CountErrorsTotal);
//                        }
//                    }
//                    catch(Exception e)
//                    {
//                        if (ThreadRunning)
//                        { WriteLog("GetPage16Values", e); }
//                        break;
//                    }
//                }
//            }
//            MeasRunning = false;
//        }

//        public static DataTable ReadImage(TEDSInfos sensorInfos, string pathTemplate, string pathFile)
//        {
//            DurationValues durationTotal = StartDuration();
//            try
//            {
//                sensorInfos.Path_Tdl = pathTemplate;
//                TEDS_Images image = new TEDS_Images(pathFile);
//                sensorInfos.PageDatas = image.PageDatas;
//                sensorInfos.Get_Values();
//                sensorInfos.Get_Header();
//                //int total = sensorInfos.PageDatas.Count;
//                //for (int pageNo = 0; pageNo < total; pageNo++)
//                //{
//                //    //GetPageFromByte(sensorInfos, pageNo);
//                //    Show_SensorResult_CPL( durationTotal.TimeElapsed, sensorInfos.PageDatas[pageNo], attents: pageNo, attentsTotal: total);
//                //}
//                Show_SensorResult_CPL(durationTotal.Stop());
//            }
//            catch (Exception e)
//            {
//                WriteLog("ReadImage", e);
//            }

//            return sensorInfos.DT_Read;
//        }


//        /*****************************************************************************
//        * Write Pages:
//        '****************************************************************************/
//        public static bool WritePage()
//        {
//            return Uc_Meas.WritePage();
//        }

//        /*****************************************************************************
//        * Calibration:
//        '****************************************************************************/
//        public enum ProcDesc
//        {
//            ReadSensorValues,
//            Write_Cal_Data_Default,
//            NTC_20kOhm_25C,
//            PT1000_20C,
//            PT1000_30C,
//            Write_Cal_Data,
//            ResetValues,
//            idle,
//            ProcessStop
//        }

//        /*****************************************************************************
//        * Thread Measurements:
//        '****************************************************************************/
//        private static Thread _Thread_P16Continous;
//        public static Thread Thread_P16Continous
//        {
//            get
//            {
//                if(_Thread_P16Continous == null)
//                {
//                    _Thread_P16Continous = Init_Thread_Meas();
//                }
//                return _Thread_P16Continous;
//            }
//        }

//        private static Thread Init_Thread_Meas()
//        {
//            return new Thread(new ThreadStart(Uc_Meas.GetPage16Values)) { Name = "Measurement", IsBackground = true };
//        }
//        public static bool ThreadRunning = false;
//        public static void ReStart(this Thread t)
//        {
//            _Thread_P16Continous.Stop();
//            _Thread_P16Continous = Init_Thread_Meas();
//            _Thread_P16Continous.Start();
//            ThreadRunning = true;
//            t = _Thread_P16Continous;
//            COM1WireMicroBackend.MeasureModusStart(true);
//        }
//        public static void Stop(this Thread t)
//        {
//            if (t.IsAlive)
//            {
//                ThreadRunning = false;
//                t.Abort();
//                COM1WireMicroBackend.MeasureModusStart(false);
//                Close_DLL();
//            }
//        }

//        public static async void MeasAVG(SynchronizationContext t, bool StandardDeviation = true)
//        {
//            {
//                if (DT_MeasAll_Count >= AVG_n_Values)
//                {
//                    try
//                    {
//                        await Task.Run(() =>
//                        {
//                            t.Send((object state) =>
//                            {
//                                if (DT_MeasAVG == null)
//                                {
//                                    DT_MeasAVG = DT_MeasAll.Clone();
//                                    DT_MeasAVG.Rows.Add(DT_MeasAVG.NewRow());
//                                    DT_MeasAVG.Rows.Add(DT_MeasAVG.NewRow());
//                                }
//                                DataRow rowAVG = DT_MeasAVG.NewRow();
//                                DataRow rowSTDdev = DT_MeasAVG.NewRow();
//                                DataTable dtChoise = DT_MeasAll.Rows.Cast<DataRow>().Take((int)AVG_n_Values).CopyToDataTable();
//                                foreach (DataColumn col in dtChoise.Columns)
//                                {
//                                    string colName = col.ColumnName;
//                                    if (colName == "datetime")
//                                    {
//                                        var time = dtChoise.Rows[0][colName];
//                                        DT_MeasAVG.Rows[0][colName] = time;
//                                        DT_MeasAVG.Rows[1][colName] = time;
//                                    }
//                                    else
//                                    {
//                                        try
//                                        {
//                                            List<double> values = new List<double>();
//                                            for (int i = 0; i < dtChoise.Rows.Count; i++)
//                                            {
//                                                double.TryParse(dtChoise.Rows[i][colName].ToString(), out double v);
//                                                values.Add(v);
//                                            }
//                                            StdDeviation(values, true, true, out double avgOut, out double stdDEVout);
//                                            string stdRounded = stdDEVout.ToString("0.00000");
//                                            string avgRounded = avgOut.ToString("0.000");
//                                            DT_MeasAVG.Rows[0][colName] = avgRounded;
//                                            DT_MeasAVG.Rows[1][colName] = stdRounded;
//                                        }
//                                        catch(Exception e) { WriteLog("MeasAVG", e); }
//                                    }
//                                }
//                            }, null);
//                        });
//                    }
//                    catch (Exception e) { WriteLog("MeasAVG", e); } 
//                }
//            }
//        }

//        public static void StdDeviation(List<double> values, bool as_sample, bool StdDeviation, out double avgOUT, out double stdDEVout)
//        {
//            // Get the mean.
//            double avg = values.Sum() / values.Count();
//            avgOUT = avg;
//            stdDEVout = 0;
//            if (StdDeviation)
//            {
//                // Get the sum of the squares of the differences
//                // between the values and the mean.
//                var squares_query = from double value in values select (value - avg) * (value - avg);
//                double sum_of_squares = squares_query.Sum();

//                if (as_sample)
//                { stdDEVout = Math.Sqrt(sum_of_squares / (values.Count() - 1)); }
//                else
//                { stdDEVout = Math.Sqrt(sum_of_squares / values.Count()); }
//            }
//        }


//        /*****************************************************************************
//        * Log Measurement:
//        '****************************************************************************/
//        public static bool isWritingMeas = false;
//        public static async void WriteMeasData(string path)
//        {
//            if (!isWritingMeas)
//            {
//                isWritingMeas = true;
//                try
//                {
//                    await Task.Run(() =>
//                    {
//                        if (path != "")
//                        {
//                            if (!File.Exists(path))
//                            {
//                                DataView dv = DT_MeasAll.DefaultView;
//                                dv.Sort = "datetime asc";
//                                dv.ToTable().WriteCsv(path);
//                            }
//                            else
//                            {
//                                try
//                                { DT_Meas_All_Report.WriteCsv(path); }
//                                catch { }
//                            }
//                        }
//                    });
//                }
//                catch
//                { }
//                isWritingMeas = false;
//            }
//        }

//    }
//}
