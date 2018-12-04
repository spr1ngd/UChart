
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UChart
{
    public class CBuffer
    {
        private static int CBufferId = UChart.uchartLayer;
        private static IDictionary<string,Color> cbufferDic = new Dictionary<string, Color>();

        private bool m_initialized = false;
        public int cBufferId { get; private set; }

        public CBuffer()
        {
            this.cBufferId = CBufferId--;
        }

        public void AddRenderer(string pickName,Mesh mesh,Material material)
        {
            if (!m_initialized)
                CreateCamera();
            CreateBufferModel(pickName,mesh,material);
        }

        private void CreateBufferModel( string pickName, Mesh pickMesh , Material pickMaterial)
        {
            GameObject pickGameObject = new GameObject(pickName);
#if UCHART_DEBUG
            pickGameObject.hideFlags = HideFlags.HideInHierarchy;
#endif
            var pickMeshFilter = pickGameObject.AddComponent<MeshFilter>();
            pickMeshFilter.mesh = pickMesh;
            var pickRender = pickGameObject.AddComponent<MeshRenderer>();
            pickRender.material = pickMaterial;
            pickGameObject.layer = cBufferId;
            cbufferDic.Add(pickName,IndexToColor(cbufferDic.Count + 1));
        }

        private void CreateCamera()
        {
            Camera mainCamera = Camera.main;
            Transform mainCameraTransform = mainCamera.transform;
            GameObject pickCameraGO = new GameObject("UCHART_PICK_CAMERA");
            pickCameraGO.transform.position = mainCameraTransform.position;
            pickCameraGO.transform.eulerAngles = mainCameraTransform.eulerAngles;
#if UCHART_DEBUG
            pickCameraGO.hideFlags = HideFlags.HideInHierarchy;
#endif
            var pickCamera = pickCameraGO.AddComponent<Camera>();
            pickCamera.cullingMask = 1 << cBufferId;
            pickCamera.clearFlags = CameraClearFlags.Color;
            pickCamera.backgroundColor = new Color(0,0,0,1);
            pickCamera.fieldOfView = mainCamera.fieldOfView;
            pickCamera.farClipPlane = mainCamera.farClipPlane;
            pickCamera.nearClipPlane = mainCamera.nearClipPlane;
            pickCamera.orthographic = mainCamera.orthographic;
            if (mainCamera.orthographic)
                pickCamera.orthographicSize = mainCamera.orthographicSize;
            pickCameraGO.layer = cBufferId;
            var renderTexture = new RenderTexture(Screen.width,Screen.height,24);
            pickCamera.targetTexture = renderTexture;
            Loom.Instance.InvokeUdpate("_UCHART_CUBFFER_UPDATE",() =>
            {
                GameObject.Find("Canvas/RawImage").GetComponent<RawImage>().texture = pickCamera.targetTexture;
                pickCameraGO.transform.position = mainCameraTransform.position;
                pickCameraGO.transform.eulerAngles = mainCameraTransform.eulerAngles;
            });
        }

        private Color IndexToColor( int index )
        {
            int colorR = 0, colorG = 0, colorB = 0;
            colorR = index / (256 * 2);
            colorG = index / 256 % 256;
            colorB = index % 256;
            return new Color(colorR,colorG,colorB,1);
        }
    }
}