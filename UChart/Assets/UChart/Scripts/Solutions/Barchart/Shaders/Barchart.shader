
Shader "UChart/Barchart/Simple"
{
	Properties
	{
		_Color("Color(RGB)",COLOR) = "white"{}
		_Alpha("Alpha",range(0,1)) = 0.6 
	}

	SubShader
	{
		Tags{"Queue"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting off
		
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct a2v
			{
				
			};

			struct v2f
			{

			};

			v2f vert( a2v IN )
			{
				v2f OUT;
				return OUT:
			}

			fixed4 frag(v2f IN):COLOR
			{
				fixed4 color = fixed4(1,1,1,1);
				return color;
			}

			ENDCG
		}
	}
}