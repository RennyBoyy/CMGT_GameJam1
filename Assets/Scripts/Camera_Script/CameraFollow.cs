using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player object to follow
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Adjusted offset to make the camera farther from the player
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement
    private Vector3 velocity = Vector3.zero; // Velocity for SmoothDamp

    void LateUpdate()
    {
        if (target == null) return; // If no target is assigned, do nothing

        // Desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        // Lock the Z-axis position (to prevent shaking in 2D games)
        desiredPosition.z = transform.position.z;

        // Use SmoothDamp for smooth camera following
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Additional optional feature: make sure the camera does not overshoot too much
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, desiredPosition.x, smoothSpeed),
                                         Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeed),
                                         transform.position.z);
    }
}
