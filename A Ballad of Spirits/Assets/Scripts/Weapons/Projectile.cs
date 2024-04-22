using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float projectileRange = 10f;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private GameObject particelOnHitPrefabVFX;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateProjectileMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    void MoveProjectile()
    {
        transform.Translate(transform.right * Time.deltaTime * moveSpeed, Space.World);
    }

    void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.GetComponent<Indestructible>();
        ObjectHealth objectHealth = other.GetComponent<ObjectHealth>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (enemyHealth || indestructible || player)
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(particelOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (!other.isTrigger && indestructible)
            {
                Instantiate(particelOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
        else if (!other.isTrigger && objectHealth)
        {
            Instantiate(particelOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
