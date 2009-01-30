using System.Collections.Generic;
using System.Drawing;
using Reverci.model;

namespace Reverci.view
{
    public interface IBoardView : IComponentView
    {
        void UpdateBoardWith(eSquareType[][] i_BoardData);

        void AddPreview(List<Point> i_Points, eSquareType i_Color);

        void EnableResizeEvents();
    }
}