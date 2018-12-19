using UnityEngine;

namespace UChart.Test
{
    public class GeometryTest : MonoBehaviour
    {
        private void OnGUI()
        {
            if( GUILayout.Button("Generate CONE"))
            {
                var meshFilter = this.GetComponent<MeshFilter>();
                var meshRenderer = this.GetComponent<MeshRenderer>();

                
            }
        }
    }
}