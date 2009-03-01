using Reverci.player;

namespace Reverci.command
{
    internal class WhiteSmartComputerCommand : AbstractCommand
    {
        public WhiteSmartComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetWhitePlayerAs(ePlayerType.SmartComputer);
        }
    }
}