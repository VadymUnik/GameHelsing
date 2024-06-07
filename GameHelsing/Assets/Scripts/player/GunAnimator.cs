using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    [SerializeField] private GameObject particleSource;
    [SerializeField] private Shooter shooter;

    [Header("Audio")]
    private AudioManager audioManager;

    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void SpawnParticles()
    {
        audioManager.PlaySound(audioManager.GlassShatter);
        Instantiate(shooter.GetWeapon().particlesPrefab, particleSource.transform.position, Quaternion.Euler(0,0,0));
    }

}
