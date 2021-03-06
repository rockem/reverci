﻿using System.Drawing;
using NUnit.Framework;
using Reverci.board;
using Reverci.model;
using Reverci.player;

namespace ReverciUT.player
{
    [TestFixture]
    public class SmartPlayerTest
    {
        private const int X = 0;
        private const int B = 1;
        private const int W = 2;

        [Test]
        public void testShouldRerieveBestStartingMove()
        {
            var boardModel = new ReverciBoardModel(createBoardFrom(
                                                       new[]
                                                           {
                                                               new[] { X, X, X, X, X },
                                                               new[] { X, W, B, W, X },
                                                               new[] { X, B, X, X, X },
                                                               new[] { X, X, X, X, X },
                                                               new[] { X, X, X, X, X },
                                                           }
                                                       ));
            var player = new SmartComputerPlayer(2);
            player.setBoardModel(boardModel);
            player.setColor(eCoinType.Black);
            Assert.AreEqual(new Point(0, 1), player.GetMove());
        }

        private eCoinType[][] createBoardFrom(int[][] ints)
        {
            var boardSize = ints.Length;
            var arr = new eCoinType[boardSize][];
            for (var i = 0; i < boardSize; i++)
            {
                arr[i] = new eCoinType[boardSize];
                for (var j = 0; j < boardSize; j++)
                {
                    arr[i][j] = convertToSquareType(ints[j][i]);
                }
            }
            return arr;
        }

        private eCoinType convertToSquareType(int i)
        {
            switch (i)
            {
                case B:
                    return eCoinType.Black;
                case W:
                    return eCoinType.White;
            }
            return eCoinType.Empty;
        }
    }
}