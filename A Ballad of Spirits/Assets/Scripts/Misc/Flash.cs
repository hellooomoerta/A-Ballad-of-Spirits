using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] Material whiteFlashMaterial;
    [SerializeField] float restoreDefaultMaterialTime = .1f;

    SpriteRenderer spriteRenderer;
    Material defaultMaterial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(restoreDefaultMaterialTime);
        spriteRenderer.material = defaultMaterial;
    }

    public float GetRestoreDefaultMaterialTime()
    {
        return restoreDefaultMaterialTime;
    }
}
