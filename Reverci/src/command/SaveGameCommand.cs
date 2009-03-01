namespace Reverci.command
{
    internal class SaveGameCommand : AbstractCommand
    {
        public SaveGameCommand(IGameCommander i_Commander) : base(i_Commander)
        {
        }

        public override void DoCommand()
        {
            if (getCommander().SaveGame())
            {
                Enabled = false;
            }
        }
    }
}