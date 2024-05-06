using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private float pickupDistance = 2f;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float accelerationRate = 0.1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPosition) < pickupDistance)
        {
            moveDirection = (playerPosition - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            if (moveSpeed > 0)
            {
                moveSpeed = moveSpeed - (accelerationRate * 2);
            }
            else
            {
                moveSpeed = 0f;
            }
            
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }
}
