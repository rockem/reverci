using System.Collections.Generic;
using System.Drawing;
using Reverci.board;

namespace Reverci.model
{
    public class ReverciBoardModel : IBoardModel
    {
        private readonly Board r_Board;

        public ReverciBoardModel(eCoinType[][] i_BoardData)
        {
            r_Board = new Board(i_BoardData);
        }

        public void MakeMove(int i_X, int i_Y, eCoinType i_Color)
        {
            validateMove(i_X, i_Y, i_Color);
            r_Board.Set(i_Color, i_X, i_Y);
            eatOtherColorFrom(i_X, i_Y, i_Color);
        }

        private void validateMove(int i_X, int i_Y, eCoinType i_Color)
        {
            if (!thereIsSomethingToEatOn(i_X, i_Y, i_Color))
            {
                throw new NonValidMoveException();
            }
        }

        private void eatOtherColorFrom(int i_X, int i_Y, eCoinType i_Color)
        {
            foreach (var position in whoCanIEatFrom(i_X, i_Y, i_Color))
            {
                r_Board.Set(i_Color, position.X, position.Y);
            }
        }

        private abstract class ScanTemplate
        {
            protected struct Direction
            {
                public int XDirection { get; set; }

                public int YDirection { get; set; }
            }

            private readonly ReverciBoardModel r_BoardModel;
            private readonly eCoinType r_Color;

            protected ScanTemplate(ReverciBoardModel i_BoardModel, eCoinType i_Color)
            {
                r_BoardModel = i_BoardModel;
                r_Color = i_Color;
            }

            public virtual IEnumerable<Point> whoToEatFrom(int i_X, int i_Y)
            {
                var allICanEat = new List<Point>();
                var directions = listOfDirections();
                for (var i = 0; i < directions.Length; i++)
                {
                    var vertDirection = directions[i].XDirection;
                    var horizDirection = directions[i].YDirection;
                    var currentX = i_X;
                    var currentY = i_Y;
                    var listToEat = new List<Point>();
                    while (true)
                    {
                        currentX += vertDirection;
                        currentY += horizDirection;
                        if (isInBounds(currentX, currentY))
                        {
                            if (isOtherColorOn(currentX, currentY))
                            {
                                listToEat.Add(new Point(currentX, currentY));
                            }
                            else
                            {
                                if (r_BoardModel.r_Board.Get(currentX, currentY) == r_Color)
                                {
                                    allICanEat.AddRange(listToEat);
                                }

                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                return allICanEat;
            }

            private bool isOtherColorOn(int currentX, int currentY)
            {
                return r_BoardModel.r_Board.Get(currentX, currentY) ==
                       SquareTypeUtil.GetOtherColor(r_Color);
            }

            protected abstract Direction[] listOfDirections();

            protected abstract bool isInBounds(int x, int y);

            protected int maxPosition()
            {
                return r_BoardModel.r_Board.Size() - 1;
            }
        }

        private class ScanForwardTemplate : ScanTemplate
        {
            public ScanForwardTemplate(ReverciBoardModel i_BoardModel, eCoinType i_Color) :
                base(i_BoardModel, i_Color)
            {
            }

            protected override Direction[] listOfDirections()
            {
                return new[]
                           {
                               new Direction { XDirection = 1, YDirection = 0 },
                               new Direction { XDirection = 0, YDirection = 1 },
                               new Direction { XDirection = 1, YDirection = 1 },
                           };
            }

            protected override bool isInBounds(int x, int y)
            {
                return x <= maxPosition() && y <= maxPosition();
            }
        }

        private class ScanBackwardTemplate : ScanTemplate
        {
            public ScanBackwardTemplate(ReverciBoardModel i_BoardModel, eCoinType i_Color) :
                base(i_BoardModel, i_Color)
            {
            }

            protected override Direction[] listOfDirections()
            {
                return new[]
                           {
                               new Direction { XDirection = -1, YDirection = 0 },
                               new Direction { XDirection = 0, YDirection = -1 },
                               new Direction { XDirection = -1, YDirection = -1 },
                           };
            }

            protected override bool isInBounds(int x, int y)
            {
                return x >= 0 && y >= 0;
            }
        }

        private class ScanDownBackDiagonalTemplate : ScanTemplate
        {
            public ScanDownBackDiagonalTemplate(ReverciBoardModel i_BoardModel, eCoinType i_Color) :
                base(i_BoardModel, i_Color)
            {
            }

            protected override Direction[] listOfDirections()
            {
                return new[]
                           {
                               new Direction { XDirection = -1, YDirection = 1 },
                           };
            }

            protected override bool isInBounds(int x, int y)
            {
                return x >= 0 && y <= maxPosition();
            }
        }

        private class ScanUpForwardDiagonalTemplate : ScanTemplate
        {
            public ScanUpForwardDiagonalTemplate(ReverciBoardModel i_BoardModel, eCoinType i_Color) :
                base(i_BoardModel, i_Color)
            {
            }

            protected override Direction[] listOfDirections()
            {
                return new[]
                           {
                               new Direction { XDirection = 1, YDirection = -1 },
                           };
            }

            protected override bool isInBounds(int x, int y)
            {
                return x <= maxPosition() && y >= 0;
            }
        }

        private List<Point> whoCanIEatFrom(int i_X, int i_Y, eCoinType i_Color)
        {
            var listToEat = new List<Point>();
            listToEat.AddRange(new ScanForwardTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanBackwardTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanDownBackDiagonalTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanUpForwardDiagonalTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));

            return listToEat;
        }

        public eCoinType[][] GetBoard()
        {
            return r_Board.GetData();
        }

        public List<Point> GetPossibleMovesFor(eCoinType i_Color)
        {
            var possibleMoves = new List<Point>();
            for (var i = 0; i < r_Board.Size(); i++)
            {
                for (var j = 0; j < r_Board.Size(); j++)
                {
                    if (thereIsSomethingToEatOn(i, j, i_Color))
                    {
                        possibleMoves.Add(new Point(i, j));
                    }
                }
            }

            return possibleMoves;
        }

        public List<Point> GetPreviewFor(int i_X, int i_Y, eCoinType i_Color)
        {
            var preview = new List<Point>();
            if (emptySquareOn(i_X, i_Y))
            {
                preview = whoCanIEatFrom(i_X, i_Y, i_Color);
            }

            return preview;
        }

        private bool emptySquareOn(int i_X, int i_Y)
        {
            return r_Board.Get(i_X, i_Y) == eCoinType.Empty;
        }

        private bool thereIsSomethingToEatOn(int i, int j, eCoinType i_Color)
        {
            var result = false;
            if (emptySquareOn(i, j))
            {
                result = whoCanIEatFrom(i, j, i_Color).Count > 0;
            }

            return result;
        }

        public int GetPieceCountOfType(eCoinType i_CoinType)
        {
            var counter = 0;
            foreach (Coin coin in r_Board)
            {
                if (coin.CoinType == i_CoinType)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}