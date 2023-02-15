using Game.Camera.Scripts;
using Game.Dungeon.Scripts;
using UnityEngine;

public class PlayerSpawnedEvent : MonoBehaviour
{
    [SerializeField] private GameObjectSpawner playerSpawner;

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
        cameraTarget.LookAtMouseAsync(playerSpawner.SpawnedObject.transform);
    }
}
