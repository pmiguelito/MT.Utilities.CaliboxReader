using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCalibox.Forms
{
    public partial class UC_Maintenance : UserControl
    {

        /**************************************************************************************
       ** Constructor: Can be used for Panel Controls Load
       ***************************************************************************************/
        #region UC Instance
        private UC_Maintenance _instance;
        public UC_Maintenance Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new UC_Maintenance(); }
                return _instance;
            }
        }
        #endregion UC Instance


        public UC_Maintenance()
        {
            InitializeComponent();
        }
    }
}
