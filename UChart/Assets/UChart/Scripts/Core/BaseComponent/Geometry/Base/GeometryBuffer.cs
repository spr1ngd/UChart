
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
            m_vertices.Add(vertexBuffer.pos);
            m_colors.Add(vertexBuffer.color);
        }

        public void AddVertex( Vector3 pos ,Color color)
        {
            m_vertices.Add(pos);
            m_colors.Add(color);
        }

        // public void AddVertex( Vector3 pos, Color color ,Vector2 uv, Vector3 normal , Vector4 tangent )
        // {

        // }

        // public void AddLine( VertexBuffer[] vertexBuffers )
        // {
            
        // }

        public void AddQuad( VertexBuffer[] vertexBuffers )
        {
            if( vertexBuffers.Length != 4 )
                throw new UChartGeometryException("Quad mush be 4 vertex,but you provide "+ vertexBuffers.Length +" vertex");
            int vertexIndexHead = m_vertices.Count;
            for( var i = 0 ; i < vertexBuffers.Length;i++ )
            {
                VertexBuffer vertexBuffer = vertexBuffers[i];
                this.AddVertex(vertexBuffer);
            }
            this.AddTriangle(new int[]
            {
                0 + vertexIndexHead,
                3 + vertexIndexHead,
                2 + vertexIndexHead
            });
            this.AddTriangle(new int[]
            {
                2 + vertexIndexHead,
                1 + vertexIndexHead,
                0 + vertexIndexHead
            });
        }

        public void AddTriangle( int[] triangleIndices )
        {
            for( var i = 0 ;i < triangleIndices.Length;i++ )
            {
                m_indices.Add(triangleIndices[i]);
            }
        }

        public void FillMesh( Mesh mesh , MeshTopology topology )
        {
            if( null == mesh )
                throw new UChartGeometryException("mesh is null.");
            UnityEngine.Debug.Log("<color=red>Vertices : ["+this.vertices.Length+"] </color><color=green>Indices : ["+this.indices.Length+"]</color>");
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
            int vertexIndexHead = m_vertices.Count;
            foreach( var vertex in geometryBuffer.vertices )
                this.m_vertices.Add(vertex);
            foreach( var color in geometryBuffer.colors )
                this.m_colors.Add(color);           
            for( var i = 0; i < geometryBuffer.indices.Length;i++ )
                this.m_indices.Add(vertexIndexHead + geometryBuffer.indices[i]);
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