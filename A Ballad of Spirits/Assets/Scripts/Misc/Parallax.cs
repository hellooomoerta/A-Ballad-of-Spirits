using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxOffset = -0.15f;

    Camera mainCam;
    Vector2 startPosition;
    Vector2 travel => (Vector2)mainCam.transform.position - startPosition;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPosition + travel * parallaxOffset;
    }
}
