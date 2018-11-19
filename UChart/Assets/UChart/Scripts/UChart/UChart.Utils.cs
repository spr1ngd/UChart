
using UnityEngine;

namespace UChart
{
    /// <summary>
    /// UChart创建工具
    /// 1. 创建点
    /// </summary>
    public partial class UChart
    {
        public static Point CreatePoint( PointType pointType , Vector3 pos )
        {
			return new Point();
        }

        public static Point2D CreatePoint2D( Vector3 pos )
        {
            return (Point2D)CreatePoint(PointType.Point2D,pos);
        }

        public static Point3D CreatePoint3D( Vector3 pos )
        {
            return (Point3D)CreatePoint(PointType.Point3D,pos);
        }
    }
}