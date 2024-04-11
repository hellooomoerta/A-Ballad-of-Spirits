using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponSO weaponInfo;

    public void Attack()
    {

    }

    public WeaponSO GetWeaponInfo()
    {
        return weaponInfo;
    }
}
