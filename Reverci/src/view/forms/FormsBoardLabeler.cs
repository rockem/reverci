using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Reverci.comp;
using Reverci.model;

namespace Reverci.view.forms
{
    public partial class FormsBoardLabeler : Panel, IBoardView, IBoardViewEventListener
    {
        private const int r_LabelsThickness = 20;

        private readonly IBoardView r_BoardView;
        private static int s_BoardSize = 8;
        private Label[] m_LetterLabels;
        private Label[] m_NumberLabels;
        private IBoardViewEventListener m_EventListener;

        public FormsBoardLabeler(IBoardView i_BoardView)
        {
            r_BoardView = i_BoardView;
            r_BoardView.setEventListener(this);
            InitializeComponent();
            initializeBoardComponent();
            m_LetterLabels = new CreateLetterLabels(this).createLabels();
            m_NumberLabels = new CreateNumberLabels(this).createLabels();
            new CreateMiddleLabel(this).createLabels();
        }

        private void initializeBoardComponent()
        {
            var board = (Panel)r_BoardView;
            board.Top = r_LabelsThickness;
            board.Left = r_LabelsThickness;
            board.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
                             | AnchorStyles.Left)
                            | AnchorStyles.Right);
            board.Margin = new Padding(0, 0, 0, 0);
            board.Size = new Size(Width - r_LabelsThickness, Height - r_LabelsThickness);
            Controls.Add(board);
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
            public CreateLetterLabels(Panel i_BoardPanel)
                : base(i_BoardPanel)
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
            public CreateNumberLabels(Panel i_BoardPanel)
                : base(i_BoardPanel)
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
            public CreateMiddleLabel(Panel i_BoardPanel)
                : base(i_BoardPanel)
            {
            }

            protected override void setBoundsAndTextOn(Label o_Label, int i_Index)
            {
                o_Label.Left = 0;
                o_Label.Top = 0;
                o_Label.Width = r_LabelsThickness;
                o_Label.Height = r_LabelsThickness;
            }
        }

        private void repaintLabels()
        {
            for (var i = 0; i < s_BoardSize; i++)
            {
                adjustLetterLabelsBoundsAt(i);
                adjustNumberLabelsBoundsAt(i);
            }

            refreshView();
        }

        private delegate void RefreshDelegator();

        private void refreshView()
        {
            var dlg = new RefreshDelegator(Refresh);
            Invoke(dlg);
        }

        private void adjustLetterLabelsBoundsAt(int i)
        {
            m_LetterLabels[i].Width = getSquareWidth();
            m_LetterLabels[i].Left = (i * m_LetterLabels[i].Width) + r_LabelsThickness;
        }

        private int getSquareWidth()
        {
            return (Width - r_LabelsThickness) / s_BoardSize;
        }

        private void adjustNumberLabelsBoundsAt(int i)
        {
            m_NumberLabels[i].Height = getSquareHeight();
            m_NumberLabels[i].Top = (i * m_NumberLabels[i].Height) + r_LabelsThickness;
        }

        private int getSquareHeight()
        {
            return (Height - r_LabelsThickness) / s_BoardSize;
        }

        public void UpdateBoardWith(eSquareType[][] i_BoardData)
        {
            if (i_BoardData.Length != s_BoardSize)
            {
                s_BoardSize = i_BoardData.Length;
                m_LetterLabels = new CreateLetterLabels(this).createLabels();
                m_NumberLabels = new CreateNumberLabels(this).createLabels();
            }

            r_BoardView.UpdateBoardWith(i_BoardData);

            repaintLabels();
        }

        public void setEventListener(IEventListener i_Listener)
        {
            m_EventListener = (IBoardViewEventListener)i_Listener;
        }

        public void AddPreview(List<Point> i_Points, eSquareType i_Color)
        {
            r_BoardView.AddPreview(i_Points, i_Color);
        }

        public void EnableResizeEvents()
        {
            r_BoardView.EnableResizeEvents();
            Resize += FormsBoardLabeler_Resize;
        }

        public void DispatchMove(int i_X, int i_Y)
        {
            m_EventListener.DispatchMove(i_X, i_Y);
        }

        public void DispatchLeaveSquare(int i_X, int i_Y)
        {
            m_EventListener.DispatchLeaveSquare(i_X, i_Y);
        }

        public void DispatchEnterSquare(int i_X, int i_Y)
        {
            m_EventListener.DispatchEnterSquare(i_X, i_Y);
        }

        private void FormsBoardLabeler_Resize(object sender, System.EventArgs e)
        {
            repaintLabels();
        }
    }
}