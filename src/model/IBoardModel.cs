﻿using System.Collections.Generic;
using System.Drawing;

namespace Othello.model
{
    public interface IBoardModel
    {
        void MakeMove(int i_X, int i_Y, eSquareType i_Color);

        eSquareType[][] GetBoard();

        List<Point> GetPossibleMovesFor(eSquareType i_Color);

        List<Point> GetPreviewFor(int i_X, int i_Y, eSquareType i_Color);

        int getWhitePieceCount();

        int getBlackPieceCount();
    }
}