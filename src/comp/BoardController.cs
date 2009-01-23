using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Reverci.model;
using Reverci.player;
using Reverci.view;

namespace Reverci.comp
{
    public class BoardController : IBoardViewEventListener
    {
        private readonly IBoardView r_BoardView;
        private readonly Dictionary<eSquareType, IPlayer> r_Players = new Dictionary<eSquareType, IPlayer>();
        private IBoardModel m_BoardModel;
        private IBoardEventListener m_EventListener = new DummyBoardEventListener();
        private eSquareType m_CurrentPlayer;
        private bool m_LockHumanPlay;
        private Point m_LastMove;

        public BoardController(IBoardView i_View)
        {
            r_BoardView = i_View;
            r_BoardView.setEventListener(this);
            initializeCurrentPlayer();
        }

        private void initializeCurrentPlayer()
        {
            m_CurrentPlayer = eSquareType.Black;
        }

        public IBoardView GetView()
        {
            return r_BoardView;
        }

        public void SetModel(IBoardModel i_BoardModel)
        {
            m_BoardModel = i_BoardModel;
            initializeCurrentPlayer();
        }

        private void dispatchCurrentState()
        {
            if (m_BoardModel.GetPossibleMovesFor(getCurrentPlayerColor()).Count == 0)
            {
                var blackCount = m_BoardModel.getBlackPieceCount();
                var whiteCount = m_BoardModel.getWhitePieceCount();
                if (blackCount > whiteCount)
                {
                    m_EventListener.DispatchCurrentState(eStateType.BlackWin);
                }
                else if (blackCount < whiteCount)
                {
                    m_EventListener.DispatchCurrentState(eStateType.WhiteWin);
                }
                else
                {
                    m_EventListener.DispatchCurrentState(eStateType.Draw);
                }

                OthelloData.GetInstance().Statistics.AddGameScore(
                    blackCount,
                    whiteCount,
                    ePlayerType.Human,
                    ePlayerType.Human);
            }
            else if (getCurrentPlayerColor() == eSquareType.Black)
            {
                m_EventListener.DispatchCurrentState(eStateType.BlackTurn);
            }
            else
            {
                m_EventListener.DispatchCurrentState(eStateType.WhiteTurn);
            }
        }

        public void DispatchMove(int x, int y)
        {
            if (!m_LockHumanPlay)
            {
                handleMove(x, y);
                automaticPlay();
            }
        }

        private void dispathPieceQuantity()
        {
            m_EventListener.DispatchPieceQuantity(
                m_BoardModel.getBlackPieceCount(), m_BoardModel.getWhitePieceCount());
        }

        public void DispatchLeaveSquare(int x, int y)
        {
            updateView();
        }

        public void DispatchEnterSquare(int x, int y)
        {
            if (getGameOptions().ShowPreview)
            {
                var previewPoints = m_BoardModel.GetPreviewFor(x, y, getCurrentPlayerColor());
                if (previewPoints.Count > 0)
                {
                    previewPoints.Insert(0, new Point(x, y));
                    r_BoardView.addPreview(previewPoints, getCurrentPlayerColor());
                }
            }
        }

        private void updateView()
        {
            var board = m_BoardModel.GetBoard();
            if (getGameOptions().ShowValidMoves)
            {
                addPossibleMovesOn(board);
            }

            r_BoardView.updateBoardWith(board);
        }

        private void addPossibleMovesOn(eSquareType[][] board)
        {
            foreach (var move in m_BoardModel.GetPossibleMovesFor(getCurrentPlayerColor()))
            {
                board[move.X][move.Y] = eSquareType.Move;
            }
        }

        public void EnablePossibleMoves(bool i_PossibleMoves)
        {
            getGameOptions().ShowValidMoves = i_PossibleMoves;
            updateView();
        }

        public void SetPreview(bool b)
        {
            getGameOptions().ShowPreview = b;
        }

        private GameOptions getGameOptions()
        {
            return OthelloData.GetInstance().OthelloOptions;
        }

        public void setEventListener(IBoardEventListener i_Listener)
        {
            m_EventListener = i_Listener;
        }

        public void setPlayers(IPlayer i_BlackPlayer, IPlayer i_WhitePlayer)
        {
            r_Players[eSquareType.Black] = setupPlayer(i_BlackPlayer, eSquareType.Black);
            r_Players[eSquareType.White] = setupPlayer(i_WhitePlayer, eSquareType.White);
            updateView();
            dispatchCurrentState();
            dispathPieceQuantity();
            automaticPlay();
        }

        private IPlayer setupPlayer(IPlayer i_BlackPlayer, eSquareType i_Color)
        {
            var player = i_BlackPlayer;
            player.setColor(i_Color);
            player.setBoardModel(m_BoardModel);
            return player;
        }

        private void automaticPlay()
        {
            if (currentPlayerCanMakeMove() && currentPlayerIsAutoPlayer())
            {
                m_LockHumanPlay = true;
                m_EventListener.DispatchCurrentState(eStateType.Thinking);
                var thread = new Thread(doAutomaticPlay);
                thread.Start(new MethodInvoker(automaticPlayFinished));
            }
        }

        private void automaticPlayFinished()
        {
            handleMove(m_LastMove.X, m_LastMove.Y);
            m_LockHumanPlay = false;
            automaticPlay();
        }

        private void doAutomaticPlay(object i_MethodInvoker)
        {
            var move = r_Players[m_CurrentPlayer].GetMove();
            m_LastMove = move;
            (i_MethodInvoker as MethodInvoker).Invoke();
        }

        private void handleMove(int x, int y)
        {
            try
            {
                m_BoardModel.MakeMove(x, y, getCurrentPlayerColor());
                m_EventListener.DispatchLastMove(x, y, getCurrentPlayerColor());
                m_CurrentPlayer = getOtherColor(m_CurrentPlayer);
                dispatchCurrentState();
                dispathPieceQuantity();
                updateView();
            }
            catch (NonValidMoveException)
            {
            }
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

        private bool currentPlayerIsAutoPlayer()
        {
            return r_Players[m_CurrentPlayer].IsAutomatic();
        }

        private bool currentPlayerCanMakeMove()
        {
            return m_BoardModel.GetPossibleMovesFor(getCurrentPlayerColor()).Count > 0;
        }

        private eSquareType getCurrentPlayerColor()
        {
            return r_Players[m_CurrentPlayer].getColor();
        }

        internal class DummyBoardEventListener : IBoardEventListener
        {
            public void DispatchLastMove(int i_X, int i_Y, eSquareType i_Color)
            {
            }

            public void DispatchCurrentState(eStateType i_State)
            {
            }

            public void DispatchPieceQuantity(int i_BlackCount, int i_WhiteCount)
            {
            }
        }

        public IBoardModel GetModel()
        {
            return m_BoardModel;
        }

        public void setCurrentPlayer(eSquareType i_Player)
        {
            m_CurrentPlayer = i_Player;
        }

        public eSquareType getCurrentPlayer()
        {
            return m_CurrentPlayer;
        }
    }
}