using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_chests : MonoBehaviour
{
    private float health = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.GetComponent<Projectile>();
        if (bullet != null)
        {
            health -= bullet.GetDamage();
            Destroy(bullet.gameObject);
            if (health <= 0)
            {
                GetComponent<Loot_bag>().InstantiateLoot(transform.position);
                Destroy(gameObject);
            }
        }
    }

}
