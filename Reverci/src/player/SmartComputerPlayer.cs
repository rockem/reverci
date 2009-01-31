using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Reverci.model;

namespace Reverci.player
{
    public class SmartComputerPlayer : AbstractPlayer
    {
        private const int MINUS_INF = -99999;
        private const int PLUS_INF = 99999;

        private readonly int r_Depth;

        public SmartComputerPlayer(int i_Depth)
        {
            r_Depth = i_Depth;
        }

        public override bool IsAutomatic()
        {
            return true;
        }

        public override Point GetMove()
        {
            Thread.Sleep(200);
            var possibleMoves = createShuffeldListFrom(getBoardModel().GetPossibleMovesFor(getColor()));
            var bestMove = possibleMoves[0];
            var result = MINUS_INF;
            foreach (var move in possibleMoves)
            {
                var res = alphaBeta(getBoardModel().GetBoard(), getColor(), move, r_Depth, MINUS_INF, PLUS_INF);
                if (res > result)
                {
                    result = res;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private List<Point> createShuffeldListFrom(IList<Point> i_List)
        {
            var randomContext = new Random();
            var count = i_List.Count;
            var newList = new List<Point>();
            for (var i = 0; i < count; i++)
            {
                var index = randomContext.Next(i_List.Count);
                newList.Add(i_List[index]);
                i_List.RemoveAt(index);
            }

            return newList;
        }

        private int alphaBeta(eSquareType[][] i_Board, eSquareType i_Color, Point i_Move, int i_Depth, int a, int b)
        {
            var model = new ReverciBoardModel(i_Board);
            model.MakeMove(i_Move.X, i_Move.Y, i_Color);
            var otherColor = SquareTypeUtil.GetOtherColor(i_Color);
            var possibleMoves = model.GetPossibleMovesFor(otherColor);
            if (possibleMoves.Count == 0 || i_Depth == 0)
            {
                a = model.GetPieceCountOfType(getColor()) -
                    model.GetPieceCountOfType(SquareTypeUtil.GetOtherColor(getColor())) + i_Depth;
            }
            else
            {
                foreach (var move in possibleMoves)
                {
                    a = Math.Max(a, alphaBeta(model.GetBoard(), otherColor, move, i_Depth - 1, -b, -a));
                    if (b <= a)
                    {
                        break;
                    }
                }
            }

            return a;
        }
    }
}