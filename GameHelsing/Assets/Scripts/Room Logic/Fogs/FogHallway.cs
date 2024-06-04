using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FogHallway : MonoBehaviour
{

    Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }
    public void Reveal()
    {
        animator.SetTrigger("Reveal");
    }

    public void Destroy()
    {
        Destroy(transform.gameObject);
    }

    public void DisableMask()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        sprite.maskInteraction = SpriteMaskInteraction.None;
    }
}
