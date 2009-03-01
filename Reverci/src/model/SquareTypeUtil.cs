using Reverci.board;

namespace Reverci.model
{
    internal class SquareTypeUtil
    {
        public static eCoinType GetOtherColor(eCoinType i_Color)
        {
            eCoinType color;
            if (i_Color == eCoinType.Black)
            {
                color = eCoinType.White;
            }
            else
            {
                color = eCoinType.Black;
            }

            return color;
        }
    }
}