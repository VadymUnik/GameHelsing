using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManadger : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenPrefab;

    private AudioManager audioManager;
    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void CreateDeathScreen()
    {
        audioManager.PlaySound(audioManager.Death);
        Instantiate(deathScreenPrefab, transform.position, transform.rotation, transform);
    }
}
