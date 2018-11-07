
using UnityEngine;

namespace UChart.Barchart
{
    public class Bar3DProgram : MonoBehaviour
    {
        private void OnGUI()
        {
            if(GUILayout.Button("Show Bar3D"))
            {
                this.GetComponent<Barchart3D>().Execute();
            }
        }
    }
}