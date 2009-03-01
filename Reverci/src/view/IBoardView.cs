using System.Collections.Generic;
using System.Drawing;
using Reverci.board;

namespace Reverci.view
{
    public interface IBoardView : IComponentView
    {
        void UpdateBoardWith(eCoinType[][] i_BoardData);

        void AddPreview(List<Point> i_Points, eCoinType i_Color);

        void EnableResizeEvents();
    }
}