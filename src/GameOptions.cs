using System;
using Othello.player;

namespace Othello
{
    [Serializable()]
    public class GameOptions
    {
        public GameOptions()
        {
            ShowValidMoves = false;
            ShowPreview = false;
            BoardSize = 8;
            BlackPlayer = ePlayerType.Human;
            WhitePlayer = ePlayerType.Computer;
        }

        public bool ShowValidMoves { get; set; }

        public bool ShowPreview { get; set; }

        public int BoardSize { get; set; }

        public ePlayerType BlackPlayer { get; set; }

        public ePlayerType WhitePlayer { get; set; }
    }
}