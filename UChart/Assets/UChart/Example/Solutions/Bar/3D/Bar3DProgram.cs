
using UnityEngine;

namespace UChart
{
    public class Bar3DProgram : MonoBehaviour
    {
        public TextureBarchart textureBarchart = null;

        public Grid3D grid3D;
        public Axis3D axis3D;

        public bool drawaGrid;
        public bool drawAxis;

        [Header("SIMULATION DATA SETTING")]
        public int xCount;
        public int yCount;

        private void Start()
        {
            if( null != grid3D && drawaGrid) grid3D.Draw();
            if( null != axis3D && drawAxis) axis3D.OnDrawMesh();
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Show Bar3D"))
            {
                // float[,] datas = new float[xCount,yCount];
                // for( int x = 0 ; x < xCount;x++ )
                // {
                //     for( int y = 0 ; y < yCount;y++ )
                //     {
                //         float height = Random.Range(1,10);
                //         datas.SetValue(height,x,y);
                //     }
                // }
                // var barchart = this.GetComponent<Barchart3D>();
                // barchart.datas = datas;
                // barchart.Draw();

                textureBarchart.Draw();
            }
        }
    }
}