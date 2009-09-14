using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Reverci.board;
using Reverci.model;
using Reverci.player;
using Reverci.view;

namespace Reverci.comp
{
    public class BoardController : IBoardViewEventListener
    {
        private readonly IBoardView r_BoardView;
        private readonly Dictionary<eCoinType, IPlayer> r_Players = new Dictionary<eCoinType, IPlayer>();
        private IBoardModel m_BoardModel;
        private IBoardEventListener m_EventListener = new DummyBoardEventListener();
        private eCoinType m_CurrentPlayer;
        private bool m_LockHumanPlay;
        private Point m_LastMove;

        public BoardController(IBoardView i_View)
        {
            r_BoardView = i_View;
            r_BoardView.SetEventListener(this);
            initializeCurrentPlayer();
        }

        private void initializeCurrentPlayer()
        {
            m_CurrentPlayer = eCoinType.Black;
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
            bool itsNotPossibleForMeToMove = noPossibleMovesFor(getCurrentPlayerColor());
            if (itsNotPossibleForMeToMove &&
                noPossibleMovesFor(SquareTypeUtil.GetOtherColor(getCurrentPlayerColor())))
            {
                var blackCount = m_BoardModel.GetPieceCountOfType(eCoinType.Black);
                var whiteCount = m_BoardModel.GetPieceCountOfType(eCoinType.White);
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
                    OthelloData.GetInstance().OthelloOptions.BlackPlayer,
                    OthelloData.GetInstance().OthelloOptions.WhitePlayer);
            }
            else
            {
                if (itsNotPossibleForMeToMove)
                {
                    m_CurrentPlayer = SquareTypeUtil.GetOtherColor(m_CurrentPlayer);
                }


                if (getCurrentPlayerColor() == eCoinType.Black)
                {
                    m_EventListener.DispatchCurrentState(eStateType.BlackTurn);
                }
                else
                {
                    m_EventListener.DispatchCurrentState(eStateType.WhiteTurn);
                }
            }
        }

        private bool noPossibleMovesFor(eCoinType i_Color)
        {
            return m_BoardModel.GetPossibleMovesFor(i_Color).Count == 0;
        }

        public void DispatchMove(int i_X, int i_Y)
        {
            if (!m_LockHumanPlay)
            {
                handleMove(i_X, i_Y);
                automaticPlay();
            }
        }

        private void dispathPieceQuantity()
        {
            m_EventListener.DispatchPieceQuantity(
                m_BoardModel.GetPieceCountOfType(eCoinType.Black),
                m_BoardModel.GetPieceCountOfType(eCoinType.White));
        }

        public void DispatchLeaveSquare(int i_X, int i_Y)
        {
            updateView();
        }

        public void DispatchEnterSquare(int i_X, int i_Y)
        {
            if (getGameOptions().ShowPreview)
            {
                var previewPoints = m_BoardModel.GetPreviewFor(i_X, i_Y, getCurrentPlayerColor());
                if (previewPoints.Count > 0)
                {
                    previewPoints.Insert(0, new Point(i_X, i_Y));
                    r_BoardView.AddPreview(previewPoints, getCurrentPlayerColor());
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

            r_BoardView.UpdateBoardWith(board);
        }

        private void addPossibleMovesOn(eCoinType[][] board)
        {
            foreach (var move in m_BoardModel.GetPossibleMovesFor(getCurrentPlayerColor()))
            {
                board[move.X][move.Y] = eCoinType.Move;
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
            r_Players[eCoinType.Black] = setupPlayer(i_BlackPlayer, eCoinType.Black);
            r_Players[eCoinType.White] = setupPlayer(i_WhitePlayer, eCoinType.White);
            updateView();
            dispatchCurrentState();
            dispathPieceQuantity();
            automaticPlay();
        }

        private IPlayer setupPlayer(IPlayer i_BlackPlayer, eCoinType i_Color)
        {
            var player = i_BlackPlayer;
            player.setColor(i_Color);
            player.setBoardModel(m_BoardModel);
            return player;
        }

        private void automaticPlay()
        {
            if (currentPlayerIsAutoPlayer() && currentPlayerCanMakeMove())
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
                m_CurrentPlayer = SquareTypeUtil.GetOtherColor(m_CurrentPlayer);
                dispatchCurrentState();
                dispathPieceQuantity();
                updateView();
            }
            catch (NonValidMoveException)
            {
            }
        }

        private bool currentPlayerIsAutoPlayer()
        {
            return r_Players[m_CurrentPlayer].IsAutomatic();
        }

        private bool currentPlayerCanMakeMove()
        {
            return m_BoardModel.GetPossibleMovesFor(getCurrentPlayerColor()).Count > 0;
        }

        private eCoinType getCurrentPlayerColor()
        {
            return r_Players[m_CurrentPlayer].getColor();
        }

        internal class DummyBoardEventListener : IBoardEventListener
        {
            public void DispatchLastMove(int i_X, int i_Y, eCoinType i_Color)
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

        public void setCurrentPlayer(eCoinType i_Player)
        {
            m_CurrentPlayer = i_Player;
        }

        public eCoinType getCurrentPlayer()
        {
            return m_CurrentPlayer;
        }

        public void UpdatePlayers()
        {
            setPlayers(
                PlayerFactory.createPlayerOfType(OthelloData.GetInstance().OthelloOptions.BlackPlayer),
                PlayerFactory.createPlayerOfType(OthelloData.GetInstance().OthelloOptions.WhitePlayer));
        }
    }
}