
Shader "UChart/HeatMap/HeatMap2D"
{
	Properties
	{
		[Toggle(BOOL_DISCRETE)]
		_Discrete("Discrete",int) = 1

		[Toggle(BOOL_DRAWLINE)]
		_DrawLine("DrawLine",int) = 1

		_ColorRamp ("Color Ramp",2D) = "white"{}
		_Alpha ("Alpha",range(0,1)) = 1.0
		_Width ("Width",range(0,200)) = 200
		_Height ("Height",range(0,200)) = 100

		_TextureWidth("Texture Width",int) = 600
		_TextureHeight("Texture Height",int) = 600
		_LineColor ("Line Color",COLOR) = (0.9,0.9,0.9,1)
	}

	SubShader
	{
		Tags{ "RenderType"="Transparent" "Queue"="Transparent" }
		ZTest [unity_GUITestMode]

		CGINCLUDE

		#pragma vertex vert
		#pragma fragment frag
		#pragma shader_feature BOOL_DISCRETE
		#pragma shader_feature BOOL_DRAWLINE

		sampler2D _ColorRamp;
		float _Alpha;
		float _Width;
		float _Height;
		float _TextureWidth;
		float _TextureHeight;
		float4 _LineColor;

		uniform int _FactorCount = 100;
		uniform float2 _Factors[100];
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

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			float4 frag( v2f IN ) : COLOR
			{
				float2 remapUV = float2(IN.uv.x * _Width,IN.uv.y *_Height);
				fixed4 color = _LineColor;
				int xIndex = remapUV.x / 1;
				int yIndex = remapUV.y / 1;
				if( remapUV.x > xIndex && remapUV.x < xIndex + 1 && remapUV.y > yIndex && remapUV.y < yIndex + 1)
				{
					float heat;
					float4 heatColor;
					#ifdef BOOL_DISCRETE
					for( int i = 0 ; i < _FactorCount ;i++)
					{
						float2 hp = _Factors[i];
						float radius = _FactorProperties[i].x;
						float intensity = _FactorProperties[i].y;
						float2 center = float2(xIndex/_Width,yIndex/_Height);
						float dis = distance(hp,center);
						float ratio = 1 - saturate(dis /radius );
						heat += intensity * ratio;
					}
					heatColor = tex2D(_ColorRamp,float2(heat,0.5));
					#else
					for( int i = 0 ; i < _FactorCount ;i++)
					{
						float2 hp = _Factors[i];
						float radius = _FactorProperties[i].x;
						float intensity = _FactorProperties[i].y;
						float dis = distance(hp,IN.uv.xy);
						float ratio = 1 - saturate(dis /radius );
						heat += intensity * ratio;
					}
					heatColor = tex2D(_ColorRamp,float2(heat,0.5));
					#endif
					float lineWidth = (_Width /2) / _TextureWidth; 
					float lineHeight = (_Height /2) / _TextureHeight;
					if( remapUV.x > xIndex + lineWidth && remapUV.x < xIndex + 1 - lineWidth && remapUV.y > yIndex + lineHeight && remapUV.y < yIndex + 1- lineHeight )
					{
						color = heatColor;
					}
					else 
					{
						#ifdef BOOL_DRAWLINE
						color = lerp(_LineColor,heatColor,0.8);
						#else
					    color = heatColor;
						#endif
					}
				}
				return fixed4(color.rgb,_Alpha);
			}

			ENDCG
		}
	}
}