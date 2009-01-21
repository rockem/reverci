using System.Collections.Generic;
using Othello.model;
using Othello.view;

namespace Othello.comp
{
    internal class StatusController
    {
        private readonly IStatusView r_StatusView;

        private Dictionary<eStateType, eColorType> m_StateToColorMap;

        private List<string[]> m_MovesList = new List<string[]>();

        public StatusController(IStatusView i_View)
        {
            r_StatusView = i_View;
            initStateToColor();
        }

        private void initStateToColor()
        {
            m_StateToColorMap = new Dictionary<eStateType, eColorType>
                                    {
                                        {eStateType.WhiteTurn, eColorType.White},
                                        {eStateType.BlackTurn, eColorType.Black},
                                        {eStateType.WhiteWin, eColorType.White},
                                        {eStateType.BlackWin, eColorType.Black},
                                        {eStateType.Draw, eColorType.NoColor}
                                    };
        }

        public void UpdateCurrentState(eStateType i_TurnState)
        {
            r_StatusView.UpdateCurrentColor(m_StateToColorMap[i_TurnState]);
        }

        public void UpdatePiecesQunatity(int i_BlackCount, int i_WhiteCount)
        {
            r_StatusView.UpdatePieceQuantity(i_BlackCount, i_WhiteCount);
        }

        public void LogMove(int i_X, int i_Y, eSquareType i_Color)
        {
            m_MovesList.Add(new[]
                            {
                                (m_MovesList.Count + 1).ToString(),
                                i_Color.ToString(),
                                (char)(i_X + 'A') + (i_Y + 1).ToString()
                            });
            updateMovesList();
        }

        private void updateMovesList()
        {
            r_StatusView.UpdateMovesList(m_MovesList);
        }

        public void Clear()
        {
            m_MovesList.Clear();
            updateMovesList();
        }

        public void SetMovesLog(List<string[]> i_List)
        {
            m_MovesList = i_List;
            updateMovesList();
        }

        public List<string[]> GetMovesLog()
        {
            return m_MovesList;
        }
    }
}