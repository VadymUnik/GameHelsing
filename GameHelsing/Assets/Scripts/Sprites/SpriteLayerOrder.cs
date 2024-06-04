using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteLayerOrder : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    GameObject Player;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        sprite.sortingOrder = Player.transform.position.y > transform.position.y ? 8 : -8;
    }
}
