using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject smokePrefab;
    [SerializeField] int waveNumber;

    private AudioManager audioManager;

    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public GameObject Spawn()
    {
        audioManager.PlaySound(audioManager.EnemySpawn);
        StartCoroutine(OnSpawn());
        return Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    private IEnumerator OnSpawn()
    {
        Instantiate(smokePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }
}
