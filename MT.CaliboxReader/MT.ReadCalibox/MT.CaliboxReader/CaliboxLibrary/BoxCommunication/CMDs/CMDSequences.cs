using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaliboxLibrary.BoxCommunication.CMDs
{
    public class CMDSequences
    {
        private DeviceCom _Device;
        public DeviceCom Device
        {
            get { return _Device; }
            set
            {
                if (_Device != null)
                {
                    _Device.DataReceived -= Device_DataReceived;
                }
                _Device = value;
                if (value != null)
                {
                    _Device.DataReceived += Device_DataReceived;
                }
            }
        }
        public CMDSequences()
        {
            CreateDataValues();
        }

        public CMDSequences(DeviceCom device) : this()
        {
            Device = device;
        }

        private void Device_DataReceived(object sender, EventDataArgs e)
        {
            if (e.CmdSended?.OpCode != Routing.Current?.OpCode)
            {
                return;
            }
            Routing.IsDataReceived = e.IsOpCodeReceivedAnswer;
        }

        /************************************************
        * FUNCTION:    Container
        * DESCRIPTION:
        ************************************************/
        public Dictionary<Enum, CmdSequence> DataValues { get; set; }
            = new Dictionary<Enum, CmdSequence>();

        private void CreateDataValues()
        {
            DataValues.Add(CaliboxLibrary.CMDs.InitBox_S999, CMD.New_InitBox());

            DataValues.Add(CaliboxLibrary.CMDs.Set0nA_G907, CMD.New_Set0nA());
            DataValues.Add(CaliboxLibrary.CMDs.Set175nA_G908, CMD.New_Set175nA());
            DataValues.Add(CaliboxLibrary.CMDs.Set4700nA_G909, CMD.New_Set4700nA());

            DataValues.Add(CaliboxLibrary.CMDs.Calib_S100_6850i, CMD.New_Calib_500And674());
            DataValues.Add(CaliboxLibrary.CMDs.Calib_S500, CMD.New_Calib_500());
            DataValues.Add(CaliboxLibrary.CMDs.Calib_S674, CMD.New_Calib_674());

            DataValues.Add(CaliboxLibrary.CMDs.GetBoxLimits, CMD.New_GetBoxLimits());
            DataValues.Add(CaliboxLibrary.CMDs.ShowBoxLimits, CMD.New_GetBoxLimits());

            DataValues.Add(CaliboxLibrary.CMDs.TempCheckNTC25_G911, CMD.New_TempCheck_NTC25());
            DataValues.Add(CaliboxLibrary.CMDs.TempCheckPT20_G915, CMD.New_TempCheck_PT20());
            DataValues.Add(CaliboxLibrary.CMDs.TempCheckPT30_G916, CMD.New_TempCheck_PT30());

            DataValues.Add(CaliboxLibrary.CMDs.CheckUpol_G910, CMD.New_PolarizationCheck());
            DataValues.Add(CaliboxLibrary.CMDs.SetUpol500_G913, CMD.New_PolarizationSet_500());
            DataValues.Add(CaliboxLibrary.CMDs.SetUpol674_G914, CMD.New_PolarizationSet_674());

            DataValues.Add(CaliboxLibrary.CMDs.ReadPage00, CMD.New_ReadSensorPage00());
            DataValues.Add(CaliboxLibrary.CMDs.ReadPage01, CMD.New_ReadSensorPage01());
            DataValues.Add(CaliboxLibrary.CMDs.ReadPage02, CMD.New_ReadSensorPage02());

            DataValues.Add(CaliboxLibrary.CMDs.ReadConverterInfos, CMD.New_ReadConverterInfos());

            DataValues.Add(CaliboxLibrary.CMDs.ActivateBoxLog, CMD.New_ActivateBoxLog());
        }

        /************************************************
         * FUNCTION:    Events
         * DESCRIPTION:
         ************************************************/
        public EventHandler<EventRoutingArgs> CommandSend;

        public void OnCommandSend(object sender, EventRoutingArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    if (e.Routine.OpCode == OpCode.S999)
                    {
                        IsLoggingActivated = false;
                        IsBoxInitialized = true;
                    }
                    else if (e.Routine.OpCode == OpCode.G901)
                    {
                        IsLoggingActivated = !IsLoggingActivated;
                    }
                    OnCommandSend(e);
                }
                catch
                {

                }
            });
        }

        private void OnCommandSend(EventRoutingArgs e)
        {
            CommandSend?.Invoke(this, e);
        }

        /************************************************
         * FUNCTION:    Data Selection
         * DESCRIPTION:
         ************************************************/
        private CmdSequence _Routing;
        public CmdSequence Routing
        {
            get { return _Routing; }
            private set { _Routing = value; }
        }

        public DateTime TimeStart { get; private set; }

        public bool IsLoggingActivated { get; private set; }

        public bool IsBoxInitialized { get; private set; }

        public bool IsRunning { get; private set; }

        /************************************************
         * FUNCTION:    Start / Stop
         * DESCRIPTION:
         ************************************************/
        private void Reset()
        {
            Routing?.Reset();
        }

        public bool Start(Enum cmd)
        {
            if (_Routing?.IsRunning ?? false == true)
            {
                return false;
            }

            Reset();
            if (_Routing != null)
            {
                _Routing.CommandSend -= OnCommandSend;
            }

            if (DataValues.TryGetValue(cmd, out _Routing) == false)
            {
                if (cmd is OpCode opcode)
                {
                    string desc = opcode.ToString();
                    _Routing = new CmdSequence(opcode, desc);
                    _Routing.Routing.Add(new CmdDefinition(opcode, wait: 1000));
                    DataValues.Add(opcode, _Routing);
                }
                else
                {
                    return false;
                }
            }
            Routing.CommandSend += OnCommandSend;
            IsRunning = true;
            TimeStart = DateTime.Now;
            Routing?.Start();
            return true;
        }
    }
}
