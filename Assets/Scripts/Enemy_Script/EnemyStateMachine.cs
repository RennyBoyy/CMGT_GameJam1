using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStateMachine : MonoBehaviour
{
  private enum State
  {
    roaming,
    following,
    attacking,
    death
  }

  private State state;
  private EnemyPathFinding enemyPathFinding;

  [SerializeField] private float stoppingDistance = 0.5f;
  [SerializeField] private float roamingRadius = 5f;

  private void Awake()
  {
    enemyPathFinding = GetComponent<EnemyPathFinding>();
    state = State.roaming;
  }

  private void Start()
  {
    StartCoroutine(RoamingCorutine());
  }

  private void Update()
  {
    if (state == State.following)
    {
      Debug.Log("Following Player");
      enemyPathFinding.animator.SetBool("IsMoving", true);
      Vector2 playerPosition = enemyPathFinding.Player.transform.position;

      float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

      if (distanceToPlayer > stoppingDistance)
      {
        enemyPathFinding.FollowTarget(playerPosition);
      }
      else
      {
        enemyPathFinding.StopMovement();
        Debug.Log("Enemy stopped near Player");
      }
    }

    if (state == State.death)
    {
      enemyPathFinding.animator.SetBool("isDead", true);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Something entered trigger");
    if (other.CompareTag("Player"))
    {
      Debug.Log("Player entered");
      state = State.following;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log("Player exited");
      state = State.roaming;
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    Debug.Log("Collision entered");
    if (other.gameObject.CompareTag("Player"))
    {
      Debug.Log("Dying");
      state = State.death;
      StartCoroutine(DeathCorutine());
    }
  }

  private IEnumerator DeathCorutine()
  {
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
  }

  private IEnumerator RoamingCorutine()
  {
    while (true)
    {
      if (state == State.roaming)
      {
        enemyPathFinding.animator.SetBool("IsMoving", true);
        Vector2 roamPosition = GetRoamPosition();
        float roamDuration = Random.Range(1f, 3f);
        float elapsedTime = 0f;
        while (elapsedTime < roamDuration && state == State.roaming)
        {
          enemyPathFinding.MoveTo(roamPosition);
          elapsedTime += Time.deltaTime;
          yield return null;
        }

        enemyPathFinding.StopMovement();
        yield return new WaitForSeconds(0.5f);
      }
      else
      {
        yield return null;
      }
    }
  }

  private Vector2 GetRoamPosition()
  {
    Vector2 randomDirection = Random.insideUnitCircle.normalized * roamingRadius;
    return enemyPathFinding.Rb.position + randomDirection;
  }
}
  

