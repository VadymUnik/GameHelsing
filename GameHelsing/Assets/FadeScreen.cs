using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] GameObject Button;
    public void ActivateButton()
    {
        Button.SetActive(true);
    }
}
