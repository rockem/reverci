using System.Collections.Generic;
using System.Drawing;
using Othello.comp;
using Othello.model;

namespace Othello.view
{
    public interface IBoardView
    {
        void updateBoardWith(eSquareType[][] i_BoardData);

        void setEventListener(IBoardViewEventListener i_Listener);

        void addPreview(List<Point> points, eSquareType color);
    }
}