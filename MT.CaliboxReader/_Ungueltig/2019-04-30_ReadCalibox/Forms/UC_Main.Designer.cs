namespace ReadCalibox.Forms
{
    partial class UC_Main
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
            this._FLP_Channels = new System.Windows.Forms.FlowLayoutPanel();
            this._Panel_Footer = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _FLP_Channels
            // 
            this._FLP_Channels.AutoScroll = true;
            this._FLP_Channels.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._FLP_Channels.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FLP_Channels.Location = new System.Drawing.Point(0, 0);
            this._FLP_Channels.Name = "_FLP_Channels";
            this._FLP_Channels.Size = new System.Drawing.Size(654, 526);
            this._FLP_Channels.TabIndex = 0;
            this._FLP_Channels.WrapContents = false;
            // 
            // _Panel_Footer
            // 
            this._Panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._Panel_Footer.Location = new System.Drawing.Point(0, 506);
            this._Panel_Footer.MaximumSize = new System.Drawing.Size(0, 20);
            this._Panel_Footer.MinimumSize = new System.Drawing.Size(0, 20);
            this._Panel_Footer.Name = "_Panel_Footer";
            this._Panel_Footer.Size = new System.Drawing.Size(654, 20);
            this._Panel_Footer.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 502);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(654, 4);
            this.panel3.TabIndex = 7;
            // 
            // UC_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this._Panel_Footer);
            this.Controls.Add(this._FLP_Channels);
            this.Name = "UC_Main";
            this.Size = new System.Drawing.Size(654, 526);
            this.Load += new System.EventHandler(this.UC_Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _FLP_Channels;
        private System.Windows.Forms.Panel _Panel_Footer;
        private System.Windows.Forms.Panel panel3;
    }
}
