using CaliboxLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static ReadCalibox.Handler;
using static STDhelper.clSTD;

namespace ReadCalibox
{
    public enum ChartMode
    {
        STDdeviation,
        MeanValue,
        ErrorValue,
        RefMean,
        RefValue,
        STD_Error
    }

    public class ChartManagement
    {
        public DeviceCom Device { get; }
        public Chart Chart { get; }
        public ChartLabels ChartLabels { get; private set; } = new ChartLabels();
        public CheckBox CheckBoxVisualalisation { get; set; }
        public ChartMode SelectedMode { get; set; } = ChartMode.STD_Error;
        public int ShowVaulesQuantity { get; set; } = 20;

        /************************************************
         * FUNCTION:    Constructor(s)
         * DESCRIPTION:
         ************************************************/
        public ChartManagement(DeviceCom device, Chart chart, CheckBox ckbChart)
        {
            Chart = chart;
            CheckBoxVisualalisation = ckbChart;
            CheckBoxVisualalisation.CheckedChanged += CheckBoxVisualisation_CheckedChanged;
        }

        public void Reset()
        {
            Activate(false);
        }

        /************************************************
         * FUNCTION:    Initialization
         * DESCRIPTION:
         ************************************************/
        public const string XVALUEMEMBER = "Count";
        public void Initialization()
        {
            Chart.Series.Clear();
            Chart.Series.Add(CreateChartSerie(MN_I_set, XVALUEMEMBER, Color.Gray));
            Chart.Series.Add(CreateChartSerie(MN_I_AVG, XVALUEMEMBER, Color.Orange));
            Chart.Series.Add(CreateChartSerie(MN_I_StdDev, XVALUEMEMBER, Color.White));
            Chart.Series.Add(CreateChartSerie(MN_I_Error, XVALUEMEMBER, Color.Yellow));
            Chart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            Chart.ChartAreas[0].AxisY2.IsStartedFromZero = false;
            Chart.ChartAreas[0].AxisX.Interval = 1;
            Chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 5);
            Chart.ChartAreas[0].AxisX.IsMarginVisible = false;
            Chart.ChartAreas[0].AxisY.IsMarginVisible = false;
            Chart.ChartAreas[0].AxisY2.IsMarginVisible = false;
            Chart.ChartAreas[0].AxisY.LabelStyle.Format = "0.##";
            Chart.ChartAreas[0].AxisY2.LabelStyle.Format = "0.##";
            Chart.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            Chart.ChartAreas[0].AxisY2.MinorGrid.Enabled = false;
            CheckBoxVisualalisation.Checked = false;
        }
        private Series CreateChartSerie(string name, string xvaluemember, Color color)
        {
            return new Series
            {
                Name = name,
                Color = color,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = false,
                ChartType = SeriesChartType.Line,
                BorderWidth = 4,
                XValueMember = xvaluemember,
                YValueMembers = name
            };
        }

        public void Chart_ChangeColorsStdDev(Color color)
        {
            Chart_ChangeColorsPrimaryAxis(MN_I_StdDev, true, color);
        }
        public void Chart_ChangeColorsPrimaryAxis(string title, bool active, Color color)
        {
            Chart.Series[title].Enabled = active;
            if (!active) { return; }
            Chart.Series[title].Color = color;
            Chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Chart.Series[title].Color;
            Chart.Series[title].YAxisType = AxisType.Primary;
            Chart.ChartAreas[0].AxisY.Title = title;
            Chart.ChartAreas[0].AxisY.TitleForeColor = Chart.Series[title].Color;
        }
        public void Chart_ChangeColorsSecondaryAxis(string title, bool active)
        {
            Chart.Series[title].Enabled = active;
            if (!active) { return; }
            Chart.Series[title].Color = Color.Yellow;
            Chart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Chart.Series[title].Color;
            Chart.Series[title].YAxisType = AxisType.Secondary;
            Chart.ChartAreas[0].AxisY2.Title = title;
            Chart.ChartAreas[0].AxisY2.TitleForeColor = Chart.Series[title].Color;
        }

        /************************************************
         * FUNCTION:    CheckBox Visualisation
         * DESCRIPTION: User seletion for Chart Show
         ************************************************/
        private void CheckBoxVisualisation_CheckedChanged(object sender, EventArgs e)
        {
            SetVisible(CheckBoxVisualalisation.Checked);
        }

        /************************************************
         * FUNCTION:    External Inputs
         * DESCRIPTION:
         ************************************************/
        public void Change_ChartModeSelection(ChartMode selection)
        {
            if (Chart.InvokeRequired)
            {
                Chart.Invoke((MethodInvoker)delegate
                {
                    Change_ChartModeSelection(selection);
                });
                return;
            }
            bool refValues = false;
            bool meanValues = false;
            bool stdValues = false;
            bool errorValues = false;
            int actives = 0;
            switch (selection)
            {
                case ChartMode.STDdeviation:
                    actives = 1;
                    Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    ShowVaulesQuantity = 20;
                    stdValues = true;
                    break;
                case ChartMode.MeanValue:
                    actives = 1;
                    ShowVaulesQuantity = 20;
                    Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    meanValues = true;
                    break;
                case ChartMode.ErrorValue:
                    actives = 1;
                    ShowVaulesQuantity = 20;
                    Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    errorValues = true;
                    break;
                case ChartMode.RefMean:
                    actives = 2;
                    ShowVaulesQuantity = 20;
                    Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    refValues = true;
                    meanValues = true;
                    break;
                //case ChartMode.All:
                //    actives = 4;
                //    Chart_ShowVaulesQuantiy = 15;
                //    Chart_Measurement.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                //    refValues = true;
                //    meanValues = true;
                //    stdValues = true;
                //    errorValues = true;
                //    break;
                case ChartMode.RefValue:
                    actives = 1;
                    ShowVaulesQuantity = 20;
                    Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    refValues = true;
                    break;
                case ChartMode.STD_Error:
                    actives = 2;
                    ShowVaulesQuantity = 20;
                    Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    errorValues = true;
                    stdValues = true;
                    break;
                default:
                    ShowVaulesQuantity = 20;
                    break;
            }
            Chart_ChangeColorsPrimaryAxis(MN_I_set, refValues, Color.White);
            Chart_ChangeColorsPrimaryAxis(MN_I_AVG, meanValues, Color.White);
            Chart_ChangeColorsPrimaryAxis(MN_I_StdDev, stdValues, Color.White);
            switch (actives)
            {
                case int n when n < 2:
                    Chart_ChangeColorsPrimaryAxis(MN_I_Error, errorValues, Color.White);
                    break;
                default:
                    Chart_ChangeColorsSecondaryAxis(MN_I_Error, errorValues);
                    break;
            }
            SelectedMode = selection;
        }

        private bool _IsVisible;

        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                SetVisible(value);
            }
        }

        private bool _IsActivated;

        public bool IsActivated
        {
            get { return _IsActivated; }
            set
            {
                _IsActivated = value;
            }
        }

        public void Activate(bool active)
        {
            if (active == false)
            {
                SetVisible(false);
            }
            _IsActivated = active;
            CheckBoxVisualalisation.Visible = active;
        }

        public void SetVisible()
        {
            if (IsVisible)
            {
                return;
            }
            SetVisible(true);
        }

        public void SetVisible(bool visible)
        {
            _IsVisible = visible;
            CheckBoxVisualalisation.CheckedChanged -= CheckBoxVisualisation_CheckedChanged;
            CheckBoxVisualalisation.Checked = visible;
            CheckBoxVisualalisation.CheckedChanged += CheckBoxVisualisation_CheckedChanged;
            Chart.Visible = visible;
            ChartLabels.SetVisible(visible);
            if (visible)
            {
                Change_ChartModeSelection(SelectedMode);
                Chart.BringToFront();
            }
        }

        /************************************************
         * FUNCTION:    Measurements
         * DESCRIPTION:
         ************************************************/
        public Color ColorStdDev { get; private set; } = Color.Black;

        public void Chart_UpdateFromMeasResults(DeviceResponseValues drv, ChannelValues channel)
        {
            if (Chart.InvokeRequired)
            {
                Chart.Invoke((MethodInvoker)delegate
                {
                    Chart_UpdateFromMeasResults(drv, channel);
                });
                return;
            }
            if (channel.GetResults(drv.BoxMode.Hex, out List<DeviceLimitsResults> results))
            {
                if (IsActivated)
                {
                    SetVisible();
                }
                var limitsLast = Chart_Values(channel, results);
                Chart_Colors(limitsLast);
            }
        }
        private void Chart_Colors(DeviceLimitsResults results)
        {
            if (results == null) { return; }
            Chart_AxisColors(results);
            Chart_LabelColors(results);
        }
        private void Chart_LabelColors(DeviceLimitsResults results)
        {
            try
            {
                ChartLabels.Mode.Value.Text = results.BoxMode.Desc;

                ChartLabels.AVG.Value.Text = results.ValueAVG.ToString("0.000");

                ChartLabels.Diff.Value.Text = results.ErrorABS.ToString("0.000");
                ChartLabels.Diff.Value.ForeColor = GetCalcColor(results, Color.Yellow);

                ChartLabels.Set.Value.Text = results.ValueSet.ToString("0.##");

                ChartLabels.StdDev.Value.Text = results.StdDev.ToString("0.000");
                ChartLabels.StdDev.Value.ForeColor = ColorStdDev;
                // GetLabelColor(results.StdDev_ok, results.StdDevSet, 1.5, results.StdDev);
            }
            catch { }
        }

        private static Color GetCalcColor(DeviceLimitsResults results, Color colorDefault)
        {
            var factor = results.ValueSet * 0.07;
            var diff = Math.Abs(results.ValueSet - results.ValueAVG);
            bool a = factor < diff;
            var color = a ? MTcolors.MT_rating_Bad_Active_No : colorDefault;
            return color;
        }

        public Color GetLabelColor(bool rating, double set, double factor, double? value)
        {
            if (rating) { return MTcolors.MT_rating_God_Active; }
            var color = value > set * factor ? MTcolors.MT_rating_Bad_Active_No : MTcolors.MT_rating_Alert_Active;
            return color;
        }

        private void Chart_AxisColors(DeviceLimitsResults results)
        {
            switch (SelectedMode)
            {
                case ChartMode.STDdeviation:
                case ChartMode.STD_Error:
                    if (results.StdDev < results.StdDevSet)
                    {
                        ColorStdDev = MTcolors.MT_rating_God_Active;
                    }
                    else if (results.StdDev < results.StdDevSet * 1.5)
                    {
                        ColorStdDev = MTcolors.MT_rating_Alert_Active;
                    }
                    else
                    {
                        ColorStdDev = MTcolors.MT_rating_Bad_Active_No;
                    }
                    Chart_ChangeColorsStdDev(ColorStdDev);
                    break;
                default:
                    break;
            }
        }
        private DeviceLimitsResults Chart_Values(ChannelValues channel, List<DeviceLimitsResults> results)
        {
            DeviceLimitsResults limitsLast = null;
            try
            {
                Chart.Series[MN_I_StdDev].Points.Clear();
                Chart.Series[MN_I_Error].Points.Clear();
                Chart.Series[MN_I_AVG].Points.Clear();
                Chart.Series[MN_I_set].Points.Clear();

                var start = results.Count - ShowVaulesQuantity + 1;
                if (start < 0) { start = 0; }
                for (int i = start; i < results.Count; i++)
                {
                    limitsLast = results[i];
                    Chart.Series[MN_I_StdDev].Points.AddXY(limitsLast.Count, limitsLast.StdDev);
                    Chart.Series[MN_I_Error].Points.AddXY(limitsLast.Count, limitsLast.ErrorABS);
                    Chart.Series[MN_I_AVG].Points.AddXY(limitsLast.Count, limitsLast.ValueAVG);
                    Chart.Series[MN_I_set].Points.AddXY(limitsLast.Count, limitsLast.ValueSet);
                }
            }
            catch (Exception ex)
            {
                Device.Logger.Save(ex);
            }
            return limitsLast;
        }
    }
}
