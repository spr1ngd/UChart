
using UnityEngine;

namespace UChart
{
    public class Axis3D : Axis
    {
        private MeshFilter m_meshFilter = null;
        private MeshRenderer m_meshRenderer = null;

        private Vector3[] aVertices;
        private Color[] aColors;
        private int[] aIndices;
        private int vIndex;

        private int indicesIndex = 0;

        private int coneIndex = 0 ;

        public override void OnDrawArrow()
        {
            GameObject arrow = new GameObject("__AXISARROW__");
            // arrow.hideFlags = HideFlags.HideInHierarchy;
            arrow.transform.SetParent(this.myTransform);

            var meshFilter = arrow.AddComponent<MeshFilter>();
            var meshRenderer = arrow.AddComponent<MeshRenderer>();

            Mesh arrowMesh = new Mesh();
            arrowMesh.name = "__AXIS3DARROW__";

            vIndex = 0;
            int vertexCount = (arrowSmooth + 2) * 3;
            aVertices = new Vector3[vertexCount];
            aColors = new Color[vertexCount];
            aIndices = new int[arrowSmooth * 6 * 3];

            Vector3 xBottom = transform.right * axisLenght * 1.1f;
            Vector3 xTop = xBottom + transform.right * arrowSize * 2;

            Vector3 yBottom = transform.up * axisLenght * 1.1f;
            Vector3 yTop = yBottom + transform.up * arrowSize * 2;

            Vector3 zBottom = transform.forward * axisLenght * 1.1f;      
            Vector3 zTop = zBottom + transform.forward * arrowSize * 2;

            DrawCone(transform.right,xBottom,xTop,xArrowColor);
            DrawCone(transform.up,yBottom,yTop,yArrowColor);
            DrawCone(transform.forward,zBottom,zTop,zArrowColor);

            // vertices
            arrowMesh.vertices = aVertices;
            // colors
            arrowMesh.colors = aColors;
            // indices
            arrowMesh.SetIndices(aIndices,MeshTopology.Triangles,0);
            arrowMesh.RecalculateNormals();
            meshFilter.mesh = arrowMesh;
            meshRenderer.material = new Material(Shader.Find("UChart/Axis/AxisArrow(Basic)"));
            coneIndex = 0;
        }

        private void DrawCone( Vector3 direction,Vector3 bottom,Vector3 top ,Color arrowColor )
        {
            aColors[vIndex] = arrowColor;
            aVertices[vIndex++] = bottom;
            aColors[vIndex] = arrowColor;
            aVertices[vIndex++] = top;
            float perAngle = 2 * Mathf.PI / arrowSmooth; 
            for( int i = 0 ; i < arrowSmooth;i++ )
            {
                float angle = i * perAngle;
                aColors[vIndex] = arrowColor;
                if( direction == transform.right )
                    aVertices[vIndex++] = bottom + new Vector3(0,Mathf.Cos(angle) * arrowSize, Mathf.Sin(angle) * arrowSize);
                else if(direction == transform.up)
                    aVertices[vIndex++] = bottom + new Vector3(Mathf.Sin(angle) * arrowSize, 0, Mathf.Cos(angle) * arrowSize);
                else if(direction == transform.forward)
                    aVertices[vIndex++] = bottom + new Vector3(Mathf.Sin(angle) * arrowSize,  Mathf.Cos(angle) * arrowSize,0);
            }

            for( int index = 2 + coneIndex * (arrowSmooth + 2); index < (coneIndex+1) * (arrowSmooth + 2) ; index++ ) //arrowSmooth + 2
            {               
                aIndices[indicesIndex++] = coneIndex * (arrowSmooth + 2);

                var tempIndex = index + 1;
                if( tempIndex > (coneIndex+1) * (arrowSmooth + 2) -1 )
                    tempIndex = tempIndex - arrowSmooth;
                aIndices[indicesIndex++] = tempIndex;

                aIndices[indicesIndex++] = index;
            }

            for( int index = 2 + coneIndex * (arrowSmooth + 2); index < (coneIndex+1) * (arrowSmooth + 2) ; index++ )
            {
                aIndices[indicesIndex++] = coneIndex * (arrowSmooth + 2) + 1;  

                aIndices[indicesIndex++] = index;
                var tempIndex = index + 1;
                if( tempIndex >  (coneIndex+1) * (arrowSmooth + 2) -1)
                    tempIndex = tempIndex - arrowSmooth;    

                aIndices[indicesIndex++] = tempIndex;
            }
            coneIndex++;
        }

        private Vector3[] veritces = null;
        private Color[] colors = null;
        private int[] indices = null;
        private int vertexIndex = 0;

        public override void OnDrawMesh()
        {
            m_meshFilter = myGameobject.AddComponent<MeshFilter>();
            m_meshRenderer = myGameobject.AddComponent<MeshRenderer>();

            float xOffset = axisLenght / xUnit;
            float yOffset = axisLenght / yUnit;
            float zOffset = axisLenght / zUnit;

            Mesh mesh = new Mesh();
            mesh.name = "__ASIX3D__";

            // TODO: 修改顶点数量
            int vertexCount = 6 + xUnit * 4 + yUnit * 4 + zUnit* 4;
            veritces = new Vector3[vertexCount];
            indices = new int[vertexCount];
            colors = new Color[vertexCount];
            vertexIndex = 0;
            // vertices 
            DrawLine(transform.position,transform.right * axisLenght * 1.1f,xArrowColor);
            DrawLine(transform.position,transform.up * axisLenght * 1.1f,yArrowColor);
            DrawLine(transform.position,transform.forward * axisLenght * 1.1f,zArrowColor);

            Vector3 origin = Vector3.zero;

            // x axis mesh
            for( int y = 1 ; y <= yUnit; y++ )
            {
                Vector3 start = origin + new Vector3(0,y * yOffset,0);
                Vector3 end = origin + new Vector3(0,y * yOffset,axisLenght);
                DrawLine(start,end,meshColor);
            }

            for( int z = 1 ; z <= zUnit; z++ )
            {
                Vector3 start = origin + new Vector3(0,0,z * zOffset);
                Vector3 end = origin + new Vector3(0,axisLenght,z * zOffset);
                DrawLine(start,end,meshColor);
            }

            // y axis mesh
            for( int x = 1 ; x <= xUnit; x++ )
            {
                Vector3 start = origin + new Vector3(x * xOffset,0,0);
                Vector3 end = origin + new Vector3(x * xOffset,0,axisLenght);
                DrawLine(start,end,meshColor);
            }

            for( int z = 1 ; z <= zUnit; z++ )
            {
                Vector3 start = origin + new Vector3(0,0,z * zOffset);
                Vector3 end = origin + new Vector3(axisLenght,0,z * zOffset);
                DrawLine(start,end,meshColor);
            }

            // z axis mesh
            for( int x = 1 ; x <= xUnit; x++ )
            {
                Vector3 start = origin + new Vector3(x * xOffset,0,0);
                Vector3 end = origin + new Vector3(x * xOffset,axisLenght,0);
                DrawLine(start,end,meshColor);
            }

            for( int y = 1 ; y <= yUnit; y++ )
            {
                Vector3 start = origin + new Vector3(0,y * yOffset,0);
                Vector3 end = origin + new Vector3(axisLenght,y * yOffset,0);
                DrawLine(start,end,meshColor);
            }

            // indices
            for( int i = 0 ; i < indices.Length;i++ )
                indices[i] = i;
            mesh.vertices = veritces;
            mesh.colors = colors;
            mesh.SetIndices(indices,MeshTopology.Lines,0);

            m_meshFilter.mesh = mesh;
            m_meshRenderer.material = new Material(Shader.Find("UChart/Axis/Axis(Basic)"));

            OnDrawArrow();
        }

        private void DrawLine(Vector3 start,Vector3 end,Color lineColor)
        {
            colors[vertexIndex] = lineColor;
            veritces[vertexIndex++] = start;
            colors[vertexIndex] = lineColor;
            veritces[vertexIndex++] = end;
        }
    }
}