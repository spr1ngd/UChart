
using UnityEngine;

namespace UChart.Scatter
{
    public class Scatter : UChartObject
    {
        public float size;

        public virtual void Generate(Vector3 size)
        {

        }

        protected override void SetColor(Color color)
        {
            base.SetColor(color);
        }

        protected override void SetAlpha(float alpha)
        {
            base.SetAlpha(alpha);
        }
    }
}