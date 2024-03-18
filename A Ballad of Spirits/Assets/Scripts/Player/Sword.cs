using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] GameObject slashEffectPrefab;
    [SerializeField] Transform SlashAnimSpawnPoint;
    [SerializeField] Transform swordCollider;

    GameObject slashAnim;
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
        swordCollider.gameObject.SetActive(false);
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
        swordCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashEffectPrefab, SlashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        swordCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(playerController.transform.position);

        if (mousePos.x < playerPosition.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            swordCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            swordCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
}
