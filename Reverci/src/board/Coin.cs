namespace Reverci.board
{
    public class Coin
    {
        private eCoinType m_CoinType;
        private int m_X;
        private int m_Y;

        public Coin(eCoinType i_CoinType, int i_X, int i_Y)
        {
            CoinType = i_CoinType;
            X = i_X;
            Y = i_Y;
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public eCoinType CoinType
        {
            get { return m_CoinType; }
            set { m_CoinType = value; }
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }

            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            if(obj.GetType() != typeof(Coin))
            {
                return false;
            }

            return Equals((Coin)obj);
        }

        public bool Equals(Coin obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }

            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(obj.m_CoinType, m_CoinType) && obj.m_X == m_X && obj.m_Y == m_Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = m_CoinType.GetHashCode();
                result = (result * 397) ^ m_X;
                result = (result * 397) ^ m_Y;
                return result;
            }
        }
    }
}