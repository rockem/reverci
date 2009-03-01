using Reverci.player;

namespace Reverci.command
{
    internal class WhiteUserCommand : AbstractCommand
    {
        public WhiteUserCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetWhitePlayerAs(ePlayerType.Human);
        }
    }
}