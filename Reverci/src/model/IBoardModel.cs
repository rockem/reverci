using System.Collections.Generic;
using System.Drawing;
using Reverci.board;

namespace Reverci.model
{
    public interface IBoardModel
    {
        void MakeMove(int i_X, int i_Y, eCoinType i_Color);

        eCoinType[][] GetBoard();

        List<Point> GetPossibleMovesFor(eCoinType i_Color);

        List<Point> GetPreviewFor(int i_X, int i_Y, eCoinType i_Color);

        int GetPieceCountOfType(eCoinType i_PieceType);
    }
}