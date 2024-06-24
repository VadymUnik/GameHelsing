using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SendScoreButton : MonoBehaviour
{
    private ScoreSender _scoreSender;
    private ScoreCounter _scoreCounter;
    [SerializeField] private List<GameObject> removeObjects;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI ScoreErrorText;

    private void Start()
    {
        _scoreSender = GameObject.FindGameObjectWithTag("ScoreSender").GetComponent<ScoreSender>();
        _scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
    }
    public void SendScorePressed()
    {
        if (inputField.text.Length > 2)
        {
            if (_scoreSender.SendScore(inputField.text, _scoreCounter.playerScore))
            {
                ScoreErrorText.text = "Score has been submitted successfully";
                removeObjects.ForEach(item => { 
                    item.SetActive(false);
                });
            }
            else
            {
                ScoreErrorText.text = "Error: Cannot connect to the server";
            }
        }
        else
        {
            ScoreErrorText.text = "Error: Name has to be at least 3 characters long";
        }
    }
}
