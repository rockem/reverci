namespace Reverci.model
{
    internal class SquareTypeUtil
    {
        public static eSquareType GetOtherColor(eSquareType i_Color)
        {
            eSquareType color;
            if (i_Color == eSquareType.Black)
            {
                color = eSquareType.White;
            }
            else
            {
                color = eSquareType.Black;
            }

            return color;
        }
    }
}