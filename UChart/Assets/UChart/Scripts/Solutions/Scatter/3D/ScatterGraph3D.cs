
using System;
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
        private Color[] pickColors = null;

        private Texture2D tempTex = null;
        private RenderTexture renderTexture = null;
        private Camera pickCamera = null;

        private Color pickColor;
        private int m_pickID = 0;

        private CBuffer m_cBuffer;

        private int pickId
        {
            set
            {
                if (value == m_pickID)
                    return;
                if (m_pickID > 0)
                {
                    colorsArray[m_pickID] = pickColor;
                }
                if (value > 0)
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
            tempTex = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
        }

        private void OnApplicationQuit()
        {

        }

        private void Update()
        {
            if (null == renderTexture)
                return;
            RenderTexture.active = renderTexture;
            tempTex.ReadPixels(new Rect(0,0,renderTexture.width,renderTexture.height),0,0);
            tempTex.Apply();
            Color pickColorTemp = tempTex.GetPixel((int)Input.mousePosition.x,(int)Input.mousePosition.y);
            if (pickColorTemp == Color.black)
            {
                pickId = 0;
                return;
            }
            for (var i = 0; i < pickColors.Length; i++)
            {
                var c = pickColors[i];
                if (c == pickColorTemp)
                {
                    pickId = i;
                    return;
                }
            }
        }

        private void FixedUpdate()
        {
            if (null == pickCamera)
                return;
            pickCamera.transform.position = Camera.main.transform.position;
            pickCamera.transform.eulerAngles = Camera.main.transform.eulerAngles;
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

            // TODO: 封装重构
            // TODO: 将其渲染到相机上进行拾取

            

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

            m_cBuffer.AddRenderer(pickMesh.name,pickMesh,pickMaterial);
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

        protected override Scatter CreateScatter(int scatterID,Vector3 position,float size)
        {
            var scatter =  new Scatter3D();
            return scatter;
        }
    }
}