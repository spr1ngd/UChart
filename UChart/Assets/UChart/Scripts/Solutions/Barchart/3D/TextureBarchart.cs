
using System;
using UnityEngine;
using System.Collections;

namespace UChart
{
    // 固定像素尺寸模式
    // 按法线计算高度（应该会更加真实）
    // TODO: 如何获取文字顶点数据
    // TODO: texture转化至normal map算法原理
    public class TextureBarchart : Barchart3D
    {
        [Header("TEXTURE SETTING (prioritize display url resource. )")]
       
        public string textureURL;
        public Texture2D texture;

        public bool fixedResolution = false;
        public Vector2 resolution = Vector2.zero;

        private IEnumerator downloadTexture( Action downloadCallback = null )
        {
            WWW www = new WWW(textureURL);
            yield return www;
            if( !string.IsNullOrEmpty(www.error) )
            {
                Debug.LogError("download texture error : " + www.error);
                yield break;
            }
            if(www.texture) 
            {
                texture = www.texture;
                if( null != downloadCallback )
                    downloadCallback();
            }
        }

        private void AnalysisTexture()
        {
            if( null == texture )
            {
                Debug.Log("texture can't be null,<color=red>please assign texture asset on inspector window.</color>");
                return;
            }

            // TODO: 完善颜色取值的算法
            // TODO: 完善固定尺寸对于平铺、适应、拉伸的情况的算法
            if( fixedResolution )
            {
                int width = (int)(resolution.x);
                int height = (int)(resolution.y);
                datas = new float[width,height];
                colors = new Color[width,height];

                int offset = Mathf.CeilToInt(texture.width / width);
                int offsetHeight = Mathf.CeilToInt(texture.height / height);
                if( offsetHeight > offset )
                    offset = offsetHeight;

                for( int x = 0 ,pixelX = 0; x < width; x++,pixelX+=offset ) 
                {
                    for( int y = 0 ,pixelY =0 ; y < height;y++,pixelY+=offset )
                    {
                        Color pixel = texture.GetPixel(pixelX,pixelY);
                        float weight = (new Vector3( 1- pixel.r,1-pixel.g,1-pixel.b) * 2 - Vector3.one).z  * 2;
                        colors.SetValue(pixel,x,y);
                        datas.SetValue(weight,x,y);
                    }
                }
            }
            else
            {
                int width = texture.width;
                int height = texture.height;
                datas = new float[width,height];
                colors = new Color[width,height];

                for( int x = 0 ; x < width; x++ ) 
                {
                    for( int y = 0 ; y < height;y++ )
                    {
                        Color pixel = texture.GetPixel(x,y);
                        float weight = (new Vector3( 1- pixel.r,1-pixel.g,1-pixel.b) * 2 - Vector3.one).z * 2;                    
                        colors.SetValue(pixel,x,y);
                        datas.SetValue(weight,x,y);
                    }
                }
            }            
        }

        public override void Draw()
        {
            if( string.IsNullOrEmpty(textureURL) )
            {
                this.AnalysisTexture();
                base.Draw();
            }
            else
            {
                StartCoroutine(downloadTexture(()=>
                {
                    this.AnalysisTexture();
                    base.Draw();
                }));
            }
        }
    }
}