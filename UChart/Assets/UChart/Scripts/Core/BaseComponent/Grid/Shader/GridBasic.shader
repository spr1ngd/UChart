
Shader "UChart/Grid/Grid(Basic)"
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
                g2f gOUT;
                gOUT.vertex = UnityObjectToClipPos(p[0].vertex);
                gOUT.color = p[0].color;
                gOUT.uv = p[0].uv;
                ls.Append(gOUT);

                gOUT.vertex = UnityObjectToClipPos(p[1].vertex);
                // if( p[0].uv.x > 0.1 )
                //     gOUT.color = _MatchColor;
                // else
                //     gOUT.color = _MainColor; 
                gOUT.color = p[0].color;
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