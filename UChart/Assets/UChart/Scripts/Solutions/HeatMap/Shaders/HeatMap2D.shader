
Shader "UChart/HeatMap/2D"
{
	Properties
	{
		_ColorRamp ("Color Ramp",2D) = "white"{}
		_Alpha ("Alpha",range(0,1)) = 1.0
		_Width ("Width",range(0,200)) = 200
		_Height ("Height",range(0,200)) = 100
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
				float4 color = float4(1,1,1,_Alpha);
				
				
				return color;
			}

			ENDCG
		}
	}
}