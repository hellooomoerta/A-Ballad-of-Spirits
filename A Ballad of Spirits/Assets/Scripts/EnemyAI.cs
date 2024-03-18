using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    State state;
    EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        state = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
