namespace ReadCalibox
{
    partial class UC_Channel
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel_Header = new System.Windows.Forms.Panel();
            this._Label_CH = new System.Windows.Forms.Label();
            this.Panel_Sep1 = new System.Windows.Forms.Panel();
            this.Panel_ItemInfos = new System.Windows.Forms.Panel();
            this.ChB_Chart = new System.Windows.Forms.CheckBox();
            this.Chart_Measurement = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._Lbl_CalMode = new System.Windows.Forms.Label();
            this._Lbl_PassNo = new System.Windows.Forms.Label();
            this._Lbl_CalMode_txt = new System.Windows.Forms.Label();
            this._Lbl_ProdType = new System.Windows.Forms.Label();
            this._Lbl_PassNo_txt = new System.Windows.Forms.Label();
            this._Lbl_User = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._Lbl_Pdno = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._Lbl_Item = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._Lbl_TAGno = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._Lbl_TAGno_TXT = new System.Windows.Forms.Label();
            this.Panel_Sep2 = new System.Windows.Forms.Panel();
            this.Panel_Time = new System.Windows.Forms.Panel();
            this._Lbl_TimeEnd = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._Lbl_TimeStart = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Panel_Sep3 = new System.Windows.Forms.Panel();
            this.Panel_Info = new System.Windows.Forms.Panel();
            this.DGV_Calibration = new System.Windows.Forms.DataGridView();
            this.Tb_Info = new System.Windows.Forms.TextBox();
            this.Panel_Sep4 = new System.Windows.Forms.Panel();
            this.Panel_Button = new System.Windows.Forms.Panel();
            this._Btn_Start = new System.Windows.Forms.Button();
            this.ContextMenuChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTDDeviationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanVauesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refMeanValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTDDeviationErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Header.SuspendLayout();
            this.Panel_ItemInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Measurement)).BeginInit();
            this.Panel_Time.SuspendLayout();
            this.Panel_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Calibration)).BeginInit();
            this.Panel_Button.SuspendLayout();
            this.ContextMenuChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Header
            // 
            this.Panel_Header.Controls.Add(this._Label_CH);
            this.Panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Header.Location = new System.Drawing.Point(0, 0);
            this.Panel_Header.Name = "Panel_Header";
            this.Panel_Header.Size = new System.Drawing.Size(150, 25);
            this.Panel_Header.TabIndex = 1;
            // 
            // _Label_CH
            // 
            this._Label_CH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Label_CH.Location = new System.Drawing.Point(0, 0);
            this._Label_CH.Name = "_Label_CH";
            this._Label_CH.Size = new System.Drawing.Size(150, 25);
            this._Label_CH.TabIndex = 0;
            this._Label_CH.Text = "_Label_CH";
            this._Label_CH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_Sep1
            // 
            this.Panel_Sep1.BackColor = System.Drawing.SystemColors.Highlight;
            this.Panel_Sep1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Sep1.Location = new System.Drawing.Point(0, 25);
            this.Panel_Sep1.Name = "Panel_Sep1";
            this.Panel_Sep1.Size = new System.Drawing.Size(150, 3);
            this.Panel_Sep1.TabIndex = 2;
            // 
            // Panel_ItemInfos
            // 
            this.Panel_ItemInfos.Controls.Add(this.ChB_Chart);
            this.Panel_ItemInfos.Controls.Add(this.Chart_Measurement);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_CalMode);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_PassNo);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_CalMode_txt);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_ProdType);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_PassNo_txt);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_User);
            this.Panel_ItemInfos.Controls.Add(this.label5);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_Pdno);
            this.Panel_ItemInfos.Controls.Add(this.label4);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_Item);
            this.Panel_ItemInfos.Controls.Add(this.label3);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_TAGno);
            this.Panel_ItemInfos.Controls.Add(this.label2);
            this.Panel_ItemInfos.Controls.Add(this._Lbl_TAGno_TXT);
            this.Panel_ItemInfos.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ItemInfos.Location = new System.Drawing.Point(0, 28);
            this.Panel_ItemInfos.Name = "Panel_ItemInfos";
            this.Panel_ItemInfos.Size = new System.Drawing.Size(150, 110);
            this.Panel_ItemInfos.TabIndex = 0;
            // 
            // ChB_Chart
            // 
            this.ChB_Chart.AutoSize = true;
            this.ChB_Chart.Location = new System.Drawing.Point(132, 3);
            this.ChB_Chart.Name = "ChB_Chart";
            this.ChB_Chart.Size = new System.Drawing.Size(15, 14);
            this.ChB_Chart.TabIndex = 3;
            this.ChB_Chart.UseVisualStyleBackColor = true;
            this.ChB_Chart.CheckedChanged += new System.EventHandler(this.ChB_Chart_CheckedChanged);
            // 
            // Chart_Measurement
            // 
            this.Chart_Measurement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.Chart_Measurement.ChartAreas.Add(chartArea1);
            this.Chart_Measurement.ContextMenuStrip = this.ContextMenuChart;
            legend1.Name = "Legend1";
            this.Chart_Measurement.Legends.Add(legend1);
            this.Chart_Measurement.Location = new System.Drawing.Point(0, 23);
            this.Chart_Measurement.Name = "Chart_Measurement";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart_Measurement.Series.Add(series1);
            this.Chart_Measurement.Size = new System.Drawing.Size(150, 87);
            this.Chart_Measurement.TabIndex = 2;
            // 
            // _Lbl_CalMode
            // 
            this._Lbl_CalMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_CalMode.AutoSize = true;
            this._Lbl_CalMode.Location = new System.Drawing.Point(65, 92);
            this._Lbl_CalMode.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_CalMode.MaximumSize = new System.Drawing.Size(78, 13);
            this._Lbl_CalMode.MinimumSize = new System.Drawing.Size(78, 13);
            this._Lbl_CalMode.Name = "_Lbl_CalMode";
            this._Lbl_CalMode.Size = new System.Drawing.Size(78, 13);
            this._Lbl_CalMode.TabIndex = 1;
            this._Lbl_CalMode.Text = "_Lbl_CalMode";
            this._Lbl_CalMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_PassNo
            // 
            this._Lbl_PassNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_PassNo.AutoSize = true;
            this._Lbl_PassNo.Location = new System.Drawing.Point(65, 78);
            this._Lbl_PassNo.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_PassNo.MaximumSize = new System.Drawing.Size(78, 13);
            this._Lbl_PassNo.MinimumSize = new System.Drawing.Size(78, 13);
            this._Lbl_PassNo.Name = "_Lbl_PassNo";
            this._Lbl_PassNo.Size = new System.Drawing.Size(78, 13);
            this._Lbl_PassNo.TabIndex = 1;
            this._Lbl_PassNo.Text = "_Lbl_PassNo";
            this._Lbl_PassNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_CalMode_txt
            // 
            this._Lbl_CalMode_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Lbl_CalMode_txt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Lbl_CalMode_txt.Location = new System.Drawing.Point(0, 92);
            this._Lbl_CalMode_txt.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_CalMode_txt.Name = "_Lbl_CalMode_txt";
            this._Lbl_CalMode_txt.Size = new System.Drawing.Size(63, 14);
            this._Lbl_CalMode_txt.TabIndex = 1;
            this._Lbl_CalMode_txt.Text = "CalMode:";
            this._Lbl_CalMode_txt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_ProdType
            // 
            this._Lbl_ProdType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_ProdType.AutoSize = true;
            this._Lbl_ProdType.Location = new System.Drawing.Point(65, 64);
            this._Lbl_ProdType.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_ProdType.MaximumSize = new System.Drawing.Size(78, 13);
            this._Lbl_ProdType.MinimumSize = new System.Drawing.Size(78, 13);
            this._Lbl_ProdType.Name = "_Lbl_ProdType";
            this._Lbl_ProdType.Size = new System.Drawing.Size(78, 13);
            this._Lbl_ProdType.TabIndex = 1;
            this._Lbl_ProdType.Text = "_Lbl_ProdType";
            this._Lbl_ProdType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_PassNo_txt
            // 
            this._Lbl_PassNo_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Lbl_PassNo_txt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Lbl_PassNo_txt.Location = new System.Drawing.Point(0, 78);
            this._Lbl_PassNo_txt.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_PassNo_txt.Name = "_Lbl_PassNo_txt";
            this._Lbl_PassNo_txt.Size = new System.Drawing.Size(63, 14);
            this._Lbl_PassNo_txt.TabIndex = 1;
            this._Lbl_PassNo_txt.Text = "PassNo:";
            this._Lbl_PassNo_txt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_User
            // 
            this._Lbl_User.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_User.AutoSize = true;
            this._Lbl_User.Location = new System.Drawing.Point(65, 50);
            this._Lbl_User.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_User.MaximumSize = new System.Drawing.Size(78, 13);
            this._Lbl_User.MinimumSize = new System.Drawing.Size(78, 13);
            this._Lbl_User.Name = "_Lbl_User";
            this._Lbl_User.Size = new System.Drawing.Size(78, 13);
            this._Lbl_User.TabIndex = 1;
            this._Lbl_User.Text = "_Lbl_User";
            this._Lbl_User.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(0, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "Prod.Type:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_Pdno
            // 
            this._Lbl_Pdno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_Pdno.AutoSize = true;
            this._Lbl_Pdno.Location = new System.Drawing.Point(65, 36);
            this._Lbl_Pdno.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_Pdno.MaximumSize = new System.Drawing.Size(78, 13);
            this._Lbl_Pdno.MinimumSize = new System.Drawing.Size(78, 13);
            this._Lbl_Pdno.Name = "_Lbl_Pdno";
            this._Lbl_Pdno.Size = new System.Drawing.Size(78, 13);
            this._Lbl_Pdno.TabIndex = 1;
            this._Lbl_Pdno.Text = "_Lbl_Pdno";
            this._Lbl_Pdno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(0, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Prüfer:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_Item
            // 
            this._Lbl_Item.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_Item.AutoSize = true;
            this._Lbl_Item.Location = new System.Drawing.Point(65, 22);
            this._Lbl_Item.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_Item.MaximumSize = new System.Drawing.Size(78, 13);
            this._Lbl_Item.MinimumSize = new System.Drawing.Size(78, 13);
            this._Lbl_Item.Name = "_Lbl_Item";
            this._Lbl_Item.Size = new System.Drawing.Size(78, 13);
            this._Lbl_Item.TabIndex = 1;
            this._Lbl_Item.Text = "_Lbl_Item";
            this._Lbl_Item.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(0, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Auftr.Nr.:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_TAGno
            // 
            this._Lbl_TAGno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Lbl_TAGno.AutoSize = true;
            this._Lbl_TAGno.Location = new System.Drawing.Point(65, 4);
            this._Lbl_TAGno.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_TAGno.MaximumSize = new System.Drawing.Size(80, 26);
            this._Lbl_TAGno.MinimumSize = new System.Drawing.Size(80, 0);
            this._Lbl_TAGno.Name = "_Lbl_TAGno";
            this._Lbl_TAGno.Size = new System.Drawing.Size(80, 13);
            this._Lbl_TAGno.TabIndex = 1;
            this._Lbl_TAGno.Text = "_Lbl_TAGno";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(0, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Art.Nr.:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _Lbl_TAGno_TXT
            // 
            this._Lbl_TAGno_TXT.AutoSize = true;
            this._Lbl_TAGno_TXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Lbl_TAGno_TXT.Location = new System.Drawing.Point(0, 4);
            this._Lbl_TAGno_TXT.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_TAGno_TXT.Name = "_Lbl_TAGno_TXT";
            this._Lbl_TAGno_TXT.Size = new System.Drawing.Size(57, 13);
            this._Lbl_TAGno_TXT.TabIndex = 1;
            this._Lbl_TAGno_TXT.Text = "TAG-Nr.:";
            // 
            // Panel_Sep2
            // 
            this.Panel_Sep2.BackColor = System.Drawing.SystemColors.Highlight;
            this.Panel_Sep2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Sep2.Location = new System.Drawing.Point(0, 138);
            this.Panel_Sep2.Name = "Panel_Sep2";
            this.Panel_Sep2.Size = new System.Drawing.Size(150, 3);
            this.Panel_Sep2.TabIndex = 4;
            // 
            // Panel_Time
            // 
            this.Panel_Time.Controls.Add(this._Lbl_TimeEnd);
            this.Panel_Time.Controls.Add(this.label7);
            this.Panel_Time.Controls.Add(this._Lbl_TimeStart);
            this.Panel_Time.Controls.Add(this.label6);
            this.Panel_Time.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Time.Location = new System.Drawing.Point(0, 141);
            this.Panel_Time.Name = "Panel_Time";
            this.Panel_Time.Size = new System.Drawing.Size(150, 18);
            this.Panel_Time.TabIndex = 5;
            // 
            // _Lbl_TimeEnd
            // 
            this._Lbl_TimeEnd.AutoSize = true;
            this._Lbl_TimeEnd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Lbl_TimeEnd.Location = new System.Drawing.Point(109, 3);
            this._Lbl_TimeEnd.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_TimeEnd.Name = "_Lbl_TimeEnd";
            this._Lbl_TimeEnd.Size = new System.Drawing.Size(34, 13);
            this._Lbl_TimeEnd.TabIndex = 1;
            this._Lbl_TimeEnd.Text = "00:00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(77, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "End:";
            // 
            // _Lbl_TimeStart
            // 
            this._Lbl_TimeStart.AutoSize = true;
            this._Lbl_TimeStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Lbl_TimeStart.Location = new System.Drawing.Point(40, 3);
            this._Lbl_TimeStart.Margin = new System.Windows.Forms.Padding(0);
            this._Lbl_TimeStart.Name = "_Lbl_TimeStart";
            this._Lbl_TimeStart.Size = new System.Drawing.Size(34, 13);
            this._Lbl_TimeStart.TabIndex = 1;
            this._Lbl_TimeStart.Text = "00:00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(2, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Start:";
            // 
            // Panel_Sep3
            // 
            this.Panel_Sep3.BackColor = System.Drawing.SystemColors.Highlight;
            this.Panel_Sep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Sep3.Location = new System.Drawing.Point(0, 159);
            this.Panel_Sep3.Name = "Panel_Sep3";
            this.Panel_Sep3.Size = new System.Drawing.Size(150, 3);
            this.Panel_Sep3.TabIndex = 6;
            // 
            // Panel_Info
            // 
            this.Panel_Info.Controls.Add(this.DGV_Calibration);
            this.Panel_Info.Controls.Add(this.Tb_Info);
            this.Panel_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Info.Location = new System.Drawing.Point(0, 162);
            this.Panel_Info.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.Panel_Info.Name = "Panel_Info";
            this.Panel_Info.Size = new System.Drawing.Size(150, 155);
            this.Panel_Info.TabIndex = 7;
            // 
            // DGV_Calibration
            // 
            this.DGV_Calibration.AllowUserToAddRows = false;
            this.DGV_Calibration.AllowUserToDeleteRows = false;
            this.DGV_Calibration.AllowUserToResizeColumns = false;
            this.DGV_Calibration.AllowUserToResizeRows = false;
            this.DGV_Calibration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV_Calibration.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.DGV_Calibration.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Calibration.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Calibration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Calibration.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_Calibration.Location = new System.Drawing.Point(0, -3);
            this.DGV_Calibration.MultiSelect = false;
            this.DGV_Calibration.Name = "DGV_Calibration";
            this.DGV_Calibration.ReadOnly = true;
            this.DGV_Calibration.RowHeadersVisible = false;
            this.DGV_Calibration.RowHeadersWidth = 30;
            this.DGV_Calibration.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_Calibration.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Calibration.Size = new System.Drawing.Size(150, 100);
            this.DGV_Calibration.TabIndex = 1;
            // 
            // Tb_Info
            // 
            this.Tb_Info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tb_Info.Location = new System.Drawing.Point(0, 99);
            this.Tb_Info.Multiline = true;
            this.Tb_Info.Name = "Tb_Info";
            this.Tb_Info.ReadOnly = true;
            this.Tb_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Tb_Info.Size = new System.Drawing.Size(150, 55);
            this.Tb_Info.TabIndex = 0;
            this.Tb_Info.TabStop = false;
            // 
            // Panel_Sep4
            // 
            this.Panel_Sep4.BackColor = System.Drawing.SystemColors.Highlight;
            this.Panel_Sep4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Sep4.Location = new System.Drawing.Point(0, 317);
            this.Panel_Sep4.Name = "Panel_Sep4";
            this.Panel_Sep4.Size = new System.Drawing.Size(150, 3);
            this.Panel_Sep4.TabIndex = 8;
            // 
            // Panel_Button
            // 
            this.Panel_Button.Controls.Add(this._Btn_Start);
            this.Panel_Button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Button.Location = new System.Drawing.Point(0, 320);
            this.Panel_Button.Name = "Panel_Button";
            this.Panel_Button.Size = new System.Drawing.Size(150, 30);
            this.Panel_Button.TabIndex = 9;
            // 
            // _Btn_Start
            // 
            this._Btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._Btn_Start.Location = new System.Drawing.Point(29, 3);
            this._Btn_Start.Name = "_Btn_Start";
            this._Btn_Start.Size = new System.Drawing.Size(89, 24);
            this._Btn_Start.TabIndex = 0;
            this._Btn_Start.Text = "Start";
            this._Btn_Start.UseVisualStyleBackColor = true;
            this._Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // ContextMenuChart
            // 
            this.ContextMenuChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem});
            this.ContextMenuChart.Name = "ContextMenuChart";
            this.ContextMenuChart.Size = new System.Drawing.Size(181, 48);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sTDDeviationToolStripMenuItem,
            this.sTDDeviationErrorToolStripMenuItem,
            this.refValuesToolStripMenuItem,
            this.meanVauesToolStripMenuItem,
            this.errorValuesToolStripMenuItem,
            this.refMeanValuesToolStripMenuItem,
            this.allValuesToolStripMenuItem });
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem.Text = "Meas. Mode";
            // 
            // sTDDeviationToolStripMenuItem
            // 
            this.sTDDeviationToolStripMenuItem.Name = "sTDDeviationToolStripMenuItem";
            this.sTDDeviationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sTDDeviationToolStripMenuItem.Text = "STD Deviation";
            this.sTDDeviationToolStripMenuItem.Click += new System.EventHandler(this.sTDDeviationToolStripMenuItem_Click);
            // 
            // refValuesToolStripMenuItem
            // 
            this.refValuesToolStripMenuItem.Name = "refValuesToolStripMenuItem";
            this.refValuesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refValuesToolStripMenuItem.Text = "Ref. Values";
            this.refValuesToolStripMenuItem.Click += new System.EventHandler(this.refValuesToolStripMenuItem_Click);
            // 
            // meanVauesToolStripMenuItem
            // 
            this.meanVauesToolStripMenuItem.Name = "meanVauesToolStripMenuItem";
            this.meanVauesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.meanVauesToolStripMenuItem.Text = "Mean. Vaues";
            this.meanVauesToolStripMenuItem.Click += new System.EventHandler(this.meanVauesToolStripMenuItem_Click);
            // 
            // errorValuesToolStripMenuItem
            // 
            this.errorValuesToolStripMenuItem.Name = "errorValuesToolStripMenuItem";
            this.errorValuesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.errorValuesToolStripMenuItem.Text = "Error Values";
            this.errorValuesToolStripMenuItem.Click += new System.EventHandler(this.errorValuesToolStripMenuItem_Click);
            // 
            // refMeanValuesToolStripMenuItem
            // 
            this.refMeanValuesToolStripMenuItem.Name = "refMeanValuesToolStripMenuItem";
            this.refMeanValuesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refMeanValuesToolStripMenuItem.Text = "Ref. & Mean Values";
            this.refMeanValuesToolStripMenuItem.Click += new System.EventHandler(this.refMeanValuesToolStripMenuItem_Click);
            // 
            // allValuesToolStripMenuItem
            // 
            this.allValuesToolStripMenuItem.Name = "allValuesToolStripMenuItem";
            this.allValuesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.allValuesToolStripMenuItem.Text = "All Values";
            this.allValuesToolStripMenuItem.Click += new System.EventHandler(this.allValuesToolStripMenuItem_Click);
            // 
            // sTDDeviationErrorToolStripMenuItem
            // 
            this.sTDDeviationErrorToolStripMenuItem.Name = "sTDDeviationErrorToolStripMenuItem";
            this.sTDDeviationErrorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sTDDeviationErrorToolStripMenuItem.Text = "STD Deviation & Error";
            this.sTDDeviationErrorToolStripMenuItem.Click += new System.EventHandler(this.sTDDeviationErrorToolStripMenuItem_Click);
            // 
            // UC_Channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.Panel_Info);
            this.Controls.Add(this.Panel_Sep4);
            this.Controls.Add(this.Panel_Button);
            this.Controls.Add(this.Panel_Sep3);
            this.Controls.Add(this.Panel_Time);
            this.Controls.Add(this.Panel_Sep2);
            this.Controls.Add(this.Panel_ItemInfos);
            this.Controls.Add(this.Panel_Sep1);
            this.Controls.Add(this.Panel_Header);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximumSize = new System.Drawing.Size(150, 350);
            this.MinimumSize = new System.Drawing.Size(150, 350);
            this.Name = "UC_Channel";
            this.Size = new System.Drawing.Size(150, 350);
            this.Load += new System.EventHandler(this.UC_Channel_Load);
            this.Panel_Header.ResumeLayout(false);
            this.Panel_ItemInfos.ResumeLayout(false);
            this.Panel_ItemInfos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Measurement)).EndInit();
            this.Panel_Time.ResumeLayout(false);
            this.Panel_Time.PerformLayout();
            this.Panel_Info.ResumeLayout(false);
            this.Panel_Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Calibration)).EndInit();
            this.Panel_Button.ResumeLayout(false);
            this.ContextMenuChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Header;
        private System.Windows.Forms.Panel Panel_Sep1;
        private System.Windows.Forms.Panel Panel_ItemInfos;
        private System.Windows.Forms.Panel Panel_Sep2;
        private System.Windows.Forms.Panel Panel_Time;
        private System.Windows.Forms.Panel Panel_Sep3;
        private System.Windows.Forms.Panel Panel_Info;
        private System.Windows.Forms.Panel Panel_Sep4;
        private System.Windows.Forms.Panel Panel_Button;
        private System.Windows.Forms.Label _Label_CH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _Lbl_TAGno_TXT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button _Btn_Start;
        private System.Windows.Forms.TextBox Tb_Info;
        private System.Windows.Forms.Label _Lbl_ProdType;
        private System.Windows.Forms.Label _Lbl_User;
        private System.Windows.Forms.Label _Lbl_Pdno;
        private System.Windows.Forms.Label _Lbl_Item;
        private System.Windows.Forms.Label _Lbl_TAGno;
        private System.Windows.Forms.Label _Lbl_TimeStart;
        private System.Windows.Forms.Label _Lbl_TimeEnd;
        private System.Windows.Forms.DataGridView DGV_Calibration;
        private System.Windows.Forms.Label _Lbl_CalMode;
        private System.Windows.Forms.Label _Lbl_PassNo;
        private System.Windows.Forms.Label _Lbl_CalMode_txt;
        private System.Windows.Forms.Label _Lbl_PassNo_txt;
        private System.Windows.Forms.CheckBox ChB_Chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Measurement;
        private System.Windows.Forms.ContextMenuStrip ContextMenuChart;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTDDeviationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meanVauesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refMeanValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTDDeviationErrorToolStripMenuItem;
    }
}
