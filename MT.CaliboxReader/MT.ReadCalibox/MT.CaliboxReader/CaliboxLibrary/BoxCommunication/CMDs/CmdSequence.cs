using System;
using System.Collections.Generic;

namespace CaliboxLibrary
{
    public class CmdSequence
    {
        public CmdSequence(Enum cmd)
        {
            CMDs = cmd;
        }

        public CmdSequence(Enum cmd, string description) : this(cmd)
        {
            Description = description;
        }

        public string Description { get; set; }

        public Enum CMDs { get; private set; }
        public List<CmdDefinition> Routing { get; set; } = new List<CmdDefinition>();

        public int Index { get; private set; }

        public CmdDefinition SelectedRoutine { get; private set; }

        /// <summary>
        /// Send S999 at the beginning
        /// </summary>
        public bool InitializeCaliBox { get; private set; }

        private bool _IsRunning;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                _IsRunning = value;
            }
        }
        public int RetriesCount { get; set; }
        public DateTime TimeCmdSend { get; set; } = DateTime.MinValue;

        public TimeSpan GetElapsed()
        {
            return DateTime.Now - TimeCmdSend;
        }

        public bool IsTimeElapsed
        {
            get
            {
                if (SelectedRoutine?.WaitMilliseconds < 1)
                {
                    return true;
                }
                if (GetElapsed().TotalMilliseconds > SelectedRoutine?.WaitMilliseconds)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsRetry
        {
            get
            {
                if (SelectedRoutine?.RetriesMax < 2)
                {
                    return false;
                }
                if (RetriesCount >= SelectedRoutine?.RetriesMax)
                {
                    return false;
                }
                return true;
            }
        }
        public int Send(int nowRunningIndex)
        {
            if (IsRunning == false)
            {
                IsRunning = true;
                RetriesCount++;
                TimeCmdSend = DateTime.Now;
                OnCommandSended(this, SelectedRoutine);
                return nowRunningIndex;
            }
            if (IsTimeElapsed)
            {
                if (IsRetry)
                {
                    RetriesCount++;
                    TimeCmdSend = DateTime.Now;
                    OnCommandSended(this, SelectedRoutine);
                    return nowRunningIndex;
                }
                nowRunningIndex++;
            }
            return nowRunningIndex;
        }

        public void Reset()
        {
            IsRunning = false;
            Index = 0;
            RetriesCount = 0;
            _TimerSender?.Stop();
        }

        public void Start()
        {
            Reset();
            Init_Timer();
            SelectedRoutine = Routing[Index];
            _TimerSender?.Start();
        }

        public void Stop()
        {
            Reset();
        }
        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:
        ***********************************************************/
        public EventHandler<EventRoutingArgs> CommandSended;
        public void OnCommandSended(object sender, CmdDefinition routine)
        {
            try
            {
                if (routine == null)
                {
                    return;
                }
                CommandSended?.Invoke(sender, new EventRoutingArgs(routine));
            }
            catch
            {

            }
        }
        /**********************************************************
        * FUNCTION:     Timer
        * DESCRIPTION:
        ***********************************************************/
        private System.Timers.Timer _TimerSender;
        private int _TimerInterval = 200;
        private void Init_Timer()
        {
            _TimerSender = new System.Timers.Timer
            {
                Interval = _TimerInterval
            };
            _TimerSender.Elapsed -= TimerSender_Elapsed;
            _TimerSender.Elapsed += TimerSender_Elapsed;
        }

        private void TimerSender_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Index >= Routing.Count)
            {
                _TimerSender.Stop();
                return;
            }
            var i = Send(Index);
            if (i != Index)
            {
                Index = i;
                if (Index < Routing.Count)
                {
                    SelectedRoutine = Routing[Index];
                }
                else
                {
                    _TimerSender.Stop();
                }
            }
        }
    }
}
