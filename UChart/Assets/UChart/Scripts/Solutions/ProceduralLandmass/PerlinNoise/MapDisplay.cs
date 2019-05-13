
using UnityEngine;

namespace UChart.PL
{
    public class MapDisplay :MonoBehaviour
    {
        public Renderer textureRenderer;

        public void DrawNoiseMap( float[,] noiseMap )
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);

            Texture2D texture = new Texture2D(width,height);
            Color[] colors = new Color[width*height];
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    colors[x * height + y] = Color.Lerp(Color.black,Color.white,noiseMap[x,y]);
                }
            }
            texture.SetPixels(colors);
            texture.Apply();
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(width,1,height);
        }
    }
}