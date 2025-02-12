using System;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public GameObject Player;

    
    private Vector2 movementDirection;
    public Rigidbody2D Rb { get; private set; }
    
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        movementDirection = (targetPosition - Rb.position).normalized;
       
    }

    public void FollowTarget(Vector2 targetPosition)
    {
        MoveTo(targetPosition);
    }

    public void StopMovement()
    {
        movementDirection = Vector2.zero;
        Rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsMoving", false);
    }
    
}
