
using UnityEngine;

namespace UChart
{
    [System.Serializable]
    public class CubeGeometry : Geometry
    {
        public bool isSquare = false;

        public Color color = Color.white;

        public Vector3 center = Vector3.zero;

        public float length = 2.0f;

        public float width = 2.0f;

        public float height = 2.0f;

        public override void FillGeometry()
        {
            anchor = GeometryAnchor.Bottom;
            Vector3 offset = Vector3.zero;
            float halfLength = length / 2.0f;
            float halfWidth = width / 2.0f;
            float halfHeight = height / 2.0f;

            switch(anchor)
            {
                case GeometryAnchor.Bottom:
                    offset = new Vector3(0,halfHeight,0);
                break;
                case GeometryAnchor.Up:
                    offset = new Vector3(0,-halfHeight,0);
                break;
            }
            
            var bFirst = center + new Vector3(halfLength,-halfHeight,halfWidth) + offset;
            var bSecond = center + new Vector3(halfLength,-halfHeight,-halfWidth)+ offset;
            var bThird = center + new Vector3(-halfLength,-halfHeight,-halfWidth)+ offset;
            var bFouth = center + new Vector3(-halfLength,-halfHeight,halfWidth)+ offset;

            var tFirst = center + new Vector3(halfLength,halfHeight,halfWidth)+ offset;
            var tSecond = center + new Vector3(halfLength,halfHeight,-halfWidth)+ offset;
            var tThird = center + new Vector3(-halfLength,halfHeight,-halfWidth)+ offset;
            var tFouth = center + new Vector3(-halfLength,halfHeight,halfWidth)+ offset;

            // bottom
            geometryBuffer.AddQuad(new VertexBuffer[]
            {
                new VertexBuffer(){pos = bFirst,color = color},
                new VertexBuffer(){pos = bSecond,color = color},
                new VertexBuffer(){pos = bThird,color = color},
                new VertexBuffer(){pos = bFouth,color = color},
            });

            // up
            geometryBuffer.AddQuad(new VertexBuffer[]
            {
                new VertexBuffer(){pos = tFouth,color = color},
                new VertexBuffer(){pos = tThird,color = color},
                new VertexBuffer(){pos = tSecond,color = color},
                new VertexBuffer(){pos = tFirst,color = color},
            });

            // right
            geometryBuffer.AddQuad(new VertexBuffer[]
            {
                new VertexBuffer(){pos = tFirst,color = color},
                new VertexBuffer(){pos = tSecond,color = color},
                new VertexBuffer(){pos = bSecond,color = color},
                new VertexBuffer(){pos = bFirst,color = color}               
            });

            // left
            geometryBuffer.AddQuad(new VertexBuffer[]
            {
                new VertexBuffer(){pos = tThird,color = color},
                new VertexBuffer(){pos = tFouth,color = color},                
                new VertexBuffer(){pos = bFouth,color = color},
                new VertexBuffer(){pos = bThird,color = color}                  
            });

            // forward
            geometryBuffer.AddQuad(new VertexBuffer[]
            {
                new VertexBuffer(){pos = tFouth,color = color},
                new VertexBuffer(){pos = tFirst,color = color},
                new VertexBuffer(){pos = bFirst,color = color},
                new VertexBuffer(){pos = bFouth,color = color},
            });

            // back 
            geometryBuffer.AddQuad(new VertexBuffer[]
            {
                new VertexBuffer(){pos = tSecond,color = color},
                new VertexBuffer(){pos = tThird,color = color},
                new VertexBuffer(){pos = bThird,color = color},
                new VertexBuffer(){pos = bSecond,color = color},
            });
        }
    }
}