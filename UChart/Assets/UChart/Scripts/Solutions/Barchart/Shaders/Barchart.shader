
Shader "UChart/Barchart/Barchart(Basic)"
{
	Properties
	{
		_MainTexture("Main Texture(RGBA)",2D) = "white"{}
	}

	SubShader
	{
		Tags{ "RenderType"="Transparent" "Queue"="Transparent" }

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "../../../Core/BaseComponent/UChartCG.cginc"

			sampler2D _MainTexture;
			float4 _MainTexture_ST;
			
			v2f_base vert( a2v_base IN )
			{
				v2f_base OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.color = tex2Dlod(_MainTexture,float4(IN.vertex.y / 10,0,0,0));
				return OUT;
			}

			fixed4 frag(v2f_base IN):COLOR
			{
				return IN.color;
			}

			ENDCG
		}
	}
}