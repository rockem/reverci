namespace Reverci.view
{
    internal interface IStatisticsView : IComponentView
    {
        void setBlackWins(int i_Wins);

        void setWhiteWins(int i_Wins);

        void setBnWDraws(int i_Draws);

        void setBlackScore(int i_Score);

        void setWhiteScore(int i_Score);

        void setComputerWins(int i_Wins);

        void setHumanWins(int i_Wins);

        void setHnCDraws(int draws);

        void setComputerScore(int score);

        void setHumanScore(int i_Score);

        void Show(int i_X, int i_Y);
    }
}