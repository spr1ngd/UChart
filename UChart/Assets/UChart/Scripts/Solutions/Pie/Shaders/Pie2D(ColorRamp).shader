
Shader "UChart/Pie/2D(ColorRamp)"
{
    Properties
    {
        [HideInInspector] _MainTex("Main Texture",2D) = "white"{}
        _Percent ("Percent",range(0,1)) = 1
        _Alpha ("Alpha",range(0,1)) = 0.5

        _Radius ("Pie Radius Percent",range(0,0.5)) = 0.5
        _HollowRadius ("Hollow Radius",range(0,0.5)) = 0.2
        _BorderWidth ("Border Width",range(0.0001,0.02)) = 0.01
        _BorderColor ("Border Color",COLOR) = (0,0,0,0)

        _StartColor ("Start Color",COLOR) = (0.2,0.1,1,1)
        _EndColor ("End Color",COLOR) = (0.2,1,1,1)

        _UVRotation ("UV Rotation",float) = 0
    }

    SubShader
    {
        Tags{"RenderType"="Transparent" "Queue"="Transparent"}
        ZTest [unity_GUIZTestMode]

        CGINCLUDE

        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"    
        #define PI 3.1415926
        #define hPI 3.1415926*0.5

        float _Percent;
        float _Alpha;

        float _Radius;
        float _HollowRadius;

        float _BorderWidth;
        float4 _BorderColor;

        float4 _StartColor;
        float4 _EndColor;

        float _UVRotation;

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
            float angle = (_UVRotation + 90)/ 180 * PI;
            IN.uv.xy -= 0.5;
            float2x2 rMatrix = float2x2(cos(angle),-sin(angle),
                                        sin(angle),cos(angle));
            OUT.uv.xy = mul(rMatrix,IN.uv.xy) + 0.5;
            return OUT;
        }

        ENDCG 

        // draw start & end point.
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM

            half4 frag( v2f IN ) : COLOR
            {
                float hRadius = (_Radius-_HollowRadius)*0.5;
                float realRadius = hRadius +_HollowRadius;
                float startRadian = -hPI;
                float offsetRaian = 2 * hRadius / (realRadius * 2 * PI);
                float endRadian =  _Percent * 2 * PI - hPI;             
                float2 startPos = float2(sin(startRadian),cos(startRadian)) * realRadius + float2(0.5,0.5);
                float2 endPos = float2(sin(endRadian),cos(endRadian)) * (hRadius +_HollowRadius ) + float2(0.5,0.5);
                float startDis = sqrt(pow(startPos.x - IN.uv.x,2) + pow(startPos.y - IN.uv.y,2));
                float endDis = sqrt(pow(endPos.x - IN.uv.x,2) + pow(endPos.y - IN.uv.y,2));
                float startRate = antialias(hRadius,_BorderWidth,startDis);
                float endRate = antialias(hRadius,_BorderWidth,endDis);
                half4 startColor = half4(0,0,0,0);
                half4 endColor = half4(0,0,0,0);
                half4 start = lerp(_StartColor,_BorderColor,startRate);
                half4 end = lerp(_EndColor,_BorderColor,endRate);     

                if( IN.uv.x <= 0.5)
                {
                    float2 left = float2(-1,0);
                    float dotStart = dot(left,normalize( startPos-float2(0.5,0.5) ));
                    float dotEnd = dot(left,normalize(endPos -float2(0.5,0.5)));
                    float dot2 = dot(left,normalize(IN.uv.xy -float2(0.5,0.5)));
                    if( IN.uv.y <= 0.5 ) // third 
                    {
                        if( dot2 < dotStart )
                            startColor = start;
                        if( dot2 > dotEnd && _Percent > 0.1)
                            endColor = end;
                    }else // fourth 
                    {
                        if( dot2 > dotStart )
                            startColor = start;
                        if( dot2 < dotEnd )
                            endColor = end;
                    }
                }
                else
                {
                    float2 right = float2(1,0);
                    float dotStart = dot(right,normalize(startPos-float2(0.5,0.5)));
                    float dotEnd = dot(right,normalize(endPos -float2(0.5,0.5)));
                    float dot2 = dot(right,normalize(IN.uv.xy -float2(0.5,0.5)));
                    if( IN.uv.y <= 0.5 ) // second
                    {
                        if( dot2 > dotStart) 
                            startColor = start;
                        if( dot2 < dotEnd )
                            endColor = end; 
                    }
                    else // first
                    {
                        if( dot2 < dotStart )    
                            startColor = start;
                        if( dot2 > dotEnd )
                            endColor = end; 
                    }
                }

                half4 col = startColor + endColor;
                return half4(col.rgb,_Alpha * col.a * IN.color.a);
            }

            ENDCG
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM            

            half4 frag( v2f IN ) : COLOR
            {
                float hRadius = (_Radius-_HollowRadius)*0.5;
                float realRadius = hRadius +_HollowRadius;
                float offsetRaian = 2 *hRadius / (realRadius * 2 * PI);
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
                float at2 = 1 - (atan2(remapUV.g,remapUV.r) / (PI *2) + 0.5);
                float percent = 1- ceil( at2 - _Percent);
              
                float4 _Color = lerp(_StartColor,_EndColor,at2 / _Percent );
                return color = half4( color.r * _Color.r, color.g * _Color.g,color.b * _Color.b,_Alpha * color.a * percent );
            }

            ENDCG
        }
    }
}