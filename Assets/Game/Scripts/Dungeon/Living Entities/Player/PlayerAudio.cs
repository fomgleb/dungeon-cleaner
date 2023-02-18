using Game.Scripts.Audio;
using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
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
