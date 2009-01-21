using NUnit.Framework;
using Othello.player;

namespace Othello
{
    [TestFixture]
    public class StatisticsHolderTest
    {
        private StatisticsHolder statisticsHolder;
        private int blackScore;
        private int whiteScore;

        [SetUp]
        public void setUp()
        {
            /* var ci =
                typeof(StatisticsHolder).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, System.Type.EmptyTypes, null);
            statisticsHolder = (StatisticsHolder)ci.Invoke(System.Type.EmptyTypes);*/
            statisticsHolder = new StatisticsHolder();
        }

        [Test]
        public void testShouldAddOneGameScore()
        {
            blackScore = 1;
            whiteScore = 2;
            statisticsHolder.AddGameScore(blackScore, whiteScore,
                                          ePlayerType.Computer, ePlayerType.Human);

            Assert.AreEqual(blackScore, statisticsHolder.BlackScore);
            Assert.AreEqual(whiteScore, statisticsHolder.WhiteScore);
            Assert.AreEqual(0, statisticsHolder.BlackWins);
            Assert.AreEqual(blackScore, statisticsHolder.WhiteWins);
            Assert.AreEqual(0, statisticsHolder.ComputerWins);
            Assert.AreEqual(blackScore, statisticsHolder.HumanWins);
            Assert.AreEqual(blackScore, statisticsHolder.CompuerScore);
            Assert.AreEqual(whiteScore, statisticsHolder.HumanScore);
        }

        [Test]
        public void testShouldAddDrawGame()
        {
            statisticsHolder.AddGameScore(2, 2, ePlayerType.Human, ePlayerType.Computer);
            Assert.AreEqual(1, statisticsHolder.BnWDraws);
            Assert.AreEqual(1, statisticsHolder.HnCDraws);
        }

        [Test]
        public void testShouldAddHumanToHumanGame()
        {
            statisticsHolder.AddGameScore(1, 2, ePlayerType.Human, ePlayerType.Human);
            Assert.AreEqual(0, statisticsHolder.ComputerWins);
            Assert.AreEqual(0, statisticsHolder.HumanWins);
            Assert.AreEqual(0, statisticsHolder.CompuerScore);
            Assert.AreEqual(0, statisticsHolder.HumanScore);
        }
    }
}