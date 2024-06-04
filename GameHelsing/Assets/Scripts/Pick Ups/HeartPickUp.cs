using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [Header("Heart info")]
    [SerializeField] private int healthAmount;
    [SerializeField] private PickupHeartTypes heartType;
    [SerializeField] private HeartSpriteSetter spriteSetter;

    [SerializeField] private Sprite fullHeart, halfHeart, emptyHeart, blueFullHeart, blueHalfHeart;


    private void Awake()
    {
        SelectSprite(spriteSetter);
    }
    public int GetHealthAmount()
    {
        return healthAmount;
    }
    public PickupHeartTypes GetHeartType()
    {
        return heartType;
    }

    private void SelectSprite(HeartSpriteSetter spriteSetter)
    {
        switch(heartType)
        {
            case PickupHeartTypes.Container:
                spriteSetter.SetSprite(emptyHeart);
                break;
            case PickupHeartTypes.Red:
                spriteSetter.SetSprite(healthAmount > 1 ? fullHeart : halfHeart);
                break;
            case PickupHeartTypes.Blue:
                spriteSetter.SetSprite(healthAmount > 1 ? blueFullHeart : blueHalfHeart);
                break;
        }
    }
}

public enum PickupHeartTypes
{
    Container = 0,
    Red = 1,
    Blue = 2,
}

