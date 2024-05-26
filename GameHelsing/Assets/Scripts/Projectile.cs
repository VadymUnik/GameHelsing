using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;

//using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float moveSpeed;
    private float lifeTime;
    private float damage;
    private float timeSinceBirth;

    [SerializeField] private Rigidbody2D rb;
    private Camera mainCam;
    private Vector3 mousePos;
    
    void Start()
    {
        // mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 direction = mousePos - transform.position;
        // rb.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
        rb.velocity = transform.right * moveSpeed;
    }
    void FixedUpdate()
    {
        timeSinceBirth += Time.deltaTime;
        if (timeSinceBirth >= lifeTime)
        {
            DestroyProjectile();
        }
    }
    public void SetParameters(float moveSpeed, float lifeTime, float damage)
    {
        this.moveSpeed = moveSpeed;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}

