using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FogRoom : MonoBehaviour
{
    Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Reveal()
    {
        animator.ResetTrigger("Hide");
        animator.SetTrigger("Reveal");
    }

    public void Hide()
    {
        animator.ResetTrigger("Reveal");
        animator.SetTrigger("Hide");
    }

    public void DisableMask()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        sprite.maskInteraction = SpriteMaskInteraction.None;
    }
}
