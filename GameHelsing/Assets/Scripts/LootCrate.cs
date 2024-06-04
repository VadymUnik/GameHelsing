using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : MonoBehaviour
{
    [SerializeField] private HealthManager health;
    [SerializeField] private List<GameObject> SpawnOnKill;
    [SerializeField] private List<GameObject> SpawnOnCreate;
    [SerializeField] private GameObject SpawnedObjectPrefab;

    [SerializeField] private CrateSound crateSound;

    private AudioManager audioManager;

    

    private void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        SpawnOnCreate.ForEach(prefab => { 
            Vector3 positionShift = new Vector3 (0, 0.4f, 0);
            Instantiate(prefab, transform.position + positionShift, transform.rotation);
        });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.GetComponent<Projectile>();
        if (bullet != null)
        {
            float damageAmount = bullet.GetDamage();
            Destroy(bullet.gameObject);
            health.Damage(damageAmount);
        }
    }

    public void OnKill()
    {
        audioManager.PlaySound(audioManager.CrateDestroyed);
        Instantiate(SpawnedObjectPrefab, transform.position, transform.rotation);

        SpawnOnKill.ForEach(prefab => { 
            Instantiate(prefab, transform.position, transform.rotation);
        });

        Destroy(gameObject);
    }
}
