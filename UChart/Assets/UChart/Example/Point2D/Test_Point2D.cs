
using UnityEngine;

namespace UChart.Example
{
    public class Test_Point2D : MonoBehaviour
    {
        private void Start()
        {
            GameObject point = new GameObject();
            Point2D point2d = point.AddComponent<Point2D>();
            point2d.transform.SetParent(this.transform.Find("Parent"));
            point2d.transform.localPosition = Vector3.zero;
            point2d.Init();

            point2d.SetColor(Color.white);
            point2d.SetSize(20);
            point2d.SetTexture(Resources.Load<Texture2D>("Points/Point2"));
        }
    }
}