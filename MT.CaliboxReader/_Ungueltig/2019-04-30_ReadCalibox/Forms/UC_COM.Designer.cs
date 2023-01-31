namespace ReadCalibox
{
    partial class UC_COM
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
            this._CkB_Active = new System.Windows.Forms.CheckBox();
            this._Lbl_CHname = new System.Windows.Forms.Label();
            this._CoB_COM = new System.Windows.Forms.ComboBox();
            this._TB_FTDIname = new System.Windows.Forms.TextBox();
            this._CoB_BaudRate = new System.Windows.Forms.ComboBox();
            this._Lbl_BaudRate = new System.Windows.Forms.Label();
            this._CkB_FTDI = new System.Windows.Forms.CheckBox();
            this._Cob_BeM = new System.Windows.Forms.ComboBox();
            this._Lbl_BeM = new System.Windows.Forms.Label();
            this._Tb_ReadDelay = new System.Windows.Forms.TextBox();
            this._Panel_BackGround = new System.Windows.Forms.Panel();
            this._Lbl_HandShake = new System.Windows.Forms.Label();
            this._Lbl_StopBits = new System.Windows.Forms.Label();
            this._Lbl_Parity = new System.Windows.Forms.Label();
            this._Lbl_DataBits = new System.Windows.Forms.Label();
            this._CoB_HandShake = new System.Windows.Forms.ComboBox();
            this._CoB_StopBits = new System.Windows.Forms.ComboBox();
            this._CoB_Parity = new System.Windows.Forms.ComboBox();
            this._CoB_DataBits = new System.Windows.Forms.ComboBox();
            this._CkB_ReadLine = new System.Windows.Forms.CheckBox();
            this._Panel_BackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CkB_Active
            // 
            this._CkB_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_Active.AutoSize = true;
            this._CkB_Active.Location = new System.Drawing.Point(107, 4);
            this._CkB_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_Active.Name = "_CkB_Active";
            this._CkB_Active.Size = new System.Drawing.Size(55, 17);
            this._CkB_Active.TabIndex = 7;
            this._CkB_Active.Text = "active";
            this._CkB_Active.UseVisualStyleBackColor = true;
            this._CkB_Active.Click += new System.EventHandler(this.Ckb_Active_Click);
            // 
            // _Lbl_CHname
            // 
            this._Lbl_CHname.AutoSize = true;
            this._Lbl_CHname.Location = new System.Drawing.Point(2, 6);
            this._Lbl_CHname.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_CHname.Name = "_Lbl_CHname";
            this._Lbl_CHname.Size = new System.Drawing.Size(34, 13);
            this._Lbl_CHname.TabIndex = 4;
            this._Lbl_CHname.Text = "CH00";
            // 
            // _CoB_COM
            // 
            this._CoB_COM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CoB_COM.FormattingEnabled = true;
            this._CoB_COM.Location = new System.Drawing.Point(39, 2);
            this._CoB_COM.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_COM.Name = "_CoB_COM";
            this._CoB_COM.Size = new System.Drawing.Size(67, 21);
            this._CoB_COM.TabIndex = 1;
            this._CoB_COM.Click += new System.EventHandler(this.CoB_COM_Click);
            // 
            // _TB_FTDIname
            // 
            this._TB_FTDIname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_FTDIname.Location = new System.Drawing.Point(39, 2);
            this._TB_FTDIname.Margin = new System.Windows.Forms.Padding(0);
            this._TB_FTDIname.Name = "_TB_FTDIname";
            this._TB_FTDIname.Size = new System.Drawing.Size(45, 20);
            this._TB_FTDIname.TabIndex = 2;
            this._TB_FTDIname.Leave += new System.EventHandler(this.TB_FTDIname_Leave);
            // 
            // _CoB_BaudRate
            // 
            this._CoB_BaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CoB_BaudRate.FormattingEnabled = true;
            this._CoB_BaudRate.Location = new System.Drawing.Point(39, 46);
            this._CoB_BaudRate.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_BaudRate.Name = "_CoB_BaudRate";
            this._CoB_BaudRate.Size = new System.Drawing.Size(67, 21);
            this._CoB_BaudRate.TabIndex = 3;
            this._CoB_BaudRate.SelectedIndexChanged += new System.EventHandler(this.CoB_BaudRate_SelectedIndexChanged);
            // 
            // _Lbl_BaudRate
            // 
            this._Lbl_BaudRate.AutoSize = true;
            this._Lbl_BaudRate.Location = new System.Drawing.Point(2, 50);
            this._Lbl_BaudRate.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_BaudRate.Name = "_Lbl_BaudRate";
            this._Lbl_BaudRate.Size = new System.Drawing.Size(32, 13);
            this._Lbl_BaudRate.TabIndex = 4;
            this._Lbl_BaudRate.Text = "Baud";
            // 
            // _CkB_FTDI
            // 
            this._CkB_FTDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_FTDI.AutoSize = true;
            this._CkB_FTDI.Location = new System.Drawing.Point(107, 48);
            this._CkB_FTDI.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_FTDI.Name = "_CkB_FTDI";
            this._CkB_FTDI.Size = new System.Drawing.Size(50, 17);
            this._CkB_FTDI.TabIndex = 6;
            this._CkB_FTDI.Text = "FTDI";
            this._CkB_FTDI.UseVisualStyleBackColor = true;
            this._CkB_FTDI.Click += new System.EventHandler(this.CkB_FTDI_Click);
            // 
            // _Cob_BeM
            // 
            this._Cob_BeM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Cob_BeM.FormattingEnabled = true;
            this._Cob_BeM.Location = new System.Drawing.Point(39, 24);
            this._Cob_BeM.Margin = new System.Windows.Forms.Padding(0);
            this._Cob_BeM.Name = "_Cob_BeM";
            this._Cob_BeM.Size = new System.Drawing.Size(67, 21);
            this._Cob_BeM.TabIndex = 4;
            this._Cob_BeM.SelectedIndexChanged += new System.EventHandler(this.Cob_BeM_SelectedIndexChanged);
            // 
            // _Lbl_BeM
            // 
            this._Lbl_BeM.AutoSize = true;
            this._Lbl_BeM.Location = new System.Drawing.Point(2, 28);
            this._Lbl_BeM.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_BeM.Name = "_Lbl_BeM";
            this._Lbl_BeM.Size = new System.Drawing.Size(29, 13);
            this._Lbl_BeM.TabIndex = 4;
            this._Lbl_BeM.Text = "BeM";
            // 
            // _Tb_ReadDelay
            // 
            this._Tb_ReadDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Tb_ReadDelay.Location = new System.Drawing.Point(107, 24);
            this._Tb_ReadDelay.Margin = new System.Windows.Forms.Padding(0);
            this._Tb_ReadDelay.Name = "_Tb_ReadDelay";
            this._Tb_ReadDelay.Size = new System.Drawing.Size(50, 20);
            this._Tb_ReadDelay.TabIndex = 5;
            this._Tb_ReadDelay.Tag = "Read Delay";
            this._Tb_ReadDelay.Leave += new System.EventHandler(this.TB_ReadDelay_Leave);
            // 
            // _Panel_BackGround
            // 
            this._Panel_BackGround.BackColor = System.Drawing.Color.LightGray;
            this._Panel_BackGround.Controls.Add(this._Tb_ReadDelay);
            this._Panel_BackGround.Controls.Add(this._CkB_Active);
            this._Panel_BackGround.Controls.Add(this._TB_FTDIname);
            this._Panel_BackGround.Controls.Add(this._CkB_ReadLine);
            this._Panel_BackGround.Controls.Add(this._CkB_FTDI);
            this._Panel_BackGround.Controls.Add(this._Lbl_BeM);
            this._Panel_BackGround.Controls.Add(this._Cob_BeM);
            this._Panel_BackGround.Controls.Add(this._CoB_COM);
            this._Panel_BackGround.Controls.Add(this._Lbl_CHname);
            this._Panel_BackGround.Controls.Add(this._Lbl_HandShake);
            this._Panel_BackGround.Controls.Add(this._Lbl_StopBits);
            this._Panel_BackGround.Controls.Add(this._Lbl_Parity);
            this._Panel_BackGround.Controls.Add(this._Lbl_DataBits);
            this._Panel_BackGround.Controls.Add(this._Lbl_BaudRate);
            this._Panel_BackGround.Controls.Add(this._CoB_HandShake);
            this._Panel_BackGround.Controls.Add(this._CoB_StopBits);
            this._Panel_BackGround.Controls.Add(this._CoB_Parity);
            this._Panel_BackGround.Controls.Add(this._CoB_DataBits);
            this._Panel_BackGround.Controls.Add(this._CoB_BaudRate);
            this._Panel_BackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Panel_BackGround.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Panel_BackGround.Location = new System.Drawing.Point(0, 0);
            this._Panel_BackGround.Margin = new System.Windows.Forms.Padding(1);
            this._Panel_BackGround.Name = "_Panel_BackGround";
            this._Panel_BackGround.Size = new System.Drawing.Size(160, 155);
            this._Panel_BackGround.TabIndex = 8;
            // 
            // _Lbl_HandShake
            // 
            this._Lbl_HandShake.AutoSize = true;
            this._Lbl_HandShake.Location = new System.Drawing.Point(2, 134);
            this._Lbl_HandShake.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_HandShake.Name = "_Lbl_HandShake";
            this._Lbl_HandShake.Size = new System.Drawing.Size(22, 13);
            this._Lbl_HandShake.TabIndex = 4;
            this._Lbl_HandShake.Tag = "Handshake";
            this._Lbl_HandShake.Text = "HS";
            // 
            // _Lbl_StopBits
            // 
            this._Lbl_StopBits.AutoSize = true;
            this._Lbl_StopBits.Location = new System.Drawing.Point(2, 113);
            this._Lbl_StopBits.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_StopBits.Name = "_Lbl_StopBits";
            this._Lbl_StopBits.Size = new System.Drawing.Size(30, 13);
            this._Lbl_StopBits.TabIndex = 4;
            this._Lbl_StopBits.Tag = "Stop Bits";
            this._Lbl_StopBits.Text = "Sbits";
            // 
            // _Lbl_Parity
            // 
            this._Lbl_Parity.AutoSize = true;
            this._Lbl_Parity.Location = new System.Drawing.Point(2, 92);
            this._Lbl_Parity.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_Parity.Name = "_Lbl_Parity";
            this._Lbl_Parity.Size = new System.Drawing.Size(33, 13);
            this._Lbl_Parity.TabIndex = 4;
            this._Lbl_Parity.Text = "Parity";
            // 
            // _Lbl_DataBits
            // 
            this._Lbl_DataBits.AutoSize = true;
            this._Lbl_DataBits.Location = new System.Drawing.Point(2, 71);
            this._Lbl_DataBits.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_DataBits.Name = "_Lbl_DataBits";
            this._Lbl_DataBits.Size = new System.Drawing.Size(32, 13);
            this._Lbl_DataBits.TabIndex = 4;
            this._Lbl_DataBits.Tag = "Data Bits";
            this._Lbl_DataBits.Text = "DBits";
            // 
            // _CoB_HandShake
            // 
            this._CoB_HandShake.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CoB_HandShake.FormattingEnabled = true;
            this._CoB_HandShake.Location = new System.Drawing.Point(39, 130);
            this._CoB_HandShake.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_HandShake.Name = "_CoB_HandShake";
            this._CoB_HandShake.Size = new System.Drawing.Size(118, 21);
            this._CoB_HandShake.TabIndex = 3;
            this._CoB_HandShake.SelectedIndexChanged += new System.EventHandler(this.CoB_HandShaking_SelectedIndexChanged);
            // 
            // _CoB_StopBits
            // 
            this._CoB_StopBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CoB_StopBits.FormattingEnabled = true;
            this._CoB_StopBits.Location = new System.Drawing.Point(39, 109);
            this._CoB_StopBits.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_StopBits.Name = "_CoB_StopBits";
            this._CoB_StopBits.Size = new System.Drawing.Size(118, 21);
            this._CoB_StopBits.TabIndex = 3;
            // 
            // _CoB_Parity
            // 
            this._CoB_Parity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CoB_Parity.FormattingEnabled = true;
            this._CoB_Parity.Location = new System.Drawing.Point(39, 88);
            this._CoB_Parity.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_Parity.Name = "_CoB_Parity";
            this._CoB_Parity.Size = new System.Drawing.Size(118, 21);
            this._CoB_Parity.TabIndex = 3;
            // 
            // _CoB_DataBits
            // 
            this._CoB_DataBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CoB_DataBits.FormattingEnabled = true;
            this._CoB_DataBits.Location = new System.Drawing.Point(39, 67);
            this._CoB_DataBits.Margin = new System.Windows.Forms.Padding(0);
            this._CoB_DataBits.Name = "_CoB_DataBits";
            this._CoB_DataBits.Size = new System.Drawing.Size(67, 21);
            this._CoB_DataBits.TabIndex = 3;
            // 
            // _CkB_ReadLine
            // 
            this._CkB_ReadLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_ReadLine.AutoSize = true;
            this._CkB_ReadLine.Location = new System.Drawing.Point(107, 71);
            this._CkB_ReadLine.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_ReadLine.Name = "_CkB_ReadLine";
            this._CkB_ReadLine.Size = new System.Drawing.Size(40, 17);
            this._CkB_ReadLine.TabIndex = 6;
            this._CkB_ReadLine.Tag = "Buffer Read Line";
            this._CkB_ReadLine.Text = "RL";
            this._CkB_ReadLine.UseVisualStyleBackColor = true;
            this._CkB_ReadLine.Click += new System.EventHandler(this.CkB_FTDI_Click);
            // 
            // UC_COM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Panel_BackGround);
            this.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Name = "UC_COM";
            this.Size = new System.Drawing.Size(160, 155);
            this.Load += new System.EventHandler(this.UC_COM_Load);
            this._Panel_BackGround.ResumeLayout(false);
            this._Panel_BackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox _CkB_Active;
        private System.Windows.Forms.Label _Lbl_CHname;
        private System.Windows.Forms.ComboBox _CoB_COM;
        private System.Windows.Forms.TextBox _TB_FTDIname;
        private System.Windows.Forms.ComboBox _CoB_BaudRate;
        private System.Windows.Forms.Label _Lbl_BaudRate;
        private System.Windows.Forms.CheckBox _CkB_FTDI;
        private System.Windows.Forms.ComboBox _Cob_BeM;
        private System.Windows.Forms.Label _Lbl_BeM;
        private System.Windows.Forms.TextBox _Tb_ReadDelay;
        private System.Windows.Forms.Panel _Panel_BackGround;
        private System.Windows.Forms.Label _Lbl_HandShake;
        private System.Windows.Forms.Label _Lbl_StopBits;
        private System.Windows.Forms.Label _Lbl_Parity;
        private System.Windows.Forms.Label _Lbl_DataBits;
        private System.Windows.Forms.ComboBox _CoB_HandShake;
        private System.Windows.Forms.ComboBox _CoB_StopBits;
        private System.Windows.Forms.ComboBox _CoB_Parity;
        private System.Windows.Forms.ComboBox _CoB_DataBits;
        private System.Windows.Forms.CheckBox _CkB_ReadLine;
    }
}
