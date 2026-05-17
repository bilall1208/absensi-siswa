using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    class KFBilal
    {
        public static void UntukFormBilal(Form FormApa, Panel PanelApa)
        {
            PanelApa.Controls.Clear();
            FormApa.FormBorderStyle = FormBorderStyle.None;
            FormApa.Dock = DockStyle.Fill;
            PanelApa.Controls.Add(FormApa);
            FormApa.Visible = true;
        }
    }
}
