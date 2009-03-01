using Reverci.player;

namespace Reverci.command
{
    internal class WhiteGeniusComputerCommand : AbstractCommand
    {
        public WhiteGeniusComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetWhitePlayerAs(ePlayerType.GeniusComputer);
        }
    }
}