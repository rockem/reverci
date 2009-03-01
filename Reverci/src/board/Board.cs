using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Reverci.board
{
    public class Board : IEnumerable<Coin>
    {
        private readonly eCoinType[][] r_Data;

        public Board(eCoinType[][] i_Data)
        {
            r_Data = i_Data;
        }

        IEnumerator<Coin> IEnumerable<Coin>.GetEnumerator()
        {
            for (var i = 0; i < r_Data.Length; i++)
            {
                for (var j = 0; j < r_Data.Length; j++)
                {
                    if(r_Data[i][j] != eCoinType.Empty)
                    {
                        yield return new Coin(r_Data[i][j], i, j);
                    }
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new BoardEnumerator(r_Data);
        }

        public void Set(eCoinType i_Color, int i_X, int i_Y)
        {
            r_Data[i_X][i_Y] = i_Color;
        }

        public eCoinType Get(int i_X, int i_Y)
        {
            return r_Data[i_X][i_Y];
        }

        public int Size()
        {
            return r_Data.Length;
        }

        public eCoinType[][] GetData()
        {
            var boardCopy = new eCoinType[r_Data.Length][];
            for (var i = 0; i < r_Data.Length; i++)
            {
                boardCopy[i] = new eCoinType[r_Data.Length];
                Array.Copy(r_Data[i], boardCopy[i], r_Data.Length);
            }

            return boardCopy;
        }

        private class BoardEnumerator : IEnumerator
        {
            private readonly eCoinType[][] r_Data;
            private Point m_currentPosistion = new Point(-1, 0);

            public BoardEnumerator(eCoinType[][] i_Data)
            {
                r_Data = i_Data;
            }

            public bool MoveNext()
            {
                var v_CanMove = true;
                do
                {
                    if (++m_currentPosistion.X == r_Data.Length)
                    {
                        m_currentPosistion.X = 0;
                        m_currentPosistion.Y++;
                    }

                    if (m_currentPosistion.Y == r_Data.Length)
                    {
                        v_CanMove = false;
                        break;
                    }
                } 
                while (dataAtCurrentPosition() == eCoinType.Empty);

                return v_CanMove;
            }

            private eCoinType dataAtCurrentPosition()
            {
                return r_Data[m_currentPosistion.X][m_currentPosistion.Y];
            }

            public void Reset()
            {
                m_currentPosistion.X = 0;
                m_currentPosistion.Y = 0;
            }

            public object Current
            {
                get
                {
                    return new Coin(
                        dataAtCurrentPosition(),
                        m_currentPosistion.X,
                        m_currentPosistion.Y);
                }
            }
        }
    }
}