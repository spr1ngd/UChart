
// UChart cg library.

#ifndef UCHART_CG_INCLUDE
#define UCHART_CG_INCLUDE

#include "UnityCG.cginc"

float4 UnityObjectToScreenPos( float4 vertex )
{
    return mul(UNITY_MATRIX_P,mul(UNITY_MATRIX_MV,float4(0,0,0,1)) + float4(vertex.x,vertex.y,0,0));    
}

#endif