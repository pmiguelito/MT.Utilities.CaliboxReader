namespace ReadCalibox
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.PanHeader = new System.Windows.Forms.Panel();
            this._Lbl_SWvwersion = new System.Windows.Forms.Label();
            this._Lbl_SWname = new System.Windows.Forms.Label();
            this._Lbl_Company = new System.Windows.Forms.Label();
            this.PanFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._Btn_Exit = new System.Windows.Forms.Button();
            this._Btn_Admin = new System.Windows.Forms.Button();
            this.Btn_Debug = new System.Windows.Forms.Button();
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
            this.PanHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanHeader.Location = new System.Drawing.Point(0, 0);
            this.PanHeader.Margin = new System.Windows.Forms.Padding(0);
            this.PanHeader.MaximumSize = new System.Drawing.Size(0, 40);
            this.PanHeader.MinimumSize = new System.Drawing.Size(100, 40);
            this.PanHeader.Name = "PanHeader";
            this.PanHeader.Size = new System.Drawing.Size(746, 40);
            this.PanHeader.TabIndex = 0;
            // 
            // _Lbl_SWvwersion
            // 
            this._Lbl_SWvwersion.AutoSize = true;
            this._Lbl_SWvwersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._Lbl_SWvwersion.ForeColor = System.Drawing.Color.White;
            this._Lbl_SWvwersion.Location = new System.Drawing.Point(0, 27);
            this._Lbl_SWvwersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this._Lbl_SWvwersion.Name = "_Lbl_SWvwersion";
            this._Lbl_SWvwersion.Size = new System.Drawing.Size(45, 13);
            this._Lbl_SWvwersion.TabIndex = 0;
            this._Lbl_SWvwersion.Text = "Version:";
            this._Lbl_SWvwersion.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // _Lbl_SWname
            // 
            this._Lbl_SWname.AutoSize = true;
            this._Lbl_SWname.Dock = System.Windows.Forms.DockStyle.Top;
            this._Lbl_SWname.ForeColor = System.Drawing.Color.White;
            this._Lbl_SWname.Location = new System.Drawing.Point(0, 0);
            this._Lbl_SWname.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this._Lbl_SWname.Name = "_Lbl_SWname";
            this._Lbl_SWname.Size = new System.Drawing.Size(79, 13);
            this._Lbl_SWname.TabIndex = 0;
            this._Lbl_SWname.Text = "Calibox Reader";
            // 
            // _Lbl_Company
            // 
            this._Lbl_Company.Dock = System.Windows.Forms.DockStyle.Right;
            this._Lbl_Company.ForeColor = System.Drawing.Color.White;
            this._Lbl_Company.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Lbl_Company.Location = new System.Drawing.Point(486, 0);
            this._Lbl_Company.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._Lbl_Company.Name = "_Lbl_Company";
            this._Lbl_Company.Size = new System.Drawing.Size(260, 40);
            this._Lbl_Company.TabIndex = 0;
            this._Lbl_Company.Text = "METTLER TOLEDO";
            this._Lbl_Company.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PanFooter
            // 
            this.PanFooter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(159)))));
            this.PanFooter.Controls.Add(this.panel1);
            this.PanFooter.Controls.Add(this.Btn_Debug);
            this.PanFooter.Controls.Add(this._Btn_Config);
            this.PanFooter.Controls.Add(this._Btn_Betrieb);
            this.PanFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanFooter.Location = new System.Drawing.Point(0, 508);
            this.PanFooter.Margin = new System.Windows.Forms.Padding(0);
            this.PanFooter.MaximumSize = new System.Drawing.Size(0, 40);
            this.PanFooter.MinimumSize = new System.Drawing.Size(0, 40);
            this.PanFooter.Name = "PanFooter";
            this.PanFooter.Size = new System.Drawing.Size(746, 40);
            this.PanFooter.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._Btn_Exit);
            this.panel1.Controls.Add(this._Btn_Admin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(441, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 40);
            this.panel1.TabIndex = 3;
            // 
            // _Btn_Exit
            // 
            this._Btn_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_Exit.BackColor = System.Drawing.SystemColors.HotTrack;
            this._Btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this._Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._Btn_Exit.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Exit.Location = new System.Drawing.Point(180, 4);
            this._Btn_Exit.Margin = new System.Windows.Forms.Padding(0);
            this._Btn_Exit.Name = "_Btn_Exit";
            this._Btn_Exit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._Btn_Exit.Size = new System.Drawing.Size(119, 33);
            this._Btn_Exit.TabIndex = 1;
            this._Btn_Exit.Text = "Beenden";
            this._Btn_Exit.UseVisualStyleBackColor = false;
            this._Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // _Btn_Admin
            // 
            this._Btn_Admin.BackColor = System.Drawing.SystemColors.HotTrack;
            this._Btn_Admin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._Btn_Admin.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Admin.Location = new System.Drawing.Point(9, 4);
            this._Btn_Admin.Margin = new System.Windows.Forms.Padding(0);
            this._Btn_Admin.Name = "_Btn_Admin";
            this._Btn_Admin.Size = new System.Drawing.Size(119, 33);
            this._Btn_Admin.TabIndex = 2;
            this._Btn_Admin.Text = "Admin";
            this._Btn_Admin.UseVisualStyleBackColor = false;
            this._Btn_Admin.Click += new System.EventHandler(this.Btn_Admin_Click);
            // 
            // Btn_Debug
            // 
            this.Btn_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Debug.AutoSize = true;
            this.Btn_Debug.BackColor = System.Drawing.Color.Gray;
            this.Btn_Debug.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Debug.ForeColor = System.Drawing.SystemColors.Control;
            this.Btn_Debug.Location = new System.Drawing.Point(263, 4);
            this.Btn_Debug.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Debug.Name = "Btn_Debug";
            this.Btn_Debug.Size = new System.Drawing.Size(119, 33);
            this.Btn_Debug.TabIndex = 0;
            this.Btn_Debug.Text = "Debug";
            this.Btn_Debug.UseVisualStyleBackColor = false;
            this.Btn_Debug.Click += new System.EventHandler(this.Btn_Debug_Click);
            // 
            // _Btn_Config
            // 
            this._Btn_Config.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._Btn_Config.AutoSize = true;
            this._Btn_Config.BackColor = System.Drawing.Color.Gray;
            this._Btn_Config.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._Btn_Config.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Config.Location = new System.Drawing.Point(134, 4);
            this._Btn_Config.Margin = new System.Windows.Forms.Padding(0);
            this._Btn_Config.Name = "_Btn_Config";
            this._Btn_Config.Size = new System.Drawing.Size(119, 33);
            this._Btn_Config.TabIndex = 0;
            this._Btn_Config.Text = "Einstellungen";
            this._Btn_Config.UseVisualStyleBackColor = false;
            this._Btn_Config.Click += new System.EventHandler(this.Btn_Config_Click);
            // 
            // _Btn_Betrieb
            // 
            this._Btn_Betrieb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._Btn_Betrieb.AutoSize = true;
            this._Btn_Betrieb.BackColor = System.Drawing.Color.Gray;
            this._Btn_Betrieb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._Btn_Betrieb.ForeColor = System.Drawing.SystemColors.Control;
            this._Btn_Betrieb.Location = new System.Drawing.Point(5, 4);
            this._Btn_Betrieb.Margin = new System.Windows.Forms.Padding(0);
            this._Btn_Betrieb.Name = "_Btn_Betrieb";
            this._Btn_Betrieb.Size = new System.Drawing.Size(119, 33);
            this._Btn_Betrieb.TabIndex = 0;
            this._Btn_Betrieb.Text = "Betrieb";
            this._Btn_Betrieb.UseVisualStyleBackColor = false;
            this._Btn_Betrieb.Click += new System.EventHandler(this.Btn_Betrieb_Click);
            // 
            // PanWork
            // 
            this.PanWork.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PanWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanWork.Location = new System.Drawing.Point(0, 126);
            this.PanWork.Margin = new System.Windows.Forms.Padding(0);
            this.PanWork.Name = "PanWork";
            this.PanWork.Size = new System.Drawing.Size(746, 382);
            this.PanWork.TabIndex = 0;
            // 
            // PanInfo
            // 
            this.PanInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.PanInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanInfo.Location = new System.Drawing.Point(0, 43);
            this.PanInfo.Margin = new System.Windows.Forms.Padding(0);
            this.PanInfo.Name = "PanInfo";
            this.PanInfo.Size = new System.Drawing.Size(746, 80);
            this.PanInfo.TabIndex = 0;
            // 
            // PanSep1
            // 
            this.PanSep1.BackColor = System.Drawing.Color.White;
            this.PanSep1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanSep1.Location = new System.Drawing.Point(0, 40);
            this.PanSep1.Margin = new System.Windows.Forms.Padding(0);
            this.PanSep1.Name = "PanSep1";
            this.PanSep1.Size = new System.Drawing.Size(746, 3);
            this.PanSep1.TabIndex = 0;
            // 
            // PanSep2
            // 
            this.PanSep2.BackColor = System.Drawing.Color.White;
            this.PanSep2.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanSep2.Location = new System.Drawing.Point(0, 123);
            this.PanSep2.Name = "PanSep2";
            this.PanSep2.Size = new System.Drawing.Size(746, 3);
            this.PanSep2.TabIndex = 0;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._Btn_Exit;
            this.ClientSize = new System.Drawing.Size(746, 548);
            this.Controls.Add(this.PanWork);
            this.Controls.Add(this.PanSep2);
            this.Controls.Add(this.PanInfo);
            this.Controls.Add(this.PanSep1);
            this.Controls.Add(this.PanHeader);
            this.Controls.Add(this.PanFooter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.Text = "Calibox Reader";
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
        private System.Windows.Forms.Button Btn_Debug;
    }
}

