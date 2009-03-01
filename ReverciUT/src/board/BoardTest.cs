using System.Collections.Generic;
using NUnit.Framework;
using Reverci.board;

namespace ReverciUT.board
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void testShouldRetrieveAllCoinsFromBoard()
        {
            var boardData = new[]
                                {
                                    new[] { eCoinType.Empty, eCoinType.White, eCoinType.Empty },
                                    new[] { eCoinType.Black, eCoinType.Empty, eCoinType.Empty },
                                    new[] { eCoinType.Empty, eCoinType.Empty, eCoinType.Black },
                                };

            var board = new Board(boardData);
            var coins = new List<Coin>();
            foreach (Coin coin in board)
            {
                coins.Add(coin);
            }
            Assert.AreEqual(3, coins.Count);
            Assert.IsTrue(coins.Contains(new Coin(eCoinType.Black, 1, 0)));
            Assert.IsTrue(coins.Contains(new Coin(eCoinType.White, 0, 1)));
            Assert.IsTrue(coins.Contains(new Coin(eCoinType.Black, 2, 2)));
        }
    }
}