using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TEMP_EnemyCollision : MonoBehaviour
{
    public TextMeshPro healthText;
    private float health = 50f;

    void Update(){
        healthText.text = "Health: " + health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.GetComponent<Projectile>();
        if (bullet != null)
        {
            health -= bullet.GetDamage();
            Destroy(bullet.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
