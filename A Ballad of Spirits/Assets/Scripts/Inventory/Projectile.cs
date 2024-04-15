using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject particelOnHitPrefabVFX;

    WeaponSO weaponInfo;
    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponSO weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    void MoveProjectile()
    {
        transform.Translate(transform.right * Time.deltaTime * moveSpeed, Space.World);
    }

    void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.GetComponent<Indestructible>();
        ObjectHealth objectHealth = other.GetComponent<ObjectHealth>();

        if (enemyHealth || indestructible)
        {
            enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
            Instantiate(particelOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (!other.isTrigger && objectHealth)
        {
            Instantiate(particelOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
