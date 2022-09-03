using UnityEngine;
using UnnamedGame.Audio.Scripts;
using UnnamedGame.LivingEntities.Scripts;
using Zenject;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerSounding : MonoBehaviour
    {
        [SerializeField] private AudioClip gotDamageSound;

        [Inject] private GlobalAudioPlayer _globalAudioPlayer;
        
        private Damageable _damageable;
        
        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            _damageable.GotDamageEvent += OnGotDamage;
        }

        private void OnDisable()
        {
            _damageable.GotDamageEvent -= OnGotDamage;
        }

        private void OnGotDamage() => _globalAudioPlayer.PlayAudio(gotDamageSound, transform.position);

    }
}
