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

        public event Action<Damageable> DiedEvent;
        public event Action GotDamageEvent;

        public void PerformDamage(float damage)
        {
            health -= damage;
            GotDamageEvent?.Invoke();
            if (health > 0) return;
            health = 0;
            SpawnAfterDie();
            DiedEvent?.Invoke(this);
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