using System;
using System.Threading;
using System.Windows.Forms;
using Reverci.comp;
using Reverci.model;
using Reverci.player;

namespace Reverci.view.forms
{
    public partial class FormsGameView : Form, IGameView, IBoardEventListener
    {
        private const string k_FilesFilter = "Othello File (*.ots)|*.ots";
        private readonly BoardController r_BoardController;
        private readonly StateController r_StateController;
        private readonly StatusController r_StatusController;
        private readonly StatisticsController r_StatisticsController;
        private string m_CurrentFileName;
        private SavedGame m_SavedGame;
        private FormsBoardLabeler m_BoardView;

        public FormsGameView()
        {
            InitializeComponent();
            initializeBoardComponent();
            r_BoardController = new BoardController(m_BoardView);
            r_BoardController.setEventListener(this);
            r_StateController = new StateController(m_StateBar);
            r_StatusController = new StatusController(m_StatusView);
            r_StatisticsController = new StatisticsController(new FormsStatisticsView());
        }

        private void initializeBoardComponent()
        {
            SuspendLayout();
            m_BoardView = new FormsBoardLabeler(new FormsBoardView());
            m_BoardView.Anchor =
                ((AnchorStyles.Top | AnchorStyles.Bottom)
                 | AnchorStyles.Left)
                | AnchorStyles.Right;
            m_BoardView.Location = new System.Drawing.Point(0, 24);
            m_BoardView.Margin = new Padding(0, 0, 0, 5);
            m_BoardView.Name = "m_BoardView";
            m_BoardView.Size = new System.Drawing.Size(436, 437);
            Controls.Add(m_BoardView);
            ResumeLayout(false);
            PerformLayout();
            m_BoardView.EnableResizeEvents();
        }

        private void setModelOnBoard(eSquareType[][] i_Board)
        {
            r_BoardController.SetModel(new ReverciBoardModel(i_Board));
            r_StatusController.Clear();
            updatePlayers();
        }

        private void FormsGameView_Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void FormsMainMenu_NewGame(object sender, EventArgs e)
        {
            setEmptyModel();
        }

        private void FormsGameView_Statistics(object sender, EventArgs e)
        {
            r_StatisticsController.ShowStatisticsView(getMiddleX(), getMiddleY());
        }

        private int getMiddleY()
        {
            return Location.Y + (Size.Height / 2);
        }

        private int getMiddleX()
        {
            return Location.X + (Size.Width / 2);
        }

        private void FormsGameView_ShowValidMoves(object sender, EventArgs e)
        {
            r_BoardController.EnablePossibleMoves(((ToolStripMenuItem)sender).Checked);
        }

        private void FormsGameView_BlackUser(object sender, EventArgs e)
        {
            setBlackPlayerAs(ePlayerType.Human);
        }

        private void updatePlayers()
        {
            r_BoardController.setPlayers(
                PlayerFactory.createPlayerOfType(OthelloData.GetInstance().OthelloOptions.BlackPlayer),
                PlayerFactory.createPlayerOfType(OthelloData.GetInstance().OthelloOptions.WhitePlayer));
        }

        private void setBlackPlayerAs(ePlayerType i_PlayerType)
        {
            uncheckAllBlackItems();
            switch (i_PlayerType)
            {
                case ePlayerType.Human:
                    m_BlackUser.Checked = true;
                    break;
                case ePlayerType.DumbComputer:
                    m_BlackDumbComputer.Checked = true;
                    break;
                case ePlayerType.OkComputer:
                    m_BlackOkComputer.Checked = true;
                    break;
                case ePlayerType.SmartComputer:
                    m_BlackSmartComputer.Checked = true;
                    break;
                case ePlayerType.GeniusComputer:
                    m_BlackGeniusComputer.Checked = true;
                    break;
            }

            OthelloData.GetInstance().OthelloOptions.BlackPlayer = i_PlayerType;
            updatePlayers();
        }

        private void uncheckAllBlackItems()
        {
            m_BlackUser.Checked = false;
            m_BlackDumbComputer.Checked = false;
            m_BlackOkComputer.Checked = false;
            m_BlackSmartComputer.Checked = false;
            m_BlackGeniusComputer.Checked = false;
        }

        private void setWhitePlayerAs(ePlayerType i_PlayerType)
        {
            uncheckAllWhiteItems();
            switch (i_PlayerType)
            {
                case ePlayerType.Human:
                    m_WhiteUser.Checked = true;
                    break;
                case ePlayerType.DumbComputer:
                    m_WhiteDumbComputer.Checked = true;
                    break;
                case ePlayerType.OkComputer:
                    m_WhiteOkComputer.Checked = true;
                    break;
                case ePlayerType.SmartComputer:
                    m_WhiteSmartComputer.Checked = true;
                    break;
                case ePlayerType.GeniusComputer:
                    m_WhiteGeniusComputer.Checked = true;
                    break;
            }

            OthelloData.GetInstance().OthelloOptions.WhitePlayer = i_PlayerType;
            updatePlayers();
        }

        private void uncheckAllWhiteItems()
        {
            m_WhiteUser.Checked = false;
            m_WhiteDumbComputer.Checked = false;
            m_WhiteOkComputer.Checked = false;
            m_WhiteSmartComputer.Checked = false;
            m_WhiteGeniusComputer.Checked = false;
        }

        private void FormsGameView_WhiteUser(object sender, EventArgs e)
        {
            setWhitePlayerAs(ePlayerType.Human);
        }

        private void FormsGameView_PreviewMoves(object sender, EventArgs e)
        {
            r_BoardController.SetPreview(((ToolStripMenuItem)sender).Checked);
        }

        public void DispatchLastMove(int i_X, int i_Y, eSquareType i_Color)
        {
            r_StatusController.LogMove(i_X, i_Y, i_Color);
        }

        public void DispatchCurrentState(eStateType i_State)
        {
            r_StateController.UpdateState(i_State);
            setProcessingOnStateController(i_State == eStateType.Thinking);
            if (i_State != eStateType.Thinking)
            {
                r_StatusController.UpdateCurrentState(i_State);
            }
        }

        private void setProcessingOnStateController(bool b)
        {
            r_StateController.ShowProcessing(b);
        }

        public void DispatchPieceQuantity(int i_BlackCount, int i_WhiteCount)
        {
            r_StatusController.UpdatePiecesQunatity(i_BlackCount, i_WhiteCount);
        }

        private void FormsGameView_Closing(object sender, FormClosingEventArgs e)
        {
            OthelloData.GetInstance().SaveData();
        }

        private void FormsGameView_Load(object sender, EventArgs e)
        {
            OthelloData.GetInstance().LoadData();
            setEmptyModel();
            updateMenu();
        }

        private void setEmptyModel()
        {
            setModelOnBoard(
                BoardState.CreateInitialBoardWithSize(OthelloData.GetInstance().OthelloOptions.BoardSize));
        }

        private void updateMenu()
        {
            var options = OthelloData.GetInstance().OthelloOptions;
            m_PreviewMoves.Checked = options.ShowPreview;
            m_ShowValidMoves.Checked = options.ShowValidMoves;
            setBlackPlayerAs(options.BlackPlayer);
            setWhitePlayerAs(options.WhitePlayer);
        }

        private void FormsGameView_SaveGame(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = k_FilesFilter };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_CurrentFileName = dlg.FileName;
                setProcessingOnStateController(true);
                var thread = new Thread(doSaveGame);
                thread.Start(new MethodInvoker(finishedSaving));
            }
        }

        private void finishedSaving()
        {
            setProcessingOnStateController(false);
        }

        private void doSaveGame(object i_MethodInvoker)
        {
            Thread.Sleep(1000);
            var savedGame = new SavedGame
                                {
                                    Board = r_BoardController.GetModel().GetBoard(),
                                    MovesHistory = r_StatusController.GetMovesLog(),
                                    CurrentTurn = r_BoardController.getCurrentPlayer()
                                };
            FileIO.SaveObjectToFile(m_CurrentFileName, savedGame);
            (i_MethodInvoker as MethodInvoker).Invoke();
        }

        private void FormsGameView_LoadGame(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = k_FilesFilter };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_CurrentFileName = dlg.FileName;
                setProcessingOnStateController(true);
                var thread = new Thread(doLoadGame);
                thread.Start(new MethodInvoker(finishedLoading));
            }
        }

        private void finishedLoading()
        {
            setProcessingOnStateController(false);
            r_BoardController.setCurrentPlayer(m_SavedGame.CurrentTurn);
            setModelOnBoard(m_SavedGame.Board);
            r_StatusController.SetMovesLog(m_SavedGame.MovesHistory);
        }

        private void doLoadGame(object i_MethodInvoker)
        {
            Thread.Sleep(1000);
            m_SavedGame = (SavedGame)FileIO.ReadObjectFrom(m_CurrentFileName);
            (i_MethodInvoker as MethodInvoker).Invoke();
        }

        private void FormsGameView_BlackDumbComputer(object sender, EventArgs e)
        {
            setBlackPlayerAs(ePlayerType.DumbComputer);
        }

        private void FormsGameView_BlackOkComputer(object sender, EventArgs e)
        {
            setBlackPlayerAs(ePlayerType.OkComputer);
        }

        private void FormsGameView_BlackSmartComputer(object sender, EventArgs e)
        {
            setBlackPlayerAs(ePlayerType.SmartComputer);
        }

        private void FormsGameView_BlackGeniusComputer(object sender, EventArgs e)
        {
            setBlackPlayerAs(ePlayerType.GeniusComputer);
        }

        private void FormsGameView_WhiteDumbComputer(object sender, EventArgs e)
        {
            setWhitePlayerAs(ePlayerType.DumbComputer);
        }

        private void FormsGameView_WhiteOkComputer(object sender, EventArgs e)
        {
            setWhitePlayerAs(ePlayerType.OkComputer);
        }

        private void FormsGameView_WhiteSmartComputer(object sender, EventArgs e)
        {
            setWhitePlayerAs(ePlayerType.SmartComputer);
        }

        private void FormsGameView_WhiteGeniusComputer(object sender, EventArgs e)
        {
            setWhitePlayerAs(ePlayerType.GeniusComputer);
        }
    }
}