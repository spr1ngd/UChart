
Shader "UChart/HeatMap/HeatMap2D"
{
	Properties
	{
		_ColorRamp ("Color Ramp",2D) = "white"{}
		_Alpha ("Alpha",range(0,1)) = 1.0
		_Width ("Width",range(0,200)) = 200
		_Height ("Height",range(0,200)) = 100

		_LineWidth ("Line Width",range(0.01,0.5)) = 0.02
		_LineColor ("Line Color",COLOR) = (0.9,0.9,0.9,1)
	}

	SubShader
	{
		Tags{ "RenderType"="Transparent" "Queue"="Transparent" }
		ZTest [unity_GUITestMode]

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _ColorRamp;
			float _Alpha;
			float _Width;
			float _Height;
			float _LineWidth;
			float4 _LineColor;
			uniform int _FactorCount = 100;
			uniform float3 _Factors[100];
			uniform float2 _FactorProperties[100];

			struct a2v
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f 
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert( a2v IN )
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			float4 frag( v2f IN ) : COLOR
			{
				float4 color = float4(1,1,1,1);
				// TODO: 重新映射UV至Width/Height数值
				float2 remapUV = float2(_Width,_Height);
				float minLength = 1;
				for(int x=0; x<remapUV.x; x++)
				{
					for(int y=0;y<remapUV.y;y++)
					{
						float2 cellUV = float2(x * minLength,y*minLength);
						float2 uv = float2(IN.uv.x*_Width,IN.uv.y*_Height);
						float xMin = uv.x + _LineWidth * _Width;
						float xMax = uv.x + 1 - _LineWidth * _Width;
						float yMin = uv.y + _LineWidth * _Height;
						float yMax = uv.y + 1 + _LineWidth * _Height;
						if( uv.x > x && uv.x < x + 1)
						{
							if( uv.x < xMin)// || uv.x > xMax
							{
								color = _LineColor;
							}
							else
							{
								color = fixed4(uv.x /255,uv.y /255,0,1);
							}
						}
					}
				}
				return color;
			}

			ENDCG
		}
	}
}