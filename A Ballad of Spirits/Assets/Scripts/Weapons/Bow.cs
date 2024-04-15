using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponSO weaponInfo;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnpoint;
    [SerializeField] Transform arrowSpawnpoint2;
    [SerializeField] Transform arrowSpawnpoint3;

    Animator myAnimator;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        int multishot = Random.Range(1, 4);

        if (multishot == 1)
        {
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnpoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        }
        else if (multishot == 2)
        {
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnpoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
            GameObject newArrow2 = Instantiate(arrowPrefab, arrowSpawnpoint2.position, ActiveWeapon.Instance.transform.rotation);
            newArrow2.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        }
        else if (multishot == 3)
        {
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnpoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
            GameObject newArrow2 = Instantiate(arrowPrefab, arrowSpawnpoint2.position, ActiveWeapon.Instance.transform.rotation);
            newArrow2.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
            GameObject newArrow3 = Instantiate(arrowPrefab, arrowSpawnpoint3.position, ActiveWeapon.Instance.transform.rotation);
            newArrow3.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        }
    }

    public WeaponSO GetWeaponInfo()
    {
        return weaponInfo;
    }
}
