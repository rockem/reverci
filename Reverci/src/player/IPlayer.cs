using System.Drawing;
using Reverci.model;

namespace Reverci.player
{
    public interface IPlayer
    {
        void setColor(eSquareType i_Color);

        void setBoardModel(IBoardModel i_Model);

        bool IsAutomatic();

        Point GetMove();

        eSquareType getColor();
    }
}