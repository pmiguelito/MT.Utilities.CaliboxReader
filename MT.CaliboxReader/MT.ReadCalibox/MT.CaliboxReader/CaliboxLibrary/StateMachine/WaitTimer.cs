using System.Timers;

namespace CaliboxLibrary
{
    public class WaitTimer
    {
        public WaitTimer(WaitDetails details)
        {
            Initialize();
            WaitDetails = details ?? new WaitDetails();
        }

        public WaitDetails WaitDetails { get; set; }

        #region Timer
        /**********************************************************
        * FUNCTION:     Timer
        * DESCRIPTION:  
        ***********************************************************/

        private Timer _Timer;
        private void Initialize()
        {
            _Timer = new Timer()
            {
                Interval = 1000
            };
            _Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

        }

        public void Start(WaitDetails details)
        {
            WaitDetails = details;
            Start();
        }
        public void Start()
        {
            _Timer.Start();
        }
        public void Stop()
        {
            _Timer.Stop();
        }
        #endregion
    }
}
