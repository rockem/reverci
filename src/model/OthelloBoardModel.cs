using System;
using System.Collections.Generic;
using System.Drawing;

namespace Reverci.model
{
    [Serializable]
    internal class OthelloBoardModel : IBoardModel
    {
        private readonly eSquareType[][] r_BoardData;
        private readonly int r_BoardSize;

        public OthelloBoardModel(eSquareType[][] i_BoardData)
        {
            r_BoardData = i_BoardData;
            r_BoardSize = r_BoardData.Length;
        }

        public void MakeMove(int x, int y, eSquareType i_Color)
        {
            validateMove(x, y, i_Color);
            r_BoardData[x][y] = i_Color;
            eatOtherColorFrom(x, y, i_Color);
        }

        private void validateMove(int x, int y, eSquareType i_Color)
        {
            if (!thereIsSomethingToEatOn(x, y, i_Color))
            {
                throw new NonValidMoveException();
            }
        }

        private void eatOtherColorFrom(int x, int y, eSquareType i_Color)
        {
            foreach (var position in whoCanIEatFrom(x, y, i_Color))
            {
                r_BoardData[position.X][position.Y] = i_Color;
            }
        }

        private abstract class ScanCommand
        {
            protected struct Direction
            {
                public int XDirection { get; set; }

                public int YDirection { get; set; }
            }

            private readonly OthelloBoardModel r_BoardModel;
            private readonly eSquareType r_Color;

            protected ScanCommand(OthelloBoardModel i_BoardModel, eSquareType i_Color)
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
                    while (isInBounds(currentX, currentY))
                    {
                        currentX += vertDirection;
                        currentY += horizDirection;
                        if (isOtherColorOn(currentX, currentY))
                        {
                            listToEat.Add(new Point(currentX, currentY));
                        }
                        else
                        {
                            break;
                        }
                    }

                    clearIfCurrentColor(currentX, currentY, listToEat);
                    allICanEat.AddRange(listToEat);
                }

                return allICanEat;
            }

            private bool isOtherColorOn(int currentX, int currentY)
            {
                return r_BoardModel.r_BoardData[currentX][currentY] == r_BoardModel.getOtherColor(r_Color);
            }

            protected abstract Direction[] listOfDirections();

            protected abstract bool isInBounds(int x, int y);

            private void clearIfCurrentColor(int x, int y, ICollection<Point> listToEat)
            {
                if (r_BoardModel.r_BoardData[x][y] != r_Color)
                {
                    listToEat.Clear();
                }
            }

            protected int maxPosition()
            {
                return r_BoardModel.r_BoardSize - 1;
            }
        }

        private class ScanForwardCommand : ScanCommand
        {
            public ScanForwardCommand(OthelloBoardModel i_BoardModel, eSquareType i_Color) :
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
                return x < maxPosition() && y < maxPosition();
            }
        }

        private class ScanBackwardCommand : ScanCommand
        {
            public ScanBackwardCommand(OthelloBoardModel i_BoardModel, eSquareType i_Color) :
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
                return x > 0 && y > 0;
            }
        }

        private class ScanDownBackDiagonalCommand : ScanCommand
        {
            public ScanDownBackDiagonalCommand(OthelloBoardModel i_BoardModel, eSquareType i_Color) :
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
                return x > 0 && y < maxPosition();
            }
        }

        private class ScanUpForwardDiagonalCommand : ScanCommand
        {
            public ScanUpForwardDiagonalCommand(OthelloBoardModel i_BoardModel, eSquareType i_Color) :
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
                return x < maxPosition() && y > 0;
            }
        }

        private List<Point> whoCanIEatFrom(int i_X, int i_Y, eSquareType i_Color)
        {
            var listToEat = new List<Point>();
            listToEat.AddRange(new ScanForwardCommand(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanBackwardCommand(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanDownBackDiagonalCommand(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanUpForwardDiagonalCommand(this, i_Color).whoToEatFrom(i_X, i_Y));

            return listToEat;
        }

        private eSquareType getOtherColor(eSquareType i_Color)
        {
            eSquareType color;
            if (i_Color == eSquareType.Black)
            {
                color = eSquareType.White;
            }
            else
            {
                color = eSquareType.Black;
            }

            return color;
        }

        public eSquareType[][] GetBoard()
        {
            return createACopyOfTheBoard();
        }

        private eSquareType[][] createACopyOfTheBoard()
        {
            var boardCopy = new eSquareType[r_BoardData.Length][];
            for (var i = 0; i < r_BoardData.Length; i++)
            {
                boardCopy[i] = new eSquareType[r_BoardData.Length];
                Array.Copy(r_BoardData[i], boardCopy[i], r_BoardData.Length);
            }

            return boardCopy;
        }

        public List<Point> GetPossibleMovesFor(eSquareType i_Color)
        {
            var possibleMoves = new List<Point>();
            for (var i = 0; i < r_BoardSize; i++)
            {
                for (var j = 0; j < r_BoardSize; j++)
                {
                    if (thereIsSomethingToEatOn(i, j, i_Color))
                    {
                        possibleMoves.Add(new Point(i, j));
                    }
                }
            }

            return possibleMoves;
        }

        public List<Point> GetPreviewFor(int i_X, int i_Y, eSquareType i_Color)
        {
            var preview = new List<Point>();
            if (emptySquareOn(i_X, i_Y))
            {
                preview = whoCanIEatFrom(i_X, i_Y, i_Color);
            }

            return preview;
        }

        private bool emptySquareOn(int i, int j)
        {
            return r_BoardData[i][j] == eSquareType.Empty;
        }

        private bool thereIsSomethingToEatOn(int i, int j, eSquareType i_Color)
        {
            var result = false;
            if (emptySquareOn(i, j))
            {
                result = whoCanIEatFrom(i, j, i_Color).Count > 0;
            }

            return result;
        }

        public int getBlackPieceCount()
        {
            return countPieceOfType(eSquareType.Black);
        }

        private int countPieceOfType(eSquareType i_SquareType)
        {
            var counter = 0;
            for (var i = 0; i < r_BoardSize; i++)
            {
                for (var j = 0; j < r_BoardSize; j++)
                {
                    if (r_BoardData[i][j] == i_SquareType)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        public int getWhitePieceCount()
        {
            return countPieceOfType(eSquareType.White);
        }
    }
}