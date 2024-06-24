using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    public int playerScore = 0;

    public void Update()
    {
        ScoreText.text = $"Score: {playerScore}";
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void DecreaseScore(int amount)
    {
        playerScore -= amount;
    }
}
