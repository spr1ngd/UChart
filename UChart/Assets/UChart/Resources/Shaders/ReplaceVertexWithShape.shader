Shader "UChart/Geometry/VertexReplace"
{
    Properties
    {
        _MainTex("Main Texture",2D) = "white"{}
        _Color("Color(RGBA)",COLOR) = (1,1,1,1)
        _QuadSize("Quad Size",float) = 2
        _CircleSize("Circle Size",float) = 0.8
        _FeatherWidth("Feather Width",range(0.001,0.2)) = 0.08
        _BorderColor("Border Color",COLOr) = (1,1,1,1)
    }

    SubShader
    {
        Tags{"Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWRITE OFF
        ZTEST Always  
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
            float _QuadSize;
            float _CircleSize;
            float _FeatherWidth;
            float4 _BorderColor;

            // uniform float _SizeArray[64];
            // uniform int _PointCount = 0;

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
                // float4 size : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR0;          
                // float4 size : TEXCOORD1;      
            };

            gIn vert(a2v IN)
            {
                gIn OUT;
                // OUT.vertex = UnityObjectToClipPos(IN.vertex);
                // OUT.vertex =  mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.vertex.x,IN.vertex.y,0,0));
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                // OUT.size = float4(1,0,0,0);
                return OUT;
            }

            [maxvertexcount(MAXCOUNT)]
            void gemo( point gIn p[1] , inout TriangleStream<v2f> triStream )
            {
                float halfS = _QuadSize * 0.5;

                float3 up = UNITY_MATRIX_IT_MV[0].xyz;
                float3 look = _WorldSpaceCameraPos - p[0].vertex;
                look.y = 0;
                look = normalize(look);
                float3 right = UNITY_MATRIX_IT_MV[1].xyz;

                float4 v[4];
                v[0] = float4(p[0].vertex + halfS * right - halfS * up, 1.0f);
                v[1] = float4(p[0].vertex + halfS * right + halfS * up, 1.0f);
                v[2] = float4(p[0].vertex - halfS * right - halfS * up, 1.0f);
                v[3] = float4(p[0].vertex - halfS * right + halfS * up, 1.0f);

                v2f pIn;
                pIn.color = p[0].color;
                // pIn.size = float4(1,1,1,1);

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

            fixed4 frag(v2f IN) : COLOR
            {
                fixed4 color = tex2D(_MainTex,IN.uv);
                float dd = sqrt(pow((0.5 - IN.uv.x),2) + pow((0.5 - IN.uv.y) ,2));
                float aliasValue = antialias(_CircleSize * IN.color.r ,_FeatherWidth,dd);
                color = lerp(IN.color,_BorderColor,aliasValue);                
                return fixed4(color.rgb,color.a * _Color.a); //color.a * _Color.a
            }

            ENDCG
        }
    }
}