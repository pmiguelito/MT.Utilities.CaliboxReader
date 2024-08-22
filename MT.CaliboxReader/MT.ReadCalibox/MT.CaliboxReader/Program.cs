using STDhelper;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ReadCalibox
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.Run(new Frm_Main()); }
            catch (Exception ex)
            {
                clLogWriter.Instance.WriteToLog(ex, clConfig.Config_Initvalues.LogError_Path);
                clLogWriter.Instance.ForceFlush();
                Thread.Sleep(500);
            }
        }
    }
}
