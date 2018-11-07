
using UnityEngine;

namespace UChart.Polygon
{
    public class Cube : Polygon
    {
        public Vector3 size = Vector3.one;

        protected override void CreateMesh(Mesh mesh)
        {
            this.vertices = new Vector3[8];
            this.triangles = new int[36];

            float x = size.x / 2.0f;
            float y = size.y / 2.0f;
            float z = size.z / 2.0f;

            vertices[0] = new Vector3(-x,-y,-z);
            vertices[1] = new Vector3(-x,-y,z);
            vertices[2] = new Vector3(x,-y,z);
            vertices[3] = new Vector3(x,-y,-z);
            vertices[4] = new Vector3(-x,y,-z);
            vertices[5] = new Vector3(-x,y,z);
            vertices[6] = new Vector3(x,y,-z);
            vertices[7] = new Vector3(x,y,z);

            triangles[0] = 0;triangles[1] = 3;triangles[2] = 2;
            triangles[3] = 2;triangles[4] = 1;triangles[5] = 0;

            triangles[6] = 0;triangles[7] = 4;triangles[8] = 3;
            triangles[9] = 3;triangles[10] = 4;triangles[11] =6;

            triangles[12] = 1;triangles[13] = 5;triangles[14] = 0;
            triangles[15] = 0;triangles[16] = 5;triangles[17] = 4;

            triangles[18] = 2;triangles[19] = 7;triangles[20] = 1;
            triangles[21] = 1;triangles[22] = 7;triangles[23] = 5;

            triangles[24] = 2;triangles[25] = 3;triangles[26] = 6;
            triangles[27] = 6;triangles[28] = 7;triangles[29] = 2;

            triangles[30] = 4;triangles[31] = 5;triangles[32] = 6;
            triangles[33] = 6;triangles[34] = 5;triangles[35] = 7;

            mesh.vertices = this.vertices;
            mesh.triangles = this.triangles;
        }
    }
}