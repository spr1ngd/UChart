
using UnityEngine;

namespace UChart
{
    public class RoundedCylinderGeometry : CylinderGeometry
    {
        [Header("Rounded Setting")]
        public int smoothDegree = 2;
        public float smoothRadius = 0.1f; // percent of cylinder's radius

        public override void FillGeometry()
        {
            // TODO: 1.绘制顶部和底部的圆形
            geometryBuffer.AddCircle(Vector3.zero,radius,smoothness,-Vector3.up);
            geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,height,0),radius,smoothness,Vector3.up);
            
            // TODO: 2.绘制侧面的矩形

            // TODO: 3.绘制圆面连接处
        }
    }
}