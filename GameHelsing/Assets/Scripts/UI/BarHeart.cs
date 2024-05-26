using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarHeart : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart, halfHeart, emptyHeart, blueFullHeart, blueHalfHeart;

    private Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHearthImage(HeartStatus status)
    {
        switch(status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }
    public void SetBlueHearthImage(BlueHeartStatus status)
    {
        switch(status)
        {
            case BlueHeartStatus.Half:
                heartImage.sprite = blueHalfHeart;
                break;
            case BlueHeartStatus.Full:
                heartImage.sprite = blueFullHeart;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}

public enum BlueHeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}