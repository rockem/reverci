namespace Reverci.command
{
    public delegate void StatusChangedDelegate(object sender);

    public interface ICommand
    {
        void DoCommand();

        bool Checked { get; set; }

        bool Enabled { get; set; }

        string Text { get; set; }

        bool Visible { get; set; }

        event StatusChangedDelegate StatusChanged;
    }
}