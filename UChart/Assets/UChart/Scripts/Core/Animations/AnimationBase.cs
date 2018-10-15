
namespace UChart
{
    public class AnimationBase : UnityEngine.MonoBehaviour
    {
        protected DG.Tweening.Tweener animationTweener = null;
        protected UnityEngine.Transform myTransform = null;

        protected virtual void Awake()
        {
            myTransform = this.transform;
        }
    }
}