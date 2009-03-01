using Reverci.player;

namespace Reverci.command
{
    internal class BlackDumbComputerCommand : AbstractCommand
    {
        public BlackDumbComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetBlackPlayerAs(ePlayerType.DumbComputer);
        }
    }
}