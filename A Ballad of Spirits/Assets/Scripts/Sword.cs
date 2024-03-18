using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator myAnimator;
    PlayerControls playerControls;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
}
