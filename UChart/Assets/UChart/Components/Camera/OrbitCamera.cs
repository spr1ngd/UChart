using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 8.0f;
    public float xSpeed = 70.0f;
    public float ySpeed = 50.0f;

    public float yMinLimit = 0f;
    public float yMaxLimit = 90f;

    public float distanceMin = 8f;
    public float distanceMax = 15f;
    public float zoomSpeed = 0.5f;
    
    private float x = 0.0f;
    private float y = 0.0f;

    private float fx = 0f;
    private float fy = 0f;
    private float fDistance = 0;

    [Header("Scroll wheel")]
    public float mouseWheelSpeed = 2.0f;
    public float minDistance = 0.5f;
    public float maxDistance = 50.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        fx = x;
        fy = y; 
        UpdateRotaAndPos();
        fDistance = distance;
    }

    void Update()
    {
        //if (Input.touchCount == 2)
        //{
        //    // Store both touches.
        //    Touch touchZero = Input.GetTouch(0);
        //    Touch touchOne = Input.GetTouch(1);

        //    // Find the position in the previous frame of each touch.
        //    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        //    // Find the magnitude of the vector (the distance) between the touches in each frame.
        //    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        //    // Find the difference in the distances between each frame.
        //    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
        //    fDistance = Mathf.Clamp(distance + deltaMagnitudeDiff * zoomSpeed, distanceMin, distanceMax);
        //}
        //distance = Mathf.Lerp(distance, fDistance, 0.25f);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && Input.touchCount < 2)
        {
            if (target)
            {
                float dx = Input.GetAxis("Mouse X");
                float dy = Input.GetAxis("Mouse Y");
                if (Input.touchCount > 0)
                {
                    dx = Input.touches[0].deltaPosition.x;
                    dy = Input.touches[0].deltaPosition.y;
                }

                x += dx * xSpeed * Time.deltaTime; //*distance
                y -= dy * ySpeed * Time.deltaTime;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

               
            }
        }

        //OnMouseWheel();

        fx = Mathf.Lerp(fx,x,0.2f);
        fy = Mathf.Lerp(fy,y,0.2f);

        UpdateRotaAndPos();
    }

    private void OnMouseWheel()
    {
        if(target)
        {
            float wheelValue = Input.GetAxis("Mouse ScrollWheel");
            Vector3 direction = Vector3.Normalize(this.transform.position - target.position);
            Vector3 offset = direction * wheelValue * mouseWheelSpeed;
            this.transform.position += offset;
        }
    }


    void UpdateRotaAndPos()
    {
        if (target)
        {
            Quaternion rotation = Quaternion.Euler(fy, fx, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}