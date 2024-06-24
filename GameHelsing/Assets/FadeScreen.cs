using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] List<GameObject> Buttons = new List<GameObject>();
    public void ActivateButton()
    {
        Buttons.ForEach(Button => { 
            Button.SetActive(true);
        });
    }
}
