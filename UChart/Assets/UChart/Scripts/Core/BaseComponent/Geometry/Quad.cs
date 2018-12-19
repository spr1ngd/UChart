

using UnityEngine;

namespace UChart
{
    public class Quad : Geometry
    {
        protected override void CreateMesh(Mesh mesh)
        {
            this.vertices = new Vector3[4];
            this.triangles = new int[6];

            // vertices
            SetVertices();

            // triangels
            SetTriangles();

            // uvs

            // normals && tangent


        }

        private void SetVertices()
        {
            float left = 0, right = 0, forword = 0, back = 0;

            switch(anchor)
            {
                case GeometryAnchor.Center:
                    left = -size.x / 2.0f;
                    right = size.x / 2.0f;
                    forword = size.z / 2.0f;
                    back = -size.z / 2.0f;
                    break;
                case GeometryAnchor.Bottom:
                    left = -size.x / 2.0f;
                    right = size.x / 2.0f;
                    forword = size.z / 2.0f;
                    back = -size.z / 2.0f;
                    break;
                default:
                    throw new UChartException(string.Format("Current version can not support {0} anchor.",anchor));
            }

            int verticeInedx = 0;
            this.vertices[verticeInedx++] = new Vector3(left,0,back);
            this.vertices[verticeInedx++] = new Vector3(left,0,forword);
            this.vertices[verticeInedx++] = new Vector3(right,0,forword);
            this.vertices[verticeInedx++] = new Vector3(right,0,back);
        }

        private void SetTriangles()
        {
            int triangleIndex = 0;
            this.triangles[triangleIndex++] = 0;
            this.triangles[triangleIndex++] = 1;
            this.triangles[triangleIndex++] = 2;

            this.triangles[triangleIndex++] = 2;
            this.triangles[triangleIndex++] = 3;
            this.triangles[triangleIndex++] = 0;
        }
    }
}