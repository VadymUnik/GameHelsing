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

        //game end if hp less= 0
        }
               if (health <= 0)
        {
            // Pause the game
            Time.timeScale = 0f;
            // Display "Game Over" in the console
            Debug.Log("Game Over");

            // Now pause all objects in the scene
            PauseAllObjects();
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

        private void PauseAllObjects() // for stopping game when hp is zero
    {
        // Get all objects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Pause each object by setting their timeScale to 0
        foreach (var obj in allObjects)
        {
            obj.GetComponent<Rigidbody2D>()?.Sleep(); // For 2D physics
            obj.GetComponent<Rigidbody>()?.Sleep(); // For 3D physics
        }
    }
}
