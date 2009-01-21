using System.Collections.Generic;

namespace Othello.view
{
    public interface IStatusView
    {
        void UpdateCurrentColor(eColorType i_Color);

        void UpdatePieceQuantity(int i_BlackCount, int i_WhiteCount);

        void UpdateMovesList(List<string[]> i_MovesList);
    }
}