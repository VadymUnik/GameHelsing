using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int blueHealth;
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
    private void AddBlueHealth(int amount)
    {
        blueHealth += amount;
        OnHealthDataChanged?.Invoke();
        Debug.Log("+"+ amount + " blue health! - " + health);
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
            if (blueHealth > 0)
                blueHealth -= amount; 
            else
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
    public int GetBlueHealth()
    {
        return blueHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyProjectile bullet = collision.GetComponent<EnemyProjectile>();
        if (bullet != null && !isDashing)
        {
            int damage = bullet.GetDamage();
            TakeDamage(damage);
            Destroy(bullet.gameObject);
        }

        HeartPickUp heartPickUp = collision.GetComponent<HeartPickUp>();
        if (heartPickUp != null)
        {   
            PickupHeartTypes hearthType = heartPickUp.GetHeartType();

            switch(hearthType)
            {
                case PickupHeartTypes.Container:
                    AddMaxHealth(2);
                    Destroy(heartPickUp.gameObject);
                    break;
                case PickupHeartTypes.Red:
                    if(health != maxHealth)
                    {
                    int addedHealth = heartPickUp.GetHealthAmount();
                    AddHealth(addedHealth);
                    Destroy(heartPickUp.gameObject);
                    }
                    break;
                case PickupHeartTypes.Blue:
                    int addedBlueHealth = heartPickUp.GetHealthAmount();
                    AddBlueHealth(addedBlueHealth);
                    Destroy(heartPickUp.gameObject);
                    break;
            }
        }
    }
}
