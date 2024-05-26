using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
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
    }

    void Update()
    {
        bool hasSightOnTarget = parentObject.GetComponent<TEMP_EnemyCollision>().GetLineOfSight();
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
            isOnShotDelay = false;
        }
    }

    private void HandleReloading() 
    {
        reloadTimer += Time.deltaTime;
        if (reloadTimer >= reloadTime) 
        {
            bulletsLeft = magSize;
            reloadTimer = 0;
            isReloading = false;
        }
    }
}
