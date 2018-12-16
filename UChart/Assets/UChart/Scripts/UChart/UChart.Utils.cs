
using UnityEngine;

namespace UChart
{
    /// <summary>
    /// UChart创建工具
    /// 1. 创建点
    /// </summary>
    public partial class UChart
    {
        // TODO: refactor
        public static Point CreatePoint( PointType pointType , Vector3 pos )
        {
            return new Point();
        }

        // TODO: refactor
        public static Point2D CreatePoint2D( Vector3 pos )
        {
            return (Point2D)CreatePoint(PointType.Point2D,pos);
        }

        // TODO: refactor
        public static Point3D CreatePoint3D( Vector3 pos )
        {
            return (Point3D)CreatePoint(PointType.Point3D,pos);
        }

        /// <summary>
        /// Int convert to Color without alpha
        /// </summary>
        /// <param name="intColor"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color Int2Color(int intColor,float alpha = 1)
        {
            int colorR = 0, colorG = 0, colorB = 0;
            colorR = intColor / (256 * 2);
            colorG = intColor / 256 % 256;
            colorB = intColor % 256;
            return new Color(colorR / 255.0f,colorG / 255.0f,colorB / 255.0f,alpha);
        }
    }
}