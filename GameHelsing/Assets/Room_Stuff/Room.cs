using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform doorLeft, doorRight, doorBottom, doorTop;

    public Transform GetDoorRight()
    {
        return doorRight;
    }
}
