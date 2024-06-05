using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private int damage;
    private float moveSpeed;
    private float lifeTime;
    private float timeSinceBirth;

    [SerializeField] private Rigidbody2D rb;
    private Camera mainCam;
    [SerializeField] Animator animator;
    
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
    public void SetParameters(float moveSpeed, float lifeTime, int damage)
    {
        this.moveSpeed = moveSpeed;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }
    private void DestroyProjectile()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        rb.velocity = new Vector2(0,0);
        animator.SetTrigger("Destroyed");
    }
    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            DestroyProjectile();
        }
    }

}
