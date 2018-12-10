
Shader "UChart/Line/Line(Basic)"
{
    Properties
    {
        _LineTexture("Line Texture",2D) = "white"{}
        _Thickness("Thickness",range(0.1,5)) = 1
        _PointSize("Point Size",range(0.1,1)) = 0.3
    }

    SubShader
    {
        Tags{"RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #define MAXVERTEXCOUNT 3

            #pragma vertex vert
            #pragma geometry geo
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Thickness;
            float _PointSize;
            sampler2D _LineTexture;
            float4 _LineTexture_ST;

            struct a2v
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
            };

            struct v2g
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
              
            };

            struct g2f
            {
                float4 vertex : POSITION;
                float4 color : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2g vert(a2v IN)
            {
                v2g OUT;
                // OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                return OUT;
            }

            [maxvertexcount(4)]
            void geo(line v2g p[2] , inout TriangleStream<g2f> ts )
            {
                float4 p0 = p[0].vertex;
                float4 p1 = p[1].vertex;
                float4 center = float4((p0.x + p1.x*0.5),
                                        (p0.y + p1.y*0.5),
                                        (p0.z + p1.z*0.5),
                                        (p0.w + p1.w*0.5));

                float dis = distance(center.xyz,_WorldSpaceCameraPos.xyz);
                // float lineWidth = _Thickness * dis * 0.01;
                // calculate line width dynamicly.
                float lineWidth = _Thickness * dis * 0.05;
                if( lineWidth > 0.02 )
                    lineWidth = 0.02;
                // p0 = UnityObjectToClipPos(p0);
                // p1 = UnityObjectToClipPos(p1);

                float3 up = UNITY_MATRIX_IT_MV[0].xyz;
                float3 right = UNITY_MATRIX_IT_MV[1].xyz;
                float halfWidth = lineWidth * 0.5;
                // TODO: 让三角面始终冲向屏幕即可
                
                float4 t1 = p0 + float4(-halfWidth,halfWidth,0,0);
                float4 t2 = p0 + float4(halfWidth,-halfWidth,0,0);
                float4 t3 = p1 + float4(-halfWidth,halfWidth,0,0);
                float4 t4 = p1 + float4(halfWidth,-halfWidth,0,0);

                g2f pIn;
                pIn.color = float4(1,1,1,1);

                // pIn.vertex = t1;
                pIn.vertex = UnityObjectToClipPos(t1);
                pIn.uv = float2(1,0);
                ts.Append(pIn);

                // pIn.vertex = t2;
                pIn.vertex = UnityObjectToClipPos(t2);
                pIn.uv = float2(0,0);
                ts.Append(pIn);

                // pIn.vertex = t3;
                pIn.vertex = UnityObjectToClipPos(t3);
                pIn.uv = float2(1,1);
                ts.Append(pIn);           

                // pIn.vertex = t4;
                pIn.vertex = UnityObjectToClipPos(t4);
                pIn.uv = float2(0,1);
                ts.Append(pIn);     
            }

            fixed4 frag( g2f IN ) : SV_Target
            {
                return tex2D(_LineTexture,IN.uv);
            }

            ENDCG
        }
    }
}