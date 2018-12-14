
Shader "UChart/Grid/Grid(Basic)"
{
    Properties
    {
        
    }

    SubShader
    {
        Tags{"RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float4 _MainColor;
            uniform float4 _MatchColor;

            struct a2v 
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            struct v2g
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            struct g2f
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2g vert( a2v IN )
            {
                v2g OUT;
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                OUT.uv = IN.uv;
                return OUT;
            }

            [maxvertexcount(2)]
            void geom( line v2g p[2] , inout LineStream<g2f> ls )
            {
                float3 horizontal = float3(0,1,0);
                float3 viewDir = UNITY_MATRIX_IT_MV[2].xyz;
                float vDoth = clamp(dot(horizontal,viewDir),0.1,0.8);

                g2f gOUT;
                gOUT.vertex = UnityObjectToClipPos(p[0].vertex);
                if( p[0].uv.x > 0.1 )
                    gOUT.color = float4(_MatchColor.xyz,vDoth);
                else
                    gOUT.color = _MainColor;
                gOUT.uv = p[0].uv;
                ls.Append(gOUT);

                gOUT.vertex = UnityObjectToClipPos(p[1].vertex);
                if( p[1].uv.x > 0.1 )
                    gOUT.color = float4(_MatchColor.xyz,vDoth);
                else
                    gOUT.color = _MainColor;
                gOUT.uv = p[1].uv;
                ls.Append(gOUT);
                ls.RestartStrip();
            }

            fixed4 frag(g2f IN) : COLOR
            {
                return IN.color;
            }

            ENDCG
        }
    }
}