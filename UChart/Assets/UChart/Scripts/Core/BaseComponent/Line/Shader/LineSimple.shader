
Shader "UChart/Line/Line(Simple)"
{
    Properties
    {
        _LineTexture("Line Texture",2D) = "white"{} //TexGen ObjectLinear
        _LineWidth("Line Width",range(0,5)) = 1 
    }

    SubShader
    {
        Tags{ "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        // Cull Back

        Pass
        {
            CGPROGRAM 

            #pragma vertex vert
            #pragma geometry geo
            #pragma fragment frag
            
            #define MAXCOUNT 4
            sampler2D _LineTexture;
            float4 _LineTexture_ST;
            float _LineWidth;

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;                
            };

            struct v2g
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            struct g2f
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2g vert( a2v IN )
            {
                v2g OUT;
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                OUT.uv = IN.uv;
                return OUT;
            }

            [maxvertexcount(MAXCOUNT)]
            void geo( line v2g p[2] , inout TriangleStream<g2f> triStream )
            {
                float halfWidth = _LineWidth * 0.5f;

                float3 up = UNITY_MATRIX_IT_MV[0].xyz;
                float3 right = UNITY_MATRIX_IT_MV[1].xyz;

                float4 v[4];
                v[0] = float4(p[0].vertex.xyz + right * halfWidth - up * halfWidth ,1.0f);
                v[1] = float4(p[1].vertex.xyz + right * halfWidth + up * halfWidth ,1.0f);
                v[2] = float4(p[0].vertex.xyz - right * halfWidth - up * halfWidth,1.0f);
                v[3] = float4(p[1].vertex.xyz - right * halfWidth + up * halfWidth ,1.0f);

                g2f gOUT;
                gOUT.color = float4(0,1,1,0.5f);               
                gOUT.uv = float2(0,0);
                // gOUT.vertex = UnityObjectToClipPos(v[0]);
                gOUT.vertex = mul(UNITY_MATRIX_P, 
                  mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
                 - float4(v[0].y, v[0].x, 0.0, 0.0)
                  * float4(0.1, 0.1, 1.0, 1.0));
                triStream.Append(gOUT);

                gOUT.vertex =UnityObjectToClipPos(v[1]);
                triStream.Append(gOUT);

                gOUT.vertex = UnityObjectToClipPos(v[2]);  
                triStream.Append(gOUT);

                gOUT.vertex = UnityObjectToClipPos(v[3]);
                triStream.Append(gOUT);               
            }

            fixed4 frag( g2f IN ) : SV_Target
            {
                // TODO: 读取贴图数据 , 以较简单的方式事项扛锯齿的效果
                return IN.color;
            }

            ENDCG
        }
    }
}