
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

        private float scatterSize = 0;

        [Range(0f,1.0f)]
        public float offset = 0f;


        private List<BoxCollider> m_colliders = new List<BoxCollider>();


        public void Execute()
        {
            CreateScatterMesh();

            int vertexIndex = 0;
            for (int x = 0; x < xCount; x++)
            {
                for(int y = 0; y < yCount; y++)
                {
                    for(int z = 0; z < zCount; z++)
                    {
                        Vector3 pos = Vector3.zero + new Vector3(x * scatterSize + x * offset,y * scatterSize + y * offset,z * scatterSize + z * offset);
                        CreateScatter(pos ,size[vertexIndex]);
                        vertexIndex++;
                    }
                }
            }
        }

        private void Update()
        {
            //foreach (BoxCollider boxCollider in m_colliders)
            //{
            //    var dis = Vector3.Distance(boxCollider.center, Camera.main.transform.position);
            //    var value = dis * 0.025f;
            //    boxCollider.size = new Vector3(value,value,value);
            //}
        }

        private List<float> size = new List<float>();

        private void CreateScatterMesh()
        {
            scatterSize = material.GetFloat("_PointRadius");
            meshFilter = this.gameObject.AddComponent<MeshFilter>();
            var meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh
            {
                name = "ScatterGraph3D"
            };

            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            List<Color> colors = new List<Color>();

            int vertexIndex = 0;
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    for (int z = 0; z < zCount; z++)
                    {
                        Vector3 pos = Vector3.zero + new Vector3(x * scatterSize + x * offset, y * scatterSize + y * offset, z * scatterSize + z * offset);
                        vertices.Add(pos);
                        indices.Add(vertexIndex);
                        float value = Random.Range(0.1f, 1.0f);
                        size.Add(value);
                        Color randomColor = new Color(value,value,value,value);
                        colors.Add(randomColor);
                        vertexIndex++;
                    }
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.SetIndices(indices.ToArray(),MeshTopology.Points, 0);
            mesh.colors = colors.ToArray();

            mesh.RecalculateBounds();
            meshFilter.mesh = mesh;
            meshRenderer.material = material;
           
        }

        protected override Scatter CreateScatter(Vector3 position,float size)
        {
            GameObject scatter = new GameObject("scatter3D");
            //scatter.hideFlags = HideFlags.HideInHierarchy;
            // todo 受到Shader Quad 和Circle尺寸影响
            scatter.transform.position = position;
            var boxCollider = scatter.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(0.1f,0.1f,0.1f);
            m_colliders.Add(boxCollider);
            var scatter3D = scatter.AddComponent<Scatter3D>();
            scatter3D.Generate(Vector3.one);
            return scatter3D;
        }
    }
}