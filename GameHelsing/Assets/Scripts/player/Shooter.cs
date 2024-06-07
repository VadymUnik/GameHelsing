using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private WeaponScriptableObject weapon;
    [SerializeField] private Animator animator;
    
    [SerializeField] private SpriteRenderer thisSprite;
 
    [Header("Prefabs")]
    [SerializeField] private GameObject reloadTextPrefab;
    [SerializeField] private Transform shootPosition; //TODO - Change To Scriptable Object!!!!!!!!
    private AudioManager audioManager;

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

    void OnEnable()
    {
        SetUp();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletsLeft = weapon.magSize;
        animator.SetInteger("Bullets Left", bulletsLeft);
    }

    void SetUp()
    {
        thisSprite.transform.localRotation = weapon.SpriteRotation;
        shootPosition.transform.localPosition = weapon.shotPosition;
        animator.runtimeAnimatorController = weapon.animatorPrefab;
    }
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if((Input.GetKey("r") && bulletsLeft < weapon.magSize && !isReloading) || (bulletsLeft == 0 && Input.GetMouseButtonDown(0) && !isReloading))
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
        float randomOffset = Random.Range(weapon.randomOffsetMin, weapon.randomOffsetMax);
        Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + randomOffset);
        
        animator.SetBool("Shoot", true);
        audioManager.PlaySound(audioManager.Shoot);

        GameObject bullet = Instantiate(weapon.bulletPrefab, shootPosition.position, bulletRotation);
        if (bullet.TryGetComponent(out Projectile projectile))
        {
            projectile.SetParameters(weapon.bulletMoveSpeed, weapon.bulletLifeTime, weapon.bulletDamage);
        }
    }


     private void HandleShooting()
    {
        if (!isOnShotDelay)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= weapon.timeBetweenShots)
            {
                isOnShotDelay = true;
                shootTimer = 0f;
            }
        }

        if (Input.GetMouseButton(0) && isOnShotDelay && canShoot)
        {

            int newnumBullets = weapon.numBullets;
            for (int i = 0; i < newnumBullets; i++)
            {
                ShootBullet();
            }

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
        if (reloadTimer >= weapon.reloadTime) 
        {
            animator.SetBool("IsEmpty", false);
            animator.SetBool("Reload", false);
            bulletsLeft = weapon.magSize;
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
            reloadTextController.SetReloadTime(weapon.reloadTime);
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

    public bool GetYFlip()
    {
        return weapon.enableYflip;
    }

    public WeaponScriptableObject GetWeapon()
    {
        return weapon;
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public void ChangeWeapon(WeaponScriptableObject newWeapon, int bulletsLeft)
    {
        this.bulletsLeft = bulletsLeft;
        weapon = newWeapon;
        SetUp();
    }

    public int GetBulletsLeft()
    {
        return bulletsLeft;
    }
}
