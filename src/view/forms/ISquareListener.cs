using Reverci.comp;

namespace Reverci.view.forms
{
    internal interface ISquareListener : IEventListener
    {
        void DispatchClick(int x, int y);

        void DispatchLeaveSquare(int x, int y);

        void DispatchEnterSquare(int x, int y);
    }
}