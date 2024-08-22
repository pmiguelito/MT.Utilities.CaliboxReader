namespace ReadCalibox
{
    partial class UC_Betrieb
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
            this.FlowPan_Channels = new System.Windows.Forms.FlowLayoutPanel();
            this.PanMessage = new System.Windows.Forms.Panel();
            this._Lbl_ErrorMessage = new System.Windows.Forms.Label();
            this.PanSep1 = new System.Windows.Forms.Panel();
            this.PanMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlowPan_Channels
            // 
            this.FlowPan_Channels.AutoSize = true;
            this.FlowPan_Channels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FlowPan_Channels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowPan_Channels.Location = new System.Drawing.Point(0, 0);
            this.FlowPan_Channels.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.FlowPan_Channels.Name = "FlowPan_Channels";
            this.FlowPan_Channels.Size = new System.Drawing.Size(600, 352);
            this.FlowPan_Channels.TabIndex = 0;
            // 
            // PanMessage
            // 
            this.PanMessage.BackColor = System.Drawing.Color.White;
            this.PanMessage.Controls.Add(this._Lbl_ErrorMessage);
            this.PanMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanMessage.Location = new System.Drawing.Point(0, 355);
            this.PanMessage.MaximumSize = new System.Drawing.Size(0, 25);
            this.PanMessage.MinimumSize = new System.Drawing.Size(0, 25);
            this.PanMessage.Name = "PanMessage";
            this.PanMessage.Size = new System.Drawing.Size(600, 25);
            this.PanMessage.TabIndex = 1;
            // 
            // _Lbl_ErrorMessage
            // 
            this._Lbl_ErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Lbl_ErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Lbl_ErrorMessage.ForeColor = System.Drawing.Color.Red;
            this._Lbl_ErrorMessage.Location = new System.Drawing.Point(0, 0);
            this._Lbl_ErrorMessage.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_ErrorMessage.Name = "_Lbl_ErrorMessage";
            this._Lbl_ErrorMessage.Size = new System.Drawing.Size(600, 25);
            this._Lbl_ErrorMessage.TabIndex = 0;
            this._Lbl_ErrorMessage.Text = "_Lbl_Info";
            this._Lbl_ErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanSep1
            // 
            this.PanSep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(159)))));
            this.PanSep1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanSep1.Location = new System.Drawing.Point(0, 352);
            this.PanSep1.Name = "PanSep1";
            this.PanSep1.Size = new System.Drawing.Size(600, 3);
            this.PanSep1.TabIndex = 7;
            // 
            // UC_Betrieb
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.FlowPan_Channels);
            this.Controls.Add(this.PanSep1);
            this.Controls.Add(this.PanMessage);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UC_Betrieb";
            this.Size = new System.Drawing.Size(600, 380);
            this.Load += new System.EventHandler(this.UC_Main_Load);
            this.PanMessage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowPan_Channels;
        private System.Windows.Forms.Panel PanMessage;
        private System.Windows.Forms.Panel PanSep1;
        private System.Windows.Forms.Label _Lbl_ErrorMessage;
    }
}
