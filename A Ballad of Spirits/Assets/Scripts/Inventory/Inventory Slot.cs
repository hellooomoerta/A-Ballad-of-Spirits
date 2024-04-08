using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    public WeaponSO GetWeaponInfo()
    {
        return weaponSO;
    }
}
