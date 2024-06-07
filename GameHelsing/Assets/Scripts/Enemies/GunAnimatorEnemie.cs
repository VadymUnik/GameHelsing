using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimatorEnemie : MonoBehaviour
{
    [SerializeField] private GameObject particleSource;

    [Header("Audio")]
    private AudioManager audioManager;

    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void SpawnParticles()
    {
        audioManager.PlaySound(audioManager.GlassShatter);
    }

}
