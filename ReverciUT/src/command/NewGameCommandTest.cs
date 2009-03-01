using NUnit.Framework;
using Reverci.command;
using Reverci.player;

namespace ReverciUT.command
{
    [TestFixture]
    public class NewGameCommandTest
    {
        class StubGameView : IGameCommander
        {
            public bool didNewGame;

            public void StartNewGame()
            {
                didNewGame = true;
            }

            public void ShowPreview(bool i_Preview)
            {
                
            }

            public void ShowValidMoves(bool i_ValidMoves)
            {
                
            }

            public bool SaveGame()
            {
                return false;
            }

            public void LoadGame()
            {
                
            }

            public void SetBlackPlayerAs(ePlayerType i_PlayerType)
            {
            }

            public void SetWhitePlayerAs(ePlayerType i_PlayerType)
            {
            }

            public void Exit()
            {
                
            }

            public void ShowStatistics()
            {
                
            }
        }

        [Test]
        public void testShouldDelegateNewGameToController()
        {
            var gv = new StubGameView();
            var ngc = new NewGameCommand(gv);
            ngc.DoCommand();
            Assert.IsTrue(gv.didNewGame);
        }
    }
}