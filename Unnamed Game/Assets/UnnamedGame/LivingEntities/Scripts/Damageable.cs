using System;
using UnityEngine;

namespace UnnamedGame.LivingEntities.Scripts
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        public float Health => health;
        public float MaxHealth => maxHealth;

        public event Action DiedEvent;
        public event Action GotDamageEvent;

        public void PerformDamage(float damage)
        {
            health -= damage;
            GotDamageEvent?.Invoke();
            if (health > 0) return;
            health = 0;
            DiedEvent?.Invoke();
        }
    }
}