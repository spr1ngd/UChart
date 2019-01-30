
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
            int turns = 0;

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
            float bottomRealRadius = bottomRadius;
            if(rounded && bottomRounded)
                bottomRealRadius -= roundedWidth;
            this.AddTurn(bottom,vertexCount,perRadian,bottomRealRadius);

            // bottom triangles
            for(int i = 2, count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= 2 + vertexCount)
                    end -= vertexCount;
                geometryBuffer.AddTriangle(new int[] { end,0,start });
            }
            turns++;

            // bottom rounded triangles
            if(rounded && bottomRounded)
            {
                float perRad = Mathf.PI /2.0f / tessellation;
                for(int i = 1; i <= tessellation; i++)
                {
                    Vector3 center = bottom + new Vector3(0,(1 - Mathf.Cos(perRad * i)) *roundedWidth,0);
                    float radius = bottomRealRadius + Mathf.Sin(perRad * i) * roundedWidth;
                    AddTurn(center,vertexCount,perRadian,radius);
                    for(int x = 2 + vertexCount * (turns - 1) , count = 0; count < iterationCount; x++,count++)
                    {
                        int bl = x;
                        int br = x + 1;
                        if(percent == 360 && br >= 2 + vertexCount * turns)
                            br -= vertexCount;
                        int tl = bl + vertexCount;
                        int tr = br + vertexCount;
                        if(percent == 360 && tr >= 2 + vertexCount * (turns + 1))
                            tr -= vertexCount;
                        geometryBuffer.AddQuad(new int[] { bl,tl,tr,br});
                    }
                    turns++;
                }
            }
            
            // top rounded triangles
            float topRealRadius = topRadius;
            if(rounded && topRounded)
                topRealRadius -= roundedWidth;
            turns++;
            if(rounded && topRounded)
            {
                float perRad = Mathf.PI / 2.0f / tessellation;                
                for(int i = tessellation; i > 0; i--)
                {
                    Vector3 center = top - new Vector3(0,(1-Mathf.Cos(perRad*i)) * roundedWidth,0);
                    float radius = topRealRadius + Mathf.Sin(perRad * i) * roundedWidth;
                    AddTurn(center,vertexCount,perRadian,radius);

                    for(int x = 2 + vertexCount * (turns - 1), count = 0; count < iterationCount; x++, count++)
                    {
                        int bl = x;
                        int br = x + 1;
                        if(percent == 360 && br >= 2 + vertexCount * turns)
                            br -= vertexCount;
                        int tl = bl + vertexCount;
                        int tr = br + vertexCount;
                        if(percent == 360 && tr >= 2 + vertexCount * (turns + 1))
                            tr -= vertexCount;
                        geometryBuffer.AddQuad(new int[] { bl,tl,tr,br });
                    }
                    turns++;
                }
            }
            this.AddTurn(top,vertexCount,perRadian,topRealRadius);

            // side triangles
            // TODO: 计算上下两层turn序号
            int sideTurn = 1;
            if(rounded)
            {
                if(bottomRounded)
                    sideTurn += tessellation;
            }
            for(int x = 2 + vertexCount * (sideTurn - 1), count = 0; count < iterationCount; x++, count++)
            {
                int bl = x;
                int br = x + 1;
                if(percent == 360 && br >= 2 + vertexCount * sideTurn)
                    br -= vertexCount;
                int tl = bl + vertexCount;
                int tr = br + vertexCount;
                if(percent == 360 && tr >= 2 + vertexCount * (sideTurn + 1))
                    tr -= vertexCount;
                geometryBuffer.AddQuad(new int[] { bl,tl,tr,br });
            }

            // top triangles
            for(int i = 2 + vertexCount * (turns-1), count = 0; count < iterationCount; i++, count++)
            {
                int start = i;
                int end = i + 1;
                if(end >= 2 + vertexCount * turns)
                    end -= vertexCount;
                geometryBuffer.AddTriangle(new int[] { start,1,end });
            }
            turns++;

            // section triangles
            if(percent < 360)
            {
                List<int> start = new List<int>();
                start.Add(0);
                int sectionVerticesCount = 4;
                if(rounded)
                {
                    if(bottomRounded)
                        sectionVerticesCount += tessellation;
                    if(topRounded)
                        sectionVerticesCount += tessellation;
                }
                for(int i = 0; i < sectionVerticesCount - 2; i++)
                    start.Add(2 + i * vertexCount);
                start.Add(1);
                for(int i = 1; i < start.Count - 1; i++)
                {
                    //geometryBuffer.AddTriangle(new int[] { 0,start[i + 1],start[i] });
                    geometryBuffer.AddTriangle(new VertexBuffer[] 
                    {
                        new VertexBuffer(){ pos = geometryBuffer.vertices[0]},
                        new VertexBuffer(){ pos = geometryBuffer.vertices[start[i + 1]]},
                        new VertexBuffer(){ pos = geometryBuffer.vertices[start[i]]},
                    });
                }


                List<int> end = new List<int>();
                end.Add(0);
                for(int i = 0; i < sectionVerticesCount - 2; i++)
                    end.Add(2 + i * vertexCount + iterationCount);
                end.Add(1);
                for(int i = 1; i < end.Count - 1; i++)
                {
                    //geometryBuffer.AddTriangle(new int[] { 0,end[i],end[i + 1] });
                    geometryBuffer.AddTriangle(new VertexBuffer[]
                   {
                        new VertexBuffer(){ pos = geometryBuffer.vertices[0]},
                        new VertexBuffer(){ pos = geometryBuffer.vertices[end[i]]},
                        new VertexBuffer(){ pos = geometryBuffer.vertices[end[i + 1]]},
                   });
                }
            }
        }

        private void AddTurn( Vector3 center,int vertexCount,float perRadian,float radius )
        {
            for(int i = 0; i < vertexCount; i++)
            {
                float radian = i * perRadian;
                geometryBuffer.AddVertex(center + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian)) * radius,color);
            }
        }
    }
}