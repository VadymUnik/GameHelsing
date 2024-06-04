using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }



    public void Flash()
    {
        sprite.color = new Color (1f, 1f, 1f, 0f);
        StopAllCoroutines();
        StartCoroutine( ChangeSpeed( 0f, 1f, 0.1f ) );
    }

    
    IEnumerator ChangeSpeed( float v_start, float v_end, float duration )
    {
        float alpha = 0f;
        float elapsed = 0.0f;
        while (elapsed < (duration / 2) )
        {
            alpha = Mathf.Lerp( v_start, v_end, elapsed / (duration / 2) );
            elapsed += Time.deltaTime;
            sprite.color = new Color (1f, 1f, 1f, alpha); 
            yield return null;
        }

        elapsed = 0.0f;
        while (elapsed < duration )
        {
            alpha = Mathf.Lerp( v_end, v_start, elapsed / duration );
            elapsed += Time.deltaTime;
            sprite.color = new Color (1f, 1f, 1f, alpha); 
            yield return null;
        }
        sprite.color = new Color (1f, 1f, 1f, 0f);
    }
}
