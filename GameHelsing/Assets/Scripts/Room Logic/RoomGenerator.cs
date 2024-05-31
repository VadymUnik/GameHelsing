using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startRoomPrefab;
    [SerializeField] private GameObject XYi; 
    [SerializeField] private GameObject ZYi;
    [SerializeField] private List<GameObject> roomPrefabs;

    [SerializeField] private int roomsAmount;
    [SerializeField] private int subRoomsAmount;

    private bool UpperLeftOpen = true, UpperRightOpen = true , LowerLeftOpen = true, LowerRightOpen = true;


    void Start()
    {
            GameObject startRoom = Instantiate(startRoomPrefab, transform.position, transform.rotation);
            startRoom.transform.position = new Vector3(0, 0, 0);

            GenerateRoomBranch(roomsAmount, startRoom, true, 3);
            Vector2 desiredDirectionChange = startRoom.GetComponent<Room>().GetDesiredDirection();
            desiredDirectionChange.y *= -1;
            startRoom.GetComponent<Room>().SetDesiredDirection(desiredDirectionChange);
            GenerateRoomBranch(roomsAmount, startRoom, true, 3);

            AstarPath.active.Scan();
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
            Rooms.Add(CreateRoom(Rooms[i], randomPrefab, 8));

            if (i == subBranchIndex && hasSubBranch && Rooms[i].TryGetComponent(out Room _Room))
            {
                bool canCreateBranch = _Room.CanCreateBranch();
                
                if (canCreateBranch == true)
                {
                    Vector2 desiredDirectionBufer = _Room.desiredDirection;

                    Vector2 newBranchDirection = SetNewBranchDirection(_Room);
                    _Room.SetDesiredDirection(newBranchDirection);
                    GenerateRoomBranch(subBranchAmount, Rooms[i], false, 0, true);



                    _Room.desiredDirection = desiredDirectionBufer;
                }
                else
                {
                    Debug.Log("Could not create sub-branch!!!!!");
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
            parentSize = _parentroom.GetRoomSize();
            childSize = _newRoom.GetRoomSize();

            float shift = parentSize/2 + distance + childSize/2;
            positionShift = SetNewRoomDirection(shift, _parentroom, _newRoom);
            if (positionShift == new Vector3(0, 0, 0))
            {
                Debug.Log("Created new room! + WRONG DIRECTION");
                return newRoom;
            }
            newRoom.transform.position = parentRoom.transform.position + positionShift;
            _newRoom.direction = new Vector2((int)positionShift.x, (int)positionShift.y).normalized;


            if (_newRoom.direction.x != 0)
            {
                if (_newRoom.direction.x < 0)
                {
                    _newRoom.doorRight.gameObject.SetActive(false);
                    _parentroom.doorLeft.gameObject.SetActive(false);
                    
                    _newRoom.EntranceRight.gameObject.SetActive(true);
                    _parentroom.EntranceLeft.gameObject.SetActive(true);

                    _newRoom.archRight.gameObject.SetActive(true);
                    _parentroom.archLeft.gameObject.SetActive(true);
                }
                else
                {
                    _newRoom.doorLeft.gameObject.SetActive(false);
                    _parentroom.doorRight.gameObject.SetActive(false);

                    _newRoom.EntranceLeft.gameObject.SetActive(true);
                    _parentroom.EntranceRight.gameObject.SetActive(true);

                    _newRoom.archLeft.gameObject.SetActive(true);
                    _parentroom.archRight.gameObject.SetActive(true);
                }
            }
            else if (_newRoom.direction.y != 0)
            {
                if (_newRoom.direction.y < 0)
                {
                    _newRoom.doorTop.gameObject.SetActive(false);
                    _parentroom.doorBottom.gameObject.SetActive(false);

                    _newRoom.EntranceTop.gameObject.SetActive(true);
                    _parentroom.EntranceBottom.gameObject.SetActive(true);

                    _newRoom.archTop.gameObject.SetActive(true);
                    _parentroom.archBottom.gameObject.SetActive(true);
                }
                else
                {
                    _newRoom.doorBottom.gameObject.SetActive(false);
                    _parentroom.doorTop.gameObject.SetActive(false);

                    _newRoom.EntranceBottom.gameObject.SetActive(true);
                    _parentroom.EntranceTop.gameObject.SetActive(true);

                    _newRoom.archBottom.gameObject.SetActive(true);
                    _parentroom.archTop.gameObject.SetActive(true);
                }
            }

            

            for (int i = 0; i < distance; i++)
            {
                GameObject Hallway = new GameObject();
                if (_newRoom.direction.y != 0)
                    Hallway = Instantiate(ZYi, parentRoom.transform.position, parentRoom.transform.rotation); 
                else
                    Hallway = Instantiate(XYi, parentRoom.transform.position, parentRoom.transform.rotation);

                Hallway.transform.position = new Vector3(parentRoom.transform.position.x + _newRoom.direction.x * (parentSize / 2 + 0.5f  + i), parentRoom.transform.position.y + _newRoom.direction.y * (parentSize / 2 + 0.5f + i));

            }
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
            parentRoom.desiredDirection = newRoom.desiredDirection;
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

    private Vector2 SetNewBranchDirection(Room room)
    {
        Vector2 direction = new Vector2
        {
            y = (int)room.desiredDirection.y,
            x = (int)room.desiredDirection.x * -1
        };

        return direction;
    }


}
