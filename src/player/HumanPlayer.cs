using System;
using System.Drawing;
using Othello.model;

namespace Othello.player
{
    internal class HumanPlayer : IPlayer
    {
        private eSquareType m_PlayColor;

        public Point GetMove()
        {
            throw new NotImplementedException();
        }

        public eSquareType getColor()
        {
            return m_PlayColor;
        }

        public void setColor(eSquareType i_Color)
        {
            m_PlayColor = i_Color;
        }

        public void setBoardModel(IBoardModel i_Model)
        {
        }

        public bool IsAutomatic()
        {
            return false;
        }
    }
}