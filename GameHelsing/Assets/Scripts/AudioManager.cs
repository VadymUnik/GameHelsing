using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip GateOpening;
    public AudioClip CrateDestroyed;


    public void PlaySound(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
