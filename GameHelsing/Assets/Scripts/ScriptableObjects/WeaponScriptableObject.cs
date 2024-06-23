using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName ="Weapon/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject particlesPrefab;
    [SerializeField] public RuntimeAnimatorController  animatorPrefab;

    [Header("Shooting parameters")]
    [SerializeField] public float bulletMoveSpeed;
    [SerializeField] public float bulletDamage;
    [SerializeField] public float bulletLifeTime;
    [SerializeField] public float timeBetweenShots;
    [SerializeField] public float reloadTime;
    [SerializeField] public int magSize;
    [SerializeField] public float randomOffsetMax;
    [SerializeField] public float randomOffsetMin;
    [SerializeField] public int numBullets;

    [Header("Visual parameters")]
    [SerializeField] public bool enableYflip = false;
    [SerializeField] public Sprite defaultSprite;
    [SerializeField] public Sprite spriteOutline;
    [SerializeField] public Vector3 shotPosition;
    [SerializeField] public Quaternion SpriteRotation;
    [SerializeField] public float gunShift;
}
