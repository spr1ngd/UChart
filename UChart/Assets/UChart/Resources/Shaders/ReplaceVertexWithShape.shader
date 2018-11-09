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
    }

    SubShader
    {
        Tags{"Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
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
                // float2 uv : TEXCOORD0;
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
                    v[i].vertex = UnityObjectToClipPos(IN);
                    // v[i].vertex =  mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(IN.x,IN.y,0,0));
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

            fixed4 frag(v2f IN) : COLOR
            {
                // TODO: 将quad绘制为圆形
                fixed4 color = tex2D(_MainTex,IN.uv);
                float distance = sqrt(pow((0.5 - IN.uv.x),2) + pow((0.5 - IN.uv.y) ,2));
                if( distance > _CircleSize )
                {
                    discard;
                }else
                {
                    color = fixed4(_Color.rgb,1);
                }
                return color;
            }

            ENDCG
        }
    }
}