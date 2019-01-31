
using UnityEngine;

namespace UChart
{
    // TODO: 支持rotation && scale
    // TODO: 支持颜色自定义
    public class UChartMeshMonitor : MonoBehaviour
    {
        public bool selectShow = true;
        public bool showNormal = true;
        public float normalLength = 0.2f;
        public bool colorful = true;

        private void OnDrawGizmos()
        {
            if(selectShow)
                return;
            OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            MeshFilter[] meshFilters = this.gameObject.GetComponentsInChildren<MeshFilter>();

            if(null == meshFilters)
                return;

            foreach(var meshFilter in meshFilters)
            {
                if(null == meshFilter.sharedMesh)
                    continue;
                var mesh = meshFilter.sharedMesh;
                var normals = mesh.normals;
                var vertices = mesh.vertices;
                for(int i = 0; i < normals.Length; i++)
                {
                    var normal = normals[i];
                    var from = vertices[i] + meshFilter.transform.position;
                    var to = from + normal * normalLength;
                    if(showNormal)
                    {
                        if(colorful)
                            Gizmos.color = Color.Lerp(Color.red,Color.green,i * 1.0f / normals.Length);
                        Gizmos.DrawLine(from,to);
                    }
                }
            }
        }
    }
}