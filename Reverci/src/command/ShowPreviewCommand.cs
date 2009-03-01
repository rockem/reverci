namespace Reverci.command
{
    internal class ShowPreviewCommand : AbstractCommand
    {
        public ShowPreviewCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            Checked = !Checked;
            getCommander().ShowPreview(Checked);
        }
    }
}