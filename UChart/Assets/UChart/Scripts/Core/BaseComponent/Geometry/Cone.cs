
using UnityEngine;

namespace UChart
{
    public class Cone : Geometry
    {
        [Header("STYLE SETTING")]
        public Vector3 bottom;

        public Color color;

        public float height;

        public float radius;

        public int smoothness;

        public override void FillGeometry()
        {
            // TODO: 添加Bottom
            var bottomVertex = new VertexBuffer();
            bottomVertex.pos = bottom;
            bottomVertex.color = color;
            geometryBuffer.AddVertex(bottomVertex);
            // TODO: 添加Top
            var topVertex = new VertexBuffer();
            topVertex.pos = bottom + Vector3.Normalize(myTransform.right) * height;
            topVertex.color = color;
            geometryBuffer.AddVertex(bottomVertex);
            
            // TODO: 添加顶点
            float perAngle = 2 * Mathf.PI / smoothness; 
            for( int i = 0 ; i < smoothness;i++ )
            {
                float angle = i * perAngle;
                var pos = bottom + new Vector3(0,Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);  
                geometryBuffer.AddVertex(pos,color);
            }

            // TODO: 添加bottom triangles
            for( int i = 2 ;  i < smoothness + 2;i++ )
            {
                int first = 0;
                int second = i + 1;
                if( i > smoothness + 1 )
                    second = second - smoothness;
                int third = i;
                geometryBuffer.AddTriangle(new int[]{first,second,third});
            }
            // TODO: 添加top triangles
            for( int i = 2 ;  i < smoothness + 2;i++ )
            {
                int first = 1;
                int second = i + 1;
                if( i > smoothness + 1 )
                    second = second - smoothness;
                int third = i;
                geometryBuffer.AddTriangle(new int[]{first,second,third});
            }
        }
    }
}