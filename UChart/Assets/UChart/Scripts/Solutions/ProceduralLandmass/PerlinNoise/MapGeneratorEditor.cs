
using UnityEditor;
using UnityEngine;

namespace UChart.PL
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MapGenerator generator = target as MapGenerator;
            if(DrawDefaultInspector())
            {
                if(generator.autoUpdate)
                {
                    generator.Generate();
                }
            }
            if(GUILayout.Button("Generate"))
            {
                generator.Generate();
            }
        }
    }
}