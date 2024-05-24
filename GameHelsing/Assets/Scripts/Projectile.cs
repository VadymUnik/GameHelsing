using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;

//using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    private float timeSinceBirth;
    private Camera mainCam;
    private Vector3 mousePos;
    public void SetParameters(float moveSpeed, float lifeTime, float damage)
    {
        this.moveSpeed = moveSpeed;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
    }
    void FixedUpdate()
    {
        timeSinceBirth += Time.deltaTime;
        if (timeSinceBirth >= lifeTime)
        {
            DestroyProjectile();
        }
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    public float GetDamage()
    {
        return damage;
    }

}

