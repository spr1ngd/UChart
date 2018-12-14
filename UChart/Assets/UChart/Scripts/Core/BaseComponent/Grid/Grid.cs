
using UnityEngine;

namespace UChart
{
    public enum GridType
    {
        Basic,
        AutoAlpha
    }

    public class Grid : UChartObject
    {
        [Header("GRID SETTING")]
        public GridType gridType = GridType.AutoAlpha;

        public float gridSize = 10;

        public int division = 10;

        public virtual void Draw()
        {
            
        }
    }
}