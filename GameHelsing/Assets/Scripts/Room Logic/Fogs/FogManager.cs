using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] List<FogHallway> HallFogs;
    [SerializeField] FogRoom RoomFog;

    public void RevealRoomFog()
    {
        RoomFog.Reveal();
    }

    public void HideRoomFog()
    {
        RoomFog.Hide();
    }

    public void RevealHallFog()
    {
        HallFogs.ForEach(fog => { 
            fog.Reveal();
        }); 
    }
}