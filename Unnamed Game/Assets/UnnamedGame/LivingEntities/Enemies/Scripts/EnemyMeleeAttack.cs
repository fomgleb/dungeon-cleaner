using System;
using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using UnnamedGame.LivingEntities.Scripts;
using Zenject;

namespace UnnamedGame.LivingEntities.Enemies.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private float damage;

        public event Action PlayerGotDamageEvent;
        
        [Inject] private PlayerInput playerInput;
        private Damageable _playerDamageable;
        
        
        private void Awake()
        {
            _playerDamageable = playerInput.GetComponent<Damageable>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherDamageable = other.collider.GetComponent<Damageable>();
            if (!other.collider.CompareTag("Player") || otherDamageable == null) return;
            _playerDamageable.PerformDamage(damage);
            PlayerGotDamageEvent?.Invoke();
        }
    }
}