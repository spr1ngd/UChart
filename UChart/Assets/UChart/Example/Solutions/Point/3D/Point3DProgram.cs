

using UnityEngine;

namespace UChart.Example
{
    public class Point3DProgram : MonoBehaviour
    {
        public MeshRenderer simplePoint3d = null;
        public MeshRenderer adaptivePoint3d = null;

        private void Awake()
        {
            adaptivePoint3d.material.SetFloatArray("_PointSize",new float[]{0.33f});
        }
    }
}