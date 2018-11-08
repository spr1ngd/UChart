
using UnityEngine;
using UChart.Polygon;

namespace UChart.Scatter
{
    public class Scatter3D : Scatter
    {
        private Material m_material;

        [HideInInspector]
        public Material material
        {
            get { return m_material; }
            set { m_material = value; }
        }

        public Mesh mesh = null;

        private MeshRenderer m_meshRenderer = null;

        public override void Generate(Vector3 size)
        {
            var meshFilter = myGameobject.AddComponent<MeshFilter>();
            m_meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            //Polygon.Polygon mesh = new Cube
            //{
            //    size = size,
            //    anchor = PolygonAnchor.Bottom
            //};
            //meshFilter.mesh = mesh.Create("scatter3d_mesh");
            meshFilter.mesh = this.mesh;
            m_meshRenderer.material = material;
            myGameobject.AddComponent<MeshCollider>();
            myGameobject.AddComponent<Anim4Point2D_01>();
        }

        #region properties && events

        protected override void SetColor(Color color)
        {
            m_meshRenderer.material.SetColor("_Color",color);
        }

        protected override void SetAlpha(float alpha)
        {
            //m_meshRenderer.material.SetFloat("_Alpha",alpha);
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