
Shader "UChart/Pie/2D(Basic)"
{
    Properties
    {
        _MainColor ("Main Color(RGB)",COLOR) = (1,1,1,1)
        _Alpha ("Alpha",range(0,1)) = 0.5

        _Radius ("Pie Radius Percent",range(0,1)) = 0.5
        _HollowRadius ("Hollow Radius",range(0,0.5)) = 0.2
        _BorderWidth ("Border Width",range(0,0.2)) = 0.05
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

        float4 _MainColor;
        float _Alpha;

        float _Radius;
        float _HollowRadius;

        float _BorderWidth;
        float4 _BorderColor;

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
            return smoothstep(radius - bordersize,radius+bordersize,distance);
        }

        float antialias1( float radius,float bordersize,float distance )
        {
            return smoothstep(radius + bordersize,radius-bordersize,distance);
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
                // 最外圈抗锯齿虚化
                if( dis > _Radius ) 
                {
                    // float rate = antialias(_Radius,_BorderWidth,dis);
                    // color = lerp(IN.color,_BorderColor,rate);
                }

                // 最内圈抗锯齿虚化 
                else if(dis < _HollowRadius)
                {
                    // float rate = antialias1(_HollowRadius,_BorderWidth,dis);
                    // color = lerp(IN.color,_BorderColor,rate);
                }
                // 中间部分，两端虚化// TODO: 异常
                else
                {
                    float rate1 = antialias(_Radius,_BorderWidth,dis);
                    float4 c1 = lerp(IN.color,_BorderColor,rate1) ;
                    float rate2 = antialias(_HollowRadius,_BorderWidth,dis);
                    float4 c2 = lerp(IN.color,_BorderColor,rate2) ;
                    color = c1+c2;
                }
                return half4(color.r,color.g,color.b,_Alpha * color.a);
            }

            ENDCG
        }
        //  Pass
        // {
        //     CGPROGRAM

        //     half4 frag(v2f IN ):COLOR
        //     {
        //         float dis = sqrt(pow(0.5-IN.uv.x,2) + pow(0.5-IN.uv.y ,2));
        //         half4 color = half4(0,0,0,0);

        //         if( dis > _HollowRadius && dis < _Radius)
        //         {
        //             color = IN.color;
        //         }              
        //         return half4(color.r,color.g,color.b,_Alpha * color.a);
        //     }

        //     ENDCG
        // }
    }
}