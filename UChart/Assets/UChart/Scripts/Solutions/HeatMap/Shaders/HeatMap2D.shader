
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

		CGINCLUDE

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
			float2 remapUV : TEXCOORD1;
		};

		v2f vert( a2v IN )
		{
			v2f OUT;
			OUT.vertex = UnityObjectToClipPos(IN.vertex);
			OUT.uv = IN.uv;
			OUT.remapUV = float2(_Width,_Height);
			return OUT;
		}

		ENDCG

		// x axis
		// Pass
		// {
		// 	Blend SrcAlpha OneMinusSrcAlpha
		// 	CGPROGRAM

		// 	float4 frag( v2f IN ) : COLOR
		// 	{
		// 		float4 color = float4(0,0,0,0);
		// 		float2 remapUV = IN.remapUV;
		// 		// for(int x=0; x<remapUV.x; x++)
		// 		// {
		// 		// 	float2 uv = float2(IN.uv.x*_Width,IN.uv.y*_Height);
		// 		// 	float xMin = x + _LineWidth ;
		// 		// 	float xMax = x + 1 - _LineWidth ;
		// 		// 	if( uv.x > x && uv.x < x + 1)
		// 		// 	{
		// 		// 		if( uv.x < xMin || uv.x > xMax)
		// 		// 		{
		// 		// 			color = _LineColor;
		// 		// 		}
		// 		// 	}
		// 		// }
		// 		for( int x = 0; x < remapUV.x ;x++ )
		// 		{
		// 			float xMin = -halfWidth + x;
		// 			float xMax = halfWidth + x;
		// 			float2 uv = float2(IN.uv.x * _Width,IN.uv.y * _Height);
		// 			if( uv.x > xMin && uv.x < xMax )
		// 				color = _LineColor;
		// 		}
		// 		return color;
		// 	}

		// 	ENDCG
		// }

		// y axis
		// Pass
		// {
		// 	Blend ONE OneMinusSrcAlpha
		// 	CGPROGRAM

		// 	float4 frag( v2f IN ) : COLOR
		// 	{
		// 		float4 color = float4(0,0,0,0);
		// 		float2 remapUV = IN.remapUV;
		// 		for(int y=0; y<remapUV.y; y++)
		// 		{
		// 			float2 uv = float2(IN.uv.x*_Width,IN.uv.y*_Height);
		// 			float yMin = y + _LineWidth ;
		// 			float yMax = y + 1 - _LineWidth ;
		// 			if( uv.y > y && uv.y < y + 1)
		// 			{
		// 				if( uv.y < yMin || uv.y > yMax)
		// 				{
		// 					color = _LineColor;
		// 				}
		// 			}
		// 		}
		// 		return color;
		// 	}

		// 	ENDCG
		// }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			float4 frag( v2f IN ) : COLOR
			{
				float2 remapUV = float2(IN.uv.x * _Width,IN.uv.y *_Height);
				fixed4 color = _LineColor;
				for( int x = 0 ;x < 1;x ++ )
				{
					for( int y = 0; y < _Height;y++ )
					{
						float xMin = _LineWidth + x;
						float yMin = _LineWidth + y;
						if( remapUV.x > xMin  ) 
							color = fixed4(1,0,0,1);
					}
				} 
				return color;
			}

			ENDCG
		}
	}
}