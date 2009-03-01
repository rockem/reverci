using System.Drawing;
using Reverci.board;
using Reverci.model;

namespace Reverci.player
{
    public abstract class AbstractPlayer : IPlayer
    {
        private eCoinType m_Color;
        private IBoardModel m_BoardModel;

        public void setColor(eCoinType i_Color)
        {
            m_Color = i_Color;
        }

        public void setBoardModel(IBoardModel i_Model)
        {
            m_BoardModel = i_Model;
        }

        public abstract bool IsAutomatic();

        public abstract Point GetMove();

        public eCoinType getColor()
        {
            return m_Color;
        }

        protected IBoardModel getBoardModel()
        {
            return m_BoardModel;
        }
    }
}