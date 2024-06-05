using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDestroyer : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    private void Destroy()
    {
        Destroy(parentObject.transform.gameObject);
    }
}
