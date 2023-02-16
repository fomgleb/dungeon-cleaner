using Game.Dungeon.Scripts;
using Game.Entities.LivingEntities.Player.Scripts;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;

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
