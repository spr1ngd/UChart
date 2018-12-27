
using UnityEngine;

namespace UChart
{
	[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
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
			var meshFilter = myGameobject.GetComponent<MeshFilter> ();
			var meshRenderer = myGameobject.GetComponent<MeshRenderer>();

			float halfWidth = barWidth / 2.0f;

			for( int x = 0 ; x < xCount; x++ )
			{
				for( int y = 0 ; y < yCount;y++ )
				{
					var pos = new Vector3( halfWidth + (x-1) * barWidth,0, halfWidth + (y-1)*barWidth );

					float height = Random.Range(1,10);
					CubeGeometry cube = new CubeGeometry();
					cube.center = pos;
					cube.length = barWidth;
					cube.width = barWidth;
					cube.height = height;
					cube.anchor = GeometryAnchor.Bottom;
					cube.color = Color.blue;
					if ( height > 5 && height < 8 )
					{
						cube.color = Color.yellow;
					}
					if(height >= 8)
					{
						cube.color = Color.red;
					}					
					cube.FillGeometry();
					buffer.CombineGeometry(cube.geometryBuffer);
				}
			}

			var mesh = new Mesh();
			mesh.name = "BARCHRAT";

			buffer.FillMesh(mesh,MeshTopology.Triangles);
			meshFilter.mesh = mesh;
			// meshRenderer.material = new Material(Shader.Find("UChart/Barchart/Barchart(Basic)"));

			if( drawOutline )
			{
				this.DrawOutline();
			}
		}

		public override void DrawOutline()
		{
			GameObject outline = new GameObject("__BARCHARTOUTLINE___");
			outline.hideFlags = HideFlags.HideInHierarchy;
			outline.transform.SetParent(this.transform);
			var meshFileter = outline.AddComponent<MeshFilter>();
			var meshRenderer = outline.AddComponent<MeshRenderer>();

			var sharedMesh = myGameobject.GetComponent<MeshFilter> ().sharedMesh;
			Mesh mesh = new Mesh();
			mesh.name = "__BARCHARTOUTLINEMESH___";
			mesh.vertices = sharedMesh.vertices;
			mesh.colors = sharedMesh.colors;
			for( int i = 0 ; i < mesh.colors.Length;i++ )
				mesh.colors[i] = Color.black;
			mesh.SetIndices(sharedMesh.GetIndices(0),MeshTopology.Lines,0);
			meshFileter.mesh = mesh;
			meshRenderer.material = new Material(Shader.Find("UChart/Barchart/Barchart(Basic)"));
		}
    }
}