namespace UChart
{
    public class UChartNotFoundException : UChartException
    {
        public UChartNotFoundException():base()
        {

        }

        public UChartNotFoundException( string message ):base(message)
        {

        }

        public UChartNotFoundException( string message,System.Exception exception ) : base(message,exception)
        {

        }
    }
}