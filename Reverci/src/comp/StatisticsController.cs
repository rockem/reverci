using Reverci.view;

namespace Reverci.comp
{
    internal class StatisticsController : IStatisticsEventListener
    {
        private readonly IStatisticsView r_StatisticsView;

        public StatisticsController(IStatisticsView i_View)
        {
            r_StatisticsView = i_View;
            i_View.setEventListener(this);
        }

        public void ShowStatisticsView(int i_MiddleX, int i_MiddleY)
        {
            updateView();
            r_StatisticsView.Show(i_MiddleX, i_MiddleY);
        }

        public void ResetStatistics()
        {
            getStatistics().ResetStatistics();
            updateView();
        }

        private StatisticsHolder getStatistics()
        {
            return OthelloData.GetInstance().Statistics;
        }

        private void updateView()
        {
            r_StatisticsView.setBlackWins(getStatistics().BlackWins);
            r_StatisticsView.setWhiteWins(getStatistics().WhiteWins);
            r_StatisticsView.setBnWDraws(getStatistics().BnWDraws);
            r_StatisticsView.setBlackScore(getStatistics().BlackScore);
            r_StatisticsView.setWhiteScore(getStatistics().WhiteScore);
            r_StatisticsView.setComputerWins(getStatistics().ComputerWins);
            r_StatisticsView.setHumanWins(getStatistics().HumanWins);
            r_StatisticsView.setHnCDraws(getStatistics().HnCDraws);
            r_StatisticsView.setComputerScore(getStatistics().CompuerScore);
            r_StatisticsView.setHumanScore(getStatistics().HumanScore);
        }
    }
}