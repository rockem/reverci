using Reverci.player;

namespace Reverci.command
{
    public interface IGameCommander
    {
        void Exit();

        void ShowStatistics();

        void StartNewGame();

        void ShowPreview(bool i_Preview);

        void ShowValidMoves(bool i_ValidMoves);

        bool SaveGame();

        void LoadGame();

        void SetBlackPlayerAs(ePlayerType i_PlayerType);

        void SetWhitePlayerAs(ePlayerType i_PlayerType);
    }
}