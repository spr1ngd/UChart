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
                // OUT.vertex =  mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.vertex.x,IN.vertexy,0,0));
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                // OUT.uv = TRANSFER_TEX(IN.uv,);
                return OUT;
            }

            // TODO: 普通Vertex Lit Shader
            // [maxvertexcount(11)]
            // void gemo(triangle gIn p[3] , inout TriangleStream<v2f> triStream)
            // {
            //     for( int i = 0; i < 3 ; i++ )
            //     {
            //         v2f OUT;
            //         OUT.vertex = p[i].vertex;
            //         OUT.color = p[i].color;
            //         triStream.Append(OUT);
            //     }
            //     triStream.RestartStrip();
            // }

            // TODO: 利用billboard代替point
            [maxvertexcount(MAXCOUNT)]
            void gemo( point gIn p[1] , inout TriangleStream<v2f> triStream )
            {
                float size = _QuadSize / 2;

                // vertices
                const float4 vertices[MAXCOUNT] = 
                {
                    float4(-size,0,-size,0),
                    float4(-size,0,size,0),
                    float4(size,0,size,0),
                    float4(size,0,size,0),
                    float4(size,0,-size,0),
                    float4(-size,0,-size,0)
                };

                // triangles
                const int triangles[6] = {0,1,2,3,4,5};

                // uvs
                const float2 uvs[MAXCOUNT] = 
                {
                    float2(0,0),
                    float2(0,1),
                    float2(1,1),
                    float2(1,1),
                    float2(1,0),
                    float2(0,0)
                };

                v2f v[MAXCOUNT];

                // vertics
                for( int i = 0 ; i < MAXCOUNT ; i++ )
                {
                    float4 IN = p[0].vertex + vertices[i];
                    float4 ori = mul(UNITY_MATRIX_MV,float4(0,0,0,1));
                    IN.y = IN.z;
                    IN.z = 0;
                    IN.xyz += ori.xyz;
                    v[i].vertex = mul(UNITY_MATRIX_P,IN);
                    // v[i].vertex = UnityObjectToClipPos(IN);
                    // v[i].vertex = mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.x,IN.y,0,0));
                    v[i].color = p[0].color;
                    v[i].uv = TRANSFORM_TEX(uvs[i],_MainTex);
                }

                for( int j = 0; j < 2 ; j++ )
                {
                    triStream.Append(v[triangles[j*3 + 0]]);
                    triStream.Append(v[triangles[j*3 + 1]]);
                    triStream.Append(v[triangles[j*3 + 2]]);
                    triStream.RestartStrip();
                }
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