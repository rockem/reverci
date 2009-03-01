namespace Reverci.command
{
    internal class ShowStatisricsCommand : AbstractCommand
    {
        public ShowStatisricsCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().ShowStatistics();
        }
    }
}