namespace ReadCalibox
{
    partial class UC_BeM
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this._Panel_BackGround = new System.Windows.Forms.Panel();
            this._TB_BeM = new System.Windows.Forms.TextBox();
            this._Lbl_CHname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._TB_Desc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._CoB_BaudRate = new System.Windows.Forms.ComboBox();
            this._Lbl_BaudRate = new System.Windows.Forms.Label();
            this._CkB_ReadLine = new System.Windows.Forms.CheckBox();
            this._Tb_ReadDelay = new System.Windows.Forms.TextBox();
            this._Panel_BackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Panel_BackGround
            // 
            this._Panel_BackGround.BackColor = System.Drawing.Color.LightGray;
            this._Panel_BackGround.Controls.Add(this._Tb_ReadDelay);
            this._Panel_BackGround.Controls.Add(this._CkB_ReadLine);
            this._Panel_BackGround.Controls.Add(this.label3);
            this._Panel_BackGround.Controls.Add(this._TB_Desc);
            this._Panel_BackGround.Controls.Add(this.label1);
            this._Panel_BackGround.Controls.Add(this._TB_BeM);
            this._Panel_BackGround.Controls.Add(this._Lbl_CHname);
            this._Panel_BackGround.Controls.Add(this._CoB_BaudRate);
            this._Panel_BackGround.Controls.Add(this._Lbl_BaudRate);
            this._Panel_BackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Panel_BackGround.Location = new System.Drawing.Point(0, 0);
            this._Panel_BackGround.Name = "_Panel_BackGround";
            this._Panel_BackGround.Size = new System.Drawing.Size(180, 85);
            this._Panel_BackGround.TabIndex = 0;
            // 
            // _TB_BeM
            // 
            this._TB_BeM.Location = new System.Drawing.Point(42, 3);
            this._TB_BeM.Margin = new System.Windows.Forms.Padding(0);
            this._TB_BeM.Name = "_TB_BeM";
            this._TB_BeM.Size = new System.Drawing.Size(136, 20);
            this._TB_BeM.TabIndex = 5;
            // 
            // _Lbl_CHname
            // 
            this._Lbl_CHname.AutoSize = true;
            this._Lbl_CHname.Location = new System.Drawing.Point(5, 7);
            this._Lbl_CHname.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_CHname.Name = "_Lbl_CHname";
            this._Lbl_CHname.Size = new System.Drawing.Size(29, 13);
            this._Lbl_CHname.TabIndex = 6;
            this._Lbl_CHname.Text = "BeM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Desc";
            // 
            // _TB_Desc
            // 
            this._TB_Desc.Location = new System.Drawing.Point(42, 23);
            this._TB_Desc.Margin = new System.Windows.Forms.Padding(0);
            this._TB_Desc.Name = "_TB_Desc";
            this._TB_Desc.Size = new System.Drawing.Size(136, 20);
            this._TB_Desc.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Delay";
            // 
            // _CoB_BaudRate
            // 
            this._CoB_BaudRate.FormattingEnabled = true;
            this._CoB_BaudRate.Location = new System.Drawing.Point(42, 43);
            this._CoB_BaudRate.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_BaudRate.Name = "_CoB_BaudRate";
            this._CoB_BaudRate.Size = new System.Drawing.Size(67, 21);
            this._CoB_BaudRate.TabIndex = 3;
            this._CoB_BaudRate.SelectedIndexChanged += new System.EventHandler(this._CoB_BaudRate_SelectedIndexChanged);
            // 
            // _Lbl_BaudRate
            // 
            this._Lbl_BaudRate.AutoSize = true;
            this._Lbl_BaudRate.Location = new System.Drawing.Point(5, 47);
            this._Lbl_BaudRate.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_BaudRate.Name = "_Lbl_BaudRate";
            this._Lbl_BaudRate.Size = new System.Drawing.Size(32, 13);
            this._Lbl_BaudRate.TabIndex = 4;
            this._Lbl_BaudRate.Text = "Baud";
            // 
            // _CkB_ReadLine
            // 
            this._CkB_ReadLine.AutoSize = true;
            this._CkB_ReadLine.Location = new System.Drawing.Point(119, 45);
            this._CkB_ReadLine.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_ReadLine.Name = "_CkB_ReadLine";
            this._CkB_ReadLine.Size = new System.Drawing.Size(40, 17);
            this._CkB_ReadLine.TabIndex = 6;
            this._CkB_ReadLine.Tag = "Buffer Read Line";
            this._CkB_ReadLine.Text = "RL";
            this._CkB_ReadLine.UseVisualStyleBackColor = true;
            this._CkB_ReadLine.Click += new System.EventHandler(this.CkB_FTDI_Click);
            // 
            // _Tb_ReadDelay
            // 
            this._Tb_ReadDelay.Location = new System.Drawing.Point(42, 63);
            this._Tb_ReadDelay.Margin = new System.Windows.Forms.Padding(0);
            this._Tb_ReadDelay.Name = "_Tb_ReadDelay";
            this._Tb_ReadDelay.Size = new System.Drawing.Size(67, 20);
            this._Tb_ReadDelay.TabIndex = 5;
            this._Tb_ReadDelay.Tag = "Read Delay";
            this._Tb_ReadDelay.Leave += new System.EventHandler(this.TB_ReadDelay_Leave);
            // 
            // UC_BeM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Panel_BackGround);
            this.Name = "UC_BeM";
            this.Size = new System.Drawing.Size(180, 85);
            this._Panel_BackGround.ResumeLayout(false);
            this._Panel_BackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _Panel_BackGround;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _TB_Desc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _TB_BeM;
        private System.Windows.Forms.Label _Lbl_CHname;
        private System.Windows.Forms.TextBox _Tb_ReadDelay;
        private System.Windows.Forms.CheckBox _CkB_ReadLine;
        private System.Windows.Forms.ComboBox _CoB_BaudRate;
        private System.Windows.Forms.Label _Lbl_BaudRate;
    }
}
