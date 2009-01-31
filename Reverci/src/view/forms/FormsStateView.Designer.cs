using System.Drawing;
using System.Windows.Forms;

namespace Reverci.view.forms
{
    partial class FormsStateView
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

            // this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.m_StateMessage = new System.Windows.Forms.ToolStripStatusLabel();

            // 
            // statusStrip1
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                             this.m_ProgressBar,
                                                                             this.m_StateMessage});
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            // this.Location = new System.Drawing.Point(0, 466);
            // this.Name = "statusStrip1";
            // this.Size = new System.Drawing.Size(633, 22);
            // this.TabIndex = 2;
            // this.Text = "statusStrip1";
            // 
            // m_ProgressBar
            // 
            this.m_ProgressBar.Name = "m_ProgressBar";
            this.m_ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.m_ProgressBar.Visible = false;
            // 
            // m_StateMessage
            // 
            this.m_StateMessage.Name = "m_StateMessage";
            this.m_StateMessage.Size = new System.Drawing.Size(109, 17);
            this.m_StateMessage.Text = "m_StateMessage";
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}