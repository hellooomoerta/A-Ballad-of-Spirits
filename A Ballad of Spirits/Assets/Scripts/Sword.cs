using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator myAnimator;
    PlayerControls playerControls;
    PlayerController playerController;
    ActiveWeapon activeWeapon;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }

    void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(playerController.transform.position);

        if (mousePos.x < playerPosition.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
}
