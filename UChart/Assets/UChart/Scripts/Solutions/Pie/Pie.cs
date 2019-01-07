
using UnityEngine;
using UnityEngine.UI;

namespace UChart
{
    public class Pie : UChartObject
    {
        public float[] pieValues;
        public Color[] pieColors;

        [Header("ADVANCED SETTING")]
        public bool antiAliasingEnable = true;

        public virtual void DataCheck()
        {
            if( null == pieValues || null == pieColors || pieValues.Length != pieColors.Length )
                throw new UChartException("count of VALUES not equal to count of COLORS.");
        }

        public virtual void Draw()
        {
            DataCheck();
        }
    }

    public class PieItem : UChartObject
    {
        private float m_value = 0;
        public float value 
        {
            get{return m_value;}
            set
            {
                m_value = value;
                SetValue();
            }
        }
        public int index {get;set;}
        public Material pieMaterial;

        public virtual void DrawPieItem( int index ,float angle ,Material material , Color color )
        {

        }

        protected virtual void SetValue()
        {

        }
    }
}