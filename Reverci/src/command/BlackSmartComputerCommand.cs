using Reverci.player;

namespace Reverci.command
{
    internal class BlackSmartComputerCommand : AbstractCommand
    {
        public BlackSmartComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetBlackPlayerAs(ePlayerType.SmartComputer);
        }
    }
}