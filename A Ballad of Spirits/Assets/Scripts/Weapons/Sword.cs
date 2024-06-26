using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject slashEffectPrefab;
    [SerializeField] Transform slashAnimSpawnPoint;
    //[SerializeField] float swordAttackCD = 0.2f;
    [SerializeField] WeaponSO weaponInfo;

    Transform weaponCollider;
    Animator myAnimator;
    GameObject slashAnim;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = PlayerController.Instance.GetSlashAnimSpawnPoint();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponSO GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashEffectPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        if (mousePos.x < playerPosition.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
