
using UnityEngine;
using UChart;

namespace UChart.Example
{
    public class Test_Point2D : MonoBehaviour
    {
        private void Start()
        {
            GameObject point = new GameObject();
            Point2D point2d = point.AddComponent<Point2D>();
            point2d.Init();
            point2d.transform.SetParent(this.transform);
        }
    }
}