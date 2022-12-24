using System;
using UnityEngine;

namespace Game.Entities.LivingEntities.Scripts
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        public float Health => health;
        public float MaxHealth => maxHealth;

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
        }
    }
}