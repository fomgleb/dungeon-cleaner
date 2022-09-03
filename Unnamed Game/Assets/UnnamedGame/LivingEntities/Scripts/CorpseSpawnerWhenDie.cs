using System;
using NTC.Global.Pool;
using UnityEngine;

namespace UnnamedGame.LivingEntities.Scripts
{
    [RequireComponent(typeof(Damageable))]
    public class CorpseSpawnerWhenDie : MonoBehaviour
    {
        [SerializeField] private GameObject corpsePrefab;

        public event Action CorpseSpawnedEvent;
        
        private Damageable _damageable;
        
        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            _damageable.DiedEvent += OnDied;
        }

        private void OnDisable()
        {
            _damageable.DiedEvent -= OnDied;
        }

        private void OnDied(Damageable damageable) => SpawnCorpse();

        private void SpawnCorpse()
        {
            NightPool.Spawn(corpsePrefab, transform.position);
            CorpseSpawnedEvent?.Invoke();
        }
    }
}
