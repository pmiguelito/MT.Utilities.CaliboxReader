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

        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:
        ***********************************************************/
        public EventHandler<EventRoutingArgs> CommandSend;
        public void OnCommandSend(object sender, CmdDefinition routine)
        {
            try
            {
                if (routine == null)
                {
                    return;
                }
                IsRunning = true;
                IsDataReceived = false;
                RetriesCount++;
                TimeCmdSend = DateTime.Now;
                CommandSend?.Invoke(sender, new EventRoutingArgs(routine));
            }
            catch
            {

            }
        }

        /**********************************************************
        * FUNCTION:     Current
        * DESCRIPTION:
        ***********************************************************/

        public int Index { get; private set; }

        public CmdDefinition Current { get; private set; }

        public bool IsRetry
        {
            get
            {
                if (Current == null)
                {
                    return false;
                }

                if (RetriesCount < Current?.RetriesMax)
                {
                    return true;
                }
                return false;
            }
        }

        private bool TrySetCurrent(int index)
        {
            Index = index;
            if (index < Routing.Count)
            {
                Current = Routing[index];
                RetriesCount = 0;
                IsDataReceived = false;
            }
            return false;
        }

        /**********************************************************
        * FUNCTION:     Status
        * DESCRIPTION:
        ***********************************************************/

        /// <summary>
        /// Send S999 at the beginning
        /// </summary>
        public bool InitializeCaliBox { get; private set; }

        public bool IsRunning { get; private set; }

        public int RetriesCount { get; set; }

        public DateTime TimeCmdSend { get; set; } = DateTime.MinValue;

        public TimeSpan GetElapsed()
        {
            return DateTime.Now - TimeCmdSend;
        }

        public bool IsTimeElapsed()
        {
            if (Current == null)
            {
                return true;
            }

            if (Current?.WaitMilliseconds < 1)
            {
                return true;
            }
            if (GetElapsed().TotalMilliseconds > Current?.WaitMilliseconds)
            {
                return true;
            }
            return false;
        }

        public bool IsDataReceived { get; set; }

        /**********************************************************
        * FUNCTION:     Start/Stop
        * DESCRIPTION:
        ***********************************************************/

        public void Reset()
        {
            IsDataReceived = false;
            IsRunning = false;
            Index = 0;
            RetriesCount = 0;
            _TimerSender?.Stop();
        }

        public void Start()
        {
            Reset();
            Init_Timer();
            TrySetCurrent(0);
            IsRunning = true;
            _TimerSender?.Start();
        }

        public void Stop()
        {
            Reset();
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
            TrySend();
        }

        private bool _IsSending;
        private void TrySend()
        {
            if (_IsSending) { return; }

            if (Index >= Routing.Count)
            {
                Stop();
                return;
            }

            _IsSending = true;
            try
            {
                var i = Send(Index);
                if (i != Index)
                {
                    TrySetCurrent(i);
                }
            }
            finally
            {
                _IsSending = false;
            }
        }

        private bool CanExecuteNext()
        {
            if (RetriesCount < 1)
            {
                return true;
            }

            if (IsDataReceived)
            {
                if (Current.NextOnAnswer)
                {
                    return true;
                }
            }
            bool elapsed = IsTimeElapsed();
            return elapsed;
        }

        private int Send(int nowRunningIndex)
        {
            if (CanExecuteNext())
            {
                if (IsRetry && IsDataReceived == false)
                {
                    OnCommandSend(this, Current);
                    return nowRunningIndex;
                }
                nowRunningIndex++;
            }
            return nowRunningIndex;
        }
    }
}
