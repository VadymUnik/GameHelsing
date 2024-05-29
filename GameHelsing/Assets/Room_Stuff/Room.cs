using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform doorLeft, doorRight, doorBottom, doorTop;
    [SerializeField] private float roomSize;

    public bool isLeftOpen = true, isRightOpen = true, isUpOpen = true, isDownOpen = true;
    public Vector2Int direction;
    public Vector2 desiredDirection = new Vector2(0, 0);

    void Start()
    {   
    }
    public float GetRoomSize()
    {
        return roomSize;
    }
    public Vector2Int GetDirection()
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
        Debug.Log(isLeftOpen + "   " + isRightOpen + "   " + isUpOpen + "   " + isDownOpen);
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

    public void SetDesiredDirection(Vector2Int desiredDirection)
    {
        this.desiredDirection = desiredDirection;
    }

}
