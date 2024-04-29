using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float destryTimer = 5f;

    void Update()
    {
        destryTimer -= Time.deltaTime;

        if (destryTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
