
namespace UChart
{
    public class UChartException : System.Exception
    {
        public UChartException():base()
        {

        }

        public UChartException(string message):base(message)
        {
            
        }

        public UChartException(string message,System.Exception exception):base(message,exception)
        {

        }
    }
}