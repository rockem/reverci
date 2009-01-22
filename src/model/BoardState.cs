namespace Reverci.model
{
    internal class BoardState
    {
        public static eSquareType[][] CreateInitialBoardWithSize(int i_BoardSize)
        {
            var boardState = createEmptyBoard(i_BoardSize);

            var middleOfBoard = i_BoardSize / 2;
            boardState[middleOfBoard - 1][middleOfBoard - 1] = eSquareType.White;
            boardState[middleOfBoard][middleOfBoard] = eSquareType.White;
            boardState[middleOfBoard][middleOfBoard - 1] = eSquareType.Black;
            boardState[middleOfBoard - 1][middleOfBoard] = eSquareType.Black;
            return boardState;
        }

        private static eSquareType[][] createEmptyBoard(int i_BoardSize)
        {
            var boardState = new eSquareType[i_BoardSize][];
            for (var i = 0; i < i_BoardSize; i++)
            {
                boardState[i] = new eSquareType[i_BoardSize];
                for (var j = 0; j < i_BoardSize; j++)
                {
                    boardState[i][j] = eSquareType.Empty;
                }
            }

            return boardState;
        }
    }
}