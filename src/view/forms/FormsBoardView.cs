using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Reverci.comp;
using Reverci.model;

namespace Reverci.view.forms
{
    internal class FormsBoardView : Panel, IBoardView, IBoardViewEventListener
    {
        private const int r_LabelsThickness = 20;
        private static int s_BoardSize = 8;

        private BoardSquare[][] m_ControlSquares;
        private Label[] m_LetterLabels;
        private Label[] m_NumberLabels;
        private Label m_middleLabel;
        private IBoardViewEventListener m_ViewEventListener;

        public FormsBoardView()
        {
            InitializeComponent();
            m_ControlSquares = createSquareControls();
            m_LetterLabels = new CreateLetterLabels(this).createLabels();
            m_NumberLabels = new CreateNumberLabels(this).createLabels();
            m_middleLabel = new CreateMiddleLabel(this).createLabels()[0];
        }

        private abstract class CreateLabelsCommand
        {
            private readonly Panel m_BoardPanel;

            protected CreateLabelsCommand(Panel i_BoardPanel)
            {
                m_BoardPanel = i_BoardPanel;
            }

            public Label[] createLabels()
            {
                var labels = new Label[s_BoardSize];
                for (var i = 0; i < s_BoardSize; i++)
                {
                    labels[i] = createInitialLabel();
                    setBoundsAndTextOn(labels[i], i);
                    m_BoardPanel.Controls.Add(labels[i]);
                }

                return labels;
            }

            private Label createInitialLabel()
            {
                var label = new Label
                                {
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    BackColor = Color.DarkGray,
                                    ForeColor = Color.White
                                };
                return label;
            }

            protected abstract void setBoundsAndTextOn(Label o_Label, int i_Index);
        }

        private class CreateLetterLabels : CreateLabelsCommand
        {
            public CreateLetterLabels(Panel i_BoardPanel) : base(i_BoardPanel)
            {
            }

            protected override void setBoundsAndTextOn(Label o_Label, int i_Index)
            {
                const int v_AAsciiCode = 65;
                o_Label.Top = 0;
                o_Label.Text = char.ConvertFromUtf32(i_Index + v_AAsciiCode);
                o_Label.Height = r_LabelsThickness;
            }
        }

        private class CreateNumberLabels : CreateLabelsCommand
        {
            public CreateNumberLabels(Panel i_BoardPanel) : base(i_BoardPanel)
            {
            }

            protected override void setBoundsAndTextOn(Label o_Label, int i_Index)
            {
                o_Label.Left = 0;
                o_Label.Text = (i_Index + 1) + string.Empty;
                o_Label.Width = r_LabelsThickness;
            }
        }

        private class CreateMiddleLabel : CreateLabelsCommand
        {
            public CreateMiddleLabel(Panel i_BoardPanel) : base(i_BoardPanel)
            {
            }

            protected override void setBoundsAndTextOn(Label o_Label, int i_Index)
            {
                o_Label.Left = 0;
                o_Label.Top = 0;
            }
        }

        private BoardSquare[][] createSquareControls()
        {
            var boardSquares = new BoardSquare[s_BoardSize][];
            for (var i = 0; i < s_BoardSize; i++)
            {
                boardSquares[i] = new BoardSquare[s_BoardSize];
                for (var j = 0; j < s_BoardSize; j++)
                {
                    var boardSquare = new BoardSquare { PieceType = eSquareType.Empty };
                    boardSquare.Position = new Point(i, j);
                    boardSquare.setEventListener(this);
                    Controls.Add(boardSquare);
                    boardSquares[i][j] = boardSquare;
                }
            }

            return boardSquares;
        }

        private void repaintBoard()
        {
            adjustMiddleLabelBounds();
            for (var i = 0; i < s_BoardSize; i++)
            {
                adjustLetterLabelsBoundsAt(i);
                adjustNumberLabelsBoundsAt(i);
                for (var j = 0; j < s_BoardSize; j++)
                {
                    setSquareDimensionsAt(j, i);
                    setSquareLocationAt(j, i);
                }
            }

            refreshView();
        }

        private void adjustLetterLabelsBoundsAt(int i)
        {
            m_LetterLabels[i].Width = getSquareWidth();
            m_LetterLabels[i].Left = (i * m_LetterLabels[i].Width) + r_LabelsThickness;
        }

        private void adjustNumberLabelsBoundsAt(int i)
        {
            m_NumberLabels[i].Height = getSquareHeight();
            m_NumberLabels[i].Top = (i * m_NumberLabels[i].Height) + r_LabelsThickness;
        }

        private void adjustMiddleLabelBounds()
        {
            m_middleLabel.Height = getSquareHeight();
            m_middleLabel.Width = getSquareWidth();
        }

        private int getSquareWidth()
        {
            return (Width - r_LabelsThickness) / s_BoardSize;
        }

        private void setSquareLocationAt(int i, int j)
        {
            m_ControlSquares[i][j].Left = (i * m_ControlSquares[i][j].Width) + r_LabelsThickness;
            m_ControlSquares[i][j].Top = (j * m_ControlSquares[i][j].Height) + r_LabelsThickness;
        }

        private void setSquareDimensionsAt(int i, int j)
        {
            m_ControlSquares[i][j].Width = getSquareWidth();
            m_ControlSquares[i][j].Height = getSquareHeight();
        }

        private int getSquareHeight()
        {
            return (Height - r_LabelsThickness) / s_BoardSize;
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
                       | AnchorStyles.Left)
                      | AnchorStyles.Right);
            ResumeLayout(false);
        }

        public void updateBoardWith(eSquareType[][] i_BoardData)
        {
            if (i_BoardData.Length != s_BoardSize)
            {
                s_BoardSize = i_BoardData.Length;
                setupBoard();
            }

            for (var i = 0; i < s_BoardSize; i++)
            {
                for (var j = 0; j < s_BoardSize; j++)
                {
                    m_ControlSquares[i][j].PieceType = i_BoardData[i][j];
                    if (m_ControlSquares[i][j].PieceType == eSquareType.Move)
                    {
                        m_ControlSquares[i][j].BackgroundType = eBackType.ValidMove;
                    }
                    else
                    {
                        m_ControlSquares[i][j].BackgroundType = eBackType.Normal;
                    }
                }
            }

            repaintBoard();
        }

        private void setupBoard()
        {
            m_ControlSquares = createSquareControls();
            m_LetterLabels = new CreateLetterLabels(this).createLabels();
            m_NumberLabels = new CreateNumberLabels(this).createLabels();
            m_middleLabel = new CreateMiddleLabel(this).createLabels()[0];
        }

        public void setEventListener(IBoardViewEventListener i_Listener)
        {
            m_ViewEventListener = i_Listener;
        }

        public void addPreview(List<Point> points, eSquareType color)
        {
            if (points.Count > 0)
            {
                var firstPoint = points[0];
                m_ControlSquares[firstPoint.X][firstPoint.Y].PieceType = color;
                for (var i = 1; i < points.Count; i++)
                {
                    m_ControlSquares[points[i].X][points[i].Y].PieceType = shadowColorFor(color);
                }
            }

            refreshView();
        }

        private delegate void RefreshDelegator();

        private void refreshView()
        {
            var dlg = new RefreshDelegator(Refresh);
            Invoke(dlg);
        }

        private eSquareType shadowColorFor(eSquareType color)
        {
            if (color == eSquareType.Black)
            {
                return eSquareType.ShadowBlack;
            }

            return eSquareType.ShadowWhite;
        }

        public void DispatchMove(int x, int y)
        {
            m_ViewEventListener.DispatchMove(x, y);
        }

        public void DispatchLeaveSquare(int x, int y)
        {
            m_ViewEventListener.DispatchLeaveSquare(x, y);
        }

        public void DispatchEnterSquare(int x, int y)
        {
            m_ViewEventListener.DispatchEnterSquare(x, y);
        }

        private void FormsBoardView_Resize(object sender, System.EventArgs e)
        {
            repaintBoard();
        }

        public void enableResizeEvents()
        {
            Resize += FormsBoardView_Resize;
        }
    }
}