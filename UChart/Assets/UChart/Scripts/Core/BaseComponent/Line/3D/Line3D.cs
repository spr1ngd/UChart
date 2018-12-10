
using UnityEngine;

namespace UChart
{
    public class Line3D : Line
    {
        public Material material = null;

        public override void Draw()
        {
            GameObject line3D = new GameObject("Line3D");
#if UCHART_DEBUG
            //line3D.hideFlags = HideFlags.HideInHierarchy;
#endif

            MeshFilter meshFilter = line3D.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = line3D.AddComponent<MeshRenderer>();
            Mesh mesh = new Mesh() { name = "LineMesh"};

            // todo vertices
            // todo color
            Vector3[] vertices = new Vector3[3]{Vector3.zero,Vector3.one,new Vector3(10,10,0)};
            Color[] colors = new Color[3]{Color.red,Color.green,Color.blue};

            // vertices[0] = start;
            // vertices[1] = end;

            // colors[0] = Color.red;
            // colors[1] = Color.green;

            mesh.vertices = vertices;
            mesh.SetIndices(new int[] { 0,1 , 2},MeshTopology.LineStrip,0);
            mesh.colors = colors;

            meshFilter.mesh = mesh;
            meshRenderer.material = material;
        }

        protected override void SetWidth(float width)
        {
            base.SetWidth(width);
        }
    }
}