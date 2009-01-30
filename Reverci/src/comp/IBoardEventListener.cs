using Reverci.model;

namespace Reverci.comp
{
    public interface IBoardEventListener
    {
        void DispatchLastMove(int i_X, int i_Y, eSquareType i_Color);

        void DispatchCurrentState(eStateType i_State);

        void DispatchPieceQuantity(int i_BlackCount, int i_WhiteCount);
    }
}