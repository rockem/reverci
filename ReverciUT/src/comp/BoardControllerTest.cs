﻿using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using NUnit.Mocks;
using Reverci;
using Reverci.board;
using Reverci.comp;
using Reverci.model;
using Reverci.player;
using Reverci.view;

namespace ReverciUT.comp
{
    [TestFixture]
    public class BoardControllerTest
    {
        private readonly eCoinType[][] r_ExpectedData = ReverciBoardBuilder.CreateInitialBoardWithSize(4);
        private StubBoardView m_BoardView;
        private BoardController m_BoardController;
        private DynamicMock m_MockModel;
        private List<Point> m_PossibleMoves;
        private DynamicMock m_EventListener;

        private class StubBoardView : IBoardView
        {
            public eCoinType[][] m_Data;
            public IBoardViewEventListener m_Listener;
            public bool m_BoardWasUpdated;
            public List<Point> m_PreviewPoints;
            public eCoinType m_PreviewColor;

            public void UpdateBoardWith(eCoinType[][] i_BoardData)
            {
                m_Data = i_BoardData;
                m_BoardWasUpdated = true;
            }

            public void SetEventListener(IEventListener i_Listener)
            {
                m_Listener = (IBoardViewEventListener)i_Listener;
            }

            public void AddPreview(List<Point> i_Points, eCoinType i_Color)
            {
                m_PreviewPoints = i_Points;
                m_PreviewColor = i_Color;
            }

            public void EnableResizeEvents()
            {
            }
        }

        private class StubPlayer : IPlayer
        {
            private eCoinType m_Color;

            public void setColor(eCoinType i_Color)
            {
                m_Color = i_Color;
            }

            public void setBoardModel(IBoardModel i_Model)
            {
            }

            public bool IsAutomatic()
            {
                return false;
            }

            public Point GetMove()
            {
                return new Point(0, 0);
            }

            public eCoinType getColor()
            {
                return m_Color;
            }
        }

        [SetUp]
        public void setUp()
        {
            m_PossibleMoves = new List<Point> {new Point(2, 3)};
            m_BoardView = new StubBoardView();
            m_MockModel = new DynamicMock(typeof (IBoardModel));
            m_BoardController = new BoardController(m_BoardView);
            m_MockModel.SetReturnValue("GetPossibleMovesFor", m_PossibleMoves);
            m_MockModel.SetReturnValue("GetBoard", r_ExpectedData);
            m_MockModel.SetReturnValue("GetCurrentColor", eCoinType.Black);
            m_EventListener = new DynamicMock(typeof (IBoardEventListener));
            m_BoardController.setEventListener((IBoardEventListener)m_EventListener.MockInstance);
            m_BoardController.SetModel((IBoardModel)m_MockModel.MockInstance);
            m_BoardController.setPlayers(new StubPlayer(), new StubPlayer());
        }

        [Test]
        public void testShouldDelegateModelToView()
        {
            Assert.AreEqual(r_ExpectedData, m_BoardView.m_Data);
        }

        [Test]
        public void testShouldDelegateToModel()
        {
            m_MockModel.Expect("MakeMove",
                               new object[] { m_PossibleMoves[0].X, m_PossibleMoves[0].Y, eCoinType.Black });
            m_BoardView.m_Listener.DispatchMove(m_PossibleMoves[0].X, m_PossibleMoves[0].Y);
            m_MockModel.Verify();
        }

        [Test]
        public void testShouldUpdateViewOnMakeMove()
        {
            m_BoardController.DispatchMove(0, 0);
            Assert.AreEqual(r_ExpectedData, m_BoardView.m_Data);
        }

        [Test]
        public void testShouldNotDelegateNonValidMove()
        {
            m_MockModel.ExpectAndThrow("MakeMove", new NonValidMoveException(), null);
            m_BoardController.DispatchMove(1, 1);
            m_MockModel.Verify();
        }

        [Test]
        public void testShouldDelegatePossibleMovesToView()
        {
            m_BoardController.EnablePossibleMoves(true);
            Assert.AreEqual(eCoinType.Move, m_BoardView.m_Data[m_PossibleMoves[0].X][m_PossibleMoves[0].Y]);
        }

        [Test]
        public void testShouldNotDelegatePossibleMoves()
        {
            OthelloData.GetInstance().OthelloOptions.ShowValidMoves = false;
            var v_ExpectedMove = new Point(1, 1);
            m_PossibleMoves.Add(v_ExpectedMove);
            m_BoardController.DispatchMove(v_ExpectedMove.X, v_ExpectedMove.Y);
            Assert.IsTrue(eCoinType.Move != m_BoardView.m_Data[v_ExpectedMove.X][v_ExpectedMove.Y]);
        }

        [Test]
        public void testShouldDelegatePreviewToView()
        {
            m_BoardController.SetPreview(true);
            var v_ICanEatThese = new List<Point>();
            var preview = new Point(2, 2);
            v_ICanEatThese.Add(preview);
            var v_Move = new Point(3, 2);
            m_MockModel.ExpectAndReturn("GetPreviewFor", v_ICanEatThese,
                                        new object[] { v_Move.X, v_Move.Y, eCoinType.Black });
            m_BoardView.m_Listener.DispatchEnterSquare(v_Move.X, v_Move.Y);

            Assert.AreEqual(v_Move, m_BoardView.m_PreviewPoints[0]);
            Assert.AreEqual(preview, m_BoardView.m_PreviewPoints[1]);
        }

        [Test]
        public void testShouldNotDelegatePreview()
        {
            OthelloData.GetInstance().OthelloOptions.ShowPreview = false;
            m_BoardView.m_PreviewPoints = null;
            m_BoardView.m_Listener.DispatchEnterSquare(0, 0);
            Assert.IsNull(m_BoardView.m_PreviewPoints);
        }

        [Test]
        public void testShouldNotDelegatePreviewWhenThereIsNon()
        {
            m_BoardController.SetPreview(true);
            var v_Move = new Point(0, 0);
            m_MockModel.ExpectAndReturn("GetPreviewFor", new List<Point>(),
                                        new object[] { v_Move.X, v_Move.Y, eCoinType.Black });
            m_BoardView.m_Listener.DispatchEnterSquare(v_Move.X, v_Move.Y);
            Assert.IsNull(m_BoardView.m_PreviewPoints);
        }

        [Test]
        public void testShouldUpdateViewOnLeaveSquare()
        {
            m_BoardView.m_BoardWasUpdated = false;
            m_BoardView.m_Listener.DispatchLeaveSquare(0, 0);
            Assert.IsTrue(m_BoardView.m_BoardWasUpdated);
        }

        [Test]
        public void testShouldDelegteMoveToEventListener()
        {
            m_EventListener.Expect("DispatchLastMove", new object[] { 0, 0, eCoinType.Black });
            m_BoardController.DispatchMove(0, 0);
            m_EventListener.Verify();
        }

        [Test]
        public void testShouldDelegateCurrentStateToEventListener()
        {
            m_EventListener.Expect("DispatchCurrentState", new object[] { eStateType.WhiteTurn });
            m_BoardController.DispatchMove(0, 0);
            m_EventListener.Verify();
        }

        [Test]
        public void testShouldDelegateStateOnSetPlayers()
        {
            m_EventListener.Expect("DispatchCurrentState", new object[] { eStateType.BlackTurn });
            m_BoardController.setPlayers(new StubPlayer(), new StubPlayer());
            m_EventListener.Verify();
        }

        [Test]
        public void testShouldDelegatePieceQuantityToEventListenerOnMove()
        {
            setExpectationForPieceQuantity();
            m_BoardController.DispatchMove(0, 0);
            m_EventListener.Verify();
        }

        private void setExpectationForPieceQuantity()
        {
            var blackCount = 3;
            var whiteCount = 4;
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", blackCount, new object[] { eCoinType.Black });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", whiteCount, new object[] { eCoinType.White });
            m_EventListener.Expect("DispatchPieceQuantity", new object[] { blackCount, whiteCount });
        }

        [Test]
        public void testShouldDelegatePieceQuantityToEventListenerOnSetPlayers()
        {
            setExpectationForPieceQuantity();
            m_BoardController.setPlayers(new StubPlayer(), new StubPlayer());
            m_EventListener.Verify();
        }

        [Test]
        public void testShouldDelegateDrawToEventListenr()
        {
            m_MockModel.SetReturnValue("GetPossibleMovesFor", new List<Point>());
            m_MockModel.SetReturnValue("GetPossibleMovesFor", new List<Point>());
            m_EventListener.Expect("DispatchCurrentState", new object[] { eStateType.Draw });
            var count = 4;
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count, new object[] { eCoinType.Black });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count, new object[] { eCoinType.White });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count, new object[] { eCoinType.Black });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count, new object[] { eCoinType.White });
            m_BoardController.DispatchMove(0, 0);
            m_EventListener.Verify();
        }

        [Test]
        public void testShouldDelegateWinToEventListener()
        {
            m_MockModel.SetReturnValue("GetPossibleMovesFor", new List<Point>());
            m_EventListener.Expect("DispatchCurrentState", new object[] { eStateType.WhiteWin });
            var count = 4;
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count, new object[] { eCoinType.Black });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count + 2, new object[] { eCoinType.White });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count, new object[] { eCoinType.Black });
            m_MockModel.ExpectAndReturn("GetPieceCountOfType", count + 2, new object[] { eCoinType.White });
            m_BoardController.DispatchMove(0, 0);
            m_EventListener.Verify();
        }

        [Test]
        public void testShouldPlayBlackOnStartThenWhite()
        {
            var myMove = new Point(1, 1);
            m_MockModel.ExpectAndReturn("MakeMove", null,
                                        new object[] { myMove.X, myMove.Y, eCoinType.Black });
            m_MockModel.ExpectAndReturn("MakeMove", null,
                                        new object[] { myMove.X, myMove.Y, eCoinType.White });
            m_BoardController.DispatchMove(myMove.X, myMove.Y);
            m_BoardController.DispatchMove(myMove.X, myMove.Y);
            m_MockModel.Verify();
        }

        [Test]
        public void testShouldSetPlayerBlackOnSetModel()
        {
            m_BoardController.DispatchMove(0, 0);
            m_BoardController.SetModel((IBoardModel)m_MockModel.MockInstance);
            m_MockModel.ExpectAndReturn("MakeMove", null,
                                        new object[] { 0, 0, eCoinType.Black });
            m_BoardController.DispatchMove(0, 0);
            m_MockModel.Verify();
        }

        [Test]
        public void testShouldNotEndGameWhenOtherPlayerCanPlay()
        {
            m_MockModel.ExpectAndReturn("GetPossibleMovesFor", new List<Point>(), eCoinType.White);
            var moves = new List<Point>
                           {
                               new Point(1, 1)
                           };
            m_MockModel.ExpectAndReturn("GetPossibleMovesFor", moves, eCoinType.Black);
            m_EventListener.Expect("DispatchCurrentState", new object[] { eStateType.BlackTurn });
            m_BoardController.DispatchMove(0, 0);
            m_MockModel.Verify();
        }
    }
}