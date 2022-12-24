using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.LivingEntities.Enemies.Scripts
{
    public class SpawnedObjectFinder : MonoBehaviour
    {
        [SerializeField] private GameObject objectPrefab;

        private List<Component> spawnedObjectComponentsList;
        
        private GameObject spawnedGameObject;
        public GameObject SpawnedGameObject
        {
            get
            {
                if (spawnedGameObject != null && spawnedGameObject.activeSelf) return spawnedGameObject;
                spawnedGameObject = GameObject.FindWithTag(objectPrefab.tag);
                return spawnedGameObject;
            }
        }

        public string Tag => SpawnedGameObject != null ? spawnedGameObject.tag : null;

        private void Awake()
        {
            spawnedObjectComponentsList = new List<Component>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public T LightweightGetComponent<T>() where T : Component
        {
            if (SpawnedGameObject.Equals(null) || !spawnedGameObject.activeSelf) return default;

            var spawnedObjectComponent = (T)spawnedObjectComponentsList.Find(c => c.GetType() == typeof(T));
            if (!spawnedObjectComponent.Equals(null)) return spawnedObjectComponent;
            
            spawnedObjectComponent = spawnedObjectComponent.GetComponent<T>();
            if (spawnedObjectComponent.Equals(null)) return default;
            
            spawnedObjectComponentsList.Add(spawnedObjectComponent);
            
            return spawnedObjectComponent;
        }
    }
}