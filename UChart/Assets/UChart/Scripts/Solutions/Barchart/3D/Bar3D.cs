
using UnityEngine;

namespace UChart
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

        private MeshRenderer m_meshRenderer = null;
    
        public void Generate(Vector3 size)
        {
            
        }

        #region properties && events

        protected override void SetColor(Color color)
        {
            // m_meshRenderer.material.SetColor("_Color", color);
        }

        protected override void SetAlpha(float alpha)
        {
            // m_meshRenderer.material.SetFloat("_Alpha",alpha);
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