namespace Othello.player
{
    public class PlayerFactory
    {
        public static IPlayer createPlayerOfType(ePlayerType i_PlayerType)
        {
            IPlayer player = null;
            switch(i_PlayerType)
            {
                case ePlayerType.Computer:
                    player = new ComputerPlayer();
                    break;
                case ePlayerType.Human:
                    player = new HumanPlayer();
                    break;
            }

            return player;
        }
    }
}