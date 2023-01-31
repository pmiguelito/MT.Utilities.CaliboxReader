using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using STDhelper;
using TT_Item_Infos;
using static STDhelper.clLogging;
using static ReadCalibox.clGlobals;

namespace ReadCalibox
{
    public static class clGlobals
    {
        private static Form_Main _frmMain;
        public static Form_Main frmMain
        {
            get
            {
                if (_frmMain == null) { _frmMain = new Form_Main(); }
                return _frmMain;
            }
            set { _frmMain = value; }
        }

        public static UC_TT_Item_Infos gTT;
        public static UC_Betrieb gBetrieb = UC_Betrieb.Instance;
        public static UC_Config gConfig = UC_Config.Instance;
        public static UC_DataRead gDataRead = UC_DataRead.Instance;
        public static UC_DataReader_DGV gDataReader_DGV = UC_DataReader_DGV.Instance;
        public static UC_Debug gDebug = UC_Debug.Instance;

        public static string EK_SW_Version;

        /*****************************************************************************************
        * Fonts and Design
        '****************************************************************************************/
        public static clFonts MTFonts = new STDhelper.clFonts();
        public static void Alloc_Fonts(Control c, float size = 8, FontStyle fontStyle = FontStyle.Regular)
        {
            MTFonts.AllocFont(c, size, fontStyle);
        }

    }
}
