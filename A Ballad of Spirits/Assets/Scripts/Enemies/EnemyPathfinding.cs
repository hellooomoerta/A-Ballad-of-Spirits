using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    Vector2 moveDirection;
    Rigidbody2D rb;
    Knockback knockback;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (knockback.GettingKnockedBack)
        {
            return;
        }

        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
}
