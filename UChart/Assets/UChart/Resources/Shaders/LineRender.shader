
Shader "UChart/Line/LineRender(RGBA)"
{
    Properties
    {
        _LineColor("Line Color(RGBA)",COLOR) = (0,0,0,1)
    }

    SubShader
    {
        Tags{"RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float4 _LineColor;

            struct a2v
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : POSITION;
            };

            v2f vert( a2v IN )
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                return OUT;
            }

            fixed4 frag( v2f IN ) : COLOR
            {
                return _LineColor;
            }

            ENDCG
        }
    }
}