// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UChart/Scatter/Circle Anti-Aliasing"
{
    Properties
    {
        _BoundColor("Bound Color", Color) = (1,1,1,1)
        _BgColor("Background Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _BoundWidth("BoundWidth", float) = 10
        _ComponentWidth("ComponentWidth", float) = 100
    }
SubShader{
Pass
            {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag Lambert alpha
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float _BoundWidth;
            fixed4 _BoundColor;
            fixed4 _BgColor;
            float _ComponentWidth;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
            float4 _MainTex_ST;
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            float antialias(float w, float d, float r) {
                    return 1-(d-r-w/2)/(2*w);
            }
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex,i.uv);
                float x = i.uv.x;
                float y = i.uv.y;
                float dis = sqrt(pow((0.5 - x), 2) + pow((0.5 - y), 2));
                if (dis > 0.5) {
                    discard;
                } else {
                    float innerRadius = (_ComponentWidth * 0.5 - _BoundWidth) / _ComponentWidth;
                    if (dis > innerRadius) {
                        c = _BoundColor;
                        //c.a = c.a*antialias(_BoundWidth, dis, innerRadius);
                    }
                    else {
                        c = _BgColor;
                    }
                }
                return c;
            }
            ENDCG
            }
}
}