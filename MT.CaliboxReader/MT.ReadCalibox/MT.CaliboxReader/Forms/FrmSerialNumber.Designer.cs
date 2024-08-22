
namespace ReadCalibox
{
    partial class FrmSerialNumber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblQuestionInput = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.LblQuestionText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(22, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(37, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "lblTitle";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(25, 213);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(416, 20);
            this.txtInput.TabIndex = 1;
            // 
            // lblQuestionInput
            // 
            this.lblQuestionInput.AutoSize = true;
            this.lblQuestionInput.Location = new System.Drawing.Point(22, 197);
            this.lblQuestionInput.Name = "lblQuestionInput";
            this.lblQuestionInput.Size = new System.Drawing.Size(83, 13);
            this.lblQuestionInput.TabIndex = 0;
            this.lblQuestionInput.Text = "lblQuestionInput";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(366, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LblQuestionText
            // 
            this.LblQuestionText.Location = new System.Drawing.Point(22, 46);
            this.LblQuestionText.Name = "LblQuestionText";
            this.LblQuestionText.Size = new System.Drawing.Size(419, 114);
            this.LblQuestionText.TabIndex = 0;
            this.LblQuestionText.Text = "LblQuestion";
            // 
            // FrmSerialNumber
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(471, 281);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblQuestionInput);
            this.Controls.Add(this.LblQuestionText);
            this.Controls.Add(this.lblTitle);
            this.Name = "FrmSerialNumber";
            this.Text = "FrmSerialNumber";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblQuestionInput;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label LblQuestionText;
    }
}