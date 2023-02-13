using System;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        public static event Action<GameObject> PlayerSpawnedEvent; 

        private GameObject spawnedPlayer;
        
        public GameObject SpawnedPlayer => spawnedPlayer;

        private void Start()
        {
            spawnedPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity, transform);
            PlayerSpawnedEvent?.Invoke(spawnedPlayer);
        }
    }
}
