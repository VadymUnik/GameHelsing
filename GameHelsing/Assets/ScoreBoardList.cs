using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardList : MonoBehaviour
{
    [SerializeField] ScoreReceiver _scoreReceiver;
    [SerializeField] TextMeshProUGUI ScoreText;
    void OnEnable()
    {
        _scoreReceiver = _scoreReceiver = GameObject.FindGameObjectWithTag("ScoreReceiver").GetComponent<ScoreReceiver>();
        ScoreText.text = _scoreReceiver.RequestTopScores();
    }
}
