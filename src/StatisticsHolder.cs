using System;
using Othello.player;

namespace Othello
{
    [Serializable()]
    public class StatisticsHolder
    {
        public int BlackScore { get; private set; }

        public int WhiteScore { get; private set; }

        public int BlackWins { get; private set; }

        public int WhiteWins { get; private set; }

        public int ComputerWins { get; private set; }

        public int HumanWins { get; private set; }

        public int CompuerScore { get; private set; }

        public int HumanScore { get; private set; }

        public int BnWDraws { get; private set; }

        public int HnCDraws { get; private set; }

        public void AddGameScore(
            int i_BlackScore, 
            int i_WhiteScore,
            ePlayerType i_BlackPlayer, 
            ePlayerType i_WhitePlayer)
        {
            BlackScore += i_BlackScore;
            WhiteScore += i_WhiteScore;

            if (i_BlackScore > i_WhiteScore)
            {
                BlackWins++;
            }
            else if (i_BlackScore < i_WhiteScore)
            {
                WhiteWins++;
            }
            else
            {
                BnWDraws++;
            }

            if (i_BlackPlayer != i_WhitePlayer)
            {
                updateVsComputerWins(i_BlackScore, i_WhiteScore, i_BlackPlayer, i_WhitePlayer);
                updateVsComputerScore(i_BlackScore, i_WhiteScore, i_BlackPlayer);
            }
        }

        private void updateVsComputerScore(int i_BlackScore, int i_WhiteScore, ePlayerType i_BlackPlayer)
        {
            if (i_BlackPlayer == ePlayerType.Computer)
            {
                CompuerScore += i_BlackScore;
                HumanScore += i_WhiteScore;
            }
            else
            {
                HumanScore += i_BlackScore;
                CompuerScore += i_WhiteScore;
            }
        }

        private void updateVsComputerWins(
            int i_BlackScore, 
            int i_WhiteScore, 
            ePlayerType i_BlackPlayer,
            ePlayerType i_WhitePlayer)
        {
            if (i_BlackScore > i_WhiteScore)
            {
                updatePlayerWins(i_BlackPlayer);
            }
            else if (i_BlackScore < i_WhiteScore)
            {
                updatePlayerWins(i_WhitePlayer);
            }
            else
            {
                HnCDraws++;
            }
        }

        private void updatePlayerWins(ePlayerType player)
        {
            if (player == ePlayerType.Computer)
            {
                ComputerWins++;
            }
            else
            {
                HumanWins++;
            }
        }

        public void ResetStatistics()
        {
            BlackScore = 0;
            WhiteScore = 0;
            BlackWins = 0;
            WhiteWins = 0;
            ComputerWins = 0;
            HumanWins = 0;
            CompuerScore = 0;
            HumanScore = 0;
            BnWDraws = 0;
            HnCDraws = 0;
        }
    }
}