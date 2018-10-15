
namespace UChart
{
    public interface IMouseEvent
    {
        void OnEnter();

        void OnStay();

        void OnExit();

        void OnClick();

        void OnDoubleClick();
    }
}