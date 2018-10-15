
using UnityEngine;
using UnityEngine.UI;

namespace UChart
{
    public class Point2D : Point 
    {
        private RectTransform m_rect = null;
        private Image m_image = null;

        private Vector3 m_originScale = Vector3.one;
        private Vector3 m_parentScale = Vector3.one;
        private float m_size = -1;

        public override void Init()
        {
            base.Init();
            m_image = myTransform.gameObject.AddComponent<Image>();
            myTransform = this.gameObject.transform;
            m_rect = this.gameObject.GetComponent<RectTransform>();

            SetSize(1);
            SetColor(Color.black);
        }

        protected override void SetColor(Color color)
        {
            m_image.color = color;
        }

        protected override void SetColor(Color32 color32)
        {
            m_image.color = color32;
        }

        protected override void SetAlpha(float alpha)
        {
            Color color = m_image.color;
            m_image.color = new Color(color.r,color.g,color.b,alpha);
        }

        protected override void SetSize(float size)
        {
            m_size = size;
            m_rect.sizeDelta = new Vector2(m_size,m_size);
        }

        protected override void SetTexture(Texture2D texture)
        {
            Sprite sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
            m_image.overrideSprite = sprite;
        }

        private void LateUpdate()
        {
            Vector3 scale = Vector3.one;
            var parent = myTransform.parent;
            if(null != parent)
                GetParentsScale(parent,ref scale);
            if (!m_parentScale.Equals(scale))
            {
                myTransform.localScale = new Vector3(m_originScale.x / scale.x,m_originScale.y / scale.y,m_originScale.z / scale.z);
                m_parentScale = scale;
            }
        }

        private void GetParentsScale(Transform parent,ref Vector3 scale)
        {
            var parentScale = parent.localScale;
            scale = new Vector3(
                scale.x * parentScale.x,
                scale.y * parentScale.y,
                scale.z * parentScale.z);
            if(null != parent.parent)
                GetParentsScale(parent.parent,ref scale);
        }
    }
}