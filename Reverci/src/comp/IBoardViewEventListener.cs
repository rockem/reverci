﻿namespace Reverci.comp
{
    public interface IBoardViewEventListener : IEventListener
    {
        void DispatchMove(int x, int y);

        void DispatchLeaveSquare(int x, int y);

        void DispatchEnterSquare(int x, int y);
    }
}