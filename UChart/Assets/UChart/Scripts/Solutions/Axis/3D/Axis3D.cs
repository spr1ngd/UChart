
using UnityEngine;

namespace UChart
{
    public class Axis3D : Axis
    {
        public override void OnDrawMesh()
        {
            var meshFilter = myGameobject.AddComponent<MeshFilter>();
            var meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();
            mesh.name = "__ASIX3D__";

            Vector3[] veritces = new Vector3[6];
            int[] indices = new int[6];
            // vertices
            veritces[0] = transform.position;
            veritces[1] = transform.right * axisLenght;
            veritces[2] = transform.position;
            veritces[3] = transform.up * axisLenght;
            veritces[4] = transform.position;
            veritces[5] = transform.forward * axisLenght;
            // indices
            for( int i = 0 ; i < indices.Length;i++ )
                indices[i] = i;

            mesh.vertices = veritces;
            mesh.SetIndices(indices,MeshTopology.Lines,0);

            meshFilter.mesh = mesh;
            meshRenderer.material = new Material(Shader.Find("Standard"));
        }
    }
}