namespace Reverci.command
{
    public class NewGameCommand : AbstractCommand
    {
        public NewGameCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().StartNewGame();
        }
    }
}