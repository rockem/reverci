using System.Windows.Forms;
using Reverci.comp;

namespace Reverci.view.forms
{
    public partial class FormsStatisticsView : Form, IStatisticsView
    {
        private IStatisticsEventListener m_EventListener;

        public FormsStatisticsView()
        {
            InitializeComponent();
        }

        public void setBlackWins(int i_Wins)
        {
            m_BlackWins.Text = i_Wins.ToString();
        }

        public void setWhiteWins(int i_Wins)
        {
            m_WhiteWins.Text = i_Wins.ToString();
        }

        public void setBnWDraws(int i_Draws)
        {
            m_Draws.Text = i_Draws.ToString();
        }

        public void setBlackScore(int i_Score)
        {
            m_BlackScore.Text = i_Score.ToString();
        }

        public void setWhiteScore(int i_Score)
        {
            m_WhiteScore.Text = i_Score.ToString();
        }

        public void setComputerWins(int i_Wins)
        {
            m_ComputerWins.Text = i_Wins.ToString();
        }

        public void setHumanWins(int i_Wins)
        {
            m_HumanWins.Text = i_Wins.ToString();
        }

        public void setHnCDraws(int i_Draws)
        {
            m_VsComputerDraw.Text = i_Draws.ToString();
        }

        public void setComputerScore(int i_Score)
        {
            m_ComputerScore.Text = i_Score.ToString();
        }

        public void setHumanScore(int i_Score)
        {
            m_HumanScore.Text = i_Score.ToString();
        }

        public void Show(int i_X, int i_Y)
        {
            Left = i_X - (Size.Width / 2);
            Top = i_Y - (Size.Height / 2);
            Show();
        }

        public void setEventListener(IEventListener i_Listener)
        {
            m_EventListener = (IStatisticsEventListener)i_Listener;
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            Hide();
        }

        private void ResetButton_Click(object sender, System.EventArgs e)
        {
            m_EventListener.ResetStatistics();
        }

        private void FormsStatisticsView_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}