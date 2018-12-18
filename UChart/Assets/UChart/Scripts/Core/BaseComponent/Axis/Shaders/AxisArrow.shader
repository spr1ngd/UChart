
Shader "UChart/Axis/AxisArrow(Basic)"
{
    Properties
    {

    }

    SubShader
    {
        Tags{"RenderType"="Opaque"}

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "../../UChartCG.cginc"

            v2f_full vert( a2v_full IN )
            {
                v2f_full OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.color = IN.color;
                OUT.normal = float4(UnityObjectToWorldNormal(IN.normal).xyz,0);
                OUT.uv = IN.uv;
                return OUT;
            }

            fixed4 frag( v2f_full IN ) : COLOR
            {
                // add lambert light model in axis arrow normal.
                return fixed4(IN.color.rgb,1);
            }

            ENDCG
        }
    }
}