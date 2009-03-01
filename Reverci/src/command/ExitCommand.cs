namespace Reverci.command
{
    internal class ExitCommand : AbstractCommand
    {
        public ExitCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().Exit();
        }
    }
}