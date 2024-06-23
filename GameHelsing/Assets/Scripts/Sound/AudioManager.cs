using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    [Header("Misc")]
    public AudioClip GateOpening;
    public AudioClip CrateDestroyed;
    public AudioClip CrateSpawn;
    public AudioClip ItemPickUp;
    public AudioClip EnemySpawn;

    [Header("Weapons")]
    public AudioClip GlassShatter;
    public AudioClip Hit;
    public AudioClip Hit_Wall;
    public AudioClip Shoot;
    public AudioClip PickUp;

    [Header("Player")]
    public AudioClip Dash;
    public AudioClip PlayerDamaged;
    public AudioClip Death;
    //public AudioClip Walk;


    public void PlaySound(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
