/// 1.鼠标拖拽旋转功能（以世界中心为target，可自由赋值）
/// 2.滚轮操控视距功能
/// 3.按下滚轮滑动操控视角范围

using UnityEngine;

namespace UChart.Com
{
    [ExecuteInEditMode]
    public class EasyCamera : MonoBehaviour
    {
        private Transform m_camera = null;

        public Transform target = null;
        public float xSpeed = 5.0f;
        public float ySpeed = 2.0f;

        [Range(0.1f,200)]
        public float distance = 15;

        [Range(1,5)]
        public float damp = 1;

        public float yMinLimit = -15;
        public float yMaxLimit = 175;

        private float x = 0,y = 0;

        private void Awake()
        {
            m_camera = Camera.main.transform;
        }

        private void LateUpdate()
        {          
            // mouse left button
            if(Input.GetMouseButton(0))
            {
                float dx = Input.GetAxis("Mouse X");
                float dy = Input.GetAxis("Mouse Y");

                x += dx * xSpeed * Time.deltaTime;
                y -= dy * ySpeed * Time.deltaTime;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            // mouse right button
            if(Input.GetMouseButton(1))
            {

            }

            // mouse scroll button
            if(Input.GetMouseButton(2))
            {

            }

            var scrollValue = Input.GetAxis("Mouse ScrollWheel");
            if(0 != scrollValue)
            {
                distance += scrollValue * damp;
            }

            float fx = Mathf.Lerp(m_camera.eulerAngles.x, x, 0.2f);
            float fy = Mathf.Lerp(m_camera.eulerAngles.y, y, 0.2f);

            UpdateCameraState(fx,fy);
        }

        private void UpdateCameraState(float x,float y)
        {
            var targetPos = target == null? Vector3.zero:target.position;
            Quaternion rotation = Quaternion.Euler(x, y, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, - distance);
            Vector3 position = rotation * negDistance + targetPos;
            transform.rotation = rotation;
            transform.position = position;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}