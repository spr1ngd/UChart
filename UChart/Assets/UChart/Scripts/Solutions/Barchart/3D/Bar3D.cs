
using UnityEngine;
using UChart.Polygon;

namespace UChart.Barchart
{
    public class Bar3D : Bar
    {
        private Material m_material;

        [HideInInspector]
        public Material material
        {
            get{return m_material;}
            set{m_material = value;}
        }

        public void Generate(Vector3 size)
        {
            var meshFilter = myGameobject.AddComponent<MeshFilter>();
            var meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            Cube mesh = new Cube();
            mesh.size = size;      
            mesh.anchor = PolygonAnchor.Bottom;    
            meshFilter.mesh = mesh.Create("bar3d_mesh");
            meshRenderer.material = material;
            myGameobject.AddComponent<MeshCollider>();
            myGameobject.AddComponent<Anim4Point2D_01>();
        }

        #region properties && events

        protected override void SetColor(Color color)
        {
            
        }

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            // TODO: trigger the axis event
        }

        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            // TODO: trigger exit the axis event
        }

        protected override void OnMouseUpAsButton()
        {
            base.OnMouseUpAsButton();
            // TODO: trigger to show the information tip
        }

        #endregion

    }
}