using Reverci.comp;

namespace Othello.comp
{
    public interface IStatisticsEventListener : IEventListener
    {
        void ResetStatistics();
    }
}