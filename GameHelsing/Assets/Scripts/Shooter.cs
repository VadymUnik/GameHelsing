using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject reloadTextPrefab;
    [SerializeField] private Transform shootPosition;

    [Header("Shooting parameters")]
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magSize;

    // New serialized fields for bullet spread
    [SerializeField] private float randomOffsetMax; // keep it above zero
    [SerializeField] private float randomOffsetMin; // keep it below zero
    [SerializeField] private int numBullets;


    private bool isOnShotDelay;
    private bool canShoot = true;
    private bool isReloading = false;
    private bool hasReloadText = false;
    private bool isDashing = false;
    private float shootTimer;
    private float reloadTimer;
    private int bulletsLeft;

    private Camera mainCam;
    private Vector3 mousePos;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletsLeft = magSize;
    }


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if((Input.GetKey("r") && bulletsLeft < magSize) || (bulletsLeft == 0 && Input.GetMouseButtonDown(0)))
        {
            isReloading = true;
            HandleReloading();
        }

        if (isReloading || bulletsLeft <= 0 || isDashing)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }

        HandleShooting();

        if (isReloading) 
        {
            HandleReloading();
        } 
    }
    
     private void ShootBullet()
    {
        float randomOffset = Random.Range(randomOffsetMin, randomOffsetMax);
        Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + randomOffset);

        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, bulletRotation);
        if (bullet.TryGetComponent(out Projectile projectile))
        {
            projectile.SetParameters(bulletMoveSpeed, bulletLifeTime, bulletDamage);
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

        if (Input.GetMouseButton(0) && isOnShotDelay && canShoot)
        {
            // Fire multiple bullets (5-10 bullets)
            int newnumBullets = numBullets;
            for (int i = 0; i < newnumBullets; i++)
            {
                ShootBullet();
            }

            bulletsLeft--; // Deduct only one bullet from the magazine
            isOnShotDelay = false;
        }
    }
    
    private void HandleReloading() 
    {
        if (!hasReloadText) 
        {
            CreateReloadText();
            hasReloadText = true;
        }

        reloadTimer += Time.deltaTime;
        if (reloadTimer >= reloadTime) 
        {
            bulletsLeft = magSize;
            reloadTimer = 0;
            hasReloadText = false;
            isReloading = false;
        }
    }
    private void CreateReloadText()
    {
        GameObject ParentObject = transform.parent.gameObject;
        GameObject ReloadText = Instantiate(reloadTextPrefab, new Vector2(ParentObject.transform.position.x, ParentObject.transform.position.y + 1), Quaternion.Euler(0, 0, 0), ParentObject.transform);
        if(ReloadText.TryGetComponent(out ReloadTextController reloadTextController))
        {
            reloadTextController.SetReloadTime(reloadTime);
        }
    }

    public void DetectDashStart()
    {
        isDashing = true;
    }
    public void DetectDashEnd()
    {
        isDashing = false;
    }
}
