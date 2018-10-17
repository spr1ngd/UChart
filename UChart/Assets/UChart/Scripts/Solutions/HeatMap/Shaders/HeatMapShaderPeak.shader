
Shader "UChart/HeatMap/Peak"
{
    Properties
    {
        _HeatMapTex("HeatMapTex",2D) = "white"{}
        _Alpha("Alpha",range(0,1)) = 0.8
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
            #include "unitycg.cginc"

            sampler2D _HeatMapTex;
            half _Alpha;
            uniform int _FactorCount;
            uniform float4 _Factors[100];
            uniform float2 _FactorsProperties[100];

            struct a2v
			{
				float4 pos : POSITION;
			};

			struct v2f
			{
				float4 pos : POSITION;
				fixed3 worldPos : TEXCOORD1;
			};

			v2f vert(a2v input)
			{
				v2f o;
                half wp = mul(unity_ObjectToWorld,input.pos).xyz;
                half heat = 0;
				for( int i = 0 ; i < _FactorCount;i++ )
				{
					half dis = distance(wp,_Factors[i].xyz);
					float radius = _FactorsProperties[i].x;
					float intensity = _FactorsProperties[i].y;
					half ratio = 1 - saturate(dis/radius);
					heat += intensity * ratio;
					// heat = clamp(heat,0.05,0.95);
				}
				o.pos = UnityObjectToClipPos(input.pos + half3(0,20,0));
				o.worldPos = mul(unity_ObjectToWorld,input.pos).xyz;
				return o;
			}

			half4 frag(v2f input):COLOR
			{
				half heat = 0;
				for( int i = 0 ; i < _FactorCount;i++ )
				{
					half dis = distance(input.worldPos,_Factors[i].xyz);
					float radius = _FactorsProperties[i].x;
					float intensity = _FactorsProperties[i].y;
					half ratio = 1 - saturate(dis/radius);
					heat += intensity * ratio;
					heat = clamp(heat,0.05,0.95);
				}
				half4 color = tex2D(_HeatMapTex,fixed2(heat,0.5));
				color.a = _Alpha;
				return color;
			}

            ENDCG
        }
    }

    Fallback "Diffuse"
}