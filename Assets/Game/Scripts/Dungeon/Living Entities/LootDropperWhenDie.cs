using System;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Dungeon.Living_Entities
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

        private void OnDied(object sender, Damageable.DiedEventArgs diedEventArgs)
        {
            foreach (var loot in loots)
            {
                if (Random.Range(0f, 1f) <= loot.DropChance)
                {
                    var thisPosition = transform.position;
                    var spawnedLoot = LeanPool.Spawn(loot.DropPrefab, thisPosition, Quaternion.identity);
                    spawnedLoot.velocity = (thisPosition - diedEventArgs.Killer.position) * loot.StartSpeed;
                }
            }
        }

        [Serializable]
        private struct Loot
        {
            [SerializeField] private Rigidbody2D dropPrefab;
            [SerializeField, Range(0, 1)] private float dropChance;
            [SerializeField] private float startSpeed;
            public Rigidbody2D DropPrefab => dropPrefab;
            public float DropChance => dropChance;
            public float StartSpeed => startSpeed;
        }
    }
}
