using Reverci.player;

namespace Reverci.command
{
    internal class BlackUserCommand : AbstractCommand
    {
        public BlackUserCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            getCommander().SetBlackPlayerAs(ePlayerType.Human);
        }
    }
}