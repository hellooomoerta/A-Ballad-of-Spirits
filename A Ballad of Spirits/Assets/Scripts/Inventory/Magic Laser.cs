using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;

    private float laserRange;
    private bool isGrowing = true;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D myCapsuleCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserRangeRoutine());
    }

    private IEnumerator IncreaseLaserRangeRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            //Sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            //Collider
            myCapsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), myCapsuleCollider.size.y);
            myCapsuleCollider.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, myCapsuleCollider.offset.y);

            yield return null;
        }
        Destroy(myCapsuleCollider);
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            isGrowing = false;
        }
    }
}
