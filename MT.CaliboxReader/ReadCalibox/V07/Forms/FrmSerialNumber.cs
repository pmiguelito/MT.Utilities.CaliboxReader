using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCalibox
{
    public partial class FrmSerialNumber : Form
    {
        public FrmSerialNumber()
        {
            InitializeComponent();
            Load += FrmSerialNumber_Load;
        }

        public FrmSerialNumber(string title, string mainQuestion, string inputQuestion) : this()
        {
            lblTitle.Text = title;
            LblQuestionText.Text = mainQuestion;
            lblQuestionInput.Text = inputQuestion;
        }
        public FrmSerialNumber(string title, string mainQuestion, string inputQuestion, string inputValue) : this(title, mainQuestion, inputQuestion)
        {
            _InitValues = inputValue;
        }

        private void FrmSerialNumber_Load(object sender, EventArgs e)
        {
            txtInput.Text = _InitValues;
        }
        private string _InitValues;

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public bool ValueChanged { get; private set; }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            CheckChanges(txtInput.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            CheckChanges(null);
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CheckChanges(string value)
        {
            _Value = value;
            ValueChanged = Value != _InitValues;
        }
    }
}
