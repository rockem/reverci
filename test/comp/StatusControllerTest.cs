using System.Collections.Generic;
using NUnit.Framework;
using Othello.model;
using Othello.view;

namespace Othello.comp
{
    [TestFixture]
    public class StatusControllerTest
    {
        private StubStatusView m_View;
        private StatusController m_StatusController;

        private class StubStatusView : IStatusView
        {
            public eColorType m_CurrentColor;
            public int m_BlackCount;
            public int m_WhiteCount;
            public List<string[]> m_MovesList;

            public void UpdateCurrentColor(eColorType i_Color)
            {
                m_CurrentColor = i_Color;
            }

            public void UpdatePieceQuantity(int i_BlackCount, int i_WhiteCount)
            {
                m_BlackCount = i_BlackCount;
                m_WhiteCount = i_WhiteCount;
            }

            public void UpdateMovesList(List<string[]> i_MovesList)
            {
                m_MovesList = i_MovesList;
            }
        }

        [SetUp]
        public void setUp()
        {
            m_View = new StubStatusView();
            m_StatusController = new StatusController(m_View);
        }

        [Test]
        public void testShouldDelegateTurnStateToView()
        {
            m_StatusController.UpdateCurrentState(eStateType.WhiteTurn);
            Assert.AreEqual(eColorType.White, m_View.m_CurrentColor);
        }

        [Test]
        public void testShouldDelegatePieceCountToView()
        {
            var whiteCount = 4;
            var blackCount = 5;
            m_StatusController.UpdatePiecesQunatity(blackCount, whiteCount);
            Assert.AreEqual(blackCount, m_View.m_BlackCount);
            Assert.AreEqual(whiteCount, m_View.m_WhiteCount);
        }

        [Test]
        public void testShouldDelegateMovesListToView()
        {
            var moves = new List<string[]>
                        {
                            new[] { "1", "Black", "B5" },
                            new[] { "2", "White", "E6" },
                        };
            m_StatusController.LogMove(1, 4, eSquareType.Black);
            m_StatusController.LogMove(4, 5, eSquareType.White);
            Assert.AreEqual(moves, m_View.m_MovesList);
        }
    }
}