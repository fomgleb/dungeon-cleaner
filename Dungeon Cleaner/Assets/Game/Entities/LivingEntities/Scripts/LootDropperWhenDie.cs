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

        private Dieable dieable;

        private void Awake()
        {
            dieable = GetComponent<Dieable>();
        }

        private void OnEnable()
        {
            dieable.DiedEvent += OnDied;
        }

        private void OnDisable()
        {
            dieable.DiedEvent -= OnDied;
        }

        private void OnDied(object sender, Dieable.DiedEventArgs eventArgs)
        {
            foreach (var loot in loots)
            {
                if (Random.Range(0f, 1f) <= loot.DropChance)
                {
                    var thisPosition = transform.position;
                    var spawnedLoot = LeanPool.Spawn(loot.DropPrefab, thisPosition, Quaternion.identity);
                    spawnedLoot.velocity = (thisPosition - eventArgs.Killer.position) * loot.StartSpeed;
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
