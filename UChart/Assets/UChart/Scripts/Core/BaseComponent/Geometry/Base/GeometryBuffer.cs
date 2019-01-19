
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

        private List<Vector2> m_uvs = new List<Vector2>();
        public List<Vector2> uvs 
        {
            get{return m_uvs;}
        }

        // private List<Vector3> m_normals = null;
        // public List<Vector3> normal
        // {
        //     get{return m_normals;}
        // }

        public GeometryBuffer()
        {

        }

        public void AddVertex( VertexBuffer vertexBuffer )
        {
            m_vertices.Add(vertexBuffer.pos);
            m_colors.Add(vertexBuffer.color);
            m_uvs.Add(vertexBuffer.uv);
        }

        public void AddVertex( Vector3 pos ,Color color)
        {
            m_vertices.Add(pos);
            m_colors.Add(color);
        }

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

        /// <summary>
        /// add quad triangle indices for geometry
        /// </summary>
        /// <param name="quadIndices"></param>
        public void AddQuad( int[] quadIndices )
        {
            if(quadIndices.Length != 4)
                throw new UChartException("quadIndices length mush be 4.");
            //Debug.Log(string.Format("ADD QUAD [{0},{1},{2},{3}]",quadIndices[0],quadIndices[1],quadIndices[2],quadIndices[3]));
            AddTriangle(new int[] { quadIndices[0],quadIndices[1],quadIndices[2]});
            AddTriangle(new int[] { quadIndices[2],quadIndices[3],quadIndices[0] });
        }

        /// <summary>
        /// Add a triangle indices for geometry.
        /// </summary>
        public void AddTriangle( int[] triangleIndices )
        {
            Debug.Log("add triangle : " + triangleIndices[0] + "," + triangleIndices[1] +"," + triangleIndices[2] );
            for( var i = 0 ;i < triangleIndices.Length;i++ )
                m_indices.Add(triangleIndices[i]);
        }

        /// <summary>
        /// FillMesh : add submesh for current mesh.
        /// </summary>
        public void FillMesh( Mesh mesh , MeshTopology topology )
        {
            if( null == mesh )
                throw new UChartGeometryException("mesh is null.");
            UnityEngine.Debug.Log("<color=red>Vertices : ["+this.vertices.Length+"] </color><color=green>Indices : ["+this.indices.Length+"]</color>");
            mesh.vertices = this.vertices;
            mesh.colors = this.colors;
            mesh.SetIndices(this.indices,topology,mesh.subMeshCount-1);
            mesh.subMeshCount++;           
        }

        /// <summary>
        /// CombineMesh : conbine destination mesh into current mesh.
        /// <summary>
        public void CombineGeometry( Mesh mesh , MeshTopology topology )
        {
            if( null == mesh )
                throw new UChartGeometryException("mesh is null.");
            var vertices = mesh.vertices;
            var colors = mesh.colors;
            var indices = mesh.GetIndices(0);
            var uvs = mesh.uv;

            int vertexIndexHead = m_vertices.Count;
            foreach( var vertex in vertices )
                this.m_vertices.Add(vertex);
            foreach( var color in colors )
                this.m_colors.Add(color);      
            foreach( var uv in uvs )
                this.m_uvs.Add(uv);     
            for( var i = 0; i < indices.Length;i++ )
                this.m_indices.Add(vertexIndexHead + indices[i]);           
        }

        /// <summary>
        /// CombineMesh : conbine destination mesh into current mesh.
        /// <summary>
        public void CombineGeometry( GeometryBuffer geometryBuffer )
        {
            if( null == geometryBuffer )
                throw new UChartGeometryException("geometryBuffer is null.");
            int vertexIndexHead = m_vertices.Count;
            foreach( var vertex in geometryBuffer.vertices )
                this.m_vertices.Add(vertex);
            foreach( var color in geometryBuffer.colors )
                this.m_colors.Add(color);           
            foreach( var uv in geometryBuffer.uvs )
                this.m_uvs.Add(uv);     
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

#region 关于GeometyBuffer额外的拓展

        public void AddCircle( Vector3 center,float radius,int smoothness ,Vector3 normal ,float percent = 1.0f)
        {
            float perRadian = Mathf.PI * 2 / smoothness;
            int smoothnessCount = (int)(percent * smoothness);

            for( int i = 0 ; i < smoothnessCount; i++)
            {
                float radian = perRadian * i;
                Vector3 vertex = new Vector3(Mathf.Sin( radian) * radius ,
                                            0,
                                            Mathf.Cos( radian) * radius ) + center;
                //Debug.Log(string.Format("CENTER :[{0}] RADIUS [{1}] POSITION :[{2}]",center,radius,vertex));
                this.AddVertex(vertex,Color.red);
            }
        }

#endregion
    }
}   