using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    int activeSlotIndexNum = 0;

    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHighlight(0); //Ser till att spelaren startar med svärdet spawnat
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void ToggleActiveSlot (int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab; //Tilldelar weaponToSpawn det aktiva vapnet
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity); //Instansierar det aktiva vapnet
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform; //Lägger det nya vapnet som ett barn till ActiveWeapon i hierarkin
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
