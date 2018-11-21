
using UnityEngine;

namespace UChart.Scatter
{
    public class Scatter : UChartObject
    {
        private float m_size;

        public float size
        {
            get { return m_size; }
            set
            {
                m_size = value;
                SetSize(value);
            }
        }

        private int m_index = 0;

        public int index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        private ScatterGraph m_scatterGraph = null;

        public ScatterGraph scatterGraph
        {
            get { return m_scatterGraph; }
            set { m_scatterGraph = value; }
        }

        public virtual void Generate(Vector3 size)
        {

        }

        protected virtual void SetSize( float size )
        {
            
        }

        protected override void SetColor(Color color)
        {
            base.SetColor(color);
        }

        protected override void SetAlpha(float alpha)
        {
            base.SetAlpha(alpha);
        }
    }
}