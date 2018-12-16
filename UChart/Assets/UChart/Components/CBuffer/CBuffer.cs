
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    public class CBuffer
    {
        private static int CBufferId = UChart.uchartLayer;
        private IDictionary<string,Color> cbufferDic = new Dictionary<string, Color>();
        private IDictionary<string,Transform> transDic = new Dictionary<string,Transform>();
        private Color[] m_colorBuffer = null;

        public int cBufferId { get; private set; }
        public bool enable { get; set; }

        private bool m_initialized = false;
        private RenderTexture m_renderTexture = null;
        private Texture2D m_readTexture = null;
        private Rect m_readTextureRect;

        public CBuffer()
        {
            this.cBufferId = CBufferId--;
            enable = true;
        }

        public void Initialize()
        {

        }

        public void Release()
        {
            cbufferDic.Clear();
            cbufferDic = null;
        }

        public bool GetCBuffer( int pixelX ,int pixelY ,out CBufferHit cBuffer )
        {
            cBuffer = null;
            if(null == m_renderTexture || null == m_readTexture)
            {
                //throw new UChartException("please call method [AddRenderer] firstly.");
                return false;
            }

            // read screen pixel to texture2d
            RenderTexture.active = m_renderTexture;
            m_readTexture.ReadPixels(m_readTextureRect,0,0);
            m_readTexture.Apply();

            // get pixel color and set cbuffer value.
            Color pickColor = m_readTexture.GetPixel(pixelX,pixelY);
            Debug.Log(pickColor);
            foreach(KeyValuePair<string,Color> pair in cbufferDic)
            {
                if(pair.Value == pickColor)
                {
                    cBuffer = new CBufferHit();
                    cBuffer.name = pair.Key;
                    cBuffer.point = transDic[pair.Key];
                    return true;
                }
            }

            // get pickId in colorbuffer
            for(int i = 0; i < m_colorBuffer.Length; i++)
            {
                if(pickColor == m_colorBuffer[i])
                {
                    cBuffer = new CBufferHit();
                    cBuffer.pickId = i;
                    return true;
                }
            }
            return false;
        }

        public bool GetCBuffer( Vector2 pixelPosition,out CBufferHit cBuffer )
        {
            return GetCBuffer((int)(pixelPosition.x),(int)(pixelPosition.y),out cBuffer);
        }

        public void AddRenderer(string pickName,Transform trans,Material material)
        {
            var meshFilters = trans.GetComponents<MeshFilter>();
            foreach(var meshFilter in meshFilters)
            {
                var pickMesh = meshFilter.mesh;
                if(null == pickMesh)
                {
                    throw new UChartRendererException(string.Format("the gameobject [ "+meshFilter.transform.name+"]' mesh filter is null."));
                }
                this.AddRenderer(meshFilter.transform.name,pickMesh,material);
            }
        }

        public void AddRenderer(string pickName,Mesh mesh,Material material,Color[] colorBuffer = null)
        {
            m_colorBuffer = colorBuffer;
            if (!m_initialized)
                CreateCamera();
            CreateBufferModel(pickName,mesh,material);
        }

        private void CreateBufferModel( string pickName, Mesh pickMesh , Material pickMaterial)
        {
            GameObject pickGameObject = new GameObject(pickName);
#if UCHART_DEBUG
            //pickGameObject.hideFlags = HideFlags.HideInHierarchy;
#endif
            var pickMeshFilter = pickGameObject.AddComponent<MeshFilter>();
            pickMeshFilter.mesh = pickMesh;
            var pickRender = pickGameObject.AddComponent<MeshRenderer>();
            pickRender.material = pickMaterial;
            pickGameObject.layer = cBufferId;
            cbufferDic.Add(pickName,UChart.Int2Color(cbufferDic.Count + 1));
        }

        private void CreateCamera()
        {
            Camera mainCamera = Camera.main;
            Transform mainCameraTransform = mainCamera.transform;
            GameObject pickCameraGO = new GameObject("UCHART_PICK_CAMERA");
            pickCameraGO.transform.position = mainCameraTransform.position;
            pickCameraGO.transform.eulerAngles = mainCameraTransform.eulerAngles;
#if UCHART_DEBUG
            //pickCameraGO.hideFlags = HideFlags.HideInHierarchy;
#endif
            // todo: 这里更改为Clone主相机，然后重置layer以及相机模式
            var pickCamera = pickCameraGO.AddComponent<Camera>();
            pickCamera.cullingMask = 1 << cBufferId;
            pickCamera.clearFlags = CameraClearFlags.Color;
            pickCamera.backgroundColor = new Color(0,0,0,0);
            pickCamera.fieldOfView = mainCamera.fieldOfView;
            pickCamera.farClipPlane = mainCamera.farClipPlane;
            pickCamera.nearClipPlane = mainCamera.nearClipPlane;
            pickCamera.orthographic = mainCamera.orthographic;
            if (mainCamera.orthographic)
                pickCamera.orthographicSize = mainCamera.orthographicSize;
            pickCameraGO.layer = cBufferId;

            // todo：依据屏幕尺寸触发修改纹理大小的事件
            m_renderTexture = new RenderTexture(Screen.width,Screen.height,24);
            m_readTextureRect = new Rect(0,0,m_renderTexture.width,m_renderTexture.height);
            m_readTexture = new Texture2D(m_renderTexture.width,m_renderTexture.height,TextureFormat.ARGB32,false);
            pickCamera.targetTexture = m_renderTexture;

            Loom.Instance.InvokeUdpate("_UCHART_CUBFFER_UPDATE",() =>
            {
                if(!this.enable)
                    return;
                pickCameraGO.transform.position = mainCameraTransform.position;
                pickCameraGO.transform.eulerAngles = mainCameraTransform.eulerAngles;
            });
        }
    }
}