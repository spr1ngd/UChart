
namespace UChart
{
    public interface IAnimationEvent
    {
        void OnEnterAnimation();

        void OnExitAnimation();

        void OnClickAnimation();

        void OnStayAnimation();

        void OnDoubleClickAnimaiton();

        void OnCreateAnimation();

        void OnDestroyAnimation();
    }
}