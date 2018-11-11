
using UnityEngine;

namespace UChart.Example
{
    public class VertexReplaceProgram : MonoBehaviour
    {
        public GameObject target = null;

        private void Update()
        {
            //if (GUILayout.Button("Render color for circle"))
            {
                var meshFilter = target.GetComponent<MeshFilter>();
                var meshRenderer = target.GetComponent<MeshRenderer>();

                int vertexCount = meshFilter.mesh.vertexCount;
                Color[] colors = new Color[vertexCount];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = new Vector4(Random.Range(0,1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f),0.5f);
                }
                meshFilter.mesh.colors = colors;
            }
        }
    }
}