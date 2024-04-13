using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        transform.Translate(transform.right * Time.deltaTime * moveSpeed, Space.World);
    }
}
