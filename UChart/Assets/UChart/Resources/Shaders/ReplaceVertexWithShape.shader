
Shader "UChart/Geometry/VertexReplace"
{
    Properties
    {
        _Color("Color(RGBA)",COLOR) = (1,1,1,1)
    }

    SubShader
    {
        Tags{"Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma geometry gemo
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define MAXCOUNT 4

            fixed4 _Color;

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : COLOR0;
            };

            struct gIn
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float color : COLOR0;
            };

            v2f vert(a2v IN)
            {
                v2f OUT;
                return OUT;
            }

            [maxvertexcount(MAXCOUNT)]
            void geom(point gIn vert[1],input TriangleStream<v2f> triStream)
            {

            }

            fixed4 frag(v2f IN) : COLOR
            {
                return _Color;
            }

            ENDCG
        }
    }
}