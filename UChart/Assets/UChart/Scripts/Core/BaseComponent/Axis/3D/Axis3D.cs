
using UnityEngine;

namespace UChart
{
    public class Axis3D : Axis
    {
        public override void OnDrawMesh()
        {
            var meshFilter = myGameobject.AddComponent<MeshFilter>();
            var meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            float xOffset = axisLenght / xUnit;
            float yOffset = axisLenght / yUnit;
            float zOffset = axisLenght / zUnit;

            Mesh mesh = new Mesh();
            mesh.name = "__ASIX3D__";

            Vector3[] veritces = new Vector3[6 + xUnit * 4 + yUnit * 4 + zUnit * 4];
            int[] indices = new int[6 + xUnit * 4 + yUnit * 4 + zUnit * 4];
            Color[] colors = new Color[6 + xUnit * 4 + yUnit * 4 + zUnit * 4];
            int vertexIndex = 0;
            // vertices 
            // TODO: 添加侧面网格，网格位置会随视角变化而切换位置
            veritces[vertexIndex++] = transform.position;
            veritces[vertexIndex++] = transform.right * axisLenght * 1.1f;
            veritces[vertexIndex++] = transform.position;
            veritces[vertexIndex++] = transform.up * axisLenght * 1.1f;
            veritces[vertexIndex++] = transform.position;
            veritces[vertexIndex++] = transform.forward * axisLenght * 1.1f;

            Vector3 origin = Vector3.zero;

            // x axis mesh
            for( int y = 0 ; y < yUnit; y++ )
            {
                Vector3 start = origin + new Vector3(0,y * yOffset,0);
                Vector3 end = origin + new Vector3(0,y * yOffset,axisLenght);
                veritces[vertexIndex++] = start;
                veritces[vertexIndex++] = end;
            }

            for( int z = 0 ; z < zUnit; z++ )
            {
                Vector3 start = origin + new Vector3(0,0,z * zOffset);
                Vector3 end = origin + new Vector3(0,axisLenght,z * zOffset);
                veritces[vertexIndex++] = start;
                veritces[vertexIndex++] = end;
            }

            // y axis mesh

            for( int x = 0 ; x < xUnit; x++ )
            {
                Vector3 start = origin + new Vector3(x * xOffset,0,0);
                Vector3 end = origin + new Vector3(x * xOffset,0,axisLenght);
                veritces[vertexIndex++] = start;
                veritces[vertexIndex++] = end;
            }

            for( int z = 0 ; z < zUnit; z++ )
            {
                Vector3 start = origin + new Vector3(0,0,z * zOffset);
                Vector3 end = origin + new Vector3(axisLenght,0,z * zOffset);
                veritces[vertexIndex++] = start;
                veritces[vertexIndex++] = end;
            }

            // z axis mesh
            for( int x = 0 ; x < xUnit; x++ )
            {
                Vector3 start = origin + new Vector3(x * xOffset,0,0);
                Vector3 end = origin + new Vector3(x * xOffset,axisLenght,0);
                veritces[vertexIndex++] = start;
                veritces[vertexIndex++] = end;
            }

            for( int y = 0 ; y < yUnit; y++ )
            {
                Vector3 start = origin + new Vector3(0,y * yOffset,0);
                Vector3 end = origin + new Vector3(axisLenght,y * yOffset,0);
                veritces[vertexIndex++] = start;
                veritces[vertexIndex++] = end;
            }

            // indices
            for( int i = 0 ; i < indices.Length;i++ )
                indices[i] = i;
            for( int i = 0 ; i < colors.Length;i++)
                colors[i] = Color.cyan;

            mesh.vertices = veritces;
            mesh.colors = colors;
            mesh.SetIndices(indices,MeshTopology.Points,0);

            meshFilter.mesh = mesh;
            meshRenderer.material = new Material(Shader.Find("UChart/Axis/Axis(Basic)"));
            // meshRenderer.material = new Material(Shader.Find("Standard"));
        }
    }
}