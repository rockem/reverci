using System.Collections.Generic;
using System.Drawing;
using Reverci.model;

namespace Reverci.view
{
    public interface IBoardView : IComponentView
    {
        void updateBoardWith(eSquareType[][] i_BoardData);

        void addPreview(List<Point> i_Points, eSquareType i_Color);

        void EnableResizeEvents();
    }
}