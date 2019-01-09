
using UnityEngine;

namespace UChart
{
    [System.Serializable]
    public class CylinderGeometry : Geometry
    {
        [Header("CYLINDER SETTING")]
        public float height = 3;
        public float topRadius = 1f;
        public float bottomRadius = 1f;

        [Range(0,1)] public float percent = 0.75f;
        [Range(3,100)] public int smoothness = 100;

        [Header("CYLINDER STYLE SETTIGN")]
        public Color color = Color.white;
        public bool outline = false;

        public override void FillGeometry()
        {
            int smoothnessCount = (int)(percent * smoothness);
            int iterationCount = smoothnessCount;
            if(percent < 1)
                iterationCount--;

            geometryBuffer.AddVertex(Vector3.zero,color);
            geometryBuffer.AddVertex(Vector3.zero + new Vector3(0,height,0),color);

            geometryBuffer.AddCircle(Vector3.zero,bottomRadius,smoothness,Vector3.down,percent);
            geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,height,0),topRadius,smoothness,Vector3.up,percent);

            for(int i = 2, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= smoothnessCount + 2)
                    end = end - smoothnessCount;
                geometryBuffer.AddTriangle(new int[] { start,0,end });
            }

            for(int i = 2 + smoothnessCount, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= smoothnessCount + smoothnessCount + 2)
                    end = end - smoothnessCount;
                geometryBuffer.AddTriangle(new int[] { end,1,start });
            }

            for(int i = 0, index = 2; i < iterationCount; i++, index++)
            {
                var topStart = index;
                var topEnd = index + 1;
                if(topEnd >= smoothnessCount + 2)
                    topEnd = topEnd - smoothnessCount;
                var bottomStart = topStart + smoothnessCount;
                var bottomEnd = topEnd + smoothnessCount;

                geometryBuffer.AddTriangle(new int[] { topStart,topEnd,bottomEnd });
                geometryBuffer.AddTriangle(new int[] { bottomEnd,bottomStart,topStart });
            }

            if(percent < 1)
            {
                geometryBuffer.AddTriangle(new int[] { 1,0,2 });
                geometryBuffer.AddTriangle(new int[] { 2,2 + smoothnessCount,1 });

                geometryBuffer.AddTriangle(new int[] { 1,2 + smoothnessCount + smoothnessCount -1 ,2 + smoothnessCount -1 });
                geometryBuffer.AddTriangle(new int[] { 2 + smoothnessCount - 1,0,1 });
            }
        }
    }
}