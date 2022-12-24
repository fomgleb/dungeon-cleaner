using System;
using Game.Scripts;
using UnityEngine;

namespace Game.Entities.Scripts
{
    public class EntitySpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private GameObject entityPrefab;
        [SerializeField] private Transform spawnPoint;

        public event Action SpawnedEvent;

        private GameObject spawnedObject;
        
        public GameObject SpawnedObject => spawnedObject;
        
        public void Spawn()
        {
            spawnedObject = Instantiate(entityPrefab, spawnPoint.position, Quaternion.identity, transform);
            SpawnedEvent?.Invoke();
        }
    }
}