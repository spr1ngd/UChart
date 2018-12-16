
using UnityEngine;

namespace UChart
{
    public class Axis3D : Axis
    {
        public override void OnDrawArrow()
        {
            var arrowMesh = myGameobject.AddComponent<MeshFilter>();
            var arrowRenderer = myGameobject.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();
            mesh.name = "__ASIXARROW__";

            int vertexCount = 0 ;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] indices = new int[vertexCount];
            Color[] colors = new Color[vertexCount];

            // TODO: vertices
            // TODO: indices
            // TODO: colors 

            mesh.vertices = vertices;
            mesh.colors = colors;
            mesh.SetIndices(indices,MeshTopology.Triangles,0);
            arrowMesh.mesh = mesh;
            arrowRenderer.material = new Material(Shader.Find("Standard"));
        }

        public override void OnDrawMesh()
        {
            var meshFilter = myGameobject.AddComponent<MeshFilter>();
            var meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            float xOffset = axisLenght / xUnit;
            float yOffset = axisLenght / yUnit;
            float zOffset = axisLenght / zUnit;

            Mesh mesh = new Mesh();
            mesh.name = "__ASIX3D__";

            int vertexCount = 6 + xUnit * 4 + yUnit * 4 + zUnit* 4;
            Vector3[] veritces = new Vector3[vertexCount];
            int[] indices = new int[vertexCount];
            Color[] colors = new Color[vertexCount];
            int vertexIndex = 0;
            // vertices 
            // TODO: 添加侧面网格，网格位置会随视角变化而切换位置
            colors[vertexIndex] = axisColor;
            veritces[vertexIndex++] = transform.position;
            colors[vertexIndex] = axisColor;
            veritces[vertexIndex++] = transform.right * axisLenght * 1.1f;
            colors[vertexIndex] = axisColor;
            veritces[vertexIndex++] = transform.position;
            colors[vertexIndex] = axisColor;
            veritces[vertexIndex++] = transform.up * axisLenght * 1.1f;
            colors[vertexIndex] = axisColor;
            veritces[vertexIndex++] = transform.position;
            colors[vertexIndex] = axisColor;
            veritces[vertexIndex++] = transform.forward * axisLenght * 1.1f;

            Vector3 origin = Vector3.zero;

            // x axis mesh
            for( int y = 1 ; y <= yUnit; y++ )
            {
                Vector3 start = origin + new Vector3(0,y * yOffset,0);
                Vector3 end = origin + new Vector3(0,y * yOffset,axisLenght);
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = start;
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = end;
            }

            for( int z = 1 ; z <= zUnit; z++ )
            {
                Vector3 start = origin + new Vector3(0,0,z * zOffset);
                Vector3 end = origin + new Vector3(0,axisLenght,z * zOffset);
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = start;
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = end;
            }

            // y axis mesh

            for( int x = 1 ; x <= xUnit; x++ )
            {
                Vector3 start = origin + new Vector3(x * xOffset,0,0);
                Vector3 end = origin + new Vector3(x * xOffset,0,axisLenght);
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = start;
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = end;
            }

            for( int z = 1 ; z <= zUnit; z++ )
            {
                Vector3 start = origin + new Vector3(0,0,z * zOffset);
                Vector3 end = origin + new Vector3(axisLenght,0,z * zOffset);
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = start;
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = end;
            }

            // z axis mesh
            for( int x = 1 ; x <= xUnit; x++ )
            {
                Vector3 start = origin + new Vector3(x * xOffset,0,0);
                Vector3 end = origin + new Vector3(x * xOffset,axisLenght,0);
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = start;
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = end;
            }

            for( int y = 1 ; y <= yUnit; y++ )
            {
                Vector3 start = origin + new Vector3(0,y * yOffset,0);
                Vector3 end = origin + new Vector3(axisLenght,y * yOffset,0);
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = start;
                colors[vertexIndex] = meshColor;
                veritces[vertexIndex++] = end;
            }

            // indices
            for( int i = 0 ; i < indices.Length;i++ )
                indices[i] = i;

            mesh.vertices = veritces;
            mesh.colors = colors;
            mesh.SetIndices(indices,MeshTopology.Lines,0);

            meshFilter.mesh = mesh;
            meshRenderer.material = new Material(Shader.Find("UChart/Axis/Axis(Basic)"));
            // meshRenderer.material = new Material(Shader.Find("Standard"));
        }
    }
}