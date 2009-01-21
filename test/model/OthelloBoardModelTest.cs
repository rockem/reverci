using System.Drawing;
using NUnit.Framework;

namespace Othello.model
{
    [TestFixture]
    public class OthelloBoardModelTest
    {
        private const int k_FirstMoveX = 3;
        private const int k_FirstMoveY = 2;
        private OthelloBoardModel m_BoardModel;

        [SetUp]
        public void setUp()
        {
            m_BoardModel = new OthelloBoardModel(BoardState.CreateInitialBoardWithSize(4));
        }

        [Test]
        public void testShouldSaveInitialState()
        {
            var boardModel = new OthelloBoardModel(BoardState.CreateInitialBoardWithSize(4));
            Assert.AreEqual(BoardState.CreateInitialBoardWithSize(4), boardModel.GetBoard());
        }

        private void makeFirstMove()
        {
            m_BoardModel.MakeMove(k_FirstMoveX, k_FirstMoveY, eSquareType.Black);
        }

        [Test]
        public void testShouldEatOpponentsHorizontally()
        {
            m_BoardModel.MakeMove(3, 2, eSquareType.Black);
            Assert.AreEqual(eSquareType.Black, m_BoardModel.GetBoard()[2][2]);
        }

        [Test]
        public void testShouldEatOpponentsVertically()
        {
            m_BoardModel.MakeMove(2, 3, eSquareType.Black);
            Assert.AreEqual(eSquareType.Black, m_BoardModel.GetBoard()[2][2]);
        }

        [Test]
        public void testShouldEatOpponentsLeftDiagnollay()
        {
            m_BoardModel.MakeMove(3, 2, eSquareType.Black); // prepare board
            m_BoardModel.MakeMove(3, 3, eSquareType.White); // our move
            Assert.AreEqual(eSquareType.White, m_BoardModel.GetBoard()[2][2]);
        }

        [Test]
        public void testShouldEatOnlyRowsBoundWithItsColor()
        {
            m_BoardModel.MakeMove(3, 2, eSquareType.Black); // prepare board
            m_BoardModel.MakeMove(3, 3, eSquareType.White); // our move
            Assert.AreEqual(eSquareType.Black, m_BoardModel.GetBoard()[3][2]);
        }

        [Test]
        public void testShouldEatInMultipleDirections()
        {
            // preparing board
            m_BoardModel.MakeMove(3, 2, eSquareType.Black);
            m_BoardModel.MakeMove(1, 3, eSquareType.White);
            m_BoardModel.MakeMove(0, 2, eSquareType.Black);

            // our move
            m_BoardModel.MakeMove(3, 1, eSquareType.White);
            Assert.AreEqual(eSquareType.White, m_BoardModel.GetBoard()[2][2]);
            Assert.AreEqual(eSquareType.White, m_BoardModel.GetBoard()[2][1]);
        }

        [Test]
        public void testShouldRetrievePossibleMoves()
        {
            var possibleMoves = m_BoardModel.GetPossibleMovesFor(eSquareType.Black);
            Assert.IsTrue(possibleMoves.Contains(new Point(3, 2)));
            Assert.IsTrue(possibleMoves.Contains(new Point(2, 3)));
            Assert.IsTrue(possibleMoves.Contains(new Point(0, 1)));
            Assert.IsTrue(possibleMoves.Contains(new Point(1, 0)));
        }

        [Test]
        public void testShouldThrowOnIllegalMove()
        {
            var v_IllegalMove = new Point(0, 0);
            try
            {
                m_BoardModel.MakeMove(v_IllegalMove.X, v_IllegalMove.Y, eSquareType.Black);
                Assert.Fail();
            }
            catch (NonValidMoveException)
            {
                // pass
            }
        }

        [Test]
        public void testShouldRetrievePreviewForMove()
        {
            Assert.IsTrue(m_BoardModel.GetPreviewFor(3, 2, eSquareType.Black).Contains(new Point(2, 2)));
        }

        [Test]
        public void testShouldRetrievePieceCount()
        {
            makeFirstMove();
            Assert.AreEqual(4, m_BoardModel.getBlackPieceCount());
            Assert.AreEqual(1, m_BoardModel.getWhitePieceCount());
        }

        [Test]
        public void testShouldNoRetrievePossibleMoveInExistingPiece()
        {
            //preparing board
            m_BoardModel.MakeMove(3, 2, eSquareType.Black);
            m_BoardModel.MakeMove(1, 3, eSquareType.White);
            m_BoardModel.MakeMove(0, 2, eSquareType.Black);

            var validMoves = m_BoardModel.GetPreviewFor(1, 3, eSquareType.White);
            Assert.IsTrue(!validMoves.Contains(new Point(1, 2)));
        }

        [Test]
        public void testShouldNotAllowMoveOnExistingPiece()
        {
            //preparing board
            m_BoardModel.MakeMove(3, 2, eSquareType.Black);
            m_BoardModel.MakeMove(3, 3, eSquareType.White);

            try
            {
                m_BoardModel.MakeMove(3, 2, eSquareType.Black);
                Assert.Fail();
            } catch(NonValidMoveException)
            {
                // pass
            }
        }
    }
}