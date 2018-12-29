
using UnityEngine;

namespace UChart
{
    public class TextureBarchart : Barchart3D
    {
        public Texture2D texture;

        private void AnalysisTexture()
        {
            // TODO: 解析texture并赋值到二维数组即可
            if( null == texture )
            {
                Debug.Log("texture can't be null,<color=red>please assign texture asset on inspector window.</color>");
                return;
            }
            int width = texture.width;
            int height = texture.height;
            datas = new float[width,height];

            float weightNumber = 256 *256 *256 / 50000.0f;
            for( int x = 0 ; x < width; x++ )
            {
                for( int y = 0 ; y < height;y++ )
                {
                    Color pixel = texture.GetPixel(x,y);
                    float weight = (pixel.r * 256 + pixel.g *256 + pixel.b * 256) / weightNumber ;
                    datas.SetValue(weight,x,y);
                }
            }
        }

        public override void Draw()
        {
            this.AnalysisTexture();
            base.Draw();
        }
    }
}