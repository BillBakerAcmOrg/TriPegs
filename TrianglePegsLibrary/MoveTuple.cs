using System;
using System.Collections.Generic;
using System.Text;

namespace trianglePegs
{
    [Serializable]
    public class MoveTuple: ICloneable
    {
        int m_original;
        int m_jumped;
        int m_destination;

        public MoveTuple(int orig, int jmp, int dest)
        {
            m_original = orig;
            m_jumped = jmp;
            m_destination = dest;
        }

        public MoveTuple()
        {
            m_original = -1;
            m_jumped = -1;
            m_destination = -1;
        }
        public override string ToString()
        {
            return string.Format("({0},{1},{2})", m_original, m_jumped, m_destination);
        }

        #region public properties
        public int original
        {
            get
            {
                return m_original;
            }
        }

        public int jumped
        {
            get
            {
                return m_jumped;
            }
        }

        public int destination
        {
            get
            {
                return m_destination;
            }
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    };
}
