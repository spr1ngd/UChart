
using UnityEngine;

namespace UChart.Polygon
{
    public class Polygon 
    {
        public Color defaultColor = Color.white;

        public Vector3 size = Vector3.one;

        public PolygonAnchor anchor = PolygonAnchor.Center;

        protected Vector3[] vertices = null;

        protected int[] triangles = null;

        protected Color[] colors = null;

        public Mesh Create(string name)
        {
            Mesh mesh = new Mesh();
            mesh.name = name;
            CreateMesh(mesh);
            mesh.vertices = this.vertices;
            mesh.triangles = this.triangles;
            //mesh.RecalculateBounds();
            //mesh.RecalculateNormals();
            //mesh.RecalculateTangents();
            return mesh;
        }

        protected virtual void CreateMesh(Mesh mesh)
        {
            if( vertices.Length == 0 || triangles.Length == 0 )
                throw new UChartException("Please override method in subclass.");
        }
    }
}