
using UnityEngine;

namespace UChart.Example
{
    public class Grid3dExample : UnityEngine.MonoBehaviour
    {
        public void OnGUI()
        {
            if( GUILayout.Button("Generate Grid3D") )
            {
                this.GetComponent<Grid3D>().Draw();
            }
        }
    }
}