using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public Transform doorLeft, doorRight, doorBottom, doorTop;
    [SerializeField] public Transform EntranceLeft, EntranceRight, EntranceBottom, EntranceTop;
    [SerializeField] public Transform archLeft, archRight, archBottom, archTop;
    [SerializeField] private float roomSize;
    [SerializeField] public bool isStartRoom = false;
    [SerializeField] public bool isStartRoomFirst = true;

    

    public bool isLeftOpen = true, isRightOpen = true, isUpOpen = true, isDownOpen = true;
    public Vector2 direction;
    public Vector2 desiredDirection = new Vector2(0, 0);

    public float GetRoomSize()
    {
        return roomSize;
    }
    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector2Int direction)
    {
        this.direction = direction;
    }

    public bool CanCreateBranch()
    {
        bool canHorisontal;
        bool canVertical;
        if (desiredDirection.x > 0)
            canHorisontal = isLeftOpen;
        else
            canHorisontal = isRightOpen;

        if (desiredDirection.y > 0)
            canVertical = isUpOpen;
        else
            canVertical = isDownOpen;
        
        Debug.Log(canHorisontal + "" + canVertical);

        return canHorisontal || canVertical;
    }

    public Vector2 GetDesiredDirection()
    {
        return desiredDirection;
    }

    public void SetRoomSize(float roomSize)
    {
        this.roomSize = roomSize;
    }
    public void SetDesiredDirection(Vector2 desiredDirection)
    {
        this.desiredDirection = desiredDirection;
    }

}
