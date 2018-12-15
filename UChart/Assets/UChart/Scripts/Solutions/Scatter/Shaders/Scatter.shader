
Shader "UChart/Scatter/Scatter3D"
{
    Properties
    {
        _MainTex("Main Texture",2D) = "white"{}
        _Alpha("Alpha",range(0,1)) = 1

        _PointRadius("Point Radius",float) = 1
        [HideInInspector]_PointSize("Point Size",float) = 0.48

        _FeatherWidth("Feather Width",range(0.001,0.02)) = 0.015
        _BorderColor("Border Color(RGB)",COLOR) = (1,1,1,0)        
    }

    SubShader
    {
        Tags{"Queue"="Transparent"}
        // Blend SrcAlpha OneMinusSrcAlpha
        // Blend SrcColor Zero

        LOD 100

        CGINCLUDE

        #pragma vertex vert
        #pragma geometry gemo
        #pragma fragment frag
        #include "UnityCG.cginc"

        #define MAXCOUNT 4
        #define SCALE_FACTOR 0.025 // 随相机距离的缩放因子
        #define MAXSIZE_FACTOR 1.2

        float _Alpha;
        float _PointRadius;
        float _PointSize;
        float _FeatherWidth;
        float4 _BorderColor;

        sampler2D _MainTex;
        float4 _MainTex_ST;

        struct a2v
        {
            float4 vertex : POSITION;
            float4 color : COLOR0;
            uint vertexId : SV_VertexID;
        };

        struct gIn
        {
            float4 vertex : SV_POSITION;
            float4 color : COLOR0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
            float4 color : COLOR0;          
        };

        gIn vert(a2v IN)
        {
            gIn OUT;
            OUT.vertex = IN.vertex;
            OUT.color = IN.color;
            return OUT;
        }

        [maxvertexcount(MAXCOUNT)]
        void gemo( point gIn p[1] , inout TriangleStream<v2f> triStream )
        {
            float radius = _PointRadius;
            float vDc = distance(p[0].vertex,_WorldSpaceCameraPos.xyz);
            _PointRadius = _PointRadius * vDc * SCALE_FACTOR * p[0].color.a;

            if( _PointRadius > MAXSIZE_FACTOR * radius )
                _PointRadius = radius * MAXSIZE_FACTOR;
            float halfS = _PointRadius;

            float3 up = UNITY_MATRIX_IT_MV[0].xyz;
            // float3 look = _WorldSpaceCameraPos - p[0].vertex;
            // look.y = 0;
            // look = normalize(look);
            float3 right = UNITY_MATRIX_IT_MV[1].xyz;

            float4 v[4];
            v[0] = float4(p[0].vertex + halfS * right - halfS * up, 1.0f);
            v[1] = float4(p[0].vertex + halfS * right + halfS * up, 1.0f);
            v[2] = float4(p[0].vertex - halfS * right - halfS * up, 1.0f);
            v[3] = float4(p[0].vertex - halfS * right + halfS * up, 1.0f);

            v2f pIn;
            pIn.color = p[0].color;

            pIn.vertex = UnityObjectToClipPos(v[0]);
            pIn.uv = float2(1.0f, 0.0f);
            triStream.Append(pIn);

            pIn.vertex = UnityObjectToClipPos(v[1]);
            pIn.uv = float2(1.0f, 1.0f);
            triStream.Append(pIn);

            pIn.vertex = UnityObjectToClipPos(v[2]);
            pIn.uv = float2(0.0f, 0.0f);
            triStream.Append(pIn);

            pIn.vertex = UnityObjectToClipPos(v[3]);
            pIn.uv = float2(0.0f, 1.0f);
            triStream.Append(pIn);
        }

        float antialias( float radius , float borderSize, float distance )
        {
            return smoothstep(radius - borderSize , radius + borderSize , distance);
        }

        ENDCG

        Pass
        {
            CULL BACK
            ZWRITE ON
            ZTEST ALWAYS
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM            

            fixed4 frag(v2f IN) : SV_Target
            {
                float dd = sqrt(pow((0.5 - IN.uv.x),2) + pow((0.5 - IN.uv.y) ,2));           
                float aliasValue = antialias(_PointSize ,_FeatherWidth,dd);
                fixed4 color = lerp(IN.color,_BorderColor,aliasValue);    
                return fixed4(color.rgb,color.a * _Alpha);
            }

            ENDCG
        }
    }
}