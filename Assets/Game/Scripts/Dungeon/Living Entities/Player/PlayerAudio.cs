using Game.Audio.Scripts;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
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
            damageable.HealthChangedEvent += OnHealthChanged;
        }

        private void OnDisable()
        {
            damageable.HealthChangedEvent -= OnHealthChanged;
        }

        private void OnHealthChanged(object sender, Damageable.HealthChangedEventArgs healthChangedEventArgs)
        {
            if (healthChangedEventArgs.AddedHealth < 0)
                gotDamageSound.Play();   
        }

        public void OnStep() => stepSound.Play();
    }
}
