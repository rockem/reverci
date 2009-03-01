using System.Windows.Forms;
using Reverci.command;
using Reverci.comp;

namespace Reverci.view.forms
{
    public partial class FormsMainToolbarView : ToolStrip
    {
        public FormsMainToolbarView()
        {
            InitializeComponent();
            new ButtonController(m_NewGameBtn, eCommandType.New);
            new ButtonController(m_SaveGameBtn, eCommandType.SaveGame);
            new ButtonController(m_ShowValidMoves, eCommandType.ShowValidMoves);
            new ButtonController(m_ShowPreview, eCommandType.ShowPreview);
            new ButtonController(m_BlackUser, eCommandType.BlackUser);
            new ButtonController(m_BlackDumbComputer, eCommandType.BlackDumbComputer);
            new ButtonController(m_BlackOkComputer, eCommandType.BlackOkComputer);
            new ButtonController(m_BlackSmartComputer, eCommandType.BlackSmartComputer);
            new ButtonController(m_BlackGeniusComputer, eCommandType.BlackGeniusComputer);
            new ButtonController(m_WhiteUser, eCommandType.WhiteUser);
            new ButtonController(m_WhiteDumbComputer, eCommandType.WhiteDumbComputer);
            new ButtonController(m_WhiteOkComputer, eCommandType.WhiteOkComputer);
            new ButtonController(m_WhiteSmartComputer, eCommandType.WhiteSmartComputer);
            new ButtonController(m_WhiteGeniusComputer, eCommandType.WhiteGeniusComputer);
        }
    }
}