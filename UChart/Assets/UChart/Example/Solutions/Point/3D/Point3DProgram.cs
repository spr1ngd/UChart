

using UnityEngine;

namespace UChart.Example
{
    [RequireComponent(typeof(Monitor))]
    public class Point3DProgram : MonoBehaviour
    {
        public MeshRenderer simplePoint3d = null;
        public MeshRenderer adaptivePoint3d = null;

        private Monitor m_monitor = null;

        private void Awake()
        {
            m_monitor = this.GetComponent<Monitor>();
            adaptivePoint3d.material.SetFloatArray("_PointSize",new float[]{0.33f});
        }

        private void Update()
        {
            m_monitor.AddMonitor("PointSize",adaptivePoint3d.material.GetFloatArray("_PointSize")[0].ToString("E"));
        }
    }
}