using UnityEngine;
using UnityEngine.UI;

namespace UChart
{
    public class Pie2D : Pie
    {
        private Image m_pie = null;

        public Image pie 
        {
            get
            {
                if( null == m_pie )
                     m_pie = this.GetComponent<Image>();
                return m_pie;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}