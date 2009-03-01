using NUnit.Framework;
using Reverci.board;

namespace ReverciUT.board
{
    [TestFixture]
    public class CoinTest
    {
        [Test]
        public void testShouldBeEqual()
        {
            Assert.AreEqual(new Coin(eCoinType.Empty, 1, 1), new Coin(eCoinType.Empty, 1, 1));
        }
    }
}