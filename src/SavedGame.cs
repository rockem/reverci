using System;
using System.Collections.Generic;
using Othello.model;

namespace Othello
{
    [Serializable]
    public class SavedGame
    {
        public eSquareType CurrentTurn { get; set; }

        public IBoardModel Model { get; set; }

        public List<string[]> MovesHistory { get; set; }
    }
}