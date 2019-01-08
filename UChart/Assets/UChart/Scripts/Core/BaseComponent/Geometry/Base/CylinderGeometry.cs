
using UnityEngine;

namespace UChart
{
    public class CylinderGeometry : Geometry
    {
        [Header("CYLINDER SETTING")]
        public float height = 3;
        public float radius = 0.5f;
        [Range(3,30)] public int smoothness = 12;

        public override void FillGeometry()
        {

        }
    }
}