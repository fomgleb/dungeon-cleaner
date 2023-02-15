using Game.Dungeon.Scripts;
using Game.Entities.LivingEntities.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private GameObjectSpawner playerSpawner;

        private Damageable playerDamageable;
    
        private void OnEnable()
        {
            playerSpawner.SpawnedEvent += OnPlayerSpawned; 
        }

        private void OnDisable()
        {
            playerSpawner.SpawnedEvent -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned()
        {
            playerDamageable = playerSpawner.SpawnedObject.GetComponent<Damageable>();
            playerDamageable.HealthChangedEvent += OnPlayerHealthChanged;
            SetHealthText();
        }

        private void OnPlayerHealthChanged(object sender, Damageable.HealthChangedEventArgs healthChangedEventArgs)
        {
            if (healthChangedEventArgs.AddedHealth != 0)
                SetHealthText();
        }

        private void SetHealthText()
        {
            healthText.text = $"{playerDamageable.Health} / {playerDamageable.MaxHealth}";
        }
    }
}
