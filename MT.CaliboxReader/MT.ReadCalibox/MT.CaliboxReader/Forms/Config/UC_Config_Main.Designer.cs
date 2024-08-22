namespace ReadCalibox
{
    partial class UC_Config_Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._Btn_SaveConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NUD_Ch_Quantity = new System.Windows.Forms.NumericUpDown();
            this.Tab_Config = new System.Windows.Forms.TabControl();
            this.Tab_Config_HW = new System.Windows.Forms.TabPage();
            this._DGV_FTDI = new System.Windows.Forms.DataGridView();
            this._Btn_Load_FTDI = new System.Windows.Forms.Button();
            this._FlowPanCOM = new System.Windows.Forms.FlowLayoutPanel();
            this.FlowPan_BeM = new System.Windows.Forms.FlowLayoutPanel();
            this.Tab_Config_DB = new System.Windows.Forms.TabPage();
            this.Uc_Config_DB = new ReadCalibox.UC_Config_DB();
            this.GrB_LogErrors = new System.Windows.Forms.GroupBox();
            this._CkB_LogError_Path_Active = new System.Windows.Forms.CheckBox();
            this._TB_LogError_Path = new System.Windows.Forms.TextBox();
            this.GrB_LogMeas = new System.Windows.Forms.GroupBox();
            this._CkB_LogMeas_DB_Active = new System.Windows.Forms.CheckBox();
            this._CkB_LogMeas_Path_Active = new System.Windows.Forms.CheckBox();
            this._TB_LogMeas_Path = new System.Windows.Forms.TextBox();
            this.Tab_Config_DebugCMD = new System.Windows.Forms.TabPage();
            this.UcDebugCMD = new ReadCalibox.UC_Debug_CMD();
            this.Tab_Config_DebugDGV = new System.Windows.Forms.TabPage();
            this.UcDebug_DGV = new ReadCalibox.UC_Debug_DGV();
            this.Pan_BackGround = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Ch_Quantity)).BeginInit();
            this.Tab_Config.SuspendLayout();
            this.Tab_Config_HW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._DGV_FTDI)).BeginInit();
            this.Tab_Config_DB.SuspendLayout();
            this.GrB_LogErrors.SuspendLayout();
            this.GrB_LogMeas.SuspendLayout();
            this.Tab_Config_DebugCMD.SuspendLayout();
            this.Tab_Config_DebugDGV.SuspendLayout();
            this.Pan_BackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._Btn_SaveConfig);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.NUD_Ch_Quantity);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 30);
            this.panel1.TabIndex = 27;
            // 
            // _Btn_SaveConfig
            // 
            this._Btn_SaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_SaveConfig.Location = new System.Drawing.Point(571, 4);
            this._Btn_SaveConfig.Name = "_Btn_SaveConfig";
            this._Btn_SaveConfig.Size = new System.Drawing.Size(75, 23);
            this._Btn_SaveConfig.TabIndex = 5;
            this._Btn_SaveConfig.Text = "Save Config";
            this._Btn_SaveConfig.UseVisualStyleBackColor = true;
            this._Btn_SaveConfig.Click += new System.EventHandler(this.Btn_SaveConfig_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "CH quantity";
            // 
            // NUD_Ch_Quantity
            // 
            this.NUD_Ch_Quantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NUD_Ch_Quantity.Location = new System.Drawing.Point(498, 5);
            this.NUD_Ch_Quantity.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NUD_Ch_Quantity.Minimum = new decimal(new int[] {
            1,
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
            // Tab_Config
            // 
            this.Tab_Config.Controls.Add(this.Tab_Config_HW);
            this.Tab_Config.Controls.Add(this.Tab_Config_DB);
            this.Tab_Config.Controls.Add(this.Tab_Config_DebugCMD);
            this.Tab_Config.Controls.Add(this.Tab_Config_DebugDGV);
            this.Tab_Config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Config.Location = new System.Drawing.Point(0, 0);
            this.Tab_Config.Name = "Tab_Config";
            this.Tab_Config.SelectedIndex = 0;
            this.Tab_Config.Size = new System.Drawing.Size(650, 570);
            this.Tab_Config.TabIndex = 26;
            // 
            // Tab_Config_HW
            // 
            this.Tab_Config_HW.Controls.Add(this._DGV_FTDI);
            this.Tab_Config_HW.Controls.Add(this._Btn_Load_FTDI);
            this.Tab_Config_HW.Controls.Add(this.FlowPan_BeM);
            this.Tab_Config_HW.Controls.Add(this._FlowPanCOM);
            this.Tab_Config_HW.Location = new System.Drawing.Point(4, 22);
            this.Tab_Config_HW.Name = "Tab_Config_HW";
            this.Tab_Config_HW.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Config_HW.Size = new System.Drawing.Size(642, 544);
            this.Tab_Config_HW.TabIndex = 0;
            this.Tab_Config_HW.Text = "Hardware";
            this.Tab_Config_HW.UseVisualStyleBackColor = true;
            // 
            // _DGV_FTDI
            // 
            this._DGV_FTDI.AllowUserToAddRows = false;
            this._DGV_FTDI.AllowUserToDeleteRows = false;
            this._DGV_FTDI.AllowUserToResizeRows = false;
            this._DGV_FTDI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DGV_FTDI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._DGV_FTDI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._DGV_FTDI.GridColor = System.Drawing.SystemColors.Control;
            this._DGV_FTDI.Location = new System.Drawing.Point(379, 0);
            this._DGV_FTDI.Name = "_DGV_FTDI";
            this._DGV_FTDI.ReadOnly = true;
            this._DGV_FTDI.RowHeadersVisible = false;
            this._DGV_FTDI.Size = new System.Drawing.Size(264, 506);
            this._DGV_FTDI.TabIndex = 4;
            // 
            // _Btn_Load_FTDI
            // 
            this._Btn_Load_FTDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_Load_FTDI.Location = new System.Drawing.Point(564, 515);
            this._Btn_Load_FTDI.Name = "_Btn_Load_FTDI";
            this._Btn_Load_FTDI.Size = new System.Drawing.Size(75, 23);
            this._Btn_Load_FTDI.TabIndex = 5;
            this._Btn_Load_FTDI.Text = "Load";
            this._Btn_Load_FTDI.UseVisualStyleBackColor = true;
            this._Btn_Load_FTDI.Click += new System.EventHandler(this.Btn_Load_FTDI_Click);
            // 
            // _FlowPanCOM
            // 
            this._FlowPanCOM.AutoScroll = true;
            this._FlowPanCOM.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._FlowPanCOM.Dock = System.Windows.Forms.DockStyle.Left;
            this._FlowPanCOM.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._FlowPanCOM.Location = new System.Drawing.Point(3, 3);
            this._FlowPanCOM.Margin = new System.Windows.Forms.Padding(0);
            this._FlowPanCOM.Name = "_FlowPanCOM";
            this._FlowPanCOM.Size = new System.Drawing.Size(170, 538);
            this._FlowPanCOM.TabIndex = 19;
            this._FlowPanCOM.WrapContents = false;
            // 
            // FlowPan_BeM
            // 
            this.FlowPan_BeM.AutoScroll = true;
            this.FlowPan_BeM.Dock = System.Windows.Forms.DockStyle.Left;
            this.FlowPan_BeM.Location = new System.Drawing.Point(173, 3);
            this.FlowPan_BeM.Name = "FlowPan_BeM";
            this.FlowPan_BeM.Size = new System.Drawing.Size(200, 538);
            this.FlowPan_BeM.TabIndex = 0;
            // 
            // Tab_Config_DB
            // 
            this.Tab_Config_DB.Controls.Add(this.Uc_Config_DB);
            this.Tab_Config_DB.Controls.Add(this.GrB_LogErrors);
            this.Tab_Config_DB.Controls.Add(this.GrB_LogMeas);
            this.Tab_Config_DB.Location = new System.Drawing.Point(4, 22);
            this.Tab_Config_DB.Name = "Tab_Config_DB";
            this.Tab_Config_DB.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Config_DB.Size = new System.Drawing.Size(642, 544);
            this.Tab_Config_DB.TabIndex = 1;
            this.Tab_Config_DB.Text = "Datenbank";
            this.Tab_Config_DB.UseVisualStyleBackColor = true;
            // 
            // Uc_Config_DB
            // 
            this.Uc_Config_DB.DB_ProdType_Active = false;
            this.Uc_Config_DB.DoCheckPassTXT = null;
            this.Uc_Config_DB.Dock = System.Windows.Forms.DockStyle.Top;
            this.Uc_Config_DB.Kategorie_Name = "Init Values";
            this.Uc_Config_DB.Location = new System.Drawing.Point(3, 3);
            this.Uc_Config_DB.Name = "Uc_Config_DB";
            this.Uc_Config_DB.ODBC_Init = null;
            this.Uc_Config_DB.ProcNr = null;
            this.Uc_Config_DB.ProdType_Table = null;
            this.Uc_Config_DB.Size = new System.Drawing.Size(636, 171);
            this.Uc_Config_DB.TabIndex = 25;
            // 
            // GrB_LogErrors
            // 
            this.GrB_LogErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrB_LogErrors.Controls.Add(this._CkB_LogError_Path_Active);
            this.GrB_LogErrors.Controls.Add(this._TB_LogError_Path);
            this.GrB_LogErrors.Location = new System.Drawing.Point(3, 494);
            this.GrB_LogErrors.Margin = new System.Windows.Forms.Padding(0);
            this.GrB_LogErrors.Name = "GrB_LogErrors";
            this.GrB_LogErrors.Size = new System.Drawing.Size(636, 47);
            this.GrB_LogErrors.TabIndex = 24;
            this.GrB_LogErrors.TabStop = false;
            this.GrB_LogErrors.Text = "Error Log:";
            // 
            // _CkB_LogError_Path_Active
            // 
            this._CkB_LogError_Path_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_LogError_Path_Active.AutoSize = true;
            this._CkB_LogError_Path_Active.Location = new System.Drawing.Point(577, 21);
            this._CkB_LogError_Path_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_LogError_Path_Active.Name = "_CkB_LogError_Path_Active";
            this._CkB_LogError_Path_Active.Size = new System.Drawing.Size(55, 17);
            this._CkB_LogError_Path_Active.TabIndex = 21;
            this._CkB_LogError_Path_Active.Text = "active";
            this._CkB_LogError_Path_Active.UseVisualStyleBackColor = true;
            // 
            // _TB_LogError_Path
            // 
            this._TB_LogError_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_LogError_Path.Location = new System.Drawing.Point(6, 19);
            this._TB_LogError_Path.Name = "_TB_LogError_Path";
            this._TB_LogError_Path.Size = new System.Drawing.Size(568, 20);
            this._TB_LogError_Path.TabIndex = 20;
            // 
            // GrB_LogMeas
            // 
            this.GrB_LogMeas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrB_LogMeas.Controls.Add(this._CkB_LogMeas_DB_Active);
            this.GrB_LogMeas.Controls.Add(this._CkB_LogMeas_Path_Active);
            this.GrB_LogMeas.Controls.Add(this._TB_LogMeas_Path);
            this.GrB_LogMeas.Location = new System.Drawing.Point(3, 447);
            this.GrB_LogMeas.Margin = new System.Windows.Forms.Padding(0);
            this.GrB_LogMeas.Name = "GrB_LogMeas";
            this.GrB_LogMeas.Size = new System.Drawing.Size(636, 47);
            this.GrB_LogMeas.TabIndex = 24;
            this.GrB_LogMeas.TabStop = false;
            this.GrB_LogMeas.Text = "Meas Log:";
            // 
            // _CkB_LogMeas_DB_Active
            // 
            this._CkB_LogMeas_DB_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_LogMeas_DB_Active.AutoSize = true;
            this._CkB_LogMeas_DB_Active.Location = new System.Drawing.Point(577, 28);
            this._CkB_LogMeas_DB_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_LogMeas_DB_Active.Name = "_CkB_LogMeas_DB_Active";
            this._CkB_LogMeas_DB_Active.Size = new System.Drawing.Size(41, 17);
            this._CkB_LogMeas_DB_Active.TabIndex = 21;
            this._CkB_LogMeas_DB_Active.Text = "DB";
            this._CkB_LogMeas_DB_Active.UseVisualStyleBackColor = true;
            // 
            // _CkB_LogMeas_Path_Active
            // 
            this._CkB_LogMeas_Path_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CkB_LogMeas_Path_Active.AutoSize = true;
            this._CkB_LogMeas_Path_Active.Location = new System.Drawing.Point(577, 9);
            this._CkB_LogMeas_Path_Active.Margin = new System.Windows.Forms.Padding(0);
            this._CkB_LogMeas_Path_Active.Name = "_CkB_LogMeas_Path_Active";
            this._CkB_LogMeas_Path_Active.Size = new System.Drawing.Size(42, 17);
            this._CkB_LogMeas_Path_Active.TabIndex = 21;
            this._CkB_LogMeas_Path_Active.Text = "File";
            this._CkB_LogMeas_Path_Active.UseVisualStyleBackColor = true;
            // 
            // _TB_LogMeas_Path
            // 
            this._TB_LogMeas_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_LogMeas_Path.Location = new System.Drawing.Point(6, 19);
            this._TB_LogMeas_Path.Name = "_TB_LogMeas_Path";
            this._TB_LogMeas_Path.Size = new System.Drawing.Size(568, 20);
            this._TB_LogMeas_Path.TabIndex = 20;
            // 
            // Tab_Config_DebugCMD
            // 
            this.Tab_Config_DebugCMD.Controls.Add(this.UcDebugCMD);
            this.Tab_Config_DebugCMD.Location = new System.Drawing.Point(4, 22);
            this.Tab_Config_DebugCMD.Name = "Tab_Config_DebugCMD";
            this.Tab_Config_DebugCMD.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Config_DebugCMD.Size = new System.Drawing.Size(642, 544);
            this.Tab_Config_DebugCMD.TabIndex = 2;
            this.Tab_Config_DebugCMD.Text = "Debug CMD";
            this.Tab_Config_DebugCMD.UseVisualStyleBackColor = true;
            // 
            // UcDebugCMD
            // 
            this.UcDebugCMD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.UcDebugCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UcDebugCMD.Location = new System.Drawing.Point(3, 3);
            this.UcDebugCMD.Name = "UcDebugCMD";
            this.UcDebugCMD.Size = new System.Drawing.Size(636, 538);
            this.UcDebugCMD.TabIndex = 0;
            // 
            // Tab_Config_DebugDGV
            // 
            this.Tab_Config_DebugDGV.Controls.Add(this.UcDebug_DGV);
            this.Tab_Config_DebugDGV.Location = new System.Drawing.Point(4, 22);
            this.Tab_Config_DebugDGV.Name = "Tab_Config_DebugDGV";
            this.Tab_Config_DebugDGV.Size = new System.Drawing.Size(642, 544);
            this.Tab_Config_DebugDGV.TabIndex = 3;
            this.Tab_Config_DebugDGV.Text = "Debung Tables";
            this.Tab_Config_DebugDGV.UseVisualStyleBackColor = true;
            // 
            // UcDebug_DGV
            // 
            this.UcDebug_DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UcDebug_DGV.Location = new System.Drawing.Point(0, 0);
            this.UcDebug_DGV.Name = "UcDebug_DGV";
            this.UcDebug_DGV.Size = new System.Drawing.Size(642, 544);
            this.UcDebug_DGV.TabIndex = 0;
            // 
            // Pan_BackGround
            // 
            this.Pan_BackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Pan_BackGround.Controls.Add(this.Tab_Config);
            this.Pan_BackGround.Controls.Add(this.panel1);
            this.Pan_BackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pan_BackGround.Location = new System.Drawing.Point(0, 0);
            this.Pan_BackGround.Name = "Pan_BackGround";
            this.Pan_BackGround.Size = new System.Drawing.Size(650, 600);
            this.Pan_BackGround.TabIndex = 26;
            // 
            // UC_Config_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Pan_BackGround);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_Config_Main";
            this.Size = new System.Drawing.Size(650, 600);
            this.Load += new System.EventHandler(this.UC_Config_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Ch_Quantity)).EndInit();
            this.Tab_Config.ResumeLayout(false);
            this.Tab_Config_HW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._DGV_FTDI)).EndInit();
            this.Tab_Config_DB.ResumeLayout(false);
            this.GrB_LogErrors.ResumeLayout(false);
            this.GrB_LogErrors.PerformLayout();
            this.GrB_LogMeas.ResumeLayout(false);
            this.GrB_LogMeas.PerformLayout();
            this.Tab_Config_DebugCMD.ResumeLayout(false);
            this.Tab_Config_DebugDGV.ResumeLayout(false);
            this.Pan_BackGround.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _Btn_SaveConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUD_Ch_Quantity;
        private System.Windows.Forms.TabControl Tab_Config;
        private System.Windows.Forms.TabPage Tab_Config_HW;
        private System.Windows.Forms.DataGridView _DGV_FTDI;
        private System.Windows.Forms.Button _Btn_Load_FTDI;
        private System.Windows.Forms.FlowLayoutPanel _FlowPanCOM;
        private System.Windows.Forms.FlowLayoutPanel FlowPan_BeM;
        private System.Windows.Forms.TabPage Tab_Config_DB;
        private System.Windows.Forms.GroupBox GrB_LogErrors;
        private System.Windows.Forms.CheckBox _CkB_LogError_Path_Active;
        private System.Windows.Forms.TextBox _TB_LogError_Path;
        private System.Windows.Forms.GroupBox GrB_LogMeas;
        private System.Windows.Forms.CheckBox _CkB_LogMeas_Path_Active;
        private System.Windows.Forms.TextBox _TB_LogMeas_Path;
        private System.Windows.Forms.Panel Pan_BackGround;
        private System.Windows.Forms.TabPage Tab_Config_DebugCMD;
        private UC_Debug_CMD UcDebugCMD;
        private System.Windows.Forms.CheckBox _CkB_LogMeas_DB_Active;
        private System.Windows.Forms.TabPage Tab_Config_DebugDGV;
        private UC_Debug_DGV UcDebug_DGV;
        private UC_Config_DB Uc_Config_DB;
    }
}
