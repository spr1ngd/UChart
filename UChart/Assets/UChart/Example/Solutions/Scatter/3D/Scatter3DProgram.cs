

using UChart.Polygon;
using UnityEngine;

namespace UChart.Scatter
{
    public class Scatter3DProgram : MonoBehaviour
    {
        public Material material;

        private void OnGUI()
        {
            if(GUILayout.Button("Show Scatter3D"))
            {
                this.GetComponent<ScatterGraph3D>().Execute();
            }
        }
    }
}