using System;
using System.Collections.Generic;
using Reverci.model;

namespace Reverci
{
    [Serializable]
    public class SavedGame
    {
        public eSquareType CurrentTurn { get; set; }

        public eSquareType[][] Board { get; set; }

        public List<string[]> MovesHistory { get; set; }
    }
}