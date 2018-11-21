
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
        private float maxScatterSize = 0.0f;

        [Range(0f,1.0f)]
        public float offset = 0f;


        private List<BoxCollider> m_colliders = new List<BoxCollider>();
        private List<Scatter> m_scatter3d = new List<Scatter>();

        private void Update()
        {
            foreach(BoxCollider boxCollider in m_colliders)
            {
                var dis = Vector3.Distance(boxCollider.transform.position,Camera.main.transform.position);
                var value = dis * 0.005f;
                if(value > maxScatterSize)
                    value = maxScatterSize;
                boxCollider.size = new Vector3(value,value,value);
            }
        }

        private Mesh mesh = null;
        private List<Vector3> vertices = null;
        private List<int> indices = null;
        private List<Color> colors = null;

        public void Execute()
        {
            scatterSize = material.GetFloat("_PointRadius");
            maxScatterSize = scatterSize * 1.5f;
            meshFilter = this.gameObject.AddComponent<MeshFilter>();
            var meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

            mesh = new Mesh
            {
                name = "ScatterGraph3D"
            };

            vertices = new List<Vector3>();
            indices = new List<int>();
            colors = new List<Color>();

            int vertexIndex = 0;
            for(int x = 0; x < xCount; x++)
            {
                for(int y = 0; y < yCount; y++)
                {
                    for(int z = 0; z < zCount; z++)
                    {
                        Vector3 pos = Vector3.zero + new Vector3(x * scatterSize + x * offset,y * scatterSize + y * offset,z * scatterSize + z * offset);
                        vertices.Add(pos);
                        indices.Add(vertexIndex);
                        float size = Random.Range(0.5f,1.0f);
                        Color randomColor = new Color(size,size,size,size);
                        colors.Add(randomColor);
                       
                        var scatter = CreateScatter(pos,size);
                        scatter.color = randomColor;
                        scatter.index = vertexIndex;
                        m_scatter3d.Add(scatter);
                        vertexIndex++;
                    }
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.SetIndices(indices.ToArray(),MeshTopology.Points,0);
            mesh.colors = colors.ToArray();
            mesh.RecalculateBounds();
            meshFilter.mesh = mesh;
            meshRenderer.material = material;
        }

        public override void RefreshScatter()
        {
            mesh.Clear();
            vertices = new List<Vector3>();
            indices = new List<int>();
            colors = new List<Color>();

            foreach (Scatter scatter in m_scatter3d)
            {
                vertices.Add(scatter.transform.position);
                indices.Add(scatter.index);
                colors.Add(scatter.color);
            }
            mesh.vertices = vertices.ToArray();
            mesh.SetIndices(indices.ToArray(),MeshTopology.Points,0);
            mesh.colors = colors.ToArray();
            mesh.RecalculateBounds();
            meshFilter.mesh = mesh;
        }

        protected override Scatter CreateScatter(Vector3 position,float size)
        {
            GameObject scatter = new GameObject("scatter3D");
            scatter.hideFlags = HideFlags.HideInHierarchy;
            scatter.transform.position = position;
            var scatter3D = scatter.AddComponent<Scatter3D>();
            scatter3D.size = size;
            scatter3D.scatterGraph = this;
            var boxCollider = scatter.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(size,size,size);
            //print(scatter3D.transform.name + boxCollider.size);
            m_colliders.Add(boxCollider);
            scatter3D.Generate(Vector3.one);
            return scatter3D;
        }
    }
}