using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSound : MonoBehaviour
{
    [SerializeField] private AudioSource sourceSound;
    [SerializeField] private AudioClip destoyed;

    public void PlayDestroyed()
    {
        Debug.Log("PlaySound");
        
        sourceSound.PlayOneShot(destoyed);
    }
}
