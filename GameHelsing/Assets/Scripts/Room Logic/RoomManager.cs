//using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{


    [SerializeField] private Transform gateSpawnerTop, gateSpawnerBottom, gateSpawnerLeft, gateSpawnerRight;
    [SerializeField] private GameObject gateHorizontalPrefab, gateTopPrefab, gateBottomPrefab;
    [SerializeField] private GameObject thisRoom;

    [Header("Loot data")]
    [SerializeField] private GameObject LootSpawnerPosition;
    [SerializeField] private GameObject lootItemPrefab;
    [Header("Enemies")]
    [SerializeField] private List<GameObject> EnemySpawners;
    [SerializeField] private FogManager fogManager;

    [SerializeField] AudioManager audioManager;
    
    
    private int currentWave = 1;
    private int finalWave;
    private List<GameObject> Gates = new List<GameObject>();
    private List<GameObject> Enemies = new List<GameObject>();
    
    private int enemyCount;
    private int currentWaveCount;
    private bool NotActivated = true;
    private bool playerInRoom = false;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        enemyCount = EnemySpawners.Count;
    }
    private void Update()
    {
        if(!NotActivated)
        {
            if (enemyCount <= 0)
            {
                RemoveGates();
                SpawnLoot();
                this.enabled = false;
            }
            else if (currentWaveCount == 0)
            {
                currentWave++;
                SpawnWave(currentWave);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && NotActivated)
        {
            //RoomStageStarted.Invoke();
            Activate();
            NotActivated = false;
            fogManager.RevealHallFog();
        }

        if (collision.CompareTag("Player"))
        {
            playerInRoom = true;
            fogManager.RevealRoomFog();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRoom = false;
            fogManager.HideRoomFog();
        }
        
    }


    private void Activate()
    {
        SetFinalWave();
        SpawnGates();
        SpawnWave(1);
    }

    private void SetFinalWave()
    {
        int result = 0;
        EnemySpawners.ForEach(spawner => { 
            int enemyCountWaveNumber = spawner.GetComponent<EnemySpawner>().GetWaveNumber();
            result = enemyCountWaveNumber > result ? enemyCountWaveNumber : result;
        }); 

        finalWave = result;
    }
    private void SpawnWave(int waveNumber)
    {
        currentWave = waveNumber;
        SpawnEnemies(waveNumber);
    }

    private void SpawnLoot()
    {
        Instantiate(lootItemPrefab, LootSpawnerPosition.transform.position, LootSpawnerPosition.transform.rotation);
    }
    private void SpawnEnemies(int waveNumber)
    {
        EnemySpawners.ForEach(spawner => { 
            if (waveNumber == spawner.GetComponent<EnemySpawner>().GetWaveNumber())
            {
                GameObject enemy = spawner.GetComponent<EnemySpawner>().Spawn();
                enemy.GetComponent<EnemyData>().SetHomeRoom(transform.gameObject);
                currentWaveCount++;
                Enemies.Add(enemy);
            }
        }); 
    }
    private void SpawnGates()
    {
        if (thisRoom.TryGetComponent(out Room room))
        {
            audioManager.PlaySound(audioManager.GateOpening);
            if (!room.doorTop.gameObject.activeSelf == true) Gates.Add(Instantiate(gateTopPrefab, gateSpawnerTop.position, transform.rotation));
            if (!room.doorBottom.gameObject.activeSelf == true) Gates.Add(Instantiate(gateBottomPrefab, gateSpawnerBottom.position, transform.rotation));

            if (!room.doorLeft.gameObject.activeSelf == true) Gates.Add(Instantiate(gateHorizontalPrefab, gateSpawnerLeft.position, transform.rotation));
            if (!room.doorRight.gameObject.activeSelf == true) Gates.Add(Instantiate(gateHorizontalPrefab, gateSpawnerRight.position, transform.rotation));
        }

    }
    private void RemoveGates()
    {
        audioManager.PlaySound(audioManager.GateOpening);
        Gates.ForEach(gate => { 
            if (gate.TryGetComponent(out Gate currentGate))
            {
                currentGate.OpenGate();
            }
        }); 
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
        currentWaveCount--;
    }




}
