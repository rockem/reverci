using NUnit.Framework;
using Reverci;
using Reverci.comp;
using Reverci.view;

namespace Reverci.comp
{
    [TestFixture]
    public class StateControllerTest
    {
        private class StubStateView : IStateView
        {
            public string m_StatusMessage;

            public void updateStatusMessageWith(string i_Status)
            {
                m_StatusMessage = i_Status;
            }

            public void SetThinking(bool b)
            {
                
            }
        }

        [Test]
        public void testShouldDelegateStatusToView()
        {
            var view = new StubStateView();
            var controller = new StateController(view);
            controller.UpdateState(eStateType.BlackTurn);
            Assert.AreEqual(StatusMessages.BlackTurnMessage, view.m_StatusMessage);
        }
    }
}