
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        //private Texture texture = null;
        private Texture2D tempTex = null;
        private RenderTexture renderTexture = null;
        private Camera pickCamera = null;

        //public RenderTexture pickTexture = null;

        private void Awake()
        {
            //texture = pickMaterial.mainTexture;
            tempTex = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
        }

        private void Update()
        {
            // TODO: 拾取Texture有问题
            // TODO: 内存泄露
            if (null == renderTexture)
                return;
            RenderTexture.active = renderTexture;
            tempTex.ReadPixels(new Rect(0,0,renderTexture.width,renderTexture.height),0,0);
            tempTex.Apply();
            GameObject.Find("Canvas/RawImage").GetComponent<RawImage>().texture = renderTexture;
            var c = tempTex.GetPixel((int)Input.mousePosition.x,(int)Input.mousePosition.y);
            print(c);
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

            // TODO: 封装重构
            // TODO: 将其渲染到相机上进行拾取


            GameObject pickGameObject = new GameObject("UCHART_PCIK_GAMEOBJECT");
            //pickGameObject.hideFlags = HideFlags.HideInHierarchy;
            var pickMesh = pickGameObject.AddComponent<MeshFilter>();
            pickMesh.mesh = mesh;
            var pickRender = pickGameObject.AddComponent<MeshRenderer>();
            pickRender.material = pickMaterial;
            pickGameObject.layer = UChart.ucharLayer;

            GameObject pickCameraGO = new GameObject("UCHART_PICK_CAMERA");
            pickCameraGO.transform.position = Camera.main.transform.position;
            pickCameraGO.transform.eulerAngles = Camera.main.transform.eulerAngles;
            //pickCameraGO.hideFlags = HideFlags.HideInHierarchy;
            pickCamera = pickCameraGO.AddComponent<Camera>();
            pickCamera.cullingMask = 1 << 31;
            pickCamera.clearFlags = CameraClearFlags.Color;
            pickCamera.backgroundColor = new Color(0,0,0,1);
            pickCameraGO.layer = UChart.ucharLayer;
            renderTexture = new RenderTexture(Screen.width,Screen.height,24);
            pickCamera.targetTexture = renderTexture;

            print(renderTexture.height+"*"+ renderTexture.width);
            //tempTex = pickTexture.toTexture2D();
            //GameObject.Find("Canvas/RawImage").GetComponent<RawImage>().texture = pickTexture;
            //for (var x = 0; x < tempTex.width; x++)
            //{
            //    for (var y = 0; y < tempTex.height; y++)
            //    {
            //        var color =  tempTex.GetPixel(x, y);
            //        print(color);
            //    }
            //}
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
            scatter.hideFlags = HideFlags.HideInHierarchy;
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