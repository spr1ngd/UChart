
Shader "UChart/Barchart/Simple"
{
	Properties
	{
		_Color ("Color",Color) = (1,1,1,1)
		_Alpha ("Alpha",range(0,1)) = 0.6 
	}

	SubShader
	{
		Tags{"Queue"="Transparent"}
	 	Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			#pragma multi_compile_instancing

			fixed4 _Color;
			fixed _Alpha;

			struct a2v
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert( a2v IN )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);				
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				return OUT;
			}

			fixed4 frag(v2f IN):COLOR
			{
				fixed4 color = fixed4(_Color.rgb ,_Alpha);
				return color;
			}

			ENDCG
		}
		
	}
}