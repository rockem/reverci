namespace Reverci.player
{
    public class PlayerFactory
    {
        public static IPlayer createPlayerOfType(ePlayerType i_PlayerType)
        {
            IPlayer player = null;
            switch (i_PlayerType)
            {
                case ePlayerType.Human:
                    player = new HumanPlayer();
                    break;
                case ePlayerType.DumbComputer:
                    player = new ComputerPlayer();
                    break;
                case ePlayerType.OkComputer:
                    player = createSmartPlayerWithSearchDepthOf(0);
                    break;
                case ePlayerType.SmartComputer:
                    player = createSmartPlayerWithSearchDepthOf(2);
                    break;
                case ePlayerType.GeniusComputer:
                    player = createSmartPlayerWithSearchDepthOf(4);
                    break;
            }

            return player;
        }

        private static IPlayer createSmartPlayerWithSearchDepthOf(int i_Depth)
        {
            return new SmartComputerPlayer(i_Depth);
        }
    }
}