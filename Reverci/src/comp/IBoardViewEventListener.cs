namespace Reverci.comp
{
    public interface IBoardViewEventListener : IEventListener
    {
        void DispatchMove(int i_X, int i_Y);

        void DispatchLeaveSquare(int i_X, int i_Y);

        void DispatchEnterSquare(int i_X, int i_Y);
    }
}