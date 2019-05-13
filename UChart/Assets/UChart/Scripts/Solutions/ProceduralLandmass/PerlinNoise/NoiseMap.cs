
using UnityEngine;

namespace UChart.PL
{
    public static class NoiseMap
    {
        public static float[,] GenerateNoiseMap( int width,int height,float scale )
        {
            float[,] noiseMap = new float[width,height];
            if(scale <= 0)
                scale = 0.0001f;
            for(int x = 0; x < width; x++)
            {
                for( int y = 0;y < height;y++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;
                    noiseMap[x,y] = Mathf.PerlinNoise(sampleX,sampleY);
                }
            }
            return noiseMap;
        }
    }
}