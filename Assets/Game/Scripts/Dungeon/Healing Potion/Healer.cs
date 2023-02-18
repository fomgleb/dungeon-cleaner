using System.Linq;
using Game.Scripts.Audio;
using Game.Scripts.Dungeon.Living_Entities;
using Lean.Pool;
using UnityEngine;

namespace Game.Scripts.Dungeon.Healing_Potion
{
    [RequireComponent(typeof(Collider2D))]
    public class Healer : MonoBehaviour
    {
        [SerializeField] private float healingAmount;
        [SerializeField] private Sound healSound;
        [SerializeField] private Damageable[] canHealPrefabs;

        private GlobalAudioPlayer globalAudioPlayer;

        private void Awake()
        {
            globalAudioPlayer = GameObject.FindWithTag(nameof(GlobalAudioPlayer)).GetComponent<GlobalAudioPlayer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var foundDamageablePrefab = canHealPrefabs.FirstOrDefault(obj => obj.CompareTag(collision.gameObject.tag));
            if (foundDamageablePrefab == null)
                return;
            var collidedDamageable = collision.gameObject.GetComponent<Damageable>();
            if (collidedDamageable.Health == collidedDamageable.MaxHealth)
                return;
            collidedDamageable.AddToHealth(transform, healingAmount);
            globalAudioPlayer.PlayAudio(healSound, transform.position);
            LeanPool.Despawn(this);
        }
    }
}
