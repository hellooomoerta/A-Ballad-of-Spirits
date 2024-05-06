using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float knockbackThrustAmount = 5f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private bool canTakeDamage = true;
    private float currentHealth;
    private Knockback knockback;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();

        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void HealPlayer()
    {
        currentHealth += 1;
    }

    public void TakeDamage(float damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockbackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }
}
