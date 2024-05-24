using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraHeight = -10f;
    private GameObject Player;
    private Vector3 setCameraPosition;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        setCameraPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, cameraHeight);
        transform.position = setCameraPosition;
    }
}
