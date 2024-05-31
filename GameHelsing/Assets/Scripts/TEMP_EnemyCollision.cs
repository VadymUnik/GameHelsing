using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class TEMP_EnemyCollision : MonoBehaviour
{
    [SerializeField] private int magSize; //TO FIX - DOES NOTHING!
    [SerializeField] private float detectionDistance; //TO FIX - DOES NOTHING!
    [SerializeField] private float lineOfSightDuration = 0.5f;

    public LayerMask IgnoreLayersMask;
    private Transform target;
    private float health = 50f;
    private float lineOfSightTimer = 0f;
    private RaycastHit2D hit;
    private bool hasLineOfSight = false;
    

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate(){

        if (target != null)
        {
            hit = Physics2D.Raycast(transform.position, (target.transform.position - transform.position).normalized, Mathf.Infinity, IgnoreLayersMask);

            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position, (target.transform.position - transform.position), Color.red);
                    lineOfSightTimer += Time.deltaTime;
                    if (lineOfSightTimer >= lineOfSightDuration)
                    {
                        hasLineOfSight = true;
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, (target.transform.position - transform.position), Color.yellow);
                    lineOfSightTimer = 0;
                    hasLineOfSight = false;
                }
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.GetComponent<Projectile>();
        if (bullet != null)
        {
            health -= bullet.GetDamage();
            Destroy(bullet.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public bool GetLineOfSight()
    {
        return hasLineOfSight;
    }
}
