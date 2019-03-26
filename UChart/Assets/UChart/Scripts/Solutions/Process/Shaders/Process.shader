Shader "UChart/Process/Process2D"
{
	Properties
	{
		[Toggle(UVCorrection)]
		_CorrectUV ("FIXED UV",float) = 1

		_Percent ("Percent",range(0,1)) = 0.8
		_Width ("Width",float) = 400.0
		_Height ("Height",float) = 20.0

		_BorderWidth ("BorderWidth" ,range(0,0.1)) = 0.001
		_BorderColor ("BorderColor" ,COLOR) = (0.1,0.1,0.1,0.3)
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
			#pragma shader_feature UVCorrection

			float4 _StartColor;
			float4 _EndColor;
			float _Alpha;
			float _Percent;

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
				float radius = lwRatio * 0.5; // 圆角半径（单位是UV）

				float2 remapUV = float2(IN.uv.x / lwRatio,IN.uv.y );
				float limitX = radius / lwRatio;

				float2 leftCenter = float2(radius,0.5);
				float2 rightCenter = float2(radius,0.5);
				if( remapUV.x > limitX )
				{
					if( _Percent > lwRatio )
						rightCenter = float2(1 * _Percent - radius,0.5);
					else
						rightCenter = float2(radius,0.5);
				}
				else
				{
					rightCenter = float2(radius,0.5);
				}
				// 用于色彩过渡的uv值
				float remapUVx = IN.uv.x; 
				#ifdef UVCorrection
					if( _Percent > 0 )
						remapUVx = IN.uv.x / _Percent;
				#endif
				if( IN.uv.x > leftCenter.x && IN.uv.x < rightCenter.x )
				{
					color = lerp(_StartColor,_EndColor,remapUVx);
					fixed4 color1;
					fixed4 color2;
					if( IN.uv.y > 0.5 )
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
					float dis;
					if( IN.uv.x < leftCenter.x )
						dis = distance(remapUV,float2(leftCenter.x / lwRatio,0.5));
					if( IN.uv.x > rightCenter.x )
						dis = distance(remapUV,float2(rightCenter.x / lwRatio,0.5));
					color = lerp(_StartColor,_EndColor,remapUVx);
					float rate = antialias(0.5,_BorderWidth,dis);							
					color = lerp(color,_BorderColor,rate);
					if( _Percent < lwRatio )
					{
						if( IN.uv.x > _Percent )
							discard;
					}
				}
				return fixed4(color.rgb, color.a*_Alpha);
			}

			ENDCG
		}
	}
}