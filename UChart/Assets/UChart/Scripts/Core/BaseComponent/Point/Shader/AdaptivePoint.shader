
Shader "UChart/Point/Point(Adpative)"
{
    Properties
    {
        _PointColor("Point Color(RGBA)",COLOR) = (1,1,0,0.5)
        _RampColor("Ramp Color(RGB)",COLOR) = (1,1,1,0.5)

        _PointRadius("Point Radius",float) = 0.5
        [HideInInspector]_PointSize("PointSize",float) = 0.48
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

            float4 _PointColor;
            float4 _RampColor;

            float _PointRadius;
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

                float3 cameraPos = _WorldSpaceCameraPos.xyz;
                float vDc = distance(IN.vertex,cameraPos);  
                _PointRadius = _PointRadius * 0.1 * vDc ;
                IN.vertex = IN.vertex * _PointRadius * 2;
                OUT.vertex = UnityObjectToScreenPos(IN.vertex);
                OUT.uv =IN.uv;
                UNITY_TRANSFER_FOG(OUT,OUT.vertex);
                return OUT;
            }

            float2 antialias( float radius,float borderSize,float distance )
            {
                return smoothstep(radius - borderSize , radius + borderSize, distance);
            }

            fixed4 frag( v2f IN ) : SV_Target 
            {
               
                float x = IN.uv.x;
                float y = IN.uv.y;
                float dis = sqrt(pow((0.5-x),2)+pow((0.5-y),2)); 
                float aliasValue = antialias(_PointSize,_FeatherWidth,dis);
                fixed4 color = lerp(_PointColor,fixed4(_RampColor.rgb,0),aliasValue);
                return color;
            }
            ENDCG
        }
    }
}