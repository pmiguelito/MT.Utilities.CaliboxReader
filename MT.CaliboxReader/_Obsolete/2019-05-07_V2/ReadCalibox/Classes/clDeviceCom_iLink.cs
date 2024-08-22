using mtpro.AdapterProtocol.Messages;
using mtpro.AdapterProtocol.Messages.Base;
using mtpro.M400.Messages;
using mtpro.M400.Messages.Base;
using System;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using static ReadCalibox.clGlobals;

namespace ReadCalibox
{
    public class clDeviceCom_iLink
    {
        //public static frmMain g_frmMain { get; set; }

        public static SerialPort serialPort { get; set; }
        //public delegate void dgOnDeviceM400Response(string command, M400ResponseMessage rspMsg);
        //public static event dgOnDeviceM400Response onDeviceM400Response;

        /*******************************************************************************************************************
       'FUNCTION:    read Dll and insert in DB
       ********************************************************************************************************************/
        private static DataTable _DT_CMD_Group;
        public static DataTable DT_CMD_Group
        {
            get
            {
                if (_DT_CMD_Group == null)
                {
                    _DT_CMD_Group = getCMDfromDBgroup();
                    //g_frmMain.cboCommand.DataSource = _DT_CMD_Group;
                    //g_frmMain.cboCommand.DisplayMember = "opcmd";
                }
                return _DT_CMD_Group;
            }
            set { _DT_CMD_Group = value; }
        }
        static DataTable _DT_CMD_DB;
        public static DataTable DT_CMD_DB
        {
            get
            { return DT_DLL; }
            set { DT_DLL = value; }
        }
        private static DataTable _DT_DLL;
        public static DataTable DT_DLL
        {
            get
            {
                if (_DT_DLL == null) { load_DLL_Classes(); }
                return _DT_DLL;
            }
            set
            {
                if (_DT_DLL == null) { addColumns_Commands(ref _DT_DLL); }
                _DT_DLL = value;
            }
        }
        static void addColumns_Commands(ref DataTable DT, string dtName = "DeviceCommands")
        {
            if (DT == null)
            { DT = new DataTable(dtName); }
            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("value", typeof(string));
                DT.Columns.Add("desc", typeof(string));
                DT.Columns.Add("opcode", typeof(string));
                DT.Columns.Add("property", typeof(string));
                DT.Columns.Add("data_type", typeof(string));
                DT.Columns.Add("command", typeof(string));
                DT.Columns.Add("class_command", typeof(string));
                DT.Columns.Add("class_response", typeof(string));
                DT.Columns.Add("protocol", typeof(string));
                DT.Columns.Add("class_Name", typeof(string));
                DT.Columns.Add("class_FullName", typeof(string));
                DT.Columns.Add("dll_FullPath", typeof(string));
                DT.Columns.Add("endpoint", typeof(string));
                DT.Columns.Add("endpoint_addr", typeof(string));
            }
            else { DT.Rows.Clear(); }
        }
        private static string SQLquery_Select_tBoardsCMD = "SELECT * FROM tBoardsCMD ORDER BY [command], [property]";
        public static void getCMDfromDB()
        {
            try
            {
                _DT_CMD_DB = new DataTable("tBoardsCMD");
                //clDatabase.getDataTable(SQLquery_Select_tBoardsCMD, ref _DT_CMD_DB,out int count, g_frmMain.m_ODBC_EK);
                if(_DT_CMD_DB == null) { addColumns_Commands(ref _DT_CMD_DB); }
                //else if(count==0) { addColumns_Commands(ref _DT_CMD_DB); }
                try { _DT_CMD_DB.Columns.Add("value", typeof(string)); } catch { }
                _DT_CMD_DB.Columns["value"].SetOrdinal(1);
                _DT_CMD_Group = DT_CMD_Group;
            }
            catch { }
        }

        public static bool getAllCMDs_FromDLLandDB()
        {
            load_DLL_Classes();
            getCMDfromDB();
            comparDBcmd();
            getCMDfromDB();
            getCMDfromDBgroup();
            //g_frmMain.m_BS_Device_CMD.DataSource = DT_CMD_DB;
            return true;
        }
        public static bool load_DLL_Classes()
        {
            addColumns_Commands(ref _DT_DLL, "DLLcommands");
            getPropertiesFromDll("M400.dll");
            getPropertiesFromDll("AdapterProtocol.dll");
            getOpcode(_DT_DLL);
            return true;
        }
        private static void getPropertiesFromDll(string dll)
        {
            try
            {
                string fullpath = Path.GetFullPath(@".\" + dll);
                Assembly mA = Assembly.LoadFrom(fullpath);
                System.Collections.Generic.IEnumerable<TypeInfo> tiClasses = mA.DefinedTypes;
                foreach (var item in tiClasses)
                {
                    string name = item.Name;
                    if (!name.Substring(0, 1).Contains("_") && !name.Contains("<"))
                    {
                        var props = item.GetProperties();
                        foreach (var prop in props)
                        {
                            DataRow row = DT_DLL.NewRow();
                            row["class_FullName"] = item.FullName;
                            row["class_Name"] = item.Name;
                            row["command"] = dllCommandString(item.Name); 
                            row["class_command"] = item.Name.Contains("CommandMessage") ? item.Name : "";
                            row["class_response"] = item.Name.Contains("ResponseMessage") ? item.Name : "";
                            row["property"] = prop.Name;
                            row["data_type"] = prop.PropertyType.Name;
                            row["dll_FullPath"] = dll;
                            DT_DLL.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception e) { }
        }
        private static string dllCommandString(string command)
        {
            string result = command.Replace("CommandMessage", "").Replace("ResponseMessage", "");
            string commLower = result.ToLower();
            string commStart = commLower.Substring(0, 3);
            if (commStart.Contains("get") || commStart.Contains("set"))
            {
                commStart = command.Substring(0, 3).ToLower().Replace("get", "GET_").Replace("set", "SET_");
                if (result.Contains("ISMDataConst")) { result = result.Replace("ISMDataConst", "ISMData"); }
                result = commStart + result.Substring(3);
            }
            else if (commLower.Contains("write") || commLower.Contains("reset") || commLower.Contains("erase"))
            { result = $"SET_{result}"; }
            else if (commLower.Contains("read"))
            { result = $"GET_{result}"; }
            return result;
        }
        private static void comparDBcmd()
        {
            DataRow[] rowsSelected = null;
            foreach(DataRow row in DT_DLL.Rows)
            {
                string command = row["command"].ToString().ToLower();
                if (command != "adaptermessage" && command != "error" && command != "m400" && command != "m400error" && command != "m400message" && !string.IsNullOrEmpty(command))
                {
                    string property = row["property"].ToString();
                    string opcode = row["opcode"].ToString();
                    string select = $"command = '{command}' AND property = '{property}'";
                    rowsSelected = DT_CMD_DB.Select(select);
                    if (rowsSelected.Length == 0)
                    { insertCMDinDB(row); }
                    else if (!string.IsNullOrEmpty(opcode))
                    {
                        select = $"command = '{command}' AND property = '{property}' AND opcode = '{opcode}'";
                        rowsSelected = DT_CMD_DB.Select(select);
                        if (rowsSelected.Length == 0)
                        { updateCMDinDB(row); }
                    }
                }
            }
        }
        private static void insertCMDinDB(DataRow row)
        {
            string opcode = row["opcode"].ToString();
            string sqlQuery="";
            if (!string.IsNullOrEmpty(opcode))
            {
                sqlQuery = "INSERT INTO tBoardsCMD " +
                  "(active, opcode, property, datatype, command, class_command, class_response, class_name, class_fullname, dll_fullpath, [desc], endpoint, endpoint_addr)" +
                  "VALUES " +
                  "( " + 1 + ",'" + opcode + "', '" + row["property"].ToString() + "', '" + row["data_type"].ToString() + "', '" + row["command"].ToString() + "', '"
                  + row["class_command"].ToString() + "', '" + row["class_response"].ToString() + "', '" + row["class_name"].ToString() + "', '"
                  + row["class_fullname"].ToString() + "', '" + row["dll_fullpath"].ToString() + "', '" + row["desc"].ToString()
                  + "', '" + row["endpoint"].ToString() + "', '" + row["endpoint_addr"].ToString() + "' )";
            }
            else
            {
                sqlQuery = "INSERT INTO tBoardsCMD " +
                  "(active, property, datatype, command, class_command, class_response, class_name, class_fullname, dll_fullpath, endpoint, endpoint_addr)" +
                  "VALUES " +
                  "( " + 0 + ", '" + row["property"].ToString() + "', '" + row["data_type"].ToString() + "', '" + row["command"].ToString() + "', '"
                  + row["class_command"].ToString() + "', '" + row["class_response"].ToString() + "', '" + row["class_name"].ToString() + "', '"
                  + row["class_fullname"].ToString() + "', '" + row["dll_fullpath"].ToString() + "', '" 
                  + row["endpoint"].ToString() + "', '" + row["endpoint_addr"].ToString() + "' )";
            }
            //clDatabase.insertRow(sqlQuery);
        }
        private static void updateCMDinDB(string opcode, string command)
        {
            string sqlQuery = $"UPDATE tBoardsCMD SET [opcode] = '{opcode}' WHERE [command] = '{command}'";
            //clDatabase.updateRow(sqlQuery);
        }
        private static void updateCMDinDB(DataRow row)
        {
            string sqlQuery = $"UPDATE tBoardsCMD SET [opcode] = '{row["opcode"].ToString()}' WHERE [command] = '{row["command"].ToString()}'";
            //clDatabase.updateRow(sqlQuery);
        }

        static string SQLquery_Select_CMDGroup = "SELECT opcode, command, active, (opcode + ': ' + command) AS opcmd FROM tBoardsCMD " +
                                                 "GROUP BY command, opcode, active HAVING (command IS NOT NULL) AND (active = 1) ORDER BY command";
        private static DataTable getCMDfromDBgroup()
        {
            DataTable dt = new DataTable("tBoardsCMD");
            //clDatabase.getDataTable(SQLquery_Select_CMDGroup, ref dt, g_frmMain.m_ODBC_EK);
            return dt;
            //DT_CMD = dt.Select("[command] is not null AND [command] <>'' ").CopyToDataTable();
        }
        private static void getOpcode(DataTable dt)
        {
            _DT_CMD_DB = dt.Copy();
            string rspValue = "";
            string commandStore = "";
            foreach (DataRow row in dt.Rows)
            {
                string command = row["command"].ToString().Trim().ToLower();
                string opcode = row["opcode"].ToString().Trim();
                if (command != commandStore)
                {
                    commandStore = command;
                    if (command != "adaptermessage" && command != "error" && command != "m400" && command != "m400error" && command != "m400message" && !string.IsNullOrEmpty(command))
                    {
                        if (!string.IsNullOrEmpty(command) && string.IsNullOrEmpty(opcode))
                        { readDevice_DT(command, "", "", ref rspValue, true); }
                    }
                }
            }
            DT_DLL = DT_CMD_DB.Copy();
            _DT_CMD_DB = null;
        }

        /*******************************************************************************************************************
       'FUNCTION:    read Device
       '*******************************************************************************************************************/
        static UC_Channel Channel;
        public static void getDevice_AllInfos(SerialPort sp, UC_Channel channel)
        {
            serialPort = sp;
            Channel = channel;
            bool error = false; 
            error = !readDevice_DT("GET_Identification", "DeviceTag");
            if (!error)
            { error = !readDevice_DT("GET_ProtocolVersion", "ProtocolVersion"); }
            //if (!error) { error = !readDevice_DT("GET_DeviceInformation"); }
            //if (!error) { error = !readDevice_DT("GET_EndpointList"); }
            //if (!error) { error = !readDevice_DT("GET_EndPointFlags"); }
            //if (!error) { error = !readDevice_DT("GET_PrefComm"); }
            //if (!error) { error = !readDevice_DT("GET_PowerStatus"); }
            //if (!error) { error = !readDevice_DT("GET_TranspParam"); }
            //g_frmMain.m_BS_Device_CMD.DataSource = DT_CMD_DB;
        }

        public static void getDeviceM400_AllInfos()
        {
            bool error = !readDevice_DT("g000");
            if (!error) { error = !readDevice_DT("g002"); }
            if (!error) { error = !readDevice_DT("g003"); }
            if (!error) { error = !readDevice_DT("g049"); }
            if (!error) { error = !readDevice_DT("g052"); }
            if (!error) { error = !readDevice_DT("g100"); }
            if (!error) { error = !readDevice_DT("g124"); }
            if (!error) { error = !readDevice_DT("g517"); }
            if (!error) { error = !readDevice_DT("g518"); }
            //g_frmMain.m_BS_Device_CMD.DataSource = DT_CMD_DB;
        }
        
        public static bool readDevice_DT(string command, string property = "")
        {
            string rspValue = "";
            return readDevice_DT(command, property, "", ref rspValue, false);
        }
        public static bool readDevice_DT(string command, string property, string value, ref string rspValue, bool dontSend, SerialPort port = null)
        {
            port = serialPort;
            rspValue = "";
            bool NoError = true;
            string selection = $"(opcode = '{command}' OR command = '{command}' OR class_command = '{command}' OR class_response = '{command}')";
            DataRow[] rows = DT_CMD_DB.Select(selection);
            if (rows.Length > 0)
            {
                DataTable rowsSelected = rows.CopyToDataTable();
                DataRow row = rows[0];
                command = row["command"].ToString().ToLower(); /* command can be opcode */
                string protocol = row["class_FullName"].ToString();
                string opcode = row["opcode"].ToString();
                if (DT_CMD_DB.Select($"command = '{command}'").Length > 0)
                {
                    string path = row["dll_FullPath"].ToString();
                    string fullpath = Path.GetFullPath(@".\" + path);
                    DataTable dt = DT_CMD_DB.Select($"command = '{command}'").CopyToDataTable();
                    DataRow[] rowRSP = dt.Select("[class_response] IS NOT NULL AND [class_response] <> ''");
                    DataRow[] rowCMD = dt.Select("[class_command] IS NOT NULL AND [class_command] <> ''");
                    Assembly mA = Assembly.LoadFrom(fullpath);
                    Type mTypeRSP = null;
                    Type mTypeCMD = null;
                    try { mTypeRSP = mA.GetType(rowRSP[0]["class_FullName"].ToString()); } catch { }
                    try { mTypeCMD = mA.GetType(rowCMD[0]["class_FullName"].ToString()); } catch { }
                    switch (protocol.ToLower().Contains("m400"))
                    {
                        /*device commands*/
                        case false:
                            string endpoint = row["endpoint"].ToString();
                            string endpoint_addr = row["endpoint_addr"].ToString();
                            try
                            {
                                byte endP = 1;
                                object[] nCMD = new object[] { endP };
                                byte endPaddr;
                                CommandMessage cmd = null;
                                if (string.IsNullOrEmpty(endpoint) || endpoint == "0")
                                { try { cmd = (CommandMessage)Activator.CreateInstance(mTypeCMD); } catch { endpoint = endP.ToString(); } }
                                else if (!string.IsNullOrEmpty(endpoint) && string.IsNullOrEmpty(endpoint_addr) && string.IsNullOrEmpty(value))
                                { if (endpoint != "1") { endP = Convert.ToByte(endpoint); }; nCMD = new object[] { endP }; }
                                else if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(endpoint_addr) && string.IsNullOrEmpty(value))
                                { if (endpoint != "1") { endP = Convert.ToByte(endpoint); }; endPaddr = Convert.ToByte(endpoint_addr); nCMD = new object[] { endP, endPaddr }; }
                                else if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(endpoint_addr) && string.IsNullOrEmpty(value))
                                { if (endpoint != "1") { endP = Convert.ToByte(endpoint); }; endPaddr = Convert.ToByte(endpoint_addr); nCMD = new object[] { endP, endPaddr }; }
                                else if (!string.IsNullOrEmpty(endpoint) && string.IsNullOrEmpty(endpoint_addr) && !string.IsNullOrEmpty(value))
                                { if (endpoint != "1") { endP = Convert.ToByte(endpoint); }; nCMD = new object[] { endP, Convert.ToInt32(value), 10 }; }
                                if (cmd == null)
                                {
                                    try { cmd = (CommandMessage)Activator.CreateInstance(mTypeCMD, nCMD); }
                                    catch
                                    {
                                        nCMD = new object[] { endP }; 
                                        try { cmd = (CommandMessage)Activator.CreateInstance(mTypeCMD, nCMD); }
                                        catch
                                        {
                                            nCMD = new object[] { endP, 0, 10 }; 
                                            try { cmd = (CommandMessage)Activator.CreateInstance(mTypeCMD, nCMD); }
                                            catch
                                            {
                                                endP = 3;  endPaddr = 1; endpoint_addr = "1"; nCMD = new object[] { endP, endPaddr };
                                                try { cmd = (CommandMessage)Activator.CreateInstance(mTypeCMD, nCMD); }
                                                catch
                                                {
                                                    endP = 6; endPaddr = 1; endpoint_addr = "1"; nCMD = new object[] { endP, endPaddr };
                                                    try { cmd = (CommandMessage)Activator.CreateInstance(mTypeCMD, nCMD); }
                                                    catch (Exception ex)
                                                    {
                                                        //g_frmMain.logText("command " + command, !dontSend, ex: ex); NoError = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (cmd != null)
                                {
                                    updateDT(command, cmd.Opcode, cmd.Endpoint.ToString(), endpoint_addr);
                                    if (!dontSend)
                                    {
                                        AdapterMessage.AdapterMessagePowerState powerState = AdapterMessage.AdapterMessagePowerState.MoreThan90;
                                        if (!serialPort.IsOpen) { serialPort.Open(); }
                                        object[] n = new object[] { powerState };
                                        ResponseMessage rsp = null;
                                        try { rsp = (ResponseMessage)Activator.CreateInstance(mTypeRSP, n); }
                                        catch { n = new object[] { (byte)1, powerState }; rsp = (ResponseMessage)Activator.CreateInstance(mTypeRSP, n); }
                                        rsp = mtpro.AdapterProtocol.ProtocolHandler.SendCommand(port, cmd);
                                        readDevice_DTCommand(selection, property, rsp, ref rspValue);
                                        try
                                        { Channel.MessageChannel = rsp.Opcode + "\t" + rspValue; }
                                        catch { }
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                //g_frmMain.logText("command: " + command, !dontSend, ex: ee); NoError = false;
                            }
                            break;
                        /*Sensor commands*/
                        case true:
                            try
                            {
                                if (mTypeCMD != null)
                                {
                                    M400CommandMessage cmd = (M400CommandMessage)Activator.CreateInstance(mTypeCMD);
                                    if (cmd != null)
                                    {
                                        updateDT(command, cmd.Opcode);
                                        if (!dontSend)
                                        {
                                            serialPort.Open();
                                            if (!writeProperties(mTypeCMD, command, property, value, ref cmd))
                                            { return false; }
                                            try
                                            {
                                                if (mTypeRSP != null)
                                                {
                                                    M400ResponseMessage rsp = (M400ResponseMessage)Activator.CreateInstance(mTypeRSP);
                                                    try
                                                    {
                                                        rsp = mtpro.M400.ProtocolHandler.SendCommand(serialPort, cmd);
                                                        readDevice_DTCommand(selection, property, rsp, ref rspValue);
                                                    }
                                                    catch {
                                                        //g_frmMain.logText($"command {command}", !dontSend, m400Msg: rsp); NoError = false;
                                                    }
                                                }
                                                else
                                                { mtpro.M400.ProtocolHandler.SendCommand(port, cmd); }
                                            }
                                            catch (Exception e) {
                                                //g_frmMain.logText($"command {command}", !dontSend, ex: e); NoError = false;
                                            }
                                        }
                                    }
                                }
                                else {
                                    //g_frmMain.logText($"command {command} cmd Type is null", !dontSend); NoError = false;
                                }
                            }
                            catch (Exception ee) {
                                //g_frmMain.logText($"Error: command {command}", !dontSend, ex: ee); NoError = false;
                            }
                            break;
                    }
                    if (!dontSend)
                    {
                        string select = $"[command] = '{command}' AND ([opcode] <>'')";
                        if (DT_CMD_DB.Select(select).Length == 0 && !string.IsNullOrEmpty(opcode))
                        { updateCMDinDB(opcode, command); }
                    }
                }
            }
            return NoError;
        }
        private static bool readDevice_DTCommand(string searcher, string property, M400ResponseMessage rspMsg, ref string result)
        {
            try
            {
                string prop = "";
                PropertyInfo proptype;
                DataRow[] dtRows = DT_CMD_DB.Select(searcher);
                foreach (DataRow row in dtRows)
                {
                    prop = row["property"].ToString();
                    proptype = rspMsg.GetType().GetProperty(prop);
                    if (proptype != null)
                    {
                        var value = proptype.GetValue(rspMsg);
                        string valueTXT = "";
                        string propName = proptype.PropertyType.Name;

                        switch (propName)
                        {
                            case "Char[]":
                                valueTXT = new string((char[])value);
                                break;
                            case "Byte[]":
                                foreach (byte b in (byte[])value)
                                { valueTXT += b + ","; }
                                try { valueTXT = valueTXT.Substring(0, valueTXT.Length - 1); }
                                catch
                                { try { valueTXT = Convert.ToString(valueTXT); } catch { } }
                                break;
                            default:
                                valueTXT = Convert.ToString(value);
                                break;
                        }
                        row["value"] = valueTXT;
                        if (property == propName) { result = valueTXT; }
                    }
                    else
                    {
                        //g_frmMain.logText($"{searcher} property not found: {prop}");
                    }
                }
            }
            catch (Exception e) {
                //g_frmMain.logText(e.Message);
            }
            return true;
        }
        private static bool readDevice_DTCommand(string searcher, string property, ResponseMessage rspMsg, ref string result)
        {
            try
            {
                string prop = "";
                PropertyInfo proptype;
                DataRow[] dtRows = DT_CMD_DB.Select(searcher);
                foreach (DataRow row in dtRows)
                {
                    prop = row["property"].ToString();
                    proptype = rspMsg.GetType().GetProperty(prop);
                    if (proptype != null)
                    {
                        var value = proptype.GetValue(rspMsg);
                        string valueTXT = "";
                        string propTypeName = proptype.PropertyType.Name.ToLower() ;
                        switch (propTypeName)
                        {
                            case "char[]":
                                valueTXT = new string((char[])value);
                                break;
                            case "byte[]":
                                byte[] bA = (byte[])value;
                                foreach (byte b in bA)
                                { valueTXT += "["+b + "]"; }
                                break;
                            default:
                                valueTXT = Convert.ToString(value);
                                break;
                        }
                        row["value"] = valueTXT;
                        if (property.ToLower() == prop.ToLower())
                        {
                            result = valueTXT;
                        }
                    }
                    else
                    {
                        //g_frmMain.logText(searcher + " property not found: " + prop);
                    }
                }
            }
            catch (Exception e) {
                //g_frmMain.logText(e.Message);
            }
            return true;
        }

        private static void updateDT(string command, string opcode, string endpoint = null, string endpointAddr = null)
        {
            bool opcodeExists = false;
            bool endpointExists = false;
            bool addrExists = false;
            //if (row["opcode"].ToString() != opcode)
            {
                DataRow[] drows = DT_CMD_DB.Select("[command] = '" + command + "'");
                foreach (DataRow r in drows)
                {
                    if (r["opcode"].ToString() != opcode && !opcodeExists)
                    {
                        r["opcode"] = opcode;
                        r["desc"] = opcode + " " + r["property"].ToString();
                    }
                    else { opcodeExists = true; }
                    if (!string.IsNullOrEmpty(endpoint) && !endpointExists)
                    {
                        if (r["endpoint"].ToString() != endpoint && !endpointExists)
                        { r["endpoint"] = endpoint; }
                        else { endpointExists = true; }
                    }
                    else { endpointExists = true; }
                    if (!string.IsNullOrEmpty(endpointAddr) && !addrExists)
                    {
                        if (r["endpoint_addr"].ToString() != endpointAddr && !addrExists)
                        { r["endpoint_addr"] = endpointAddr; }
                        else { addrExists = true; }
                    }
                    else { addrExists = true; }
                    if (opcodeExists && endpointExists && addrExists) { break; }
                }
            }
        }

        private static bool writeProperties(Type cmdType, string command, string property, string value, ref M400CommandMessage cmd)
        {
            if (command.Contains("set_"))
            {
                string commandGET = command.Replace("set_", "get_");
                string sel = String.Format("command = '{0}'", commandGET);
                DataRow[] rows = DT_CMD_DB.Select(sel);
                if (rows.Length > 1)
                {
                    DataTable dtSelection = rows.CopyToDataTable();
                    PropertyInfo[] properties = cmdType.GetProperties();
                    string result = "";
                    foreach (PropertyInfo pi in properties)
                    {
                        string propName = pi.Name.ToLower();
                        if (propName != "opcode" && propName != "errorcode")
                        {
                            sel = String.Format("command = '{0}' AND property = '{1}'", commandGET, propName);
                            DataRow row = DT_CMD_DB.Select(sel)[0];
                            string datatype = pi.PropertyType.Name.ToString();
                            if (property.ToLower() == propName)
                            { result = value; }
                            else
                            { result = row["value"].ToString(); }
                            switch (datatype.ToLower())
                            {
                                case "single":
                                    Single r = Convert.ToSingle(result);
                                    pi.SetValue(cmd, r, null);
                                    break;
                                case "char[]":
                                    string sV = new String((char[])pi.GetValue(cmd));
                                    int lV = sV.Length;
                                    int lR = result.Length;
                                    while (lR < lV)
                                    {
                                        result += " ";
                                        lR = result.Length;
                                    }
                                    Char[] rCharArray = result.ToCharArray();
                                    pi.SetValue(cmd, rCharArray, null);
                                    break;
                                case "byte":
                                    byte rByte = Convert.ToByte(result);
                                    pi.SetValue(cmd, rByte, null);
                                    break;
                                case "byte[]":
                                    byte[] rByteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(result);
                                    pi.SetValue(cmd, rByteArray, null);
                                    break;
                                case "uint16":
                                    UInt16 rUInt16 = Convert.ToUInt16(result);
                                    pi.SetValue(cmd, rUInt16, null);
                                    break;
                                case "uint32":
                                    UInt32 rUInt32 = Convert.ToUInt32(result);
                                    pi.SetValue(cmd, rUInt32, null);
                                    break;
                                default:
                                    pi.SetValue(cmd, result, null);
                                    break;
                            }

                        }
                    }
                }
                else { return false; }
            }

            return true;
        }


        /*******************************************************************************************************************
        'FUNCTION:    Device Power ON/OFF power state And DeviceInfos
        ********************************************************************************************************************/
        public static bool setPowerOnOff(bool powerOn, bool forceClose)
        {
            //return setPowerOnOff(powerOn, true, g_frmMain.chkILinkMulti.Checked);
            return setPowerOnOff(powerOn, true, true);
        }
        public static bool setPowerOnOff(bool powerOn = true, bool forceClose = false, bool forcePowerOn = true)
        {
            string flags = "0";
            if (powerOn) { flags = "1"; }
            bool executed = false;
            //if (g_frmMain.chkDeviceActive.Checked && forcePowerOn && !g_frmMain.m_Error_Device)
            {
                string rspValue = "";
                executed = readDevice_DT("SET_PowerControl", "RespFlags", flags, ref rspValue, false);
                //g_frmMain.m_powerOn = rspValue == "1";
                executed = true;
            }
            if (forceClose) { serialPort.Close(); }
            return executed;
        }

        public static bool getPowerState(bool forceClose = false)
        {
            bool executed = false;
            //if (g_frmMain.chkDeviceActive.Checked && g_frmMain.chkILinkMulti.Checked && !g_frmMain.m_Error_Device)
            {
                string rspValue = "";
                executed = readDevice_DT("GET_PowerStatus", "ResponseFlags", "", ref rspValue, false);
                //g_frmMain.m_powerOn = rspValue == "1";
                executed = true;
            }
            if (forceClose) { serialPort.Close(); }
            return executed;
        }
        public static bool getDeviceInfo(bool forceClose = false, bool showInGui = false, SerialPort port = null)
        {
            bool executed = false;
            //if (g_frmMain.chkDeviceActive.Checked)
            //{
            //    string rspValue = "";
            //    if (readDevice_DT("GET_DeviceInformation", "", "", ref rspValue, false))
            //    {
            //        string selection = "command = 'GET_DeviceInformation'";
            //        DataRow[] rows = DT_CMD_DB.Select(selection);
            //        if (rows.Length > 0)
            //        {
            //            DataTable dt = rows.CopyToDataTable();
            //            g_frmMain.txtDeviceDesc.Text = dt.Select("property ='DeviceDescription'")[0]["value"].ToString();
            //            g_frmMain.txtDeviceFW.Text = dt.Select("property ='FirmwareVersion'")[0]["value"].ToString();
            //            g_frmMain.txtDeviceHW.Text = dt.Select("property ='HardwareVersion'")[0]["value"].ToString();
            //            g_frmMain.txtDeviceSN.Text = dt.Select("property ='SerialNumber'")[0]["value"].ToString();
            //        }
            //        executed = true;
            //    }
            //}
            //serialPort.Close();
            return executed;
        }


        /*******************************************************************************************************************
        'FUNCTION:    Device Test Functions
        ********************************************************************************************************************/
        [Obsolete("use for test", false)]
        public static bool readDevice(string command, int flags = 0, bool showInGui = false)
        {
            if (!serialPort.IsOpen) { return false; }
            CommandMessage cmdMsg = null;
            ResponseMessage rspMsg = null;
            byte endPoint = 1;
            AdapterMessage.AdapterMessagePowerState powerState = AdapterMessage.AdapterMessagePowerState.MoreThan90;
            switch (command)
            {
                case "r011":
                case "GET_DeviceInformation":
                    try
                    {
                        cmdMsg = new GetDeviceInformationCommandMessage();
                        rspMsg = new GetDeviceInformationResponseMessage(powerState);
                    }
                    catch { }
                    break;
                case "w040":
                case "SET_PowerControl":
                    try
                    {
                        cmdMsg = new SetPowerControlCommandMessage(endPoint, flags, 10);
                        rspMsg = new SetPowerControlResponseMessage(powerState);
                    }
                    catch { }
                    break;
                case "r040":
                case "GET_PowerStatus":
                    try
                    {
                        cmdMsg = new GetPowerStatusCommandMessage(endPoint);
                        rspMsg = new GetPowerStatusResponseMessage(powerState);
                    }
                    catch { }
                    break;
                case "r701":
                case "GET_Read8bitAddress":
                    try
                    {
                        endPoint = 3;
                        byte addr = 1;
                        cmdMsg = new Read8bitAddressCommandMessage(endPoint, addr);
                        byte[] vs = new byte[] { 1, 3 };
                        rspMsg = new Read8bitAddressResponseMessage(vs, powerState);
                    }
                    catch { }
                    break;
                default:
                    //g_frmMain.logText("Error: readDevice command: " + command, true);
                    serialPort.Close();
                    return false;
            }

            try
            { rspMsg = mtpro.AdapterProtocol.ProtocolHandler.SendCommand(serialPort, cmdMsg); }
            catch (Exception ee)
            {
                //g_frmMain.logText("Error: readDevice command: " + command, showInGui, ee);
                //g_frmMain.m_Error_Device = true;
                serialPort.Close();
                return false;
            }

            if (rspMsg != null && rspMsg.ErrorCode == AdapterMessage.AdapterMessageErrorCode.NoError)
            { readDeviceResponse(command, rspMsg); }
            serialPort.Close();
            return true;
        }
        [Obsolete("use for test", false)]
        private static void readDeviceResponse(string command, ResponseMessage rspMsg)
        {
            switch (command)
            {
                case "w040":
                case "SET_PowerControl":
                    if (((SetPowerControlResponseMessage)rspMsg).RespFlags == 1)
                    { }
                    //{ g_frmMain.m_powerOn = true; } else { g_frmMain.m_powerOn = false; }
                    break;
                case "r040":
                case "GET_PowerStatus":
                    if (((GetPowerStatusResponseMessage)rspMsg).ResponseFlags == 1)
                    { }
                    //{ g_frmMain.m_powerOn = true; } else { g_frmMain.m_powerOn = false; }
                    break;
                case "r011":
                case "GET_DeviceInformation":
                    //g_frmMain.txtDeviceDesc.Text = string.Join("", ((GetDeviceInformationResponseMessage)rspMsg).DeviceDescription);
                    //g_frmMain.txtDeviceFW.Text = string.Join("", ((GetDeviceInformationResponseMessage)rspMsg).FirmwareVersion);
                    //g_frmMain.txtDeviceHW.Text = string.Join("", ((GetDeviceInformationResponseMessage)rspMsg).HardwareVersion);
                    //g_frmMain.txtDeviceSN.Text = string.Join("", ((GetDeviceInformationResponseMessage)rspMsg).SerialNumber);
                    break;
                case "r701":
                case "GET_Read8bitAddress":
                    string result = string.Join("", ((Read8bitAddressResponseMessage)rspMsg).Data);
                    break;
            }
        }

    }
}
