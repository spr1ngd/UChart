
using UnityEngine;

namespace UChart
{
    public class Grid3D : Grid
    {
        public override void Draw()
        {
            Vector3 start = new Vector3(-gridSize / 2.0f,-gridSize / 2.0f,0);
            float cellSize = gridSize / division;

            var meshFilter = myGameobject.AddComponent<MeshFilter>();
            var meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();
            mesh.name = "__GRID3D__";

            Vector3[] vertices = new Vector3[ (division + 1) * 2 * 2];
            int[] indices = new int[(division+1) * 2 * 2];
            
            int vertexIndex = 0;
            for( int i = 0 ; i <= division ;i++ )
            {
                vertices[vertexIndex++] = start + new Vector3(cellSize * i ,0,0);
                vertices[vertexIndex++] = start + new Vector3(cellSize * i ,gridSize,0);
                // vertices[vertexIndex++] = Vector3.zero;
                // vertices[vertexIndex++] = Vector3.zero;
            }
            for( int j = 0 ; j <= division;j++ )
            {
                vertices[vertexIndex++] = start + new Vector3(0 ,cellSize * j,0);
                vertices[vertexIndex++] = start + new Vector3(gridSize ,cellSize * j,0);
                // vertices[vertexIndex++] = Vector3.zero;
                // vertices[vertexIndex++] = Vector3.zero;
            }
            // vertices
            mesh.vertices = vertices;
            // indices
            for( int i = 0 ; i < indices.Length ; i++ )
            {
                indices[i] = i;
            }

            mesh.vertices = vertices;
            mesh.SetIndices(indices,MeshTopology.Lines,0);

            meshFilter.mesh = mesh;
            meshRenderer.material = new Material(Shader.Find("UChart/Grid/Grid(Basic)"));
        }
    }
}