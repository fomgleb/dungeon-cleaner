using System;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Entities.LivingEntities.Scripts
{
    [RequireComponent(typeof(Damageable))]
    public class LootDropperWhenDie : MonoBehaviour
    {
        [SerializeField] private Loot[] loots;

        private Damageable damageable;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            damageable.DiedEvent += OnDied;
        }

        private void OnDisable()
        {
            damageable.DiedEvent -= OnDied;
        }

        private void OnDied(Damageable obj)
        {
            foreach (var loot in loots)
            {
                if (Random.Range(0f, 1f) < loot.DropChance)
                    LeanPool.Spawn(loot.DropPrefab, transform.position, Quaternion.identity);
            }
        }

        [Serializable]
        private struct Loot
        {
            [SerializeField] private GameObject dropPrefab;
            [SerializeField, Range(0, 1)] private float dropChance;
            public GameObject DropPrefab => dropPrefab;
            public float DropChance => dropChance;
        }
    }
}
