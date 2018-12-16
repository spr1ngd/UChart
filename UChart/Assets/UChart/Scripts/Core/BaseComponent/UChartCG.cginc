
// UChart cg library.

#ifndef UCHART_CG_INCLUDE
#define UCHART_CG_INCLUDE

#include "UnityCG.cginc"

struct a2v
{
    float4 vertex : POSITION;
    float4 color : COLOR0;
    float2 uv : TEXCOORD1;
};

struct v2g
{
    float4 vertex : POSITION;
    float4 color : COLOR0;
    float2 uv : TEXCOORD1;
};

struct g2f
{
    float4 vertex : POSITION;
    float4 color : COLOR0;
    float2 uv : TEXCOORD1;
};

struct v2f
{
    float4 vertex : POSITION;
    float4 color : COLOR0;
    float2 uv : TEXCOORD1;
};

float4 UnityObjectToScreenPos( float4 vertex )
{
    return mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(vertex.x,vertex.y,0,0));    
}

#endif