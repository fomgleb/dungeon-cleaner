using Game.Entities.LivingEntities.Scripts;
using Game.Global.Audio.Scripts;
using UnityEngine;

namespace Game.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private Sound gotDamageSound;
        [SerializeField] private Sound stepSound;

        private Damageable damageable;
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            damageable.GotDamageEvent += OnGotDamage;
        }

        private void OnDisable()
        {
            damageable.GotDamageEvent -= OnGotDamage;
        }

        private void OnGotDamage() => gotDamageSound.Play();

        public void OnStep() => stepSound.Play();
    }
}
