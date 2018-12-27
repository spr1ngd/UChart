
using UnityEngine;

namespace UChart
{
    public class Geometry
    {
        protected GeometryBuffer m_geometryBuffer = new GeometryBuffer();
        public GeometryBuffer geometryBuffer 
        {
            get{return m_geometryBuffer;}
        }

        public GeometryAnchor anchor = GeometryAnchor.Center;

        public virtual void FillGeometry()
        {

        }
    }
}