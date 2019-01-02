
using UnityEngine;
using System.Collections;

namespace UChart
{
	public class Barchart3D : Barchart
    {
		public const float F_BAR_MAXCOUNT = 50.0f;
		public const int I_BAR_MAXCOUNT = 50;

		[Header("BARCHART SETTING")]

		[Range(0,100)]
		public float baseHeight = 2.0f;
		public float barOffset = 0.0f;
		public float barWidth = 2.0f;

		[Header("BARCHART STYLE")]
		public bool drawOutline = false;		
		public Color outlineColor = Color.blue;

		public Material mainMaterial = null;
		public Material outlineMaterial = null;

		public override void Draw()
		{
			base.DataCheck();
			// TODO: 添加鼠标交互事件(附带动画，信息展示)
			// TODO: 添加纵轴数据显示(由三维数组展示)			
			outlineMaterial.SetColor("_Color",outlineColor);
			StartCoroutine(drawSubmeshPart());
		}

		private IEnumerator drawSubmeshPart()
		{
			int xSubmeshCount = Mathf.CeilToInt(xCount / F_BAR_MAXCOUNT);
			int ySubmeshCount = Mathf.CeilToInt(yCount / F_BAR_MAXCOUNT);

			for( int x = 1 ; x <= xSubmeshCount ; x++ )
			{
				for( int y = 1 ,summeshIndex = 0; y <= ySubmeshCount; y++ ,summeshIndex++)
				{
					GameObject submeshPart = new GameObject("submesh_part"+summeshIndex);
					submeshPart.hideFlags = HideFlags.HideInHierarchy;
					submeshPart.transform.SetParent(this.myTransform);
					var meshFilter = submeshPart.AddComponent<MeshFilter>();
					var meshRenderer = submeshPart.AddComponent<MeshRenderer>();
					meshRenderer.sharedMaterials = new Material[]{mainMaterial,outlineMaterial};

					var xNumber = I_BAR_MAXCOUNT;
					var yNumber = I_BAR_MAXCOUNT;
					if( x == xSubmeshCount  ) xNumber = xCount - (x -1)*I_BAR_MAXCOUNT;
					if( y == ySubmeshCount ) yNumber = yCount - (y -1)*I_BAR_MAXCOUNT;
					var startPos = new Vector3( (x -1)* I_BAR_MAXCOUNT *(barOffset+barWidth),0,(y-1)* I_BAR_MAXCOUNT *(barOffset + barWidth));
					this.DrawPerBarchart(xNumber , yNumber , (x-1)*I_BAR_MAXCOUNT,(y-1)*I_BAR_MAXCOUNT ,startPos, meshFilter);
					yield return null;
				}
			}
		}

		private void DrawPerBarchart( int xCount ,int yCount ,int xArrayIndex,int yArrayIndex ,Vector3 startPos ,MeshFilter meshFilter)
		{
			float halfWidth = barWidth / 2.0f;

			Mesh mesh = new Mesh();
			mesh.name = "__submesh__";

			GeometryBuffer buffer = new GeometryBuffer();
			for( int x = 0 ; x < xCount; x++ )
			{
				for( int y = 0 ; y < yCount;y++ )
				{
					var pos = startPos + new Vector3( halfWidth + (x-1) * barWidth + (x-1) * barOffset,0, halfWidth + (y-1)*barWidth + (y-1) * barOffset);

					CubeGeometry cube = new CubeGeometry();
					cube.center = pos;
					cube.length = barWidth;
					cube.width = barWidth;
					cube.color = colors[xArrayIndex+x,yArrayIndex+y]; 
					cube.height = baseHeight + datas[xArrayIndex+x,yArrayIndex+y]; 
					cube.anchor = GeometryAnchor.Bottom;				

					cube.FillGeometry();
					buffer.CombineGeometry(cube.geometryBuffer);
				}
			}
			buffer.FillMesh(mesh,MeshTopology.Triangles);
			mesh.RecalculateNormals();
			mesh.RecalculateTangents();
			if( drawOutline )
			{
				buffer.FillMesh(mesh,MeshTopology.Lines);
			}
			meshFilter.mesh = mesh;	
		}
    }
}