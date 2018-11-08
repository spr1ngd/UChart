
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

			UNITY_INSTANCING_CBUFFER_START(InstanceProperties)
			// fixed4 _Color;
			 UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
			UNITY_INSTANCING_CBUFFER_END
			fixed _Alpha;

			struct a2v
			{
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 vertex : POSITION;				
			};

			struct v2f
			{
				UNITY_VERTEX_INPUT_INSTANCE_ID
				// UNITY_VERTEX_OUTPUT_STEREO
				float4 vertex : SV_POSITION;				
			};

			v2f vert( a2v IN )
			{
				v2f OUT;
				
				// UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

				UNITY_INITIALIZE_OUTPUT(v2f,OUT);		
				UNITY_SETUP_INSTANCE_ID(IN);		
				UNITY_TRANSFER_INSTANCE_ID(IN,OUT);

				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				return OUT;
			}

			fixed4 frag(v2f IN):COLOR
			{	
				UNITY_SETUP_INSTANCE_ID(IN);
				// fixed4 color = fixed4(_Color.rgb ,_Alpha);
				// fixed3 rgb = _Color.rgb;
				fixed3 rgb = UNITY_ACCESS_INSTANCED_PROP(_Color).rgb;
				fixed4 color = fixed4( rgb,_Alpha);
				return color;
			}

			ENDCG
		}
		
	}
}