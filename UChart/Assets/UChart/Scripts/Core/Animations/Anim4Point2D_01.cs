
using DG.Tweening;
using UnityEngine;

namespace UChart
{
    public class Anim4Point2D_01 : AnimationBase, IAnimationEvent
    {
        private Vector3 m_animationOriginScale;

        public void OnEnterAnimation()
        {
            animationTweener.Complete();
            animationTweener.Kill();
            m_animationOriginScale = myTransform.localScale;
            animationTweener = this.myTransform.DOScale(m_animationOriginScale * 1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnExitAnimation()
        {
            animationTweener.Complete();
            animationTweener.Kill();
            animationTweener = this.myTransform.DOScale(m_animationOriginScale, 0.5f);
        }

        public void OnClickAnimation()
        {
        }

        public void OnStayAnimation()
        {
        }

        public void OnDoubleClickAnimaiton()
        {
        }

        public void OnCreateAnimation()
        {
        }

        public void OnDestroyAnimation()
        {
        }
    }
}