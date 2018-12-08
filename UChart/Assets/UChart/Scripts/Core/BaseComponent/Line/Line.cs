
using UnityEngine;

namespace UChart
{
    public class Line : UChartObject
    {
        [SerializeField]
        private Vector3 m_start;

        public Vector3 start
        {
            get { return m_start; }
            set
            {
                m_start = value;
                SetStart(value);
            }
        }

        [SerializeField]
        private Vector3 m_end;

        public Vector3 end
        {
            get { return m_end; }
            set
            {
                m_end = value;
                SetEnd(value);
            }
        }

        private float m_width = 1.0f;

        public float width
        {
            get { return m_width; }
            set
            {
                m_width = value;
                SetWidth(value);
            }
        }

        protected virtual void SetStart( Vector3 start )
        {
            m_start = start;
        }

        protected virtual void SetEnd( Vector3 end )
        {
            m_end = end;
        }

        protected virtual void SetWidth( float width )
        {
            m_width = width;
        }

        public virtual void Draw()
        {

        }
    }
}