using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaliboxLibrary.BoxCommunication.CMDs
{
    public class CMDSequences
    {
        public DeviceCom Device { get; set; }
        public CMDSequences()
        {
            Create();
            Init_Timer();
        }

        public CMDSequences(DeviceCom device) : this()
        {
            Device = device;
        }
        /************************************************
         * FUNCTION:    Container
         * DESCRIPTION:
         ************************************************/
        public Dictionary<Enum, CmdSequence> DataValues { get; set; }
            = new Dictionary<Enum, CmdSequence>();
        private void Create()
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
        public EventHandler<EventRoutingArgs> CommandSended;

        public void OnCommandSended(object sender, EventRoutingArgs e)
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
                    CommandSended?.Invoke(this, e);
                }
                catch
                {

                }
            });
        }
        //public void OnCommandSended(CmdDefinition routine)
        //{

        //    OnCommandSended(this, new EventRoutingArgs(routine));
        //}

        /************************************************
         * FUNCTION:    Data Selection
         * DESCRIPTION:
         ************************************************/
        //public List<CmdDefinition> Routing { get; set; } = new List<CmdDefinition>();
        //public int Index { get; private set; }
        private CmdSequence _Routing;
        public CmdSequence Routing
        {
            get { return _Routing; }
            private set { _Routing = value; }
        }
        public DateTime TimeStart { get; private set; }

        public bool IsLoggingActivated { get; set; }

        public bool IsBoxInitialized { get; set; }

        public bool IsRunning { get; private set; }

        /************************************************
         * FUNCTION:    Start / Stop
         * DESCRIPTION:
         ************************************************/
        private void Reset()
        {
            //Index = -1;
            //Routing = null;
            Routing?.Reset();
        }

        public bool Start(Enum cmd)
        {
            Reset();
            if (_Routing != null)
            {
                _Routing.CommandSended -= OnCommandSended;
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
            Routing.CommandSended += OnCommandSended;
            IsRunning = true;
            //selection.Reset();
            //Routing = selection.Routing;
            TimeStart = DateTime.Now;
            //SetSelection(0);

            //_TimerSender.Start();
            Routing?.Start();
            return true;
        }

        //private bool SetSelection(int index)
        //{
        //    if (Index != index)
        //    {
        //        if (Routing != null)
        //        {
        //            //SelectedRoutine.IsRunning = false;
        //        }
        //        Index = index;
        //    }
        //    if (Index < Routing.Count)
        //    {
        //        Routing = Routing[Index];
        //        if (SelectedRoutine.OpCode == OpCode.G906)
        //        {
        //            if (IsLoggingActivated)
        //            {
        //                if (Routing.Count > 1)
        //                {
        //                    Index++;
        //                    return SetSelection(Index);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        //private bool MoveNext()
        //{
        //    if (Index < Routing.Count)
        //    {
        //        if (SelectedRoutine.IsRunning == false)
        //        {
        //            SelectedRoutine.Start();
        //            OnCommandSended(SelectedRoutine);
        //            return true;
        //        }
        //        if (SelectedRoutine.IsTimeElapsed)
        //        {
        //            if (SelectedRoutine.IsRetry)
        //            {
        //                SelectedRoutine.Start();
        //                OnCommandSended(SelectedRoutine);
        //                return true;
        //            }

        //            Index++;
        //            if (SetSelection(Index))
        //            {
        //                return MoveNext();
        //            }
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        /**********************************************************
        * FUNCTION:     Timer
        * DESCRIPTION:
        ***********************************************************/
        private Timer _TimerSender = new Timer();
        private int _TimerInterval = 200;
        private void Init_Timer()
        {
            if (_TimerSender != null)
            {
                _TimerSender.Tick -= TimerSender_Tick;
            }
            _TimerSender = new Timer
            {
                Interval = _TimerInterval
            };
            _TimerSender.Tick += TimerSender_Tick;
        }

        private void TimerSender_Tick(object sender, EventArgs e)
        {
            //if (MoveNext() == false)
            //{
            //    IsRunning = false;
            //    _TimerSender.Stop();
            //    return;
            //}
        }
    }
}
