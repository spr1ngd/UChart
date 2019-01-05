
Shader "UChart/Pie/2D(Texture)"
{
    Properties
    {
        [Toggle(ANIMATE)] _Animate ("Animate", Int) = 1
        _Value ("Percent",range(0,1)) = 1

        _Color ("Main Color(remapUVB)",COLOR) = (1,1,1,1)
        _Texture ("Texture",2D) = "white"{}
        _Alpha ("Alpha",range(0,1)) = 0.5

        _Radius ("Pie Radius Percent",range(0,0.5)) = 0.5
        _HollowRadius ("Hollow Radius",range(0,0.5)) = 0.2
        _BorderWidth ("Border Width",range(0.0001,0.02)) = 0.01
        _BorderColor ("Border Color",COLOR) = (0.1,0.1,0.1,0.1)
    }

    SubShader
    {
        Tags{"RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        CGINCLUDE

        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #pragma shader_feature ANIMATE

        float _Value;

        sampler2D _Texture;
        float4 _Texture_ST;
        float4 _Color;
        float _Alpha;

        float _Radius;
        float _HollowRadius;

        float _BorderWidth;
        float4 _BorderColor;

        struct a2v
        {
            float4 vertex : POSITION;
            float4 color : COLOR;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float4 color : COLOR;
            float2 uv : TEXCOORD0;
        };

        float antialias( float radius,float bordersize,float distance )
        {
            return smoothstep(radius - bordersize,radius + bordersize,distance);
        }

        float antialias1( float radius,float bordersize,float distance )
        {
            return smoothstep(radius + bordersize,radius - bordersize,distance);
        }

        v2f vert( a2v IN)
        {
            v2f OUT;
            OUT.vertex = UnityObjectToClipPos(IN.vertex);
            OUT.color = IN.color;
            OUT.uv = TRANSFORM_TEX(IN.uv,_Texture);
            return OUT;
        }

        ENDCG 

        Pass
        {
            CGPROGRAM            

            half4 frag( v2f IN ) : COLOR
            {
                float dis = sqrt(pow(0.5-IN.uv.x,2) + pow(0.5-IN.uv.y ,2));
                half4 color = half4(0,0,0,0);
                
                half circleHalf = (_Radius+_HollowRadius)*.5;
                if( dis > _Radius ) 
                {
                    float rate = antialias(_Radius,_BorderWidth,dis);
                    color = lerp(IN.color,_BorderColor,rate);
                }
                else if(dis > circleHalf && dis < _Radius )
                {
                    float rate = antialias(_Radius,_BorderWidth,dis);
                    color = lerp(IN.color,_BorderColor,rate);
                }
                else
                {
                    float rate = antialias1(_HollowRadius,_BorderWidth,dis);
                    color = lerp(IN.color,_BorderColor,rate);
                }

                float2 remapUV = IN.uv *2.0 + -1.0;
                float percent = 1 - ceil((atan2(remapUV.g,remapUV.r) / (3.1415926 *2) + 0.5) - _Value); 
                float4 texColor = tex2D(_Texture,IN.uv);
                return half4( color.r * _Color.r * texColor.r, 
                            color.g * _Color.g * texColor.g,
                            color.b * _Color.b * texColor.b,
                            _Alpha * color.a * percent * texColor.a);
            }

            ENDCG
        }
    }
}