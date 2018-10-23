
Shader "UChart/Scatter/Simple"
{
    Properties 
    {
        _PointSize("Point Size",range(1,50)) = 1
        _Alpha("Alpha",range(0,1)) = 1
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

            float _PointSize;
            float _Alpha;

            struct a2v
            {
                float4 pos : POSITION;
                half4 color : TEXCOORD1;
            };

            struct v2f 
            {
                float4 pos : POSITION;
                half4 color : TEXCOORD0;
                float size : PSIZE;
                float2 uv : TEXCOORD1;
            };

            v2f vert( a2v input )
            {
                v2f o;
                o.pos = UnityObjectToClipPos(input.pos);
                o.color = input.color;
                o.size = _PointSize;
                return o;
            }

            half4 frag(v2f input):COLOR
            {
                float distance = sqrt(pow(input.uv.x, 2) + pow(input.uv.y,2));
                 //float distancez = sqrt(distance * distance + i.l.z * i.l.z);
 
 
                if(distance > 0.5f)
                {
                    return half4(1,0,0,1);
                }
                else
                {
                    return half4(0,1,0,1);
                }
                // return half4(input.color.rgb,_Alpha);
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}