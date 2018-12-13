Shader "UChart/Vertex/VertexColor"
{
    Properties
    {
        _Alpha("Alpha",range(0,1)) = 1
    }

    SubShader
    {
        Tags{"RenderType"="Transparent"}

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed _Alpha;

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
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.color = IN.color;
                OUT.uv = IN.uv;
                return OUT;
            }

            [maxvertexcount(1)]
            void geom( point v2g p[1] ,inout PointStream<g2f> ps )
            {
                g2f gOUT;
                gOUT.vertex = p[0].vertex;
                gOUT.uv = p[0].uv;
                // OUT.color = p[0].color;
                if( p[0].uv.x > 0.1 )
                    gOUT.color = float4(1,0,0,1);
                else
                    gOUT.color = float4(0,0,0,1);
                ps.Append(gOUT);
            }

            fixed4 frag( g2f IN ) : COLOR
            {
                return fixed4(IN.color.rgba);
            }

            ENDCG
        }
    }
}