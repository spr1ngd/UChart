
using UnityEngine;

namespace UChart.Example
{
    public class PieProgram : MonoBehaviour
    {
        public Pie2D pie2D;

        private void OnGUI()
        {
            if( GUILayout.Button("Draw Pie2D") )
            {
                pie2D.Draw();
            }

            if( GUILayout.Button("Draw Pie3D") )
            {
                
            }
        }
    }
}