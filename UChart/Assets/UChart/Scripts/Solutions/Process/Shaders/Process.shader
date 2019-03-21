Shader "UChart/Process/Process2D"
{
	Properties
	{
		_Width ("Width",float) = 400.0
		_Height ("Height",float) = 20.0

		_BorderWidth ("BorderWidth" ,range(0,0.1)) = 0.001
		_BorderColor ("BorderColor" ,COLOR) = (0.1,0.1,0.1,0)
		_StartColor ("StartColor",COLOR) = (1,0,0,1)
		_EndColor ("EndColor",COLOR) = (0,1,0,1)
		_Alpha ("Alpha",range(0,1)) = 1.0
	}

	SubShader
	{
		Tags{"RenderType"="Transparent" "Queue"="Transparent"}
		ZTest [unity_GUITestMode]

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			float4 _StartColor;
			float4 _EndColor;
			float _Alpha;

			float _Width;
			float _Height;
			float _BorderWidth;
			float4 _BorderColor;

			struct a2v
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD1;
			};

			v2f vert( a2v IN )
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			float antialias( float standard ,float borderWidth,float distance )
			{
				return smoothstep( standard - borderWidth , standard + borderWidth ,distance );
			}

			fixed4 frag( v2f IN ) : COLOR
			{
				fixed4 color = fixed4(0,0,0,0);
				float lwRatio = _Height / _Width;
				float radius = lwRatio * 0.5;

				float2 leftCenter = float2(radius,0.5);
				float2 rightCenter = float2(1 - radius,0.5);
				if( IN.uv.x > leftCenter.x && IN.uv.x < rightCenter.x )
				{
					color = lerp(_StartColor,_EndColor,IN.uv.x);
					fixed4 color1;
					fixed4 color2;
					if( IN.uv.y > 0.5  )
					{
						float rate = antialias(1,_BorderWidth,IN.uv.y);							
						color = lerp(color,_BorderColor,rate);
					}
					if( IN.uv.y <= 0.5  )
					{
						float rate = antialias(0,_BorderWidth,IN.uv.y );							
						color = lerp(_BorderColor,color,rate);
					}
				}
				else
				{
					float2 transferXY = float2(IN.uv.x / lwRatio,IN.uv.y ) ;
					float dis;
					if( IN.uv.x < leftCenter.x )
						dis = distance(transferXY,float2(leftCenter.x / lwRatio,0.5));
					if( IN.uv.x > rightCenter.x )
						dis = distance(transferXY,float2(rightCenter.x/lwRatio,0.5));
					color = lerp(_StartColor,_EndColor,IN.uv.x);
					float rate = antialias(0.5,_BorderWidth,dis);							
					color = lerp(color,_BorderColor,rate);
				}
				return fixed4(color.rgb, color.a*_Alpha);
			}

			ENDCG
		}
	}
}