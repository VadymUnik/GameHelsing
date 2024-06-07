using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float health = 50f;

    public AudioClip HitSound;

    private AudioManager audioManager;



    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public UnityEvent OnKilled;

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Kill();
        }
        audioManager.PlaySound(HitSound);
    }

    public void Kill()
    {
        OnKilled.Invoke();
    }

}
