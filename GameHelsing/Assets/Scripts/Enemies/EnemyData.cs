using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using Pathfinding;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private int magSize; //TO FIX - DOES NOTHING!
    [SerializeField] private float detectionDistance; //TO FIX - DOES NOTHING!
    [SerializeField] private float lineOfSightDuration = 0.5f;

    [SerializeField] private HealthManager health;
    [SerializeField] private EnemyShooter shooter;
    [SerializeField] private GameObject sprite;
    [SerializeField] private EnemyFlash spriteFlash;
    [SerializeField] private AIPath aiPath;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] GameObject Gun;
    

    public LayerMask IgnoreLayersMask;
    private GameObject homeRoom;
    private Transform target;
    
    private float lineOfSightTimer = 0f;
    private RaycastHit2D hit;
    private bool hasLineOfSight = false;
    private bool alive = true;

    [SerializeField] AudioClip Death;
    private AudioManager audioManager;

    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    private void Update()
    {
        if (alive)
        {
            Quaternion gunRotation = shooter.GetRotation();
            if (gunRotation.eulerAngles.z > 95 && gunRotation.eulerAngles.z < 265 )
            {
                sprite.transform.localScale = new Vector3(-8, 8, 8);
                Gun.transform.localPosition = new Vector3(-0.188f, 0f, 0f);
            }
            else if (gunRotation.eulerAngles.z < 85 || gunRotation.eulerAngles.z > 275 )
            {
                sprite.transform.localScale = new Vector3(8, 8, 8);
                Gun.transform.localPosition = new Vector3(0.188f, 0f, 0f);
            }
            animator.SetFloat("Speed", aiPath.desiredVelocity.magnitude);
        }
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        homeRoom = GameObject.FindGameObjectWithTag("Room");
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
        if (bullet != null && alive)
        {
            float damageAmount = bullet.GetDamage();
            Debug.Log("Enemy Received Damage : " + damageAmount);
            Destroy(bullet.gameObject);
            health.Damage(damageAmount);
            FillInWhite();
        }
    }

    public void OnKill()
    {
        if (alive)
        {
            alive = false;
            aiPath.canMove = false;
            Destroy(shooter.transform.gameObject);
            if (homeRoom.TryGetComponent(out RoomManager room))
            {
                room.DecreaseEnemyCount();
            }
            audioManager.PlaySound(Death);
            animator.SetTrigger("Death");
            
        }
    }

    public void DestroyEnemy()
    {
        Destroy(transform.gameObject);
    }

    public void SetHomeRoom(GameObject homeRoom)
    {
        this.homeRoom = homeRoom;
    }
    public bool GetLineOfSight()
    {
        return hasLineOfSight;
    }

    private void FillInWhite()
    {
        spriteFlash.Flash();
    }
}