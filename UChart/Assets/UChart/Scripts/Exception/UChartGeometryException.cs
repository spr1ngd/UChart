using System;

namespace UChart
{
    public class UChartGeometryException : Exception
    {
        public UChartGeometryException() : base()
        {

        }

        public UChartGeometryException( string message ) : base(message)
        {

        }

        public UChartGeometryException( string message,Exception exception ) : base(message,exception)
        {

        }
    }
}