using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Reverci.view.forms
{
    public partial class FormsStatusView : UserControl, IStatusView
    {
        private readonly Dictionary<eColorType, Color> r_TypeToColorMap = new Dictionary<eColorType, Color>();

        public FormsStatusView()
        {
            InitializeComponent();
            initTypeToColor();
        }

        private void initTypeToColor()
        {
            r_TypeToColorMap.Add(eColorType.Black, Color.Black);
            r_TypeToColorMap.Add(eColorType.White, Color.White);
            r_TypeToColorMap.Add(eColorType.NoColor, Color.LightSlateGray);
        }

        public void UpdateCurrentColor(eColorType i_Color)
        {
            m_CurrentColor.BackColor = r_TypeToColorMap[i_Color];
        }

        public void UpdatePieceQuantity(int i_BlackCount, int i_WhiteCount)
        {
            GenericDelegate dlg = () =>
                                      {
                                          m_BlackCount.Text = i_BlackCount.ToString();
                                          m_WhiteCount.Text = i_WhiteCount.ToString();
                                      };
            Invoke(dlg);
        }

        private delegate void GenericDelegate();

        public void UpdateMovesList(List<string[]> i_MovesList)
        {
            var dlg = new UpdateListDelegator(updateMovesList);
            Invoke(dlg, new object[] { i_MovesList });
        }

        private delegate void UpdateListDelegator(List<string[]> i_MovesList);

        private void updateMovesList(List<string[]> i_MovesList)
        {
            m_MovesLogger.Items.Clear();
            foreach (var row in i_MovesList)
            {
                m_MovesLogger.Items.Add(new ListViewItem(row));
                m_MovesLogger.Items[m_MovesLogger.Items.Count - 1].EnsureVisible();
            }
        }
    }
}