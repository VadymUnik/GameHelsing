using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParcticles : MonoBehaviour
{
    
    [SerializeField] private GameObject particlesPrefab;

    [SerializeField] private GameObject particleSource;

    public void SpawnParticles()
    {
        Instantiate(particlesPrefab, particleSource.transform.position, Quaternion.Euler(0,0,0));
    }
}
