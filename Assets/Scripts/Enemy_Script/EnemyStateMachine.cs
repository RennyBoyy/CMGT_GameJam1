using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
  private enum State
  {
    roaming,
    following
  }

  private State state;
  private EnemyPathFinding enemyPathFinding;

  private void Awake()
  {
    enemyPathFinding = GetComponent<EnemyPathFinding>();
    state = State.roaming;
  }

  private void Start()
  {
    StartCoroutine(RoamingCorutine());
  }

  private IEnumerator RoamingCorutine()
  {
    while(state == State.roaming)
    {
      Vector2 roamPosition = GetRoamPosition();
      enemyPathFinding.MoveTo(roamPosition);
      yield return new WaitForSeconds(2f);
    }
  }

  private Vector2 GetRoamPosition()
  {
    return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
  }
}
