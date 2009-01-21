namespace Othello.view.forms
{
    partial class FormsStatusView
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
            this.m_BlackText = new System.Windows.Forms.Label();
            this.m_WhiteText = new System.Windows.Forms.Label();
            this.m_CurrentText = new System.Windows.Forms.Label();
            this.m_CurrentColor = new System.Windows.Forms.Panel();
            this.m_BlackCount = new System.Windows.Forms.Label();
            this.m_WhiteCount = new System.Windows.Forms.Label();
            this.m_MovesLogger = new System.Windows.Forms.ListView();
            this.moveNumber = new System.Windows.Forms.ColumnHeader();
            this.player = new System.Windows.Forms.ColumnHeader();
            this.position = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // m_BlackText
            // 
            this.m_BlackText.AutoSize = true;
            this.m_BlackText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_BlackText.Location = new System.Drawing.Point(19, 15);
            this.m_BlackText.Name = "m_BlackText";
            this.m_BlackText.Size = new System.Drawing.Size(49, 19);
            this.m_BlackText.TabIndex = 1;
            this.m_BlackText.Text = "Black:";
            // 
            // m_WhiteText
            // 
            this.m_WhiteText.AutoSize = true;
            this.m_WhiteText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_WhiteText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_WhiteText.Location = new System.Drawing.Point(19, 54);
            this.m_WhiteText.Name = "m_WhiteText";
            this.m_WhiteText.Size = new System.Drawing.Size(55, 19);
            this.m_WhiteText.TabIndex = 2;
            this.m_WhiteText.Text = "White:";
            // 
            // m_CurrentText
            // 
            this.m_CurrentText.AutoSize = true;
            this.m_CurrentText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_CurrentText.Location = new System.Drawing.Point(19, 100);
            this.m_CurrentText.Name = "m_CurrentText";
            this.m_CurrentText.Size = new System.Drawing.Size(65, 19);
            this.m_CurrentText.TabIndex = 3;
            this.m_CurrentText.Text = "Current:";
            // 
            // m_CurrentColor
            // 
            this.m_CurrentColor.Location = new System.Drawing.Point(91, 100);
            this.m_CurrentColor.Name = "m_CurrentColor";
            this.m_CurrentColor.Size = new System.Drawing.Size(83, 19);
            this.m_CurrentColor.TabIndex = 4;
            // 
            // m_BlackCount
            // 
            this.m_BlackCount.AutoSize = true;
            this.m_BlackCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_BlackCount.Location = new System.Drawing.Point(88, 19);
            this.m_BlackCount.Name = "m_BlackCount";
            this.m_BlackCount.Size = new System.Drawing.Size(14, 13);
            this.m_BlackCount.TabIndex = 5;
            this.m_BlackCount.Text = "0";
            // 
            // m_WhiteCount
            // 
            this.m_WhiteCount.AutoSize = true;
            this.m_WhiteCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_WhiteCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_WhiteCount.Location = new System.Drawing.Point(88, 58);
            this.m_WhiteCount.Name = "m_WhiteCount";
            this.m_WhiteCount.Size = new System.Drawing.Size(14, 13);
            this.m_WhiteCount.TabIndex = 6;
            this.m_WhiteCount.Text = "0";
            // 
            // m_MovesLogger
            // 
            this.m_MovesLogger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_MovesLogger.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.moveNumber,
            this.player,
            this.position});
            this.m_MovesLogger.Location = new System.Drawing.Point(0, 140);
            this.m_MovesLogger.Margin = new System.Windows.Forms.Padding(0);
            this.m_MovesLogger.MultiSelect = false;
            this.m_MovesLogger.Name = "m_MovesLogger";
            this.m_MovesLogger.Size = new System.Drawing.Size(229, 314);
            this.m_MovesLogger.TabIndex = 7;
            this.m_MovesLogger.UseCompatibleStateImageBehavior = false;
            this.m_MovesLogger.View = System.Windows.Forms.View.Details;
            // 
            // moveNumber
            // 
            this.moveNumber.Text = "#";
            this.moveNumber.Width = 30;
            // 
            // player
            // 
            this.player.Text = "Player";
            this.player.Width = 70;
            // 
            // position
            // 
            this.position.Text = "Position";
            this.position.Width = 80;
            // 
            // FormsStatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.Controls.Add(this.m_MovesLogger);
            this.Controls.Add(this.m_WhiteCount);
            this.Controls.Add(this.m_BlackCount);
            this.Controls.Add(this.m_CurrentColor);
            this.Controls.Add(this.m_CurrentText);
            this.Controls.Add(this.m_WhiteText);
            this.Controls.Add(this.m_BlackText);
            this.Name = "FormsStatusView";
            this.Size = new System.Drawing.Size(229, 454);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_BlackText;
        private System.Windows.Forms.Label m_WhiteText;
        private System.Windows.Forms.Label m_CurrentText;
        private System.Windows.Forms.Panel m_CurrentColor;
        private System.Windows.Forms.Label m_BlackCount;
        private System.Windows.Forms.Label m_WhiteCount;
        private System.Windows.Forms.ListView m_MovesLogger;
        private System.Windows.Forms.ColumnHeader moveNumber;
        private System.Windows.Forms.ColumnHeader player;
        private System.Windows.Forms.ColumnHeader position;
    }
}