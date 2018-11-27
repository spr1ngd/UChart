
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UChart.Scatter
{
    public class ScatterGraph3D : ScatterGraph
    {
        private const float SCALE_FACTOR = 0.025f;
        private const float MAXSIZE_FACTOR = 1.2f;

        public Material material;
        public MeshFilter meshFilter = null;
        private MeshRenderer meshRenderer = null;

        [Range(0,50)] public int xCount;
        [Range(0,50)] public int yCount;
        [Range(0,50)] public int zCount;
        [Range(0f,1.0f)] public float offset = 0f;

        private float scatterSize = 0;
        private float maxScatterSize = 0.0f;

        private List<BoxCollider> m_colliders = new List<BoxCollider>();
        private List<Scatter> m_scatter3d = new List<Scatter>();

        private Mesh mesh = null;
        private Vector3[] verticesArray = null;
        private int[] indicesArray = null;
        private Color[] colorsArray = null;

        public Texture texture;
        public RenderTexture renderTexture;
        private Rect rect = new Rect(0f,0,200f,200f);

        void Start()
        {
            Graphics.Blit(this.texture,this.renderTexture,Instantiate<Material>(this.material));
        }
        
        void OnGUI()
        {
            
            GUI.DrawTexture(this.rect,this.renderTexture);
        }

        public void Execute()
        {
            scatterSize = material.GetFloat("_PointRadius");
            maxScatterSize = scatterSize * MAXSIZE_FACTOR;
            int pointCount = xCount * yCount * zCount;
            verticesArray = new Vector3[pointCount];
            indicesArray = new int[pointCount];
            colorsArray = new Color[pointCount];

            meshFilter = this.gameObject.AddComponent<MeshFilter>();
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
            mesh = new Mesh { name = "ScatterGraph3D" };

            int vertexIndex = 0;
            for(int x = 0; x < xCount; x++)
            {
                for(int y = 0; y < yCount; y++)
                {
                    for(int z = 0; z < zCount; z++)
                    {
                        Vector3 pos = Vector3.zero + new Vector3(x * scatterSize + x * offset,y * scatterSize + y * offset,z * scatterSize + z * offset);
                       
                        // TODO : color颜色获取需求变更
                        float size = Random.Range(0.5f,1.0f);
                        Color randomColor = new Color(size,size,size,size);

                        // TODO: 利用协程处理大量对象创建时产生的卡顿
                        var scatter = CreateScatter(pos,size);
                        scatter.color = randomColor;
                        scatter.index = vertexIndex;

                        verticesArray[vertexIndex] = pos;
                        indicesArray[vertexIndex] = vertexIndex;
                        colorsArray[vertexIndex] = randomColor;

                        m_scatter3d.Add(scatter);
                        vertexIndex++;
                    }
                }
            }

            mesh.vertices = verticesArray;
            mesh.SetIndices(indicesArray,MeshTopology.Points,0);
            mesh.colors = colorsArray;

            meshFilter.mesh = mesh;
            meshRenderer.material = material;
        }

        public void RefreshMeshData( int index , Color color )
        {
            colorsArray[index] = color;
        }

        // TODO: 与Execute内容进行整合重构 提炼整合该方法至基类中
        public override void RefreshScatter()
        {
            mesh.Clear();
            mesh.vertices = verticesArray;
            mesh.SetIndices(indicesArray,MeshTopology.Points,0);
            mesh.colors = colorsArray;
            meshFilter.mesh = mesh;
        }

        protected override Scatter CreateScatter(Vector3 position,float size)
        {
            GameObject scatter = new GameObject("scatter3D");
            scatter.layer = 8;
            //scatter.hideFlags = HideFlags.HideInHierarchy;
            scatter.transform.position = position;
            var scatter3D = scatter.AddComponent<Scatter3D>();
            scatter3D.size = size;
            scatter3D.scatterGraph = this;
            //var boxCollider = scatter.AddComponent<BoxCollider>();
            //boxCollider.isTrigger = true;
            //boxCollider.size = new Vector3(size,size,size);
            //m_colliders.Add(boxCollider);
            scatter3D.Generate(Vector3.one);
            return scatter3D;
        }
    }
}