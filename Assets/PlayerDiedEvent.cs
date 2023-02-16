using Game.Camera.Scripts;
using Game.Dungeon.Scripts;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;

public class PlayerDiedEvent : MonoBehaviour
{
    [SerializeField] private GameObjectSpawner playerSpawner;
    [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;
    [SerializeField] private CameraTarget cameraTarget;
    [SerializeField] private Window menuWindow;
    [SerializeField] private Window youDiedWindow;

    private Damageable playerDamageable;

    private void OnEnable()
    {
        playerSpawner.SpawnedEvent += OnPlayerSpawned;
        if (playerDamageable != null)
            playerDamageable.DiedEvent += OnPlayerDied;
    }

    private void OnDisable()
    {
        playerSpawner.SpawnedEvent -= OnPlayerSpawned;
        if (playerDamageable != null)
            playerDamageable.DiedEvent -= OnPlayerDied;
    }

    private void OnPlayerSpawned()
    {
        playerDamageable = playerSpawner.SpawnedObject.GetComponent<Damageable>();
        playerDamageable.DiedEvent += OnPlayerDied;
    }

    private void OnPlayerDied(object sender, Damageable.DiedEventArgs e)
    {
        playerDamageable = null;
        
        dungeonMusicPlayer.StopAbruptly();
        cameraTarget.StopLookingAtMouse();
        
        menuWindow.ShowAsync();
        youDiedWindow.ShowAsync();
    }
}
