using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartBarManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerData playerData;
    private List<BarHeart> hearts = new List<BarHeart>();

    private void OnEnable()
    {
        PlayerData.OnHealthDataChanged += DrawHearts;
    }
    private void OnDisable()
    {
        PlayerData.OnHealthDataChanged -= DrawHearts;
    }
    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        ClearHearts();

        int maxHearths = playerData.GetMaxHealth();
        int playerHealth = playerData.GetHealth();
        float maxHeartsRemainder = maxHearths % 2;

        int heartsToMake = (int)((maxHearths / 2) + maxHeartsRemainder);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth - (i*2), 0, 2);
            hearts[i].SetHearthImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform);

        BarHeart newHeartComponent = newHeart.GetComponent<BarHeart>();
        newHeartComponent.SetHearthImage(HeartStatus.Empty);
        hearts.Add(newHeartComponent);
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<BarHeart>();
    }
}
