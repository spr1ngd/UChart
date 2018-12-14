
using UnityEngine;

namespace UChart
{
    public class Axis : UChartObject
    {
        [Header("AXIS UNIT SETTING")]
        public float xUnit = 1;

        public float yUnit = 1.5f;

        public float zUnit = 2;

        public float axisLenght = 5.0f;

        [Header("AXIS STYLE SETTING")]

        public bool renderArrow = false;

        public Color axisColor = Color.gray;

        public LineType lineType = LineType.Solid;

        public virtual void OnDrawMesh()
        {

        }
    }
}