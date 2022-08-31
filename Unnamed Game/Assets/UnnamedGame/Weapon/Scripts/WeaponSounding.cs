using UnityEngine;

namespace UnnamedGame.Weapon.Scripts
{
    [RequireComponent(typeof(WeaponAttack))]
    [RequireComponent(typeof(AudioSource))]
    public class WeaponSounding : MonoBehaviour
    {
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip hitEnemySound;
        [SerializeField] private AudioClip hitWallSound;

        private WeaponAttack _weaponAttack;
        private IWeaponAttack _certainWeaponAttack;
        private AudioSource _audioSource;

        private void Awake()
        {
            _weaponAttack = GetComponent<WeaponAttack>();
            _certainWeaponAttack = GetComponent<IWeaponAttack>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _weaponAttack.AttackedEvent += OnAttacked;
            _certainWeaponAttack.HitEnemyEvent += OnHitEnemy;
            //_certainWeaponAttack.HitWallEvent += OnHitWall;
        }

        private void OnDisable()
        {
            _weaponAttack.AttackedEvent -= OnAttacked;
            _certainWeaponAttack.HitEnemyEvent -= OnHitEnemy;
            //_certainWeaponAttack.HitWallEvent -= OnHitWall;
        }
        
        private void OnAttacked() => PlayAttackSound();
        private void OnHitEnemy() => PlayHitEnemySound();
        private void OnHitWall() => PlayerHitWallSound();

        private void PlayAttackSound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(attackSound);
        }
        
        private void PlayHitEnemySound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(hitEnemySound);
        }

        private void PlayerHitWallSound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(hitWallSound);
        }
    }
}
