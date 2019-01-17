
using UnityEngine;

namespace UChart
{
    [System.Serializable]
    public class RoundedCylinderGeometry : CylinderGeometry
    {
        [Header("Rounded Setting")]
        public int smoothDegree = 5;
        public float smoothRadius = 0.1f; // percent of cylinder's radius

        public bool topRounded = true;
        public bool bottomRounded = true;

        public override void FillGeometry()
        {
            float realTopSmoothRadius = smoothRadius * topRadius;
            float realBottomSmoothRadius = smoothRadius * bottomRadius;

            int smoothnessCount = (int)(percent * smoothness);
            int iterationCount = smoothnessCount;
            if(percent < 1)
                iterationCount--;

            geometryBuffer.AddVertex(Vector3.zero,color);
            geometryBuffer.AddVertex(Vector3.zero + new Vector3(0,height,0),color);

            geometryBuffer.AddCircle(Vector3.zero,bottomRadius * (1- smoothRadius),smoothness,Vector3.down,percent);
            geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,height,0),topRadius * (1- smoothRadius),smoothness,Vector3.up,percent);

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
                if(end >= smoothnessCount * 2 + 2)
                    end = end - smoothnessCount;
                geometryBuffer.AddTriangle(new int[] { end,1,start });
            }

            // 新的圆周侧壁
            geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,smoothRadius * bottomRadius,0) ,bottomRadius,smoothness,Vector3.down,percent);
            geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,height - smoothRadius * topRadius,0),topRadius,smoothness,Vector3.up,percent);

            for(int i = 2 + smoothnessCount * 2, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= smoothnessCount * 3 + 2)
                    end = end - smoothnessCount;
				geometryBuffer.AddTriangle (new int[]{start,end,start+smoothnessCount});
            }

            for(int i = 2 + smoothnessCount * 3, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= smoothnessCount * 4 + 2)
                    end = end - smoothnessCount;
				geometryBuffer.AddTriangle (new int[]{start,end - smoothnessCount ,end});
            }

			// TODO: bottom radius triangles
			for( int i = 0 ;i < smoothDegree ;i++ )
			{
				for (int y = 0; y < iterationCount; y++) 
				{
					// add vertices
					// triangles
				}
			}

            // TODO: top radius triangles
            for( int i = 0 ; i < smoothDegree ;i++ )
            {
				for (int y = 0; y < iterationCount; y++) 
				{
					// add vertices
					// triangles
				}
            }

            // TODO: 截面弥补
            //if(percent < 1)
            //{
            //    geometryBuffer.AddTriangle(new int[] { 1,0,2 });
            //    geometryBuffer.AddTriangle(new int[] { 2,2 + smoothnessCount,1 });

            //    geometryBuffer.AddTriangle(new int[] { 1,2 + smoothnessCount + smoothnessCount - 1,2 + smoothnessCount - 1 });
            //    geometryBuffer.AddTriangle(new int[] { 2 + smoothnessCount - 1,0,1 });
            //}
        }
    }
}