using System.Drawing;
using Othello.model;

namespace Othello.player
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