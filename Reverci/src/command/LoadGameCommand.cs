namespace Reverci.command
{
    internal class LoadGameCommand : AbstractCommand
    {
        public LoadGameCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().LoadGame();
        }
    }
}