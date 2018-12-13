
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

            struct a2v 
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                // float2 uv : TEXCOORD1;
            };

            struct v2g
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                // float2 uv : TEXCOORD1;
            };

            struct g2f
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                // float2 uv : TEXCOORD1;
            };

            v2g vert( a2v IN )
            {
                v2g OUT;
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                // OUT.uv = IN.uv;
                return OUT;
            }

            [maxvertexcount(2)]
            void geom( line v2g p[2] , inout LineStream<g2f> ls )
            {
                g2f gOUT;
                gOUT.vertex = UnityObjectToClipPos(p[0].vertex);
                if( p[0].color.a > 0.5 )
                    gOUT.color = float4(1,0,0,1);
                else
                    gOUT.color = float4(0,0,0,1);
                // gOUT.uv = p[0].uv;
                ls.Append(gOUT);

                gOUT.vertex = UnityObjectToClipPos(p[1].vertex);
                if( p[1].color.a > 0.5 )
                    gOUT.color = float4(1,0,0,1);
                else
                    gOUT.color = float4(0,0,0,1);
                // gOUT.uv = p[1].uv;
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