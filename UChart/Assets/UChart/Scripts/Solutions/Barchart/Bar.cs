
using UnityEngine;
using UnityEngine.EventSystems;

namespace UChart.Barchart
{
    public class Bar : UChartObject
    {
        [Range(0.5f,5)]
        public float barWidth = 1.0f;
    }
}