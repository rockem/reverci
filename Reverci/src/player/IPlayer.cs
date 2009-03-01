using System.Drawing;
using Reverci.board;
using Reverci.model;

namespace Reverci.player
{
    public interface IPlayer
    {
        void setColor(eCoinType i_Color);

        void setBoardModel(IBoardModel i_Model);

        bool IsAutomatic();

        Point GetMove();

        eCoinType getColor();
    }
}