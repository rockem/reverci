﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Reverci.model
{
    public class ReverciBoardModel : IBoardModel
    {
        private readonly eSquareType[][] r_BoardData;
        private readonly int r_BoardSize;

        public ReverciBoardModel(eSquareType[][] i_BoardData)
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

        private abstract class ScanTemplate
        {
            protected struct Direction
            {
                public int XDirection { get; set; }

                public int YDirection { get; set; }
            }

            private readonly ReverciBoardModel r_BoardModel;
            private readonly eSquareType r_Color;

            protected ScanTemplate(ReverciBoardModel i_BoardModel, eSquareType i_Color)
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
                                if (r_BoardModel.r_BoardData[currentX][currentY] == r_Color)
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
                return r_BoardModel.r_BoardData[currentX][currentY] ==
                       SquareTypeUtil.GetOtherColor(r_Color);
            }

            protected abstract Direction[] listOfDirections();

            protected abstract bool isInBounds(int x, int y);

            protected int maxPosition()
            {
                return r_BoardModel.r_BoardSize - 1;
            }
        }

        private class ScanForwardTemplate : ScanTemplate
        {
            public ScanForwardTemplate(ReverciBoardModel i_BoardModel, eSquareType i_Color) :
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
            public ScanBackwardTemplate(ReverciBoardModel i_BoardModel, eSquareType i_Color) :
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
            public ScanDownBackDiagonalTemplate(ReverciBoardModel i_BoardModel, eSquareType i_Color) :
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
            public ScanUpForwardDiagonalTemplate(ReverciBoardModel i_BoardModel, eSquareType i_Color) :
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

        private List<Point> whoCanIEatFrom(int i_X, int i_Y, eSquareType i_Color)
        {
            var listToEat = new List<Point>();
            listToEat.AddRange(new ScanForwardTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanBackwardTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanDownBackDiagonalTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));
            listToEat.AddRange(new ScanUpForwardDiagonalTemplate(this, i_Color).whoToEatFrom(i_X, i_Y));

            return listToEat;
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

        public int GetPieceCountOfType(eSquareType i_SquareType)
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
    }
}