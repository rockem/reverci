using System;
using System.Drawing;
using System.Threading;

namespace Reverci.player
{
    internal class ComputerPlayer : AbstractPlayer
    {
        public override bool IsAutomatic()
        {
            return true;
        }

        public override Point GetMove()
        {
            Thread.Sleep(200);
            var possibleMoves = getBoardModel().GetPossibleMovesFor(getColor());
            var randomContext = new Random();
            var chosenMove = possibleMoves[randomContext.Next(possibleMoves.Count)];
            return chosenMove;
        }
    }
}