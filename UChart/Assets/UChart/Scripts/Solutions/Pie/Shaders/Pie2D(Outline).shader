
Shader "UChart/Pie/2D(Outline)"
{
    Properties
    {
        [Toggle(OUTLINE)] _Outline ("Outline", Int) = 1
        _Value ("Percent",range(0,1)) = 1

        _Color ("Main Color(remapUVB)",COLOR) = (1,1,1,1)
        _Alpha ("Alpha",range(0,1)) = 0.5

        _Radius ("Pie Radius Percent",range(0,0.5)) = 0.5
        _HollowRadius ("Hollow Radius",range(0,0.5)) = 0.2
        _BorderWidth ("Border Width",range(0.0001,0.02)) = 0.01
        _BorderColor ("Border Color",COLOR) = (0.1,0.1,0.1,0.1)

        _OutlineThinkness ("Outline Thinkness",range(0.1,1)) = 0.2
        _OutlineWidth ("Outline Width Percent",range(0.8,1.0)) = 0.8
    }

    SubShader
    {
        Tags{"RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        CGINCLUDE

        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #pragma shader_feature OUTLINE

        float _Value;

        float4 _Color;
        float _Alpha;

        float _Radius;
        float _HollowRadius;

        float _BorderWidth;
        float4 _BorderColor;

        float _OutlineThinkness;
        float _OutlineWidth;

        struct a2v
        {
            float4 vertex : POSITION;
            float4 color : COLOR;
            float4 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float4 color : COLOR;
            float4 uv : TEXCOORD0;
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
            OUT.uv = IN.uv;
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
                    color = lerp(IN.color ,_BorderColor,rate);
                    if( dis > _OutlineWidth * _Radius )
                        color = half4(color.rgb * (1.0 + _OutlineThinkness) ,color.a);
                }
                else
                {
                    float rate = antialias1(_HollowRadius,_BorderWidth,dis);
                    color = lerp(IN.color,_BorderColor,rate);
                    if( dis < _HollowRadius * (2.0 - _OutlineWidth) && dis > _HollowRadius)
                    {
                        color = half4(color.rgb * (1.0 - _OutlineThinkness) ,color.a);
                    }
                }

                float2 remapUV = IN.uv *2.0 + -1.0;
                float percent = 1 - ceil((atan2(remapUV.g,remapUV.r) / (3.1415926 *2) + 0.5) - _Value); 
                return half4( color.r * _Color.r, color.g * _Color.g,color.b * _Color.b,_Alpha * color.a * percent);
            }

            ENDCG
        }
    }
}