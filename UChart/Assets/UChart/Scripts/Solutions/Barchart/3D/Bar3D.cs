
using UnityEngine;

namespace UChart.Barchart
{
    public class Bar3D : Bar
    {
        private Material m_material;

        public Material material
        {
            get{return m_material;}
            set{m_material = value;}
        }

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
    }
}