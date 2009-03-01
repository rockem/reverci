using Reverci.command;
using Reverci.view;

namespace Reverci.comp
{
    internal class ButtonController : IButtonEventListener
    {
        private readonly ICommand r_Command;
        private readonly IButtonView r_View;

        public ButtonController(IButtonView i_View, eCommandType i_Command)
        {
            r_View = i_View;
            r_View.SetEventListener(this);
            r_Command = CommandCommander.GetInstance().GetCommand(i_Command);
            r_Command.StatusChanged += handleStatusChange;
        }

        private void handleStatusChange(object sender)
        {
            var command = (ICommand)sender;
            r_View.SetChecked(command.Checked);
            r_View.SetEnabled(command.Enabled);
        }

        public void DispatchClick()
        {
            r_Command.DoCommand();
        }

        public void SetChecked(bool i_Checked)
        {
            r_View.SetChecked(i_Checked);
        }

        public void SetEnabled(bool i_Enabled)
        {
            r_View.SetEnabled(i_Enabled);
        }

        public void SetText(string i_Text)
        {
        }

        public void SetVisible(bool i_Visible)
        {
        }
    }
}