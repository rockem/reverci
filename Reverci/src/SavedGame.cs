using System;
using System.Collections.Generic;
using Reverci.board;

namespace Reverci
{
    [Serializable]
    public class SavedGame
    {
        public eCoinType CurrentTurn { get; set; }

        public eCoinType[][] Board { get; set; }

        public List<string[]> MovesHistory { get; set; }
    }
}