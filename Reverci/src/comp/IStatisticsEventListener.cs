﻿using Reverci.comp;

namespace Reverci.comp
{
    public interface IStatisticsEventListener : IEventListener
    {
        void ResetStatistics();
    }
}