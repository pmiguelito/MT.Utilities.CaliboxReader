using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OneWireUI;

namespace ConverterCalib
{
    public static class Globals
    {
        /*****************************************************************************
        * UserControls:
        '****************************************************************************/
        public static UcConfig Config1Wire { get; set; }
        public static UcCalib UcCalib { get; set; }

        public static string[] FilesEnding_TDL = new string[] { "tdl", "icf" };

        private static bool _Debugging = true;
        public static bool Debugging 
        { 
            get { return _Debugging; }
            set 
            {
                _Debugging = value;
                if (!value)
                {
                    TestLimits = false;
                }
            } 
        }
        public static bool TestLimits = true;
    }
}
