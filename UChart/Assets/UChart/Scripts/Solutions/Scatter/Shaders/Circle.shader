Shader "UChart/Scatter/Circle"
{
    Properties
    {
        _MainTex("Main Texture(RGB)",2D) = "white"{}
        _Color("Circle Color(RGBA)",COLOR) = (1,1,0,0.5)
        _RampColor("Ramp Color(RGBA)",COLOR) = (1,1,1,0.5)
        _CircleSize("Circle Size",range(0,0.5)) = 0.5
        _FeatherWidth("Feather Width",range(0,0.02)) = 0.02
    }

    SubShader
    {
        Tags{"Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _RampColor;

            float _CircleSize;
            float _FeatherWidth;

            struct a2v
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(a2v IN)
            {
                v2f OUT;
                // OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.vertex = mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.vertex.x,IN.vertex.y,0,0));
                OUT.uv = TRANSFORM_TEX(IN.uv,_MainTex);
                UNITY_TRANSFER_FOG(OUT,OUT.vertex);

                return OUT;
            }

            float2 antialias( float radius,float borderSize,float distance )
            {
                return smoothstep(radius - borderSize , radius + borderSize, distance);
            }

            fixed4 frag( v2f IN ) : SV_Target 
            {
                fixed4 color = tex2D(_MainTex,IN.uv);
                float x = IN.uv.x;
                float y = IN.uv.y;
                float dis = sqrt(pow((0.5-x),2)+pow((0.5-y),2));
                float aliasValue = antialias(_CircleSize,_FeatherWidth,dis);
                color = lerp(_Color,_RampColor,aliasValue);
                return color;
            }
            ENDCG
        }
    }
}