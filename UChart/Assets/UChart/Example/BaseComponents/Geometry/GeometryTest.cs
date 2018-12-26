using UnityEngine;

namespace UChart.Test
{
    public class GeometryTest : MonoBehaviour
    {
        private void OnGUI()
        {
            if( GUILayout.Button("Generate CONE"))
            {
                var meshFilter = this.GetComponent<MeshFilter>();
                var meshRenderer = this.GetComponent<MeshRenderer>();

                Mesh mesh = new Mesh();
                mesh.name = "CONE";

                var cone = this.GetComponent<Cone>();
                cone.FillGeometry();
                cone.geometryBuffer.FillMesh(mesh,MeshTopology.Triangles);

                meshFilter.mesh = mesh;
                meshRenderer.material = new Material(Shader.Find("Standard"));
            }
        }
    }
}