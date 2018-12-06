
using UnityEngine;

namespace UChart.Example
{
    public class LineTest : MonoBehaviour
    {
        private Line3D line3D = null;

        public void OnGUI()
        {
            if(GUILayout.Button("Generate Line"))
            {
                line3D = this.GetComponent<Line3D>();
                line3D.Draw();
            }
        }
    }
}