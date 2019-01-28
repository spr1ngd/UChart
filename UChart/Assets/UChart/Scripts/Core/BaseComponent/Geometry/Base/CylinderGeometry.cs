
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    [System.Serializable]
    public class CylinderGeometry : Geometry
    {
        [Header("CYLINDER SETTING")]
        public float height = 3;
        public float topRadius = 1f;
        public float bottomRadius = 1f;


        [Range(1,360)] public int percent = 360;

        [Header("CYLINDER STYLE SETTIGN")]
        public Color color = Color.white;
        public bool outline = false;

        public bool rounded = true;
        public bool topRounded = true;
        public bool bottomRounded = true;

        [Range(0,0.5f)] public float roundedWidth = 0.3f;
        [Range(1,8)] public int tessellation = 3; 

        public override void FillGeometry()
        {
            List<VertexBuffer> sectionVertices = new List<VertexBuffer>();

            Vector3 bottom = Vector3.zero;
            Vector3 top = bottom + new Vector3(0,height,0);

            int vertexCount = percent + 1;
            if(percent >= 360)
                vertexCount = 360;
            int iterationCount = percent;
            float perRadian = Mathf.PI * 2 / 360.0f;
            Debug.Log("<color=yellow>ITERATION COUNT: " + iterationCount + "</color>");

            geometryBuffer.AddVertex(bottom,color);
            geometryBuffer.AddVertex(top,color);

            // bottom vertices
            float bottomRealRadius = bottomRadius - roundedWidth;
            for(int i = 0; i < vertexCount; i++)
            {
                float radian = i * perRadian;
                geometryBuffer.AddVertex(bottom + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian))*bottomRealRadius,color);
            }

            // bottom triangles
            for(int i = 2, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= 2 + vertexCount)
                    end -= vertexCount;
                geometryBuffer.AddTriangle(new int[] { end,0,start });
            }

            // top vertices
            float topRealRadius = topRadius - roundedWidth;
            for(int i = 0; i < vertexCount; i++)
            {
                float radian = i * perRadian;
                geometryBuffer.AddVertex(top + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian)) * topRealRadius,color);
            }

            // top triangles
            for(int i = 2 + vertexCount, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= 2 + vertexCount * 2)
                    end -= vertexCount;
                geometryBuffer.AddTriangle(new int[] { start,1,end });
            }

            // bottom rounded triangles
            if( rounded && bottomRounded )
            {
                for( int i = 0 ; i < tessellation;i++ )
                {

                }
            }

            // side triangles
            for(int i = 0; i < iterationCount; i++)
            {
                int bl = i + 2;
                int br = bl + 1;
                if(percent == 360 && br >= 2 + vertexCount)
                    br -= vertexCount;
                int tl = bl + vertexCount;
                int tr = br + vertexCount;
                if(percent == 360 && tr >= 2 + vertexCount * 2)
                    tr -= vertexCount;
                geometryBuffer.AddQuad(new int[] { bl,tl,tr,br });
            }

            // top rounded triangles
            if( rounded && topRounded)
            {
                for( int i = 0 ; i < tessellation;i++ )
                {
                    
                }
            }

            // section triangles
            if(percent < 360)
            {
                geometryBuffer.AddTriangle(new int[] { 0,1,2 + vertexCount });
                geometryBuffer.AddTriangle(new int[] { 2 + vertexCount,2,0 });

                geometryBuffer.AddTriangle(new int[] { 2 + vertexCount * 2 - 1,1,0 });
                geometryBuffer.AddTriangle(new int[] { 0,vertexCount + 2 - 1,2 + vertexCount * 2 - 1 });
            }
        }
    }
}