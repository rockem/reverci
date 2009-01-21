namespace Othello.view.forms
{
    partial class BoardSquare
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
            this.SuspendLayout();
            // 
            // BoardSquare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "BoardSquare";
            this.Size = new System.Drawing.Size(64, 64);
            this.MouseLeave += new System.EventHandler(this.BoardSquare_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardSquare_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BoardSquare_Click);
            this.MouseEnter += new System.EventHandler(this.BoardSquare_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}