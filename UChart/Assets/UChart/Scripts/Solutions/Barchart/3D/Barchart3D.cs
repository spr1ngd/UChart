
using UnityEngine;

namespace UChart
{
	public class Barchart3D : Barchart
    {
		[Header("BARCHART SETTING")]
		public int xCount = 10;
		public int yCount = 10;

		public float barOffset = 0.0f;

		public float barWidth = 2.0f;

		[Header("BARCHART STYLE")]
		public bool drawOutline = false;		

		public Color barColor = Color.blue;

		public override void Draw()
		{
			GeometryBuffer buffer = new GeometryBuffer ();
			var meshFilter = myGameobject.AddComponent<MeshFilter> ();
			var meshRenderer = myGameobject.AddComponent<MeshRenderer>();

			float halfWidth = barWidth / 2.0f;

			for( int x = 0 ; x < xCount; x++ )
			{
				for( int y = 0 ; y < yCount;y++ )
				{
					var pos = new Vector3( halfWidth + (x-1) * barWidth,0, halfWidth + (y-1)*barWidth );
					buffer.AddVertex(pos,barColor);
				}
			}

			var mesh = new Mesh();
			mesh.name = "BARCHRAT";

			buffer.FillMesh(mesh,MeshTopology.Points);
			meshFilter.mesh = mesh;
			meshRenderer.material = new Material(Shader.Find("UChart/Vertex/VertexColor"));
		}
    }
}