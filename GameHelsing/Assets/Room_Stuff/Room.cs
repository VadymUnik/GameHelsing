using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform doorLeft, doorRight, doorBottom, doorTop;
    [SerializeField] private float roomSize;

    private Vector3Int direction;

    public int desiredVertical;

    public int desiredHorizontal;


    public Transform GetDoorRight()
    {
        return doorRight;
    }

    public float GetRoomSize()
    {
        return roomSize;
    }
    public Vector3Int GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector3Int direction)
    {
        this.direction = direction;
    }

}
