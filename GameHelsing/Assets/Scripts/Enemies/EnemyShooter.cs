using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPosition;

    [Header("Shooting parameters")]
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magSize;
    
    private GameObject parentObject;

    
    private bool isReloading = false;
    private bool isOnShotDelay;
    private bool canShoot = true;
    private float shootTimer;
    private float reloadTimer;
    private int bulletsLeft;

    private GameObject player;
    private Vector3 playerPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletsLeft = magSize;
        parentObject = transform.parent.gameObject;
        animator.SetInteger("Bullets Left", bulletsLeft);
    }

    void Update()
    {
        bool hasSightOnTarget = parentObject.GetComponent<EnemyData>().GetLineOfSight();
        playerPos = player.transform.position;
        Vector3 rotation = playerPos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (isReloading || bulletsLeft <= 0 || !hasSightOnTarget)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }

        if (bulletsLeft > 0)
            HandleShooting();
        else
            HandleReloading();
    }
    
    private void ShootBullet()
    {
        animator.SetBool("Shoot", true);
        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
        if(bullet.TryGetComponent(out EnemyProjectile enemyProjectile))
        {
            enemyProjectile.SetParameters(bulletMoveSpeed, bulletLifeTime, bulletDamage);
        }
        
    } 
    private void HandleShooting() 
    {
        if (!isOnShotDelay) 
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= timeBetweenShots) 
            {
                isOnShotDelay = true;
                shootTimer = 0f;
            }
        }

        if (isOnShotDelay && canShoot) 
        {
            ShootBullet();
            bulletsLeft--;
            if (bulletsLeft <= 0)
            {
                animator.SetBool("Shoot", false);
                animator.SetBool("IsEmpty", true);
            }
            isOnShotDelay = false;
        }
    }

    private void HandleReloading() 
    {   if (reloadTimer == 0)
        {
            animator.ResetTrigger("Idle");
            animator.SetBool("Reload", true);
        }
        reloadTimer += Time.deltaTime;
        if (reloadTimer >= reloadTime) 
        {
            animator.SetBool("IsEmpty", false);
            animator.SetBool("Reload", false);
            bulletsLeft = magSize;
            reloadTimer = 0;
            isReloading = false;
        }
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

}
