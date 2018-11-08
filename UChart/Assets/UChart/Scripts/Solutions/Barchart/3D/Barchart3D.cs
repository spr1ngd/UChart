
using UnityEngine;

namespace UChart.Barchart
{
    public class Barchart3D : Barchart
    {
        public Material barMaterial = null;

        [Range(1,20)]
        public int xCount = 10;

        [Range(1,20)]
        public int yCount = 10;

        private float xOffset = 0.8f;

        private float yOffset = 0.8f;

        private float barWidth = 1.0f;
        
        public void Execute()
        {
            for( int x = 0 ; x < xCount;x++ )
            {
                for( int y = 0 ; y < yCount;y++ )
                {
                    Vector3 barPos = Vector3.zero;
                    barPos += new Vector3(x * barWidth + (x-1)*xOffset,0,y*barWidth + (y -1)*yOffset);
                    CreateBar(barPos);
                }
            }
        }

        protected override void CreateBar(Vector3 position)
        {
            GameObject bar3D = new GameObject("Bar3D");
            bar3D.hideFlags = HideFlags.HideInHierarchy;
            bar3D.transform.position = position;
            var bar = bar3D.AddComponent<Bar3D>();
            bar.material = this.barMaterial;
            bar.barWidth = this.barWidth;
            float value = Random.Range(0, 5);
            bar.Generate(new Vector3(barWidth,value,barWidth));
            bar.color = Color.green * Mathf.Abs(5 - value) / 5 + Color.red * value / 5;
            bar.alpha = 0.5f;
        }
    }
}