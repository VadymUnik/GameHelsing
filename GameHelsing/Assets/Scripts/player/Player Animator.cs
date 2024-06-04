using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Shooter PlayerShooter;

    [SerializeField] SpriteRenderer sprite;

    [SerializeField] GameObject Gun;

    playerMovement playerMovement;

    

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
                Gun.transform.localPosition = new Vector3(-0.43f, 0f, 0f);
            }
            else if (gunRotation.eulerAngles.z < 85 || gunRotation.eulerAngles.z > 275 )
            {
                sprite.transform.localScale = new Vector3(6.25f, 6.25f, 6.25f);
                Gun.transform.localPosition = new Vector3(0.43f, 0f, 0f);
            }
        }

        animator.SetFloat("Speed", playerMovement.rb.velocity.magnitude);
        animator.SetFloat("VelocityX", playerMovement.rb.velocity.x);
        animator.SetFloat("ScaleX", sprite.transform.localScale.x);
    }
}
