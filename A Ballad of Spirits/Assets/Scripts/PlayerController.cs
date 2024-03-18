using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Vector2 movement;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D rb;
    PlayerControls playerControls;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        FlipSprite();
    }

    void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>().normalized;

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    void FlipSprite()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerPosition.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
}
