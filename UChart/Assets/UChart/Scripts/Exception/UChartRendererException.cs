namespace UChart
{
    public class UChartRendererException : UChartException
    {
        public UChartRendererException() : base()
        {

        }

        public UChartRendererException(string message) : base(message)
        {

        }

        public UChartRendererException( string message,System.Exception exception ) : base(message,exception)
        {

        }
    }
}