using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private int damage;

    public int GetDamage()
    {
        return damage;
    }
}
