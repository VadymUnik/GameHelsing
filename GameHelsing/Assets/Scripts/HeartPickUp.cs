using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField]
    private int addedHealth;

    public int GetAddedHealth()
    {
        return addedHealth;
    }
}
