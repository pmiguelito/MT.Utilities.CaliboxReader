using System.Windows.Forms;

namespace ReadCalibox
{
    public class LabelsPaar
    {
        public Label Title { get; set; }
        public Label Value { get; set; }

        public void SetVisible(bool visible)
        {
            Title.Visible = visible;
            Value.Visible = visible;
            Value.Text = string.Empty;
        }
    }
}
