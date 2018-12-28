
using UnityEngine;

namespace UChart
{
	[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
	public class Barchart3D : Barchart
    {
		public const float F_BAR_MAXCOUNT = 40.0f;
		public const int I_BAR_MAXCOUNT = 40;

		[Header("BARCHART SETTING")]
		public int xCount = 10;
		public int yCount = 10;

		public float barOffset = 0.0f;

		public float barWidth = 2.0f;

		[Header("BARCHART STYLE")]
		public bool drawOutline = false;		
		public Color outlineColor = Color.blue;

		public override void Draw()
		{
			GeometryBuffer buffer = new GeometryBuffer ();
			var meshFilter = myGameobject.GetComponent<MeshFilter> ();
			var meshRenderer = myGameobject.GetComponent<MeshRenderer>();

			// TODO: 处理大量柱状图时需要处理顶点拆分为多个 submesh
			// TODO: 默认处理40*40

			int xSubmeshCount = Mathf.CeilToInt(xCount / F_BAR_MAXCOUNT);
			int ySubmeshCount = Mathf.CeilToInt(yCount / F_BAR_MAXCOUNT);

			int submeshIndex = 0;
			for( int x = 0 ; x < xSubmeshCount ; x++ )
			{
				for( int y = 0; y < ySubmeshCount; y++ )
				{
					var xNumber = x * I_BAR_MAXCOUNT;
					var yNumber = y * I_BAR_MAXCOUNT;
					// var startPos = new Vector3( x  );
					this.DrawPerBarchart(xNumber , yNumber ,Vector3.zero, submeshIndex,ref buffer);
				}
			}

			var mesh = new Mesh();
			mesh.name = "BARCHRAT";
			buffer.FillMesh(mesh,MeshTopology.Triangles);
			meshFilter.mesh = mesh;
			mesh.RecalculateNormals();
			mesh.RecalculateTangents();

			if( drawOutline )
			{
				buffer.FillMesh(mesh,MeshTopology.Lines,1);			
				meshFilter.mesh = mesh;
			}
		}

		private void DrawPerBarchart( int xCount ,int yCount ,Vector3 startPos , int submeshIndex,ref GeometryBuffer buffer)
		{
			float halfWidth = barWidth / 2.0f;

			for( int x = 0 ; x < xCount; x++ )
			{
				for( int y = 0 ; y < yCount;y++ )
				{
					var pos = startPos + new Vector3( halfWidth + (x-1) * barWidth + (x-1) * barOffset,0, halfWidth + (y-1)*barWidth + (y-1) * barOffset);

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
		}

		public override void DrawOutline()
		{
			
		}
    }
}