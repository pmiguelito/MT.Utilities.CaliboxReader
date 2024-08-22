namespace ReadCalibox
{
    public class ChartLabels
    {
        public LabelsPaar Mode { get; set; } = new LabelsPaar();
        public LabelsPaar Set { get; set; } = new LabelsPaar();
        public LabelsPaar AVG { get; set; } = new LabelsPaar();
        public LabelsPaar Diff { get; set; } = new LabelsPaar();
        public LabelsPaar StdDev { get; set; } = new LabelsPaar();

        public void SetVisible(bool visible)
        {
            Mode.SetVisible(visible);
            Set.SetVisible(visible);
            AVG.SetVisible(visible);
            Diff.SetVisible(visible);
            StdDev.SetVisible(visible);
        }
    }
}
