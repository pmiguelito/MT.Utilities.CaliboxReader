namespace ConverterCalib
{
    partial class UcCalib
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
            this.CkbAutoNoStop = new System.Windows.Forms.CheckBox();
            this._GB_Sensor_Val = new System.Windows.Forms.GroupBox();
            this.TbSensorValues_NTC = new System.Windows.Forms.TextBox();
            this.TbSensorValues_PT20 = new System.Windows.Forms.TextBox();
            this.TbSensorValues_PT30 = new System.Windows.Forms.TextBox();
            this.TbSensorValues_Date = new System.Windows.Forms.TextBox();
            this.TbSensorValues_Desc = new System.Windows.Forms.TextBox();
            this._GB_Meas_Val = new System.Windows.Forms.GroupBox();
            this.LedRating_PT30 = new UserControls.LEDsingle();
            this.LedRating_PT20 = new UserControls.LEDsingle();
            this.LedRating_NTC = new UserControls.LEDsingle();
            this.TbMeasVal_NTC = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT20 = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT30 = new System.Windows.Forms.TextBox();
            this.TbMeasVal_Date = new System.Windows.Forms.TextBox();
            this._TB_Meas_Counter = new System.Windows.Forms.TextBox();
            this._GB_Meas_Temp = new System.Windows.Forms.GroupBox();
            this.TbMeasVal_NTC_Temp = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT20_Temp = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT30_Temp = new System.Windows.Forms.TextBox();
            this._GB_Values_Diff = new System.Windows.Forms.GroupBox();
            this.TbMeasVal_NTC_Max = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT20_Max = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TbMeasVal_NTC_Min = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT20_Min = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT30_Max = new System.Windows.Forms.TextBox();
            this.TbMeasVal_NTC_Diff = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT30_Min = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT20_Diff = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT30_Diff = new System.Windows.Forms.TextBox();
            this._GB_STDdev = new System.Windows.Forms.GroupBox();
            this.TbMeasVal_NTC_StdDev = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT20_StdDev = new System.Windows.Forms.TextBox();
            this.TbMeasVal_PT30_StdDev = new System.Windows.Forms.TextBox();
            this.CkbStartStop = new System.Windows.Forms.CheckBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this._Lbl_PT1000_30 = new System.Windows.Forms.Label();
            this._Lbl_PT1000_20 = new System.Windows.Forms.Label();
            this._Lbl_NTCoffset = new System.Windows.Forms.Label();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.NudRatingValues = new System.Windows.Forms.NumericUpDown();
            this.NudNvalues = new System.Windows.Forms.NumericUpDown();
            this.LblHEX = new System.Windows.Forms.Label();
            this.LblTdlPathInUse = new System.Windows.Forms.Label();
            this.BtnGetP16 = new System.Windows.Forms.Button();
            this.BtnCommTest = new System.Windows.Forms.Button();
            this.LedError = new UserControls.LEDsingle();
            this.BtnLoadPathTdl = new System.Windows.Forms.Button();
            this.TbPathTdl = new System.Windows.Forms.TextBox();
            this.LblPathTdl = new System.Windows.Forms.Label();
            this.LblDurationTxt = new System.Windows.Forms.Label();
            this.LblStatusTxt = new System.Windows.Forms.Label();
            this.DgvMeasValues = new System.Windows.Forms.DataGridView();
            this.LblPage2Title = new System.Windows.Forms.Label();
            this.LblPage2 = new System.Windows.Forms.Label();
            this.LblPage3Title = new System.Windows.Forms.Label();
            this.LblPage3 = new System.Windows.Forms.Label();
            this.LblPage14Title = new System.Windows.Forms.Label();
            this.LblPage14 = new System.Windows.Forms.Label();
            this.LblPage30Title = new System.Windows.Forms.Label();
            this.LblPage30 = new System.Windows.Forms.Label();
            this.BtnCalibValues = new System.Windows.Forms.Button();
            this.PanelMeasVal = new System.Windows.Forms.Panel();
            this.GrbLimitsUNTC = new System.Windows.Forms.GroupBox();
            this.GrbLimitsUNTCstdDev = new System.Windows.Forms.GroupBox();
            this.CkbLimitsUntcStdDevActive = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.TbLimitsUntcStdDevPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcStdDevSet = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcStdDevMin = new System.Windows.Forms.TextBox();
            this.GrbLimitsUNTCavg = new System.Windows.Forms.GroupBox();
            this.CkbLimitsUntcAvgActive = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.TbLimitsUntcAvgPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcAvgSet = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcAvgMin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CkbLimitsUntcMinMaxDiffActive = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.TbLimitsUntcMinMaxDiffPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcMinMaxDiffSet = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcMinMaxDiffMin = new System.Windows.Forms.TextBox();
            this.GrbLimitsUNTCvalue = new System.Windows.Forms.GroupBox();
            this.CkbLimitsUntcActive = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.TbLimitsUntcPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcSet = new System.Windows.Forms.TextBox();
            this.TbLimitsUntcMin = new System.Windows.Forms.TextBox();
            this.GrbLimitsTemp = new System.Windows.Forms.GroupBox();
            this.GrbLimitsTempStdDev = new System.Windows.Forms.GroupBox();
            this.CkbLimitsTempStdDevActive = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TbLimitsTempStdDevPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsTempStdDevSet = new System.Windows.Forms.TextBox();
            this.TbLimitsTempStdDevMin = new System.Windows.Forms.TextBox();
            this.GrbLimitsTempAVG = new System.Windows.Forms.GroupBox();
            this.CkbLimitsTempAvgActive = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TbLimitsTempAvgPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsTempAvgSet = new System.Windows.Forms.TextBox();
            this.TbLimitsTempAvgMin = new System.Windows.Forms.TextBox();
            this.GrbLimitsTempMinMaxDiff = new System.Windows.Forms.GroupBox();
            this.CkbLimitsTempMinMaxDiffActive = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.TbLimitsTempMinMaxDiffPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsTempMinMaxDiffSet = new System.Windows.Forms.TextBox();
            this.TbLimitsTempMinMaxDiffMin = new System.Windows.Forms.TextBox();
            this.GrbLimitsTempValue = new System.Windows.Forms.GroupBox();
            this.CkbLimitsTempActive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TbLimitsTempPlus = new System.Windows.Forms.TextBox();
            this.TbLimitsTempSet = new System.Windows.Forms.TextBox();
            this.TbLimitsTempMin = new System.Windows.Forms.TextBox();
            this.GrbMeasVal = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.CkbDebugging = new System.Windows.Forms.CheckBox();
            this.PanelLimits = new System.Windows.Forms.Panel();
            this._GB_Sensor_Val.SuspendLayout();
            this._GB_Meas_Val.SuspendLayout();
            this._GB_Meas_Temp.SuspendLayout();
            this._GB_Values_Diff.SuspendLayout();
            this._GB_STDdev.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudRatingValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudNvalues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMeasValues)).BeginInit();
            this.PanelMeasVal.SuspendLayout();
            this.GrbLimitsUNTC.SuspendLayout();
            this.GrbLimitsUNTCstdDev.SuspendLayout();
            this.GrbLimitsUNTCavg.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GrbLimitsUNTCvalue.SuspendLayout();
            this.GrbLimitsTemp.SuspendLayout();
            this.GrbLimitsTempStdDev.SuspendLayout();
            this.GrbLimitsTempAVG.SuspendLayout();
            this.GrbLimitsTempMinMaxDiff.SuspendLayout();
            this.GrbLimitsTempValue.SuspendLayout();
            this.GrbMeasVal.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelLimits.SuspendLayout();
            this.SuspendLayout();
            // 
            // CkbAutoNoStop
            // 
            this.CkbAutoNoStop.AutoSize = true;
            this.CkbAutoNoStop.Location = new System.Drawing.Point(884, 74);
            this.CkbAutoNoStop.Name = "CkbAutoNoStop";
            this.CkbAutoNoStop.Size = new System.Drawing.Size(70, 17);
            this.CkbAutoNoStop.TabIndex = 30;
            this.CkbAutoNoStop.Text = "AutoStop";
            this.CkbAutoNoStop.UseVisualStyleBackColor = true;
            // 
            // _GB_Sensor_Val
            // 
            this._GB_Sensor_Val.Controls.Add(this.TbSensorValues_NTC);
            this._GB_Sensor_Val.Controls.Add(this.TbSensorValues_PT20);
            this._GB_Sensor_Val.Controls.Add(this.TbSensorValues_PT30);
            this._GB_Sensor_Val.Controls.Add(this.TbSensorValues_Date);
            this._GB_Sensor_Val.Controls.Add(this.TbSensorValues_Desc);
            this._GB_Sensor_Val.Location = new System.Drawing.Point(107, 15);
            this._GB_Sensor_Val.Name = "_GB_Sensor_Val";
            this._GB_Sensor_Val.Size = new System.Drawing.Size(130, 130);
            this._GB_Sensor_Val.TabIndex = 29;
            this._GB_Sensor_Val.TabStop = false;
            this._GB_Sensor_Val.Text = "Sensor Values";
            // 
            // TbSensorValues_NTC
            // 
            this.TbSensorValues_NTC.Location = new System.Drawing.Point(8, 16);
            this.TbSensorValues_NTC.Name = "TbSensorValues_NTC";
            this.TbSensorValues_NTC.ReadOnly = true;
            this.TbSensorValues_NTC.Size = new System.Drawing.Size(120, 20);
            this.TbSensorValues_NTC.TabIndex = 2;
            this.TbSensorValues_NTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbSensorValues_PT20
            // 
            this.TbSensorValues_PT20.Location = new System.Drawing.Point(8, 36);
            this.TbSensorValues_PT20.Name = "TbSensorValues_PT20";
            this.TbSensorValues_PT20.ReadOnly = true;
            this.TbSensorValues_PT20.Size = new System.Drawing.Size(120, 20);
            this.TbSensorValues_PT20.TabIndex = 2;
            this.TbSensorValues_PT20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbSensorValues_PT30
            // 
            this.TbSensorValues_PT30.Location = new System.Drawing.Point(8, 56);
            this.TbSensorValues_PT30.Name = "TbSensorValues_PT30";
            this.TbSensorValues_PT30.ReadOnly = true;
            this.TbSensorValues_PT30.Size = new System.Drawing.Size(120, 20);
            this.TbSensorValues_PT30.TabIndex = 2;
            this.TbSensorValues_PT30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbSensorValues_Date
            // 
            this.TbSensorValues_Date.Location = new System.Drawing.Point(8, 76);
            this.TbSensorValues_Date.Name = "TbSensorValues_Date";
            this.TbSensorValues_Date.ReadOnly = true;
            this.TbSensorValues_Date.Size = new System.Drawing.Size(120, 20);
            this.TbSensorValues_Date.TabIndex = 2;
            this.TbSensorValues_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbSensorValues_Desc
            // 
            this.TbSensorValues_Desc.BackColor = System.Drawing.Color.LightYellow;
            this.TbSensorValues_Desc.Location = new System.Drawing.Point(8, 98);
            this.TbSensorValues_Desc.Name = "TbSensorValues_Desc";
            this.TbSensorValues_Desc.Size = new System.Drawing.Size(120, 20);
            this.TbSensorValues_Desc.TabIndex = 2;
            this.TbSensorValues_Desc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _GB_Meas_Val
            // 
            this._GB_Meas_Val.Controls.Add(this.LedRating_PT30);
            this._GB_Meas_Val.Controls.Add(this.LedRating_PT20);
            this._GB_Meas_Val.Controls.Add(this.LedRating_NTC);
            this._GB_Meas_Val.Controls.Add(this.TbMeasVal_NTC);
            this._GB_Meas_Val.Controls.Add(this.TbMeasVal_PT20);
            this._GB_Meas_Val.Controls.Add(this.TbMeasVal_PT30);
            this._GB_Meas_Val.Controls.Add(this.TbMeasVal_Date);
            this._GB_Meas_Val.Location = new System.Drawing.Point(243, 15);
            this._GB_Meas_Val.Name = "_GB_Meas_Val";
            this._GB_Meas_Val.Size = new System.Drawing.Size(151, 130);
            this._GB_Meas_Val.TabIndex = 28;
            this._GB_Meas_Val.TabStop = false;
            this._GB_Meas_Val.Text = "Meas Values";
            // 
            // LedRating_PT30
            // 
            this.LedRating_PT30.BackColor = System.Drawing.Color.Transparent;
            this.LedRating_PT30.Checked = false;
            this.LedRating_PT30.ColorOFF = System.Drawing.Color.Red;
            this.LedRating_PT30.Location = new System.Drawing.Point(130, 61);
            this.LedRating_PT30.Name = "LedRating_PT30";
            this.LedRating_PT30.Size = new System.Drawing.Size(15, 15);
            this.LedRating_PT30.TabIndex = 19;
            this.LedRating_PT30.Text = "leDsingle1";
            // 
            // LedRating_PT20
            // 
            this.LedRating_PT20.BackColor = System.Drawing.Color.Transparent;
            this.LedRating_PT20.Checked = false;
            this.LedRating_PT20.ColorOFF = System.Drawing.Color.Red;
            this.LedRating_PT20.Location = new System.Drawing.Point(130, 42);
            this.LedRating_PT20.Name = "LedRating_PT20";
            this.LedRating_PT20.Size = new System.Drawing.Size(15, 15);
            this.LedRating_PT20.TabIndex = 18;
            this.LedRating_PT20.Text = "leDsingle1";
            // 
            // LedRating_NTC
            // 
            this.LedRating_NTC.BackColor = System.Drawing.Color.Transparent;
            this.LedRating_NTC.Checked = false;
            this.LedRating_NTC.ColorOFF = System.Drawing.Color.Red;
            this.LedRating_NTC.Location = new System.Drawing.Point(130, 23);
            this.LedRating_NTC.Name = "LedRating_NTC";
            this.LedRating_NTC.Size = new System.Drawing.Size(15, 15);
            this.LedRating_NTC.TabIndex = 17;
            this.LedRating_NTC.Text = "leDsingle1";
            // 
            // TbMeasVal_NTC
            // 
            this.TbMeasVal_NTC.Location = new System.Drawing.Point(6, 20);
            this.TbMeasVal_NTC.Name = "TbMeasVal_NTC";
            this.TbMeasVal_NTC.ReadOnly = true;
            this.TbMeasVal_NTC.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_NTC.TabIndex = 2;
            this.TbMeasVal_NTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT20
            // 
            this.TbMeasVal_PT20.Location = new System.Drawing.Point(6, 39);
            this.TbMeasVal_PT20.Name = "TbMeasVal_PT20";
            this.TbMeasVal_PT20.ReadOnly = true;
            this.TbMeasVal_PT20.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_PT20.TabIndex = 2;
            this.TbMeasVal_PT20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT30
            // 
            this.TbMeasVal_PT30.Location = new System.Drawing.Point(6, 58);
            this.TbMeasVal_PT30.Name = "TbMeasVal_PT30";
            this.TbMeasVal_PT30.ReadOnly = true;
            this.TbMeasVal_PT30.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_PT30.TabIndex = 2;
            this.TbMeasVal_PT30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_Date
            // 
            this.TbMeasVal_Date.Location = new System.Drawing.Point(6, 79);
            this.TbMeasVal_Date.Name = "TbMeasVal_Date";
            this.TbMeasVal_Date.ReadOnly = true;
            this.TbMeasVal_Date.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_Date.TabIndex = 2;
            this.TbMeasVal_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _TB_Meas_Counter
            // 
            this._TB_Meas_Counter.Location = new System.Drawing.Point(138, 98);
            this._TB_Meas_Counter.Name = "_TB_Meas_Counter";
            this._TB_Meas_Counter.ReadOnly = true;
            this._TB_Meas_Counter.Size = new System.Drawing.Size(60, 20);
            this._TB_Meas_Counter.TabIndex = 19;
            this._TB_Meas_Counter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _GB_Meas_Temp
            // 
            this._GB_Meas_Temp.Controls.Add(this.TbMeasVal_NTC_Temp);
            this._GB_Meas_Temp.Controls.Add(this.TbMeasVal_PT20_Temp);
            this._GB_Meas_Temp.Controls.Add(this.TbMeasVal_PT30_Temp);
            this._GB_Meas_Temp.Location = new System.Drawing.Point(748, 15);
            this._GB_Meas_Temp.Name = "_GB_Meas_Temp";
            this._GB_Meas_Temp.Size = new System.Drawing.Size(130, 130);
            this._GB_Meas_Temp.TabIndex = 27;
            this._GB_Meas_Temp.TabStop = false;
            this._GB_Meas_Temp.Text = "Meas Temp";
            // 
            // TbMeasVal_NTC_Temp
            // 
            this.TbMeasVal_NTC_Temp.Location = new System.Drawing.Point(4, 20);
            this.TbMeasVal_NTC_Temp.Name = "TbMeasVal_NTC_Temp";
            this.TbMeasVal_NTC_Temp.ReadOnly = true;
            this.TbMeasVal_NTC_Temp.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_NTC_Temp.TabIndex = 2;
            this.TbMeasVal_NTC_Temp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT20_Temp
            // 
            this.TbMeasVal_PT20_Temp.Location = new System.Drawing.Point(4, 39);
            this.TbMeasVal_PT20_Temp.Name = "TbMeasVal_PT20_Temp";
            this.TbMeasVal_PT20_Temp.ReadOnly = true;
            this.TbMeasVal_PT20_Temp.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_PT20_Temp.TabIndex = 2;
            this.TbMeasVal_PT20_Temp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT30_Temp
            // 
            this.TbMeasVal_PT30_Temp.Location = new System.Drawing.Point(4, 58);
            this.TbMeasVal_PT30_Temp.Name = "TbMeasVal_PT30_Temp";
            this.TbMeasVal_PT30_Temp.ReadOnly = true;
            this.TbMeasVal_PT30_Temp.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_PT30_Temp.TabIndex = 2;
            this.TbMeasVal_PT30_Temp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _GB_Values_Diff
            // 
            this._GB_Values_Diff.Controls.Add(this.NudRatingValues);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_NTC_Max);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_PT20_Max);
            this._GB_Values_Diff.Controls.Add(this._TB_Meas_Counter);
            this._GB_Values_Diff.Controls.Add(this.label6);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_NTC_Min);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_PT20_Min);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_PT30_Max);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_NTC_Diff);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_PT30_Min);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_PT20_Diff);
            this._GB_Values_Diff.Controls.Add(this.TbMeasVal_PT30_Diff);
            this._GB_Values_Diff.Location = new System.Drawing.Point(400, 15);
            this._GB_Values_Diff.Name = "_GB_Values_Diff";
            this._GB_Values_Diff.Size = new System.Drawing.Size(206, 130);
            this._GB_Values_Diff.TabIndex = 24;
            this._GB_Values_Diff.TabStop = false;
            this._GB_Values_Diff.Text = "Values Differenz";
            // 
            // TbMeasVal_NTC_Max
            // 
            this.TbMeasVal_NTC_Max.Location = new System.Drawing.Point(138, 20);
            this.TbMeasVal_NTC_Max.Name = "TbMeasVal_NTC_Max";
            this.TbMeasVal_NTC_Max.ReadOnly = true;
            this.TbMeasVal_NTC_Max.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_NTC_Max.TabIndex = 2;
            this.TbMeasVal_NTC_Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT20_Max
            // 
            this.TbMeasVal_PT20_Max.Location = new System.Drawing.Point(138, 39);
            this.TbMeasVal_PT20_Max.Name = "TbMeasVal_PT20_Max";
            this.TbMeasVal_PT20_Max.ReadOnly = true;
            this.TbMeasVal_PT20_Max.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_PT20_Max.TabIndex = 2;
            this.TbMeasVal_PT20_Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Meas counter";
            // 
            // TbMeasVal_NTC_Min
            // 
            this.TbMeasVal_NTC_Min.Location = new System.Drawing.Point(72, 20);
            this.TbMeasVal_NTC_Min.Name = "TbMeasVal_NTC_Min";
            this.TbMeasVal_NTC_Min.ReadOnly = true;
            this.TbMeasVal_NTC_Min.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_NTC_Min.TabIndex = 2;
            this.TbMeasVal_NTC_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT20_Min
            // 
            this.TbMeasVal_PT20_Min.Location = new System.Drawing.Point(72, 39);
            this.TbMeasVal_PT20_Min.Name = "TbMeasVal_PT20_Min";
            this.TbMeasVal_PT20_Min.ReadOnly = true;
            this.TbMeasVal_PT20_Min.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_PT20_Min.TabIndex = 2;
            this.TbMeasVal_PT20_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT30_Max
            // 
            this.TbMeasVal_PT30_Max.Location = new System.Drawing.Point(138, 58);
            this.TbMeasVal_PT30_Max.Name = "TbMeasVal_PT30_Max";
            this.TbMeasVal_PT30_Max.ReadOnly = true;
            this.TbMeasVal_PT30_Max.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_PT30_Max.TabIndex = 2;
            this.TbMeasVal_PT30_Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_NTC_Diff
            // 
            this.TbMeasVal_NTC_Diff.Location = new System.Drawing.Point(6, 20);
            this.TbMeasVal_NTC_Diff.Name = "TbMeasVal_NTC_Diff";
            this.TbMeasVal_NTC_Diff.ReadOnly = true;
            this.TbMeasVal_NTC_Diff.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_NTC_Diff.TabIndex = 2;
            this.TbMeasVal_NTC_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT30_Min
            // 
            this.TbMeasVal_PT30_Min.Location = new System.Drawing.Point(72, 58);
            this.TbMeasVal_PT30_Min.Name = "TbMeasVal_PT30_Min";
            this.TbMeasVal_PT30_Min.ReadOnly = true;
            this.TbMeasVal_PT30_Min.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_PT30_Min.TabIndex = 2;
            this.TbMeasVal_PT30_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT20_Diff
            // 
            this.TbMeasVal_PT20_Diff.Location = new System.Drawing.Point(6, 39);
            this.TbMeasVal_PT20_Diff.Name = "TbMeasVal_PT20_Diff";
            this.TbMeasVal_PT20_Diff.ReadOnly = true;
            this.TbMeasVal_PT20_Diff.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_PT20_Diff.TabIndex = 2;
            this.TbMeasVal_PT20_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT30_Diff
            // 
            this.TbMeasVal_PT30_Diff.Location = new System.Drawing.Point(6, 58);
            this.TbMeasVal_PT30_Diff.Name = "TbMeasVal_PT30_Diff";
            this.TbMeasVal_PT30_Diff.ReadOnly = true;
            this.TbMeasVal_PT30_Diff.Size = new System.Drawing.Size(60, 20);
            this.TbMeasVal_PT30_Diff.TabIndex = 2;
            this.TbMeasVal_PT30_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _GB_STDdev
            // 
            this._GB_STDdev.Controls.Add(this.TbMeasVal_NTC_StdDev);
            this._GB_STDdev.Controls.Add(this.TbMeasVal_PT20_StdDev);
            this._GB_STDdev.Controls.Add(this.TbMeasVal_PT30_StdDev);
            this._GB_STDdev.Location = new System.Drawing.Point(612, 15);
            this._GB_STDdev.Name = "_GB_STDdev";
            this._GB_STDdev.Size = new System.Drawing.Size(130, 130);
            this._GB_STDdev.TabIndex = 26;
            this._GB_STDdev.TabStop = false;
            this._GB_STDdev.Text = "STD deviation";
            // 
            // TbMeasVal_NTC_StdDev
            // 
            this.TbMeasVal_NTC_StdDev.Location = new System.Drawing.Point(6, 20);
            this.TbMeasVal_NTC_StdDev.Name = "TbMeasVal_NTC_StdDev";
            this.TbMeasVal_NTC_StdDev.ReadOnly = true;
            this.TbMeasVal_NTC_StdDev.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_NTC_StdDev.TabIndex = 2;
            this.TbMeasVal_NTC_StdDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT20_StdDev
            // 
            this.TbMeasVal_PT20_StdDev.Location = new System.Drawing.Point(6, 39);
            this.TbMeasVal_PT20_StdDev.Name = "TbMeasVal_PT20_StdDev";
            this.TbMeasVal_PT20_StdDev.ReadOnly = true;
            this.TbMeasVal_PT20_StdDev.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_PT20_StdDev.TabIndex = 2;
            this.TbMeasVal_PT20_StdDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbMeasVal_PT30_StdDev
            // 
            this.TbMeasVal_PT30_StdDev.Location = new System.Drawing.Point(6, 58);
            this.TbMeasVal_PT30_StdDev.Name = "TbMeasVal_PT30_StdDev";
            this.TbMeasVal_PT30_StdDev.ReadOnly = true;
            this.TbMeasVal_PT30_StdDev.Size = new System.Drawing.Size(120, 20);
            this.TbMeasVal_PT30_StdDev.TabIndex = 2;
            this.TbMeasVal_PT30_StdDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CkbStartStop
            // 
            this.CkbStartStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.CkbStartStop.Location = new System.Drawing.Point(884, 19);
            this.CkbStartStop.Name = "CkbStartStop";
            this.CkbStartStop.Size = new System.Drawing.Size(200, 50);
            this.CkbStartStop.TabIndex = 23;
            this.CkbStartStop.Text = "Start";
            this.CkbStartStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CkbStartStop.UseVisualStyleBackColor = true;
            this.CkbStartStop.CheckedChanged += new System.EventHandler(this.CkbStartStop_CheckedChanged);
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(884, 97);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(200, 50);
            this.BtnReset.TabIndex = 22;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // _Lbl_PT1000_30
            // 
            this._Lbl_PT1000_30.AutoSize = true;
            this._Lbl_PT1000_30.Location = new System.Drawing.Point(9, 74);
            this._Lbl_PT1000_30.Name = "_Lbl_PT1000_30";
            this._Lbl_PT1000_30.Size = new System.Drawing.Size(71, 13);
            this._Lbl_PT1000_30.TabIndex = 16;
            this._Lbl_PT1000_30.Text = "PT1000 30°C";
            // 
            // _Lbl_PT1000_20
            // 
            this._Lbl_PT1000_20.AutoSize = true;
            this._Lbl_PT1000_20.Location = new System.Drawing.Point(9, 54);
            this._Lbl_PT1000_20.Name = "_Lbl_PT1000_20";
            this._Lbl_PT1000_20.Size = new System.Drawing.Size(71, 13);
            this._Lbl_PT1000_20.TabIndex = 14;
            this._Lbl_PT1000_20.Text = "PT1000 20°C";
            // 
            // _Lbl_NTCoffset
            // 
            this._Lbl_NTCoffset.AutoSize = true;
            this._Lbl_NTCoffset.Location = new System.Drawing.Point(9, 34);
            this._Lbl_NTCoffset.Name = "_Lbl_NTCoffset";
            this._Lbl_NTCoffset.Size = new System.Drawing.Size(98, 13);
            this._Lbl_NTCoffset.TabIndex = 12;
            this._Lbl_NTCoffset.Text = "NTC 22kOhm 25°C";
            // 
            // PanelHeader
            // 
            this.PanelHeader.Controls.Add(this.CkbDebugging);
            this.PanelHeader.Controls.Add(this.NudNvalues);
            this.PanelHeader.Controls.Add(this.LblHEX);
            this.PanelHeader.Controls.Add(this.LblTdlPathInUse);
            this.PanelHeader.Controls.Add(this.BtnGetP16);
            this.PanelHeader.Controls.Add(this.BtnCommTest);
            this.PanelHeader.Controls.Add(this.LedError);
            this.PanelHeader.Controls.Add(this.BtnLoadPathTdl);
            this.PanelHeader.Controls.Add(this.TbPathTdl);
            this.PanelHeader.Controls.Add(this.LblPathTdl);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.LblDurationTxt);
            this.PanelHeader.Controls.Add(this.LblStatusTxt);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1223, 62);
            this.PanelHeader.TabIndex = 33;
            // 
            // NudRatingValues
            // 
            this.NudRatingValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NudRatingValues.BackColor = System.Drawing.Color.LightYellow;
            this.NudRatingValues.Location = new System.Drawing.Point(138, 77);
            this.NudRatingValues.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NudRatingValues.Name = "NudRatingValues";
            this.NudRatingValues.Size = new System.Drawing.Size(60, 20);
            this.NudRatingValues.TabIndex = 16;
            this.NudRatingValues.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NudRatingValues.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // NudNvalues
            // 
            this.NudNvalues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NudNvalues.BackColor = System.Drawing.Color.LightYellow;
            this.NudNvalues.Location = new System.Drawing.Point(1174, 33);
            this.NudNvalues.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NudNvalues.Name = "NudNvalues";
            this.NudNvalues.Size = new System.Drawing.Size(46, 20);
            this.NudNvalues.TabIndex = 16;
            this.NudNvalues.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NudNvalues.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NudNvalues.ValueChanged += new System.EventHandler(this.NudNvalues_ValueChanged);
            // 
            // LblHEX
            // 
            this.LblHEX.AutoSize = true;
            this.LblHEX.Location = new System.Drawing.Point(351, 46);
            this.LblHEX.Name = "LblHEX";
            this.LblHEX.Size = new System.Drawing.Size(43, 13);
            this.LblHEX.TabIndex = 15;
            this.LblHEX.Text = "LblHEX";
            // 
            // LblTdlPathInUse
            // 
            this.LblTdlPathInUse.AutoSize = true;
            this.LblTdlPathInUse.Location = new System.Drawing.Point(351, 29);
            this.LblTdlPathInUse.Name = "LblTdlPathInUse";
            this.LblTdlPathInUse.Size = new System.Drawing.Size(86, 13);
            this.LblTdlPathInUse.TabIndex = 15;
            this.LblTdlPathInUse.Text = "LblTdlPathInUse";
            // 
            // BtnGetP16
            // 
            this.BtnGetP16.Location = new System.Drawing.Point(253, 33);
            this.BtnGetP16.Name = "BtnGetP16";
            this.BtnGetP16.Size = new System.Drawing.Size(75, 23);
            this.BtnGetP16.TabIndex = 14;
            this.BtnGetP16.Text = "Get P16";
            this.BtnGetP16.UseVisualStyleBackColor = true;
            this.BtnGetP16.Click += new System.EventHandler(this.BtnGetP16_Click);
            // 
            // BtnCommTest
            // 
            this.BtnCommTest.Location = new System.Drawing.Point(6, 32);
            this.BtnCommTest.Name = "BtnCommTest";
            this.BtnCommTest.Size = new System.Drawing.Size(75, 23);
            this.BtnCommTest.TabIndex = 14;
            this.BtnCommTest.Text = "Comm Test";
            this.BtnCommTest.UseVisualStyleBackColor = true;
            this.BtnCommTest.Click += new System.EventHandler(this.BtnCommTest_Click);
            // 
            // LedError
            // 
            this.LedError.BackColor = System.Drawing.Color.Transparent;
            this.LedError.Checked = true;
            this.LedError.ColorON = System.Drawing.Color.Red;
            this.LedError.Location = new System.Drawing.Point(99, 30);
            this.LedError.Name = "LedError";
            this.LedError.Size = new System.Drawing.Size(25, 25);
            this.LedError.TabIndex = 13;
            this.LedError.Text = "LedError";
            // 
            // BtnLoadPathTdl
            // 
            this.BtnLoadPathTdl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLoadPathTdl.Location = new System.Drawing.Point(1145, 5);
            this.BtnLoadPathTdl.Name = "BtnLoadPathTdl";
            this.BtnLoadPathTdl.Size = new System.Drawing.Size(75, 23);
            this.BtnLoadPathTdl.TabIndex = 2;
            this.BtnLoadPathTdl.Text = "Load";
            this.BtnLoadPathTdl.UseVisualStyleBackColor = true;
            this.BtnLoadPathTdl.Click += new System.EventHandler(this.BtnLoadPathTdl_Click);
            // 
            // TbPathTdl
            // 
            this.TbPathTdl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbPathTdl.BackColor = System.Drawing.Color.LightYellow;
            this.TbPathTdl.Location = new System.Drawing.Point(41, 7);
            this.TbPathTdl.Name = "TbPathTdl";
            this.TbPathTdl.Size = new System.Drawing.Size(1098, 20);
            this.TbPathTdl.TabIndex = 1;
            // 
            // LblPathTdl
            // 
            this.LblPathTdl.AutoSize = true;
            this.LblPathTdl.Location = new System.Drawing.Point(3, 10);
            this.LblPathTdl.Name = "LblPathTdl";
            this.LblPathTdl.Size = new System.Drawing.Size(32, 13);
            this.LblPathTdl.TabIndex = 0;
            this.LblPathTdl.Text = "Path:";
            // 
            // LblDurationTxt
            // 
            this.LblDurationTxt.AutoSize = true;
            this.LblDurationTxt.Location = new System.Drawing.Point(146, 47);
            this.LblDurationTxt.Name = "LblDurationTxt";
            this.LblDurationTxt.Size = new System.Drawing.Size(76, 13);
            this.LblDurationTxt.TabIndex = 12;
            this.LblDurationTxt.Text = "LblDurationTxt";
            // 
            // LblStatusTxt
            // 
            this.LblStatusTxt.AutoSize = true;
            this.LblStatusTxt.Location = new System.Drawing.Point(146, 30);
            this.LblStatusTxt.Name = "LblStatusTxt";
            this.LblStatusTxt.Size = new System.Drawing.Size(66, 13);
            this.LblStatusTxt.TabIndex = 12;
            this.LblStatusTxt.Text = "LblStatusTxt";
            // 
            // DgvMeasValues
            // 
            this.DgvMeasValues.AllowUserToAddRows = false;
            this.DgvMeasValues.AllowUserToDeleteRows = false;
            this.DgvMeasValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMeasValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.DgvMeasValues.Location = new System.Drawing.Point(0, 62);
            this.DgvMeasValues.Name = "DgvMeasValues";
            this.DgvMeasValues.ReadOnly = true;
            this.DgvMeasValues.Size = new System.Drawing.Size(1223, 85);
            this.DgvMeasValues.TabIndex = 34;
            // 
            // LblPage2Title
            // 
            this.LblPage2Title.AutoSize = true;
            this.LblPage2Title.Location = new System.Drawing.Point(91, 3);
            this.LblPage2Title.Name = "LblPage2Title";
            this.LblPage2Title.Size = new System.Drawing.Size(44, 13);
            this.LblPage2Title.TabIndex = 12;
            this.LblPage2Title.Text = "Page 2:";
            // 
            // LblPage2
            // 
            this.LblPage2.AutoSize = true;
            this.LblPage2.Location = new System.Drawing.Point(141, 3);
            this.LblPage2.Name = "LblPage2";
            this.LblPage2.Size = new System.Drawing.Size(52, 13);
            this.LblPage2.TabIndex = 12;
            this.LblPage2.Text = "LblPage2";
            // 
            // LblPage3Title
            // 
            this.LblPage3Title.AutoSize = true;
            this.LblPage3Title.Location = new System.Drawing.Point(91, 16);
            this.LblPage3Title.Name = "LblPage3Title";
            this.LblPage3Title.Size = new System.Drawing.Size(44, 13);
            this.LblPage3Title.TabIndex = 12;
            this.LblPage3Title.Text = "Page 3:";
            // 
            // LblPage3
            // 
            this.LblPage3.AutoSize = true;
            this.LblPage3.Location = new System.Drawing.Point(141, 16);
            this.LblPage3.Name = "LblPage3";
            this.LblPage3.Size = new System.Drawing.Size(52, 13);
            this.LblPage3.TabIndex = 12;
            this.LblPage3.Text = "LblPage3";
            // 
            // LblPage14Title
            // 
            this.LblPage14Title.AutoSize = true;
            this.LblPage14Title.Location = new System.Drawing.Point(91, 29);
            this.LblPage14Title.Name = "LblPage14Title";
            this.LblPage14Title.Size = new System.Drawing.Size(50, 13);
            this.LblPage14Title.TabIndex = 12;
            this.LblPage14Title.Text = "Page 14:";
            // 
            // LblPage14
            // 
            this.LblPage14.AutoSize = true;
            this.LblPage14.Location = new System.Drawing.Point(141, 29);
            this.LblPage14.Name = "LblPage14";
            this.LblPage14.Size = new System.Drawing.Size(58, 13);
            this.LblPage14.TabIndex = 12;
            this.LblPage14.Text = "LblPage14";
            // 
            // LblPage30Title
            // 
            this.LblPage30Title.AutoSize = true;
            this.LblPage30Title.Location = new System.Drawing.Point(91, 42);
            this.LblPage30Title.Name = "LblPage30Title";
            this.LblPage30Title.Size = new System.Drawing.Size(50, 13);
            this.LblPage30Title.TabIndex = 12;
            this.LblPage30Title.Text = "Page 30:";
            // 
            // LblPage30
            // 
            this.LblPage30.AutoSize = true;
            this.LblPage30.Location = new System.Drawing.Point(141, 42);
            this.LblPage30.Name = "LblPage30";
            this.LblPage30.Size = new System.Drawing.Size(58, 13);
            this.LblPage30.TabIndex = 12;
            this.LblPage30.Text = "LblPage30";
            // 
            // BtnCalibValues
            // 
            this.BtnCalibValues.Location = new System.Drawing.Point(6, 3);
            this.BtnCalibValues.Name = "BtnCalibValues";
            this.BtnCalibValues.Size = new System.Drawing.Size(75, 52);
            this.BtnCalibValues.TabIndex = 14;
            this.BtnCalibValues.Text = "Calib Values";
            this.BtnCalibValues.UseVisualStyleBackColor = true;
            this.BtnCalibValues.Click += new System.EventHandler(this.BtnCalibValues_Click);
            // 
            // PanelMeasVal
            // 
            this.PanelMeasVal.Controls.Add(this.GrbMeasVal);
            this.PanelMeasVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelMeasVal.Location = new System.Drawing.Point(0, 207);
            this.PanelMeasVal.Name = "PanelMeasVal";
            this.PanelMeasVal.Size = new System.Drawing.Size(1223, 156);
            this.PanelMeasVal.TabIndex = 35;
            // 
            // GrbLimitsUNTC
            // 
            this.GrbLimitsUNTC.Controls.Add(this.GrbLimitsUNTCstdDev);
            this.GrbLimitsUNTC.Controls.Add(this.GrbLimitsUNTCavg);
            this.GrbLimitsUNTC.Controls.Add(this.groupBox2);
            this.GrbLimitsUNTC.Controls.Add(this.GrbLimitsUNTCvalue);
            this.GrbLimitsUNTC.Location = new System.Drawing.Point(551, 6);
            this.GrbLimitsUNTC.Name = "GrbLimitsUNTC";
            this.GrbLimitsUNTC.Size = new System.Drawing.Size(533, 156);
            this.GrbLimitsUNTC.TabIndex = 0;
            this.GrbLimitsUNTC.TabStop = false;
            this.GrbLimitsUNTC.Text = "Limits UNTC";
            // 
            // GrbLimitsUNTCstdDev
            // 
            this.GrbLimitsUNTCstdDev.Controls.Add(this.CkbLimitsUntcStdDevActive);
            this.GrbLimitsUNTCstdDev.Controls.Add(this.label17);
            this.GrbLimitsUNTCstdDev.Controls.Add(this.label18);
            this.GrbLimitsUNTCstdDev.Controls.Add(this.label19);
            this.GrbLimitsUNTCstdDev.Controls.Add(this.TbLimitsUntcStdDevPlus);
            this.GrbLimitsUNTCstdDev.Controls.Add(this.TbLimitsUntcStdDevSet);
            this.GrbLimitsUNTCstdDev.Controls.Add(this.TbLimitsUntcStdDevMin);
            this.GrbLimitsUNTCstdDev.Location = new System.Drawing.Point(401, 19);
            this.GrbLimitsUNTCstdDev.Name = "GrbLimitsUNTCstdDev";
            this.GrbLimitsUNTCstdDev.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsUNTCstdDev.TabIndex = 31;
            this.GrbLimitsUNTCstdDev.TabStop = false;
            this.GrbLimitsUNTCstdDev.Text = "StdDev";
            // 
            // CkbLimitsUntcStdDevActive
            // 
            this.CkbLimitsUntcStdDevActive.AutoSize = true;
            this.CkbLimitsUntcStdDevActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsUntcStdDevActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsUntcStdDevActive.Name = "CkbLimitsUntcStdDevActive";
            this.CkbLimitsUntcStdDevActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsUntcStdDevActive.TabIndex = 3;
            this.CkbLimitsUntcStdDevActive.Text = "Active";
            this.CkbLimitsUntcStdDevActive.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Set";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 94);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Plus";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(24, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Min";
            // 
            // TbLimitsUntcStdDevPlus
            // 
            this.TbLimitsUntcStdDevPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcStdDevPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsUntcStdDevPlus.Name = "TbLimitsUntcStdDevPlus";
            this.TbLimitsUntcStdDevPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcStdDevPlus.TabIndex = 2;
            this.TbLimitsUntcStdDevPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcStdDevSet
            // 
            this.TbLimitsUntcStdDevSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcStdDevSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsUntcStdDevSet.Name = "TbLimitsUntcStdDevSet";
            this.TbLimitsUntcStdDevSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcStdDevSet.TabIndex = 2;
            this.TbLimitsUntcStdDevSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcStdDevMin
            // 
            this.TbLimitsUntcStdDevMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcStdDevMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsUntcStdDevMin.Name = "TbLimitsUntcStdDevMin";
            this.TbLimitsUntcStdDevMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcStdDevMin.TabIndex = 2;
            this.TbLimitsUntcStdDevMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbLimitsUNTCavg
            // 
            this.GrbLimitsUNTCavg.Controls.Add(this.CkbLimitsUntcAvgActive);
            this.GrbLimitsUNTCavg.Controls.Add(this.label20);
            this.GrbLimitsUNTCavg.Controls.Add(this.label21);
            this.GrbLimitsUNTCavg.Controls.Add(this.label22);
            this.GrbLimitsUNTCavg.Controls.Add(this.TbLimitsUntcAvgPlus);
            this.GrbLimitsUNTCavg.Controls.Add(this.TbLimitsUntcAvgSet);
            this.GrbLimitsUNTCavg.Controls.Add(this.TbLimitsUntcAvgMin);
            this.GrbLimitsUNTCavg.Location = new System.Drawing.Point(269, 19);
            this.GrbLimitsUNTCavg.Name = "GrbLimitsUNTCavg";
            this.GrbLimitsUNTCavg.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsUNTCavg.TabIndex = 31;
            this.GrbLimitsUNTCavg.TabStop = false;
            this.GrbLimitsUNTCavg.Text = "AVG";
            // 
            // CkbLimitsUntcAvgActive
            // 
            this.CkbLimitsUntcAvgActive.AutoSize = true;
            this.CkbLimitsUntcAvgActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsUntcAvgActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsUntcAvgActive.Name = "CkbLimitsUntcAvgActive";
            this.CkbLimitsUntcAvgActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsUntcAvgActive.TabIndex = 3;
            this.CkbLimitsUntcAvgActive.Text = "Active";
            this.CkbLimitsUntcAvgActive.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 50);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Set";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 94);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(27, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Plus";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 72);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(24, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Min";
            // 
            // TbLimitsUntcAvgPlus
            // 
            this.TbLimitsUntcAvgPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcAvgPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsUntcAvgPlus.Name = "TbLimitsUntcAvgPlus";
            this.TbLimitsUntcAvgPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcAvgPlus.TabIndex = 2;
            this.TbLimitsUntcAvgPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcAvgSet
            // 
            this.TbLimitsUntcAvgSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcAvgSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsUntcAvgSet.Name = "TbLimitsUntcAvgSet";
            this.TbLimitsUntcAvgSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcAvgSet.TabIndex = 2;
            this.TbLimitsUntcAvgSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcAvgMin
            // 
            this.TbLimitsUntcAvgMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcAvgMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsUntcAvgMin.Name = "TbLimitsUntcAvgMin";
            this.TbLimitsUntcAvgMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcAvgMin.TabIndex = 2;
            this.TbLimitsUntcAvgMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CkbLimitsUntcMinMaxDiffActive);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.TbLimitsUntcMinMaxDiffPlus);
            this.groupBox2.Controls.Add(this.TbLimitsUntcMinMaxDiffSet);
            this.groupBox2.Controls.Add(this.TbLimitsUntcMinMaxDiffMin);
            this.groupBox2.Location = new System.Drawing.Point(137, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(124, 126);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MinMaxDiff";
            // 
            // CkbLimitsUntcMinMaxDiffActive
            // 
            this.CkbLimitsUntcMinMaxDiffActive.AutoSize = true;
            this.CkbLimitsUntcMinMaxDiffActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsUntcMinMaxDiffActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsUntcMinMaxDiffActive.Name = "CkbLimitsUntcMinMaxDiffActive";
            this.CkbLimitsUntcMinMaxDiffActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsUntcMinMaxDiffActive.TabIndex = 3;
            this.CkbLimitsUntcMinMaxDiffActive.Text = "Active";
            this.CkbLimitsUntcMinMaxDiffActive.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 50);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(23, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Set";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 94);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(27, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "Plus";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 72);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(24, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Min";
            // 
            // TbLimitsUntcMinMaxDiffPlus
            // 
            this.TbLimitsUntcMinMaxDiffPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcMinMaxDiffPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsUntcMinMaxDiffPlus.Name = "TbLimitsUntcMinMaxDiffPlus";
            this.TbLimitsUntcMinMaxDiffPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcMinMaxDiffPlus.TabIndex = 2;
            this.TbLimitsUntcMinMaxDiffPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcMinMaxDiffSet
            // 
            this.TbLimitsUntcMinMaxDiffSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcMinMaxDiffSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsUntcMinMaxDiffSet.Name = "TbLimitsUntcMinMaxDiffSet";
            this.TbLimitsUntcMinMaxDiffSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcMinMaxDiffSet.TabIndex = 2;
            this.TbLimitsUntcMinMaxDiffSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcMinMaxDiffMin
            // 
            this.TbLimitsUntcMinMaxDiffMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcMinMaxDiffMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsUntcMinMaxDiffMin.Name = "TbLimitsUntcMinMaxDiffMin";
            this.TbLimitsUntcMinMaxDiffMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcMinMaxDiffMin.TabIndex = 2;
            this.TbLimitsUntcMinMaxDiffMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbLimitsUNTCvalue
            // 
            this.GrbLimitsUNTCvalue.Controls.Add(this.CkbLimitsUntcActive);
            this.GrbLimitsUNTCvalue.Controls.Add(this.label23);
            this.GrbLimitsUNTCvalue.Controls.Add(this.label24);
            this.GrbLimitsUNTCvalue.Controls.Add(this.label25);
            this.GrbLimitsUNTCvalue.Controls.Add(this.TbLimitsUntcPlus);
            this.GrbLimitsUNTCvalue.Controls.Add(this.TbLimitsUntcSet);
            this.GrbLimitsUNTCvalue.Controls.Add(this.TbLimitsUntcMin);
            this.GrbLimitsUNTCvalue.Location = new System.Drawing.Point(8, 19);
            this.GrbLimitsUNTCvalue.Name = "GrbLimitsUNTCvalue";
            this.GrbLimitsUNTCvalue.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsUNTCvalue.TabIndex = 31;
            this.GrbLimitsUNTCvalue.TabStop = false;
            this.GrbLimitsUNTCvalue.Text = "Value";
            // 
            // CkbLimitsUntcActive
            // 
            this.CkbLimitsUntcActive.AutoSize = true;
            this.CkbLimitsUntcActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsUntcActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsUntcActive.Name = "CkbLimitsUntcActive";
            this.CkbLimitsUntcActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsUntcActive.TabIndex = 3;
            this.CkbLimitsUntcActive.Text = "Active";
            this.CkbLimitsUntcActive.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 50);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(23, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "Set";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 94);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(27, 13);
            this.label24.TabIndex = 1;
            this.label24.Text = "Plus";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 72);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 13);
            this.label25.TabIndex = 1;
            this.label25.Text = "Min";
            // 
            // TbLimitsUntcPlus
            // 
            this.TbLimitsUntcPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsUntcPlus.Name = "TbLimitsUntcPlus";
            this.TbLimitsUntcPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcPlus.TabIndex = 2;
            this.TbLimitsUntcPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcSet
            // 
            this.TbLimitsUntcSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsUntcSet.Name = "TbLimitsUntcSet";
            this.TbLimitsUntcSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcSet.TabIndex = 2;
            this.TbLimitsUntcSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsUntcMin
            // 
            this.TbLimitsUntcMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsUntcMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsUntcMin.Name = "TbLimitsUntcMin";
            this.TbLimitsUntcMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsUntcMin.TabIndex = 2;
            this.TbLimitsUntcMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbLimitsTemp
            // 
            this.GrbLimitsTemp.Controls.Add(this.GrbLimitsTempStdDev);
            this.GrbLimitsTemp.Controls.Add(this.GrbLimitsTempAVG);
            this.GrbLimitsTemp.Controls.Add(this.GrbLimitsTempMinMaxDiff);
            this.GrbLimitsTemp.Controls.Add(this.GrbLimitsTempValue);
            this.GrbLimitsTemp.Location = new System.Drawing.Point(9, 6);
            this.GrbLimitsTemp.Name = "GrbLimitsTemp";
            this.GrbLimitsTemp.Size = new System.Drawing.Size(536, 156);
            this.GrbLimitsTemp.TabIndex = 0;
            this.GrbLimitsTemp.TabStop = false;
            this.GrbLimitsTemp.Text = "Limits Temp";
            // 
            // GrbLimitsTempStdDev
            // 
            this.GrbLimitsTempStdDev.Controls.Add(this.CkbLimitsTempStdDevActive);
            this.GrbLimitsTempStdDev.Controls.Add(this.label14);
            this.GrbLimitsTempStdDev.Controls.Add(this.label15);
            this.GrbLimitsTempStdDev.Controls.Add(this.label16);
            this.GrbLimitsTempStdDev.Controls.Add(this.TbLimitsTempStdDevPlus);
            this.GrbLimitsTempStdDev.Controls.Add(this.TbLimitsTempStdDevSet);
            this.GrbLimitsTempStdDev.Controls.Add(this.TbLimitsTempStdDevMin);
            this.GrbLimitsTempStdDev.Location = new System.Drawing.Point(404, 19);
            this.GrbLimitsTempStdDev.Name = "GrbLimitsTempStdDev";
            this.GrbLimitsTempStdDev.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsTempStdDev.TabIndex = 31;
            this.GrbLimitsTempStdDev.TabStop = false;
            this.GrbLimitsTempStdDev.Text = "StdDev";
            // 
            // CkbLimitsTempStdDevActive
            // 
            this.CkbLimitsTempStdDevActive.AutoSize = true;
            this.CkbLimitsTempStdDevActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsTempStdDevActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsTempStdDevActive.Name = "CkbLimitsTempStdDevActive";
            this.CkbLimitsTempStdDevActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsTempStdDevActive.TabIndex = 3;
            this.CkbLimitsTempStdDevActive.Text = "Active";
            this.CkbLimitsTempStdDevActive.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Set";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Plus";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Min";
            // 
            // TbLimitsTempStdDevPlus
            // 
            this.TbLimitsTempStdDevPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempStdDevPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsTempStdDevPlus.Name = "TbLimitsTempStdDevPlus";
            this.TbLimitsTempStdDevPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempStdDevPlus.TabIndex = 2;
            this.TbLimitsTempStdDevPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempStdDevSet
            // 
            this.TbLimitsTempStdDevSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempStdDevSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsTempStdDevSet.Name = "TbLimitsTempStdDevSet";
            this.TbLimitsTempStdDevSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempStdDevSet.TabIndex = 2;
            this.TbLimitsTempStdDevSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempStdDevMin
            // 
            this.TbLimitsTempStdDevMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempStdDevMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsTempStdDevMin.Name = "TbLimitsTempStdDevMin";
            this.TbLimitsTempStdDevMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempStdDevMin.TabIndex = 2;
            this.TbLimitsTempStdDevMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbLimitsTempAVG
            // 
            this.GrbLimitsTempAVG.Controls.Add(this.CkbLimitsTempAvgActive);
            this.GrbLimitsTempAVG.Controls.Add(this.label11);
            this.GrbLimitsTempAVG.Controls.Add(this.label12);
            this.GrbLimitsTempAVG.Controls.Add(this.label13);
            this.GrbLimitsTempAVG.Controls.Add(this.TbLimitsTempAvgPlus);
            this.GrbLimitsTempAVG.Controls.Add(this.TbLimitsTempAvgSet);
            this.GrbLimitsTempAVG.Controls.Add(this.TbLimitsTempAvgMin);
            this.GrbLimitsTempAVG.Location = new System.Drawing.Point(272, 19);
            this.GrbLimitsTempAVG.Name = "GrbLimitsTempAVG";
            this.GrbLimitsTempAVG.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsTempAVG.TabIndex = 31;
            this.GrbLimitsTempAVG.TabStop = false;
            this.GrbLimitsTempAVG.Text = "AVG";
            // 
            // CkbLimitsTempAvgActive
            // 
            this.CkbLimitsTempAvgActive.AutoSize = true;
            this.CkbLimitsTempAvgActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsTempAvgActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsTempAvgActive.Name = "CkbLimitsTempAvgActive";
            this.CkbLimitsTempAvgActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsTempAvgActive.TabIndex = 3;
            this.CkbLimitsTempAvgActive.Text = "Active";
            this.CkbLimitsTempAvgActive.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Set";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Plus";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Min";
            // 
            // TbLimitsTempAvgPlus
            // 
            this.TbLimitsTempAvgPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempAvgPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsTempAvgPlus.Name = "TbLimitsTempAvgPlus";
            this.TbLimitsTempAvgPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempAvgPlus.TabIndex = 2;
            this.TbLimitsTempAvgPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempAvgSet
            // 
            this.TbLimitsTempAvgSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempAvgSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsTempAvgSet.Name = "TbLimitsTempAvgSet";
            this.TbLimitsTempAvgSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempAvgSet.TabIndex = 2;
            this.TbLimitsTempAvgSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempAvgMin
            // 
            this.TbLimitsTempAvgMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempAvgMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsTempAvgMin.Name = "TbLimitsTempAvgMin";
            this.TbLimitsTempAvgMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempAvgMin.TabIndex = 2;
            this.TbLimitsTempAvgMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbLimitsTempMinMaxDiff
            // 
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.CkbLimitsTempMinMaxDiffActive);
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.label26);
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.label27);
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.label28);
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.TbLimitsTempMinMaxDiffPlus);
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.TbLimitsTempMinMaxDiffSet);
            this.GrbLimitsTempMinMaxDiff.Controls.Add(this.TbLimitsTempMinMaxDiffMin);
            this.GrbLimitsTempMinMaxDiff.Location = new System.Drawing.Point(140, 19);
            this.GrbLimitsTempMinMaxDiff.Name = "GrbLimitsTempMinMaxDiff";
            this.GrbLimitsTempMinMaxDiff.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsTempMinMaxDiff.TabIndex = 31;
            this.GrbLimitsTempMinMaxDiff.TabStop = false;
            this.GrbLimitsTempMinMaxDiff.Text = "MinMaxDiff";
            // 
            // CkbLimitsTempMinMaxDiffActive
            // 
            this.CkbLimitsTempMinMaxDiffActive.AutoSize = true;
            this.CkbLimitsTempMinMaxDiffActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsTempMinMaxDiffActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsTempMinMaxDiffActive.Name = "CkbLimitsTempMinMaxDiffActive";
            this.CkbLimitsTempMinMaxDiffActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsTempMinMaxDiffActive.TabIndex = 3;
            this.CkbLimitsTempMinMaxDiffActive.Text = "Active";
            this.CkbLimitsTempMinMaxDiffActive.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 50);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(23, 13);
            this.label26.TabIndex = 1;
            this.label26.Text = "Set";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 94);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(27, 13);
            this.label27.TabIndex = 1;
            this.label27.Text = "Plus";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 72);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(24, 13);
            this.label28.TabIndex = 1;
            this.label28.Text = "Min";
            // 
            // TbLimitsTempMinMaxDiffPlus
            // 
            this.TbLimitsTempMinMaxDiffPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempMinMaxDiffPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsTempMinMaxDiffPlus.Name = "TbLimitsTempMinMaxDiffPlus";
            this.TbLimitsTempMinMaxDiffPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempMinMaxDiffPlus.TabIndex = 2;
            this.TbLimitsTempMinMaxDiffPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempMinMaxDiffSet
            // 
            this.TbLimitsTempMinMaxDiffSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempMinMaxDiffSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsTempMinMaxDiffSet.Name = "TbLimitsTempMinMaxDiffSet";
            this.TbLimitsTempMinMaxDiffSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempMinMaxDiffSet.TabIndex = 2;
            this.TbLimitsTempMinMaxDiffSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempMinMaxDiffMin
            // 
            this.TbLimitsTempMinMaxDiffMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempMinMaxDiffMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsTempMinMaxDiffMin.Name = "TbLimitsTempMinMaxDiffMin";
            this.TbLimitsTempMinMaxDiffMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempMinMaxDiffMin.TabIndex = 2;
            this.TbLimitsTempMinMaxDiffMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbLimitsTempValue
            // 
            this.GrbLimitsTempValue.Controls.Add(this.CkbLimitsTempActive);
            this.GrbLimitsTempValue.Controls.Add(this.label8);
            this.GrbLimitsTempValue.Controls.Add(this.label9);
            this.GrbLimitsTempValue.Controls.Add(this.label10);
            this.GrbLimitsTempValue.Controls.Add(this.TbLimitsTempPlus);
            this.GrbLimitsTempValue.Controls.Add(this.TbLimitsTempSet);
            this.GrbLimitsTempValue.Controls.Add(this.TbLimitsTempMin);
            this.GrbLimitsTempValue.Location = new System.Drawing.Point(8, 19);
            this.GrbLimitsTempValue.Name = "GrbLimitsTempValue";
            this.GrbLimitsTempValue.Size = new System.Drawing.Size(124, 126);
            this.GrbLimitsTempValue.TabIndex = 31;
            this.GrbLimitsTempValue.TabStop = false;
            this.GrbLimitsTempValue.Text = "Value";
            // 
            // CkbLimitsTempActive
            // 
            this.CkbLimitsTempActive.AutoSize = true;
            this.CkbLimitsTempActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbLimitsTempActive.Location = new System.Drawing.Point(46, 25);
            this.CkbLimitsTempActive.Name = "CkbLimitsTempActive";
            this.CkbLimitsTempActive.Size = new System.Drawing.Size(56, 17);
            this.CkbLimitsTempActive.TabIndex = 3;
            this.CkbLimitsTempActive.Text = "Active";
            this.CkbLimitsTempActive.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Set";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Plus";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Min";
            // 
            // TbLimitsTempPlus
            // 
            this.TbLimitsTempPlus.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempPlus.Location = new System.Drawing.Point(40, 91);
            this.TbLimitsTempPlus.Name = "TbLimitsTempPlus";
            this.TbLimitsTempPlus.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempPlus.TabIndex = 2;
            this.TbLimitsTempPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempSet
            // 
            this.TbLimitsTempSet.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempSet.Location = new System.Drawing.Point(40, 48);
            this.TbLimitsTempSet.Name = "TbLimitsTempSet";
            this.TbLimitsTempSet.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempSet.TabIndex = 2;
            this.TbLimitsTempSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbLimitsTempMin
            // 
            this.TbLimitsTempMin.BackColor = System.Drawing.Color.LightYellow;
            this.TbLimitsTempMin.Location = new System.Drawing.Point(40, 70);
            this.TbLimitsTempMin.Name = "TbLimitsTempMin";
            this.TbLimitsTempMin.Size = new System.Drawing.Size(76, 20);
            this.TbLimitsTempMin.TabIndex = 2;
            this.TbLimitsTempMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrbMeasVal
            // 
            this.GrbMeasVal.Controls.Add(this.CkbStartStop);
            this.GrbMeasVal.Controls.Add(this._Lbl_NTCoffset);
            this.GrbMeasVal.Controls.Add(this._GB_Meas_Temp);
            this.GrbMeasVal.Controls.Add(this._Lbl_PT1000_20);
            this.GrbMeasVal.Controls.Add(this._GB_Values_Diff);
            this.GrbMeasVal.Controls.Add(this._Lbl_PT1000_30);
            this.GrbMeasVal.Controls.Add(this._GB_STDdev);
            this.GrbMeasVal.Controls.Add(this.CkbAutoNoStop);
            this.GrbMeasVal.Controls.Add(this._GB_Meas_Val);
            this.GrbMeasVal.Controls.Add(this._GB_Sensor_Val);
            this.GrbMeasVal.Controls.Add(this.BtnReset);
            this.GrbMeasVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.GrbMeasVal.Location = new System.Drawing.Point(0, 0);
            this.GrbMeasVal.Name = "GrbMeasVal";
            this.GrbMeasVal.Size = new System.Drawing.Size(1223, 150);
            this.GrbMeasVal.TabIndex = 31;
            this.GrbMeasVal.TabStop = false;
            this.GrbMeasVal.Text = "MeasVal";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnCalibValues);
            this.panel2.Controls.Add(this.LblPage2Title);
            this.panel2.Controls.Add(this.LblPage2);
            this.panel2.Controls.Add(this.LblPage3Title);
            this.panel2.Controls.Add(this.LblPage3);
            this.panel2.Controls.Add(this.LblPage14Title);
            this.panel2.Controls.Add(this.LblPage30);
            this.panel2.Controls.Add(this.LblPage14);
            this.panel2.Controls.Add(this.LblPage30Title);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1223, 60);
            this.panel2.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1129, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "nValues";
            // 
            // CkbDebugging
            // 
            this.CkbDebugging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CkbDebugging.AutoSize = true;
            this.CkbDebugging.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbDebugging.Location = new System.Drawing.Point(1045, 35);
            this.CkbDebugging.Name = "CkbDebugging";
            this.CkbDebugging.Size = new System.Drawing.Size(78, 17);
            this.CkbDebugging.TabIndex = 17;
            this.CkbDebugging.Text = "Debugging";
            this.CkbDebugging.UseVisualStyleBackColor = true;
            // 
            // PanelLimits
            // 
            this.PanelLimits.Controls.Add(this.GrbLimitsUNTC);
            this.PanelLimits.Controls.Add(this.GrbLimitsTemp);
            this.PanelLimits.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelLimits.Location = new System.Drawing.Point(0, 363);
            this.PanelLimits.Name = "PanelLimits";
            this.PanelLimits.Size = new System.Drawing.Size(1223, 164);
            this.PanelLimits.TabIndex = 37;
            // 
            // UcCalib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelLimits);
            this.Controls.Add(this.PanelMeasVal);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.DgvMeasValues);
            this.Controls.Add(this.PanelHeader);
            this.Name = "UcCalib";
            this.Size = new System.Drawing.Size(1223, 991);
            this.Load += new System.EventHandler(this.UC_Calib_Load);
            this._GB_Sensor_Val.ResumeLayout(false);
            this._GB_Sensor_Val.PerformLayout();
            this._GB_Meas_Val.ResumeLayout(false);
            this._GB_Meas_Val.PerformLayout();
            this._GB_Meas_Temp.ResumeLayout(false);
            this._GB_Meas_Temp.PerformLayout();
            this._GB_Values_Diff.ResumeLayout(false);
            this._GB_Values_Diff.PerformLayout();
            this._GB_STDdev.ResumeLayout(false);
            this._GB_STDdev.PerformLayout();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudRatingValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudNvalues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMeasValues)).EndInit();
            this.PanelMeasVal.ResumeLayout(false);
            this.GrbLimitsUNTC.ResumeLayout(false);
            this.GrbLimitsUNTCstdDev.ResumeLayout(false);
            this.GrbLimitsUNTCstdDev.PerformLayout();
            this.GrbLimitsUNTCavg.ResumeLayout(false);
            this.GrbLimitsUNTCavg.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GrbLimitsUNTCvalue.ResumeLayout(false);
            this.GrbLimitsUNTCvalue.PerformLayout();
            this.GrbLimitsTemp.ResumeLayout(false);
            this.GrbLimitsTempStdDev.ResumeLayout(false);
            this.GrbLimitsTempStdDev.PerformLayout();
            this.GrbLimitsTempAVG.ResumeLayout(false);
            this.GrbLimitsTempAVG.PerformLayout();
            this.GrbLimitsTempMinMaxDiff.ResumeLayout(false);
            this.GrbLimitsTempMinMaxDiff.PerformLayout();
            this.GrbLimitsTempValue.ResumeLayout(false);
            this.GrbLimitsTempValue.PerformLayout();
            this.GrbMeasVal.ResumeLayout(false);
            this.GrbMeasVal.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PanelLimits.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox CkbAutoNoStop;
        private System.Windows.Forms.GroupBox _GB_Sensor_Val;
        private System.Windows.Forms.TextBox TbSensorValues_NTC;
        private System.Windows.Forms.TextBox TbSensorValues_PT20;
        private System.Windows.Forms.TextBox TbSensorValues_PT30;
        private System.Windows.Forms.TextBox TbSensorValues_Date;
        private System.Windows.Forms.TextBox TbSensorValues_Desc;
        private System.Windows.Forms.GroupBox _GB_Meas_Val;
        private System.Windows.Forms.TextBox TbMeasVal_NTC;
        private System.Windows.Forms.TextBox TbMeasVal_PT20;
        private System.Windows.Forms.TextBox TbMeasVal_PT30;
        private System.Windows.Forms.TextBox TbMeasVal_Date;
        private System.Windows.Forms.TextBox _TB_Meas_Counter;
        private System.Windows.Forms.GroupBox _GB_Meas_Temp;
        private System.Windows.Forms.TextBox TbMeasVal_NTC_Temp;
        private System.Windows.Forms.TextBox TbMeasVal_PT20_Temp;
        private System.Windows.Forms.TextBox TbMeasVal_PT30_Temp;
        private System.Windows.Forms.GroupBox _GB_Values_Diff;
        private System.Windows.Forms.TextBox TbMeasVal_NTC_Max;
        private System.Windows.Forms.TextBox TbMeasVal_PT20_Max;
        private System.Windows.Forms.TextBox TbMeasVal_NTC_Min;
        private System.Windows.Forms.TextBox TbMeasVal_PT20_Min;
        private System.Windows.Forms.TextBox TbMeasVal_PT30_Max;
        private System.Windows.Forms.TextBox TbMeasVal_NTC_Diff;
        private System.Windows.Forms.TextBox TbMeasVal_PT30_Min;
        private System.Windows.Forms.TextBox TbMeasVal_PT20_Diff;
        private System.Windows.Forms.TextBox TbMeasVal_PT30_Diff;
        private System.Windows.Forms.GroupBox _GB_STDdev;
        private System.Windows.Forms.TextBox TbMeasVal_NTC_StdDev;
        private System.Windows.Forms.TextBox TbMeasVal_PT20_StdDev;
        private System.Windows.Forms.TextBox TbMeasVal_PT30_StdDev;
        private System.Windows.Forms.CheckBox CkbStartStop;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label _Lbl_PT1000_30;
        private System.Windows.Forms.Label _Lbl_PT1000_20;
        private System.Windows.Forms.Label _Lbl_NTCoffset;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Button BtnLoadPathTdl;
        private System.Windows.Forms.Label LblPathTdl;
        private System.Windows.Forms.Label LblDurationTxt;
        private System.Windows.Forms.Label LblStatusTxt;
        private UserControls.LEDsingle LedError;
        private System.Windows.Forms.Button BtnCommTest;
        private System.Windows.Forms.Button BtnGetP16;
        private System.Windows.Forms.Label LblHEX;
        private System.Windows.Forms.Label LblTdlPathInUse;
        private System.Windows.Forms.DataGridView DgvMeasValues;
        private System.Windows.Forms.NumericUpDown NudNvalues;
        public System.Windows.Forms.TextBox TbPathTdl;
        private System.Windows.Forms.Label LblPage2Title;
        private System.Windows.Forms.Label LblPage2;
        private System.Windows.Forms.Label LblPage3Title;
        private System.Windows.Forms.Label LblPage3;
        private System.Windows.Forms.Label LblPage14Title;
        private System.Windows.Forms.Label LblPage14;
        private System.Windows.Forms.Label LblPage30Title;
        private System.Windows.Forms.Label LblPage30;
        private System.Windows.Forms.Button BtnCalibValues;
        private System.Windows.Forms.Panel PanelMeasVal;
        private System.Windows.Forms.Panel panel2;
        private UserControls.LEDsingle LedRating_PT30;
        private UserControls.LEDsingle LedRating_PT20;
        private UserControls.LEDsingle LedRating_NTC;
        private System.Windows.Forms.GroupBox GrbLimitsUNTC;
        private System.Windows.Forms.GroupBox GrbLimitsUNTCstdDev;
        private System.Windows.Forms.CheckBox CkbLimitsUntcStdDevActive;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox TbLimitsUntcStdDevPlus;
        private System.Windows.Forms.TextBox TbLimitsUntcStdDevSet;
        private System.Windows.Forms.TextBox TbLimitsUntcStdDevMin;
        private System.Windows.Forms.GroupBox GrbLimitsUNTCavg;
        private System.Windows.Forms.CheckBox CkbLimitsUntcAvgActive;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox TbLimitsUntcAvgPlus;
        private System.Windows.Forms.TextBox TbLimitsUntcAvgSet;
        private System.Windows.Forms.TextBox TbLimitsUntcAvgMin;
        private System.Windows.Forms.GroupBox GrbLimitsUNTCvalue;
        private System.Windows.Forms.CheckBox CkbLimitsUntcActive;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox TbLimitsUntcPlus;
        private System.Windows.Forms.TextBox TbLimitsUntcSet;
        private System.Windows.Forms.TextBox TbLimitsUntcMin;
        private System.Windows.Forms.GroupBox GrbLimitsTemp;
        private System.Windows.Forms.GroupBox GrbLimitsTempStdDev;
        private System.Windows.Forms.CheckBox CkbLimitsTempStdDevActive;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox TbLimitsTempStdDevPlus;
        private System.Windows.Forms.TextBox TbLimitsTempStdDevSet;
        private System.Windows.Forms.TextBox TbLimitsTempStdDevMin;
        private System.Windows.Forms.GroupBox GrbLimitsTempAVG;
        private System.Windows.Forms.CheckBox CkbLimitsTempAvgActive;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TbLimitsTempAvgPlus;
        private System.Windows.Forms.TextBox TbLimitsTempAvgSet;
        private System.Windows.Forms.TextBox TbLimitsTempAvgMin;
        private System.Windows.Forms.GroupBox GrbLimitsTempValue;
        private System.Windows.Forms.CheckBox CkbLimitsTempActive;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TbLimitsTempPlus;
        private System.Windows.Forms.TextBox TbLimitsTempSet;
        private System.Windows.Forms.TextBox TbLimitsTempMin;
        private System.Windows.Forms.GroupBox GrbMeasVal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox CkbLimitsUntcMinMaxDiffActive;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox TbLimitsUntcMinMaxDiffPlus;
        private System.Windows.Forms.TextBox TbLimitsUntcMinMaxDiffSet;
        private System.Windows.Forms.TextBox TbLimitsUntcMinMaxDiffMin;
        private System.Windows.Forms.GroupBox GrbLimitsTempMinMaxDiff;
        private System.Windows.Forms.CheckBox CkbLimitsTempMinMaxDiffActive;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox TbLimitsTempMinMaxDiffPlus;
        private System.Windows.Forms.TextBox TbLimitsTempMinMaxDiffSet;
        private System.Windows.Forms.TextBox TbLimitsTempMinMaxDiffMin;
        private System.Windows.Forms.NumericUpDown NudRatingValues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CkbDebugging;
        private System.Windows.Forms.Panel PanelLimits;
    }
}
