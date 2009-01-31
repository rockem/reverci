using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Reverci.comp;
using Reverci.model;

namespace Reverci.view.forms
{
    internal class FormsBoardView : Panel, IBoardView, ISquareListener
    {
        private static int s_BoardSize = 8;

        private BoardSquare[][] m_ControlSquares;
        private IBoardViewEventListener m_ViewEventListener;

        public FormsBoardView()
        {
            m_ControlSquares = createSquareControls();
        }

        private BoardSquare[][] createSquareControls()
        {
            var boardSquares = new BoardSquare[s_BoardSize][];
            for (var i = 0; i < s_BoardSize; i++)
            {
                boardSquares[i] = new BoardSquare[s_BoardSize];
                for (var j = 0; j < s_BoardSize; j++)
                {
                    var boardSquare = new BoardSquare
                                          {
                                              PieceType = eSquareType.Empty,
                                              Position = new Point(i, j)
                                          };
                    boardSquare.setEventListener(this);
                    Controls.Add(boardSquare);
                    boardSquares[i][j] = boardSquare;
                }
            }

            return boardSquares;
        }

        public void UpdateBoardWith(eSquareType[][] i_BoardData)
        {
            if (i_BoardData.Length != s_BoardSize)
            {
                s_BoardSize = i_BoardData.Length;
                m_ControlSquares = createSquareControls();
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

        private void repaintBoard()
        {
            for (var i = 0; i < s_BoardSize; i++)
            {
                for (var j = 0; j < s_BoardSize; j++)
                {
                    setSquareDimensionsAt(j, i);
                    setSquareLocationAt(j, i);
                }
            }

            refreshView();
        }

        private void setSquareDimensionsAt(int i, int j)
        {
            m_ControlSquares[i][j].Width = getSquareWidth();
            m_ControlSquares[i][j].Height = getSquareHeight();
        }

        private int getSquareWidth()
        {
            return Width / s_BoardSize;
        }

        private int getSquareHeight()
        {
            return Height / s_BoardSize;
        }

        private void setSquareLocationAt(int i, int j)
        {
            m_ControlSquares[i][j].Left = i * m_ControlSquares[i][j].Width;
            m_ControlSquares[i][j].Top = j * m_ControlSquares[i][j].Height;
        }

        private void refreshView()
        {
            var dlg = new RefreshDelegator(Refresh);
            Invoke(dlg);
        }

        private delegate void RefreshDelegator();

        public void setEventListener(IEventListener i_Listener)
        {
            m_ViewEventListener = (IBoardViewEventListener)i_Listener;
        }

        public void AddPreview(List<Point> i_Points, eSquareType i_Color)
        {
            if (i_Points.Count > 0)
            {
                var firstPoint = i_Points[0];
                m_ControlSquares[firstPoint.X][firstPoint.Y].PieceType = i_Color;
                for (var i = 1; i < i_Points.Count; i++)
                {
                    m_ControlSquares[i_Points[i].X][i_Points[i].Y].PieceType = shadowColorFor(i_Color);
                }
            }

            refreshView();
        }

        private eSquareType shadowColorFor(eSquareType color)
        {
            if (color == eSquareType.Black)
            {
                return eSquareType.ShadowBlack;
            }

            return eSquareType.ShadowWhite;
        }

        public void DispatchClick(int x, int y)
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

        public void EnableResizeEvents()
        {
            Resize += FormsBoardView_Resize;
        }
    }
}