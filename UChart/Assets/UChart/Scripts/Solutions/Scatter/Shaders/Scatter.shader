
Shader "UChart/Scatter/Simple"
{
    Properties 
    {
        
    }

    SubShader
    {
        Tags {"Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct a2v
            {
                float4 pos : POSITION;
                half4 color : TEXCOORD1;
            };

            struct v2f 
            {
                float4 pos : POSITION;
                half4 color : TEXCOORD0;
            };

            v2f vert( a2v input )
            {
                v2f o;
                o.pos = UnityObjectToClipPos(input.pos);
                o.color = input.color;
                return o;
            }

            half4 frag(v2f input):COLOR
            {
                return input.color;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}