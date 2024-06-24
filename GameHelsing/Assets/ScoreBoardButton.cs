using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardButton : MonoBehaviour
{
    [SerializeField] List<GameObject> ObjectsToEnable;
    [SerializeField] List<GameObject> ObjectsToDisable;
    public void ScorePressed()
    {
        ObjectsToEnable.ForEach(item => { 
            item.SetActive(true);
        });

        ObjectsToDisable.ForEach(item => { 
            item.SetActive(false);
        });
    }
}
