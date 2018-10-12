
using UnityEngine;
using UnityEngine.UI;

namespace UChart
{
    public class Point2D : Point
    {
        private RectTransform m_rect = null;
        private Image m_image = null;

        public override void Init()
        {
            base.Init();
            m_rect = myTransform.gameObject.AddComponent<RectTransform>();
            m_image = myTransform.GetComponent<Image>();

            SetSize(1);
            SetColor(Color.black);
            this.gameObject.name = uchartId;
        }

        public override void SetColor(Color32 color32)
        {
            m_image.color = color32;
        }
        
        public override void SetSize(int size)
        {
            m_rect.sizeDelta = new Vector2(size,size);
        }
    }
}