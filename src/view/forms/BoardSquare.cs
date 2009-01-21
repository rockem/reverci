using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Othello.comp;
using Othello.model;

namespace Othello.view.forms
{
    public enum eBackType
    {
        Normal,
        ValidMove
    }

    public partial class BoardSquare : UserControl, IBoardSquareView
    {
        private static readonly Color r_EmptyBackColor = Color.MediumSlateBlue;
        private static readonly Color r_MoveBackColor = Color.DarkOliveGreen;
        private IBoardViewEventListener m_ViewEventListener;
        private Color m_BackgroundColor;
        private eSquareType m_PieceType;
        private eBackType m_BackgroundType;

        public BoardSquare()
        {
            InitializeComponent();
            ResizeRedraw = true;

            // Prevent the control from receiving focus via the TAB key.
            TabStop = false;

            // Set double-buffering to prevent flicker when drawing the control.
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public eSquareType PieceType
        {
            get { return m_PieceType; }
            set { m_PieceType = value; }
        }

        public eBackType BackgroundType
        {
            get { return m_BackgroundType; }
            set
            {
                m_BackgroundType = value;
                setBackgroundColor();
            }
        }

        public Point Position { get; set; }

        private void BoardSquare_Paint(object sender, PaintEventArgs e)
        {
            // For Smoother graphics
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(m_BackgroundColor);

            drawSquareBorder(e);

            if (m_PieceType != eSquareType.Empty && m_PieceType != eSquareType.Move)
            {
                drawBoardPiece(e);
            }
        }

        private void setBackgroundColor()
        {
            if (m_BackgroundType == eBackType.ValidMove)
            {
                m_BackgroundColor = r_MoveBackColor;
            }
            else
            {
                m_BackgroundColor = r_EmptyBackColor;
            }
        }

        private void drawSquareBorder(PaintEventArgs e)
        {
            var pen = new Pen(Color.Black) { Width = 1 };
            var topLeft = new Point(0, 0);
            var topRight = new Point(Width - 1, 0);
            var bottomLeft = new Point(0, Height - 1);
            var bottomRight = new Point(Width - 1, Height - 1);

            pen.Color = adjustBrightness(m_BackgroundColor, 1.5);
            e.Graphics.DrawLine(pen, bottomLeft, topLeft);
            e.Graphics.DrawLine(pen, topLeft, topRight);

            pen.Color = adjustBrightness(m_BackgroundColor, 0.6);
            e.Graphics.DrawLine(pen, bottomLeft, bottomRight);
            e.Graphics.DrawLine(pen, bottomRight, topRight);
        }

        private void drawBoardPiece(PaintEventArgs e)
        {
            var pieceWidth = (int)(Width * 0.80);
            var pieceHeight = (int)(Height * 0.80);
            var left = (Width - pieceWidth) / 2;
            var top = (Height - pieceHeight) / 2;

            var path = new GraphicsPath();
            path.Reset();
            path.AddEllipse(left, top, pieceWidth, pieceHeight);

            var solidBrush = new SolidBrush(Color.Black);
            var gradientBrush = new PathGradientBrush(path)
                                {
                                    CenterPoint = new Point(pieceWidth / 3, pieceHeight / 3)
                                };

            setPieceColor(gradientBrush, solidBrush);

            e.Graphics.FillEllipse(solidBrush, left, top, pieceWidth, pieceHeight);
            gradientBrush.SurroundColors = new[] { solidBrush.Color };
            e.Graphics.FillEllipse(gradientBrush, left, top, pieceWidth, pieceHeight);
            gradientBrush.Dispose();
        }

        private void setPieceColor(PathGradientBrush gradientBrush, SolidBrush solidBrush)
        {
            if (m_PieceType == eSquareType.White || m_PieceType == eSquareType.ShadowWhite)
            {
                if (m_PieceType == eSquareType.ShadowWhite)
                {
                    solidBrush.Color = Color.FromArgb(96, adjustBrightness(Color.White, 0.80));
                }
                else
                {
                    solidBrush.Color = adjustBrightness(Color.White, 0.80);
                }

                solidBrush.Color = adjustBrightness(Color.White, 0.80);
                gradientBrush.CenterColor = Color.White;
            }
            else
            {
                if (m_PieceType == eSquareType.ShadowBlack)
                {
                    solidBrush.Color = Color.FromArgb(96, Color.Black);
                }
                else
                {
                    solidBrush.Color = Color.Black;
                }

                gradientBrush.CenterColor = Color.FromArgb(128, Color.DarkGray);
            }

            if (m_PieceType == eSquareType.ShadowWhite || m_PieceType == eSquareType.ShadowBlack)
            {
                solidBrush.Color = Color.Gray;
            }
        }

        private Color adjustBrightness(Color color, double m)
        {
            var r = (int)Math.Max(0, Math.Min(255, Math.Round(color.R * m)));
            var g = (int)Math.Max(0, Math.Min(255, Math.Round(color.G * m)));
            var b = (int)Math.Max(0, Math.Min(255, Math.Round(color.B * m)));

            return Color.FromArgb(r, g, b);
        }

        public void setEventListener(IBoardViewEventListener i_listener)
        {
            m_ViewEventListener = i_listener;
        }

        private void BoardSquare_Click(object sender, MouseEventArgs e)
        {
            m_ViewEventListener.DispatchMove(Position.X, Position.Y);
        }

        private void BoardSquare_MouseLeave(object sender, EventArgs e)
        {
            m_ViewEventListener.DispatchLeaveSquare(Position.X, Position.Y);
        }

        private void BoardSquare_MouseEnter(object sender, EventArgs e)
        {
            m_ViewEventListener.DispatchEnterSquare(Position.X, Position.Y);
        }
    }
}