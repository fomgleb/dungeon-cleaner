using UnityEngine;

namespace Game.Scripts.Dungeon.Weapon
{
    [RequireComponent(typeof(WeaponAttack))]
    [RequireComponent(typeof(AudioSource))]
    public class WeaponSounding : MonoBehaviour
    {
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip[] hitWallSounds;

        private WeaponAttack weaponAttack;
        private IWeaponAttack certainWeaponAttack;
        private AudioSource audioSource;

        private void Awake()
        {
            weaponAttack = GetComponent<WeaponAttack>();
            certainWeaponAttack = GetComponent<IWeaponAttack>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            weaponAttack.AttackedEvent += OnAttacked;
            certainWeaponAttack.HitWallEvent += OnHitWall;
        }

        private void OnDisable()
        {
            weaponAttack.AttackedEvent -= OnAttacked;
            certainWeaponAttack.HitWallEvent -= OnHitWall;
        }
        
        private void OnAttacked() => PlayAttackSound();
        private void OnHitWall() => PlayerHitWallSound();

        private void PlayAttackSound()
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(attackSound);
        }
        
        private void PlayerHitWallSound()
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(hitWallSounds[Random.Range(0, hitWallSounds.Length)]);
        }
    }
}
