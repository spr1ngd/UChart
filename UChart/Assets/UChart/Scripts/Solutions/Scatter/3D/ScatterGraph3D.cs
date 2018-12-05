
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UChart.Scatter
{
    public class ScatterGraph3D : ScatterGraph
    {
        private const float SCALE_FACTOR = 0.025f;
        private const float MAXSIZE_FACTOR = 1.2f;

        public Material material;
        public Material pickMaterial;
        private Mesh mesh = null;
        private MeshRenderer meshRenderer = null;

        [Range(0,50)] public int xCount;
        [Range(0,50)] public int yCount;
        [Range(0,50)] public int zCount;
        [Range(0f,1.0f)] public float offset = 0f;

        private float scatterSize = 0;
        private float maxScatterSize = 0.0f;

        private List<Scatter> m_scatter3d = new List<Scatter>(); // record scatte3d data.
        private Vector3[] verticesArray = null;
        private int[] indicesArray = null;
        private Color[] colorsArray = null;
        private Color[] pickColors = null;
        
        private CBuffer m_cBuffer;
        private CBufferHit m_cBufferHit = null;
        private Color pickColor;

        private int m_pickID = -1;
        private int pickId
        {
            set
            {
                if(value == m_pickID)
                    return;
                if(m_pickID >= 0)
                {
                    colorsArray[m_pickID] = pickColor;
                }
                if(value >= 0)
                {
                    pickColor = colorsArray[value];
                    colorsArray[value] = Color.yellow;
                }
                mesh.colors = colorsArray;
                m_pickID = value;
            }
        }

        private void Awake()
        {
            m_cBuffer = new CBuffer();
        }

        private void OnApplicationQuit()
        {

        }

        private void Update()
        {
            if(m_cBuffer.GetCBuffer(Input.mousePosition,out m_cBufferHit)) this.pickId = m_cBufferHit.pickId;
            else this.pickId = -1;
        }

        public void Execute()
        {
            scatterSize = material.GetFloat("_PointRadius");
            maxScatterSize = scatterSize * MAXSIZE_FACTOR;
            int pointCount = xCount * yCount * zCount;
            verticesArray = new Vector3[pointCount];
            indicesArray = new int[pointCount];
            colorsArray = new Color[pointCount];

            var meshFilter = this.gameObject.AddComponent<MeshFilter>();
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
                        float size = Random.Range(0.2f,0.5f);
                        Color randomColor = new Color(size,size,size,size);
                        var scatter = CreateScatter(vertexIndex,pos,size);
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

            pickColors = new Color[pointCount];
            for(int i = 0, count = 1; count <= pickColors.Length; i++, count++)
            {
                int colorR = 0, colorG = 0, colorB = 0;
                colorR = count / (256 * 2);
                colorG = count / 256 % 256;
                colorB = count % 256;
                pickColors[i] = new Color(colorR / 255.0f,colorG / 255.0f,colorB / 255.0f,1);
            }
            var pickMesh = new Mesh() { name = "UCHART_PICKMESH" };
            pickMesh.vertices = verticesArray;
            pickMesh.SetIndices(indicesArray,MeshTopology.Points,0);
            pickMesh.colors = pickColors;
            m_cBuffer.AddRenderer(pickMesh.name,pickMesh,pickMaterial,pickColors);
        }

        protected override Scatter CreateScatter(int scatterID,Vector3 position,float size)
        {
            var scatter =  new Scatter3D();
            return scatter;
        }
    }
}