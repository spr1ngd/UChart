Shader "UChart/Axis/Axis(Basic)"
{
    Properties
    {
        _AxisColor("Axis Color(RGBA)",Color) = (1,1,1,1)
        _MeshColor("Mesh Color(RGBA)",Color) = (0.6,0.6,0.6,0.6)
    }

    Category
    {
        SubShader
        {
            Tags{"RenderType"="Transparent" "Queue"="Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM

                #pragma vertex vert
                #pragma geometry geom
                #pragma fragment frag
                #include "UnityCG.cginc"
                #include "../../UChartCG.cginc"  

                float4 _AxisColor;

                v2g vert( a2v IN )
                {
                    v2g OUT;
                    OUT.vertex = IN.vertex;
                    OUT.color = IN.color;
                    OUT.uv = IN.uv;
                    return OUT;
                }

                [maxvertexcount(2)]
                void geom( line v2g p[2] ,inout LineStream<g2f> ls )
                {
                    g2f OUT;
                    OUT.vertex = UnityObjectToClipPos(p[0].vertex);
                    OUT.color = p[0].color;
                    OUT.uv = p[0].uv;
                    ls.Append(OUT);

                    OUT.vertex = UnityObjectToClipPos(p[1].vertex);
                    OUT.uv = p[1].uv;
                    ls.Append(OUT);
                    ls.RestartStrip();
                }

                fixed4 frag( g2f IN ) : Color
                {
                    return IN.color;
                }

                ENDCG
            }
        }
    }
}