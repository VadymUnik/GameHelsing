using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startRoomPrefab;
    [SerializeField] private List<GameObject> roomPrefabs;
   // [SerializeField] private List<GameObject> Rooms;

    [SerializeField] private int roomsAmount;

    private bool UpperLeftOpen = true, UpperRightOpen = true , LowerLeftOpen = true, LowerRightOpen = true;


    void Start()
    {
        GameObject startRoom = Instantiate(startRoomPrefab, transform.position, transform.rotation);

        GenerateRoomBranch(roomsAmount, startRoom, true, 10);
        

    }

    private void GenerateRoomBranch(int amount, GameObject startingRoom, bool hasSubBranch = false, int subBranchAmount = 0, bool isSubBranch = false)
    {
        List<GameObject> Rooms = new List<GameObject>
        {
            startingRoom
        };

        int subBranchIndex = 0;
        hasSubBranch = isSubBranch ? false : hasSubBranch;

        if (hasSubBranch) 
        {
            subBranchIndex = Random.Range(1, amount);
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject randomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            Rooms.Add(CreateRoom(Rooms[i], randomPrefab, 2));

            if (i == subBranchIndex && hasSubBranch && Rooms[i].TryGetComponent(out Room _Room))
            {
                bool canCreateBranch = _Room.CanCreateBranch();
                Debug.Log(canCreateBranch);
                if (canCreateBranch == true)
                {
                    Vector2 desiredDirectionBufer = _Room.desiredDirection;
                    _Room.SetDesiredDirection(SetNewBranchDirection(_Room));
                    GenerateRoomBranch(subBranchAmount, Rooms[i], false, 0, true);
                    _Room.desiredDirection = desiredDirectionBufer;
                }
                else
                {
                    subBranchIndex++;
                }

            }

        }
    }

    private GameObject CreateRoom(GameObject parentRoom, GameObject roomPrefab, float distance)
    {
        GameObject newRoom = Instantiate(roomPrefab, parentRoom.transform.position, parentRoom.transform.rotation);

        float parentSize;
        float childSize;
        Vector3 positionShift;

        if(newRoom.TryGetComponent(out Room _newRoom) && parentRoom.TryGetComponent(out Room _parentroom))
        {
            parentSize = _newRoom.GetRoomSize();
            childSize = _parentroom.GetRoomSize();

            positionShift = SetNewRoomDirection(parentSize/2 + distance + childSize/2, _parentroom, _newRoom);
            if (positionShift == new Vector3(0, 0, 0))
            {
                Debug.Log("Created new room! + WRONG DIRECTION");
                return newRoom;
            }
            newRoom.transform.position = parentRoom.transform.position + positionShift;
            _newRoom.direction = new Vector2Int((int)positionShift.x, (int)positionShift.y);
        }    
        Debug.Log("Created new room!");
        return newRoom;
    }

    private Vector3 SetNewRoomDirection(float shift, Room parentRoom, Room newRoom)
    {
        Vector3 direction = new Vector3(0, 0, 0);

        int upOrSide = Random.Range(0, 9);

        bool horisontalCanConnect = false, verticalCanConnect = false;

        if (parentRoom.desiredDirection == new Vector2(0, 0))
        {
            if (UpperLeftOpen || UpperRightOpen || LowerLeftOpen || LowerRightOpen)
            {
                while(newRoom.desiredDirection == new Vector2(0, 0))
                    {
                        int randomValue = Random.Range(0, 4);
                        switch (randomValue)
                        {
                            case 0:
                                if (UpperLeftOpen)
                                {
                                    newRoom.desiredDirection.Set(-1, 1);
                                    UpperLeftOpen = false;
                                }
                                break;
                            case 1:
                                if (UpperRightOpen)
                                {
                                    newRoom.desiredDirection.Set(1, 1);
                                    UpperRightOpen = false;
                                }
                                break;
                            case 2:
                                if (LowerLeftOpen)
                                {
                                    newRoom.desiredDirection.Set(-1, -1);
                                    LowerLeftOpen = false;
                                }
                                break;
                            case 3:
                                if (LowerRightOpen)
                                {
                                    newRoom.desiredDirection.Set(1, -1);
                                    LowerRightOpen = false;
                                }
                                break;
                        }
                    }
            }
        }
        else
        {
            newRoom.desiredDirection = parentRoom.desiredDirection;
        }

        if ((parentRoom.isRightOpen && newRoom.isLeftOpen && newRoom.desiredDirection.x > 0) || (parentRoom.isLeftOpen && newRoom.isRightOpen && newRoom.desiredDirection.x < 0))
            horisontalCanConnect = true;

        if ((parentRoom.isUpOpen && newRoom.isDownOpen && newRoom.desiredDirection.y > 0) || (parentRoom.isDownOpen && newRoom.isUpOpen && newRoom.desiredDirection.y < 0))
            verticalCanConnect = true;

        if(horisontalCanConnect && verticalCanConnect)
        {
            horisontalCanConnect = upOrSide > 4 ? true : false;
            verticalCanConnect = upOrSide > 4 ? false : true;
        }

        if (horisontalCanConnect)
        {
            direction.Set(newRoom.desiredDirection.x * shift, 0, 0);
        }

        if (verticalCanConnect)
        {
            direction.Set(0, newRoom.desiredDirection.y * shift, 0);
        }

        if(direction.x != 0)
        {
            newRoom.isLeftOpen = direction.x > 0 ? false : newRoom.isLeftOpen;
            newRoom.isRightOpen = direction.x < 0 ? false : newRoom.isRightOpen;

            parentRoom.isLeftOpen = direction.x < 0 ? false : parentRoom.isLeftOpen;
            parentRoom.isRightOpen = direction.x > 0 ? false : parentRoom.isRightOpen;
        }

        newRoom.isUpOpen = newRoom.desiredDirection.y < 0 ? false : true;
        newRoom.isDownOpen = newRoom.desiredDirection.y < 0 ? true : false;

        parentRoom.isUpOpen = newRoom.isDownOpen;
        parentRoom.isDownOpen = newRoom.isUpOpen;

        return direction;
    }

    private Vector2Int SetNewBranchDirection(Room room)
    {
        Vector2Int direction = new Vector2Int();

        // if(room.desiredDirection.y == 1)
        // {
        //     direction.y = 1;
        //     if(UpperRightOpen)
        //     {
        //         direction.x = 1;
        //         UpperRightOpen = false;
        //     }
        //     else
        //     {
        //         direction.x = -1;
        //         UpperLeftOpen = false;
        //     }
        // }
        // else
        // {
        //     direction.y = -1;
        //     if(LowerRightOpen)
        //     {
        //         direction.x = 1;
        //         LowerRightOpen = false;
        //     }
        //     else
        //     {
        //         direction.x = -1;
        //         LowerLeftOpen = false;
        //     }
        // }
        direction.y = (int)room.desiredDirection.y;
        direction.x = (int)room.desiredDirection.x * -1;

        return direction;
    }


}
