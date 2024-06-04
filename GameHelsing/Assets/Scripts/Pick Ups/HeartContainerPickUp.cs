using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainerPickUp : MonoBehaviour
{
    [SerializeField]
    private int addedMaxHealth;

    public int GetAddedMaxHealth()
    {
        return addedMaxHealth;
    }
}
