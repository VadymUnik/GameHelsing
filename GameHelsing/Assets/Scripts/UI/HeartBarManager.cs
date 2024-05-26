using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartBarManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerData playerData;
    private List<BarHeart> hearts = new List<BarHeart>();

    int redHeartsCount = 0;
    int blueHeartsCount = 0;

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

        int playerBlueHealth = playerData.GetBlueHealth();
        
        float maxHeartsRemainder = maxHearths % 2;
        float blueHeartsRemainder = playerBlueHealth % 2;

        int heartsToMake = (int)((maxHearths / 2) + maxHeartsRemainder);
        int blueHeartsToMake = (int)((playerBlueHealth / 2) + blueHeartsRemainder);

        for(int i = 0; i < heartsToMake; i++)
        {
            redHeartsCount++;
            CreateEmptyHeart();
        }
        for(int i = 0; i < blueHeartsToMake; i++)
        {
            blueHeartsCount++;
            CreateEmptyHeart();
        }

        for(int i = 0; i < redHeartsCount; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth - (i*2), 0, 2);
            hearts[i].SetHearthImage((HeartStatus)heartStatusRemainder);
        }

        for(int i = redHeartsCount, j = 0; i < blueHeartsCount + redHeartsCount; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerBlueHealth - (j*2), 0, 2);
            hearts[i].SetBlueHearthImage((BlueHeartStatus)heartStatusRemainder);
            j++;
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
        redHeartsCount = 0;
        blueHeartsCount = 0;
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<BarHeart>();
    }
}
