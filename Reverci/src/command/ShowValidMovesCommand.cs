namespace Reverci.command
{
    internal class ShowValidMovesCommand : AbstractCommand
    {
        public ShowValidMovesCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            Checked = !Checked;
            getCommander().ShowValidMoves(Checked);
        }
    }
}