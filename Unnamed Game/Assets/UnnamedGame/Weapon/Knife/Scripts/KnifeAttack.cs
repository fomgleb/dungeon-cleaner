using System;
using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;
using UnnamedGame.Weapon.Scripts;

namespace UnnamedGame.Weapon.Knife.Scripts
{
    [RequireComponent(typeof(WeaponAttack))]
    public class KnifeAttack : MonoBehaviour, IWeaponAttack
    {
        public event Action HitEnemyEvent;
        public event Action HitWallEvent;

        private WeaponAttack _weaponAttack;
        
        private void Awake()
        {
            _weaponAttack = GetComponent<WeaponAttack>();
        }

        public void Attack()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("Enemy"))
            {
                var enemyDamageable = otherCollider.GetComponent<Damageable>();
                enemyDamageable.PerformDamage(_weaponAttack.Damage);
                HitEnemyEvent?.Invoke();
            }
            else if (otherCollider.CompareTag("Wall"))
            {
                HitWallEvent?.Invoke();
            }
        }
    }
}