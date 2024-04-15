using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponSO weaponInfo;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnpoint;

    Animator myAnimator;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnpoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }

    public WeaponSO GetWeaponInfo()
    {
        return weaponInfo;
    }
}
