using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private WeaponScriptableObject weapon;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer outLine;

    [SerializeField] private int bulletsLeft;

    public void OnEnable()
    {
        bulletsLeft = weapon.magSize;
        SetUp();
    }
    public void SetUp()
    {
        sprite.transform.localRotation = weapon.SpriteRotation;
        outLine.transform.localRotation = weapon.SpriteRotation;
        sprite.sprite = weapon.defaultSprite;
        outLine.sprite = weapon.spriteOutline;
    }

    public void ChangeWeapon(WeaponScriptableObject newWeapon, int bulletsLeft)
    {
        weapon = newWeapon;
        this.bulletsLeft = bulletsLeft;
        SetUp();
    }

    public WeaponScriptableObject GetWeapon()
    {
        return weapon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            outLine.color = new Color(1,1,1,1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            outLine.color = new Color(0,0,0,0);
        }
    }

    public int GetBulletsLeft()
    {
        return bulletsLeft;
    }
}
