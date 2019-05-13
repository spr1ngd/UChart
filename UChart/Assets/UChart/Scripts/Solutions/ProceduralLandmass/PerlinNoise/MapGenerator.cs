
using UnityEngine;

namespace UChart.PL
{
    public class MapGenerator : MonoBehaviour
    {
        public int width;
        public int height;
        public float scale;
        public bool autoUpdate = false;
        public MapDisplay display;

        public void Generate()
        {
            float[,] noise = NoiseMap.GenerateNoiseMap(width,height,scale);
            display.DrawNoiseMap(noise);
        }
    }
}