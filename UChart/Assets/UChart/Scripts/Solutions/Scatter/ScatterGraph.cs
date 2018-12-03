
namespace UChart.Scatter
{
    public class ScatterGraph : UChartObject
    {
        protected virtual Scatter CreateScatter(int id,UnityEngine.Vector3 position,float size)
        {
            throw new UChartException("Please override method in subclass.");
        }

        public virtual void RefreshScatter()
        {
            throw new UChartException("Please override method in subclass.");
        }
    }
}