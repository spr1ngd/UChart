
using UnityEngine;

namespace UChart
{
    public class Point : UChartObject
    {
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

        protected virtual void SetSize(float size)
        {

        }

        protected virtual void SetTexture(Texture2D texture)
        {

        }
    }
}