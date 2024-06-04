using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GateAnimator animator;
    public void OpenGate()
    {
        animator.Open();
    }
    public void DestroyGate()
    {
        Destroy(transform.gameObject);
    }
}
