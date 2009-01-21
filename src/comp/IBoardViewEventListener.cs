namespace Othello.comp
{
    public interface IBoardViewEventListener
    {
        void DispatchMove(int x, int y);

        void DispatchLeaveSquare(int x, int y);

        void DispatchEnterSquare(int x, int y);
    }
}