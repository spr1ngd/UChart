
Shader "UChart/Vertex/VertexLit(Basic)"
{
    Properties
    {

    }

    SubShader
    {
        Tags{"RenderType"="Opaque"}

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            v2f vert(a2v IN) 
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.color = IN.color;
                return OUT;
            }

            fixed4 frag(v2f IN) : COLOR
            {   
                return IN.color;
            }

            ENDCG
        }
    }
}