
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


        [Range(1,360)] public int percent = 360;

        [Header("CYLINDER STYLE SETTIGN")]
        public Color color = Color.white;
        public bool outline = false;

        public override void FillGeometry()
        {
            // TODO: 提升饼图的精度 可手动设置饼图平均每最小份的弧度(最小弧度由饼块的数量决定)
            // TODO: 手动计算每块饼图的最后一个细分Mesh的顶点信息,确保多块饼图可完美拼合
            // TODO: 后续考虑是否点数量越界，自动切分饼图mesh
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

            // bottom
            for(int i = 0; i < vertexCount; i++)
            {
                float radian = i * perRadian;
                geometryBuffer.AddVertex(bottom + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian)),color);
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

            // top 
            for(int i = 0; i < vertexCount; i++)
            {
                float radian = i * perRadian;
                geometryBuffer.AddVertex(top + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian)),color);
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

            // section triangles
            if(percent < 360)
            {
                geometryBuffer.AddTriangle(new int[] { 0,1,2 + vertexCount });
                geometryBuffer.AddTriangle(new int[] { 2 + vertexCount,2,0 });

                geometryBuffer.AddTriangle(new int[] { 2 + vertexCount * 2 - 1,1,0 });
                geometryBuffer.AddTriangle(new int[] { 0,vertexCount + 2 - 1,2 + vertexCount * 2 - 1 });
            }

            //// bottom
            //for(int i = 0; i < vertexCount; i++)
            //{
            //    float radian = i * perRadian;
            //    geometryBuffer.AddVertex(bottom + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian)),color);
            //}

            //// bottom triangles
            //for(int i = 2, count = 0; count < iterationCount; i++, count++)
            //{
            //    int start = i;
            //    int end = i + 1;
            //    geometryBuffer.AddTriangle(new int[] { end,0,start });
            //}

            //// top 
            //for(int i = 0; i < vertexCount; i++)
            //{
            //    float radian = i * perRadian;
            //    geometryBuffer.AddVertex(top + new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian)),color);
            //}

            //// top triangles
            //for(int i = 2 + vertexCount, count = 0; count < iterationCount; i++, count++)
            //{
            //    int start = i;
            //    int end = i + 1;
            //    geometryBuffer.AddTriangle(new int[] { start,1,end});
            //}

            //// side triangles
            //for(int i = 0; i < iterationCount; i++)
            //{
            //    int bl = i + 2;
            //    int br = bl + 1;
            //    int tl = bl + vertexCount;
            //    int tr = br + vertexCount;
            //    geometryBuffer.AddQuad(new int[] { bl,tl,tr,br });
            //}

            //// section triangles
            //if(percent < 360)
            //{
            //    geometryBuffer.AddTriangle(new int[] { 0,1,2 + vertexCount });
            //    geometryBuffer.AddTriangle(new int[] { 2 + vertexCount,2,0 });

            //    geometryBuffer.AddTriangle(new int[] { 2 + vertexCount * 2 - 1,1,0 });
            //    geometryBuffer.AddTriangle(new int[] { 0,vertexCount + 2 - 1,2 + vertexCount * 2 - 1 });
            //}
        }

        private void AddCircle( int iterationCount )
        {
            
        }
    }
}