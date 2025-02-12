using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float collisionOffset = 0.001f;
    public ContactFilter2D movementFilter;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isAttacking = false; 
    private int killCount = 0; 
    public float growthFactor = 0.1f;

    private PlayerInput playerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>(); // Get Player Input component
    }

    private void FixedUpdate()
    {
        if (isAttacking) return; // Prevent movement when attacking

        bool success = false;

        if (movementInput != Vector2.zero)
        {
            success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
        }

        // Update animation state
        animator.SetBool("isMoving", success);

        // Flip sprite based on movement direction
        if (movementInput.x < 0) spriteRenderer.flipX = true;
        else if (movementInput.x > 0) spriteRenderer.flipX = false;
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
        }
        return false;
    }

    // New Input System Movement Callback
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    // New Input System Attack Callback
    void OnAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");

            // Optional: Reset attack state after animation finishes
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Adjust duration to match attack animation
        isAttacking = false;
    }
    [SerializeField] private TMPro.TextMeshProUGUI killCountText;   
    
    public void KillEnemy()
    {
        killCount++;
        Debug.Log($"Enemies Killed: {killCount}");
        transform.localScale += new Vector3(growthFactor, growthFactor, 0);
        
        if (killCountText != null)
        {
            killCountText.text = $"Kills: {killCount}";
        } 
    }
}
