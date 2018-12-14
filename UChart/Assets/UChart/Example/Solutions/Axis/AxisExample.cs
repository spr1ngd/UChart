
using UnityEngine;

namespace UChart.Example
{
    public class AxisExample : MonoBehaviour
    {
        private void OnGUI()
        {
            if( GUILayout.Button("Generator Axis") )
            {
                transform.GetComponent<Axis3D>().OnDrawMesh();
            }
        }
    }
}