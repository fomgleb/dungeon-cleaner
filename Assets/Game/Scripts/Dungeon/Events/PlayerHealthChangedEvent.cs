using Game.Scripts.Dungeon.Living_Entities;
using Game.Scripts.Dungeon.Living_Entities.Player;
using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
    public class PlayerHealthChangedEvent : MonoBehaviour
    {
        [SerializeField] private GameObjectSpawner playerSpawner;

        [SerializeField] private PlayerHealthUI playerHealthUI;

        private Damageable playerDamageable;
    
        private void OnEnable()
        {
            playerSpawner.SpawnedEvent += OnPlayerSpawned;
            if (playerDamageable != null)
                playerDamageable.HealthChangedEvent += OnPlayerHealthChanged;
        }

        private void OnDisable()
        {
            playerSpawner.SpawnedEvent -= OnPlayerSpawned;
            if (playerDamageable != null)
                playerDamageable.HealthChangedEvent -= OnPlayerHealthChanged;
        }

        private void OnPlayerSpawned()
        {
            playerDamageable = playerSpawner.SpawnedObject.GetComponent<Damageable>();
            playerDamageable.HealthChangedEvent += OnPlayerHealthChanged;
        }

        private void OnPlayerHealthChanged(object sender, Damageable.HealthChangedEventArgs e)
        {
            if (playerDamageable.Health <= 0)
                playerDamageable = null;
        
            playerHealthUI.SetHealthText();
        }
    }
}
