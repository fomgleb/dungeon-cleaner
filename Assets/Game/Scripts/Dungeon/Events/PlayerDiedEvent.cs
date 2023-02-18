using Game.Scripts.Camera;
using Game.Scripts.Dungeon.Living_Entities;
using Game.Scripts.Dungeon.Menus;
using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
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
}
