using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;

    void Start()
    {
        Transform StartPos = CreateRoom(transform);

        for (int i = 0; i < 10; i++)
        {
            StartPos = CreateRoom(StartPos);
        }
    }
    
    private Transform CreateRoom(Transform startTransform)
    {
        GameObject newRoom = Instantiate(roomPrefab, startTransform.position, startTransform.rotation);
        return newRoom.GetComponent<Room>().GetDoorRight();
    }


}
