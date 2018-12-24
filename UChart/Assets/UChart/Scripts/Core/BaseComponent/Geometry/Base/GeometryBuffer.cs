
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    public class GeometryBuffer
    {
        private List<int> m_indices = new List<int>();
        public int[] indices 
        {
            get
            {
                if( null == m_indices )
                    throw new UChartGeometryException("indices of geometry can't be null.");
                return m_indices.ToArray();
            }
        }

        private List<Vector3> m_vertices = new List<Vector3>();
        public Vector3[] vertices 
        {
            get
            {
                if( null == m_vertices )
                    throw new UChartGeometryException("vertices of geometry can't be null.");
                return m_vertices.ToArray();
            }
        }       

        private List<Color> m_colors = new List<Color>();
        public Color[] colors
        {
            get
            {
                if( null == m_colors )
                    throw new UChartGeometryException("colors of geometry can't be null.");
                return m_colors.ToArray();
            }
        }

        // private List<Vector2> m_uvs = null;
        // public List<Vector2> uvs 
        // {
        //     get{return m_uvs;}
        // }

        // private List<Vector3> m_normals = null;
        // public List<Vector3> normal
        // {
        //     get{return m_normals;}
        // }

        public void AddVertex( VertexBuffer vertexBuffer )
        {

        }

        public void AddVertex( Vector3 pos ,Color color)
        {
            m_vertices.Add(pos);
            m_colors.Add(color);
            m_indices.Add(m_indices.Count);
        }

        public void AddVertex( Vector3 pos, Color color ,Vector2 uv, Vector3 normal , Vector4 tangent )
        {

        }

        public void AddLine( VertexBuffer[] vertexBuffers )
        {
            
        }

        public void AddTriangle( int[] triangleIndices )
        {
            
        }

        public void FillMesh( Mesh mesh , MeshTopology topology )
        {
            if( null == mesh )
                throw new UChartGeometryException("mesh is null.");
            mesh.vertices = this.vertices;
            mesh.colors = this.colors;
            mesh.SetIndices(this.indices,topology,0);
        }

        public void CombineGeometry( Mesh mesh , MeshTopology topology )
        {
            if( null == mesh )
                throw new UChartGeometryException("mesh is null.");
            // TODO: 将数据导出加入到当前GeometryBuffer
        }

        public void CombineGeometry( GeometryBuffer geometryBuffer )
        {
            if( null == geometryBuffer )
                throw new UChartGeometryException("geometryBuffer is null.");
            // TODO: 将数据导出加入到当前GeometryBuffer
        }

        public void Clear()
        {
            m_indices.Clear();
            m_vertices.Clear();
            m_colors.Clear();
            // m_uvs.Clear();
            // m_normals.Clear();
        }
    }
}   