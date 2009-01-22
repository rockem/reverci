using System.Collections.Generic;
using System.Drawing;
using Reverci.comp;
using Reverci.model;

namespace Reverci.view
{
    public interface IBoardView
    {
        void updateBoardWith(eSquareType[][] i_BoardData);

        void setEventListener(IBoardViewEventListener i_Listener);

        void addPreview(List<Point> points, eSquareType color);
    }
}