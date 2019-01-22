
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
            //Vector3 bottomCenter = Vector3.zero;
            //Vector3 topCenter = Vector3.zero + new Vector3(0,height,0);

            //float realTopSmoothRadius = smoothRadius * topRadius;
            //float realBottomSmoothRadius = smoothRadius * bottomRadius;

            //int smoothnessCount = (int)(percent * smoothness);
            //int iterationCount = smoothnessCount;
            //if(percent < 1)
            //    iterationCount++;
            //Debug.Log("<color=yellow>ITERATION COUNT: " + iterationCount+"</color>");

            //geometryBuffer.AddVertex(bottomCenter,color);
            //geometryBuffer.AddVertex(topCenter,color);

            //geometryBuffer.AddCircle(bottomCenter,bottomRadius * (1- smoothRadius),smoothness,Vector3.down,percent);
            //geometryBuffer.AddCircle(topCenter,topRadius * (1- smoothRadius),smoothness,Vector3.up,percent);

            //for(int i = 2, count = 0; count < iterationCount; i++, count++)
            //{
            //    int start = i;
            //    int end = i + 1;
            //    if(end >= smoothnessCount + 2)
            //        end = end - smoothnessCount;
            //    geometryBuffer.AddTriangle(new int[] { start,0,end });
            //}

            //// FIX : 断面后这里会出现bug
            //for(int i = 2 + smoothnessCount, count = 0; count < iterationCount; i++, count++)
            //{
            //    int start = i;
            //    int end = i + 1;
            //    if(end >= smoothnessCount * 2 + 2)
            //        end = end - smoothnessCount;
            //    geometryBuffer.AddTriangle(new int[] { end,1,start });
            //}

            // 新的圆周侧壁
    //        geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,smoothRadius * bottomRadius,0) ,bottomRadius,smoothness,Vector3.down,percent);
    //        geometryBuffer.AddCircle(Vector3.zero + new Vector3(0,height - smoothRadius * topRadius,0),topRadius,smoothness,Vector3.up,percent);

    //        for(int i = 2 + smoothnessCount * 2, count = 0; count < iterationCount; i++, count++)
    //        {
    //            int start = i;
    //            int end = i + 1;
    //            if(end >= smoothnessCount * 3 + 2)
    //                end = end - smoothnessCount;
				//geometryBuffer.AddTriangle (new int[]{start,end,start+smoothnessCount});
    //        }

    //        for(int i = 2 + smoothnessCount * 3, count = 0; count < iterationCount; i++, count++)
    //        {
    //            int start = i;
    //            int end = i + 1;
    //            if(end >= smoothnessCount * 4 + 2)
    //                end = end - smoothnessCount;
				//geometryBuffer.AddTriangle (new int[]{start,end - smoothnessCount ,end});
    //        }

            //float topRoundedRadius = topRadius * smoothRadius;
            //float bottomRoundedRadius = bottomRadius * smoothRadius;
            //float perRadian = Mathf.PI * 0.5f / smoothDegree;

            // BOTTOM
            //for(int i = 0; i <= smoothDegree; i++)
            //{
            //    float radius = perRadian * i;
            //    Vector3 center = bottomCenter + new Vector3(0,bottomRoundedRadius - bottomRoundedRadius * Mathf.Cos(radius),0);
            //    float curRadius = bottomRadius * ( 1 - smoothRadius ) + Mathf.Sin(radius) * bottomRadius * smoothRadius;
            //    geometryBuffer.AddCircle(center,curRadius,smoothnessCount,Vector3.up,percent);
            //}
            //for(int i = 0; i < smoothDegree ; i++)
            //{
            //    int curIndex = 4 + i;
            //    int nextIndex = curIndex + 1;
            //    for(int y = 2 + smoothnessCount * curIndex, count = 0; count < iterationCount; y++, count++)
            //    {
            //        int first = y;
            //        int second = y + smoothnessCount;
            //        int third = second + 1;
            //        if(third >= 2 + smoothnessCount * (nextIndex + 1))
            //            third -= smoothnessCount;
            //        int fourth = first + 1;
            //        if(fourth >= 2 + smoothnessCount * (curIndex + 1))
            //            fourth -= smoothnessCount;
            //        geometryBuffer.AddQuad(new int[] { fourth,third,second,first});
            //    }
            //}

            //// TOP
            //for(int i = smoothDegree; i >= 0; i-- )
            //{
            //    float radius = perRadian * i;
            //    Vector3 center = topCenter - new Vector3(0,topRoundedRadius - topRoundedRadius * Mathf.Cos(radius),0);
            //    float curRadius = topRadius * (1 - smoothRadius) + Mathf.Sin(radius) * topRadius * smoothRadius;
            //    geometryBuffer.AddCircle(center,curRadius,smoothnessCount,Vector3.down,percent);
            //}
            //for(int i = 0; i < smoothDegree; i++)
            //{
            //    int curIndex = 4 + smoothDegree + 1 + i;
            //    int nextIndex = curIndex + 1;
            //    for(int y = 2 + smoothnessCount * curIndex, count = 0; count < iterationCount; y++, count++)
            //    {
            //        int first = y;
            //        int second = y + smoothnessCount;
            //        int third = second + 1;
            //        if(third >= 2 + smoothnessCount * (nextIndex + 1))
            //            third -= smoothnessCount;
            //        int fourth = first + 1;
            //        if(fourth >= 2 + smoothnessCount * (curIndex + 1))
            //            fourth -= smoothnessCount;
            //        geometryBuffer.AddQuad(new int[] { fourth,third,second,first });
            //    }
            //}

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