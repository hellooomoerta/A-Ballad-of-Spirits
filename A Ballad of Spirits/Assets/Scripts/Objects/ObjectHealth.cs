using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;

    float currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
