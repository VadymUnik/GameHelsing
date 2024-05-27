using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Timeline;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startRoomPrefab;
    [SerializeField] private List<GameObject> roomPrefabs;
    [SerializeField] private List<GameObject> Rooms;
     [SerializeField] private List<GameObject> RoomsTwo;
     [SerializeField] private List<GameObject> RoomsThree;
    [SerializeField] private int roomsAmount;

    private bool UpperLeftOpen = true, UpperRightOpen = true , LowerLeftOpen = true, LowerRightOpen = true;


    void Start()
    {
        Rooms.Add(CreateRoom(transform.gameObject, startRoomPrefab, 0));

        for (int i = 0; i < roomsAmount; i++)
        {
            GameObject randomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            Rooms.Add(CreateRoom(Rooms[i], randomPrefab, 2));
        }

        RoomsTwo.Add(CreateRoom(transform.gameObject, startRoomPrefab, 0));
        for (int i = 0; i < roomsAmount; i++)
        {
            GameObject randomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            RoomsTwo.Add(CreateRoom(RoomsTwo[i], randomPrefab, 2));
        }

        RoomsThree.Add(CreateRoom(transform.gameObject, startRoomPrefab, 0));
        for (int i = 0; i < roomsAmount; i++)
        {
            GameObject randomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            RoomsThree.Add(CreateRoom(RoomsThree[i], randomPrefab, 2));
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
            newRoom.transform.position = parentRoom.transform.position + positionShift;
        }    
        
        return newRoom;
    }

    private Vector3 SetNewRoomDirection(float shift, Room parentRoom, Room newRoom)
    {
        Vector3 direction = new Vector3(0, shift, 0);

        int upOrSide = Random.Range(0, 9);

        if (parentRoom.desiredHorizontal == 0)
            {
                if(UpperLeftOpen && UpperRightOpen)
                {
                    int randomDirection = Random.Range(0 , 2);
                    if(randomDirection == 1)
                    {
                        newRoom.desiredHorizontal = -1;
                        UpperLeftOpen = false;
                    }
                    else
                    {
                        newRoom.desiredHorizontal = 1;
                        UpperRightOpen = false;
                    }
                }
                else
                {
                    if (UpperLeftOpen) newRoom.desiredHorizontal = -1;
                    if (UpperRightOpen) newRoom.desiredHorizontal = 1;
                }
            }
        else
        {
            newRoom.desiredHorizontal = parentRoom.desiredHorizontal;
        }



        if (parentRoom.desiredVertical == 0)
        {
            if(LowerLeftOpen && LowerRightOpen)
            {   
                int randomDirection = Random.Range(0 , 2);
                newRoom.desiredVertical = randomDirection == 0 ? -1 : 1;
                LowerLeftOpen = newRoom.desiredVertical == -1 ? false : true;
                LowerRightOpen = newRoom.desiredVertical == 1 ? false : true;
            }
            else
            {
                newRoom.desiredVertical = LowerLeftOpen ? -1 : newRoom.desiredVertical = 1;
            }
        }
        else
        {
            newRoom.desiredVertical = parentRoom.desiredVertical;
        }

        if (upOrSide > 4)
        {
            direction.Set(newRoom.desiredHorizontal * shift, 0, 0);
            return direction;
        }
        else{
            direction.Set(0, newRoom.desiredVertical * shift, 0);
            return direction;
        }
    }
}
