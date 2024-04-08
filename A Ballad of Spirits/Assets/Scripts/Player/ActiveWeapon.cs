using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    bool attackButtonDown, isAttacking = false;

    PlayerControls playerControls;


    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();

    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void StartAttacking()
    {
        attackButtonDown = true;
    }

    void StopAttacking()
    {
        attackButtonDown = false;
    }

    void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
        isAttacking = true;
        (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
