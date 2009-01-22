using System.Windows.Forms;
using Reverci.view.forms;

namespace Othello.view.forms
{
    partial class FormsGameView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormsGameView));
            this.m_MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_Statistics = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ShowValidMoves = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PreviewMoves = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_BlackUser = new System.Windows.Forms.ToolStripMenuItem();
            this.m_BlackComputer = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_WhiteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.m_WhiteComputer = new System.Windows.Forms.ToolStripMenuItem();
            this.m_StateBar = new FormsStateView();
            this.m_StatusView = new FormsStatusView();
            this.m_BoardView = new FormsBoardView();
            this.m_MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MainMenu
            // 
            this.m_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.playersToolStripMenuItem});
            this.m_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.m_MainMenu.Name = "m_MainMenu";
            this.m_MainMenu.Size = new System.Drawing.Size(633, 24);
            this.m_MainMenu.TabIndex = 1;
            this.m_MainMenu.Text = "formsMainMenu1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_NewGame,
            this.saveGameToolStripMenuItem,
            this.loadGameToolStripMenuItem,
            this.toolStripSeparator1,
            this.m_Statistics,
            this.toolStripSeparator2,
            this.m_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // m_NewGame
            // 
            this.m_NewGame.Name = "m_NewGame";
            this.m_NewGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.m_NewGame.Size = new System.Drawing.Size(164, 22);
            this.m_NewGame.Text = "New Game";
            this.m_NewGame.Click += new System.EventHandler(this.FormsMainMenu_NewGame);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveGameToolStripMenuItem.Text = "Save Game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.FormsGameView_SaveGame);
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadGameToolStripMenuItem.Text = "Load Game";
            this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.FormsGameView_LoadGame);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // m_Statistics
            // 
            this.m_Statistics.Name = "m_Statistics";
            this.m_Statistics.Size = new System.Drawing.Size(164, 22);
            this.m_Statistics.Text = "Statistics...";
            this.m_Statistics.Click += new System.EventHandler(this.FormsGameView_Statistics);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // m_Exit
            // 
            this.m_Exit.Name = "m_Exit";
            this.m_Exit.Size = new System.Drawing.Size(164, 22);
            this.m_Exit.Text = "Exit";
            this.m_Exit.Click += new System.EventHandler(this.FormsGameView_Exit);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ShowValidMoves,
            this.m_PreviewMoves});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // m_ShowValidMoves
            // 
            this.m_ShowValidMoves.CheckOnClick = true;
            this.m_ShowValidMoves.Name = "m_ShowValidMoves";
            this.m_ShowValidMoves.Size = new System.Drawing.Size(159, 22);
            this.m_ShowValidMoves.Text = "Show Valid Moves";
            this.m_ShowValidMoves.CheckedChanged += new System.EventHandler(this.FormsGameView_ShowValidMoves);
            // 
            // m_PreviewMoves
            // 
            this.m_PreviewMoves.CheckOnClick = true;
            this.m_PreviewMoves.Name = "m_PreviewMoves";
            this.m_PreviewMoves.Size = new System.Drawing.Size(159, 22);
            this.m_PreviewMoves.Text = "Preview Moves";
            this.m_PreviewMoves.CheckedChanged += new System.EventHandler(this.FormsGameView_PreviewMoves);
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blackToolStripMenuItem,
            this.whiteToolStripMenuItem});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_BlackUser,
            this.m_BlackComputer});
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.blackToolStripMenuItem.Text = "Black";
            // 
            // m_BlackUser
            // 
            this.m_BlackUser.Checked = true;
            this.m_BlackUser.CheckOnClick = true;
            this.m_BlackUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_BlackUser.Name = "m_BlackUser";
            this.m_BlackUser.Size = new System.Drawing.Size(121, 22);
            this.m_BlackUser.Text = "User";
            this.m_BlackUser.Click += new System.EventHandler(this.FormsGameView_BlackUser);
            // 
            // m_BlackComputer
            // 
            this.m_BlackComputer.CheckOnClick = true;
            this.m_BlackComputer.Name = "m_BlackComputer";
            this.m_BlackComputer.Size = new System.Drawing.Size(121, 22);
            this.m_BlackComputer.Text = "Computer";
            this.m_BlackComputer.Click += new System.EventHandler(this.FormsGameView_BlackComputer);
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_WhiteUser,
            this.m_WhiteComputer});
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.whiteToolStripMenuItem.Text = "White";
            // 
            // m_WhiteUser
            // 
            this.m_WhiteUser.Checked = true;
            this.m_WhiteUser.CheckOnClick = true;
            this.m_WhiteUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_WhiteUser.Name = "m_WhiteUser";
            this.m_WhiteUser.Size = new System.Drawing.Size(121, 22);
            this.m_WhiteUser.Text = "User";
            this.m_WhiteUser.Click += new System.EventHandler(this.FormsGameView_WhiteUser);
            // 
            // m_WhiteComputer
            // 
            this.m_WhiteComputer.CheckOnClick = true;
            this.m_WhiteComputer.Name = "m_WhiteComputer";
            this.m_WhiteComputer.Size = new System.Drawing.Size(121, 22);
            this.m_WhiteComputer.Text = "Computer";
            this.m_WhiteComputer.Click += new System.EventHandler(this.FormsGameView_WhiteComputer);
            // 
            // m_StateBar
            // 
            this.m_StateBar.BackColor = System.Drawing.SystemColors.MenuBar;
            this.m_StateBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.m_StateBar.Location = new System.Drawing.Point(0, 466);
            this.m_StateBar.Name = "m_StateBar";
            this.m_StateBar.Size = new System.Drawing.Size(633, 22);
            this.m_StateBar.TabIndex = 2;
            this.m_StateBar.Text = "m_StateBar";
            // 
            // m_StatusView
            // 
            this.m_StatusView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_StatusView.BackColor = System.Drawing.Color.LightSlateGray;
            this.m_StatusView.Location = new System.Drawing.Point(441, 24);
            this.m_StatusView.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.m_StatusView.Name = "m_StatusView";
            this.m_StatusView.Size = new System.Drawing.Size(192, 442);
            this.m_StatusView.TabIndex = 3;
            // 
            // m_BoardView
            // 
            this.m_BoardView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BoardView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_BoardView.Location = new System.Drawing.Point(0, 24);
            this.m_BoardView.Name = "m_BoardView";
            this.m_BoardView.Size = new System.Drawing.Size(440, 439);
            this.m_BoardView.TabIndex = 0;
            // 
            // FormsGameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(633, 488);
            this.Controls.Add(this.m_StatusView);
            this.Controls.Add(this.m_StateBar);
            this.Controls.Add(this.m_BoardView);
            this.Controls.Add(this.m_MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormsGameView";
            this.Text = "Reverci";
            this.Load += new System.EventHandler(this.FormsGameView_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormsGameView_Closing);
            this.m_MainMenu.ResumeLayout(false);
            this.m_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FormsBoardView m_BoardView;
        private MenuStrip m_MainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem m_NewGame;
        private ToolStripMenuItem m_Exit;
        private ToolStripMenuItem m_Statistics;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem m_ShowValidMoves;
        private ToolStripMenuItem m_PreviewMoves;
        private ToolStripMenuItem playersToolStripMenuItem;
        private ToolStripMenuItem blackToolStripMenuItem;
        private ToolStripMenuItem m_BlackUser;
        private ToolStripMenuItem m_BlackComputer;
        private ToolStripMenuItem whiteToolStripMenuItem;
        private ToolStripMenuItem m_WhiteUser;
        private ToolStripMenuItem m_WhiteComputer;
        private FormsStateView m_StateBar;
        private FormsStatusView m_StatusView;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem loadGameToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
    }
}