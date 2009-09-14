using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Reverci.board;
using Reverci.command;
using Reverci.comp;
using Reverci.model;
using Reverci.player;

namespace Reverci.view.forms
{
    public partial class FormsGameView : Form, IGameCommander, IBoardEventListener
    {
        private const string k_FilesFilter = "Othello File (*.ots)|*.ots";
        private readonly BoardController r_BoardController;
        private readonly StateController r_StateController;
        private readonly StatusController r_StatusController;
        private readonly StatisticsController r_StatisticsController;
        private readonly StatisticsHolder r_StatisticsHolder = new StatisticsHolder();
        private GameOptions r_GameOptions = new GameOptions();

        private static Dictionary<ePlayerType, eCommandType> m_BPlayerToCommand;
        private static Dictionary<ePlayerType, eCommandType> m_WPlayerToCommand;

        private string m_CurrentFileName;
        private SavedGame m_SavedGame;
        private FormsBoardLabeler m_BoardView;

        public FormsGameView()
        {
            initalizePlayerToCommand();
            registerCommands();
            InitializeComponent();
            initializeBoardComponent();
            initializeActionControllers();
            r_BoardController = new BoardController(m_BoardView);
            r_BoardController.setEventListener(this);
            r_StateController = new StateController(m_StateBar);
            r_StatusController = new StatusController(m_StatusView);
            r_StatisticsController = new StatisticsController(new FormsStatisticsView());
            CommandCommander.GetInstance().GetCommand(eCommandType.SaveGame).Enabled = false;
        }

        private void initalizePlayerToCommand()
        {
            m_BPlayerToCommand = new Dictionary<ePlayerType, eCommandType>
                                     {
                                         { ePlayerType.Human, eCommandType.BlackUser },
                                         { ePlayerType.DumbComputer, eCommandType.BlackDumbComputer },
                                         { ePlayerType.OkComputer, eCommandType.BlackOkComputer },
                                         { ePlayerType.SmartComputer, eCommandType.BlackSmartComputer },
                                         { ePlayerType.GeniusComputer, eCommandType.BlackGeniusComputer }
                                     };

            m_WPlayerToCommand = new Dictionary<ePlayerType, eCommandType>
                                     {
                                         { ePlayerType.Human, eCommandType.WhiteUser },
                                         { ePlayerType.DumbComputer, eCommandType.WhiteDumbComputer },
                                         { ePlayerType.OkComputer, eCommandType.WhiteOkComputer },
                                         { ePlayerType.SmartComputer, eCommandType.WhiteSmartComputer },
                                         { ePlayerType.GeniusComputer, eCommandType.WhiteGeniusComputer }
                                     };
        }

        private void registerCommands()
        {
            CommandCommander.GetInstance().RegisterCommand(eCommandType.New, new NewGameCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.SaveGame, new SaveGameCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.LoadGame, new LoadGameCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.ShowStatistics, new ShowStatisricsCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.Exit, new ExitCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.ShowValidMoves, new ShowValidMovesCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.ShowPreview, new ShowPreviewCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.WhiteUser, new WhiteUserCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.WhiteDumbComputer, new WhiteDumbComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.WhiteOkComputer, new WhiteOkComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.WhiteSmartComputer, new WhiteSmartComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.WhiteGeniusComputer, new WhiteGeniusComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.BlackUser, new BlackUserCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.BlackDumbComputer, new BlackDumbComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.BlackOkComputer, new BlackOkComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.BlackSmartComputer, new BlackSmartComputerCommand(this));
            CommandCommander.GetInstance().RegisterCommand(eCommandType.BlackGeniusComputer, new BlackGeniusComputerCommand(this));
        }

        private void initializeActionControllers()
        {
            new ButtonController(m_NewGame, eCommandType.New);
            new ButtonController(m_LoadGame, eCommandType.LoadGame);
            new ButtonController(m_SaveGame, eCommandType.SaveGame);
            new ButtonController(m_Statistics, eCommandType.ShowStatistics);
            new ButtonController(m_Exit, eCommandType.Exit);
            new ButtonController(m_ShowValidMoves, eCommandType.ShowValidMoves);
            new ButtonController(m_PreviewMoves, eCommandType.ShowPreview);
            new ButtonController(m_BlackUser, eCommandType.BlackUser);
            new ButtonController(m_BlackDumbComputer, eCommandType.BlackDumbComputer);
            new ButtonController(m_BlackOkComputer, eCommandType.BlackOkComputer);
            new ButtonController(m_BlackSmartComputer, eCommandType.BlackSmartComputer);
            new ButtonController(m_BlackGeniusComputer, eCommandType.BlackGeniusComputer);
            new ButtonController(m_WhiteUser, eCommandType.WhiteUser);
            new ButtonController(m_WhiteDumbComputer, eCommandType.WhiteDumbComputer);
            new ButtonController(m_WhiteOkComputer, eCommandType.WhiteOkComputer);
            new ButtonController(m_WhiteSmartComputer, eCommandType.WhiteSmartComputer);
            new ButtonController(m_WhiteGeniusComputer, eCommandType.WhiteGeniusComputer);
        }

        private void initializeBoardComponent()
        {
            SuspendLayout();
            m_BoardView = new FormsBoardLabeler(new FormsBoardView())
                              {
                                  Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
                                             | AnchorStyles.Left)
                                            | AnchorStyles.Right),
                                  Location = new System.Drawing.Point(0, 48),
                                  Margin = new Padding(0, 0, 0, 5),
                                  Name = "m_BoardView",
                                  Size = new System.Drawing.Size(436, 413)
                              };
            Controls.Add(m_BoardView);
            ResumeLayout(false);
            PerformLayout();
            m_BoardView.EnableResizeEvents();
        }

        private void setModelOnBoard(eCoinType[][] i_Board)
        {
            r_BoardController.SetModel(new ReverciBoardModel(i_Board));
            r_StatusController.Clear();
            r_BoardController.UpdatePlayers();
        }

        private int getMiddleY()
        {
            return Location.Y + (Size.Height / 2);
        }

        private int getMiddleX()
        {
            return Location.X + (Size.Width / 2);
        }

        private void setBlackPlayerAs(ePlayerType i_PlayerType)
        {
            CommandCommander.GetInstance().GetCommand(m_BPlayerToCommand[i_PlayerType]).DoCommand();
        }

        private void setWhitePlayerAs(ePlayerType i_PlayerType)
        {
            CommandCommander.GetInstance().GetCommand(m_WPlayerToCommand[i_PlayerType]).DoCommand();
        }

        public void DispatchLastMove(int i_X, int i_Y, eCoinType i_Color)
        {
            r_StatusController.LogMove(i_X, i_Y, i_Color);
            setEnabledOnSaveTo(true);
        }

        private void setEnabledOnSaveTo(bool i_Enabled)
        {
            CommandCommander.GetInstance().GetCommand(eCommandType.SaveGame).Enabled = i_Enabled;
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
            StartNewGame();
            updateOptions();
        }

        public void StartNewGame()
        {
            setModelOnBoard(
                ReverciBoardBuilder.CreateInitialBoardWithSize(OthelloData.GetInstance().OthelloOptions.BoardSize));
            setEnabledOnSaveTo(false);
        }

        public void ShowPreview(bool i_Preview)
        {
            r_BoardController.SetPreview(i_Preview);
        }

        public void ShowValidMoves(bool i_ValidMoves)
        {
            r_BoardController.EnablePossibleMoves(i_ValidMoves);
        }

        public bool SaveGame()
        {
            var dlg = new SaveFileDialog { Filter = k_FilesFilter };
            var gameSaved = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_CurrentFileName = dlg.FileName;
                setProcessingOnStateController(true);
                var thread = new Thread(doSaveGame);
                thread.Start(new MethodInvoker(finishedSaving));
                gameSaved = true;
            }

            return gameSaved;
        }

        public void LoadGame()
        {
            var dlg = new OpenFileDialog { Filter = k_FilesFilter };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_CurrentFileName = dlg.FileName;
                setProcessingOnStateController(true);
                var thread = new Thread(doLoadGame);
                thread.Start(new MethodInvoker(finishedLoading));
                setEnabledOnSaveTo(false);
            }
        }

        public void SetBlackPlayerAs(ePlayerType i_PlayerType)
        {
            var currentPlayerCommand = m_BPlayerToCommand[OthelloData.GetInstance().OthelloOptions.BlackPlayer];
            CommandCommander.GetInstance().GetCommand(currentPlayerCommand).Checked = false;
            CommandCommander.GetInstance().GetCommand(m_BPlayerToCommand[i_PlayerType]).Checked = true;

            OthelloData.GetInstance().OthelloOptions.BlackPlayer = i_PlayerType;
            r_BoardController.UpdatePlayers();
        }

        public void SetWhitePlayerAs(ePlayerType i_PlayerType)
        {
            var currentPlayerCommand = m_WPlayerToCommand[OthelloData.GetInstance().OthelloOptions.WhitePlayer];
            CommandCommander.GetInstance().GetCommand(currentPlayerCommand).Checked = false;
            CommandCommander.GetInstance().GetCommand(m_WPlayerToCommand[i_PlayerType]).Checked = true;

            OthelloData.GetInstance().OthelloOptions.WhitePlayer = i_PlayerType;
            r_BoardController.UpdatePlayers();
        }

        public void Exit()
        {
            Close();
        }

        public void ShowStatistics()
        {
            r_StatisticsController.ShowStatisticsView(getMiddleX(), getMiddleY());
        }

        private void updateOptions()
        {
            var options = OthelloData.GetInstance().OthelloOptions;
            CommandCommander.GetInstance().GetCommand(eCommandType.ShowPreview).Checked = options.ShowPreview;
            CommandCommander.GetInstance().GetCommand(eCommandType.ShowValidMoves).Checked = options.ShowValidMoves;
            setBlackPlayerAs(options.BlackPlayer);
            setWhitePlayerAs(options.WhitePlayer);
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
    }
}