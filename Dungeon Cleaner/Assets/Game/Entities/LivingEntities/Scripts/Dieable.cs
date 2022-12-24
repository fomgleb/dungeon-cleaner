using System;
using Lean.Pool;
using UnityEngine;

namespace Game.Entities.LivingEntities.Scripts
{
    [RequireComponent(typeof(Damageable))]
    public class Dieable : MonoBehaviour
    {
        [SerializeField] private GameObject corpsePrefab;
        [SerializeField] private bool destroyIfDie;

        public event EventHandler<DiedEventArgs> DiedEvent;
        public class DiedEventArgs : EventArgs
        {
            public readonly Transform Killer;
            public DiedEventArgs(Transform killer)
            {
                Killer = killer;
            }
        }
        
        private Damageable damageable;
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            damageable.HealthChangedEvent += OnHealthChanged;
        }
        
        private void OnDisable()
        {
            damageable.HealthChangedEvent -= OnHealthChanged;
        }

        private void OnHealthChanged(object sender, Damageable.HealthChangedEventArgs healthChangedEventArgs)
        {
            if (!(damageable.Health <= 0)) return;
            
            LeanPool.Spawn(corpsePrefab, transform.position, Quaternion.identity);
            DiedEvent?.Invoke(this, new DiedEventArgs(healthChangedEventArgs.HealthChanger));
            if (destroyIfDie)
                LeanPool.Despawn(gameObject);
        }
    }
}
