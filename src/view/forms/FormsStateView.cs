using System.Windows.Forms;

namespace Reverci.view.forms
{
    public partial class FormsStateView : StatusStrip, IStateView
    {
        private ToolStripProgressBar m_ProgressBar;
        private ToolStripStatusLabel m_StateMessage;

        public FormsStateView()
        {
            InitializeComponent();
            m_ProgressBar.Style = ProgressBarStyle.Marquee;
            m_ProgressBar.MarqueeAnimationSpeed = 200;
        }

        private delegate void GenericDelegate();

        public void updateStatusMessageWith(string i_Status)
        {
            GenericDelegate dlg = delegate { m_StateMessage.Text = i_Status; };
            Invoke(dlg);
        }

        public void SetThinking(bool b)
        {
            GenericDelegate dlg = delegate { m_ProgressBar.Visible = b; };
            Invoke(dlg);
        }
    }
}