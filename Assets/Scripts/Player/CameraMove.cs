using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // Assign the player's transform here in the inspector
    public float smoothSpeed = 0.125f; // Adjust for smoother or more immediate follow

    void FixedUpdate()
    {
        if (target == null) return;

        // Determine the desired position based on target's position and offset
        Vector3 desiredPosition = target.position;
        
        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

    }
}
