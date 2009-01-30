using System;
using Reverci.player;

namespace Reverci
{
    [Serializable]
    public class GameOptions
    {
        public GameOptions()
        {
            ShowValidMoves = false;
            ShowPreview = false;
            BoardSize = 8;
            BlackPlayer = ePlayerType.Human;
            WhitePlayer = ePlayerType.DumbComputer;
        }

        public bool ShowValidMoves { get; set; }

        public bool ShowPreview { get; set; }

        public int BoardSize { get; set; }

        public ePlayerType BlackPlayer { get; set; }

        public ePlayerType WhitePlayer { get; set; }
    }
}