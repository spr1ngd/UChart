
using UnityEngine;

namespace UChart
{
    public class Axis : UChartObject
    {
        [Header("AXIS UNIT SETTING")]
        public int xUnit = 1;
        public int yUnit = 3;
        public int zUnit = 2;
        public float axisLenght = 5.0f;

        [Header("AXIS STYLE SETTING")]       

        public Color axisColor = Color.gray;
        public Color meshColor = Color.magenta;
        public LineType lineType = LineType.Solid;

        [Header("ARROW STYLE SETTING")]

        public bool renderArrow = false;
        public float arrowSize = 1.5f;

        [Range(3,15)] public int arrowSmooth = 3;
        public Color xArrowColor = Color.red;
        public Color yArrowColor = Color.green;
        public Color zArrowColor = Color.blue;

        public virtual void OnDrawMesh()
        {
            OnDrawArrow();
        }

        public virtual void OnDrawArrow()
        {
            
        }
    }
}