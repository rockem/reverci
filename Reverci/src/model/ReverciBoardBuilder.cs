using Reverci.board;

namespace Reverci.model
{
    public class ReverciBoardBuilder
    {
        public static eCoinType[][] CreateInitialBoardWithSize(int i_BoardSize)
        {
            var boardState = createEmptyBoard(i_BoardSize);

            var middleOfBoard = i_BoardSize / 2;
            boardState[middleOfBoard - 1][middleOfBoard - 1] = eCoinType.White;
            boardState[middleOfBoard][middleOfBoard] = eCoinType.White;
            boardState[middleOfBoard][middleOfBoard - 1] = eCoinType.Black;
            boardState[middleOfBoard - 1][middleOfBoard] = eCoinType.Black;
            return boardState;
        }

        private static eCoinType[][] createEmptyBoard(int i_BoardSize)
        {
            var boardState = new eCoinType[i_BoardSize][];
            for (var i = 0; i < i_BoardSize; i++)
            {
                boardState[i] = new eCoinType[i_BoardSize];
                for (var j = 0; j < i_BoardSize; j++)
                {
                    boardState[i][j] = eCoinType.Empty;
                }
            }

            return boardState;
        }
    }
}