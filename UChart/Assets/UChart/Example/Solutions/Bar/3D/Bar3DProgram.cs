
using UnityEngine;

namespace UChart
{
    public class Bar3DProgram : MonoBehaviour
    {
        public Grid3D grid3D;
        public Axis3D axis3D;

        private void Start()
        {
            grid3D.Draw();
            axis3D.OnDrawMesh();
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Show Bar3D"))
            {
                this.GetComponent<Barchart3D>().Draw();
            }
        }
    }
}