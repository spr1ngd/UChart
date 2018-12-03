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

        /// <summary>
        /// layer of spesical uchart object
        /// </summary>
        public const int uchartLayer = 31;
    }
}