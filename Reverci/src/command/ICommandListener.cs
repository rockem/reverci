namespace Reverci.command
{
    public interface ICommandListener
    {
        void SetChecked(bool i_Checked);

        void SetEnabled(bool i_Enabled);

        void SetText(string i_Text);

        void SetVisible(bool i_Visible);
    }
}