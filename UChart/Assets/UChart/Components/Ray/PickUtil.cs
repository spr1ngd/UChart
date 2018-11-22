
using UnityEngine;

namespace UChart
{
    public class PickUtil
    {
        public static Vector3 Pick()
        {
            // todo 1.摄像机中心发射射线
            //Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
            //RaycastHit hitInfo;
            //if (Physics.Raycast(ray, out hitInfo))
            //{

            //    UnityEngine.Debug.Log(string.Format("Layer : [{0}] , Triangle index : [{1}]",
            //        hitInfo.transform.gameObject.layer + "__" + hitInfo.transform.name,
            //        hitInfo.triangleIndex));
            //}

            // todo 2.随鼠标位置发射射线
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = new Vector3(mousePosition.x,mousePosition.y,20);
            var worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            //UnityEngine.Debug.Log(mousePosition +"[--]"+worldPos);
            return worldPos;
        }
    }
}