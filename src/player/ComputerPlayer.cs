using System;
using System.Drawing;
using System.Threading;
using Othello.model;

namespace Othello.player
{
    internal class ComputerPlayer : IPlayer
    {
        private IBoardModel m_BoardModel;
        private eSquareType m_PlayColor;

        public void setColor(eSquareType i_Color)
        {
            m_PlayColor = i_Color;
        }

        public void setBoardModel(IBoardModel i_Model)
        {
            m_BoardModel = i_Model;
        }

        public bool IsAutomatic()
        {
            return true;
        }

        public eSquareType getColor()
        {
            return m_PlayColor;
        }

        public Point GetMove()
        {
            Thread.Sleep(2000);
            var possibleMoves = m_BoardModel.GetPossibleMovesFor(m_PlayColor);
            var randomContext = new Random();
            var chosenMove = possibleMoves[randomContext.Next(possibleMoves.Count)];
            return chosenMove;
        }
    }
}