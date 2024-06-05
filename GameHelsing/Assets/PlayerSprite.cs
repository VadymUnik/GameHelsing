using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void UnDashAnimation()
    {
        animator.SetBool("Dashed", false);
    }
}
