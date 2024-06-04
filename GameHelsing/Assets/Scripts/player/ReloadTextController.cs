using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReloadTextController : MonoBehaviour
{
    public TextMeshPro reloadText;
    public float reloadCountDown;
    public bool isCounterSet = false;
    void Start()
    {
    }
    void Update()
    {
        
        reloadCountDown -= Time.deltaTime;
        reloadText.text = "Reloading:" + Mathf.RoundToInt(reloadCountDown);

        if (reloadCountDown <= 0 && isCounterSet)
        {
            Destroy(gameObject);
        }
    }

    public void SetReloadTime(float reloadTime){
        this.reloadCountDown = reloadTime;
        isCounterSet = true;
    }
}
