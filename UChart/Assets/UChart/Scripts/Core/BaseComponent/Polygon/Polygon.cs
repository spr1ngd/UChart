
using UnityEngine;

namespace UChart.Polygon
{
    public class Polygon 
    {
        public Color defaultColor = Color.white;

        protected Vector3[] vertices = null;

        protected int[] triangles = null;

        public Mesh Create(string name)
        {
            Mesh mesh = new Mesh();
            mesh.name = name;
            CreateMesh(mesh);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            return mesh;
        }

        protected virtual void CreateMesh(Mesh mesh)
        {
            throw new UChartException("Please override method in subclass.");
        }
    }
}