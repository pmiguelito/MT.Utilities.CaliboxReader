namespace ReadCalibox
{
    partial class UC_Config
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
            this._DGV_FTDI = new System.Windows.Forms.DataGridView();
            this._Btn_Load_FTDI = new System.Windows.Forms.Button();
            this._Btn_SaveConfig = new System.Windows.Forms.Button();
            this._FlowPanCOM = new System.Windows.Forms.FlowLayoutPanel();
            this._TB_Path_MeasLog = new System.Windows.Forms.TextBox();
            this._GrB_Log = new System.Windows.Forms.GroupBox();
            this._CkB_Path_MeasLog_Active = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NUD_Ch_Quantity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._CkB_DB_ProdType_Active = new System.Windows.Forms.CheckBox();
            this._TB_DoCheckPass = new System.Windows.Forms.TextBox();
            this._TB_ProcNr = new System.Windows.Forms.TextBox();
            this._TB_ODBC_Initial = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._CkB_Path_Log_Active = new System.Windows.Forms.CheckBox();
            this._TB_Path_Log = new System.Windows.Forms.TextBox();
            this.Pan_BackGround = new System.Windows.Forms.Panel();
            this.FlowPan_BeM = new System.Windows.Forms.FlowLayoutPanel();
            this._TB_ProdType_Table = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._DGV_FTDI)).BeginInit();
            this._GrB_Log.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Ch_Quantity)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.Pan_BackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // _DGV_FTDI
            // 
            this._DGV_FTDI.AllowUserToAddRows = false;
            this._DGV_FTDI.AllowUserToDeleteRows = false;
            this._DGV_FTDI.AllowUserToResizeRows = false;
            this._DGV_FTDI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DGV_FTDI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._DGV_FTDI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._DGV_FTDI.GridColor = System.Drawing.SystemColors.Control;
            this._DGV_FTDI.Location = new System.Drawing.Point(183, 0);
            this._DGV_FTDI.Name = "_DGV_FTDI";
            this._DGV_FTDI.ReadOnly = true;
            this._DGV_FTDI.RowHeadersVisible = false;
            this._DGV_FTDI.Size = new System.Drawing.Size(467, 127);
            this._DGV_FTDI.TabIndex = 4;
            // 
            // _Btn_Load_FTDI
            // 
            this._Btn_Load_FTDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_Load_FTDI.Location = new System.Drawing.Point(567, 133);
            this._Btn_Load_FTDI.Name = "_Btn_Load_FTDI";
            this._Btn_Load_FTDI.Size = new System.Drawing.Size(75, 23);
            this._Btn_Load_FTDI.TabIndex = 5;
            this._Btn_Load_FTDI.Text = "Load";
            this._Btn_Load_FTDI.UseVisualStyleBackColor = true;
            this._Btn_Load_FTDI.Click += new System.EventHandler(this.Btn_Load_FTDI_Click);
            // 
            // _Btn_SaveConfig
            // 
            this._Btn_SaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_SaveConfig.Location = new System.Drawing.Point(572, 574);
            this._Btn_SaveConfig.Name = "_Btn_SaveConfig";
            this._Btn_SaveConfig.Size = new System.Drawing.Size(75, 23);
            this._Btn_SaveConfig.TabIndex = 5;
            this._Btn_SaveConfig.Text = "Save Config";
            this._Btn_SaveConfig.UseVisualStyleBackColor = true;
            this._Btn_SaveConfig.Click += new System.EventHandler(this.Btn_SaveConfig_Click);
            // 
            // _FlowPanCOM
            // 
            this._FlowPanCOM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._FlowPanCOM.AutoScroll = true;
            this._FlowPanCOM.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._FlowPanCOM.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._FlowPanCOM.Location = new System.Drawing.Point(0, 0);
            this._FlowPanCOM.Margin = new System.Windows.Forms.Padding(0);
            this._FlowPanCOM.Name = "_FlowPanCOM";
            this._FlowPanCOM.Size = new System.Drawing.Size(180, 600);
            this._FlowPanCOM.TabIndex = 19;
            this._FlowPanCOM.WrapContents = false;
            // 
            // _TB_Path_MeasLog
            // 
            this._TB_Path_MeasLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_Path_MeasLog.Location = new System.Drawing.Point(6, 19);
            this._TB_Path_MeasLog.Name = "_TB_Path_MeasLog";
            this._TB_Path_MeasLog.Size = new System.Drawing.Size(395, 20);
            this._TB_Path_MeasLog.TabIndex = 20;
            // 
            // _GrB_Log
            // 
            this._GrB_Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GrB_Log.Controls.Add(this._CkB_Path_MeasLog_Active);
            this._GrB_Log.Controls.Add(this._TB_Path_MeasLog);
            this._GrB_Log.Location = new System.Drawing.Point(183, 478);
            this._GrB_Log.Margin = new System.Windows.Forms.Padding(0);
            this._GrB_Log.Name = "_GrB_Log";
            this._GrB_Log.Size = new System.Drawing.Size(463, 47);
            this._GrB_Log.TabIndex = 24;
            this._GrB_Log.TabStop = false;
            this._GrB_Log.Text = "Meas Log:";
            // 
            // _CkB_Path_MeasLog_Active
            // 
            this._CkB_Path_MeasLog_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_Path_MeasLog_Active.AutoSize = true;
            this._CkB_Path_MeasLog_Active.Location = new System.Drawing.Point(404, 21);
            this._CkB_Path_MeasLog_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_Path_MeasLog_Active.Name = "_CkB_Path_MeasLog_Active";
            this._CkB_Path_MeasLog_Active.Size = new System.Drawing.Size(55, 17);
            this._CkB_Path_MeasLog_Active.TabIndex = 21;
            this._CkB_Path_MeasLog_Active.Text = "active";
            this._CkB_Path_MeasLog_Active.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.NUD_Ch_Quantity);
            this.groupBox1.Controls.Add(this._TB_DoCheckPass);
            this.groupBox1.Controls.Add(this._TB_ProcNr);
            this.groupBox1.Controls.Add(this._TB_ProdType_Table);
            this.groupBox1.Controls.Add(this._TB_ODBC_Initial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._CkB_DB_ProdType_Active);
            this.groupBox1.Location = new System.Drawing.Point(408, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 126);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Init values";
            // 
            // NUD_Ch_Quantity
            // 
            this.NUD_Ch_Quantity.Location = new System.Drawing.Point(85, 103);
            this.NUD_Ch_Quantity.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NUD_Ch_Quantity.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUD_Ch_Quantity.Name = "NUD_Ch_Quantity";
            this.NUD_Ch_Quantity.Size = new System.Drawing.Size(67, 20);
            this.NUD_Ch_Quantity.TabIndex = 0;
            this.NUD_Ch_Quantity.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "CH quantity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "DoCheckPass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "ProcNr";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "ODBC EK Init";
            // 
            // _CkB_DB_ProdType_Active
            // 
            this._CkB_DB_ProdType_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_DB_ProdType_Active.AutoSize = true;
            this._CkB_DB_ProdType_Active.Location = new System.Drawing.Point(196, 59);
            this._CkB_DB_ProdType_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_DB_ProdType_Active.Name = "_CkB_DB_ProdType_Active";
            this._CkB_DB_ProdType_Active.Size = new System.Drawing.Size(41, 17);
            this._CkB_DB_ProdType_Active.TabIndex = 21;
            this._CkB_DB_ProdType_Active.Text = "DB";
            this._CkB_DB_ProdType_Active.UseVisualStyleBackColor = true;
            this._CkB_DB_ProdType_Active.CheckedChanged += new System.EventHandler(this._CkB_DB_ProdType_Active_CheckedChanged);
            // 
            // _TB_DoCheckPass
            // 
            this._TB_DoCheckPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_DoCheckPass.Location = new System.Drawing.Point(85, 80);
            this._TB_DoCheckPass.Name = "_TB_DoCheckPass";
            this._TB_DoCheckPass.Size = new System.Drawing.Size(154, 20);
            this._TB_DoCheckPass.TabIndex = 20;
            this._TB_DoCheckPass.MouseLeave += new System.EventHandler(this._TB_DoCheckPass_MouseLeave);
            // 
            // _TB_ProcNr
            // 
            this._TB_ProcNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_ProcNr.Location = new System.Drawing.Point(85, 57);
            this._TB_ProcNr.Name = "_TB_ProcNr";
            this._TB_ProcNr.Size = new System.Drawing.Size(91, 20);
            this._TB_ProcNr.TabIndex = 20;
            this._TB_ProcNr.Leave += new System.EventHandler(this._TB_ProcNr_Leave);
            // 
            // _TB_ODBC_Initial
            // 
            this._TB_ODBC_Initial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_ODBC_Initial.Location = new System.Drawing.Point(85, 12);
            this._TB_ODBC_Initial.Name = "_TB_ODBC_Initial";
            this._TB_ODBC_Initial.Size = new System.Drawing.Size(153, 20);
            this._TB_ODBC_Initial.TabIndex = 20;
            this._TB_ODBC_Initial.Leave += new System.EventHandler(this._TB_ODBC_Initial_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._CkB_Path_Log_Active);
            this.groupBox2.Controls.Add(this._TB_Path_Log);
            this.groupBox2.Location = new System.Drawing.Point(183, 524);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 47);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Error Log:";
            // 
            // _CkB_Path_Log_Active
            // 
            this._CkB_Path_Log_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_Path_Log_Active.AutoSize = true;
            this._CkB_Path_Log_Active.Location = new System.Drawing.Point(404, 21);
            this._CkB_Path_Log_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_Path_Log_Active.Name = "_CkB_Path_Log_Active";
            this._CkB_Path_Log_Active.Size = new System.Drawing.Size(55, 17);
            this._CkB_Path_Log_Active.TabIndex = 21;
            this._CkB_Path_Log_Active.Text = "active";
            this._CkB_Path_Log_Active.UseVisualStyleBackColor = true;
            // 
            // _TB_Path_Log
            // 
            this._TB_Path_Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_Path_Log.Location = new System.Drawing.Point(6, 19);
            this._TB_Path_Log.Name = "_TB_Path_Log";
            this._TB_Path_Log.Size = new System.Drawing.Size(395, 20);
            this._TB_Path_Log.TabIndex = 20;
            // 
            // Pan_BackGround
            // 
            this.Pan_BackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Pan_BackGround.Controls.Add(this.groupBox1);
            this.Pan_BackGround.Controls.Add(this.FlowPan_BeM);
            this.Pan_BackGround.Controls.Add(this._Btn_Load_FTDI);
            this.Pan_BackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pan_BackGround.Location = new System.Drawing.Point(0, 0);
            this.Pan_BackGround.Name = "Pan_BackGround";
            this.Pan_BackGround.Size = new System.Drawing.Size(650, 600);
            this.Pan_BackGround.TabIndex = 26;
            // 
            // FlowPan_BeM
            // 
            this.FlowPan_BeM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FlowPan_BeM.AutoScroll = true;
            this.FlowPan_BeM.Location = new System.Drawing.Point(183, 133);
            this.FlowPan_BeM.Name = "FlowPan_BeM";
            this.FlowPan_BeM.Size = new System.Drawing.Size(219, 342);
            this.FlowPan_BeM.TabIndex = 0;
            // 
            // _TB_ProdType_Table
            // 
            this._TB_ProdType_Table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_ProdType_Table.Location = new System.Drawing.Point(85, 34);
            this._TB_ProdType_Table.Name = "_TB_ProdType_Table";
            this._TB_ProdType_Table.Size = new System.Drawing.Size(153, 20);
            this._TB_ProdType_Table.TabIndex = 20;
            this._TB_ProdType_Table.Leave += new System.EventHandler(this._TB_ODBC_Initial_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "ProdType Table";
            // 
            // UC_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._GrB_Log);
            this.Controls.Add(this._FlowPanCOM);
            this.Controls.Add(this._Btn_SaveConfig);
            this.Controls.Add(this._DGV_FTDI);
            this.Controls.Add(this.Pan_BackGround);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_Config";
            this.Size = new System.Drawing.Size(650, 600);
            this.Load += new System.EventHandler(this.UC_Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DGV_FTDI)).EndInit();
            this._GrB_Log.ResumeLayout(false);
            this._GrB_Log.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Ch_Quantity)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Pan_BackGround.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView _DGV_FTDI;
        private System.Windows.Forms.Button _Btn_Load_FTDI;
        private System.Windows.Forms.Button _Btn_SaveConfig;
        private System.Windows.Forms.FlowLayoutPanel _FlowPanCOM;
        private System.Windows.Forms.TextBox _TB_Path_MeasLog;
        private System.Windows.Forms.GroupBox _GrB_Log;
        private System.Windows.Forms.CheckBox _CkB_Path_MeasLog_Active;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _CkB_DB_ProdType_Active;
        private System.Windows.Forms.TextBox _TB_DoCheckPass;
        private System.Windows.Forms.TextBox _TB_ProcNr;
        private System.Windows.Forms.TextBox _TB_ODBC_Initial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox _CkB_Path_Log_Active;
        private System.Windows.Forms.TextBox _TB_Path_Log;
        private System.Windows.Forms.Panel Pan_BackGround;
        private System.Windows.Forms.NumericUpDown NUD_Ch_Quantity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel FlowPan_BeM;
        private System.Windows.Forms.TextBox _TB_ProdType_Table;
        private System.Windows.Forms.Label label5;
    }
}
