using System.Drawing;
using NUnit.Framework;
using Reverci.board;
using Reverci.model;

namespace ReverciUT.model
{
    [TestFixture]
    public class ReverciBoardModelTest_MakeMove
    {
        private const int X = 0;
        private const int B = 1;
        private const int W = 2;

        [Test]
        public void testShouldNoRetrievePossibleMoveInExistingPiece()
        {
            var m_BoardModel = createModelFrom(
                new[]
                    {
                        new[] { X, X, X, X },
                        new[] { X, W, B, X },
                        new[] { B, B, B, B },
                        new[] { X, W, X, X },
                    }
                );

            var validMoves = m_BoardModel.GetPreviewFor(1, 3, eCoinType.White);
            Assert.IsTrue(!validMoves.Contains(new Point(1, 2)));
        }

        [Test]
        public void testShouldNotAllowMoveOnExistingPiece()
        {
            var m_BoardModel = createModelFrom(
                new[]
                    {
                        new[] { X, X, X, X },
                        new[] { X, W, B, X },
                        new[] { X, B, W, B },
                        new[] { X, X, X, W },
                    }
                );

            try
            {
                m_BoardModel.MakeMove(3, 2, eCoinType.Black);
                Assert.Fail();
            }
            catch (NonValidMoveException)
            {
                // pass
            }
        }

        [Test]
        public void testShouldMakeUpMoveWhenOnFirstColumn()
        {
            var m_BoardModel = createModelFrom(
                new[]
                    {
                        new[] { B, X, X },
                        new[] { W, X, X },
                        new[] { X, X, X }
                    }
                );
            try
            {
                m_BoardModel.MakeMove(0, 2, eCoinType.Black);
                //pass
            }
            catch (NonValidMoveException)
            {
                Assert.Fail();
            }
        }

        private IBoardModel createModelFrom(int[][] ints)
        {
            var boardSize = ints.Length;
            var arr = new eCoinType[boardSize][];
            for (var i = 0; i < boardSize; i++)
            {
                arr[i] = new eCoinType[boardSize];
                for (var j = 0; j < boardSize; j++)
                {
                    arr[i][j] = convertToSquareType(ints[j][i]);
                }
            }
            return new ReverciBoardModel(arr);
        }

        private eCoinType convertToSquareType(int i)
        {
            switch (i)
            {
                case B:
                    return eCoinType.Black;
                case W:
                    return eCoinType.White;
            }
            return eCoinType.Empty;
        }

        [Test]
        public void testShouldEatForwardOnEdge()
        {
            var m_BoardModel = createModelFrom(
                new[]
                    {
                        new[] { X, X, X, X },
                        new[] { X, W, B, X },
                        new[] { X, W, W, X },
                        new[] { X, X, B, W },
                    }
                );

            try
            {
                m_BoardModel.MakeMove(1, 3, eCoinType.White);
            }
            catch (NonValidMoveException)
            {
                Assert.Fail();
            }
        }
    }
}