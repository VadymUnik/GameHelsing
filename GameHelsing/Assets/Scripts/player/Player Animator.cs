using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Shooter PlayerShooter;
    [SerializeField] GameObject DashSmokePrefab;
    [SerializeField] SpriteRenderer sprite;

    [SerializeField] GameObject Gun;

    playerMovement playerMovement;

    GameObject DashSmoke;

    private AudioManager audioManager;

    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        playerMovement = GetComponent<playerMovement>();
    }

    void Update()
    {
        if (true)
        {
            Quaternion gunRotation = PlayerShooter.GetRotation();
            if (gunRotation.eulerAngles.z > 95 && gunRotation.eulerAngles.z < 265 )
            {
                sprite.transform.localScale = new Vector3(-6.25f, 6.25f, 6.25f);
                Gun.transform.localPosition = new Vector3(-PlayerShooter.GetWeapon().gunShift, 0f, 0f);
                Gun.transform.localScale = new Vector3(1f, -1f, 1f);
            }
            else if (gunRotation.eulerAngles.z < 85 || gunRotation.eulerAngles.z > 275 )
            {
                sprite.transform.localScale = new Vector3(6.25f, 6.25f, 6.25f);
                Gun.transform.localPosition = new Vector3(PlayerShooter.GetWeapon().gunShift, 0f, 0f);
                Gun.transform.localScale = new Vector3(1f, PlayerShooter.GetYFlip() ? 1f : 1f, 1f);
            }
        }

        animator.SetFloat("Speed", playerMovement.rb.velocity.magnitude);
        animator.SetFloat("VelocityX", playerMovement.rb.velocity.x);
        animator.SetFloat("ScaleX", sprite.transform.localScale.x);
    }

    public void Dashed()
    {
        audioManager.PlaySound(audioManager.Dash);
        DashSmoke = Instantiate(DashSmokePrefab, transform.position, transform.rotation, transform);
        animator.SetBool("Dashed", true);
    }
    public void UnDashed()
    {
        animator.SetBool("Dashed", false);
    }

    public void Died()
    {
        animator.SetBool("Died", true);
    }
}
