namespace ReadCalibox
{
    partial class UC_Config_DB
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
            this.CoB_Tables = new System.Windows.Forms.ComboBox();
            this.TB_DoCheckPass = new System.Windows.Forms.TextBox();
            this.TB_ProcNr = new System.Windows.Forms.TextBox();
            this.TB_ODBC_TT = new System.Windows.Forms.TextBox();
            this.TB_ODBC_EK = new System.Windows.Forms.TextBox();
            this.TB_ODBC_Initial = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CkB_DB_ProdType_Active = new System.Windows.Forms.CheckBox();
            this.GrB_ODBC = new System.Windows.Forms.GroupBox();
            this.TB_ProdType_Table = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Grb_Values = new System.Windows.Forms.GroupBox();
            this.Ckb_GenerateSN = new System.Windows.Forms.CheckBox();
            this.Ckb_Log_Path_TT = new System.Windows.Forms.CheckBox();
            this.TB_UserTable = new System.Windows.Forms.TextBox();
            this.TB_Log_Path_TT = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GrB_ODBC.SuspendLayout();
            this.Grb_Values.SuspendLayout();
            this.SuspendLayout();
            // 
            // CoB_Tables
            // 
            this.CoB_Tables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CoB_Tables.FormattingEnabled = true;
            this.CoB_Tables.Location = new System.Drawing.Point(89, 13);
            this.CoB_Tables.Name = "CoB_Tables";
            this.CoB_Tables.Size = new System.Drawing.Size(180, 21);
            this.CoB_Tables.TabIndex = 23;
            this.CoB_Tables.SelectedIndexChanged += new System.EventHandler(this.CoB_Tables_SelectedIndexChanged);
            // 
            // TB_DoCheckPass
            // 
            this.TB_DoCheckPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_DoCheckPass.Location = new System.Drawing.Point(89, 38);
            this.TB_DoCheckPass.Name = "TB_DoCheckPass";
            this.TB_DoCheckPass.Size = new System.Drawing.Size(180, 20);
            this.TB_DoCheckPass.TabIndex = 20;
            // 
            // TB_ProcNr
            // 
            this.TB_ProcNr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ProcNr.Location = new System.Drawing.Point(366, 38);
            this.TB_ProcNr.Name = "TB_ProcNr";
            this.TB_ProcNr.Size = new System.Drawing.Size(119, 20);
            this.TB_ProcNr.TabIndex = 20;
            this.TB_ProcNr.Leave += new System.EventHandler(this.TB_ProcNr_Leave);
            // 
            // TB_ODBC_TT
            // 
            this.TB_ODBC_TT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ODBC_TT.Location = new System.Drawing.Point(366, 65);
            this.TB_ODBC_TT.Name = "TB_ODBC_TT";
            this.TB_ODBC_TT.ReadOnly = true;
            this.TB_ODBC_TT.Size = new System.Drawing.Size(228, 20);
            this.TB_ODBC_TT.TabIndex = 20;
            // 
            // TB_ODBC_EK
            // 
            this.TB_ODBC_EK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ODBC_EK.Location = new System.Drawing.Point(89, 64);
            this.TB_ODBC_EK.Name = "TB_ODBC_EK";
            this.TB_ODBC_EK.ReadOnly = true;
            this.TB_ODBC_EK.Size = new System.Drawing.Size(180, 20);
            this.TB_ODBC_EK.TabIndex = 20;
            // 
            // TB_ODBC_Initial
            // 
            this.TB_ODBC_Initial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ODBC_Initial.Location = new System.Drawing.Point(89, 16);
            this.TB_ODBC_Initial.Name = "TB_ODBC_Initial";
            this.TB_ODBC_Initial.Size = new System.Drawing.Size(180, 20);
            this.TB_ODBC_Initial.TabIndex = 20;
            this.TB_ODBC_Initial.Leave += new System.EventHandler(this.TB_ODBC_Initial_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "DoCheckPass:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(280, 69);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "ODBC TT:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "ProcNr:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "ODBC EK:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "ProdType Table:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "ODBC EK Init:";
            // 
            // CkB_DB_ProdType_Active
            // 
            this.CkB_DB_ProdType_Active.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CkB_DB_ProdType_Active.AutoSize = true;
            this.CkB_DB_ProdType_Active.Location = new System.Drawing.Point(556, 18);
            this.CkB_DB_ProdType_Active.Margin = new System.Windows.Forms.Padding(0);
            this.CkB_DB_ProdType_Active.Name = "CkB_DB_ProdType_Active";
            this.CkB_DB_ProdType_Active.Size = new System.Drawing.Size(41, 17);
            this.CkB_DB_ProdType_Active.TabIndex = 21;
            this.CkB_DB_ProdType_Active.Text = "DB";
            this.CkB_DB_ProdType_Active.UseVisualStyleBackColor = true;
            this.CkB_DB_ProdType_Active.CheckedChanged += new System.EventHandler(this.CkB_DB_ProdType_Active_CheckedChanged);
            // 
            // GrB_ODBC
            // 
            this.GrB_ODBC.Controls.Add(this.CkB_DB_ProdType_Active);
            this.GrB_ODBC.Controls.Add(this.TB_ODBC_Initial);
            this.GrB_ODBC.Controls.Add(this.label2);
            this.GrB_ODBC.Controls.Add(this.TB_ProdType_Table);
            this.GrB_ODBC.Controls.Add(this.label7);
            this.GrB_ODBC.Dock = System.Windows.Forms.DockStyle.Top;
            this.GrB_ODBC.Location = new System.Drawing.Point(0, 0);
            this.GrB_ODBC.Name = "GrB_ODBC";
            this.GrB_ODBC.Size = new System.Drawing.Size(600, 51);
            this.GrB_ODBC.TabIndex = 26;
            this.GrB_ODBC.TabStop = false;
            this.GrB_ODBC.Text = "Init values";
            // 
            // TB_ProdType_Table
            // 
            this.TB_ProdType_Table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ProdType_Table.Location = new System.Drawing.Point(366, 16);
            this.TB_ProdType_Table.Name = "TB_ProdType_Table";
            this.TB_ProdType_Table.ReadOnly = true;
            this.TB_ProdType_Table.Size = new System.Drawing.Size(180, 20);
            this.TB_ProdType_Table.TabIndex = 20;
            this.TB_ProdType_Table.Leave += new System.EventHandler(this.TB_ProcNr_Leave);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(280, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "ProdType Table:";
            // 
            // Grb_Values
            // 
            this.Grb_Values.Controls.Add(this.Ckb_GenerateSN);
            this.Grb_Values.Controls.Add(this.Ckb_Log_Path_TT);
            this.Grb_Values.Controls.Add(this.TB_UserTable);
            this.Grb_Values.Controls.Add(this.TB_ODBC_TT);
            this.Grb_Values.Controls.Add(this.CoB_Tables);
            this.Grb_Values.Controls.Add(this.TB_Log_Path_TT);
            this.Grb_Values.Controls.Add(this.TB_ODBC_EK);
            this.Grb_Values.Controls.Add(this.label3);
            this.Grb_Values.Controls.Add(this.label9);
            this.Grb_Values.Controls.Add(this.label5);
            this.Grb_Values.Controls.Add(this.label6);
            this.Grb_Values.Controls.Add(this.TB_DoCheckPass);
            this.Grb_Values.Controls.Add(this.label4);
            this.Grb_Values.Controls.Add(this.label8);
            this.Grb_Values.Controls.Add(this.TB_ProcNr);
            this.Grb_Values.Controls.Add(this.label1);
            this.Grb_Values.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grb_Values.Location = new System.Drawing.Point(0, 51);
            this.Grb_Values.Name = "Grb_Values";
            this.Grb_Values.Size = new System.Drawing.Size(600, 118);
            this.Grb_Values.TabIndex = 27;
            this.Grb_Values.TabStop = false;
            this.Grb_Values.Text = "groupBox1";
            // 
            // Ckb_GenerateSN
            // 
            this.Ckb_GenerateSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ckb_GenerateSN.AutoSize = true;
            this.Ckb_GenerateSN.Enabled = false;
            this.Ckb_GenerateSN.Location = new System.Drawing.Point(502, 40);
            this.Ckb_GenerateSN.Name = "Ckb_GenerateSN";
            this.Ckb_GenerateSN.Size = new System.Drawing.Size(93, 17);
            this.Ckb_GenerateSN.TabIndex = 24;
            this.Ckb_GenerateSN.Text = "Generate S/N";
            this.Ckb_GenerateSN.UseVisualStyleBackColor = true;
            // 
            // Ckb_Log_Path_TT
            // 
            this.Ckb_Log_Path_TT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ckb_Log_Path_TT.AutoSize = true;
            this.Ckb_Log_Path_TT.Enabled = false;
            this.Ckb_Log_Path_TT.Location = new System.Drawing.Point(502, 94);
            this.Ckb_Log_Path_TT.Name = "Ckb_Log_Path_TT";
            this.Ckb_Log_Path_TT.Size = new System.Drawing.Size(56, 17);
            this.Ckb_Log_Path_TT.TabIndex = 24;
            this.Ckb_Log_Path_TT.Text = "Active";
            this.Ckb_Log_Path_TT.UseVisualStyleBackColor = true;
            // 
            // TB_UserTable
            // 
            this.TB_UserTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_UserTable.Location = new System.Drawing.Point(366, 13);
            this.TB_UserTable.Name = "TB_UserTable";
            this.TB_UserTable.ReadOnly = true;
            this.TB_UserTable.Size = new System.Drawing.Size(228, 20);
            this.TB_UserTable.TabIndex = 20;
            // 
            // TB_Log_Path_TT
            // 
            this.TB_Log_Path_TT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Log_Path_TT.Location = new System.Drawing.Point(89, 90);
            this.TB_Log_Path_TT.Name = "TB_Log_Path_TT";
            this.TB_Log_Path_TT.ReadOnly = true;
            this.TB_Log_Path_TT.Size = new System.Drawing.Size(396, 20);
            this.TB_Log_Path_TT.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(280, 17);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "User Table:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 94);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Log TT:";
            // 
            // UC_Config_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grb_Values);
            this.Controls.Add(this.GrB_ODBC);
            this.Name = "UC_Config_DB";
            this.Size = new System.Drawing.Size(600, 169);
            this.Load += new System.EventHandler(this.UC_Config_DB_Load);
            this.GrB_ODBC.ResumeLayout(false);
            this.GrB_ODBC.PerformLayout();
            this.Grb_Values.ResumeLayout(false);
            this.Grb_Values.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TB_DoCheckPass;
        private System.Windows.Forms.TextBox TB_ProcNr;
        private System.Windows.Forms.TextBox TB_ODBC_Initial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CkB_DB_ProdType_Active;
        private System.Windows.Forms.ComboBox CoB_Tables;
        private System.Windows.Forms.TextBox TB_ODBC_TT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GrB_ODBC;
        private System.Windows.Forms.TextBox TB_ProdType_Table;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox Grb_Values;
        private System.Windows.Forms.CheckBox Ckb_GenerateSN;
        private System.Windows.Forms.CheckBox Ckb_Log_Path_TT;
        private System.Windows.Forms.TextBox TB_UserTable;
        private System.Windows.Forms.TextBox TB_Log_Path_TT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox TB_ODBC_EK;
    }
}
