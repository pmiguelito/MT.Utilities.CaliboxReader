namespace ReadCalibox
{
    partial class Frm_Main
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
            { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.PanHeader = new System.Windows.Forms.Panel();
            this._Lbl_SWvwersion = new System.Windows.Forms.Label();
            this._Lbl_SWname = new System.Windows.Forms.Label();
            this._Lbl_Company = new System.Windows.Forms.Label();
            this.PanFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._Btn_Exit = new System.Windows.Forms.Button();
            this._Btn_Admin = new System.Windows.Forms.Button();
            this._Btn_Config = new System.Windows.Forms.Button();
            this._Btn_Betrieb = new System.Windows.Forms.Button();
            this.PanWork = new System.Windows.Forms.Panel();
            this.PanInfo = new System.Windows.Forms.Panel();
            this.PanSep1 = new System.Windows.Forms.Panel();
            this.PanSep2 = new System.Windows.Forms.Panel();
            this.PanHeader.SuspendLayout();
            this.PanFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanHeader
            // 
            this.PanHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(159)))));
            this.PanHeader.Controls.Add(this._Lbl_SWvwersion);
            this.PanHeader.Controls.Add(this._Lbl_SWname);
            this.PanHeader.Controls.Add(this._Lbl_Company);
            resources.ApplyResources(this.PanHeader, "PanHeader");
            this.PanHeader.Name = "PanHeader";
            // 
            // _Lbl_SWvwersion
            // 
            resources.ApplyResources(this._Lbl_SWvwersion, "_Lbl_SWvwersion");
            this._Lbl_SWvwersion.ForeColor = System.Drawing.Color.White;
            this._Lbl_SWvwersion.Name = "_Lbl_SWvwersion";
            // 
            // _Lbl_SWname
            // 
            resources.ApplyResources(this._Lbl_SWname, "_Lbl_SWname");
            this._Lbl_SWname.ForeColor = System.Drawing.Color.White;
            this._Lbl_SWname.Name = "_Lbl_SWname";
            // 
            // _Lbl_Company
            // 
            resources.ApplyResources(this._Lbl_Company, "_Lbl_Company");
            this._Lbl_Company.ForeColor = System.Drawing.Color.White;
            this._Lbl_Company.Name = "_Lbl_Company";
            // 
            // PanFooter
            // 
            resources.ApplyResources(this.PanFooter, "PanFooter");
            this.PanFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(159)))));
            this.PanFooter.Controls.Add(this.panel1);
            this.PanFooter.Controls.Add(this._Btn_Config);
            this.PanFooter.Controls.Add(this._Btn_Betrieb);
            this.PanFooter.Name = "PanFooter";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._Btn_Exit);
            this.panel1.Controls.Add(this._Btn_Admin);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // _Btn_Exit
            // 
            resources.ApplyResources(this._Btn_Exit, "_Btn_Exit");
            this._Btn_Exit.BackColor = System.Drawing.SystemColors.HotTrack;
            this._Btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this._Btn_Exit.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Exit.Name = "_Btn_Exit";
            this._Btn_Exit.UseVisualStyleBackColor = false;
            this._Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // _Btn_Admin
            // 
            this._Btn_Admin.BackColor = System.Drawing.SystemColors.HotTrack;
            resources.ApplyResources(this._Btn_Admin, "_Btn_Admin");
            this._Btn_Admin.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Admin.Name = "_Btn_Admin";
            this._Btn_Admin.UseVisualStyleBackColor = false;
            this._Btn_Admin.Click += new System.EventHandler(this.Btn_Admin_Click);
            // 
            // _Btn_Config
            // 
            resources.ApplyResources(this._Btn_Config, "_Btn_Config");
            this._Btn_Config.BackColor = System.Drawing.Color.Gray;
            this._Btn_Config.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Config.Name = "_Btn_Config";
            this._Btn_Config.UseVisualStyleBackColor = false;
            this._Btn_Config.Click += new System.EventHandler(this.Btn_Config_Click);
            // 
            // _Btn_Betrieb
            // 
            resources.ApplyResources(this._Btn_Betrieb, "_Btn_Betrieb");
            this._Btn_Betrieb.BackColor = System.Drawing.Color.Gray;
            this._Btn_Betrieb.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Betrieb.Name = "_Btn_Betrieb";
            this._Btn_Betrieb.UseVisualStyleBackColor = false;
            this._Btn_Betrieb.Click += new System.EventHandler(this.Btn_Betrieb_Click);
            // 
            // PanWork
            // 
            resources.ApplyResources(this.PanWork, "PanWork");
            this.PanWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PanWork.Name = "PanWork";
            // 
            // PanInfo
            // 
            this.PanInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            resources.ApplyResources(this.PanInfo, "PanInfo");
            this.PanInfo.Name = "PanInfo";
            // 
            // PanSep1
            // 
            this.PanSep1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.PanSep1, "PanSep1");
            this.PanSep1.Name = "PanSep1";
            // 
            // PanSep2
            // 
            this.PanSep2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.PanSep2, "PanSep2");
            this.PanSep2.Name = "PanSep2";
            // 
            // Frm_Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._Btn_Exit;
            this.Controls.Add(this.PanWork);
            this.Controls.Add(this.PanSep2);
            this.Controls.Add(this.PanInfo);
            this.Controls.Add(this.PanSep1);
            this.Controls.Add(this.PanHeader);
            this.Controls.Add(this.PanFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Frm_Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.PanHeader.ResumeLayout(false);
            this.PanHeader.PerformLayout();
            this.PanFooter.ResumeLayout(false);
            this.PanFooter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanHeader;
        private System.Windows.Forms.Panel PanFooter;
        private System.Windows.Forms.Panel PanWork;
        private System.Windows.Forms.Panel PanInfo;
        private System.Windows.Forms.Label _Lbl_SWvwersion;
        private System.Windows.Forms.Label _Lbl_Company;
        private System.Windows.Forms.Label _Lbl_SWname;
        private System.Windows.Forms.Button _Btn_Exit;
        private System.Windows.Forms.Button _Btn_Admin;
        private System.Windows.Forms.Button _Btn_Config;
        private System.Windows.Forms.Button _Btn_Betrieb;
        private System.Windows.Forms.Panel PanSep1;
        private System.Windows.Forms.Panel PanSep2;
        private System.Windows.Forms.Panel panel1;
    }
}

