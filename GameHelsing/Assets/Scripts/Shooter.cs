using System.Collections;
using System.Collections.Generic;
//using System.Numerics;

//using System.Numerics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject reloadTextPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magSize;

    private bool canShoot;
    private bool isReloading;
    private float shootTimer;
    private float reloadTimer;
    private Camera mainCam;
    private Vector3 mousePos;
    private int bulletsLeft;
    private bool hasReloadText = false;

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

        if (isReloading) 
        {
            HandleReloading();
        } 
        else 
        {
            HandleShooting();
        }

        if((Input.GetKey("r") && bulletsLeft < magSize) || (bulletsLeft == 0 && Input.GetMouseButton(0)))
        {
            isReloading = true;
        }
    }

    public void ShootBullet() {
        Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
    }
    
    private void HandleShooting() 
    {
        if (!canShoot) 
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= timeBetweenShots) 
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }

        if (Input.GetMouseButton(0) && canShoot && bulletsLeft > 0) 
        {
            ShootBullet();
            bulletsLeft--;
            canShoot = false;
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

    public void CreateReloadText()
    {
        GameObject ReloadText = Instantiate(reloadTextPrefab, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.Euler(0, 0, 0));
        if(ReloadText.TryGetComponent(out ReloadTextController reloadTextController))
        {
            reloadTextController.SetReloadTime(reloadTime);
        }
    }
}
