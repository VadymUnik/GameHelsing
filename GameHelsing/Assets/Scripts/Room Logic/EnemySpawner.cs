using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int waveNumber;

    public GameObject Spawn()
    {
        return Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }
}
