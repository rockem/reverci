using Reverci.player;

namespace Reverci.command
{
    internal class BlackOkComputerCommand : AbstractCommand
    {
        public BlackOkComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetBlackPlayerAs(ePlayerType.OkComputer);
        }
    }
}