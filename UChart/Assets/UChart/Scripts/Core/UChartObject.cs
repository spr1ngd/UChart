
using UnityEngine;
using UnityEngine.EventSystems;

namespace UChart
{
    public class UChartObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        protected IAnimationEvent animationEvent { get { return (IAnimationEvent)this.GetComponent<AnimationBase>(); } }
        protected string uchartId = string.Empty;
        protected Transform myTransform = null;
        protected GameObject myGameobject = null;
        
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

        #region Mouse Events

        private void OnMouseEnter()
        {
            if( null != animationEvent )
                animationEvent.OnEnterAnimation();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Enter",uchartId));
        }

        private void OnMouseExit()
        {
            if (null != animationEvent)
                animationEvent.OnExitAnimation();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Exit",uchartId));
        }

        private void OnMouseUpAsButton()
        {
            if (null != animationEvent)
                animationEvent.OnClickAnimation();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Click",uchartId));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (null != animationEvent)
                animationEvent.OnEnterAnimation();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Enter",uchartId));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (null != animationEvent)
                animationEvent.OnExitAnimation();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Exit",uchartId));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (null != animationEvent)
                animationEvent.OnClickAnimation();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Click",uchartId));
        }

        #endregion
    }
}