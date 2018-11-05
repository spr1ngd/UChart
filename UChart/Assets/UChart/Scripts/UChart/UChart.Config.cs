namespace UChart
{   
    public enum UChartState
    {
        Pixel,
        Scale
    }

    public enum UChartColorTweenMode
    {
        NORMAL,
        FADE,
    }

    public partial class UChart
    {
        public const UChartState uchartState = UChartState.Pixel;
    }
}