
using UnityEngine;

namespace UChart
{
    public enum E_Axis
    {
        X,
        Y,
        Z
    };

    public class RotateTween : MonoBehaviour
    {
        public E_Axis rotateAxis = E_Axis.Y;
        public float rotationSpeed = 30;

        private void Update()
        {
            Vector3 direaction = Vector3.up;
            if(rotateAxis == E_Axis.Y)
                direaction = Vector3.up;
            if(rotateAxis == E_Axis.X)
                direaction = Vector3.forward;
            if(rotateAxis == E_Axis.Z)
                direaction = Vector3.right;
            transform.Rotate(direaction,rotationSpeed);
        }
    }
}