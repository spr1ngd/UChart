
using System.Collections.Generic;
using UnityEngine;

namespace UChart.Scatter
{
    public class ScatterGraph3D : ScatterGraph
    {
        public Material material;

        public MeshFilter meshFilter = null;

        [Range(0,50)]
        public int xCount;

        [Range(0,50)]
        public int yCount;

        [Range(0,50)]
        public int zCount;

        [Range(0.5f,2.0f)]
        public float scatterSize = 1.0f;

        [Range(0.5f,2.0f)]
        public float offset = 2f;

        public void Execute()
        {
            CreateScatterMesh();

            for (int x = 0; x < xCount; x++)
            {
                for(int y = 0; y < yCount; y++)
                {
                    for(int z = 0; z < zCount; z++)
                    {
                        Vector3 pos = Vector3.zero + new Vector3(x * scatterSize + (x - 1) * offset,y * scatterSize + (y - 1) * offset,z * scatterSize + (z - 1) * offset);
                        CreateScatter(pos);
                    }
                }
            }
        }

        private void CreateScatterMesh()
        {
            var meshFilter = this.gameObject.AddComponent<MeshFilter>();
            var meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();
            mesh.name = "ScatterGraph3D";

            // vertices
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            //Vector3[] vertices = new Vector3[xCount * yCount * zCount];
            int vertexIndex = 0;
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    for (int z = 0; z < zCount; z++)
                    {
                        Vector3 pos = Vector3.zero + new Vector3(x * scatterSize + (x - 1) * offset, y * scatterSize + (y - 1) * offset, z * scatterSize + (z - 1) * offset);
                        vertices.Add(pos);
                        indices.Add(vertexIndex);
                        vertexIndex++;
                    }
                }
            }

            mesh.vertices = vertices.ToArray();
            //mesh.triangles = indices.ToArray();
            mesh.SetIndices(indices.ToArray(),MeshTopology.Points, 0);
            mesh.RecalculateBounds();
            meshFilter.mesh = mesh;
            meshRenderer.material = material;
        }

        protected override Scatter CreateScatter(Vector3 position)
        {
            GameObject scatter = new GameObject("scatter3D");
            //scatter.hideFlags = HideFlags.HideInHierarchy;
            scatter.transform.position = position;
            scatter.AddComponent<BoxCollider>();
            var scatter3D = scatter.AddComponent<Scatter3D>();
            //scatter3D.material = this.material;
            //scatter3D.size = this.scatterSize;
            //scatter3D.mesh = meshFilter.mesh;
            scatter3D.Generate(Vector3.one);
            //float value = Random.Range(0,5);
            //scatter3D.color = Color.green * Mathf.Abs(5 - value) / 5 + Color.red * value / 5;
            //scatter3D.color = new Color(scatter3D.color.r,
            //    scatter3D.color.g,
            //    scatter3D.color.b,0.5f);
            //return scatter3D;
            return null;
        }
    }
}