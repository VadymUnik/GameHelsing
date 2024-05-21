using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        rb.velocity = transform.right * moveSpeed;
    }
    void DestroyProjectile()
    {
        
    }
}

