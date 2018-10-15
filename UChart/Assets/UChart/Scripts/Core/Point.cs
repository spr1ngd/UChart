
using UnityEngine;

namespace UChart
{
    public class Point : UChartObject
    {
        // todo 将颜色、尺寸、贴图、shader、posteffect等改为属性设置
        private Color32 m_color32;
        public Color32 color32
        {
            get { return m_color32; }
            set
            {
                m_color32 = value;
                SetColor(value);
            }
        }

        private Color m_color;
        public Color color
        {
            get { return m_color; }
            set
            {
                m_color = value;
                SetColor(value);
            }
        }

        private float m_alpha = 1;
        public float alpha
        {
            get { return m_alpha; }
            set
            {
                m_alpha = value;
                SetAlpha(value);
            }
        }

        private float m_size = 1;
        public float size
        {
            get { return m_size; }
            set
            {
                m_size = value;
                SetSize(value);
            }
        }

        public override void Init()
        {
            base.Init();
        }

        public override void GenerateId()
        {
            uchartId = string.Format("point_{0}", NewGuid());
        }

        public virtual Point AddAnimation<T>() where T : AnimationBase, IAnimationEvent
        {
            myGameobject.AddComponent<T>();
            return this;
        }

        protected virtual void SetColor(Color color)
        {

        }

        protected virtual void SetColor(Color32 color32)
        {

        }

        protected virtual void SetAlpha(float alpha)
        {

        }

        protected virtual void SetSize(float size)
        {

        }

        protected virtual void SetTexture(Texture2D texture)
        {

        }
    }
}