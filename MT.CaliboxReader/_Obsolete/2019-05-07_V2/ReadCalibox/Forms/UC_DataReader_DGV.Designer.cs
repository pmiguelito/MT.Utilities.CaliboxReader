namespace ReadCalibox
{
    partial class UC_DataReader_DGV
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
            this._DGV_Message = new System.Windows.Forms.DataGridView();
            this._Btn_Refresh = new System.Windows.Forms.Button();
            this._CkB_Autorefresh = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._DGV_Message)).BeginInit();
            this.SuspendLayout();
            // 
            // _DGV_Message
            // 
            this._DGV_Message.AllowUserToAddRows = false;
            this._DGV_Message.AllowUserToDeleteRows = false;
            this._DGV_Message.AllowUserToOrderColumns = true;
            this._DGV_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DGV_Message.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this._DGV_Message.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._DGV_Message.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._DGV_Message.Location = new System.Drawing.Point(0, 31);
            this._DGV_Message.Margin = new System.Windows.Forms.Padding(2);
            this._DGV_Message.MultiSelect = false;
            this._DGV_Message.Name = "_DGV_Message";
            this._DGV_Message.ReadOnly = true;
            this._DGV_Message.RowHeadersVisible = false;
            this._DGV_Message.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._DGV_Message.RowTemplate.Height = 24;
            this._DGV_Message.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._DGV_Message.Size = new System.Drawing.Size(445, 216);
            this._DGV_Message.TabIndex = 6;
            this._DGV_Message.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this._DGV_Message_DataError);
            // 
            // _Btn_Refresh
            // 
            this._Btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_Refresh.Location = new System.Drawing.Point(364, 3);
            this._Btn_Refresh.Name = "_Btn_Refresh";
            this._Btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this._Btn_Refresh.TabIndex = 7;
            this._Btn_Refresh.Text = "Refresh";
            this._Btn_Refresh.UseVisualStyleBackColor = true;
            this._Btn_Refresh.Click += new System.EventHandler(this._Btn_Refresh_Click);
            // 
            // _CkB_Autorefresh
            // 
            this._CkB_Autorefresh.Appearance = System.Windows.Forms.Appearance.Button;
            this._CkB_Autorefresh.AutoSize = true;
            this._CkB_Autorefresh.Location = new System.Drawing.Point(287, 3);
            this._CkB_Autorefresh.Name = "_CkB_Autorefresh";
            this._CkB_Autorefresh.Size = new System.Drawing.Size(79, 23);
            this._CkB_Autorefresh.TabIndex = 8;
            this._CkB_Autorefresh.Text = "Auto Refresh";
            this._CkB_Autorefresh.UseVisualStyleBackColor = true;
            this._CkB_Autorefresh.CheckedChanged += new System.EventHandler(this._CkB_Autorefresh_CheckedChanged);
            // 
            // UC_DataReader_DGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._CkB_Autorefresh);
            this.Controls.Add(this._Btn_Refresh);
            this.Controls.Add(this._DGV_Message);
            this.Name = "UC_DataReader_DGV";
            this.Size = new System.Drawing.Size(445, 247);
            this.Load += new System.EventHandler(this.UC_DataReader_DGV_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DGV_Message)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _DGV_Message;
        private System.Windows.Forms.Button _Btn_Refresh;
        private System.Windows.Forms.CheckBox _CkB_Autorefresh;
    }
}
