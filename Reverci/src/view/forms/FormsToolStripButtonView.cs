using System;
using System.Windows.Forms;
using Reverci.comp;

namespace Reverci.view.forms
{
    public partial class FormsToolStripButtonView : ToolStripButton, IButtonView
    {
        private IButtonEventListener m_EventListener;

        public FormsToolStripButtonView()
        {
            InitializeComponent();
        }

        public void SetEventListener(IEventListener i_Listener)
        {
            m_EventListener = (IButtonEventListener)i_Listener;
        }

        public void SetChecked(bool i_Checked)
        {
            Checked = i_Checked;
        }

        public void SetEnabled(bool i_Enabled)
        {
            Enabled = i_Enabled;
        }

        private void FormsToolStripButtonView_Click(object sender, EventArgs e)
        {
            m_EventListener.DispatchClick();
        }
    }
}