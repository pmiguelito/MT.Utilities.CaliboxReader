namespace ReadCalibox
{
    partial class UC_Debug
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
            this.CoB_COM = new System.Windows.Forms.ComboBox();
            this.CoB_BaudRate = new System.Windows.Forms.ComboBox();
            this._Lbl_BaudRate = new System.Windows.Forms.Label();
            this.CoB_CMD = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Tb_Info = new System.Windows.Forms.TextBox();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.Btn_StopRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CoB_COM
            // 
            this.CoB_COM.FormattingEnabled = true;
            this.CoB_COM.Location = new System.Drawing.Point(53, 4);
            this.CoB_COM.Margin = new System.Windows.Forms.Padding(0);
            this.CoB_COM.Name = "CoB_COM";
            this.CoB_COM.Size = new System.Drawing.Size(73, 21);
            this.CoB_COM.TabIndex = 2;
            this.CoB_COM.SelectedIndexChanged += new System.EventHandler(this.CoB_COM_SelectedIndexChanged);
            // 
            // CoB_BaudRate
            // 
            this.CoB_BaudRate.FormattingEnabled = true;
            this.CoB_BaudRate.Location = new System.Drawing.Point(161, 4);
            this.CoB_BaudRate.Margin = new System.Windows.Forms.Padding(0);
            this.CoB_BaudRate.Name = "CoB_BaudRate";
            this.CoB_BaudRate.Size = new System.Drawing.Size(73, 21);
            this.CoB_BaudRate.TabIndex = 3;
            this.CoB_BaudRate.SelectedIndexChanged += new System.EventHandler(this.CoB_BaudRate_SelectedIndexChanged);
            // 
            // _Lbl_BaudRate
            // 
            this._Lbl_BaudRate.AutoSize = true;
            this._Lbl_BaudRate.Location = new System.Drawing.Point(127, 8);
            this._Lbl_BaudRate.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_BaudRate.Name = "_Lbl_BaudRate";
            this._Lbl_BaudRate.Size = new System.Drawing.Size(32, 13);
            this._Lbl_BaudRate.TabIndex = 4;
            this._Lbl_BaudRate.Text = "Baud";
            // 
            // CoB_CMD
            // 
            this.CoB_CMD.FormattingEnabled = true;
            this.CoB_CMD.Location = new System.Drawing.Point(277, 4);
            this.CoB_CMD.Margin = new System.Windows.Forms.Padding(0);
            this.CoB_CMD.Name = "CoB_CMD";
            this.CoB_CMD.Size = new System.Drawing.Size(73, 21);
            this.CoB_CMD.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CMD";
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(368, 3);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(75, 23);
            this.Btn_Send.TabIndex = 5;
            this.Btn_Send.Text = "SEND";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Tb_Info
            // 
            this.Tb_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tb_Info.Location = new System.Drawing.Point(0, 31);
            this.Tb_Info.Multiline = true;
            this.Tb_Info.Name = "Tb_Info";
            this.Tb_Info.ReadOnly = true;
            this.Tb_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Tb_Info.Size = new System.Drawing.Size(784, 455);
            this.Tb_Info.TabIndex = 6;
            this.Tb_Info.TabStop = false;
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Clear.Location = new System.Drawing.Point(706, 3);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.Btn_Clear.TabIndex = 5;
            this.Btn_Clear.Text = "Clear";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // Btn_StopRead
            // 
            this.Btn_StopRead.Location = new System.Drawing.Point(449, 4);
            this.Btn_StopRead.Name = "Btn_StopRead";
            this.Btn_StopRead.Size = new System.Drawing.Size(75, 23);
            this.Btn_StopRead.TabIndex = 5;
            this.Btn_StopRead.Text = "STOP Read";
            this.Btn_StopRead.UseVisualStyleBackColor = true;
            this.Btn_StopRead.Click += new System.EventHandler(this.Btn_StopRead_Click);
            // 
            // UC_Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.Tb_Info);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_StopRead);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.CoB_COM);
            this.Controls.Add(this.CoB_CMD);
            this.Controls.Add(this.CoB_BaudRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._Lbl_BaudRate);
            this.Name = "UC_Debug";
            this.Size = new System.Drawing.Size(784, 489);
            this.Load += new System.EventHandler(this.UC_Debug_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CoB_COM;
        private System.Windows.Forms.ComboBox CoB_BaudRate;
        private System.Windows.Forms.Label _Lbl_BaudRate;
        private System.Windows.Forms.ComboBox CoB_CMD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.TextBox Tb_Info;
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.Button Btn_StopRead;
    }
}
