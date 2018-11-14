// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UChart/Geometry/VertexReplace"
{
    Properties
    {
        _MainTex("Main Texture",2D) = "white"{}
        _Color("Color(RGBA)",COLOR) = (1,1,1,1)
        _QuadSize("Quad Size",Range(0.001,1.0)) = 0.5
        _CircleSize("Circle Size",Range(0.001,1.0)) = 0.1
        _FeatherWidth("Feather Width",range(0.001,0.2)) = 0.08
        _BorderColor("Border Color",COLOr) = (1,1,1,1)
        _Scale("Scale",range(1,10)) = 1
    }

    SubShader
    {
        Tags{"Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWRITE OFF
        // ZTEST Always  
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma geometry gemo
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define MAXCOUNT 6

            fixed4 _Color;
            float _QuadSize;
            float _CircleSize;
            float _FeatherWidth;
            float4 _BorderColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Scale;

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : COLOR0;
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
                // OUT.vertex = UnityObjectToClipPos(IN.vertex);
                // OUT.vertex =  mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.vertex.x,IN.vertex.y,0,0));
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                return OUT;
            }

            [maxvertexcount(MAXCOUNT)]
            void gemo( point gIn p[1] , inout TriangleStream<v2f> triStream )
            {
                float halfS = _QuadSize / 2;

                float3 up = UNITY_MATRIX_IT_MV[0].xyz;
                float3 look = _WorldSpaceCameraPos - p[0].vertex;
                look.y = 0;
                look = normalize(look);
                float right = UNITY_MATRIX_IT_MV[1].xyz;

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

                // vertices
                // const float4 vertices[MAXCOUNT] = 
                // {
                //     float4(-size,0,-size,0),
                //     float4(-size,0,size,0),
                //     float4(size,0,size,0),
                //     float4(size,0,size,0),
                //     float4(size,0,-size,0),
                //     float4(-size,0,-size,0)
                // };

                // triangles
                // const int triangles[6] = {0,1,2,3,4,5};

                // uvs
                // const float2 uvs[MAXCOUNT] = 
                // {
                //     float2(0,0),
                //     float2(0,1),
                //     float2(1,1),
                //     float2(1,1),
                //     float2(1,0),
                //     float2(0,0)
                // };

                // v2f v[MAXCOUNT];

                // vertics
                // for( int i = 0 ; i < MAXCOUNT ; i++ )
                // {
                //     float4 IN = p[0].vertex + vertices[i];
                //     v[i].vertex = UnityObjectToClipPos(IN);
                //     // v[i].vertex = mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.x,IN.y,0,0) *float4(_Scale,_Scale,1,1));
                //     v[i].color = p[0].color;
                //     v[i].uv = TRANSFORM_TEX(uvs[i],_MainTex);
                // }

                // for( int j = 0; j < 2 ; j++ )
                // {
                //     triStream.Append(v[triangles[j*3 + 0]]);
                //     triStream.Append(v[triangles[j*3 + 1]]);
                //     triStream.Append(v[triangles[j*3 + 2]]);
                //     triStream.RestartStrip();
                // }
            }

            float antialias( float radius , float borderSize, float distance )
            {
                return smoothstep(radius - borderSize , radius + borderSize , distance);
            }

            fixed4 frag(v2f IN) : COLOR
            {
                fixed4 color = tex2D(_MainTex,IN.uv);
                float dd = sqrt(pow((0.5 - IN.uv.x),2) + pow((0.5 - IN.uv.y) ,2));

                // TODO: 在进行运算之前，计算出主色及边缘色
                float aliasValue = antialias(_CircleSize,_FeatherWidth,dd);
                color = lerp(IN.color,_BorderColor,aliasValue);   
                return color;
            }

            ENDCG
        }
    }
}