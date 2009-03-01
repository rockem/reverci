using Reverci.player;

namespace Reverci.command
{
    internal class WhiteDumbComputerCommand : AbstractCommand
    {
        public WhiteDumbComputerCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetWhitePlayerAs(ePlayerType.DumbComputer);
        }
    }
}