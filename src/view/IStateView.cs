namespace Othello.view
{
    internal interface IStateView
    {
        void updateStatusMessageWith(string i_Status);

        void SetThinking(bool b);
    }
}