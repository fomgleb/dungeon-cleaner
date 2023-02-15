using Game.Camera.Scripts;
using Game.Dungeon.Scripts;
using Game.Entities.LivingEntities.Player.Scripts;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;

public class PlayerSpawnedEvent : MonoBehaviour
{
    [SerializeField] private GameObjectSpawner playerSpawner;

    [SerializeField] private PlayerHealthUI playerHealthUI;
    [SerializeField] private CameraTarget cameraTarget;

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
        playerHealthUI.Init(playerSpawner.SpawnedObject.GetComponent<Damageable>());
        playerHealthUI.SetHealthText();
        cameraTarget.LookAtMouseAsync(playerSpawner.SpawnedObject.transform);
    }
}
