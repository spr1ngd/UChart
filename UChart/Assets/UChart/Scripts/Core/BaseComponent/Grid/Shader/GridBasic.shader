
Shader "UChart/Grid/Grid(Basic)"
{
    Properties
    {
        _GridSize("Grid Size",float) = 10
        _Division("Division",int) = 10
    }

    SubShader
    {
        Tags{"RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #define MAXVERTEXCOUNT 128

            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Division;

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
                // float2 uv : TEXCOORD1;
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
                g2f start;
                g2f end;

                start.vertex = UnityObjectToClipPos(p[0].vertex);
                start.color = p[0].color;
                end.vertex = UnityObjectToClipPos(p[1].vertex);
                end.color = p[1].color;

                ls.Append(start);
                ls.Append(end);
                ls.RestartStrip();
            }

            fixed4 frag(g2f IN):COLOR
            {
                return IN.color;
            }

            ENDCG
        }
    }
}