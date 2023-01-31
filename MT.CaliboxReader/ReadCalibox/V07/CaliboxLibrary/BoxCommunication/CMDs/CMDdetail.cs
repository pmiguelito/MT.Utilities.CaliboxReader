using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CaliboxLibrary
{
    public class CMDdetail
    {
        public CMDdetail(CMDs cmd)
        {
            CMDs = cmd;
        }
        public CMDs CMDs { get; private set; }
        public List<Routine> Routing { get; set; } = new List<Routine>();

        public int Index { get; private set; }

        public Routine SelectedRoutine { get; private set; }

        /// <summary>
        /// Send S999 at the beginning
        /// </summary>
        public bool InitializeCaliBox { get; private set; }
        public void Start()
        {
            Init_Timer();
            Index = 0;
        }

        #region Timer
        /**********************************************************
        * FUNCTION:     Timer
        * DESCRIPTION:  
        ***********************************************************/
        private Timer _TimerSender = new Timer();
        private int _TimerInterval = 500;
        private void Init_Timer()
        {
            _TimerSender = new Timer
            {
                Interval = _TimerInterval
            };
            _TimerSender.Tick += TimerSender_Tick;
        }
        private void TimerSender_Tick(object sender, EventArgs e)
        {
            if (Index >= Routing.Count)
            {
                _TimerSender.Stop();
                return;
            }
            var i = SelectedRoutine.Send(Index);
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
        #endregion
    }
}
