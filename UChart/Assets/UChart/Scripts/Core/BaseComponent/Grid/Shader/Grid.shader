
Shader "UChart/Grid/Grid"
{
    Properties
    {
        _MainColor("MainColor(RGBA)",Color) = (0,0,0,0.8)
        _MatchColor("MatchColor(RGBA)",Color) = (0.6,0.6,0.6,0.6)
    }

    SubShader
    {
        Tags{"RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #define CAMERAMAXDISTANCE 10

            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "../../UChartCG.cginc"        

            float4 _MainColor;
            float4 _MatchColor;

            v2g vert( a2v IN )
            {
                v2g OUT;
                OUT.vertex = IN.vertex;
                OUT.color = IN.color;
                OUT.uv = IN.uv;
                return OUT;
            }

            [maxvertexcount(2)]
            void geom( line v2g p[2] , inout LineStream<g2f> ls )
            {
                float3 horizontal = float3(0,1,0);
                float3 viewDir = UNITY_MATRIX_IT_MV[2].xyz;
                float vDoth = clamp(dot(horizontal,viewDir),0.1,0.9);
                float vDc = distance(float3(0,0,0),_WorldSpaceCameraPos.xyz);
                float dAlpha = clamp(1 - clamp((vDc - CAMERAMAXDISTANCE) / CAMERAMAXDISTANCE,0,1),0.1,0.9);

                g2f gOUT;
                gOUT.vertex = UnityObjectToClipPos(p[0].vertex);
                if( p[0].uv.x > 0.1 )
                    gOUT.color = float4(_MatchColor.rgb,vDoth *dAlpha );
                else
                    gOUT.color = float4(_MainColor.rgb  , vDoth ); 
                gOUT.uv = p[0].uv;
                ls.Append(gOUT);

                gOUT.vertex = UnityObjectToClipPos(p[1].vertex);
                if( p[1].uv.x > 0.1 )
                    gOUT.color = float4(_MatchColor.rgb,vDoth * dAlpha  ); 
                else
                    gOUT.color = float4(_MainColor.rgb , vDoth );
                gOUT.uv = p[1].uv;
                ls.Append(gOUT);
                ls.RestartStrip();
            }

            fixed4 frag(g2f IN) : COLOR
            {
                return IN.color;
            }

            ENDCG
        }
    }
}