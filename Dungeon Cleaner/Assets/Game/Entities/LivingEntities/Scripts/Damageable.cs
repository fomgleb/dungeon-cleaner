using System;
using Lean.Pool;
using UnityEngine;

namespace Game.Entities.LivingEntities.Scripts
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private GameObject[] spawnAfterDiePrefabs;
        [SerializeField] private bool destroyAfterDie;

        public float Health => health;
        public float MaxHealth => maxHealth;

        public event EventHandler<DiedEventArgs> DiedEvent;
        public class DiedEventArgs : EventArgs
        {
            public readonly Transform Killer;
            public DiedEventArgs(Transform killer)
            {
                Killer = killer;
            }
        }
        
        public event EventHandler<HealthChangedEventArgs> HealthChangedEvent;
        public class HealthChangedEventArgs : EventArgs
        {
            public readonly Transform HealthChanger;
            public readonly float AddedHealth;
            public HealthChangedEventArgs(Transform healthChanger, float addedHealth)
            {
                HealthChanger = healthChanger;
                AddedHealth = addedHealth;
            }
        }

        public void AddToHealth(Transform healthChanger, float addingAmount)
        {
            health += addingAmount;
            if (health > maxHealth)
                health = maxHealth;
            else if (health < 0)
                health = 0;
            
            HealthChangedEvent?.Invoke(gameObject, new HealthChangedEventArgs(healthChanger, addingAmount));

            if (health > 0)
                return;
            
            SpawnAfterDie();
            DiedEvent?.Invoke(gameObject, new DiedEventArgs(healthChanger));
            if (destroyAfterDie)
                Destroy(gameObject);
        }

        private void SpawnAfterDie()
        {
            foreach (var spawnAfterDiePrefab in spawnAfterDiePrefabs)
            {
                LeanPool.Spawn(spawnAfterDiePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}