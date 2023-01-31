namespace ReadCalibox
{
    partial class UC_DataRead
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
            this._Btn_Start = new System.Windows.Forms.Button();
            this._Btn_Stop = new System.Windows.Forms.Button();
            this._Btn_Clear = new System.Windows.Forms.Button();
            this._Tb_Info = new System.Windows.Forms.TextBox();
            this._Lbl_COMcounter = new System.Windows.Forms.Label();
            this._TB_COMports = new System.Windows.Forms.TextBox();
            this._NUD_LinesBuffer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._NUD_LinesBuffer)).BeginInit();
            this.SuspendLayout();
            // 
            // _Btn_Start
            // 
            this._Btn_Start.AutoSize = true;
            this._Btn_Start.Location = new System.Drawing.Point(2, 0);
            this._Btn_Start.Margin = new System.Windows.Forms.Padding(2);
            this._Btn_Start.Name = "_Btn_Start";
            this._Btn_Start.Size = new System.Drawing.Size(50, 25);
            this._Btn_Start.TabIndex = 1;
            this._Btn_Start.Text = "Start";
            this._Btn_Start.UseVisualStyleBackColor = true;
            this._Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // _Btn_Stop
            // 
            this._Btn_Stop.AutoSize = true;
            this._Btn_Stop.Location = new System.Drawing.Point(56, 0);
            this._Btn_Stop.Margin = new System.Windows.Forms.Padding(2);
            this._Btn_Stop.Name = "_Btn_Stop";
            this._Btn_Stop.Size = new System.Drawing.Size(50, 25);
            this._Btn_Stop.TabIndex = 2;
            this._Btn_Stop.Text = "Stop";
            this._Btn_Stop.UseVisualStyleBackColor = true;
            this._Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // _Btn_Clear
            // 
            this._Btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_Clear.AutoSize = true;
            this._Btn_Clear.Location = new System.Drawing.Point(414, -1);
            this._Btn_Clear.Margin = new System.Windows.Forms.Padding(2);
            this._Btn_Clear.Name = "_Btn_Clear";
            this._Btn_Clear.Size = new System.Drawing.Size(50, 25);
            this._Btn_Clear.TabIndex = 2;
            this._Btn_Clear.Text = "Clear";
            this._Btn_Clear.UseVisualStyleBackColor = true;
            this._Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // _Tb_Info
            // 
            this._Tb_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Tb_Info.Location = new System.Drawing.Point(2, 29);
            this._Tb_Info.Margin = new System.Windows.Forms.Padding(2);
            this._Tb_Info.Multiline = true;
            this._Tb_Info.Name = "_Tb_Info";
            this._Tb_Info.ReadOnly = true;
            this._Tb_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._Tb_Info.Size = new System.Drawing.Size(396, 349);
            this._Tb_Info.TabIndex = 4;
            this._Tb_Info.WordWrap = false;
            // 
            // _Lbl_COMcounter
            // 
            this._Lbl_COMcounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_COMcounter.AutoSize = true;
            this._Lbl_COMcounter.Location = new System.Drawing.Point(403, 32);
            this._Lbl_COMcounter.Name = "_Lbl_COMcounter";
            this._Lbl_COMcounter.Size = new System.Drawing.Size(35, 13);
            this._Lbl_COMcounter.TabIndex = 6;
            this._Lbl_COMcounter.Text = "label1";
            // 
            // _TB_COMports
            // 
            this._TB_COMports.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TB_COMports.Location = new System.Drawing.Point(400, 48);
            this._TB_COMports.Multiline = true;
            this._TB_COMports.Name = "_TB_COMports";
            this._TB_COMports.ReadOnly = true;
            this._TB_COMports.Size = new System.Drawing.Size(69, 327);
            this._TB_COMports.TabIndex = 23;
            // 
            // _NUD_LinesBuffer
            // 
            this._NUD_LinesBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._NUD_LinesBuffer.Location = new System.Drawing.Point(338, 2);
            this._NUD_LinesBuffer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._NUD_LinesBuffer.Name = "_NUD_LinesBuffer";
            this._NUD_LinesBuffer.Size = new System.Drawing.Size(60, 20);
            this._NUD_LinesBuffer.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Lines Buffer:";
            // 
            // UC_DataRead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._NUD_LinesBuffer);
            this.Controls.Add(this._TB_COMports);
            this.Controls.Add(this._Lbl_COMcounter);
            this.Controls.Add(this._Tb_Info);
            this.Controls.Add(this._Btn_Clear);
            this.Controls.Add(this._Btn_Stop);
            this.Controls.Add(this._Btn_Start);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_DataRead";
            this.Size = new System.Drawing.Size(472, 378);
            ((System.ComponentModel.ISupportInitialize)(this._NUD_LinesBuffer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _Btn_Start;
        private System.Windows.Forms.Button _Btn_Stop;
        private System.Windows.Forms.Button _Btn_Clear;
        private System.Windows.Forms.TextBox _Tb_Info;
        private System.Windows.Forms.Label _Lbl_COMcounter;
        private System.Windows.Forms.TextBox _TB_COMports;
        private System.Windows.Forms.NumericUpDown _NUD_LinesBuffer;
        private System.Windows.Forms.Label label1;
    }
}
