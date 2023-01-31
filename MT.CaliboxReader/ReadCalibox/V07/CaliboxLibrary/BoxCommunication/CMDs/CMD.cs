using System;
using System.Collections.Generic;

namespace CaliboxLibrary
{

    public enum CMDs
    {
        InitBox_S999,
        Set0nA_G907,
        Set175nA_G908,
        Set4700nA_G909,
        Calib_S100_6850i,
        Calib_S674,
        Calib_S500,
        GetBoxLimits,
        ShowBoxLimits,
        TempCheckNTC25_G911,
        TempCheckPT20_G915,
        TempCheckPT30_G916,
        CheckUpol_G910,
        SetUpol500_G913,
        SetUpol674_G914,

        ReadPage00,
        ReadPage01,
        ReadPage02
    }

    public static class CMD
    {

        #region Events
        /**********************************************************
        * FUNCTION:     Events
        * DESCRIPTION:  
        ***********************************************************/
        public static EventHandler<EventRoutingArgs> SendCMD;
        public static void OnSendCMD(object sender, Routine routine)
        {
            SendCMD?.Invoke(sender, new EventRoutingArgs(routine));
        }

        #endregion
        #region BoxReset
        /**********************************************************
        * FUNCTION:     BoxReset
        * DESCRIPTION:  
        ***********************************************************/
        public static CMDdetail InitBox_S999 { get; } = new CMDdetail(CMDs.InitBox_S999)
        {
            Routing = new List<Routine>()
            {
                new Routine(OpCode.S999, "S999", wait: 3000),
                new Routine(OpCode.G015, "G015", wait: 3000, retry: 2),
            }
        };
        #endregion

        #region ReadContinous
        /**********************************************************
        * FUNCTION:     ReadContinous
        * DESCRIPTION:  
        ***********************************************************/
        public static bool G001sended { get; set; }
        #endregion

        public static CMDdetail Set0nA_G907 { get { return GetSet0nA_G907(); } }

        private static CMDdetail _Set0nA_G907;
        private static CMDdetail GetSet0nA_G907()
        {
            if (_Set0nA_G907 != null)
            {
                return _Set0nA_G907;
            }
            _Set0nA_G907 = new CMDdetail(CMDs.Set0nA_G907);

            _Set0nA_G907.Routing.AddRange(InitBox_S999.Routing);
            _Set0nA_G907.Routing.Add(new Routine(OpCode.G907, "G907", wait: 2000));
            _Set0nA_G907.Routing.Add(new Routine(OpCode.G906, "G906", wait: 1000, retry: 10));

            return _Set0nA_G907;
        }
    }
}
