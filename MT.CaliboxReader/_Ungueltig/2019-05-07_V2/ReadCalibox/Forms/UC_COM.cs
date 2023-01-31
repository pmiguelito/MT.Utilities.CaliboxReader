using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using static ReadCalibox.clGlobals;
using static ReadCalibox.clConfig;
using static ReadCalibox.clGlobals;
using static STDhelper.clMTcolors;
using static STDhelper.clFTDI;


/* INFORMATIONS ************************************************************************
* Created by:       P. Miguelito / 19.12.2018
* 
* Function:         Using SerialPorts, creating this dynamic from others forms
* 
* Initialization:   Creating a new element, all properties can be set by creating,
*                   set "Initialize = true" at the end, to initialize all settings 
*                   
* Requirements:     MT_Colors, FTDI (STDhelper)
*                   Main Configuration Loader
****************************************************************************************/

namespace ReadCalibox
{
    public partial class UC_COM : UserControl
    {
        public UC_Channel UCChannel;
        public Thread Thread_Sreader;

        /***************************************************************************************
        * Constructor
        ****************************************************************************************/
        Label Lbl_CHname, Lbl_BaudRate, Lbl_DataBits, Lbl_Parity, Lbl_StopBits, Lbl_HandShake, Lbl_BeM;
        ComboBox CoB_COM, CoB_BaudRate, CoB_BeM, CoB_DataBits, CoB_Parity, CoB_StopBits, CoB_HandShaking;
        CheckBox CkB_active, CkB_FTDI, CkB_ReadLine;
        TextBox TB_FTDIname, TB_ReadDelay;
        Panel Panel_BackGround;

        private void ObjRef()
        {
            CkB_active = _CkB_Active;
            Lbl_CHname = _Lbl_CHname;
            Lbl_BaudRate = _Lbl_BaudRate;
            Lbl_DataBits = _Lbl_DataBits;
            Lbl_Parity = _Lbl_Parity;
            Lbl_StopBits = _Lbl_StopBits;
            Lbl_HandShake = _Lbl_HandShake;
            Lbl_BeM = _Lbl_BeM;
            CoB_COM = _CoB_COM;
            CoB_COM.Enabled = false;
            CoB_BaudRate = _CoB_BaudRate;
            CoB_BaudRate.Enabled = false;
            CoB_BeM = _Cob_BeM;
            CoB_BeM.Enabled = false;
            CoB_DataBits = _CoB_DataBits;
            CoB_DataBits.Enabled = false;
            CoB_Parity = _CoB_Parity;
            CoB_Parity.Enabled = false;
            CoB_StopBits = _CoB_StopBits;
            CoB_StopBits.Enabled = false;
            CoB_HandShaking = _CoB_HandShake;
            CoB_HandShaking.Enabled = false;
            CkB_FTDI = _CkB_FTDI;
            CkB_FTDI.Enabled = false;
            CkB_ReadLine = _CkB_ReadLine;
            CkB_ReadLine.Enabled = false;
            TB_FTDIname = _TB_FTDIname;
            
            TB_FTDIname.Enabled = false;
            TB_ReadDelay = _Tb_ReadDelay;
            TB_ReadDelay.Enabled = false;
            Panel_BackGround = _Panel_BackGround;
        }
        public UC_COM()
        {
            InitializeComponent();
            ObjRef();
        }

        private void UC_COM_Load(object sender, EventArgs e)
        {
            if (!Initialized)
            { Init(); }
            Load_GUI();
        }

        /***************************************************************************************
        * Initialization
        ****************************************************************************************/
        public bool Initialized { get; private set; }
        public bool Initialize
        {
            set
            {
                if (value)
                { Init(); }
                Initialized = value;
            }
        }

        public void Init()
        {
            ModusFTDIchanger(ModusFTDI);
            CoB_BaudRate_Init(Baudrate);
            CoB_BeM_Init(BeM);
            CoB_COM_Init(PortName);
            CoB_HandShaking_Init(HandShake_Selected);
            CoB_DataBits_Init(DataBits_Selected);
            CoB_StopBits_Init(StopBits_Selected);
            CoB_Parity_Init(Parity_Selected);
            SerialPort_Init();
            COM_Enabled(false, Active);
            Initialized = true;
        }

        void Init_Colors()
        {
            Alloc_Fonts(Lbl_CHname, 8.5f, FontStyle.Bold);
            Panel_BackGround.BackColor = SystemColors.Control; // MT_Color(selection.Background_EMPTY);
        }

        /***************************************************************************************
        * Master
        ****************************************************************************************/
        public bool Running { get; private set; }
        public bool MeasReady { get { return (Active & SerialPort_Found); } }
        public int Ch_no { get; set; }
        public string Ch_name { get; set; }

        void Load_GUI()
        {
            TB_FTDIname.Width = 67;
            Init_Colors();
            Load_CHno();
        }

        void Load_CHno()
        {
            Lbl_CHname.Text = Ch_name;
        }

        /***************************************************************************************
        * Active
        ****************************************************************************************/
        private bool _Active;
        public bool Active
        {
            get { return _Active; }
            set
            {
                CkB_active.Checked = value;
                COM_Enabled(running: false, enabled: value);
                _Active = value;
            }
        }

        public bool COM_Enabled(bool running, bool enabled)
        {
            this.Enabled = !running;
            if (!running)
            {
                try
                {
                    ModusFTDIchanger(ModusFTDI, enabled);
                    BeM_Modus(BeM, enabled);
                    CoB_BeM.Enabled = enabled;
                    CoB_BaudRate.Enabled = enabled;
                    CkB_active.Enabled = true;
                }
                catch { }
            }
            return enabled;
        }

        public void Ckb_Active_Click(object sender, EventArgs e)
        {
            _Active = CkB_active.Checked;
            COM_Enabled(running: false, enabled: CkB_active.Checked);
            SerialPort_Init();
        }

        public string ErrorMessageChannel { set { UCChannel.MessageChannel = value; } }

        /***************************************************************************************
        * BeM
        ****************************************************************************************/
        private Size PanelSize_BeM = new Size(170,80);
        private Size PanelSize_BeM_No = new Size(170, 155);

        public string BeM { get { return CoB_BeM.Text; } set { CoB_BeM.Text = value; } }

        private void CoB_BeM_Init(string selection)
        {
            if (!string.IsNullOrEmpty(selection))
            {
                CoB_BeM.SelectedIndexChanged -= new EventHandler(Cob_BeM_SelectedIndexChanged);
                CoB_BeM.Items.Clear();
                int index = 0;
                foreach (UC_BeM be in Config_BeMsList)
                {
                    index = be.Index;
                    CoB_BeM.Items.Add(new { Name = Convert.ToString(be.BeMname), no = index });
                }
                CoB_BeM.Items.Add(new { Name = "No", no = (index + 1) });
                CoB_BeM.ValueMember = "no";
                CoB_BeM.DisplayMember = "Name";//"BeMvalues.Desc";
                //CoB_BeM.ValueMember = "Index";
                if (string.IsNullOrEmpty(selection)) { selection = "No"; }
                CoB_BeM.Text = selection;
                CoB_BeM.SelectedIndexChanged += new EventHandler(Cob_BeM_SelectedIndexChanged);
            }
        }

        public bool Find(string name, out UC_BeM item)
        {
            item = Config_BeMsList.Find(x => x.BeMname == name);
            return item != null;
        }
        public void BeM_Modus(string bem, bool enabled = true)
        {
            if (bem != "")
            {
                bool state = true;
                if (bem != "No")
                {
                    if (Find(bem, out UC_BeM bemvalues))
                    {
                        Baudrate = bemvalues.Baudrate;
                        ReadDelay = bemvalues.BeMdelay.ToString();
                    }
                }
                else
                {
                    Baudrate = Baudrate_No;
                    ReadDelay = ReadDelay_No;
                    state = false;
                }
                BeM_Modus_visible(state);
            }
        }

        private void BeM_Modus(int index)
        {
            bool bem = true;
            if (index < Config_BeMsListCount)
            {
                var item = Config_BeMsList[index];
                Baudrate = item.Baudrate;
                ReadDelay = item.BeMdelay.ToString();
                ReadLine = item.BeMreadLine;
            }
            else
            {
                Baudrate = Baudrate_No;
                ReadDelay = ReadDelay_No;
                ReadLine = ReadLine_No;
                bem = false;
            }
            BeM_Modus_visible(bem);
        }
        private void BeM_Modus_visible(bool bem)
        {
            bool enabled = Active ? true : false;
            if (bem)
            { enabled = false; }
            TB_ReadDelay.Enabled = bem ? false : enabled;
            CoB_BaudRate.Enabled = bem ? false : enabled;
            CkB_ReadLine.Enabled = bem ? false : enabled;

            CoB_DataBits.Enabled = enabled;
            CoB_Parity.Enabled = enabled;
            CoB_StopBits.Enabled = enabled;
            CoB_HandShaking.Enabled = enabled;
        }
        private void Cob_BeM_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeM = CoB_BeM.Text;
            BeM_Modus(CoB_BeM.SelectedIndex);
        }

        /***************************************************************************************
        * SerialPort:   Changes events
        ****************************************************************************************/
        public event System.EventHandler SerialPort_Found_Changed;
        protected virtual void OnSerialPort_Found_Changed()
        {
            if (SerialPort_Found_Changed != null)
            {
                try { SerialPort_Found_Changed(this, EventArgs.Empty); } catch { }
            }
        }
        private bool _SerialPort_Found;
        public bool SerialPort_Found
        {
            get { return _SerialPort_Found; }
            private set
            {
                _SerialPort_Found = value;
                OnSerialPort_Found_Changed();
            }
        }

        /***************************************************************************************
        * SerialPort:   Executions
        ****************************************************************************************/
        public bool Start(bool discardbuffer)
        {
            try
            {
                if (Active)
                {
                    if (Serialport == null) { SerialPort_Init(); }
                    if (!Serialport.IsOpen)
                    {
                        try
                        {
                            Serialport.Open();
                            if (discardbuffer)
                            {
                                Serialport.DiscardInBuffer();
                                Serialport.DiscardOutBuffer();
                            }
                        }
                        catch { return false; }
                    }
                    Running = !COM_Enabled(running: true, enabled: !Serialport.IsOpen);
                    return Running;
                }
            }
            catch { }
            return false;
        }
        public bool Stop()
        {
            if (Serialport.IsOpen)
            {
                Serialport.Close();
                Serialport.Dispose();
            }
            return !COM_Enabled(running: false, enabled: !Serialport.IsOpen);
        }

        /***************************************************************************************
        * SerialPort:   Comport
        ****************************************************************************************/
        public SerialPort Serialport { get; set; }
        
        public string PortName { get { return CoB_COM.Text; } set { CoB_COM.Text = value; } }

        public SerialPort SerialPort_Init_Modus(string portName, out string portNameOut)
        {
            portNameOut = portName;
            if (ModusFTDI)
            {
                if (Get_COM_FromFTDI(portName, out portNameOut))
                {
                    PortName = portNameOut;
                    return clConfig.SerialPort_Init(portNameOut, Baudrate, DataBits_Selected, Parity_Selected, StopBits_Selected);
                }
            }
            else if (!string.IsNullOrEmpty(portName))
            {
                return clConfig.SerialPort_Init(portName, Baudrate);
            }
            return null;
        }
        public SerialPort SerialPort_Init_Modus(string portName)
        {
            return SerialPort_Init_Modus(portName, out string portNameOut);
        }
        

        public void SerialPort_Init()
        {
            if (Active)
            {
                string select = ModusFTDI ? FTDIname : PortName;
                Serialport = SerialPort_Init_Modus(select);
            }
            SerialPort_Check(Serialport);
        }
        public bool SerialPort_Check(SerialPort serialPort)
        {
            if (serialPort != null && Active)
            {
                foreach (string spName in SerialPortList)
                {
                    if (spName == serialPort.PortName)
                    {
                        SerialPort_Found = true;
                        return true;
                    }
                }
            }
            SerialPort_Found = false;
            return false;
        }
        public bool SerialPort_Check()
        {
            return SerialPort_Check(Serialport);
        }

        void CoB_COM_Init(string comName)
        {
            string _COMname = comName;
            if (SerialPortList != null)
            {
                CoB_COM.SelectedIndexChanged -= new EventHandler(CoB_COM_SelectedIndexChanged);
                CoB_COM.BindingContext = new BindingContext();
                CoB_COM.DataSource = SerialPortList;
                PortName = _COMname;
                CoB_COM.SelectedIndexChanged += new EventHandler(CoB_COM_SelectedIndexChanged);
            }
        }

        void CoB_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPort_Init();
        }

        void CoB_COM_Click(object sender, EventArgs e)
        {
            if (!ModusFTDI)
            { CoB_COM_Init(CoB_COM.Text); }
        }

        /***************************************************************************************
        * SerialPort:   Buffer Read Line
        ****************************************************************************************/
        private bool _ReadLine_No;
        private bool ReadLine_No { get { return _ReadLine_No; } set { if(BeM == "No") { _ReadLine_No = value; } } }
        public bool ReadLine { get { return CkB_ReadLine.Checked; } set { CkB_ReadLine.Checked = value; ReadLine_No = value; } }

        /***************************************************************************************
        * SerialPort:   Read Delay
        ****************************************************************************************/
        private string _ReadDelay_No;
        private string ReadDelay_No
        {
            get
            {
                if (string.IsNullOrEmpty(_ReadDelay_No))
                { _ReadDelay_No = ReadDelay; }
                return _ReadDelay_No;
            }
            set { _ReadDelay_No = value; }
        }
        private string _ReadDelay;
        public string ReadDelay
        {
            get { return TB_ReadDelay.Text; }
            set { TB_ReadDelay.Text = value; _ReadDelay = value; }
        }
        public int ReadDelay_int
        {
            get { return Convert.ToInt32(ReadDelay); }
        }
        private void TB_ReadDelay_Leave(object sender, EventArgs e)
        {
            ReadDelay = TB_ReadDelay.Text;
            ReadDelay_No = ReadDelay;
        }

        /***************************************************************************************
        * SerialPort:   BaudRate
        ****************************************************************************************/
        private int _Baudrate_No = -1;
        private int Baudrate_No
        {
            get
            {
                if (_Baudrate_No == -1)
                { _Baudrate_No = Baudrate; }
                return _Baudrate_No;
            }
            set { _Baudrate_No = value; }
        }
        private int _Baudrate;
        public int Baudrate
        {
            get { try { return _Baudrate; } catch { return Baudrate_No; } }
            set
            {
                string br = value.ToString();
                if (CoB_BaudRate.Text != br)
                { CoB_BaudRate.Text = br; }
                _Baudrate = value;
            }
        }

        private void CoB_BaudRate_Init(int baudrate)
        {
            CoB_BaudRate.SelectedIndexChanged -= _CoB_BaudRate_SelectedIndexChanged;
            CoB_BaudRate.BindingContext = new BindingContext();
            CoB_BaudRate.DataSource = BaudRateList;
            if (baudrate > 0)
            { Baudrate = baudrate; }
            CoB_BaudRate.SelectedIndexChanged += _CoB_BaudRate_SelectedIndexChanged;
        }
        private void _CoB_BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Baudrate = Convert.ToInt32(CoB_BaudRate.Text); ;
            if (Serialport == null)
            { Serialport = clConfig.SerialPort_Init(PortName, Baudrate); }
            else
            { Serialport.BaudRate = Baudrate; }
            if (CoB_BeM.Text == "No")
            { Baudrate_No = Baudrate; }
        }

        /***************************************************************************************
        * SerialPort:   HandShaking
        ****************************************************************************************/
        public Handshake HandShake_Selected;

        void CoB_HandShaking_Init(Handshake value)
        {
            CoB_HandShaking.SelectedIndexChanged -= new EventHandler(CoB_HandShaking_SelectedIndexChanged);
            CoB_HandShaking.BindingContext = new BindingContext();
            CoB_HandShaking.DataSource = HandShakingSelection;
            HandShake_Selected = value;
            CoB_HandShaking.Text = value.ToString();
            CoB_HandShaking.SelectedIndexChanged += new EventHandler(CoB_HandShaking_SelectedIndexChanged);
        }
        void CoB_HandShaking_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HandShake_selected = CoB_HandShaking.Text;
            Enum.TryParse(HandShake_selected, out HandShake_Selected);
            if (Serialport != null)
            { Serialport.Handshake = HandShake_Selected; }
        }

        /***************************************************************************************
        * SerialPort:   DataBits
        ****************************************************************************************/
        public int DataBits_Selected;

        void CoB_DataBits_Init(int value)
        {
            CoB_DataBits.SelectedIndexChanged -= new EventHandler(CoB_DataBits_SelectedIndexChanged);
            CoB_DataBits.BindingContext = new BindingContext();
            CoB_DataBits.DataSource = DataBitsSelection;
            DataBits_Selected = value;
            CoB_DataBits.Text = value.ToString();
            CoB_DataBits.SelectedIndexChanged += new System.EventHandler(CoB_DataBits_SelectedIndexChanged);
        }
        void CoB_DataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBits_Selected = Convert.ToInt32(CoB_DataBits.Text);
            if (Serialport != null)
            { Serialport.DataBits = DataBits_Selected; }
        }

        /***************************************************************************************
        * SerialPort:   StopBits
        ****************************************************************************************/
        public StopBits StopBits_Selected;

        void CoB_StopBits_Init(StopBits value)
        {
            CoB_StopBits.SelectedIndexChanged -= new EventHandler(CoB_StopBits_SelectedIndexChanged);
            CoB_StopBits.BindingContext = new BindingContext();
            CoB_StopBits.DataSource = StopBitsSelection;
            StopBits_Selected = value;
            CoB_StopBits.Text = value.ToString();
            CoB_StopBits.SelectedIndexChanged += new System.EventHandler(CoB_StopBits_SelectedIndexChanged);
        }
        void CoB_StopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StopBits_selected = CoB_StopBits.Text;
            Enum.TryParse(StopBits_selected, out StopBits_Selected);
            if (Serialport != null)
            { Serialport.StopBits = StopBits_Selected; }
        }

        /***************************************************************************************
        * SerialPort:   Parity
        ****************************************************************************************/
        public Parity Parity_Selected;

        private void CoB_Parity_Init(Parity value)
        {
            CoB_Parity.SelectedIndexChanged -= new EventHandler(CoB_Parity_SelectedIndexChanged);
            CoB_Parity.BindingContext = new BindingContext();
            CoB_Parity.DataSource = ParitySelection;
            Parity_Selected = value;
            //Enum.TryParse(value, out Parity_Selected);
            CoB_Parity.Text = value.ToString();
            CoB_Parity.SelectedIndexChanged += new System.EventHandler(CoB_Parity_SelectedIndexChanged);
        }
        private void CoB_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Parity_selected = CoB_Parity.Text;
            Enum.TryParse(Parity_selected, out Parity_Selected);
            if (Serialport != null)
            { Serialport.Parity = Parity_Selected; }
        }

        /***************************************************************************************
        * FTDI: Names
        ****************************************************************************************/
        private STDhelper.clFTDI _FTDI = new STDhelper.clFTDI();
        private string _FTDIname;
        public string FTDIname
        {
            get { return _FTDIname; }
            set { TB_FTDIname.Text = value; _FTDIname = value; }
        }
        private bool _ModusFTDI = false;
        public bool ModusFTDI
        {
            get { return _ModusFTDI; }
            set { _ModusFTDI = value; ModusFTDIchanger(value); }
        }

        /***************************************************************************************
        * FTDI: Changes
        ****************************************************************************************/
        public void ModusFTDIchanger(bool modusFTDI, bool enabled = true)
        {
            CkB_FTDI.Checked = modusFTDI;
            TB_FTDIname.Visible = modusFTDI;
            CoB_COM.Visible = !modusFTDI;

            TB_FTDIname.Enabled = Active? enabled : false;
            CoB_COM.Enabled = Active ? enabled : false;
            CkB_FTDI.Enabled = Active ? enabled : false;
        }

        private void TB_FTDIname_Leave(object sender, EventArgs e)
        {
            _FTDIname = TB_FTDIname.Text;
            SerialPort_Init();
        }
        private void CkB_FTDI_Click(object sender, EventArgs e)
        {
            ModusFTDI = CkB_FTDI.Checked;
        }

        private bool Get_COM_FromFTDI(string serialNumber, out string portName)
        {
            portName = _FTDI.Get_SerialPort(DTFTDI, serialNumber);
            return !string.IsNullOrEmpty(portName);
        }
    }
}
