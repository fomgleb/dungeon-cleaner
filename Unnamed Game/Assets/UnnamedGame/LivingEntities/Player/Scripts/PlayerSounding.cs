using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Damageable))]
    public class PlayerSounding : MonoBehaviour
    {
        [SerializeField] private AudioClip gotDamageSound;

        private Damageable _damageable;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _damageable.GotDamageEvent += OnGotDamage;
        }

        private void OnDisable()
        {
            _damageable.GotDamageEvent -= OnGotDamage;
        }

        private void OnGotDamage() => _audioSource.PlayOneShot(gotDamageSound);

    }
}
