
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UChart
{
    public class UChartObject : MonoBehaviour , IMouseEvent , IAnimationEvent , IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        protected Transform myTransform = null;
        protected string uchartId = string.Empty;
        protected Tweener animationTweener = null;

        public virtual void Init()
        {
            this.myTransform = this.transform;
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

        #region Mouse Event Animaiton

        public virtual void OnEnterAnimation()
        {
        }

        public virtual void OnExitAnimation()
        {
        }

        public virtual void OnClickAnimation()
        {
        }

        public virtual void OnStayAnimation()
        {
        }

        public virtual void OnDoubleClickAnimaiton()
        {
        }

        public virtual void OnCreateAnimation()
        {
        }

        public virtual void OnDestroyAnimation()
        {
        }

        #endregion

        #region Mouse Events

        private void OnMouseEnter()
        {
            OnEnter();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Enter",uchartId));
        }

        private void OnMouseExit()
        {
            OnExit();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Exit",uchartId));
        }

        private void OnMouseUpAsButton()
        {
            OnClick();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Click",uchartId));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Enter",uchartId));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Exit",uchartId));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
            Debug.Log(string.Format("<color=green>{0}->{1}</color>","Click",uchartId));
        }

        public virtual void OnEnter()
        {
            OnEnterAnimation();
        }

        public virtual void OnStay()
        {
            OnStayAnimation();
        }

        public virtual void OnExit()
        {
            OnExitAnimation();
        }

        public virtual void OnClick()
        {
            OnClickAnimation();
        }

        public virtual void OnDoubleClick()
        {
            OnDoubleClickAnimaiton();
        }

        #endregion
    }
}