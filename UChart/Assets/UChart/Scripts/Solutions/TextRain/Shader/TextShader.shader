
Shader "UChart/TextShader"
{
	Properties
	{
		_PixelRadius("Pixel Radius",range(0.5,1.5)) = 1.0
		_SCALE ("Scale",int) = 1
		_ClearColor ("Clear Color",COLOR) = (0.3,0.3,0.3,1.0)
		_FontColor ("Font Color",COLOR) = (1.0,1.0,0.0,1.0)
	}

	SubShader
	{
		Tags{"RenderType"="Opaque" "Queue"="Geometry"}
		Pass
		{
			CGPROGRAM 

			float4 ch_0 = float4(0x007CC6,0xD6D6D6,0xD6D6C6,0x7C0000);
			float4 ch_1 = float4(0x001030,0xF03030,0x303030,0xFC0000);

			float _PixelRadius;
			int _SCALE;
			float4 _ClearColor;
			float4 _FontColor;
			int TEXT_MODE = 1; 

			float2 remapUV;
			float2 print_Pos;

			#define MAX_INT_DIGIT 4
			#define CHAR_SIZE float2(8,12)
			#define CAHR_SPACING float2(8,12)

			#define NORMAL 1
			#define INVERT 2
			#define UNDERLINE 3

			#pragma vertex vert
			#pragma fragment frag

			struct a2v
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert( a2v IN )
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			float4 get_digit( float d )
			{
				float digit = floor(d);
				if( digit == 0.0 ) return ch_0;
				if( digit == 1.0 ) return ch_1;
				return float4(0.0,0.0,0.0,0.0);
			}

			float extract_bit( float n , float b )
			{
				b = clamp(b,-1.0,24.0);
				return floor(fmod(floor(n / pow(2.0,floor(b))),2.0)); 
			}

			float sprite( float4 ch,float2 size,float2 uv )
			{
				uv = floor(uv);
    
				//Calculate the bit to extract (x + y * width) (flipped on x-axis)
				float bit = (size.x-uv.x-1.0) + uv.y * size.x;

				//Clipping bound to remove garbage outside the sprite's boundaries.
				// bool bounds = all(LEqual(uv,float2(0.0,0.0))) && all(lessThan(uv,size));

				float pixels = 0.0;
				pixels += extract_bit(ch.x, bit - 72.0);
				pixels += extract_bit(ch.y, bit - 48.0);
				pixels += extract_bit(ch.z, bit - 24.0);
				pixels += extract_bit(ch.w, bit - 00.0);

				return pixels;
				// return bounds ? pixels : 0.0;
			}

			float char( float4 ch,float2 uv )
			{
				if(TEXT_MODE == INVERT)
				{

				}
				if(TEXT_MODE == UNDERLINE)
				{

				}
				float px = sprite(ch,CHAR_SIZE,uv); // 绘制字符
				print_Pos.x += CAHR_SPACING.x;
				return px;
			}

			
			float text_number(float number,float2 uv )
			{
				float color = 0.0;
				// MAX_INT_DIGIT 代表所能输出的最大位数的数字
				for( int i = MAX_INT_DIGIT;i >= 0;i-- )
				{
					// 依次读取数字转换成十进制后的每一位数字
					float digit = fmod(number/pow(10,i),10.0);
					if( abs(number) > pow(10.0,i) || i ==0 )
					{
						color += char(get_digit(digit),uv);
					}
				}
				return color;
			}

			float text( float2 uv )
			{
				float color = 0.0;
				TEXT_MODE = NORMAL;
				print_Pos = float2(0.0,0.0);
				color += text_number(ch_0,uv);
				return color;
			} 

			float text_char()
			{

			}

			float4 frag( v2f IN ) : COLOR
			{ 
				float2 remapUV = IN.uv.xy * 100;
				float c = (1.-distance(fmod(remapUV.xy,float2(1.0,1.0)),float2(0.5,0.5)))*_PixelRadius;
				float p = text(remapUV);
				float4 color = lerp(_ClearColor,_FontColor,p);
				return color*c;
			} 

			ENDCG
		}
	}
}