using System.Collections.Generic;
using Reverci.view;

namespace Reverci.comp
{
    public class StateController
    {
        private readonly IStateView r_StateView;

        private Dictionary<eStateType, string> r_MessagesMap;

        public StateController(IStateView i_View)
        {
            r_StateView = i_View;
            initMessageMap();
        }

        private void initMessageMap()
        {
            r_MessagesMap = new Dictionary<eStateType, string>
                                {
                                    { eStateType.BlackTurn, StatusMessages.BlackTurnMessage },
                                    { eStateType.WhiteTurn, StatusMessages.WhiteTurnMessage },
                                    { eStateType.BlackWin, StatusMessages.BlackWinMessage },
                                    { eStateType.WhiteWin, StatusMessages.WhiteWinMessage },
                                    { eStateType.Draw, StatusMessages.DrawMessage },
                                    { eStateType.Thinking, StatusMessages.ThinkMessage }
                                };
        }

        public void UpdateState(eStateType i_State)
        {
            r_StateView.updateStatusMessageWith(r_MessagesMap[i_State]);
        }

        public void ShowProcessing(bool i_Show)
        {
            r_StateView.SetThinking(i_Show);
        }
    }
}