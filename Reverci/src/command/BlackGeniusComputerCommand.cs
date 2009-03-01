using Reverci.player;

namespace Reverci.command
{
    internal class BlackGeniusComputerCommand : AbstractCommand
    {
        public BlackGeniusComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetBlackPlayerAs(ePlayerType.GeniusComputer);
        }
    }
}