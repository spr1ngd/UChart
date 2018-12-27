
using UnityEngine;

namespace UChart
{
    [System.Serializable]
    public class ConeGeometry : Geometry
    {
        public Vector3 bottom;

        public Color color;

        public float height;

        public float radius;

        public int smoothness;

        public override void FillGeometry()
        {
            // Bottom
            var bottomVertex = new VertexBuffer();
            bottomVertex.pos = bottom;
            bottomVertex.color = color;
            geometryBuffer.AddVertex(bottomVertex);
            // Top
            var topVertex = new VertexBuffer();
            topVertex.pos = bottom + Vector3.Normalize(Vector3.right) * height;
            topVertex.color = color;
            geometryBuffer.AddVertex(topVertex);
            
            float perAngle = 2 * Mathf.PI / smoothness; 
            for( int i = 0 ; i < smoothness;i++ )
            {
                float angle = i * perAngle;
                var pos = bottom + new Vector3(0,Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);  
                geometryBuffer.AddVertex(pos,color);
            }

            // bottom triangles
            for( int i = 2 ;  i < smoothness + 2;i++ )
            {
                int first = 0;
                int second = i + 1;
                if( i >= smoothness + 1 )
                    second = second - smoothness;
                int third = i;
                geometryBuffer.AddTriangle(new int[]{first,second,third});
            }
            // top triangles
            for( int i = 2 ;  i < smoothness + 2;i++ )
            {
                int first = 1;
                int second = i + 1;
                if( i >= smoothness + 1 )
                    second = second - smoothness;
                int third = i;
                geometryBuffer.AddTriangle(new int[]{first,third,second});
            }
        }
    }
}