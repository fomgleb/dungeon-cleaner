using System;
using UnityEngine;

namespace Game.Dungeon.Scripts
{
    public class GameObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;
        
        public event Action SpawnedEvent;
        
        public GameObject SpawnedObject { get; private set; }

        public void Spawn()
        {
            SpawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            SpawnedEvent?.Invoke();
        }
    }
}
