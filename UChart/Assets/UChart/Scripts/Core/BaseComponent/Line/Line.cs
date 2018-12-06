
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
                SetStart();
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
                SetEnd();
            }
        }

        protected virtual void SetStart()
        {

        }

        protected virtual void SetEnd()
        {

        }

        public virtual void Draw()
        {

        }
    }
}