using System.Collections.Generic;
using System.Drawing;

namespace Reverci.model
{
    public interface IBoardModel
    {
        void MakeMove(int i_X, int i_Y, eSquareType i_Color);

        eSquareType[][] GetBoard();

        List<Point> GetPossibleMovesFor(eSquareType i_Color);

        List<Point> GetPreviewFor(int i_X, int i_Y, eSquareType i_Color);

        int GetPieceCountOfType(eSquareType i_PieceType);
    }
}