using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpriteSetter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = new SpriteRenderer();

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
}
