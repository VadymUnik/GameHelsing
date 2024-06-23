using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> lootPool;

    private void Start()
    {
        SpawnLoot();
    }
    public void SpawnLoot()
    {
        int spawnChance = Random.Range(0, 11);
        if (spawnChance > 4)
        {
        int lootPoolSize = lootPool.Count;

        int lootIndex = Random.Range(0, lootPoolSize);

        Instantiate(lootPool[lootIndex], transform.position, transform.rotation);
        }
    }
}

