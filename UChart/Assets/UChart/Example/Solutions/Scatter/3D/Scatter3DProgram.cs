

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
                //GameObject scatter = new GameObject("scatter3D");
                //scatter.transform.position = Vector3.zero;
                //scatter.AddComponent<MeshFilter>().mesh = new Quad().Create("scatter3d");
                //scatter.AddComponent<MeshRenderer>().material = material;
            }
        }
    }
}