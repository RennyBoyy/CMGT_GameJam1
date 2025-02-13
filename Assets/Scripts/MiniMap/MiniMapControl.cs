using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player == null) return;

        
        transform.position = new Vector3(
            Mathf.Round(player.position.x * 100) / 100,
            Mathf.Round(player.position.y * 100) / 100,
            transform.position.z
        );
    }
}
