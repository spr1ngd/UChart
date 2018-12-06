
using UnityEngine;
using UnityEngine.EventSystems;

namespace UChart
{
    public class UChartObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        #region base components
        [HideInInspector]
        public string uchartId = string.Empty;
        protected IAnimationEvent animationEvent { get { return (IAnimationEvent)this.GetComponent<AnimationBase>(); } }
        protected Transform myTransform = null;
        protected GameObject myGameobject = null;

        #endregion

        #region base properties

        protected Color m_color;

        public Color color
        {
            get{return m_color;}
            set
            {
                SetColor(value);
                m_color = value;
            }
        }

        protected float m_alpha;
        public float alpha
        {
            get{return m_alpha;}
            set
            {
                SetAlpha(value);
                m_alpha = value;
            }
        }

        #endregion

        #region uchart interactive 

        public bool interactive = true;

        #endregion
        
        #region uchart base method

        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            this.myTransform = this.transform;
            this.myGameobject = myTransform.gameObject;
            GenerateId();
            this.gameObject.name = uchartId;
        }

        public virtual void GenerateId()
        {
            uchartId = string.Format("uchart_{0}",NewGuid());
        }

        public string NewGuid()
        {
            return System.Guid.NewGuid().ToString();
        }

        #endregion

        #region uchart properties methods

        protected virtual void SetColor(Color color){}

        protected virtual void SetAlpha(float alpha){}

        #endregion

        #region Mouse Events

        public virtual UChartObject AddAnimation<T>() where T : AnimationBase, IAnimationEvent
        {
            myGameobject.AddComponent<T>();
            return this;
        }

        protected virtual void OnMouseEnter()
        {
            if( !interactive )
                return;
            if( null != animationEvent )
                animationEvent.OnEnterAnimation();
            //Debug.Log(string.Format("<color=green>{0}->{1}</color>","Enter",uchartId));
        }

        protected virtual void OnMouseExit()
        {
            if( !interactive )
                return;
            if (null != animationEvent)
                animationEvent.OnExitAnimation();
            //Debug.Log(string.Format("<color=green>{0}->{1}</color>","Exit",uchartId));
        }

        protected virtual void OnMouseUpAsButton()
        {
            if( !interactive )
                return;
            if (null != animationEvent)
                animationEvent.OnClickAnimation();
            //Debug.Log(string.Format("<color=green>{0}->{1}</color>","Click",uchartId));
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if( !interactive )
                return;
            if (null != animationEvent)
                animationEvent.OnEnterAnimation();
            //Debug.Log(string.Format("<color=green>{0}->{1}</color>","Enter",uchartId));
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if( !interactive )
                return;
            if (null != animationEvent)
                animationEvent.OnExitAnimation();
            //Debug.Log(string.Format("<color=green>{0}->{1}</color>","Exit",uchartId));
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if( !interactive )
                return;
            if (null != animationEvent)
                animationEvent.OnClickAnimation();
            //Debug.Log(string.Format("<color=green>{0}->{1}</color>","Click",uchartId));
        }

        #endregion
    }
}