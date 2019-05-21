
using UnityEngine;

namespace UChart.Wireframe
{ 
    public class WireframeModel : MonoBehaviour
    {
        private Mesh mesh;

        private void Awake()
        {
            mesh = this.GetComponent<MeshFilter>().mesh;

            var triangles = mesh.triangles;
            var vertices = mesh.vertices;

            for(int i = 0; i < triangles.Length; i+=3 )
            {

            }
        }
    }
}