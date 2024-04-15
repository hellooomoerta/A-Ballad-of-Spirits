using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float dashSpeed = 4f;
    [SerializeField] float dashTime = .2f;
    [SerializeField] TrailRenderer myTrailRenderer;
    [SerializeField] Transform weaponCollider;
    [SerializeField] Transform slashAnimSpawnPoint;

    float defaultMoveSpeed;
    bool facingLeft = false;
    bool isDashing = false;
    Vector2 movement;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D rb;
    Knockback knockback;
    PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();

        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        playerControls = new PlayerControls();
        defaultMoveSpeed = moveSpeed;
    }

    private void Start()
    {
        playerControls.Movement.Dash.performed += _ => Dash();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    public Transform GetSlashAnimSpawnPoint()
    {
        return slashAnimSpawnPoint;
    }

    void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>().normalized;

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    void Move()
    {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerPosition.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            myTrailRenderer.emitting = true;
            moveSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
    }

    IEnumerator EndDashRoutine()
    {
        float dashCD = dashTime + 0.05f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = defaultMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
}
