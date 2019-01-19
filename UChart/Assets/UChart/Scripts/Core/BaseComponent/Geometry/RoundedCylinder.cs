
using UnityEngine;

namespace UChart
{
    public class RoundedCylinder : UChartObject
    {
        [Header("ROUNDED CYLINDER SETTING")]
        public RoundedCylinderGeometry roundedCylinder;


        private void OnDrawGizmos()
        {
            //for(int i = 26; i < roundedCylinder.geometryBuffer.vertices.Length; i++)
            //{
            //    Gizmos.DrawSphere(roundedCylinder.geometryBuffer.vertices[i],0.05f);
            //}
        }
    }
}