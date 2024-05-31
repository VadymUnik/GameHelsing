using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] Transform gateSpawnerTop, gateSpawnerBottom, gateSpawnerLeft, gateSpawnerRight;
    [SerializeField] GameObject gateHorizontalPrefab, gateTopPrefab, gateBottomPrefab;

    public static event Action RoomStageStarted;
    public static event Action RoomStageFinished;



    private bool NotActivated = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && NotActivated)
        {
            //RoomStageStarted.Invoke();
            NotActivated = false;
            SpawnGates();
        }
    }
    
    private void SpawnGates()
    {
        GameObject GateTop = Instantiate(gateTopPrefab, gateSpawnerTop.position, transform.rotation);
        GameObject GateBottom = Instantiate(gateBottomPrefab, gateSpawnerBottom.position, transform.rotation);

        GameObject GateLeft = Instantiate(gateHorizontalPrefab, gateSpawnerLeft.position, transform.rotation);
        GameObject GateRight = Instantiate(gateHorizontalPrefab, gateSpawnerRight.position, transform.rotation);
    }
}
