
namespace UChart.Scatter
{
    public class ScatterGraph : UChartObject
    {
        protected virtual Scatter CreateScatter( UnityEngine.Vector3 position )
        {
            throw new UChartException("Please override method in subclass.");
        }
    }
}