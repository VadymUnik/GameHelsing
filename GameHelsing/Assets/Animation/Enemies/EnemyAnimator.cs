using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] EnemyData enemy;
    SpriteMask mask;
    SpriteRenderer sprite;

    void OnEnable()
    {
        mask = GetComponent<SpriteMask>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (mask.enabled && mask.sprite != sprite.sprite)
        {
            mask.sprite = sprite.sprite;
        }
    }

    private void DestroyEnemy()
    {
        enemy.DestroyEnemy();
    } 
}
