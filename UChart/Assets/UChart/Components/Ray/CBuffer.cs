
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    public class CBuffer
    {
        private static int CBufferId = UChart.uchartLayer;
        private static List<Color> CBuffers = new List<Color>();

        private int cBufferId;

        public CBuffer(Mesh mesh,Shader shader)
        {
            this.cBufferId = CBufferId--;
            this.CreateModel(mesh,new Material(shader));
            this.CreateCamera();
        }

        public CBuffer(Mesh mesh,Material material)
        {
            this.CreateModel(mesh,material);
            this.CreateCamera();
        }

        private void CreateModel( Mesh pickMesh , Material pickMaterial)
        {
            GameObject pickGameObject = new GameObject("UCHART_PCIK_GAMEOBJECT_"+ this.cBufferId);
            pickGameObject.hideFlags = HideFlags.HideInHierarchy;
            var pickMeshFilter = pickGameObject.AddComponent<MeshFilter>();
            pickMeshFilter.mesh = pickMesh;
            var pickRender = pickGameObject.AddComponent<MeshRenderer>();
            pickRender.material = pickMaterial;
            pickGameObject.layer = cBufferId;
        }

        private void CreateCamera()
        {
            GameObject pickCameraGO = new GameObject("UCHART_PICK_CAMERA");
            pickCameraGO.transform.position = Camera.main.transform.position;
            pickCameraGO.transform.eulerAngles = Camera.main.transform.eulerAngles;
            pickCameraGO.hideFlags = HideFlags.HideInHierarchy;
            var pickCamera = pickCameraGO.AddComponent<Camera>();
            pickCamera.cullingMask = 1 << cBufferId;
            pickCamera.clearFlags = CameraClearFlags.Color;
            pickCamera.backgroundColor = new Color(0,0,0,1);
            pickCameraGO.layer = cBufferId;
            var renderTexture = new RenderTexture(Screen.width,Screen.height,24);
            pickCamera.targetTexture = renderTexture;
        }

    }
}