using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int money;

    public static event Action OnHealthDataChanged;
    private bool isInvincible = false;
    private bool isDashing = false;

    private void AddMoney(int amount)
    {
        money += amount;
    }
    private void SubtrMoney(int amount)
    {
        money -= amount;
    }
    private void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        OnHealthDataChanged?.Invoke();
        Debug.Log("+"+ amount + " health! - " + health);
    }
    private void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        OnHealthDataChanged?.Invoke();
        Debug.Log("+"+ amount + " max health! - " + maxHealth);
    }
    private void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
        health -= amount;
        OnHealthDataChanged?.Invoke();
        Debug.Log("Received " + amount + " damage! - " + health);
        }
    }
    private void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        OnHealthDataChanged?.Invoke();
        Debug.Log("Health increased by: " + amount + " - " + maxHealth);
    }
    private void DecreaseMaxHealth(int amount)
    {
        maxHealth -= amount;
        OnHealthDataChanged?.Invoke();
        Debug.Log("Health decreased by: " + amount + " - " + maxHealth);
    }
    
    public void DetectDashStart()
    {
        isDashing = true;
    }
    public void DetectDashEnd()
    {
        isDashing = false;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyProjectile bullet = collision.GetComponent<EnemyProjectile>();
        if (bullet != null && !isDashing)
        {
            int damage = bullet.GetDamage();
            TakeDamage(damage);
            Destroy(bullet.gameObject);
        }

        HeartPickUp heartPickUp = collision.GetComponent<HeartPickUp>();
        if (heartPickUp != null && health != maxHealth)
        {
            int addedHealth = heartPickUp.GetAddedHealth();
            AddHealth(addedHealth);
            Destroy(heartPickUp.gameObject);
        }

        HeartContainerPickUp heartContainerPickUp = collision.GetComponent<HeartContainerPickUp>();
        if (heartContainerPickUp != null)
        {
            int addedMaxHealth = heartContainerPickUp.GetAddedMaxHealth();
            AddMaxHealth(addedMaxHealth);
            Destroy(heartContainerPickUp.gameObject);
        }
    }
}
