using System;
using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;

namespace UnnamedGame.LivingEntities.Enemies.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private float damage;

        public event Action PlayerGotDamageEvent;
        
        public Damageable Player { get; set; }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherDamageable = other.collider.GetComponent<Damageable>();
            if (!other.collider.CompareTag("Player") || otherDamageable == null) return;
            Player.PerformDamage(damage);
            PlayerGotDamageEvent?.Invoke();
        }
    }
}