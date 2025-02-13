using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0f, 0f, -10f); 
    public float smoothSpeed = 0.125f; 
    private Vector3 velocity = Vector3.zero; 

    void LateUpdate()
    {
        if (target == null) return; 

        
        Vector3 desiredPosition = target.position + offset;

        
        desiredPosition.z = transform.position.z;

       
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, desiredPosition.x, smoothSpeed),
                                         Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeed),
                                         transform.position.z);
    }
}
