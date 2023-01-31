namespace ConverterCalib
{
    partial class FrmMain
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.LblEnterprise = new System.Windows.Forms.Label();
            this.LblSwVersion = new System.Windows.Forms.Label();
            this.LblSwVersionTitel = new System.Windows.Forms.Label();
            this.LblSwTitle = new System.Windows.Forms.Label();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.TabCtrMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PanelHeader.SuspendLayout();
            this.PanelFooter.SuspendLayout();
            this.TabCtrMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.HotTrack;
            this.PanelHeader.Controls.Add(this.LblEnterprise);
            this.PanelHeader.Controls.Add(this.LblSwVersion);
            this.PanelHeader.Controls.Add(this.LblSwVersionTitel);
            this.PanelHeader.Controls.Add(this.LblSwTitle);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1164, 50);
            this.PanelHeader.TabIndex = 0;
            // 
            // LblEnterprise
            // 
            this.LblEnterprise.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblEnterprise.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEnterprise.ForeColor = System.Drawing.Color.White;
            this.LblEnterprise.Location = new System.Drawing.Point(808, 0);
            this.LblEnterprise.Name = "LblEnterprise";
            this.LblEnterprise.Size = new System.Drawing.Size(356, 50);
            this.LblEnterprise.TabIndex = 0;
            this.LblEnterprise.Text = "METTLER TOLEDO";
            this.LblEnterprise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblSwVersion
            // 
            this.LblSwVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblSwVersion.AutoSize = true;
            this.LblSwVersion.ForeColor = System.Drawing.Color.White;
            this.LblSwVersion.Location = new System.Drawing.Point(56, 30);
            this.LblSwVersion.Name = "LblSwVersion";
            this.LblSwVersion.Size = new System.Drawing.Size(71, 13);
            this.LblSwVersion.TabIndex = 0;
            this.LblSwVersion.Text = "LblSwVersion";
            // 
            // LblSwVersionTitel
            // 
            this.LblSwVersionTitel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblSwVersionTitel.AutoSize = true;
            this.LblSwVersionTitel.ForeColor = System.Drawing.Color.White;
            this.LblSwVersionTitel.Location = new System.Drawing.Point(8, 30);
            this.LblSwVersionTitel.Margin = new System.Windows.Forms.Padding(0);
            this.LblSwVersionTitel.Name = "LblSwVersionTitel";
            this.LblSwVersionTitel.Size = new System.Drawing.Size(45, 13);
            this.LblSwVersionTitel.TabIndex = 0;
            this.LblSwVersionTitel.Text = "Version:";
            // 
            // LblSwTitle
            // 
            this.LblSwTitle.AutoSize = true;
            this.LblSwTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSwTitle.ForeColor = System.Drawing.Color.White;
            this.LblSwTitle.Location = new System.Drawing.Point(6, 2);
            this.LblSwTitle.Margin = new System.Windows.Forms.Padding(0);
            this.LblSwTitle.Name = "LblSwTitle";
            this.LblSwTitle.Size = new System.Drawing.Size(121, 25);
            this.LblSwTitle.TabIndex = 0;
            this.LblSwTitle.Text = "LblSwTitle";
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.SystemColors.HotTrack;
            this.PanelFooter.Controls.Add(this.BtnExit);
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 812);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Size = new System.Drawing.Size(1164, 50);
            this.PanelFooter.TabIndex = 1;
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(1055, 10);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(100, 30);
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Text = "Beenden";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TabCtrMain
            // 
            this.TabCtrMain.Controls.Add(this.tabPage1);
            this.TabCtrMain.Controls.Add(this.tabPage2);
            this.TabCtrMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrMain.Location = new System.Drawing.Point(0, 50);
            this.TabCtrMain.Name = "TabCtrMain";
            this.TabCtrMain.SelectedIndex = 0;
            this.TabCtrMain.Size = new System.Drawing.Size(1164, 762);
            this.TabCtrMain.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1156, 736);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1156, 736);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 862);
            this.Controls.Add(this.TabCtrMain);
            this.Controls.Add(this.PanelFooter);
            this.Controls.Add(this.PanelHeader);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.PanelFooter.ResumeLayout(false);
            this.TabCtrMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Panel PanelFooter;
        private System.Windows.Forms.Label LblEnterprise;
        private System.Windows.Forms.Label LblSwVersion;
        private System.Windows.Forms.Label LblSwVersionTitel;
        private System.Windows.Forms.Label LblSwTitle;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.TabControl TabCtrMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

