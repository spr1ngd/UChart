
Shader "UChart/Line/Line(Simple)"
{
    Properties
    {

    }

    SubShader
    {
        Tags{ "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma geometry geo
            #pragma fragment frag
            
            #define MAXCOUNT 2

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float4 uv : TEXCOORD1;                
            };

            struct v2g
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float4 uv : TEXCOORD1;
            };

            struct g2f
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float4 uv : TEXCOORD1;
            };

            v2g vert( a2v IN )
            {
                v2g OUT;
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                OUT.uv = IN.uv;
                return OUT;
            }

            [maxvertexcount(MAXCOUNT)]
            void geo( point v2g p[1] , inout LineStream<g2f> triStream )
            {
                g2f gOUT;
                gOUT.vertex = p[0].vertex;
                gOUT.color = p[0].color;
                gOUT.uv = p[0].uv;
                triStream.Append(gOUT);
            }

            fixed4 frag( g2f IN ) : COLOR
            {
                return fixed4(1,1,1,1);
            }

            ENDCG
        }
    }
}