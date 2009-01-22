using System;
using Othello;

namespace Reverci
{
    internal class OthelloData
    {
        private static readonly string r_AppDirectory =
            Environment.GetEnvironmentVariable("USERPROFILE") + "/" + ".othello";

        private static readonly string k_StatisticsFileName = r_AppDirectory + "/" + "statistics.otl";
        private static readonly string k_OptionsFileName = r_AppDirectory + "/" + "options.otl";

        public StatisticsHolder Statistics { get; private set; }

        public GameOptions OthelloOptions { get; private set; }

        private OthelloData()
        {
            Statistics = new StatisticsHolder();
            OthelloOptions = new GameOptions();
        }

        public static OthelloData GetInstance()
        {
            return InstanceHolder.Instance;
        }

        private static class InstanceHolder
        {
            public static readonly OthelloData Instance = new OthelloData();
        }

        public void LoadData()
        {
            FileIO.CreateIfNotExists(r_AppDirectory);
            var statistics = (StatisticsHolder)FileIO.ReadObjectFrom(k_StatisticsFileName);
            if (statistics != null)
            {
                Statistics = statistics;
            }

            var options = (GameOptions)FileIO.ReadObjectFrom(k_OptionsFileName);
            if (options != null)
            {
                OthelloOptions = options;
            }
        }

        public void SaveData()
        {
            FileIO.SaveObjectToFile(k_OptionsFileName, OthelloOptions);
            FileIO.SaveObjectToFile(k_StatisticsFileName, Statistics);
        }
    }
}