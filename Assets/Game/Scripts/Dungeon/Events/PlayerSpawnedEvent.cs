using Game.Scripts.Camera;
using Game.Scripts.Dungeon.Living_Entities;
using Game.Scripts.Dungeon.Living_Entities.Player;
using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
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
}
