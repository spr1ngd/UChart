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
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed _Alpha;

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD0;
            };

            v2f vert( a2v IN )
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.color = IN.color;
                return OUT;
            }

            fixed4 frag( v2f IN ) : COLOR
            {
                return fixed4(IN.color.rgba);
            }

            ENDCG
            }
    }
}