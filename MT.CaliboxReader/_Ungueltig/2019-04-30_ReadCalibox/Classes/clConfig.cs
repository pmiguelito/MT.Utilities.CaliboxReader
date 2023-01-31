using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTDI;
using STDhelper;
using static STDhelper.clLogging;


namespace ReadCalibox
{
    public static class clConfig
    {
        public static Element_InitParameters Config_Initvalues { get; set; }
        public static Element_ChangeParameters Config_ChangeParameters { get; set; }
        public static Register_ComPorts Config_ComPorts { get; set; }

        /***************************************************************************************
        * Configuration:    SerialPort Load
        *                   Reference to other Users Controls
        ****************************************************************************************/

        private static List<UC_COM> _Config_PortList;
        public static List<UC_COM> Config_PortList
        {
            get
            {
                if(_Config_PortList == null)
                {
                    try
                    { Load_SerialPorts(Config_Initvalues.CHquantity); }
                    catch { Load_SerialPorts(1); }
                }
                return _Config_PortList;
            }
            set
            {
                _Config_PortList = value;
            }
        }

        /***************************************************************************************
        * Configuration:    Channels Load
        *                   Reference to other Users Controls
        ****************************************************************************************/

        private static List<UC_Channel> _Config_ChannelsList;
        public static List<UC_Channel> Config_ChannelsList
        {
            get
            {
                if(_Config_ChannelsList == null) { Load_Channels(); }
                return _Config_ChannelsList;
            }
            set { _Config_ChannelsList = value; }
        }
        public static List<UC_Channel> Load_Channels()
        {
            try
            {
                _Config_ChannelsList = new List<UC_Channel>();
                foreach (UC_COM com in Config_PortList)
                { _Config_ChannelsList.Add(new UC_Channel(com)); }
            }
            catch { }
            return _Config_ChannelsList;
        }

        /***************************************************************************************
        * Configuration:    BeM Load
        *                   Reference to other Users Controls
        ****************************************************************************************/
        [Obsolete("replace with Config_BeMsList", false)]
        private static List<Element_BeM> Config_BeMList { get; set; }
        private static List<UC_BeM> _Config_BeMsList;
        public static List<UC_BeM> Config_BeMsList
        {
            get
            {
                if (_Config_BeMsList == null) { Load_Config_BeM(); }
                return _Config_BeMsList;
            }
            set { _Config_BeMsList = value; }
        }
        public static void Load_Config_BeM()
        {
            _Config_BeMsList = new List<UC_BeM>();
            foreach (Element_BeM uc in Config_BeMList)
            {
                _Config_BeMsList.Add(new UC_BeM(uc));
            }
        }
        public static int Config_BeMsListCount { get { return Config_BeMsList.Count; } }

        /***************************************************************************************
        * Configuration:    Constructor and Initialization
        ****************************************************************************************/
        private static void Error(string path, bool active)
        {
            ErrorLineSeparator = true;
            PathLog = path;
            PathLog_Active = active;
        }
        public static void Load()
        {
            Load_InitValues();
            Error(Config_Initvalues.Log_Path, Config_Initvalues.Log_Active);
            Load_ChangeValues();
            Load_BeM();
            Load_SerialPorts(Config_Initvalues.CHquantity);
        }

        private static void Load_InitValues()
        {
            Register_InitParameters Config_InitParameters = Register_InitParameters.GetConfig();
            if (Config_InitParameters.Count < 1)
            {
                Config_InitParameters.New();
            }
            Config_Initvalues = Config_InitParameters.InitParameters[0];
        }
        private static void Load_ChangeValues()
        {
            Register_ChangeParameters Config_ChangeParameters = Register_ChangeParameters.GetConfig();
            if (Config_ChangeParameters.Count < 1)
            {
                Config_ChangeParameters.New();
            }
            clConfig.Config_ChangeParameters = Config_ChangeParameters.ChangeParameters[0];
        }

        private static void Load_BeM()
        {
            Register_BeM Config_BeM = Register_BeM.GetConfig();
            Config_BeMList = Config_BeM.Init_BeM_All();
        }

        private static void Load_SerialPorts(int chQuantity)
        {
            Config_ComPorts = Register_ComPorts.GetConfig();
            int count = Config_ComPorts.Count;
            if (chQuantity < 1) { chQuantity = 1; }
            for (int i = (count - 1); i < chQuantity; i++)
            { Config_ComPorts.New(); }
            if (chQuantity > count)
            { Register_ComPorts.Save(); }
            _Config_PortList = new List<UC_COM>();
            foreach (Element_ComPorts item in Config_ComPorts.ComPorts)
            {
                if (item.Index < Config_Initvalues.CHquantity)
                {
                    UC_COM uc = (new UC_COM()
                    {
                        Ch_no = item.Index,
                        Ch_name = item.Ch_Name,

                        ReadDelay = item.COMreadDelay.ToString(),
                        ModusFTDI = item.ModusFTDI,
                        ReadLine = item.BufferReadLine,

                        FTDIname = item.FTDIname,

                        PortName = item.SerialPortName,
                        BeM = item.BeM,

                        Baudrate = item.BaudRate,
                        HandShake_Selected = item.HandShake,
                        DataBits_Selected = item.DataBits,
                        StopBits_Selected = item.StopBits,
                        Parity_Selected = item.Parity,
                        Active = item.Active,

                        Initialize = true
                    });
                    _Config_PortList.Add(uc);
                }
                else { return; }
            }
        }

        public static void Save()
        {

        }

        public static void Save_UserChanges()
        {
            Register_ChangeParameters.Save();
        }

        

        /***************************************************************************************
        * Configuration:    FTDI
        ****************************************************************************************/
        public static clFTDI FTDI = new clFTDI();
        private static DateTime FTDIcheck;
        private static DataTable _DTFTDI;
        public static DataTable DTFTDI
        {
            get
            {
                if ((DateTime.Now - COMcheck).TotalSeconds > 15 || _DTFTDI == null)
                {
                    FTDIcheck = DateTime.Now;
                    _DTFTDI = FTDI.getDevices();
                }
                return _DTFTDI;
            }
            set { _DTFTDI = value; }
        }

        /***************************************************************************************
        * Configuration:    SerialPort Helpers
        ****************************************************************************************/

        private static DateTime COMcheck;
        private static string[] _SerialPortList;
        public static string[] SerialPortList
        {
            get
            {
                if ((DateTime.Now - COMcheck).TotalSeconds > 15 || _SerialPortList == null)
                {
                    COMcheck = DateTime.Now;
                    _SerialPortList = SerialPort.GetPortNames();
                }
                return _SerialPortList;
            }
            set { _SerialPortList = value; }
        }

        public static SerialPort SerialPort_Init(string portName, int baudrate, int databits = 8, Parity parity = Parity.None, StopBits stopbits = StopBits.One)
        {
            try
            {
                return new SerialPort()
                {
                    BaudRate = baudrate,
                    DataBits = databits,
                    DiscardNull = false,
                    DtrEnable = false,
                    Handshake = Handshake.None,
                    Parity = parity,
                    PortName = portName,
                    ReadBufferSize = 2048,
                    ReadTimeout = 1000,
                    RtsEnable = false,
                    StopBits = stopbits
                };
            }
            catch { }
            return new SerialPort();
        }

        /***************************************************************************************
        * Configuration:    SerialPort Default Values
        ****************************************************************************************/
        public static readonly int[] BaudRateList = new int[] { 4800, 9600, 14400, 19200, 38400, 57600, 115200 };

        private static string[] _HandShakingSelection;
        public static string[] HandShakingSelection { get { if (_HandShakingSelection == null) { HandShakingArray(); } return _HandShakingSelection; } }
        private static void HandShakingArray()
        {
            _HandShakingSelection = System.Enum.GetNames(typeof(Handshake)).ToArray();
        }

        public static readonly int[] DataBitsSelection = new int[] { 7, 8 };

        private static string[] _StopBitsSelection;
        public static string[] StopBitsSelection { get { if (_StopBitsSelection == null) { StopBitToArray(); } return _StopBitsSelection; } }
        private static void StopBitToArray()
        {
            var items = System.Enum.GetNames(typeof(StopBits)).ToArray();
            List<string> vs = new List<string>();
            foreach (string item in items)
            { if (item != "None") { vs.Add(item); } }
            _StopBitsSelection = vs.ToArray();
        }

        private static string[] _ParitySelection;
        public static string[] ParitySelection { get { if (_ParitySelection == null) { ParityToArray(); } return _ParitySelection; } }
        private static void ParityToArray() { _ParitySelection = System.Enum.GetNames(typeof(Parity)).ToArray(); }


        /***************************************************************************************
        * Configuration File:    File
        ****************************************************************************************/
        class ConfigFile
        {
            private static string _Path;
            public static string Path
            {
                get
                {
                    if (string.IsNullOrEmpty(_Path))
                    {
                        System.Reflection.Assembly assy = System.Reflection.Assembly.GetEntryAssembly();
                        _Path = assy.Location;
                        if (_Path.EndsWith(".config", StringComparison.InvariantCultureIgnoreCase))
                        {
                            _Path = _Path.Remove(_Path.Length - 7);
                        }
                    }
                    return _Path;
                }
            }
            public static Configuration Config { get; private set; }

            public static void Load() { Config = ConfigurationManager.OpenExeConfiguration(Path); }
            public static void Save()
            {
                try { Config.Save(ConfigurationSaveMode.Modified); } catch { }
            }
            public static void RefreshSection(string section) { ConfigurationManager.RefreshSection(section); }
        }

        /***************************************************************************************
        * Configuration File:    SerialPort
        ****************************************************************************************/
        #region SerialPort
        public class Element_ComPorts : ConfigurationElement
        {
            const string K_Index = "Index";
            const string K_ChannelName = "Ch_Name";
            const string K_SerialPortName = "SerialPortName";
            const string K_FTDIname = "FTDIname";
            const string K_BeM = "BeM";
            
            const string K_COMreadDelay = "COMreadDelay";
            const string K_Active = "Active";
            const string K_ModusFTDI = "ModusFTDI";

            const string K_BufferReadLine = "BufferReadLine";

            const string K_BaudRate = "BaudRate";
            const string K_HandShake = "HandShake";
            const string K_DataBits = "DataBits";
            const string K_StopBits = "StopBits";
            const string K_Parity = "Parity";

            [ConfigurationProperty(K_Index, IsRequired = true)]
            public int Index
            {
                get{return (int)this[K_Index];}
                set { this[K_Index] = value; }
            }
            [ConfigurationProperty(K_ChannelName)]
            public string Ch_Name
            {
                get { return (string)this[K_ChannelName]; }
                set { this[K_ChannelName] = value; }
            }
            [ConfigurationProperty(K_SerialPortName)]
            public string SerialPortName
            {
                get { return (string)this[K_SerialPortName]; }
                set { this[K_SerialPortName] = value; }
            }
            [ConfigurationProperty(K_FTDIname)]
            public string FTDIname
            {
                get { return (string)this[K_FTDIname]; }
                set { this[K_FTDIname] = value; }
            }

            [ConfigurationProperty(K_BufferReadLine)]
            public bool BufferReadLine
            {
                get { return (bool)this[K_BufferReadLine]; }
                set { this[K_BufferReadLine] = value; }
            }

            [ConfigurationProperty(K_BeM)]
            public string BeM
            {
                get { return (string)this[K_BeM]; }
                set { this[K_BeM] = value; }
            }
            
            [ConfigurationProperty(K_COMreadDelay)]
            public int COMreadDelay
            {
                get { return (int)this[K_COMreadDelay]; }
                set { this[K_COMreadDelay] = value; }
            }
            [ConfigurationProperty("Active")]
            public bool Active
            {
                get { return (bool)this[K_Active]; }
                set { this[K_Active] = value; }
            }
            [ConfigurationProperty("ModusFTDI")]
            public bool ModusFTDI
            {
                get { return (bool)base[K_ModusFTDI]; }
                set { this[K_ModusFTDI] = value; }
            }

            [ConfigurationProperty(K_BaudRate)]
            public int BaudRate
            {
                get { return (int)this[K_BaudRate]; }
                set { this[K_BaudRate] = value; }
            }
            [ConfigurationProperty(K_HandShake)]
            public System.IO.Ports.Handshake HandShake
            {
                get
                {
                    try
                    {
                        return (System.IO.Ports.Handshake)this[K_HandShake];
                    }
                    catch
                    {
                        return System.IO.Ports.Handshake.None;
                    }
                }
                set { this[K_HandShake] = value; }
            }
            [ConfigurationProperty(K_DataBits)]
            public int DataBits
            {
                get { return (int)this[K_DataBits]; }
                set { this[K_DataBits] = value; }
            }
            [ConfigurationProperty(K_StopBits)]
            public System.IO.Ports.StopBits StopBits
            {
                get
                {
                    try { return (System.IO.Ports.StopBits)this[K_StopBits]; }
                    catch { return System.IO.Ports.StopBits.One; }
                }
                set { this[K_StopBits] = value; }
            }
            [ConfigurationProperty(K_Parity)]
            public System.IO.Ports.Parity Parity
            {
                get
                {
                    try { return (System.IO.Ports.Parity)this[K_Parity]; }
                    catch { return System.IO.Ports.Parity.None; }
                }
                set { this[K_Parity] = value; }
            }
        }

        public class Collection_ComPorts : ConfigurationElementCollection
        {
            public Element_ComPorts this[int index]
            {
                get
                {
                    return base.BaseGet(index) as Element_ComPorts;
                }
                set
                {
                    try
                    {
                        if (base.BaseGet(index) != null)
                        { base.BaseRemoveAt(index); }
                    }
                    catch { }
                    this.BaseAdd(index, value);
                }
            }

            public new Element_ComPorts this[string responseString]
            {
                get { return (Element_ComPorts)BaseGet(responseString); }
                set
                {
                    if (BaseGet(responseString) != null)
                    { BaseRemoveAt(BaseIndexOf(BaseGet(responseString))); }
                    BaseAdd(value);
                    Register_ComPorts.Save();
                }
            }

            public void Add(int index, Element_ComPorts sp)
            {
                LockItem = false;
                sp.Index = index;
                sp.Ch_Name = $"CH{(index + 1).ToString().PadLeft(2, '0')}";
                BaseAdd(sp);
            }
            
            protected override ConfigurationElement CreateNewElement()
            {
                return new Element_ComPorts();
            }
            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((Element_ComPorts)element).Index;
            }
        }

        public class Register_ComPorts : ConfigurationSection
        {
            static Register_ComPorts instance;
            public const string SectionNameCollection = "Collection_ComPorts";
            public const string SectionNameElements = "Element_ComPorts";
            public const string SectionName = "Register_ComPorts";

            public int Count { get { return ComPorts.Count; } }

            public static Register_ComPorts GetConfig()
            {
                if (ConfigFile.Config == null) { Load(); }
                Register_ComPorts o = (Register_ComPorts)ConfigurationManager.GetSection(SectionName);
                if(o == null)
                { o = ADD_Section(); }
                return o;
            }

            [ConfigurationProperty(SectionNameCollection)]
            [ConfigurationCollection(typeof(Collection_ComPorts), AddItemName = SectionNameElements)]
            public Collection_ComPorts ComPorts
            {
                get
                {
                    object o = this[SectionNameCollection];
                    return o as Collection_ComPorts;
                }
                set { }
            }
            
            private static Register_ComPorts ADD_Section()
            {
                instance = new Register_ComPorts();
                if (ConfigFile.Config.Sections[SectionNameCollection] == null)
                {
                    ConfigFile.Config.Sections.Add(SectionNameCollection, instance);
                    Save();
                }
                else
                { instance = (Register_ComPorts)ConfigFile.Config.Sections[SectionNameCollection]; }
                return instance;
            }

            public void New() { ComPorts.Add(ComPorts.Count, EmptyElement); }
            public void New(Element_ComPorts element) { ComPorts.Add(ComPorts.Count, element); }
            public Element_ComPorts EmptyElement
            {
                get
                {
                    return new Element_ComPorts()
                    {
                        Index = 0,
                        Ch_Name = "CH00",
                        SerialPortName = "COM1",
                        FTDIname = "",
                        BeM = "No",
                        Active = false,
                        ModusFTDI = false,
                        BufferReadLine = false,
                        COMreadDelay = 25,

                        BaudRate = 9600,
                        HandShake = System.IO.Ports.Handshake.None,
                        DataBits = 8,
                        StopBits = System.IO.Ports.StopBits.One,
                        Parity = System.IO.Ports.Parity.None
                    };
                }
            }

            public static void Load() { ConfigFile.Load(); }
            public static void Save() { ConfigFile.Save(); Refresh(); }
            public static void Refresh() { ConfigFile.RefreshSection(SectionNameCollection); }
        }
        #endregion SerialPort

        /***************************************************************************************
        * Configuration File:    BeM
        ****************************************************************************************/
        #region BeM
        public class Element_BeM : ConfigurationElement
        {
            const string K_Index = "Index";
            const string K_BeM_Name = "BeM_Name";
            const string K_BeM_Desc = "BeM_Desc";
            const string K_BaudRate = "BaudRate";
            const string K_COMreadDelay = "COMreadDelay";
            const string K_BufferReadLine = "BufferReadLine";

            [ConfigurationProperty(K_Index, IsRequired = true)]
            public int Index
            {
                get { return (int)this[K_Index]; }
                set { this[K_Index] = value; }
            }
            [ConfigurationProperty(K_BeM_Name)]
            public string BeM_Name
            {
                get { return (string)this[K_BeM_Name]; }
                set { this[K_BeM_Name] = value; }
            }
            [ConfigurationProperty(K_BeM_Desc)]
            public string BeM_Desc
            {
                get { return (string)this[K_BeM_Desc]; }
                set { this[K_BeM_Desc] = value; }
            }
            [ConfigurationProperty(K_BaudRate)]
            public int BaudRate
            {
                get { return (int)this[K_BaudRate]; }
                set { this[K_BaudRate] = value; }
            }
            [ConfigurationProperty(K_COMreadDelay)]
            public int COMreadDelay
            {
                get { return (int)this[K_COMreadDelay]; }
                set { this[K_COMreadDelay] = value; }
            }
            [ConfigurationProperty(K_BufferReadLine)]
            public bool BufferReadLine
            {
                get { return (bool)this[K_BufferReadLine]; }
                set { this[K_BufferReadLine] = value; }
            }
        }

        public class Collection_BeM : ConfigurationElementCollection
        {
            public Element_BeM this[int index]
            {
                get
                {
                    return base.BaseGet(index) as Element_BeM;
                }
                set
                {
                    try
                    {
                        if (base.BaseGet(index) != null)
                        { base.BaseRemoveAt(index); }
                    }
                    catch { }
                    this.BaseAdd(index, value);
                }
            }

            public new Element_BeM this[string responseString]
            {
                get { return (Element_BeM)BaseGet(responseString); }
                set
                {
                    if (BaseGet(responseString) != null)
                    { BaseRemoveAt(BaseIndexOf(BaseGet(responseString))); }
                    BaseAdd(value);
                    Register_BeM.Save();
                }
            }

            public void Add(int index, Element_BeM sp)
            {
                LockItem = false;
                sp.Index = index;
                BaseAdd(sp);
            }
            protected override ConfigurationElement CreateNewElement()
            {
                return new Element_BeM();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((Element_BeM)element).Index;
            }
        }

        public class Register_BeM : ConfigurationSection
        {
            static Register_BeM instance;
            public const string SectionNameCollection = "Collection_BeM";
            public const string SectionNameElements = "Element_BeM";
            public const string SectionName = "Register_BeM";

            public int Count { get { return BeM.Count; } }

            public static Register_BeM GetConfig()
            {
                if (ConfigFile.Config == null) { Load(); }
                Register_BeM o = (Register_BeM)ConfigurationManager.GetSection(SectionName);
                if (o == null)
                { o = ADD_Section(); }
                return o;
            }

            [ConfigurationProperty(SectionNameCollection)]
            [ConfigurationCollection(typeof(Collection_BeM), AddItemName = SectionNameElements)]
            public Collection_BeM BeM
            {
                get
                {
                    object o = this[SectionNameCollection];
                    return o as Collection_BeM;
                }
                set { }
            }

            private static Register_BeM ADD_Section()
            {
                instance = new Register_BeM();
                if (ConfigFile.Config.Sections[SectionNameCollection] == null)
                {
                    ConfigFile.Config.Sections.Add(SectionNameCollection, instance);
                    Save();
                }
                else
                { instance = (Register_BeM)ConfigFile.Config.Sections[SectionNameCollection]; }
                return instance;
            }

            public static void Load() { ConfigFile.Load(); }
            public static void Save() { ConfigFile.Save(); Refresh(); }
            public static void Refresh() { ConfigFile.RefreshSection(SectionNameCollection); }

            /***************************************************************************************
            * Init:  BeM
            ****************************************************************************************/
            private Element_BeM _BeM_30326849;
            public Element_BeM BeM_30326849
            {
                get
                {
                    if (_BeM_30326849 == null)
                    {
                        _BeM_30326849 = new Element_BeM()
                        {
                            Index = 0,
                            BeM_Name = "30326849",
                            BeM_Desc = "O2 Allgemein",
                            BaudRate = 9600,
                            COMreadDelay = 25,
                            BufferReadLine = true
                        };
                    }
                    return _BeM_30326849;
                }
            }

            private Element_BeM _BeM_30259861;
            public Element_BeM BeM_30259861
            {
                get
                {
                    if (_BeM_30259861 == null)
                    {
                        _BeM_30259861 = new Element_BeM()
                        {
                            Index = 1,
                            BeM_Name = "30259861",
                            BeM_Desc = "Ozon NG",
                            BaudRate = 19200,
                            COMreadDelay = 25,
                            BufferReadLine = true
                        };
                    }
                    return _BeM_30259861;
                }
            }

            private Element_BeM _BeM_11556;
            public Element_BeM BeM_11556
            {
                get
                {
                    if (_BeM_11556 == null)
                    {
                        _BeM_11556 = new Element_BeM()
                        {
                            Index = 2,
                            BeM_Name = "11556",
                            BeM_Desc = "O2",
                            BaudRate = 9600,
                            COMreadDelay = 25,
                            BufferReadLine = true
                        };
                    }
                    return _BeM_11556;
                }
            }

            private Element_BeM _BeM_30014462;
            public Element_BeM BeM_30014462
            {
                get
                {
                    if (_BeM_30014462 == null)
                    {
                        _BeM_30014462 = new Element_BeM()
                        {
                            Index = 3,
                            BeM_Name = "30014462",
                            BeM_Desc = "O2",
                            BaudRate = 9600,
                            COMreadDelay = 25,
                            BufferReadLine = true
                        };
                    }
                    return _BeM_30014462;
                }
            }

            private List<Element_BeM> _BeMArray_Default;
            public List<Element_BeM> BeMArray_Default
            {
                get
                {
                    if (_BeMArray_Default == null)
                    {
                        _BeMArray_Default = new List<Element_BeM>();
                        _BeMArray_Default.Add(BeM_30326849);
                        _BeMArray_Default.Add(BeM_30259861);
                        _BeMArray_Default.Add(BeM_11556);
                        _BeMArray_Default.Add(BeM_30014462);
                    }
                    return _BeMArray_Default;
                }
            }
            
            public bool Find(string name)
            {
                return Find_inDefault(name, out Element_BeM item);
            }
            public bool Find_inDefault(string name, out Element_BeM item)
            {
                item = BeMArray_Default.Find(x => x.BeM_Name == name);
                return item != null;
            }
            public bool Find_inConfig(string name, out Element_BeM item)
            {
                foreach(Element_BeM el in BeM)
                {
                    if(el.BeM_Name == name)
                    {
                        item = el;
                        return true;
                    }
                }
                item = null;
                return false;
            }

            private List<Element_BeM> _BeMList;
            public List<Element_BeM> BeMList
            {
                get
                {
                    if (_BeMList == null)
                    { Init_BeM_All(); }
                    return _BeMList;
                }
                set { _BeMList = value; }
            }
            public List<Element_BeM> Init_BeM_All()
            {
                BeMList = new List<Element_BeM>();
                foreach (Element_BeM element in BeMArray_Default)
                {
                    if(!Find_inConfig(element.BeM_Name, out Element_BeM outItem))
                    {
                        BeM.Add(element.Index, element);
                    }
                }
                Save();
                //Load();
                foreach(Element_BeM element in BeM)
                {
                    BeMList.Add(element);
                }
                return BeMList;
            }
        }

        #endregion BeM

        /***************************************************************************************
        * Configuration File:    InitParameters
        ****************************************************************************************/
        #region InitParameters
        public class Element_InitParameters : ConfigurationElement
        {
            const string K_Index = "Index";

            const string K_CHquantity = "CHquantity";

            const string K_ODBC_Init = "ODBC_Init";
            const string K_Log_Path = "Log_Path";
            const string K_Log_Active = "Log_Active";

            const string K_MeasLog_Path = "MeasLog_Path";
            const string K_MeasLog_Active = "MeasLog_Active";

            const string K_DB_ProdType_Active = "DB_ProdType_Active";
            const string K_DB_ProdType_Table = "DB_ProdType_Table";
            const string K_ProcNr = "ProcNr";
            const string K_DoCheckPass = "DoCheckPass";

            [ConfigurationProperty(K_Index, IsRequired = true)]
            public int Index
            {
                get { return (int)this[K_Index]; }
                set { this[K_Index] = value; }
            }

            [ConfigurationProperty(K_CHquantity, IsRequired = true)]
            public int CHquantity
            {
                get { return (int)this[K_CHquantity]; }
                set { this[K_CHquantity] = value; }
            }

            [ConfigurationProperty(K_ODBC_Init)]
            public string ODBC_Init
            {
                get { return (string)this[K_ODBC_Init]; }
                set { this[K_ODBC_Init] = value; }
            }

            [ConfigurationProperty(K_Log_Path)]
            public string Log_Path
            {
                get { return (string)this[K_Log_Path]; }
                set { this[K_Log_Path] = value; }
            }

            [ConfigurationProperty(K_Log_Active)]
            public bool Log_Active
            {
                get { return (bool)this[K_Log_Active]; }
                set { this[K_Log_Active] = value; }
            }

            [ConfigurationProperty(K_MeasLog_Path)]
            public string MeasLog_Path
            {
                get { return (string)this[K_MeasLog_Path]; }
                set { this[K_MeasLog_Path] = value; }
            }

            [ConfigurationProperty(K_MeasLog_Active)]
            public bool MeasLog_Active
            {
                get { return (bool)this[K_MeasLog_Active]; }
                set { this[K_MeasLog_Active] = value; }
            }

            [ConfigurationProperty(K_DB_ProdType_Active)]
            public bool DB_ProdType_Active
            {
                get { return (bool)this[K_DB_ProdType_Active]; }
                set { this[K_DB_ProdType_Active] = value; }
            }

            [ConfigurationProperty(K_DB_ProdType_Table)]
            public string DB_ProdType_Table
            {
                get { return (string)this[K_DB_ProdType_Table]; }
                set { this[K_DB_ProdType_Table] = value; }
            }

            [ConfigurationProperty(K_ProcNr)]
            public int ProcNr
            {
                get { return (int)this[K_ProcNr]; }
                set { this[K_ProcNr] = value; }
            }

            [ConfigurationProperty(K_DoCheckPass)]
            public string DoCheckPass
            {
                get { return (string)this[K_DoCheckPass]; }
                set { this[K_DoCheckPass] = value; }
            }
        }

        public class Collection_InitParameters : ConfigurationElementCollection
        {
            public Element_InitParameters this[int index]
            {
                get
                { return base.BaseGet(index) as Element_InitParameters; }
                set
                {
                    try
                    {
                        if (base.BaseGet(index) != null)
                        { base.BaseRemoveAt(index); }
                    }
                    catch { }
                    this.BaseAdd(index, value);
                }
            }

            public new Element_InitParameters this[string responseString]
            {
                get { return (Element_InitParameters)BaseGet(responseString); }
                set
                {
                    if (BaseGet(responseString) != null)
                    { BaseRemoveAt(BaseIndexOf(BaseGet(responseString))); }
                    BaseAdd(value);
                    Register_InitParameters.Save();
                }
            }

            public void Add(int index, Element_InitParameters sp)
            {
                LockItem = false;
                sp.Index = index;
                BaseAdd(sp);
            }
            protected override ConfigurationElement CreateNewElement()
            { return new Element_InitParameters(); }

            protected override object GetElementKey(ConfigurationElement element)
            { return ((Element_InitParameters)element).Index; }
        }

        public class Register_InitParameters : ConfigurationSection
        {
            static Register_InitParameters instance;
            public const string SectionNameCollection = "Collection_InitParameters";
            public const string SectionNameElements = "Element_InitParameters";
            public const string SectionName = "Register_InitParameters";

            public int Count { get { return InitParameters.Count; } }

            public static Register_InitParameters GetConfig()
            {
                if (ConfigFile.Config == null) { Load(); }
                Register_InitParameters o = (Register_InitParameters)ConfigurationManager.GetSection(SectionName);
                if (o == null)
                { o = ADD_Section(); }
                return o;
            }

            [ConfigurationProperty(SectionNameCollection)]
            [ConfigurationCollection(typeof(Collection_InitParameters), AddItemName = SectionNameElements)]
            public Collection_InitParameters InitParameters
            {
                get
                {
                    object o = this[SectionNameCollection];
                    return o as Collection_InitParameters;
                }
            }

            private static Register_InitParameters ADD_Section()
            {
                instance = new Register_InitParameters();
                if (ConfigFile.Config.Sections[SectionNameCollection] == null)
                {
                    ConfigFile.Config.Sections.Add(SectionNameCollection, instance);
                    Save();
                }
                else
                { instance = (Register_InitParameters)ConfigFile.Config.Sections[SectionNameCollection]; }
                return instance;
            }

            public static void Load() { ConfigFile.Load(); }
            public static void Save() { try { ConfigFile.Save(); Refresh(); } catch { } }
            public static void Refresh() { ConfigFile.RefreshSection(SectionNameCollection); }


            public void New(bool save = true, bool load = true)
            {
                InitParameters.Add(InitParameters.Count, EmptyElement);
                if (save) { Save(); }
                if (load) { Load(); }
            }
            public void New(Element_InitParameters element)
            {
                InitParameters.Add(InitParameters.Count, element);
            }
            public static Element_InitParameters EmptyElement
            {
                get
                {
                    return new Element_InitParameters()
                    {
                        Index = 0,
                        CHquantity = 5,
                        ODBC_Init = "OneTrackTrace_Test",
                        MeasLog_Active = true,
                        MeasLog_Path = @"C:\TEMP\CaliboxReader",
                        Log_Active = true,
                        Log_Path = @"C:\TEMP\CaliboxReader\CaliboxReader_Error.log",
                        DB_ProdType_Active = false,
                        ProcNr = -1,
                        DoCheckPass = "0"
                    };
                }
            }
        }
        #endregion InitParameters

        /***************************************************************************************
        * Configuration File:    ChangeParameters
        ****************************************************************************************/
        #region InitParameters
        public class Element_ChangeParameters : ConfigurationElement
        {
            const string K_Index = "Index";
            const string K_UserNameID = "UserNameID";
            const string K_UserName = "UserName";
            const string K_ProductionTypeID = "ProductionTypeID";
            const string K_ProductionType = "ProductionType";

            [ConfigurationProperty(K_Index, IsRequired = true)]
            public int Index
            {
                get { return (int)this[K_Index]; }
                set { this[K_Index] = value; }
            }

            [ConfigurationProperty(K_UserNameID)]
            public int UserNameID
            {
                get { return (int)this[K_UserNameID]; }
                set { this[K_UserNameID] = value; }
            }

            [ConfigurationProperty(K_UserName)]
            public string UserName
            {
                get { return (string)this[K_UserName]; }
                set { this[K_UserName] = value; }
            }

            [ConfigurationProperty(K_ProductionTypeID)]
            public int ProductionTypeID
            {
                get { return (int)this[K_ProductionTypeID]; }
                set { this[K_ProductionTypeID] = value; }
            }

            [ConfigurationProperty(K_ProductionType)]
            public string ProductionType
            {
                get { return (string)this[K_ProductionType]; }
                set { this[K_ProductionType] = value; }
            }

        }

        public class Collection_ChangeParameters : ConfigurationElementCollection
        {
            public Element_ChangeParameters this[int index]
            {
                get
                { return base.BaseGet(index) as Element_ChangeParameters; }
                set
                {
                    try
                    {
                        if (base.BaseGet(index) != null)
                        { base.BaseRemoveAt(index); }
                    }
                    catch { }
                    this.BaseAdd(index, value);
                }
            }

            public new Element_ChangeParameters this[string responseString]
            {
                get { return (Element_ChangeParameters)BaseGet(responseString); }
                set
                {
                    if (BaseGet(responseString) != null)
                    { BaseRemoveAt(BaseIndexOf(BaseGet(responseString))); }
                    BaseAdd(value);
                    Register_ChangeParameters.Save();
                }
            }

            public void Add(int index, Element_ChangeParameters sp)
            {
                LockItem = false;
                sp.Index = index;
                BaseAdd(sp);
            }
            protected override ConfigurationElement CreateNewElement()
            { return new Element_ChangeParameters(); }

            protected override object GetElementKey(ConfigurationElement element)
            { return ((Element_ChangeParameters)element).Index; }
        }

        public class Register_ChangeParameters : ConfigurationSection
        {
            static Register_ChangeParameters instance;
            public const string SectionNameCollection = "Collection_ChangeParameters";
            public const string SectionNameElements = "Element_ChangeParameters";
            public const string SectionName = "Register_ChangeParameters";

            public int Count { get { return ChangeParameters.Count; } }
            public static Register_ChangeParameters GetConfig()
            {
                if (ConfigFile.Config == null) { Load(); }
                Register_ChangeParameters o = (Register_ChangeParameters)ConfigurationManager.GetSection(SectionName);
                if (o == null)
                { o = ADD_Section(); }
                return o;
            }

            [ConfigurationProperty(SectionNameCollection)]
            [ConfigurationCollection(typeof(Collection_ChangeParameters), AddItemName = SectionNameElements)]
            public Collection_ChangeParameters ChangeParameters
            {
                get
                {
                    object o = this[SectionNameCollection];
                    return o as Collection_ChangeParameters;
                }
            }

            private static Register_ChangeParameters ADD_Section()
            {
                instance = new Register_ChangeParameters();
                if (ConfigFile.Config.Sections[SectionNameCollection] == null)
                {
                    ConfigFile.Config.Sections.Add(SectionNameCollection, instance);
                    Save();
                }
                else
                { instance = (Register_ChangeParameters)ConfigFile.Config.Sections[SectionNameCollection]; }
                return instance;
            }

            public static void Load() { ConfigFile.Load(); }
            public static void Save() { ConfigFile.Save(); Refresh(); }
            public static void Refresh() { ConfigFile.RefreshSection(SectionNameCollection); }


            public void New(bool save = true, bool load = true)
            {
                ChangeParameters.Add(ChangeParameters.Count, EmptyElement);
                if (save) { Save(); }
                if (load) { Load(); }
            }
            public void New(Element_ChangeParameters element, bool save = true, bool load = true)
            {
                ChangeParameters.Add(ChangeParameters.Count, element);
                if (save) { Save(); }
                if (load) { Load(); }
            }
            public Element_ChangeParameters EmptyElement
            {
                get
                {
                    return new Element_ChangeParameters()
                    {
                        Index = 0,
                        ProductionTypeID = -1,
                        ProductionType = "Test",
                        UserNameID = -1,
                        UserName = "Pedro"
                    };
                }
            }
        }
        #endregion InitParameters


        /***************************************************************************************
        * ProductionType:   
        ****************************************************************************************/
        public static TT_ProdType.clTTProdTypeValues Config_PTV_live = TT_ProdType.clTTProdType.ProdTypeDefault_Prod;
        public static TT_ProdType.clTTProdTypeValues Config_PTV_test = TT_ProdType.clTTProdType.ProdTypeDefault_Test;

        private static List<TT_ProdType.clTTProdTypeValues> _ProdTypes_Config;
        private static string odbc_prodTypes_config = "";
        public static string ODBC_ProdTypes_GUI = "";
        private static bool Compare_ODBC
        {
            get
            {
                bool result = false;
                if (!string.IsNullOrEmpty(ODBC_ProdTypes_GUI))
                {
                    result = odbc_prodTypes_config != ODBC_ProdTypes_GUI;
                    odbc_prodTypes_config = ODBC_ProdTypes_GUI;
                }
                else
                {
                    result = odbc_prodTypes_config != Config_Initvalues.ODBC_Init;
                    odbc_prodTypes_config = Config_Initvalues.ODBC_Init;
                }
                TT_ProdType.clTTProdType.ODBC_Initial = odbc_prodTypes_config;
                return result;
            }
        }
        public static List<TT_ProdType.clTTProdTypeValues> ProdTypes_Config
        {
            get
            {
                if (_ProdTypes_Config == null || Compare_ODBC)
                {
                    _ProdTypes_Config = new List<TT_ProdType.clTTProdTypeValues>();
                    Config_PTV_live.active = true;
                    Config_PTV_live.ProductionType_ID = 1;
                    Config_PTV_live.do_check_TT = Config_Initvalues.DoCheckPass;
                    Config_PTV_test.do_check_TT = Config_Initvalues.DoCheckPass;
                    Config_PTV_live.ODBC_EK = odbc_prodTypes_config;
                    Config_PTV_test.ODBC_EK = odbc_prodTypes_config;
                    Config_PTV_test.ProductionType_ID = 2;
                    _ProdTypes_Config.Add(Config_PTV_live);
                    _ProdTypes_Config.Add(Config_PTV_test);
                }
                return _ProdTypes_Config;
            }
        }

    }
}