using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimator : MonoBehaviour
{
    [SerializeField] Gate gate;
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
    } 

    private void DestroyGateObject()
    {
        gate.DestroyGate();
    } 
}
