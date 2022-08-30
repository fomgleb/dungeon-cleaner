using System;
using NTC.Global.Pool;
using UnityEngine;
using UnnamedGame.LivingEntities.Enemies.Slime.Scripts;
using UnnamedGame.Weapon.Scripts;

namespace UnnamedGame.Weapon.Knife.Scripts
{
    [RequireComponent(typeof(WeaponAttack))]
    public class KnifeAttack : MonoBehaviour, IWeaponAttack
    {
        public event Action HitEnemyEvent;
        public event Action HitWallEvent;

        public void Attack()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.GetComponent<SlimeAI>() != null)
            {
                NightPool.Despawn(otherCollider);
                HitEnemyEvent?.Invoke();
            }
            else if (otherCollider.CompareTag("Wall"))
            {
                HitWallEvent?.Invoke();
            }
        }
    }
}