using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer thisSprite;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject reloadTextPrefab;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private Transform shootPosition;

    [Header("Shooting parameters")]
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magSize;
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
        animator.SetInteger("Bullets Left", bulletsLeft);
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if((Input.GetKey("r") && bulletsLeft < magSize && !isReloading) || (bulletsLeft == 0 && Input.GetMouseButtonDown(0) && !isReloading))
        {
            isReloading = true;
            HandleReloading();
        }

        if (isReloading || bulletsLeft <= 0 || isDashing)
        {
            animator.ResetTrigger("Idle");
            animator.SetBool("Shoot", false);
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
        animator.SetBool("Shoot", true);
        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
        if(bullet.TryGetComponent(out Projectile projectile))
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
            ShootBullet();
            bulletsLeft--;
            if (bulletsLeft <= 0)
            {
                animator.SetBool("Shoot", false);
                animator.SetBool("IsEmpty", true);
            }
            isOnShotDelay = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Shoot", false);
        }
    }
    private void HandleReloading() 
    {
        if (!hasReloadText) 
        {
            animator.ResetTrigger("Idle");
            animator.SetBool("Reload", true);
            CreateReloadText();
            hasReloadText = true;
        }

        reloadTimer += Time.deltaTime;
        if (reloadTimer >= reloadTime) 
        {
            animator.SetBool("IsEmpty", false);
            animator.SetBool("Reload", false);
            bulletsLeft = magSize;
            reloadTimer = 0;
            hasReloadText = false;
            isReloading = false;
        }
    }
    private void CreateReloadText()
    {
        GameObject ParentObject = transform.parent.gameObject;
        GameObject ReloadText = Instantiate(reloadTextPrefab, new Vector2(ParentObject.transform.position.x, ParentObject.transform.position.y + 1.5f), Quaternion.Euler(0, 0, 0), ParentObject.transform);
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

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
