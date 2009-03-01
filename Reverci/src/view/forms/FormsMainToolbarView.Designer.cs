using System.Windows.Forms;

namespace Reverci.view.forms
{
    partial class FormsMainToolbarView
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_NewGameBtn = new FormsToolStripButtonView();
            this.m_SaveGameBtn = new FormsToolStripButtonView();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ShowValidMoves = new FormsToolStripButtonView();
            this.m_ShowPreview = new FormsToolStripButtonView();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_BlackCombo = new System.Windows.Forms.ToolStripDropDownButton();
            this.m_BlackUser = new FormsToolstripMenuItemView();
            this.m_BlackDumbComputer = new FormsToolstripMenuItemView();
            this.m_BlackOkComputer = new FormsToolstripMenuItemView();
            this.m_BlackSmartComputer = new FormsToolstripMenuItemView();
            this.m_BlackGeniusComputer = new FormsToolstripMenuItemView();
            this.m_WhiteCombo = new System.Windows.Forms.ToolStripDropDownButton();
            this.m_WhiteUser = new FormsToolstripMenuItemView();
            this.m_WhiteDumbComputer = new FormsToolstripMenuItemView();
            this.m_WhiteOkComputer = new FormsToolstripMenuItemView();
            this.m_WhiteSmartComputer = new FormsToolstripMenuItemView();
            this.m_WhiteGeniusComputer = new FormsToolstripMenuItemView();
            this.SuspendLayout();

            // 
            // formsToolbarView1
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_NewGameBtn,
            this.m_SaveGameBtn,
            this.toolStripSeparator3,
            this.m_ShowValidMoves,
            this.m_ShowPreview,
            this.toolStripSeparator4,
            this.m_BlackCombo,
            this.m_WhiteCombo});
            // 
            // m_NewGameBtn
            // 
            this.m_NewGameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_NewGameBtn.Image = global::Reverci.Properties.Resources.IconNewGame;
            this.m_NewGameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_NewGameBtn.Name = "m_NewGameBtn";
            this.m_NewGameBtn.Size = new System.Drawing.Size(23, 22);
            this.m_NewGameBtn.Text = "New Game";
            // 
            // m_SaveGameBtn
            // 
            this.m_SaveGameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_SaveGameBtn.Image = global::Reverci.Properties.Resources.IconSave;
            this.m_SaveGameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_SaveGameBtn.Name = "m_SaveGameBtn";
            this.m_SaveGameBtn.Size = new System.Drawing.Size(23, 22);
            this.m_SaveGameBtn.Text = "Save";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // m_ShowValidMoves
            // 
            this.m_ShowValidMoves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ShowValidMoves.Image = global::Reverci.Properties.Resources.IconShowPreview;
            this.m_ShowValidMoves.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ShowValidMoves.Name = "m_ShowValidMoves";
            this.m_ShowValidMoves.Size = new System.Drawing.Size(23, 22);
            this.m_ShowValidMoves.Text = "Show Valid Moves";
            // 
            // m_ShowPreview
            // 
            this.m_ShowPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ShowPreview.Image = global::Reverci.Properties.Resources.IconShowValidMoves;
            this.m_ShowPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ShowPreview.Name = "m_ShowPreview";
            this.m_ShowPreview.Size = new System.Drawing.Size(23, 22);
            this.m_ShowPreview.Text = "Show Preview";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // m_BlackCombo
            // 
            this.m_BlackCombo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_BlackUser,
            this.m_BlackDumbComputer,
            this.m_BlackOkComputer,
            this.m_BlackSmartComputer,
            this.m_BlackGeniusComputer});
            this.m_BlackCombo.Image = global::Reverci.Properties.Resources.CoinBlack;
            this.m_BlackCombo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_BlackCombo.Name = "m_BlackCombo";
            this.m_BlackCombo.Size = new System.Drawing.Size(60, 22);
            this.m_BlackCombo.Text = "Balck";
            // 
            // m_BlackUser
            // 
            this.m_BlackUser.Name = "m_BlackUser";
            this.m_BlackUser.Size = new System.Drawing.Size(156, 22);
            this.m_BlackUser.Text = "User";
            // 
            // m_BlackDumbComputer
            // 
            this.m_BlackDumbComputer.Name = "m_BlackDumbComputer";
            this.m_BlackDumbComputer.Size = new System.Drawing.Size(156, 22);
            this.m_BlackDumbComputer.Text = "Dumb Computer";
            // 
            // m_BlackOkComputer
            // 
            this.m_BlackOkComputer.Name = "m_BlackOkComputer";
            this.m_BlackOkComputer.Size = new System.Drawing.Size(156, 22);
            this.m_BlackOkComputer.Text = "Ok Computer";
            // 
            // m_BlackSmartComputer
            // 
            this.m_BlackSmartComputer.Name = "m_BlackSmartComputer";
            this.m_BlackSmartComputer.Size = new System.Drawing.Size(156, 22);
            this.m_BlackSmartComputer.Text = "Smart Computer";
            // 
            // m_BlackGeniusComputer
            // 
            this.m_BlackGeniusComputer.Name = "m_BlackGeniusComputer";
            this.m_BlackGeniusComputer.Size = new System.Drawing.Size(156, 22);
            this.m_BlackGeniusComputer.Text = "Genius Computer";
            // 
            // m_WhiteCombo
            // 
            this.m_WhiteCombo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_WhiteUser,
            this.m_WhiteDumbComputer,
            this.m_WhiteOkComputer,
            this.m_WhiteSmartComputer,
            this.m_WhiteGeniusComputer});
            this.m_WhiteCombo.Image = global::Reverci.Properties.Resources.CoinWhite;
            this.m_WhiteCombo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_WhiteCombo.Name = "m_WhiteCombo";
            this.m_WhiteCombo.Size = new System.Drawing.Size(64, 22);
            this.m_WhiteCombo.Text = "White";
            // 
            // m_WhiteUser
            // 
            this.m_WhiteUser.Name = "m_WhiteUser";
            this.m_WhiteUser.Size = new System.Drawing.Size(156, 22);
            this.m_WhiteUser.Text = "User";
            // 
            // m_WhiteDumbComputer
            // 
            this.m_WhiteDumbComputer.Name = "m_WhiteDumbComputer";
            this.m_WhiteDumbComputer.Size = new System.Drawing.Size(156, 22);
            this.m_WhiteDumbComputer.Text = "Dumb Computer";
            // 
            // m_WhiteOkComputer
            // 
            this.m_WhiteOkComputer.Name = "m_WhiteOkComputer";
            this.m_WhiteOkComputer.Size = new System.Drawing.Size(156, 22);
            this.m_WhiteOkComputer.Text = "Ok Computer";
            // 
            // m_WhiteSmartComputer
            // 
            this.m_WhiteSmartComputer.Name = "m_WhiteSmartComputer";
            this.m_WhiteSmartComputer.Size = new System.Drawing.Size(156, 22);
            this.m_WhiteSmartComputer.Text = "Smart Computer";
            // 
            // m_WhiteGeniusComputer
            // 
            this.m_WhiteGeniusComputer.Name = "m_WhiteGeniusComputer";
            this.m_WhiteGeniusComputer.Size = new System.Drawing.Size(156, 22);
            this.m_WhiteGeniusComputer.Text = "Genius Computer";
            components = new System.ComponentModel.Container();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private FormsToolStripButtonView m_NewGameBtn;
        private FormsToolStripButtonView m_SaveGameBtn;
        private FormsToolStripButtonView m_ShowValidMoves;
        private FormsToolStripButtonView m_ShowPreview;
        private ToolStripDropDownButton m_BlackCombo;
        private ToolStripDropDownButton m_WhiteCombo;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private FormsToolstripMenuItemView m_BlackUser;
        private FormsToolstripMenuItemView m_BlackDumbComputer;
        private FormsToolstripMenuItemView m_BlackOkComputer;
        private FormsToolstripMenuItemView m_BlackSmartComputer;
        private FormsToolstripMenuItemView m_BlackGeniusComputer;
        private FormsToolstripMenuItemView m_WhiteUser;
        private FormsToolstripMenuItemView m_WhiteDumbComputer;
        private FormsToolstripMenuItemView m_WhiteOkComputer;
        private FormsToolstripMenuItemView m_WhiteSmartComputer;
        private FormsToolstripMenuItemView m_WhiteGeniusComputer;
    }
}