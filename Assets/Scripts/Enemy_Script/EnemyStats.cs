using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public GameObject playerToFollow;
    public float Health;
    public float MaxHealth;
    public float Speed;
    public float MoveSpeed;
    void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
            
    }
}
