Shader "UChart/Point/Point3D(Texture)"
{
    Properties
    {
        _MainTex("Main Texture(RGB)",2D) = "white"{}
        _PointColor("Point Color(RGBA)",COLOR) = (1,1,0,0.5)
        _RampColor("Ramp Color(RGB)",COLOR) = (1,1,1,0.5)

        _PointSize("Point Radius",float) = 0.5
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
            #include "../../UChartCG.cginc"

            #define POINTRADIUS 0.48 // remain 0.02 for feather width.

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _PointColor;
            float4 _RampColor;

            float _PointSize;
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
                IN.vertex = IN.vertex * _PointSize * 2;
                OUT.vertex = UnityObjectToScreenPos(IN.vertex);
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
                fixed4 texColor = tex2D(_MainTex,IN.uv);
                float x = IN.uv.x;
                float y = IN.uv.y;
                float dis = sqrt(pow((0.5-x),2)+pow((0.5-y),2)); 
                float aliasValue = antialias(POINTRADIUS,_FeatherWidth,dis);
                fixed4 color = lerp(_PointColor,fixed4(_RampColor.rgb,0),aliasValue);
                return color;
            }
            ENDCG
        }
    }
}