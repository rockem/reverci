using System;
using System.Drawing;

namespace Reverci.player
{
    internal class HumanPlayer : AbstractPlayer
    {
        public override Point GetMove()
        {
            throw new NotImplementedException();
        }

        public override bool IsAutomatic()
        {
            return false;
        }
    }
}